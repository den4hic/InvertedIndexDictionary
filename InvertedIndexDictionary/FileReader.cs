using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace InvertedIndexDictionary
{
    internal class FileReader
    {
        public static string GetTextFromFile(string fileName)
        {
            XDocument xDocument = XDocument.Load("../../../books/" + fileName);
            FileInfo file = new FileInfo("../../../books/" + fileName);
            OutputDictionary.OverallSize += (int)file.Length / 1024;

            XElement? text = xDocument.Element("{http://www.gribuser.ru/xml/fictionbook/2.0}FictionBook")?
                .Element("{http://www.gribuser.ru/xml/fictionbook/2.0}body");

            Console.OutputEncoding = UTF8Encoding.UTF8;

            return text.Value;
        }
    }
}
