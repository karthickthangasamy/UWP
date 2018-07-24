using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Syncfusion.ProjectSettings.Checker
{
    public class ProjectCheck
    {
        string projectfilePath = string.Empty;
        ProjectLocation objLocation = new ProjectLocation();
        public string isProjectFileMismatch(string svnpath, string platform, string projectname,string VSVersion)
        {
            
            string ProjectFileMisMatchVersion=string.Empty;
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if (VSVersion == "2010" && platform.ToLower().Equals("silverlight") || (projectname.Equals("OlapSilverlight.Base")) || (projectname.Equals("OlapSilverlight.BaseWrapper")))
            {
                if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj"))
                    ProjectFileMisMatchVersion = "2010";
                if (!projectname.ToLower().Contains("design") && objLocation.isVSProjectPresent(projectfilePath, "2010", true))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj"))
                        ProjectFileMisMatchVersion = "2010";
                }
            }
            else if (((platform.ToLower().Equals("mvc") || platform.ToLower().Equals("ej.mvc")) && (VSVersion == "2010" || VSVersion == "2012" || VSVersion == "2015")) || (platform.ToLower().Equals("wpf") && (VSVersion != "2005")) || (platform.ToLower().Equals("wp8") && (VSVersion == "2012")) || (platform.ToLower().Equals("web") && (VSVersion != "2005")) || (platform.ToLower().Equals("ej.web") && (VSVersion == "2010" || VSVersion == "2012" || VSVersion == "2013" || VSVersion == "2015")) || (platform.ToLower().Equals("windows")) || (platform.ToLower().Equals("base")) || (platform.ToLower().Equals("universal") && (VSVersion == "2013")) || (platform.ToLower().Equals("wp81sl") && (VSVersion == "2013")) || (platform.ToLower().Equals("wp81winrt") && (VSVersion == "2013")) || (platform.ToLower().Equals("uwp") && (VSVersion == "2015")))
            {
                if (projectname.ToLower().Contains("dll.design") && (platform.ToLower().Equals("wpf")) && (VSVersion == "2008"))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".csproj") ||
                       !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".sln"))
                    {
                        ProjectFileMisMatchVersion = VSVersion;
                    }
                }
                else if(!projectname.ToLower().Contains("dll.design"))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".csproj") ||
                        !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".sln"))
                    {
                        ProjectFileMisMatchVersion = VSVersion;
                    }
                }
                
            }            

            else if (platform.ToLower().Equals("winrt") && (VSVersion == "2012" || VSVersion == "2013"))
            {
                if ((projectname.Contains("DirectXWrapper.WinRT")) || (projectname.Contains("ChartCPP.WinRT")))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".vcxproj"))
                    {
                        ProjectFileMisMatchVersion = VSVersion;
                    }

                }
                else
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".csproj") ||
                        !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".sln"))
                    {
                        ProjectFileMisMatchVersion = VSVersion;
                    }
                }
            }

            return ProjectFileMisMatchVersion;
        }
       
    }
}
