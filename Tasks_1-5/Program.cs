using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите задание для выполнения:");
                Console.WriteLine("1 - Задание 1.7 (List)");
                Console.WriteLine("2 - Задание 2.7 (LinkedList)");
                Console.WriteLine("3 - Задание 3.7 (HashSet)");
                Console.WriteLine("4 - Задание 4.7 (HashSet с текстом)");
                Console.WriteLine("5 - Задание 5.7 (Dictionary)");
                Console.WriteLine("0 - Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();
                
                try
                {
                    switch (choice)
                    {
                        case "1":
                            ExecuteTask1();
                            break;
                        case "2":
                            ExecuteTask2();
                            break;
                        case "3":
                            ExecuteTask3();
                            break;
                        case "4":
                            ExecuteTask4();
                            break;
                        case "5":
                            ExecuteTask5();
                            break;
                        case "0":
                            Console.WriteLine("Выход из программы.");
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ExecuteTask1()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 1.7 - Перенос первого элемента в конец списка ===");
            
            Console.WriteLine("Введите элементы списка через пробел:");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: список не может быть пустым");
                return;
            }

            var items = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var list = new List<string>(items);
            
            Console.WriteLine($"Исходный список: [{string.Join(", ", list)}]");
            
            StaticTasks.MoveFirstToEnd(list);
            Console.WriteLine($"После переноса первого элемента в конец: [{string.Join(", ", list)}]");
        }

        static void ExecuteTask2()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 2.7 - Удаление элементов с одинаковыми соседями ===");
            
            Console.WriteLine("Введите элементы связанного списка через пробел:");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: список не может быть пустым");
                return;
            }

            var items = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var linkedList = new LinkedList<string>(items);
            
            Console.WriteLine($"Исходный список: [{string.Join(", ", linkedList)}]");
            
            StaticTasks.RemoveElementsWithEqualNeighbors(linkedList);
            Console.WriteLine($"После удаления элементов с одинаковыми соседями: [{string.Join(", ", linkedList)}]");
        }

        static void ExecuteTask3()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 3.7 - Анализ покупок мебели ===");
            
            Console.WriteLine("Введите названия фабрик через запятую:");
            string factoriesInput = Console.ReadLine();
            
            var allFactories = new HashSet<string>();
            if (!string.IsNullOrWhiteSpace(factoriesInput))
            {
                allFactories = new HashSet<string>(
                    factoriesInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(f => f.Trim())
                );
            }

            var customerPurchases = new Dictionary<string, HashSet<string>>();
            
            Console.WriteLine("\nВведите данные о покупках клиентов (для завершения введите 'end'):");
            Console.WriteLine("Формат: ИмяКлиента: Фабрика1, Фабрика2, ...");
            
            while (true)
            {
                Console.Write("> ");
                string purchaseInput = Console.ReadLine();
                
                if (purchaseInput?.ToLower() == "end" || string.IsNullOrWhiteSpace(purchaseInput))
                    break;
                
                var parts = purchaseInput.Split(':', 2);
                if (parts.Length == 2)
                {
                    string customer = parts[0].Trim();
                    var factories = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                           .Select(f => f.Trim())
                                           .ToHashSet();
                    customerPurchases[customer] = factories;
                }
                else
                {
                    Console.WriteLine("Неверный формат. Используйте: Имя: Фабрика1, Фабрика2");
                }
            }
            
            var (purchasedByAll, purchasedBySome, purchasedByNone) = 
                StaticTasks.AnalyzeFurniturePurchases(allFactories, customerPurchases);

            Console.WriteLine("\nФабрики, мебель которых приобреталась всеми покупателями:");
            foreach (var factory in purchasedByAll)
                Console.WriteLine($"  - {factory}");
            if (!purchasedByAll.Any()) Console.WriteLine("  (не найдено)");
                
            Console.WriteLine("\nФабрики, мебель которых приобреталась некоторыми покупателями:");
            foreach (var factory in purchasedBySome)
                Console.WriteLine($"  - {factory}");
                
            Console.WriteLine("\nФабрики, мебель которых не приобреталась никем:");
            foreach (var factory in purchasedByNone)
                Console.WriteLine($"  - {factory}");
            if (!purchasedByNone.Any()) Console.WriteLine("  (не найдено)");
        }

        static void ExecuteTask4()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 4.7 - Анализ русскоязычного текста ===");
            
            Console.WriteLine("Введите текст для анализа:");
            string text = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Ошибка: текст не может быть пустым");
                return;
            }

            Console.WriteLine($"\nАнализируемый текст: {text}");
            
            var result = StaticTasks.AnalyzeRussianText(text);
            
            Console.WriteLine("Глухие согласные, входящие в каждое нечетное слово и не входящие хотя бы в одно четное:");
            foreach (var consonant in result)
            {
                Console.WriteLine($"  - {consonant}");
            }
            
            if (!result.Any())
            {
                Console.WriteLine("  (не найдено)");
            }
        }

        static void ExecuteTask5()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 5.7 - Анализ цен на сметану ===");
            
            int n = Validator.GetValidatedInt("количество магазинов", 1, 100);

            var data = new List<string>();
            Console.WriteLine("\nВведите данные о магазинах в формате: Магазин Улица Жирность Цена");
            Console.WriteLine("Пример: Перекресток Короленко 25 3200");
            
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Магазин {i + 1}: ");
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    data.Add(input);
                }
            }

            string dataFile = "sour_cream_data.txt";
            StaticTasks.FillDataFile(dataFile, data);
            Console.WriteLine($"Файл {dataFile} успешно заполнен данными");
            
            var (count15, count20, count25) = StaticTasks.AnalyzeSourCreamPrices(dataFile);
            
            Console.WriteLine($"\nРезультат анализа:");
            Console.WriteLine($"{count15} {count20} {count25}");
            
            File.Delete(dataFile);
        }
    }
}