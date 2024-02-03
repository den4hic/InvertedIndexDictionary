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
        public List<int> ResultInvertedIndexList { get; private set; } = new List<int>();

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
        private static List<bool> OperationForIndenceMatrix(List<bool> firstRow, List<bool> secondRow, string operation)
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
            var wordsIndexes = new List<List<int>>();
            List<int> allBooksIndexes = FileReader.Books.Keys.ToList();

            foreach (var word in operationOrderAndWords)
            {
                if (word == "AND" || word == "OR" || word == "NOT")
                {
                    operationOrder.Add(word);
                    continue;
                }
                if (!result.ContainsKey(word))
                {
                    wordsIndexes.Add(new List<int>());
                    continue;
                }

                wordsIndexes.Add(result[word]);
            }

            for (int i = 0; i < operationOrder.Count; i++)
            {
                if (operationOrder[i] == "NOT")
                {
                    var firstRow = wordsIndexes[i];
                    wordsIndexes.RemoveAt(i);
                    wordsIndexes.Insert(i, allBooksIndexes.Except(firstRow).ToList());
                    operationOrder.RemoveAt(i);
                }
            }

            foreach (var operation in operationOrder)
            {
                var firstRow = wordsIndexes[0];
                var secondRow = wordsIndexes[1];
                wordsIndexes.RemoveAt(0);
                wordsIndexes.RemoveAt(0);
                wordsIndexes.Insert(0, OperationForInvertedIndex(firstRow, secondRow, operation));
            }

            ResultInvertedIndexList = wordsIndexes[0];
        }

        private List<int> OperationForInvertedIndex(List<int> firstRow, List<int> secondRow, string operation)
        {
            switch (operation)
            {
                case "AND":
                    return firstRow.Intersect(secondRow).ToList();
                case "OR":
                    return firstRow.Union(secondRow).ToList();
                default:
                    break;
            }

            return null;
        }

        public void ConsoleOutputInvertedIndex()
        {
            for (int i = 0; i < ResultInvertedIndexList.Count; i++)
            {
                Console.WriteLine(ResultInvertedIndexList[i] + " -> " + FileReader.Books[ResultInvertedIndexList[i]]);   
            }
        }
    }
}
