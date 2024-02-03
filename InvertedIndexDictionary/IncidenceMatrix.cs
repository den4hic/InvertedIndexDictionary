using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndexDictionary
{
    internal class IncidenceMatrix
    {
        public Dictionary<string, List<bool>> Matrix { get; set; } = new Dictionary<string, List<bool>>();
        public List<string> Words { get; set; } = new List<string>();

        public IncidenceMatrix()
        {
            Matrix = Matrix.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public void AddWords(string[] words, int id)
        {
            foreach (var word in words)
            {
                string newWord = word.ToLower().Trim();
                if (!Matrix.ContainsKey(newWord))
                {
                    Matrix.Add(newWord, new List<bool>());
                    foreach (var _ in Enumerable.Range(0, 10))
                    {
                        Matrix[newWord].Add(false);
                    }
                    Matrix[newWord][id] = true;
                } else
                {
                    Matrix[newWord][id] = true;
                }
            }
        }

        public void ConsoleOutput()
        {
            foreach (var word in Matrix)
            {
                Console.Write(word.Key + " ");
                foreach (var wordValue in word.Value)
                {
                    Console.Write(wordValue + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
