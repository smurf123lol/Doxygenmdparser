using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoxygenLogseqParse.md
{

    public class MDString
    {
        int level = 0;
        public string text;
        public MDString() { }
        public void AddSubTitle(string title)
        {
            for(int i = 0; i < level; i++)
            {
                text += "\t";
            }
            text += "- " + title+"\n";
            level++;
        }
        public void AddId(string id)
        {
            for (int i = 0; i < level-1; i++)
            {
                text += "\t";
            }
            text += "  id:: " + id+'\n';
        }
        public void EndSubtitle()
        {
            level--;
        }
        public static string TextWithRefference(string text, string reference)
        {
            return $"[{text}]({reference})";
        }
    }
}
