using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoxygenLogseqParse.XML
{
    public class Reference
    {
        public static Dictionary<string, Reference> references = new Dictionary<string, Reference>();

        //Ref example 66351694-156b-4dc1-9982-1011e3f6cc02
        public string logseqID;
        Reference() {
            logseqID = CreateString(8) +'-'+
                CreateString(4) + '-' +
                CreateString(4) + '-' +
                CreateString(4) + '-' +
                CreateString(12);
        }
        static Random rd = new Random();
        internal static string CreateString(int stringLength)
        {
            const string allowedChars = "1234567890abcdef";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
        public static Reference GetReference(string refID)
        {
            if (references.ContainsKey(refID))
            {
                return references[refID];
            }
            else
            {
                references.Add(refID, new Reference());
                return references[refID];
            }
        }
    }
}
