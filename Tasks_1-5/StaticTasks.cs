using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab4_1
{
    public static class StaticTasks
    {
        // ЗАДАНИЕ 1.7 - List
        public static void MoveFirstToEnd<T>(List<T> list)
        {
            if (list == null || list.Count == 0)
                return;
                
            T firstElement = list[0];
            list.RemoveAt(0);
            list.Add(firstElement);
        }

        // ЗАДАНИЕ 2.7 - LinkedList
        public static void RemoveElementsWithEqualNeighbors<T>(LinkedList<T> list)
        {
            if (list == null || list.Count < 3)
                return;

            var nodesToRemove = new List<LinkedListNode<T>>();
            var current = list.First;

            while (current != null)
            {
                var previous = current.Previous ?? list.Last;
                var next = current.Next ?? list.First;

                if (previous.Value.Equals(next.Value))
                {
                    nodesToRemove.Add(current);
                }

                current = current.Next;
            }

            foreach (var node in nodesToRemove)
            {
                list.Remove(node);
            }
        }

        // ЗАДАНИЕ 3.7 - HashSet
        public static (IEnumerable<string> purchasedByAll, IEnumerable<string> purchasedBySome, IEnumerable<string> purchasedByNone) 
            AnalyzeFurniturePurchases(HashSet<string> allFactories, Dictionary<string, HashSet<string>> customerPurchases)
        {
            var purchasedByAll = allFactories.Where(f => 
                customerPurchases.Values.All(purchases => purchases.Contains(f)));
            
            var purchasedBySome = allFactories.Where(f => 
                customerPurchases.Values.Any(purchases => purchases.Contains(f)));
            
            var purchasedByNone = allFactories.Where(f => 
                customerPurchases.Values.All(purchases => !purchases.Contains(f)));

            return (purchasedByAll, purchasedBySome, purchasedByNone);
        }

        // ЗАДАНИЕ 4.7 - HashSet с текстом
        public static IEnumerable<char> AnalyzeRussianText(string text)
        {
            char[] deafConsonants = { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };
            
            var words = text.ToLower().Split(new[] { ' ', ',', '.', '!', '?', ';', ':', '\n', '\r', '\t' }, 
                                  StringSplitOptions.RemoveEmptyEntries);
            
            var oddWordsChars = new HashSet<char>();
            var evenWordsChars = new HashSet<char>();
            
            for (int i = 0; i < words.Length; i++)
            {
                var wordChars = words[i].Where(char.IsLetter);
                
                if ((i + 1) % 2 == 1)
                {
                    oddWordsChars.UnionWith(wordChars);
                }
                else
                {
                    evenWordsChars.UnionWith(wordChars);
                }
            }
            
            return deafConsonants.Where(c => 
                oddWordsChars.Contains(c) && !evenWordsChars.Contains(c))
                .OrderBy(c => c);
        }

        // ЗАДАНИЕ 5.7 - Dictionary
        
        public static void FillDataFile(string filePath, List<string> data)
        {
            File.WriteAllLines(filePath, data);
        }
        
        public static (int count15, int count20, int count25) AnalyzeSourCreamPrices(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            
            var prices15 = new List<int>();
            var prices20 = new List<int>();
            var prices25 = new List<int>();

            foreach (var line in lines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 4 && int.TryParse(parts[2], out int fat) && 
                    int.TryParse(parts[3], out int price))
                {
                    switch (fat)
                    {
                        case 15: prices15.Add(price); break;
                        case 20: prices20.Add(price); break;
                        case 25: prices25.Add(price); break;
                    }
                }
            }

            int count15 = prices15.Count > 0 ? prices15.Count(p => p == prices15.Min()) : 0;
            int count20 = prices20.Count > 0 ? prices20.Count(p => p == prices20.Min()) : 0;
            int count25 = prices25.Count > 0 ? prices25.Count(p => p == prices25.Min()) : 0;

            return (count15, count20, count25);
        }
    }
}