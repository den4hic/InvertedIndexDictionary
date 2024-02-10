using System;
using System.Collections.Generic;

namespace InvertedIndexDictionary
{
    internal class TextParser
    {
        public string[] Words { get; set; }
        public string Text { get; }
        public int Id { get; set; }
        public int NumberOfWords { get; set; } 
        public TextParser(string text, int id)
        {
            Text = text;
            Id = id;
            Words = RemoveSpecialCharacters(text);
        }

        private string[] RemoveSpecialCharacters(string word)
        {
            return word.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '-', '!', '?', '—', '…', '*', '¡', '¿', '‘', '’', '”', '“', '"', '(', ')', '/', '#', '•', '[', '©', '+', '=', '<', '>', '«', '»', '`' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public Dictionary<string, List<int>> GetWords()
        {
            Dictionary<string, List<int>> wordBookIds = new Dictionary<string, List<int>>();
            IEnumerable<string> words = Words;

            NumberOfWords = words.Count();

            foreach (string word in words)
            {
                if (!int.TryParse(word, out int index))
                {
                    string lowercaseWord = word.ToLower().Trim();

                    if (!lowercaseWord.StartsWith("'") && !lowercaseWord.StartsWith("...") && !lowercaseWord.EndsWith("...") &&!wordBookIds.ContainsKey(lowercaseWord))
                    {
                        wordBookIds[lowercaseWord] = new List<int>();
                        wordBookIds[lowercaseWord].Add(Id);
                    }

                }
            }

            wordBookIds = wordBookIds.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            return wordBookIds;
        }

        public void GetWordsWithPositions()
        {
            Dictionary<string, Dictionary<int, List<int>>> wordsWithPosition = CoordinateIndexes.CoordinateIndexesResult;

            IEnumerable<string> words = Words;
            int counter = 0;

            foreach (string word in words)
            {
                if (!int.TryParse(word, out int index))
                {
                    string lowercaseWord = word.ToLower().Trim();

                    if (!lowercaseWord.StartsWith("'") && !lowercaseWord.StartsWith("...") && !lowercaseWord.EndsWith("..."))
                    {
                        if (wordsWithPosition.ContainsKey(lowercaseWord))
                        {
                            if (wordsWithPosition[lowercaseWord].ContainsKey(Id))
                            {
                                wordsWithPosition[lowercaseWord][Id].Add(counter++);
                            }
                            else
                            {
                                wordsWithPosition[lowercaseWord][Id] = new List<int>() { counter++};
                            }
                        }
                        else
                        {
                            wordsWithPosition.Add(lowercaseWord, new Dictionary<int, List<int>>());
                            wordsWithPosition[lowercaseWord][Id] = new List<int>() { counter++ };
                        }
                    }
                }
            }

            CoordinateIndexes.CoordinateIndexesResult = wordsWithPosition;
        }

        public Dictionary<string, List<int>> GetTwoWords()
        {
            Dictionary<string, List<int>> wordBookIds = new Dictionary<string, List<int>>();
            IEnumerable<string> words = Words;

            NumberOfWords = words.Count();

            for (int i = 0; i < NumberOfWords - 1; i++)
            {
                var twoWords = new { FirstWord = Words[i], SecondWord = Words[i + 1] };

                if (!int.TryParse(twoWords.FirstWord, out int index1) && !int.TryParse(twoWords.SecondWord, out int index2))
                {
                    string lowercaseWord = twoWords.FirstWord.ToLower().Trim() + " " + twoWords.SecondWord.ToLower().Trim();

                    if (!lowercaseWord.StartsWith("'") && !lowercaseWord.StartsWith("...") && !lowercaseWord.EndsWith("...") && !wordBookIds.ContainsKey(lowercaseWord))
                    {
                        wordBookIds[lowercaseWord] = new List<int>() { Id };
                    }
                }

            }

            return wordBookIds;
        }
    }
}
