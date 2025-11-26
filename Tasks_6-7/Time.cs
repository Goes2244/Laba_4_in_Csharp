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

        // ЗАДАНИЕ 3.7 - HashSet (фабрики)
        public static (IEnumerable<string> purchasedByAll, IEnumerable<string> purchasedBySome, IEnumerable<string> purchasedByNone) 
            AnalyzeFurniturePurchases(HashSet<string> allFactories, Dictionary<string, HashSet<string>> customerPurchases)
        {
            if (customerPurchases.Count == 0)
                return (new HashSet<string>(), new HashSet<string>(), allFactories);

            var purchasedByAll = new HashSet<string>(allFactories);
            var purchasedBySome = new HashSet<string>();

            foreach (var purchases in customerPurchases.Values)
            {
                purchasedByAll.IntersectWith(purchases);
                purchasedBySome.UnionWith(purchases);
            }

            var purchasedByNone = new HashSet<string>(allFactories);
            purchasedByNone.ExceptWith(purchasedBySome);

            return (purchasedByAll, purchasedBySome, purchasedByNone);
        }

        // ЗАДАНИЕ 4.7 - HashSet с текстом
        public static IEnumerable<char> AnalyzeRussianText(string text)
        {
            char[] deafConsonants = { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };
            
            var words = text.ToLower().Split(new[] { ' ', ',', '.', '!', '?', ';', ':', '\n', '\r', '\t' },
                StringSplitOptions.RemoveEmptyEntries);
            
            var nchetWordsChars = new HashSet<char>();
            var chetWordsChars = new HashSet<char>();
            
            for (int i = 0; i < words.Length; i++)
            {
                foreach (char c in words[i])
                {
                    if (!char.IsLetter(c)) continue;
                    
                    if ((i + 1) % 2 == 1)
                    {
                        nchetWordsChars.Add(c);
                    }
                    else
                    {
                        chetWordsChars.Add(c);
                    }
                }
            }
            
            var result = new HashSet<char>();
            foreach (char consonant in deafConsonants)
            {
                if (nchetWordsChars.Contains(consonant) && !chetWordsChars.Contains(consonant))
                {
                    result.Add(consonant);
                }
            }
            
            var sortedResult = new List<char>(result);
            sortedResult.Sort();
            return sortedResult;
        }

        // ЗАДАНИЕ 5.7 - Dictionary
        
        public static void FillDataFile(string filePath, List<string> data)
        {
            File.WriteAllLines(filePath, data);
        }
        
        public static (int count15, int count20, int count25) AnalyzeSourCreamPrices(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            
            var minPrices = new Dictionary<int, (int minPrice, int count)>
            {
                { 15, (int.MaxValue, 0) },
                { 20, (int.MaxValue, 0) },
                { 25, (int.MaxValue, 0) }
            };

            foreach (var line in lines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 4 && int.TryParse(parts[2], out int fat) && 
                    int.TryParse(parts[3], out int price))
                {
                    if (minPrices.ContainsKey(fat))
                    {
                        var current = minPrices[fat];
                        
                        if (price < current.minPrice)
                        {
                            minPrices[fat] = (price, 1);
                        }
                        else if (price == current.minPrice)
                        {
                            minPrices[fat] = (current.minPrice, current.count + 1);
                        }
                    }
                }
            }
            
            int count15 = minPrices[15].minPrice != int.MaxValue ? minPrices[15].count : 0;
            int count20 = minPrices[20].minPrice != int.MaxValue ? minPrices[20].count : 0;
            int count25 = minPrices[25].minPrice != int.MaxValue ? minPrices[25].count : 0;

            return (count15, count20, count25);
        }
    }
}
