using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Syncfusion.ProjectSettings.Checker
{
    public class AssemblyName
    {
        // Local variables
        string projectfilePath = string.Empty;
        string projectFileName = string.Empty;
        string assemblyName = string.Empty;
        bool isMismatch = false;

        ProjectLocation objLocation = new ProjectLocation();

        /// <summary>
        /// Get the assembly name, check the assembly name value is matched the spcified format and 
        /// return boolean value to calling funtion
        /// </summary>
        /// <param name="svnpath"></param>
        /// <param name="platform"></param>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public bool isAssmeblyNameMismatch(string svnpath, string platform, string projectname, string VSversion)
        {
            // Local variables
            string projectfilePath = string.Empty;
            string projectFileName = string.Empty;
            string assemblyName = string.Empty;


            //ProjectLocation objLocation = new ProjectLocation();
            bool isMismatch = false;
            // set the project file location based on the platform
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);

			if(VSversion.Equals ("2005") || VSversion.Equals ("2008"))
            {
            if (platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web"))
            {
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_"+VSversion+".csproj";
                if (File.Exists(projectFileName))
                {
                    assemblyName = getAssemblyName(projectFileName);
                    if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                    {
                        isMismatch = true;
                    }
                }
            }
            }

            // Set VS2010 and VS2012 project files, if platform is not equal to silverlight
            if(!platform.ToLower().Equals("silverlight"))
            {
                
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_"+VSversion+".csproj";
                if (File.Exists(projectFileName) && !projectFileName.ToLower().Contains("wp8"))
                {
                    assemblyName = getAssemblyName(projectFileName);
                    if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                    {
                        isMismatch = true;
                    }
                }
               
            }

            // If platform is equal to silverlight, set project file name for SL 4 and SL 5 projects
            if (platform.ToLower().Equals("silverlight"))
            {
                // Checking silverlight 4 project file
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectFileName))
                {
                    assemblyName = getAssemblyName(projectFileName);
                    if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                    {
                        isMismatch = true;
                    }
                }
                if (!projectname.ToLower().Contains("design"))
                {
                    // Checking silverlight 5 project file
                    projectFileName = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                    if (File.Exists(projectFileName))
                    {
                        assemblyName = getAssemblyName(projectFileName);
                        if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                        {
                            isMismatch = true;
                        }
                    }
                }
            }
            return isMismatch;
        }


        /// <summary>
        /// Get the Assembly name from project name and return it to calling function
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string getAssemblyName(string filename)
        {
            bool isPresent = false;
            string assemblyname = null;
            XmlDocument doc = new XmlDocument();
            if (File.Exists(filename))
            {
                // Load the project file
                doc.Load(filename);
                // Get <PropertyGroup> tags
               // XmlNode parentNode = doc.GetElementsByTagName("PropertyGroup")[0];
                //XmlNodeList childNodes = parentNode.ChildNodes;
                XmlNodeList parentNodeList = doc.GetElementsByTagName("PropertyGroup");
                foreach (XmlNode parentnode in parentNodeList)
                {
                    if (parentnode.Attributes.Count == 0)
                    {
                        XmlNodeList childnodeList = parentnode.ChildNodes;
                        foreach (XmlNode innernode in childnodeList)
                        {
                            // Iterate each node in "PrpertyGroup" node and get <AssemblyName> tag
                            if (innernode.Name == "AssemblyName")
                            {
                                // Get the assembly name of the project by reading Inner text of <AssemblyName> tag
                                assemblyname = innernode.InnerText;
                                isPresent = true;
                                break;
                            }
                        }
                        return assemblyname;
                    }
                }
                
            }

            return assemblyname;
        }


        public bool isVSOldAssemblynameMismatch(string svnpath, string platform, string projectname,string VSVersion)
        {
            isMismatch = false;
             projectfilePath = objLocation.projectPath(svnpath, platform, projectname);

            // If platform is equal to windows or base then check VS2005 project files
             if (platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web"))
             {
                 projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_"+VSVersion+".csproj";
                 if (File.Exists(projectFileName))
                 {
                     assemblyName = getAssemblyName(projectFileName);
                     if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                     {
                         isMismatch = true;
                     }
                 }
             }
             return isMismatch;
        }


        
        public bool isVSAssemblynameMismatch(string svnpath, string platform, string projectname, string VSVersion)
        {
            isMismatch = false;
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if (VSVersion == "2010" && platform.ToLower().Equals("silverlight"))
            {
                // Checking silverlight 4 project file
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectFileName))
                {
                    assemblyName = getAssemblyName(projectFileName);
                    if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                    {
                        isMismatch = true;
                    }
                }
                if (!projectname.ToLower().Contains("design"))
                {
                    // Checking silverlight 5 project file
                    projectFileName = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                    if (File.Exists(projectFileName))
                    {
                        assemblyName = getAssemblyName(projectFileName);
                        if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                        {
                            isMismatch = true;
                        }
                    }
                }
            }
            else
            {
                projectFileName = projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".csproj";
                if (File.Exists(projectFileName) && !projectFileName.ToLower().Contains("wp8"))
                {
                    assemblyName = getAssemblyName(projectFileName);
                    if (string.IsNullOrEmpty(assemblyName) || !assemblyName.ToLower().Equals("syncfusion." + projectname.ToLower()))
                    {
                        isMismatch = true;
                    }
                }
            }
            return isMismatch;
        }
    }
}
