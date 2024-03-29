﻿using System;
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
    }
}
