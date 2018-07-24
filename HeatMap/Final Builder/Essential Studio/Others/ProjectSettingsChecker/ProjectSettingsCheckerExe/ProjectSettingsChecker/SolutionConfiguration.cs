using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace Syncfusion.ProjectSettings.Checker
{
    public class SolutionConfiguration
    {
        //static bool isMismatch = false, isSlnMismatch = true;
        bool isMatch = false;
        bool isSlnMismatch = false;
        string projectfilePath = string.Empty;
        string projectLocation = string.Empty;
        string projectFileName = string.Empty;

        ProjectLocation objLocation = new ProjectLocation();
        
        /// <summary>
        /// This function is used to check the solution configuraion of thr project file and
        /// retun True, if the configuration is missed or mismatched
        /// </summary>
        /// <param name="svnpath"></param>
        /// <param name="platform"></param>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public bool isConfigurationMismatch(string svnpath, string platform, string projectname, string VSVersion)
        {
            bool isMismatch = false, isSlnMismatch = true;
            //string projectfilePath = string.Empty;

            //ProjectLocation objLocation = new ProjectLocation();
            //string projectLocation = string.Empty;
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if(platform.ToLower().Equals("windows") | platform.ToLower().Equals("base"))
            {
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2005.csproj";
                if (File.Exists(projectLocation))
                {
                    isMismatch = parseConfiguration(projectLocation);
                }
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2005.sln";
                if (File.Exists(projectLocation))
                {
                    isSlnMismatch = parseSolutionFile(projectLocation);
                }
            }
            if (VSVersion.Equals("2005") || VSVersion.Equals("2008"))
            {
                if (platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web"))
                {
                    projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_"+VSVersion+".csproj";
                    if (File.Exists(projectLocation))
                    {
                        isMismatch = parseConfiguration(projectLocation);
                    }
                    projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_"+VSVersion+".sln";
                    if (File.Exists(projectLocation))
                    {
                        isSlnMismatch = parseSolutionFile(projectLocation);
                    }
                }
            }
            if (!platform.ToLower().Equals("silverlight"))
            {
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_"+VSVersion+".csproj";
                if (File.Exists(projectLocation) && !projectLocation.ToLower().Contains("wp8"))
                {
                    isMismatch = parseConfiguration(projectLocation);
                }
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".sln";
                if (File.Exists(projectLocation))
                {
                    isSlnMismatch = parseSolutionFile(projectLocation);
                }

            }
            if (platform.ToLower().Equals("silverlight") && !projectname.ToLower().Contains("dll.design"))
            {
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectLocation))
                {
                    isMismatch = parseConfiguration(projectLocation); 
                }

                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2010.sln";
                if (File.Exists(projectLocation))
                {
                    isSlnMismatch = parseSolutionFile(projectLocation);
                }

                if (!projectname.ToLower().Contains("design"))
                {
                    projectLocation = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                    if (File.Exists(projectLocation))
                    {
                        isMismatch = parseConfiguration(projectLocation);
                    }
                    projectLocation = projectfilePath + "\\Syncfusion." + projectname + "5_2010.sln";
                    if (File.Exists(projectLocation))
                    {
                        isSlnMismatch = parseSolutionFile(projectLocation);
                    }
                }
            }
            if (isMismatch)// || !isSlnMismatch)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get the configurations which are in project file and check it is in proper format.
        /// </summary>
        /// <param name="projectpath"></param>
        /// <returns></returns>
        public bool parseConfiguration(string projectpath)
        {
            // boolean variables for storing missied configurations status
            bool isRelease = false, isDebug = false, isReleaseXML = false;
            bool isReleaseXmlMVC3 = false, isReleaseXmlMVC4 = false, isReleaseXmlMVC5 = false, isReleaseXmlMVC5_1 = false;
            bool isReleaseMVC3 = false, isDebugMVC3 = false, isReleaseMVC4 = false, isDebugMVC4 = false, isReleaseMVC5 = false, isDebugMVC5 = false, isReleaseMVC5_1 = false, isDebugMVC5_1 = false;
            string attributeValue = string.Empty;
            XmlDocument doc = new XmlDocument();
            if (File.Exists(projectpath))
            {
                doc.Load(projectpath);
                XmlNodeList nodeList = doc.GetElementsByTagName("PropertyGroup");
                foreach (XmlNode parentnode in nodeList)
                {
                    if (parentnode.Attributes != null && parentnode.Attributes.Count != 0)
                    {
                        var conditionAttribute = parentnode.Attributes["Condition"];
                        attributeValue = conditionAttribute.Value;
                        if (!projectfilePath.ToLower().Contains("mvc"))
                        {
                            if (!isRelease)
                                isRelease = checkConfiguration(attributeValue, "Release");
                            if (!isDebug)
                                isDebug = checkConfiguration(attributeValue, "Debug");
                            if (!isReleaseXML)
                                isReleaseXML = checkConfiguration(attributeValue, "Release-XML");
                        }
                        if (projectfilePath.ToLower().Contains("mvc"))
                        {
                            if (!isReleaseMVC3)
                                isReleaseMVC3 = checkConfiguration(attributeValue, "Release-MVC3");
                            if (!isReleaseMVC4)
                                isReleaseMVC4 = checkConfiguration(attributeValue, "Release-MVC4");
                            if (!isReleaseMVC5)
                                isReleaseMVC5 = checkConfiguration(attributeValue, "Release-MVC5");
                            if (!isReleaseMVC5_1)
                                isReleaseMVC5_1 = checkConfiguration(attributeValue, "Release-MVC5_1");
                            if (!isDebugMVC3)
                                isDebugMVC3 = checkConfiguration(attributeValue, "Debug-MVC3");
                            if (!isDebugMVC4)
                                isDebugMVC4 = checkConfiguration(attributeValue, "Debug-MVC4");
                            if (!isDebugMVC5)
                                isDebugMVC5 = checkConfiguration(attributeValue, "Debug-MVC5");
                            if (!isDebugMVC5_1)
                                isDebugMVC5_1 = checkConfiguration(attributeValue, "Debug-MVC5_1");
                            if (!isReleaseXmlMVC3)
                                isReleaseXmlMVC3 = checkConfiguration(attributeValue, "Release-XML-MVC3");
                            if (!isReleaseXmlMVC4)
                                isReleaseXmlMVC4 = checkConfiguration(attributeValue, "Release-XML-MVC4");
                            if (!isReleaseXmlMVC5)
                                isReleaseXmlMVC5 = checkConfiguration(attributeValue, "Release-XML-MVC5");
                            if (!isReleaseXmlMVC5_1)
                                isReleaseXmlMVC5_1 = checkConfiguration(attributeValue, "Release-XML-MVC5_1");
                        }
                    }
                }
            }
            if ((!isRelease ||  !isReleaseXML || !isDebug ) && (!projectfilePath.ToLower().Contains("mvc")))
            {
                return true;
            }
            else if (projectfilePath.ToLower().Contains("mvc"))
            {
                if (!isReleaseMVC3 || !isReleaseMVC4 || !isReleaseMVC5 || !isReleaseMVC5_1 || !isReleaseXmlMVC3 || !isReleaseXmlMVC4 || !isReleaseXmlMVC5 || !isReleaseXmlMVC5_1 || !isDebugMVC3 || !isDebugMVC4 || !isDebugMVC5 || !isDebugMVC5_1)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Check the configuration is matched or not
        /// </summary>
        /// <param name="attributevalue"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool checkConfiguration(string attributevalue, string config)
        {
            if (attributevalue.ToLower().Contains(config.ToLower() + "|anycpu"))
            {
                // if matched
                return true;
            }
            else
            {
                // if not matched
                return false;
            }
        }


        public bool parseSolutionFile(string filename)
        {
            bool isConfigPresent = false;
            int configCount = 0, mvcConfigcount = 0;
            string currentline = string.Empty;
            StreamReader reader = new StreamReader(filename);
            do
            {
                currentline = reader.ReadLine();
                if (currentline != null)
                {
                    if (!filename.ToLower().Contains("mvc"))
                    {
                        if (currentline.Contains("Debug|Any CPU = Debug|Any CPU"))
                        {
                            configCount++;
                        }
                        if (currentline.Contains("Release|Any CPU = Release|Any CPU"))
                        {
                            configCount++;
                        }
                        if (currentline.Contains("Release-XML|Any CPU = Release-XML|Any CPU"))
                        {
                            configCount++;
                        }
                        if (configCount == 3)
                        {
                            isConfigPresent = true;
                            break;
                        }
                    }
                    else
                    {
                        if (currentline.Contains("Debug-MVC3|Any CPU = Debug-MVC3|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Debug-MVC4|Any CPU = Debug-MVC4|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Debug-MVC5|Any CPU = Debug-MVC5|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Debug-MVC5_1|Any CPU = Debug-MVC5_1|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Release-MVC3|Any CPU = Release-MVC3|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Release-MVC4|Any CPU = Release-MVC4|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Release-MVC5|Any CPU = Release-MVC5|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Release-MVC5_1|Any CPU = Release-MVC5_1|Any CPU"))
                        {
                            mvcConfigcount++;
                        } 
                        if (currentline.Contains("Release-xml-MVC3|Any CPU = Release-xml-MVC3|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Release-xml-MVC4|Any CPU = Release-xml-MVC4|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Release-xml-MVC5|Any CPU = Release-xml-MVC5|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (currentline.Contains("Release-xml-MVC5_1|Any CPU = Release-xml-MVC5_1|Any CPU"))
                        {
                            mvcConfigcount++;
                        }
                        if (configCount == 12)
                        {
                            isConfigPresent = true;
                            break;
                        }

                    }
                }
            } while (currentline != null);
            
            return isConfigPresent;
        }


        public bool isDebugAnycpuPresent(string svnpath, string platform, string projectname,string vsversion,string mvcversion,bool isSL5)
        {
            string configValue = "Debug|AnyCpu";
            isMatch = true;
            if (!string.IsNullOrEmpty(mvcversion))
            {
                if(mvcversion.ToLower().Equals("mvc3"))
                {
                    configValue = "Debug-MVC3|AnyCpu";
                }
                else if (mvcversion.ToLower().Equals("mvc4"))
                {
                    configValue = "Debug-MVC4|AnyCpu";
                }
                else if (mvcversion.ToLower().Equals("mvc5"))
                {
                    configValue = "Debug-MVC5|AnyCpu";
                }
				else if (mvcversion.ToLower().Equals("mvc6"))
                {
                    configValue = "Debug-MVC6|AnyCpu";
                }
                else
                {
                    configValue = "Debug-MVC5_1|AnyCpu";
                }
            }

            if (isSL5)
            {
                projectFileName = getSL5FileName(svnpath, platform, projectname);
                if (File.Exists(projectFileName))
                    isMatch = parseConfigurations(projectFileName, configValue);
            }
            else
            {
                projectFileName = getFileName(svnpath, platform, projectname, vsversion);
                if (File.Exists(projectFileName))
                    isMatch = parseConfigurations(projectFileName, configValue);
            }
            return isMatch;
        }

        public bool isReleaseAnycpuPresent(string svnpath, string platform, string projectname, string vsversion, string mvcversion,bool isSL5)
        {
            isMatch = true;
            string configValue = "Release|AnyCpu";
            //isMatch = false;
            if (!string.IsNullOrEmpty(mvcversion))
            {
                if (mvcversion.ToLower().Equals("mvc3"))
                {
                    configValue = "Release-MVC3|AnyCpu";
                }
                else if (mvcversion.ToLower().Equals("mvc4"))
                {
                    configValue = "Release-MVC4|AnyCpu";
                }
                else if (mvcversion.ToLower().Equals("mvc5"))
                {
                    configValue = "Release-MVC5|AnyCpu";
                }
				else if (mvcversion.ToLower().Equals("mvc6"))
                {
                    configValue = "Release-MVC6|AnyCpu";
                }
                else
                {
                    configValue = "Release-MVC5_1|AnyCpu";
                }

            }

            if (isSL5)
            {
                projectFileName = getSL5FileName(svnpath, platform, projectname);
                if (File.Exists(projectFileName))
                    isMatch = parseConfigurations(projectFileName, configValue);
            }
            else
            {
                projectFileName = getFileName(svnpath, platform, projectname, vsversion);
                if (File.Exists(projectFileName))
                    isMatch = parseConfigurations(projectFileName, configValue);
            }
            return isMatch;
        }


        public bool isReleaseXmlAnycpuPresent(string svnpath, string platform, string projectname, string vsversion, string mvcversion,bool isSL5)
        {
            isMatch = true;
            string configValue = "Release-Xml|AnyCpu";
            //isMatch = false;
            if (!string.IsNullOrEmpty(mvcversion))
            {
                if (mvcversion.ToLower().Equals("mvc3"))
                {
                    configValue = "Release-Xml-MVC3|AnyCpu";
                }
                else if (mvcversion.ToLower().Equals("mvc4"))
                {
                    configValue = "Release-Xml-MVC4|AnyCpu";
                }
                else if (mvcversion.ToLower().Equals("mvc5"))
                {
                    configValue = "Release-Xml-MVC5|AnyCpu";
                }
				else if (mvcversion.ToLower().Equals("mvc6"))
                {
                    configValue = "Release-Xml-MVC6|AnyCpu";
                }
                else
                {
                    configValue = "Release-Xml-MVC5_1|AnyCpu";
                }
            }

            if (isSL5)
            {
                projectFileName = getSL5FileName(svnpath, platform, projectname);
                if (File.Exists(projectFileName))
                    isMatch = parseConfigurations(projectFileName, configValue);
            }
            else
            {
                projectFileName = getFileName(svnpath, platform, projectname, vsversion);
                if (File.Exists(projectFileName))
                    isMatch = parseConfigurations(projectFileName, configValue);
            }
            return isMatch;
        }


        public bool parseConfigurations(string projectpath,string config )
        {
            bool isConfigPresent = false;
            string attributeValue = string.Empty;
            XmlDocument doc = new XmlDocument();
            if (File.Exists(projectpath))
            {
                doc.Load(projectpath);
                XmlNodeList nodeList = doc.GetElementsByTagName("PropertyGroup");
                foreach (XmlNode parentnode in nodeList)
                {
                    if (parentnode.Attributes != null && parentnode.Attributes.Count != 0)
                    {
                        var conditionAttribute = parentnode.Attributes["Condition"];
                        attributeValue = conditionAttribute.Value;
                        if (attributeValue.ToLower().Contains(config.ToLower()))
                        {
                            isConfigPresent = true;
                            return isConfigPresent;
                        }
                    }
                }
            }
            return isConfigPresent;
        }

        public string getFileName(string svnpath, string platform, string projectname,string vsversion)
        {
            string fileName = string.Empty;
            
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if ((platform.ToLower().Equals("windows") | platform.ToLower().Equals("base")) && vsversion.Equals("2005"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname +"_" +vsversion + ".csproj";
                //projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2005.sln";
            }
            if ((platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web")) && vsversion.Equals("2008"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname + "_2008.csproj";
                //projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2008.sln";
            }
            if (vsversion.Equals("2010") && !projectname.ToLower().Contains("dll.design"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
               // projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2010.sln";
            }
            if (!platform.ToLower().Equals("silverlight") && vsversion.Equals("2012") && !projectname.ToLower().Contains("dll.design"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname + "_2012.csproj";
                //projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2012.sln";
            }
            if (!platform.ToLower().Equals("silverlight") && vsversion.Equals("2013") && !projectname.ToLower().Contains("dll.design"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname + "_2013.csproj";
                
            }
			if (!platform.ToLower().Equals("silverlight") && vsversion.Equals("2015") && !projectname.ToLower().Contains("dll.design"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname + "_2015.csproj";
            }
            return fileName;
        }

        public string getSL5FileName(string svnpath, string platform, string projectname)
        {
            string fileName = string.Empty;
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if (!projectname.ToLower().Contains("design"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
             //   projectLocation = projectfilePath + "\\Syncfusion." + projectname + "5_2010.sln";
            }
            return fileName;
        }

        /// <summary>
        /// Checks the UWP Projects were created using Visual Studio 2015 RTM version
        /// </summary>
        /// <param name="svnpath"></param>
        /// <param name="platform"></param>
        /// <param name="projectname"></param>
        /// <param name="VSVersion"></param>
        /// <returns></returns>
        public bool CheckSolutionFileVersion(string svnpath, string platform, string projectname, string VSVersion)
        {
            try
            {
                projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
                string projectName = projectfilePath + "\\Syncfusion." + projectname + "_" + VSVersion + ".sln";

                if (File.Exists(projectName))
                {
                    string[] vsVersion;
                    string[] readSolutionFileContent = File.ReadAllLines(projectName);
                    foreach (string vsVersionLine in readSolutionFileContent)
                    {
                        if (vsVersionLine.ToLower().Contains("visualstudioversion") && !vsVersionLine.ToLower().Contains("minimumvisualstudioversion"))
                        {
                            vsVersion = Regex.Split(vsVersionLine, @"\.");
                            if (Convert.ToInt32(vsVersion[2]) >= 23107)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return false;
        }

    }
}
