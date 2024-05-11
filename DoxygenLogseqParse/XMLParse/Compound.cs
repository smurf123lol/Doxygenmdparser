using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DoxygenLogseqParse.XML
{

    public class Compound
    {
        public enum Kind
        {
            Class,
            Namespace,
            File,
            Dir
        }
        private static readonly Dictionary<Kind, string> KindStrings = new Dictionary<Kind, string>()
        {
            { Kind.Class,"class" },
            { Kind.Namespace,"namespace" },
            { Kind.File,"file" },
            { Kind.Dir,"dir" }
        };
        public string refid;
        public string name;
        public Kind kind;
        public string src;
        public Compound(XmlElement xnode,string src)
        {
            string kindAttr = xnode.GetAttribute("kind");
            kind = KindStrings.FirstOrDefault(x => x.Value == kindAttr).Key;
            refid = xnode.GetAttribute("refid").Replace('/','\\');
            name = xnode.SelectSingleNode("name").InnerText;

            this.src = src;
        }

        public object? ParseCompound()
        {
            switch (kind)
            {
                case Kind.Class:
                    return new Class(src +"\\"+ refid + ".xml");
                case Kind.Namespace:
                    return null;
                case Kind.File:
                    return null;
                case Kind.Dir:
                    return null;
                default:
                    return null;
            }
        }

    }

}
