using System.Xml;

namespace DoxygenLogseqParse.XML
{
    public class Function
    {
        public string name;
        public string typename;

        public Function(XmlNode func)
        {
            name = func.SelectSingleNode("name").InnerText;
            typename = func.SelectSingleNode("type").InnerText;
        }
    }
}