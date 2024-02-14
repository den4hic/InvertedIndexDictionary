﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndexDictionary
{
    internal class CoordinateIndexes
    {
        public static Dictionary<string, Dictionary<int, List<int>>> CoordinateIndexesResult { get; set; } = new Dictionary<string, Dictionary<int, List<int>>>();

	    public static void GetSearchWithCoordinateForManyWords(string s)
		{
			var words = s.Split(' ').Reverse().ToArray();
			HashSet<int> hashSet = new HashSet<int>();

            words = words.Select(word => word.ToLower()).ToArray();

            if(words.All(word => CoordinateIndexesResult.ContainsKey(word)))
            {
                List<Dictionary<int, List<int>>> dicts = new List<Dictionary<int, List<int>>>();
                foreach (var word in words)
                {
                    dicts.Add(CoordinateIndexesResult[word]);
                }

                int count = words.Length;

                for(int i = 0; i < count; i++)
                {
                    dicts[i] = dicts[i].ToDictionary(pair => pair.Key, pair => pair.Value.Select(value => value + i).ToList());
                }

                var answer = dicts[0].Join(dicts[1], pair => pair.Key, pair => pair.Key, (pair1, pair2) =>
                {
					var list1 = pair1.Value;
					var list2 = pair2.Value;
					var result = list1.Intersect(list2).ToList();

					if (result.Count != 0)
                    {
						hashSet.Add(pair1.Key);
					}

					return -1;
				}).ToList();

				ConsoleOutputSearchWithCoordinates(hashSet);
			}
		}
		public static void GetSearchWithCoordinate(string phraseToSearch)
        {
            var words = phraseToSearch.Split(' ');
            HashSet<int> hashSet = new HashSet<int>();

            if (CoordinateIndexesResult.ContainsKey(words[0]) && CoordinateIndexesResult.ContainsKey(words[1]))
            {
                var dict1 = CoordinateIndexesResult[words[0]];
                var dict2 = CoordinateIndexesResult[words[1]];
                var answer = dict1.Zip(dict2, (item1, item2) =>
                {
                    var list1 = item1.Value;
                    var list2 = item2.Value;
                    list1 = list1.Select(value => value + 1).ToList();

                    var result = list1.Intersect(list2).ToList();

                    if(result.Count != 0)
                    {
                        hashSet.Add(item1.Key);
                    }

                    return -1;
                }).ToList();

                ConsoleOutputSearchWithCoordinates(hashSet);
            }
        }

        public static void OutputCoordinateIndexes()
        {
            CoordinateIndexesResult = CoordinateIndexesResult.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (var word in CoordinateIndexesResult)
            {
                Console.WriteLine(word.Key + " ");
                foreach (var index in word.Value)
                {
                    Console.Write(index.Key + " -> ");
                    foreach (var value in index.Value)
                    {
                        Console.Write(value + " ");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void ConsoleOutputSearchWithCoordinates(HashSet<int> hashSet)
        {
            foreach (var index in hashSet)
            {
                Console.WriteLine(index + " -> " + FileReader.Books[index]);
            }
        }
	}
}
