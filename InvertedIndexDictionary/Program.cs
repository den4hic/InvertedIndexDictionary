using InvertedIndexDictionary;
using System.Collections.Generic;
using System.Diagnostics;

List<string> files = new List<string>() { "book1.fb2", "book2.fb2", "book3.fb2", "book4.fb2", "book5.fb2", "book6.fb2", "book7.fb2", "book8.fb2", "book9.fb2", "book10.fb2" };
List<int> pointer = new List<int>();
List<Dictionary<string, List<int>>> allWords = new List<Dictionary<string, List<int>>>();
List<Dictionary<string, List<int>>> allTwoWords = new List<Dictionary<string, List<int>>>();
int overallNumberOfWords = 0;

IncidenceMatrix incidenceMatrix = new IncidenceMatrix();

for (int i = 0; i < files.Count; i++)
{
    pointer.Add(0);
    TextParser textParser = new TextParser(FileReader.GetTextFromFile(files[i]), i);

    allWords.Add(textParser.GetWords());
    allTwoWords.Add(textParser.GetTwoWords());
    textParser.GetWordsWithPositions();
    textParser.GetTwoWords();
    incidenceMatrix.AddWords(textParser.Words, i);

    overallNumberOfWords += textParser.NumberOfWords;
}

Dictionary<string, List<int>> result = new Dictionary<string, List<int>>();

for (int i = 0; i < files.Count; i++)
{
    foreach (var word in allWords[i])
    {
        if (!result.ContainsKey(word.Key))
        {
            result[word.Key] = new List<int>();
        }
        result[word.Key].AddRange(word.Value);
    }
}

result = result.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

//OutputDictionary outputDictionary = new OutputDictionary(result, overallNumberOfWords);

//outputDictionary.ConsoleOutput();
//outputDictionary.FileOutput();
//outputDictionary.Serialize();

//BooleanSearch booleanSearch = new BooleanSearch();

//Stopwatch stopwatch = new Stopwatch();
//stopwatch.Start();
//booleanSearch.GetSearchIncidenceMatrix(incidenceMatrix, "murder AND evidence OR knife AND NOT gun AND either AND me AND he");
//stopwatch.Stop();
//TimeSpan timeSpan1 = stopwatch.Elapsed;
//stopwatch.Restart();
//booleanSearch.GetSearchInvertedIndex(result, "murder AND evidence OR knife AND NOT gun AND either AND me AND he");
//stopwatch.Stop();
//TimeSpan timeSpan2 = stopwatch.Elapsed;

//booleanSearch.ConsoleOutputIndenceMatrix();
//Console.WriteLine($"Time is {timeSpan1}");
//Console.WriteLine("------------------------");
//booleanSearch.ConsoleOutputInvertedIndex();
//Console.WriteLine($"Time is {timeSpan2}");

//CoordinateIndexes.OutputCoordinateIndexes();
//CoordinateIndexes.GetSearchWithCoordinate("two women");
CoordinateIndexes.GetSearchWithCoordinateForManyWords("Entwhistle had looked at Cora earlier");
TwoWordsIndex.MakeInvertedTwoWordsIndex(allTwoWords, files.Count);
//TwoWordsIndex.OutputTwoWordsIndex();
BooleanSearch booleanSearch = new BooleanSearch();
booleanSearch.GetSearchTwoWordsInvertedIndex(TwoWordsIndex.TwoWordsIndexResult, "two women");

Console.WriteLine("--------------------");

booleanSearch.ConsoleOutputInvertedIndex();