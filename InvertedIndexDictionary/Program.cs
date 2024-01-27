using InvertedIndexDictionary;
using System.Text;
using System.Xml;
using System.Xml.Linq;

//string content = File.ReadAllText(path: "C:/Papka/NaUKMA/Projects/InformationRetrieval/InvertedIndexDictionary/InvertedIndexDictionary/books/book1.fb2");

List<string> files = new List<string>() { "book1.fb2", "book.fb2", "book2.fb2" };

for(int i = 0; i < files.Count; i++)
{
    TextParser textParser = new TextParser(FileReader.GetTextFromFile(files[i]), i);
    /*
    foreach (var word in textParser.GetWords())
    {
        Console.Write(word.Key + " ");
        foreach (var wordValue in word.Value)
        {
            Console.Write(wordValue + " ");
        }
        Console.WriteLine();
    }
    */
    List<Dictionary<string, List<int>>> allWords = new List<Dictionary<string, List<int>>>();

    allWords.Add(textParser.GetWords());
}