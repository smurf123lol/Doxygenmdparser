using DoxygenLogseqParse.md;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DoxygenLogseqParse.XML
{

    public class Class
    {
        public string name;
        public Dictionary<string, List<Variable>> variables = new();
        public Dictionary<string, List<Function>> functions = new();
        public Reference refID;
        public Class(string source)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(source);
            XmlNode? xRoot = doc.DocumentElement.FirstChild;
            refID = Reference.GetReference(xRoot.Attributes.GetNamedItem("id").Value);
            name = xRoot.SelectSingleNode("compoundname").InnerText;
            foreach (XmlNode section in xRoot?.SelectNodes("sectiondef"))
            {
                var attr = section?.Attributes?["kind"]?.Value;
                if (attr.Contains("func"))
                {
                    functions.Add(attr, new());
                    foreach(XmlNode func in section?.ChildNodes)
                    {
                        functions[attr].Add(new(func));
                    }
                }
                else if (attr.Contains("attrib"))
                {
                    variables.Add(attr, new());
                    foreach (XmlNode attrib in section?.ChildNodes)
                    {
                        variables[attr].Add(new(attrib));
                    }
                }
            }
        }

        public MDString generateMd()
        {
            MDString md = new MDString();
            md.AddSubTitle(name);
            md.AddId(refID.logseqID);
            foreach(var  Functions in functions)
            {
                md.AddSubTitle(Functions.Key);
                foreach(var function in Functions.Value)
                {
                    md.AddSubTitle(function.typename + " " + function.name);
                    md.EndSubtitle();
                }
                md.EndSubtitle();
            }
            foreach (var Variables in variables)
            {
                md.AddSubTitle(Variables.Key);
                foreach (var variable in Variables.Value)
                {
                    md.AddSubTitle(variable.typename + " " + variable.name);
                    if(variable.selfRef!=null)md.AddId(variable.selfRef.logseqID);
                    md.EndSubtitle();
                }
                md.EndSubtitle();
            }
            return md;
        }
    }
}
