using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace Syncfusion.ProjectSettings.Checker
{
    public class ConditionalCompilationChecker
    {
        string projectFileName = string.Empty;
        string projectfilePath = string.Empty;

        ProjectLocation objLocation = new ProjectLocation();

        /// <summary>
        /// This function retuns the boolean value based on the DefineConstants values which is in project file
        /// </summary>
        /// <param name="svnpath"></param>
        /// <param name="platform"></param>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public bool isDefineConstantMismatch(string svnpath, string platform, string projectname, string VSVersion )
        {
            // Local variables
             bool isMismatch = false, isSlnMismatch = false;
             string projectfilePath = string.Empty;

            ProjectLocation objLocation = new ProjectLocation();
            string projectLocation = string.Empty;
            // Set VS2005 project file location, if platform is equal to windows or base
            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if (platform.ToLower().Equals("windows") || platform.ToLower().Equals("base"))
            {
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2005.csproj";
                if (File.Exists(projectLocation))
                {
                    isMismatch = parsingProjectFile(projectLocation);
                }
                
            }

            // Set VS2008 project files, if platform is equal to wpf or windows or web or base
            if (platform.ToLower().Equals("base") || platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("web"))
            {
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2008.csproj";
                if (File.Exists(projectLocation))
                {
                    isMismatch = parsingProjectFile(projectLocation);
                }
            }

            // Set VS2010 and VS2012 project files, if platform is not equal to silverlight
            if (!platform.ToLower().Equals("silverlight"))
            {
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_"+VSVersion+".csproj";
                if (File.Exists(projectLocation))
                {
                    isMismatch = parsingProjectFile(projectLocation);
                }

                
            }

            // Only for silverlight projects
            if (platform.ToLower().Equals("silverlight") && !projectname.ToLower().Contains("dll.design"))
            {
                // For silverlight 4 projects
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "_2010.csproj";
                if (File.Exists(projectLocation))
                {
                    isMismatch = parsingProjectFile(projectLocation);
                }

                // For silverlight5 projeccts 
                projectLocation = projectfilePath + "\\Syncfusion." + projectname + "5_2010.csproj";
                // Check file existence
                if (File.Exists(projectLocation))
                {
                    isMismatch = parsingProjectFile(projectLocation);
                }
            }

            return isMismatch;
        }

        /// <summary>
        /// Read the project file and get DefineConstants innertext value
        /// </summary>
        /// <param name="projectpath"></param>
        /// <returns></returns>
        public bool parsingProjectFile(string projectpath)
        {
            string attributeValue = string.Empty;
            string constantValue = string.Empty;
            bool isTagPresent = false, isTagvalue = false; // Variables for storing result of DefineConstants tag existence
            XmlDocument doc = new XmlDocument();
            if (File.Exists(projectpath))
            {
                // Load the project file
                doc.Load(projectpath);
                // get all proprtygroup nodes from project file
                XmlNodeList nodeList = doc.GetElementsByTagName("PropertyGroup");
                foreach (XmlNode parentnode in nodeList)
                {
                    if (parentnode.Attributes != null && parentnode.Attributes.Count != 0)
                    {
                        XmlNodeList childNodeList = parentnode.ChildNodes;
                        var conditionAttribute = parentnode.Attributes["Condition"];
                        attributeValue = conditionAttribute.Value;
                        foreach (XmlNode childnode in childNodeList)
                        {
                            if (childnode.Name.ToLower().Equals("defineconstants"))
                            {
                                isTagPresent = true;
                                constantValue = childnode.InnerText;
                                //isTagvalue = checkDefineConstant(attributeValue, constantValue, projectfilePath);
                                isTagvalue = checkDefineConstant(attributeValue, constantValue, projectpath);
                            }
                        }
                    }
                }

                // if constant symbols missed or constant symbol mismatched
                if (!isTagPresent || isTagvalue)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check the constant value by calling checkByFramework() function 
        /// </summary>
        /// <param name="attributevalue"></param>
        /// <param name="constantinnertext"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool checkDefineConstant(string attributevalue, string constantinnertext, string filename)
        {
            bool isConstantSymbolMissed = false;
            if (filename.ToLower().Contains("2005"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(attributevalue, constantinnertext, filename, "syncfusionframework2_0");
            }
            if (filename.ToLower().Contains("2008"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(attributevalue, constantinnertext, filename, "syncfusionframework3_5");
            }
            if (filename.ToLower().Contains("2010"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(attributevalue, constantinnertext, filename, "syncfusionframework4_0");
            }
            if (filename.ToLower().Contains("2012"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(attributevalue, constantinnertext, filename, "syncfusionframework4_5");
            }
            if (filename.ToLower().Contains("2013"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(attributevalue, constantinnertext, filename, "syncfusionframework4_5_1");
            }
			if (filename.ToLower().Contains("2015"))
           {
                isConstantSymbolMissed = checkConditionalSymbol(attributevalue, constantinnertext, filename, "syncfusionframework4_6");
            }

            return isConstantSymbolMissed;
        }


        /// <summary>
        /// Check define constants symbol inner text and return the boolean value
        /// </summary>
        /// <param name="attributevalue"></param>
        /// <param name="constantsymboltext"></param>
        /// <param name="filename"></param>
        /// <param name="condionalsymbol"></param>
        /// <returns></returns>
        public bool checkConditionalSymbol(string attributevalue, string constantsymboltext, string filename, string condionalsymbol)
        {
            bool isConditionalNotPresent = false;
            if ((!filename.ToLower().Contains("mvc") && !filename.ToLower().Contains("silverlight")) || (filename.ToLower().Contains("silverlight") && filename.ToLower().Contains("design")) || filename.ToLower().Contains("olapsilverlight.basewrapper") || filename.ToLower().Contains("phone"))
            {
                if (attributevalue.ToLower().Contains("debug|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-xml|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol))
                    {
                        isConditionalNotPresent = true;
                    }
                }
            }
            //For MVC Projects
            else if (filename.ToLower().Contains("mvc"))
            {
                //MVC3
                if (attributevalue.ToLower().Contains("debug-mvc3|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc3"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-mvc3|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc3"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-xml-mvc3|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc3"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                // MVC4
                if (attributevalue.ToLower().Contains("debug-mvc4|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc4"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-mvc4|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc4"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-xml-mvc4|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc4"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                // MVC5
                if (attributevalue.ToLower().Contains("debug-mvc5|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc5"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-mvc5|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc5"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-xml-mvc5|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc5"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                // MVC5.1
                if (attributevalue.ToLower().Contains("debug-mvc5_1|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc5_1"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-mvc5_1|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc5_1"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-xml-mvc5_1|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("mvc5_1"))
                    {
                        isConditionalNotPresent = true;
                    }
                }

            }

            // For silverlight projects // Design check removed
            else if (filename.ToLower().Contains("silverlight") && !filename.ToLower().Contains("design") && !filename.ToLower().Contains("olapsilverlight.basewrapper") && !filename.ToLower().Contains("5_2010.csproj"))
            {
                // Check "Silverlight4" is in DefineConstant
                if (attributevalue.ToLower().Contains("debug|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("silverlight4"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("silverlight4"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-xml|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("silverlight4"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
            }

            // Check "Silverlight5" is in DefineConstant

            else if (filename.ToLower().Contains("silverlight") && !filename.ToLower().Contains("design") && filename.ToLower().Contains("5_2010.csproj"))
            {
                if (attributevalue.ToLower().Contains("debug|anycpu"))
                {
                    //if (!constantsymboltext.ToLower().Contains("debug") || !constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("silverlight5"))
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("silverlight5"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("silverlight5"))
                    {
                        isConditionalNotPresent = true;
                    }
                }
                if (attributevalue.ToLower().Contains("release-xml|anycpu"))
                {
                    if (!constantsymboltext.ToLower().Contains(condionalsymbol) || !constantsymboltext.ToLower().Contains("silverlight5"))
                    {
                        isConditionalNotPresent = true;
                    }
                }

            }

            return isConditionalNotPresent;
        }

        #region Debug configurations
        public bool getDebugConditionalSymbol(string svnpath, string platform, string projectname, string vsversion, string mvcversion,bool isSL5)
        {
            bool isMismatch = false;
            string configValue = "Debug|AnyCpu";
            string conditionalSymbol = string.Empty;

            if (!string.IsNullOrEmpty(mvcversion))
            {
                if (mvcversion.ToLower().Equals("mvc3"))
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
                {
                    conditionalSymbol = parseProjectFile(projectFileName, configValue);
                    isMismatch = checkDefineConstants(configValue, conditionalSymbol, projectFileName);
                }
            }
            else
            {
                projectFileName = getFileName(svnpath, platform, projectname, vsversion);
                if (File.Exists(projectFileName))
                {
                    // Get DefineConstants text
                    conditionalSymbol = parseProjectFile(projectFileName, configValue);

                    // Check conditional compilational synbol
                    isMismatch = checkDefineConstants(configValue, conditionalSymbol, projectFileName);
                }
            }
            return isMismatch;
        }

        #endregion

        #region Release configurations
        public bool getReleaseConditionalSymbol(string svnpath, string platform, string projectname, string vsversion, string mvcversion,bool isSL5)
        {
            bool isMismatch = false;
            string configValue = "Release|AnyCpu";
            string conditionalSymbol = string.Empty;

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
                {
                    conditionalSymbol = parseProjectFile(projectFileName, configValue);
                    isMismatch = checkDefineConstants(configValue, conditionalSymbol, projectFileName);
                }
            }
            else
            {
                projectFileName = getFileName(svnpath, platform, projectname, vsversion);
                if (File.Exists(projectFileName))
                {
                    // Get DefineConstants text
                    conditionalSymbol = parseProjectFile(projectFileName, configValue);

                    // Check conditional compilational synbol
                    isMismatch = checkDefineConstants(configValue, conditionalSymbol, projectFileName);
                }
            }
            return isMismatch;
        }

        #endregion

        #region Release-Xml configurations
        public bool getReleaseXmlConditionalSymbol(string svnpath, string platform, string projectname, string vsversion, string mvcversion,bool isSL5)
        {
            bool isMismatch = false;
            string configValue = "Release-Xml|AnyCpu";
            string conditionalSymbol = string.Empty;

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

            projectFileName = getFileName(svnpath, platform, projectname, vsversion);
            if (File.Exists(projectFileName))
            {
                // Get DefineConstants text
                conditionalSymbol = parseProjectFile(projectFileName, configValue);

                // Check conditional compilational synbol
                isMismatch = checkDefineConstants(configValue, conditionalSymbol, projectFileName);
            }
            //if (platform.ToLower().Equals("silverlight"))
            if(isSL5)
            {
                projectFileName = getSL5FileName(svnpath, platform, projectname);
                if (File.Exists(projectFileName))
                {
                    conditionalSymbol = parseProjectFile(projectFileName, configValue);
                    isMismatch = checkDefineConstants(configValue, conditionalSymbol, projectFileName);
                }
            }

            return isMismatch;
        }

        #endregion
        public string getFileName(string svnpath, string platform, string projectname, string vsversion)
        {
            string fileName = string.Empty;

            projectfilePath = objLocation.projectPath(svnpath, platform, projectname);
            if ((platform.ToLower().Equals("windows") | platform.ToLower().Equals("base")) && vsversion.Equals("2005"))
            {
                fileName = projectfilePath + "\\Syncfusion." + projectname + vsversion + ".csproj";
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

        public string parseProjectFile(string filename, string configvalue)
        {
            string attributeValue = string.Empty;
            string constantValue = string.Empty;
            bool isTagPresent = false, isTagvalue = false; // Variables for storing result of DefineConstants tag existence
            XmlDocument doc = new XmlDocument();
            if (File.Exists(filename))
            {
                // Load the project file
                doc.Load(filename);
                // get all proprtygroup nodes from project file
                XmlNodeList nodeList = doc.GetElementsByTagName("PropertyGroup");
                foreach (XmlNode parentnode in nodeList)
                {
                    if (parentnode.Attributes != null && parentnode.Attributes.Count != 0)
                    {
                        XmlNodeList childNodeList = parentnode.ChildNodes;
                        var conditionAttribute = parentnode.Attributes["Condition"];
                        attributeValue = conditionAttribute.Value;
                        if (attributeValue.ToLower().Contains(configvalue.ToLower()))
                        {
                            foreach (XmlNode childnode in childNodeList)
                            {
                                if (childnode.Name.ToLower().Equals("defineconstants"))
                                {
                                    constantValue = childnode.InnerText;
                                    return constantValue;
                                }
                            }
                        }
                    }
                }
            }
            return constantValue;
        }

        public bool checkDefineConstants(string configvalue, string constantinnertext, string filename)
        {
            bool isConstantSymbolMissed = false;
            if (filename.ToLower().Contains("_2005"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(configvalue, constantinnertext, filename, "syncfusionframework2_0");
            }
            if (filename.ToLower().Contains("_2008"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(configvalue, constantinnertext, filename, "syncfusionframework3_5");
            }
            if (filename.ToLower().Contains("_2010"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(configvalue, constantinnertext, filename, "syncfusionframework4_0");
            }
            if (filename.ToLower().Contains("_2012"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(configvalue, constantinnertext, filename, "syncfusionframework4_5");
            }
            if (filename.ToLower().Contains("_2013"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(configvalue, constantinnertext, filename, "syncfusionframework4_5_1");
            }
			if (filename.ToLower().Contains("_2015"))
            {
                isConstantSymbolMissed = checkConditionalSymbol(configvalue, constantinnertext, filename, "syncfusionframework4_6");
            }
            return isConstantSymbolMissed;
        }

        public bool XMLEntryinReleasse(string projectpath)
        {
            string attributeValue = string.Empty;
            string configvalue = "release|anycpu";
            string constantValue = string.Empty;
            bool isXMLPresent = false; // Variables for storing result of documentation tag existence in Release Mode.
            XmlDocument doc = new XmlDocument();
            if (File.Exists(projectpath) && projectfilePath.ToLower().Contains("design"))
            {
                // Load the project file
                doc.Load(projectpath);
                // get all proprtygroup nodes from project file
                XmlNodeList nodeList = doc.GetElementsByTagName("PropertyGroup");
                foreach (XmlNode parentnode in nodeList)
                {
                    if (parentnode.Attributes != null && parentnode.Attributes.Count != 0)
                    {
                        XmlNodeList childNodeList = parentnode.ChildNodes;
                        var conditionAttribute = parentnode.Attributes["Condition"];
                        attributeValue = conditionAttribute.Value;
                        if (attributeValue.ToLower().Contains(configvalue.ToLower()))
                        {
                            foreach (XmlNode childnode in childNodeList)
                            {
                                if (childnode.Name.ToLower().Equals("documentationfile"))
                                {
                                    constantValue = childnode.InnerText;
                                    if (constantValue.ToLower().Contains("xml"))
                                    {
                                        isXMLPresent = true;
                                    }
                                }
                            }
                        }
                    }
                }

                // if constant symbols missed or constant symbol mismatched
                if (isXMLPresent)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
