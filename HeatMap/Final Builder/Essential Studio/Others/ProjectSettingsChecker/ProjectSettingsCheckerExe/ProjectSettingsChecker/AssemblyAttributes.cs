using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Syncfusion.ProjectSettings.Checker
{
    public class AssemblyAttributes
    {
        public static bool IsAssemblyAttributesExists(string svnpath, string platform, string projectname,string format)
        {
            ProjectLocation objlocation = new ProjectLocation();
            string projectfileLocation = string.Empty;
            bool isExists = true;
            projectfileLocation = objlocation.projectPath(svnpath, platform, projectname);
            if (File.Exists((projectfileLocation + "\\AssemblyInfo.cs")))
            {
                isExists = searchAssemblyAttributes(projectfileLocation + "\\AssemblyInfo.cs",format);
            }
            else if (File.Exists(projectfileLocation + "\\Properties\\AssemblyInfo.cs"))
            {
                isExists = searchAssemblyAttributes(projectfileLocation + "\\Properties\\AssemblyInfo.cs",format);
            }
            return isExists;
        }
        public static bool searchAssemblyAttributes(string filelocation,string format)
        {
            bool isMatch = false;
            string input = File.ReadAllText(filelocation);
            if (!string.IsNullOrEmpty(input))
            {
                Regex regex1 = new Regex("\\[\\s*assembly\\s*:\\s*"+format+"\\s*(.*?)\\s*]");                
                Regex regex2 = new Regex(@"#if.*?#endif", RegexOptions.Singleline);
                MatchCollection matches = regex2.Matches(input);                
                    if (regex1.Match(input).Success)
                    {
                        isMatch = true;
                        foreach (Match m in matches)
                        {
                            if (!m.ToString().ToLower().Contains("clientprofile"))
                            {
                                foreach (Match match in Regex.Matches(m.Value, regex1.ToString(), RegexOptions.IgnoreCase))
                                {
                                    isMatch = false;
                                }
                            }
                        }                    
                    }          
            }
            return isMatch;
        }
    }
}
