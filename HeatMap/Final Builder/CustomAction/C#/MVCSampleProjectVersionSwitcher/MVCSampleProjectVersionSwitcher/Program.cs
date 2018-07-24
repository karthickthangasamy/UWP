using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace MVCSampleProjectVersionSwitcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string csProjFile = args[0];
            string SyncVersion = args[1];

            string input = File.ReadAllText(csProjFile);
            string modifiedReferrenceContent=", Version="+SyncVersion+", Culture=neutral, PublicKeyToken=3d67ed1f87d44c89, processorArchitecture=MSIL\" />";
            Regex MvcVersionAssemblies=new Regex("<\\s*[R|r]eference\\s*[I|i]nclude\\s*=\\s*\\\"Syncfusion.*?\\.[M|m][V|v][C|c].*?/\\s*>");
            MatchCollection mvcReference = MvcVersionAssemblies.Matches(input);

            foreach (Match reference in mvcReference)
            {
                Regex assemblyNamePatten = new Regex("Syncfusion.*?\\.[M|m][V|v][C|c]");
                Match assemblyName = assemblyNamePatten.Match(reference.ToString());
                input = input.Replace(reference.ToString(), "<Reference Include=\"" + assemblyName + modifiedReferrenceContent);
            }
            File.Delete(csProjFile);
            File.AppendAllText(csProjFile, input);
        }
    }
}
