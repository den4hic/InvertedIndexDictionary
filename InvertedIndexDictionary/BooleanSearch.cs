using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndexDictionary
{
    internal class BooleanSearch
    {
        public List<bool> ResultIncidenceMatrixList { get; private set; } = new List<bool>();

        public static List<bool> OperationForIndenceMatrix(List<bool> firstRow, List<bool> secondRow, string operation)
        {
            switch (operation)
            {
                case "AND":
                    return firstRow.Zip(secondRow, (a, b) => a && b).ToList();
                case "OR":
                    return firstRow.Zip(secondRow, (a, b) => a || b).ToList();
                default:
                    break;
            }

            return null;
        }

        public void GetSearchIncidenceMatrix(IncidenceMatrix incidenceMatrix, string statment)
        {
            var operationOrderAndWords = statment.Split(' ');
            var operationOrder = new List<string>();
            var wordsIndexes = new List<List<bool>>();
            foreach (var word in operationOrderAndWords)
            {
                if (word == "AND" || word == "OR" || word == "NOT")
                {
                    operationOrder.Add(word);
                    continue;
                }
                if (!incidenceMatrix.Matrix.ContainsKey(word))
                {
                    List<bool> emptyList = new List<bool>();
                    for (int i = 0; i < FileReader.NumberOfFiles; i++)
                    {
                        emptyList.Add(false);
                    }
                    wordsIndexes.Add(emptyList);
                    continue;
                }

                wordsIndexes.Add(incidenceMatrix.Matrix[word]);
            }
            
            for (int i = 0; i < operationOrder.Count; i++)
            {
                if (operationOrder[i] == "NOT")
                {
                    var firstRow = wordsIndexes[i];
                    wordsIndexes.RemoveAt(i);
                    wordsIndexes.Insert(i, firstRow.Select(x => !x).ToList());
                    operationOrder.RemoveAt(i);
                }
            }
            
            foreach (var operation in operationOrder)
            {
                var firstRow = wordsIndexes[0];
                var secondRow = wordsIndexes[1];
                wordsIndexes.RemoveAt(0);
                wordsIndexes.RemoveAt(0);
                wordsIndexes.Insert(0, OperationForIndenceMatrix(firstRow, secondRow, operation));
            }

            ResultIncidenceMatrixList = wordsIndexes[0];
        }
        public void ConsoleOutputIndenceMatrix()
        {
            for (int i = 0; i < ResultIncidenceMatrixList.Count; i++)
            {
                if (ResultIncidenceMatrixList[i])
                {
                    Console.WriteLine(i + " -> " + FileReader.Books[i]);
                }
            }
        }
        public void GetSearchInvertedIndex(Dictionary<string, List<int>> result, string statment)
        {
            var operationOrderAndWords = statment.Split(' ');
            var operationOrder = new List<string>();
        }
        public List<int> FindWordPosition(Dictionary<string, List<int>> invertedIndex,string word)
        {
            if(invertedIndex.ContainsKey(word))
            {
                return invertedIndex[word];
            }
            return new List<int>();
        }

        public void ConsoleOutputInvertedIndex()
        {

        }
    }
}
