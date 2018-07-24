using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;


namespace Syncfusion.ProjectSettings.Checker
{
    public class ValidateXaml
    {
        string projectfilePath = string.Empty;
        string projectFileName = string.Empty;
        string targetFrameworkVersion = string.Empty;

        ProjectLocation objLocation = new ProjectLocation();

        public bool isValidateXamlMismatch(string svnpath, string platform, string projectname)
        {
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);

            if (platform.ToLower().Equals("silverlight") && !projectname.ToLower().Contains("dll.design") && !projectname.ToLower().Contains("VisualStudio.Design") && !projectname.ToLower().Contains("Expression.Design"))
            {
                // Checking silverlight 4 project file
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectFileName))
                {
                    string content = File.ReadAllText(projectFileName);
                    Regex validateRegex = new Regex(@"<ValidateXaml>.*?</ValidateXaml>");
                    MatchCollection mc = validateRegex.Matches(content);
                    if(mc.Count == 0)
                    {
                        return true;
                    }
                    if(mc.Count > 0)
                    {
                        foreach (Match val in mc)
                        {
                            if (!val.ToString().ToLower().Contains("false"))
                                {
                                    return true;
                                }
                        }
                    }
                }

                // Checking silverlight 5 project file
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                if (File.Exists(projectFileName))
                {
                    string content = File.ReadAllText(projectFileName);
                    Regex validateRegex = new Regex(@"<ValidateXaml>.*?</ValidateXaml>");
                    MatchCollection mc = validateRegex.Matches(content);
                    if(mc.Count == 0)
                    {
                        return true;
                    }
                    if(mc.Count > 0)
                    {
                        foreach (Match val in mc)
                         {
                            if (!val.ToString().ToLower().Contains("false"))
                            {
                                return true;
                            }
                          }
                    }
                }
            }
            return false;
        }
    }
}
