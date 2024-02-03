using System.Text;
using System.Xml.Serialization;

namespace InvertedIndexDictionary
{
    [XmlRoot("OutputDictionary")]
    public class OutputDictionary
    {
        public static int OverallSize { get; set; }

        [XmlArray("Result")]
        [XmlArrayItem("Word")]
        public List<WordItem> Result { get; set; }

        [XmlElement("OverallNumberOfWords")]
        public int OverallNumberOfWords { get; set; }

        public OutputDictionary() { }

        public OutputDictionary(Dictionary<string, List<int>> result, int overallNumberOfWords)
        {
            Result = new List<WordItem>();
            foreach (var entry in result)
            {
                Result.Add(new WordItem { Key = entry.Key, Values = entry.Value });
            }

            OverallNumberOfWords = overallNumberOfWords;
        }

        public void ConsoleOutput()
        {
            foreach (var word in Result)
            {
                Console.Write(word.Key + " ");
                foreach (var wordValue in word.Values)
                {
                    Console.Write(wordValue + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(WriteInfoAboutText());
        }

        private string WriteInfoAboutText()
        {
            string info = "Amount of unique words: " + Result.Count + "\n";
            info += "Amount of books: " + Result.SelectMany(x => x.Values).Distinct().Count() + "\n";
            info += "Overall number of words: " + OverallNumberOfWords + "\n";
            info += "Overall size of all books: " + OverallSize + " kilobytes\n";
            return info;
        }

        public void FileOutput()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(WriteInfoAboutText());

            foreach (var word in Result)
            {
                stringBuilder.Append(word.Key + " ");
                foreach (var wordValue in word.Values)
                {
                    stringBuilder.Append(wordValue + " ");
                }
                stringBuilder.AppendLine();
            }

            File.WriteAllText(@"../../../result.txt", stringBuilder.ToString());
        }

        public void Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(OutputDictionary));

            using (FileStream fileStream = new FileStream("../../../result.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fileStream, this);
            }
        }
    }

    public class WordItem
    {
        [XmlElement("Key")]
        public string? Key { get; set; }

        [XmlArray("Values")]
        [XmlArrayItem("Value")]
        public List<int>? Values { get; set; }
    }
}
