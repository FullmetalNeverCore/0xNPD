using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0xNotepad.misc
{
    internal static class middleware
    {
        public static string? highlight_definer(string text)
        {
            string? finalres = null;
            switch (text)
            {
                case ".py":
                    finalres = "python";
                    break;
                case ".cs":
                    finalres = "c#";
                    break;
                case ".java":
                    finalres = "java";
                    break;
                case ".html":
                    finalres = "html";
                    break;
                case ".css":
                    finalres = "css";
                    break;

            }
            return finalres;
        }
    }
}
