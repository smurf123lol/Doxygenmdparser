using System.Xml;

namespace DoxygenLogseqParse.XML
{
    public class Variable
    {
        public string name;
        public string typename;
        public Reference? selfRef;
        public Variable(XmlNode attrib)
        {

            name = attrib.SelectSingleNode("name").InnerText;
            var id = attrib?.Attributes?.GetNamedItem("id")?.InnerText;
            if (id != null) {
                selfRef = Reference.GetReference(id);
            }
            Func<XmlNode,string> recRefferenceReplace = null;

            var typenode =attrib.SelectSingleNode("type");
            typename = "";
            foreach (XmlNode node in typenode.ChildNodes)
            {
                if(node.Name == "ref")
                {
                    typename += $"[{node.InnerText}]({Reference.GetReference(node.Attributes.GetNamedItem("refid").Value).logseqID})";
                }else
                {
                    typename += node.InnerText;
                }
            }
            typename=typename.Replace(" ", "");
        }
    }
}