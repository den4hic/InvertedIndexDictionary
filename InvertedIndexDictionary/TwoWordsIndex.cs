using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndexDictionary
{
    internal class TwoWordsIndex
    {
        public static Dictionary<string, List<int>> TwoWordsIndexResult { get; set; } = new Dictionary<string, List<int>>();

        public static void OutputTwoWordsIndex()
        {
            TwoWordsIndexResult = TwoWordsIndexResult.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (var word in TwoWordsIndexResult)
            {
                Console.Write(word.Key + " ");
                foreach (var wordValue in word.Value)
                {
                    Console.Write(wordValue + " ");
                }
                Console.WriteLine();
            }
        }

        public static void MakeInvertedTwoWordsIndex(List<Dictionary<string, List<int>>> allWords, int filesCount)
        {
            for (int i = 0; i < filesCount; i++)
            {
                foreach (var word in allWords[i])
                {
                    if (!TwoWordsIndexResult.ContainsKey(word.Key))
                    {
                        TwoWordsIndexResult[word.Key] = new List<int>();
                    }
                    TwoWordsIndexResult[word.Key].AddRange(word.Value);
                }
            }

            TwoWordsIndexResult = TwoWordsIndexResult.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
