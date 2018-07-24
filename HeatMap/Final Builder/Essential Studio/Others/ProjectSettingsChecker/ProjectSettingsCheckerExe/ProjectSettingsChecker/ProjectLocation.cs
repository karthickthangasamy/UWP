using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Syncfusion.ProjectSettings.Checker
{
    public class ProjectLocation
    {

        public string projectPath(string svnpath, string platform, string projectname)
        {
            string projectfilePath = string.Empty;
            if (platform.ToLower().Equals("wp8"))
            {
                projectfilePath = svnpath + "\\Phone\\" + platform + "\\" + projectname;
            }
            else if (platform.ToLower().Equals("wp81sl"))
            {
                projectfilePath = svnpath + "\\Phone\\Silverlight\\" + projectname;
            }
            else if (platform.ToLower().Equals("wp81winrt"))
            {
                projectfilePath = svnpath + "\\Phone\\WinRT\\" + projectname;
            }
            else
            {
                projectfilePath = svnpath + "\\" + platform + "\\" + projectname;
            }
            if (Directory.Exists(projectfilePath + "\\Src"))
            {
                return projectfilePath + "\\Src";
            }
            else
            {
                return projectfilePath + "\\src";
            }
        }

        public bool isVSProjectPresent(string sourcepath,string vsversion,bool isSL5)
        {
            string[] filelist = Directory.GetFiles(sourcepath, "*.csproj");
            foreach (string filename in filelist)
            {
                if (isSL5)
                {
                    if(filename.ToLower().Contains("silverlight5"))
                        return true;
                }
                else
                {
                    if (filename.ToLower().Contains(vsversion))
                        return true;
                }
            }
            return false;
        }
    }
}
