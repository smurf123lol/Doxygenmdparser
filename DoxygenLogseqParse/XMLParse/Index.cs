using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DoxygenLogseqParse.XML
{
    public class Index
    {
        public List<Class> classes = new();
        public Index(XmlElement Root, string src)
        {
            List<Compound> compounds = new();
            foreach (XmlElement xnode in Root)
            {
                compounds.Add(new Compound(xnode, src));  
            }
            foreach(Compound compound in compounds)
            {
                object c =compound.ParseCompound();
                if(c != null)
                {
                    switch (c)
                    {
                        case Class cls:
                            classes.Add(cls);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
