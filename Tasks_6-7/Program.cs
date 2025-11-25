using System;

namespace lab4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите задание для выполнения:");
                Console.WriteLine("1 - Задание 6.7 (Метод Subtract)");
                Console.WriteLine("2 - Задание 7.7 (Перегрузка операторов)");
                Console.WriteLine("0 - Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();
                
                try
                {
                    switch (choice)
                    {
                        case "1":
                            TestSubtractionMethod();
                            break;
                        case "2":
                            TestOperators();
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

        static void TestSubtractionMethod()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 6.7 - Тестирование метода Subtract ===");
            
            Console.WriteLine("Введите два времени для вычитания:");
            Time time1 = Validator.GetValidatedTime("первое время");
            Time time2 = Validator.GetValidatedTime("второе время");
            
            Console.WriteLine($"\nВремя 1: {time1}");
            Console.WriteLine($"Время 2: {time2}");
            
            try
            {
                Time result = time1.Subtract(time2);
                Console.WriteLine($"Результат вычитания (time1 - time2): {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при вычитании: {ex.Message}");
            }
        }

        static void TestOperators()
        {
            Console.WriteLine("\n=== ЗАДАНИЕ 7.7 - Тестирование перегрузки операторов ===");
            
            Console.WriteLine("Введите время для тестирования операторов:");
            Time time = Validator.GetValidatedTime("время");
            
            Console.WriteLine($"\nИсходное время: {time}");
            
            Console.WriteLine("\nУнарные операторы:");
            Time timePlusPlus = time++;
            Console.WriteLine($"time++ = {timePlusPlus} ");
            
            Time timeMinusMinus = time--;
            Console.WriteLine($"time-- = {timeMinusMinus} ");
            
            Console.WriteLine("\nОператоры приведения:");
            Console.WriteLine($"(bool)time = {(bool)time}");
            Console.WriteLine($"(int)time = {(int)time} минут");
            
            Console.WriteLine("\nВведите второе время для сравнения:");
            Time time2 = Validator.GetValidatedTime("второе время");
            Console.WriteLine($"\nСравнение {time} и {time2}:");
            Console.WriteLine($"time < time2 = {time < time2}");
            Console.WriteLine($"time > time2 = {time > time2}");
            
            Console.WriteLine("\nАрифметические операторы с минутами:");
            uint minutesToAdd = Validator.GetValidatedUint("минуты для сложения", 0);
            uint minutesToSubtract = Validator.GetValidatedUint("минуты для вычитания", 0);
            
            Console.WriteLine($"time + {minutesToAdd} минут = {time + minutesToAdd}");
            Console.WriteLine($"time - {minutesToSubtract} минут = {time - minutesToSubtract}");
            
            Console.WriteLine("\nАрифметические операторы с временем:");
            Console.WriteLine($"time + time2 = {time + time2}");
            Console.WriteLine($"time - time2 = {time - time2}");
        }
    }
}