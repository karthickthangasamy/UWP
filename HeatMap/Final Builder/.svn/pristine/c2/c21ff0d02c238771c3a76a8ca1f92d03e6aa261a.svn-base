using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


namespace Syncfusion.ProjectSettings.Checker
{
    public class TargetFrameworkVersionChecker
    {
        string projectfilePath = string.Empty;
        string projectFileName = string.Empty;
        string targetFrameworkVersion = string.Empty;
        bool isMismatch = false;

        ProjectLocation objLocation = new ProjectLocation();

        /// <summary>
        /// Check the target framework version based on visual studio project
        /// </summary>
        /// <param name="svnpath"></param>
        /// <param name="platform"></param>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public bool isTargetFrameworkVersionMismatch(string svnpath, string platform, string projectname)
        {
            //bool isMismatch = false;
            //ProjectLocation objLocation = new ProjectLocation();
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            
            //if (platform.ToLower().Equals("windows") || platform.ToLower().Equals("base"))
            //{
            //    projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2005.csproj";
            //    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
            //    if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v2.0"))
            //    {
            //        isMismatch = true;
            //    }
            //}

            if (platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web"))
            {
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2008.csproj";
                if (File.Exists(projectFileName))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v3.5"))
                    {
                        isMismatch = true;
                    }
                }
            }

            if (!platform.ToLower().Equals("silverlight"))
            {
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectFileName))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v4.0"))
                    {
                        isMismatch = true;
                    }
                }

                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2012.csproj";
                if (File.Exists(projectFileName))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v4.5"))
                    {
                        isMismatch = true;
                    }
                }
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2013.csproj";
                if (File.Exists(projectFileName) && !projectFileName.ToLower().Contains("wp8"))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (platform.ToLower().Equals("universal"))
                    {
                        if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v4.6"))
                        {
                            isMismatch = true;
                        }
                    }
                    else if (platform.ToLower().Equals("wp81sl") || platform.ToLower().Equals("wp81winrt"))
                    {
                        if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v8.1"))
                        {
                            isMismatch = true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v4.5.1"))
                        {
                            isMismatch = true;
                        }
                    }
                }

                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2015.csproj";
                if (File.Exists(projectFileName))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v4.6"))
                    {
                        isMismatch = true;
                    }
                }
            
			}
            if (platform.ToLower().Equals("silverlight") && !projectname.ToLower().Contains("dll.design"))
            {
                // Checking silverlight 4 project file
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectFileName))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v4.0"))
                    {
                        isMismatch = true;
                    }
                }
                // Checking silverlight 5 project file
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                if (!projectname.ToLower().Contains("design") && File.Exists(projectFileName))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v5.0"))
                    {
                        isMismatch = true;
                    }
                }
            }
            return isMismatch;
        }

        /// <summary>
        /// Get the target framework version from project file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string getTargetFrameworkVersion(string filename)
        {
            //bool isPresent = false;
            string frameworkVersion = null;
            XmlDocument doc = new XmlDocument();
            if (File.Exists(filename))
            {
                doc.Load(filename);
                XmlNodeList parentNodeList = doc.GetElementsByTagName("PropertyGroup");
                foreach (XmlNode parentnode in parentNodeList)
                {
                    if (parentnode.Attributes.Count == 0)
                    {
                        //XmlNode parentNode = doc.GetElementsByTagName("PropertyGroup")[0];
                        XmlNodeList childNodes = parentnode.ChildNodes;
                        foreach (XmlNode innernode in childNodes)
                        {
                            if (innernode.Name == "TargetFrameworkVersion")
                            {
                                frameworkVersion = innernode.InnerText;
                                //isPresent = true;
                                break;
                            }
                        }

                    }
                }
                
            }
            return frameworkVersion;
        }
        
       

        public bool isVSTargetFWMismatch(string svnpath, string platform, string projectname, string VSVersion)
        {
            isMismatch = false;
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);

            if (!platform.ToLower().Equals("silverlight"))
            {
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_"+VSVersion+".csproj";
                if (File.Exists(projectFileName) && !projectFileName.ToLower().Contains("wp8"))
                {
                    targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                    if (targetFrameworkVersion != null)
                    {
                        if (platform.ToLower().Equals("universal"))
                        {
                            if (VSVersion.Equals("2013") && (!targetFrameworkVersion.ToLower().Equals("v4.6") || string.IsNullOrEmpty(targetFrameworkVersion)))
                            {
                                isMismatch = true;
                            }
                        }
                        else if (platform.ToLower().Equals("wp81sl") || platform.ToLower().Equals("wp81winrt"))
                        {
                            if (VSVersion.Equals("2013") && (!targetFrameworkVersion.ToLower().Equals("v8.1") || string.IsNullOrEmpty(targetFrameworkVersion)))
                            {
                                isMismatch = true;
                            }
                        }
                        else
                        {
                            if (VSVersion.Equals("2008") && (!targetFrameworkVersion.ToLower().Equals("v3.5") || string.IsNullOrEmpty(targetFrameworkVersion)))
                            {
                                isMismatch = true;
                            }
                            if (VSVersion.Equals("2010") && (!targetFrameworkVersion.ToLower().Equals("v4.0") || string.IsNullOrEmpty(targetFrameworkVersion)))
                            {
                                isMismatch = true;
                            }
                            else if (VSVersion.Equals("2012") && (!targetFrameworkVersion.ToLower().Equals("v4.5") || string.IsNullOrEmpty(targetFrameworkVersion)))
                            {
                                isMismatch = true;
                            }
                            else if (VSVersion.Equals("2013") && (!targetFrameworkVersion.ToLower().Equals("v4.5.1") || string.IsNullOrEmpty(targetFrameworkVersion)))
                            {
                                isMismatch = true;
                            }
                            else if (VSVersion.Equals("2015") && (!targetFrameworkVersion.ToLower().Equals("v4.6") || string.IsNullOrEmpty(targetFrameworkVersion)))
                            {
                                isMismatch = true;
                            }
                        }
                    }
                    else
                    {
                        isMismatch = true;
                    }
                    //else if ((string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v8.0")) && platform.ToLower().Equals("wp8"))
                    //{
                    //}
                }
            }
            else if (platform.ToLower().Equals("silverlight") && !projectname.ToLower().Contains("dll.design") && VSVersion.Equals ("2010"))
                {
                    // Checking silverlight 4 project file
                    projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                    if (File.Exists(projectFileName))
                    {
                        targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                        if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v4.0"))
                        {
                            isMismatch = true;
                        }
                    }
                    // Checking silverlight 5 project file
                    projectFileName = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                    if (!projectname.ToLower().Contains("design") && File.Exists(projectFileName))
                    {
                        targetFrameworkVersion = getTargetFrameworkVersion(projectFileName);
                        if (string.IsNullOrEmpty(targetFrameworkVersion) || !targetFrameworkVersion.ToLower().Equals("v5.0"))
                        {
                            isMismatch = true;
                        }
                    }
                }
            return isMismatch;
        }
        
    }
}
