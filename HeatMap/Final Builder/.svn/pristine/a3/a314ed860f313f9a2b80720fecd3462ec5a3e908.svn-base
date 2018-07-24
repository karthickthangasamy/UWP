using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Syncfusion.ProjectSettings.Checker
{
    public class ProjectCoreReferenceCheck
    {
        // Local variables
        string projectfilePath = string.Empty;
        string projectFileName = string.Empty;
        bool isPresent = false;

        ProjectLocation objLocation = new ProjectLocation();


        /// <summary>
        /// Check the Project Core Reference from project name and return it to calling function
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool getProjectCoreReference(string filename)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                if (File.Exists(filename))
                {
                    // Load the project file

                    doc.Load(filename);
                    string fileExtension = Path.GetExtension(filename);
                    string[] referenceCount = new string[200];
                    if (fileExtension.Equals(".csproj"))
                    {
                        var referenceNode = doc.GetElementsByTagName("Reference");
                        referenceCount = new string[referenceNode.Count];

                        for (int i = 0; i < referenceNode.Count; i++)
                        {
                            var xmlAttributeCollection = referenceNode[i].Attributes;
                            if (xmlAttributeCollection != null)
                            {
                                var attribute = xmlAttributeCollection["Include"];
                                if (attribute != null)
                                    referenceCount[i] = attribute.Value;
                            }
                        }
                    }
                    else if (fileExtension.Equals(".config"))
                    {
                        var referenceNode = doc.GetElementsByTagName("add");
                        referenceCount = new string[referenceNode.Count];

                        for (int i = 0; i < referenceNode.Count; i++)
                        {
                            var xmlAttributeCollection = referenceNode[i].Attributes;
                            if (xmlAttributeCollection != null)
                            {
                                var attribute = xmlAttributeCollection["assembly"];
                                if (attribute != null)
                                    referenceCount[i] = attribute.Value;
                            }
                        }
                    }
                    foreach (string reference in referenceCount)
                    {
                        if (!string.IsNullOrEmpty(reference))
                        {
                            if (reference.ToLower().Contains("syncfusion.core"))
                                return true;
                        }
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public bool isVSOldProjectCoreReferenceEnable(string svnpath, string platform, string projectname, string VSVersion, string sourceType)
        {
            isPresent = false;
            projectfilePath = SourceFilePath(svnpath, platform, projectname, sourceType);

            // If platform is equal to windows or base then check VS2005 project files
            if (platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web"))
            {
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".csproj";
                if (File.Exists(projectFileName))
                {
                    isPresent = getProjectCoreReference(projectFileName);
                }
            }
            return isPresent;
        }



        public bool isVSProjectCoreReferenceEnable(string svnpath, string platform, string projectname, string VSVersion, string sourceType)
        {
            isPresent = false;
            projectfilePath = SourceFilePath(svnpath, platform, projectname, sourceType);
            if (VSVersion == "2010" && platform.ToLower().Equals("silverlight"))
            {
                // Checking silverlight 4 project file
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectFileName))
                {
                    isPresent = getProjectCoreReference(projectFileName);
                }
                if (!projectname.ToLower().Contains("design"))
                {
                    // Checking silverlight 5 project file
                    projectFileName = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                    if (File.Exists(projectFileName))
                    {
                        isPresent = getProjectCoreReference(projectFileName);
                    }
                }
            }
            else
            {
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".csproj";
                if (File.Exists(projectFileName) && !projectFileName.ToLower().Contains("wp8"))
                {
                    isPresent = getProjectCoreReference(projectFileName);
                }
            }
            return isPresent;
        }

        public string SourceFilePath(string svnpath, string platform, string projectname, string sourceType)
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

            if (sourceType.Equals("Source"))
            {
                if (Directory.Exists(projectfilePath + "\\Src"))
                {
                    projectfilePath= projectfilePath + "\\Src";
                }
                else
                {
                    projectfilePath= projectfilePath + "\\src";
                }
            }
            else if (sourceType.Equals("Samples"))
            {
                if (Directory.Exists(projectfilePath + "\\Samples"))
                {
                    projectfilePath= projectfilePath + "\\Samples";
                }
                else
                {
                    projectfilePath= projectfilePath + "\\samples";
                }
            }
            return projectfilePath;
        }
    }
}
