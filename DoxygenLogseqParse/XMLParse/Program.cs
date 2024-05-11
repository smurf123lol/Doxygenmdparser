using CommandLine;
using DoxygenLogseqParse.XML;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
namespace DoxygenLogseqParse
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ConsoleArgs>(args)
                   .WithParsed<ConsoleArgs>(o =>
                   {
                       XmlDocument doc = new XmlDocument();
                       doc.Load(o.srcFolder+"\\index.xml");
                       XmlElement? xRoot = doc.DocumentElement;
                       if (xRoot != null)
                       {
                           DoxygenLogseqParse.XML.Index i = new DoxygenLogseqParse.XML.Index(xRoot,o.srcFolder);
                           if (!Directory.Exists(o.destFolder))
                           {
                               Directory.CreateDirectory(o.destFolder);
                           }
                           foreach (var cls in i.classes)
                           {
                               string fileName = cls.name+".md";
                               fileName = fileName.Replace(":",".");
                               File.WriteAllText(Path.Combine(o.destFolder,fileName), cls.generateMd().text);
                               
                           }
                       }
                   });
        }

        private class ConsoleArgs
        {
            [Option("source", Required = true, HelpText = "Source folder(xml) is required.")]
            public string srcFolder { get; set; }
            [Option("destination", Required = true, HelpText = "Destination folder is required.")]
            public string destFolder { get; set; }
        }
    }
}