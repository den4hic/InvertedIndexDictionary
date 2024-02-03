using InvertedIndexDictionary;

List<string> files = new List<string>() { "book1.fb2", "book2.fb2", "book3.fb2", "book4.fb2", "book5.fb2", "book6.fb2", "book7.fb2", "book8.fb2", "book9.fb2", "book10.fb2" };
List<int> pointer = new List<int>();
List<Dictionary<string, List<int>>> allWords = new List<Dictionary<string, List<int>>>();
int overallNumberOfWords = 0;

IncidenceMatrix incidenceMatrix = new IncidenceMatrix();

for (int i = 0; i < files.Count; i++)
{
    pointer.Add(0);
    TextParser textParser = new TextParser(FileReader.GetTextFromFile(files[i]), i);

    allWords.Add(textParser.GetWords());
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

OutputDictionary outputDictionary = new OutputDictionary(result, overallNumberOfWords);

//outputDictionary.ConsoleOutput();
//outputDictionary.FileOutput();
//outputDictionary.Serialize();

//incidenceMatrix.ConsoleOutput();

BooleanSearch booleanSearch = new BooleanSearch();

booleanSearch.GetSearchIncidenceMatrix(incidenceMatrix, "harry AND potter");