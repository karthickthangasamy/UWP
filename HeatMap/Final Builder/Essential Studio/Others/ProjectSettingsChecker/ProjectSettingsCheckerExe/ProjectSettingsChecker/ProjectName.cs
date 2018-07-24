using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Syncfusion.ProjectSettings.Checker
{
    public class ProjectName
    {
        //static bool isProjectMismatch = false;
        //static bool isSolutionMismatch = false;
        //static bool isVS2005 = false, isVS2008 = false, isVS2010 = false, isVS2012 = false;
        //static string projectfilePath = string.Empty;
        string projectfilePath = string.Empty;
        bool isMisMatch = false;
        ProjectLocation objLocation = new ProjectLocation();
        
        /// <summary>
        /// this function is check whether the Project name and Solution file name are in correct format
        /// it checks the file name based on th platform argument.
        /// </summary>
        /// <param name="svnpath"></param>
        /// <param name="platform"></param>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public bool isProjectNameMismatch(string svnpath, string platform, string projectname)
        {
            bool isVS2005 = false, isVS2008 = false, isVS2010 = false, isVS2012 = false, isVS2013 = false, isVS2015 = false;
             //string projectfilePath = string.Empty;

            //ProjectLocation objLocation = new ProjectLocation();
            projectfilePath = objLocation.projectPath(svnpath,platform,projectname);
            if ((platform.ToLower().Equals("windows") || platform.ToLower().Equals("base")) && objLocation.isVSProjectPresent(projectfilePath,"2005",false))
            {
                if(!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2005.csproj") || 
                    !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2005.sln"))
                {
                    isVS2005=true;
                }
            }
            if ((platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web")) && objLocation.isVSProjectPresent(projectfilePath,"2008",false))
            {
                if(!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2008.csproj") ||
                    !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2008.sln"))
                    isVS2008=true;
            }
            if(!platform.ToLower().Equals("silverlight"))
            {
                if (objLocation.isVSProjectPresent(projectfilePath, "2010",false))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj") ||
                        !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2010.sln"))
                        isVS2010 = true;
                }
                if (objLocation.isVSProjectPresent(projectfilePath, "2012",false))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2012.csproj") ||
                        !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2012.sln"))
                        isVS2012 = true;
                }
                if (objLocation.isVSProjectPresent(projectfilePath, "2013", false))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2013.csproj") ||
                        !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2013.sln"))
                        isVS2013 = true;
                }
				if (objLocation.isVSProjectPresent(projectfilePath, "2015", false))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2015.csproj") ||
                        !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2015.sln"))
                        isVS2015 = true;
                }
            }
            if (platform.ToLower().Equals("silverlight"))
            {
                if(!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj"))
                    isVS2010=true;
                if(!projectname.ToLower().Contains("design"))
                {
                    if(!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj"))
                        isVS2010=true;
                }
            }
            if(isVS2005 || isVS2008 || isVS2010 || isVS2012 || isVS2013 || isVS2015)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool isVSOldProjectNameMismatch(string svnpath, string platform, string projectname, string VSVersion)
        {
            isMisMatch = false;
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if(VSVersion.Equals("2005"))
            {
            if ((platform.ToLower().Equals("windows") || platform.ToLower().Equals("base")) && objLocation.isVSProjectPresent(projectfilePath, "2005",false))
            {
                if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2005.csproj") ||
                    !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2005.sln"))
                {
                    isMisMatch = true;
                }
            }
            }
            else if (VSVersion.Equals("2008"))
            {
                if ((platform.ToLower().Equals("windows") || platform.ToLower().Equals("base") || platform.ToLower().Equals("web") || platform.ToLower().Equals("wpf")) && objLocation.isVSProjectPresent(projectfilePath, "2005", false))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2008.csproj") ||
                     !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2008.sln"))
                    {
                        isMisMatch = true;
                    }
                }
            }


            return isMisMatch;
        }

       
        public bool isVSProjectNameMismatch(string svnpath, string platform, string projectname,string VSVersion)
        {
            isMisMatch = false;
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if (VSVersion == "2010" && platform.ToLower().Equals("silverlight"))
            {
                if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj"))
                    isMisMatch = true;
                if (!projectname.ToLower().Contains("design") && objLocation.isVSProjectPresent(projectfilePath, "2010", true))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj"))
                        isMisMatch = true;
                }
            }
            else
            {
                if (objLocation.isVSProjectPresent(projectfilePath, VSVersion, false))
                {
                    if (!File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".csproj") ||
                        !File.Exists(projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".sln"))
                    {
                        isMisMatch = true;
                    }
                }
            }

            return isMisMatch;
        }
    }
}
