using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Syncfusion.ProjectSettings.Checker;

namespace ProjectSettingsConsole
{
    class Program
    {
        public static Dictionary<FileInfo, string> fi = new Dictionary<FileInfo, string>();
        public static DirectoryInfo excludeDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "ExcludeList");
        public static DirectoryInfo di;


        static void Main(string[] args)
        {
            bool isMismatched = false;
            string path = string.Empty;
            string excludeListFile = String.Empty;
            string excludeListVSVersionFile = String.Empty;
            string VSVersion = args[2];
            string tempVSVersion = string.Empty;
            string MismatchedVSVersion = string.Empty;
            string ConditionalCompilationSymbol = string.Empty;
            string TargetFrameworkVersion = string.Empty;
            string sourceType = args[3];


            bool isFolderStruc = false, isProjName = false, isAssemblyName = false, isSlnconfig = false, isConditionalSymbol = false, isDelaySign = false, isTargetFW = false, isValidateMismatch = false, isFileDescription = false, isProductName = false, isProjReference = false, isCoreReferred = false, isOutputPathMismatch = false, isRTMVersionNotUsed = false;

            if (args[3].Equals("Source")||args[3].Equals("Samples"))
            {
                // Set parent path 
                if (args[1].ToLower().Equals("wp8"))
                {
                    path = args[0] + "\\Phone\\" + args[1];
                }
                else if (args[1].ToLower().Equals("wp81sl"))
                {
                    path = args[0] + "\\Phone\\Silverlight";
                }
                else if (args[1].ToLower().Equals("wp81winrt"))
                {
                    path = args[0] + "\\Phone\\WinRT";
                }
                else
                    path = args[0] + "\\" + args[1];
            }
            else if (args[3].Equals("Utilities"))
            {
                sourceType = "Utilities";
                path = args[0] + @"\Utilities\";
            }

            // Get exclude list file name based on the platform
            excludeListFile = getExcludeListFilename(args[1]);

            excludeListVSVersionFile = getExcludeListVSVersion(args[2]);

            ProjectName objProjectName = new ProjectName();
            ProjectCheck objProjectCheck = new ProjectCheck();
            AssemblyName objAssemblyName = new AssemblyName();
            SolutionConfiguration objSlnConfig = new SolutionConfiguration();
            ConditionalCompilationChecker objConditional = new ConditionalCompilationChecker();
            TargetFrameworkVersionChecker objTargetFramework = new TargetFrameworkVersionChecker();
            GenerateReportLog objLog = new GenerateReportLog();
            ProjectLocation objLocation = new ProjectLocation();
            ValidateXaml objValidateXaml = new ValidateXaml();
            ProjectReferenceCheck objProjReference = new ProjectReferenceCheck();
            ProjectCoreReferenceCheck objCoreReference = new ProjectCoreReferenceCheck();

            if (sourceType.Equals("Source"))
            {
                if (Directory.Exists(path))
                {
                    di = new DirectoryInfo(path);
                    string[] dirInfoList = Directory.GetDirectories(path);
                    foreach (string dirName in dirInfoList)
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                        isMismatched = false;
                        if (ExcludeDirectory(excludeListFile, dirInfo) && !dirInfo.FullName.Contains(".svn"))
                        {
                            if (!FolderStructure.isFolderStructureMismatch(args[0], args[1], dirInfo.Name.ToString())) // false means present
                            {
                                if (ExcludeDirectory(excludeListVSVersionFile, dirInfo))
                                {
                                    tempVSVersion = objProjectCheck.isProjectFileMismatch(args[0], args[1], dirInfo.Name.ToString(), args[2]);  // returns true - mismatched
                                    if (!string.IsNullOrEmpty(tempVSVersion))
                                    {
                                        objLog.FolderStructureReport(@"Syncfusion." + dirInfo.Name.ToString() + "_" + tempVSVersion, @"Missed", "missedvisualstudioprojectfiles", tempVSVersion, dirInfo.Name.ToString(), "", sourceType);
                                        isMismatched = true;
                                    }
                                    if (args[2].Equals("2010") && (!args[1].ToLower().Equals("wp81sl") || args[1].ToLower().Equals("wp81winrt")))
                                    {
                                        bool isFileDescriptionpresent = AssemblyAttributes.IsAssemblyAttributesExists(args[0], args[1], dirInfo.Name.ToString(), "AssemblyTitle");
                                        if (!isFileDescriptionpresent)
                                        {
                                            objLog.FolderStructureReport(@"Syncfusion." + dirInfo.Name.ToString(), @"Missed", "filedescription", args[2], dirInfo.Name.ToString(), "",sourceType);
                                            isMismatched = true;
                                        }
                                        bool isProductNamepresent = AssemblyAttributes.IsAssemblyAttributesExists(args[0], args[1], dirInfo.Name.ToString(), "AssemblyProduct");
                                        if (!isProductNamepresent)
                                        {
                                            objLog.FolderStructureReport(@"Syncfusion." + dirInfo.Name.ToString(), @"Missed", "productname", args[2], dirInfo.Name.ToString(), "",sourceType);
                                            isMismatched = true;
                                        }
                                    }
                                }

                                // check xml configuration in Release mode. 
                                if (dirName.ToLower().Contains("design"))
                                {
                                    string filename = objConditional.getFileName(args[0], args[1], dirInfo.Name.ToString(), args[2]);
                                    bool isXMLPresent = objConditional.XMLEntryinReleasse(filename);
                                    if (isXMLPresent)
                                    {
                                        objLog.FolderStructureReport("No", "Yes", "isxmlpresent", args[2], dirInfo.Name.ToString(), "",sourceType);
                                        isMismatched = true;
                                    }

                                    // Check Syncfusion.Core reference
                                    if (objCoreReference.isVSOldProjectCoreReferenceEnable(args[0], args[1], dirInfo.Name.ToString(), VSVersion, sourceType))
                                    {
                                        objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                        isMismatched = true;
                                        isCoreReferred = true;
                                    }

                                }

                                // check configuration settings for 2005 projects
                                #region VS 2005 projects
                                if ((args[1].ToLower().Equals("windows") || args[1].ToLower().Equals("base")) && ExcludeDirectory(excludeListVSVersionFile, dirInfo))
                                {
                                    isFolderStruc = false; isProjName = false; isAssemblyName = false; isSlnconfig = false; isConditionalSymbol = false; isDelaySign = false; isTargetFW = false; isFileDescription = false; isProductName = false;
                                    if (objProjectName.isVSOldProjectNameMismatch(args[0], args[1], dirInfo.Name.ToString(), VSVersion))  // returns true - mismatched
                                    {
                                        if (VSVersion.Equals("2005"))
                                        {
                                            objLog.FolderStructureReport(@"syncfusion.projectname_2005", @"Missed", "projectname", "2005", dirInfo.Name.ToString(), "", sourceType);
                                            isMismatched = true;
                                            isProjName = true;
                                        }
                                        else if (VSVersion.Equals("2008"))
                                        {
                                            objLog.FolderStructureReport(@"Syncfusion." + dirInfo.Name.ToString() + "_2008", @"Missed", "projectname", "2008", dirInfo.Name.ToString(), "", sourceType);
                                            isMismatched = true;
                                            isProjName = true;
                                        }
                                    }
                                    else
                                    {
                                        // Check assembly name 
                                        if (objAssemblyName.isVSOldAssemblynameMismatch(args[0], args[1], dirInfo.Name.ToString(), VSVersion))  // returns true - mismatched
                                        {
                                            objLog.FolderStructureReport(@"syncfusion.projectname", @"Missed", "assemblyname", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                            isMismatched = true;
                                            isAssemblyName = true;
                                        }

                                        // Check Project Reference 
                                        if (objProjReference.isVSOldProjectReferenceEnable(args[0], args[1], dirInfo.Name.ToString(), VSVersion))  // returns true - mismatched
                                        {
                                            objLog.FolderStructureReport(@"You have referred a Project instead of Assembly", @"Missed", "projectreference", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                            isMismatched = true;
                                            isProjReference = true;
                                        }

                                        // Check Syncfusion.Core reference
                                        if (args[1].Contains("EJ.LightSwitch"))
                                        {
                                            try
                                            {
                                                string[] Projfiles = Directory.GetFiles(path + @"\Web\Src\", "*.csproj", SearchOption.AllDirectories);
                                                isMismatched = false;
                                                foreach (string file in Projfiles)
                                                {
                                                    string filename = Path.GetFileName(file);
                                                    string utility = Path.GetFileNameWithoutExtension(file);

                                                    if (ProjectCoreReferenceCheck.getProjectCoreReference(file))
                                                    {
                                                        objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, utility, "", sourceType);
                                                        isMismatched = true;
                                                        isCoreReferred = true;
                                                    }

                                                }

                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                        else if (objCoreReference.isVSOldProjectCoreReferenceEnable(args[0], args[1], dirInfo.Name.ToString(), VSVersion, sourceType))
                                        {
                                            objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                            isMismatched = true;
                                            isCoreReferred = true;
                                        }

                                        #region Check UWP Projects Visual Studio Version

                                        if (args[1].ToLower().Equals("uwp") && !objSlnConfig.CheckSolutionFileVersion(args[0], args[1], dirInfo.Name.ToString(), VSVersion))
                                        {
                                            objLog.FolderStructureReport(@"Project was not created using Visual Studio 2015 RTM", @"Visual Studio 2015 RC", "vs2015rtmversion", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                            isRTMVersionNotUsed = true;
											isMismatched = true;
                                        }

                                        #endregion

                                        #region Checking Solution configurations
                                        // check solution configurations such as Debug, Release and Release-Xml
                                        if (!args[1].ToLower().Equals("mvc"))
                                        {
                                            if (VSVersion.Equals("2005"))
                                            {
                                                ConditionalCompilationSymbol = "SyncfusionFramework2_0";
                                            }
                                            else if (VSVersion.Equals("2008"))
                                            {
                                                ConditionalCompilationSymbol = "SyncfusionFramework3_5";
                                            }

                                            // Checking Debug configuration
                                            if (objSlnConfig.isDebugAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false)) //retun true - present
                                            {
                                                if (objConditional.getDebugConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                {
                                                    // Mismatched
                                                    objLog.FolderStructureReport(@"Under Debug|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                    isMismatched = true;
                                                }

                                            }
                                            else
                                            {
                                                objLog.FolderStructureReport(@"Debug|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                isMismatched = true;
                                            }

                                            // Checking Release Configuration
                                            if (objSlnConfig.isReleaseAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                            {
                                                if (objConditional.getReleaseConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                {
                                                    // Mismatched
                                                    objLog.FolderStructureReport(@"Under Release|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                    isMismatched = true;
                                                }
                                            }
                                            else
                                            {
                                                objLog.FolderStructureReport(@"Release|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                isMismatched = true;
                                            }

                                            // Checking Release-Xml configuration
                                            if (objSlnConfig.isReleaseXmlAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                            {
                                                // Check conditional symbol
                                                if (objConditional.getReleaseXmlConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                {
                                                    // Mismatched
                                                    objLog.FolderStructureReport(@"Under Release-Xml|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                    isMismatched = true;
                                                }
                                            }
                                            else
                                            {
                                                objLog.FolderStructureReport(@"Release-Xml|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                isMismatched = true;
                                            }

                                        }

                                        #endregion
                                    }
                                }
                                if (isMismatched)
                                {
                                    fillEmptyCell(isFolderStruc, isAssemblyName, isProjName, isSlnconfig, isConditionalSymbol, isDelaySign, isTargetFW, isValidateMismatch, isFileDescription, isProductName, isProjReference, isCoreReferred, isOutputPathMismatch, VSVersion, dirInfo.Name.ToString(),sourceType, isRTMVersionNotUsed);
                                }
                                #endregion


                                #region VS projects
                                if ((!args[1].ToLower().Equals("windows") && !args[1].ToLower().Equals("base")) && ExcludeDirectory(excludeListVSVersionFile, dirInfo))
                                {
                                    if (!args[1].ToLower().Equals("winrt") && !args[1].ToLower().Equals("wp8") && !args[1].ToLower().Equals("wp81sl"))
                                    {
                                        isFolderStruc = false; isProjName = false; isAssemblyName = false; isSlnconfig = false; isConditionalSymbol = false; isDelaySign = false; isTargetFW = false; isFileDescription = false; isProductName = false; isProjReference = false;
                                        if (objProjectName.isVSProjectNameMismatch(args[0], args[1], dirInfo.Name.ToString(), VSVersion))  // returns true - mismatched
                                        {
                                            objLog.FolderStructureReport(@"Syncfusion." + dirInfo.Name.ToString() + "_" + VSVersion, @"Missed", "projectname", VSVersion, dirInfo.Name.ToString(), "",sourceType);
                                            isMismatched = true;
                                            isProjName = true;
                                        }
                                        else
                                        {
                                            // Check assembly name 
                                            if (objAssemblyName.isVSAssemblynameMismatch(args[0], args[1], dirInfo.Name.ToString(), VSVersion))  // returns true - mismatched
                                            {
                                                objLog.FolderStructureReport(@"Syncfusion." + dirInfo.Name.ToString(), @"Missed", "assemblyname", VSVersion, dirInfo.Name.ToString(), "",sourceType);
                                                isMismatched = true;
                                                isAssemblyName = true;
                                            }

                                            // Check Project Reference
                                            if (objProjReference.isVSProjectReferenceEnable(args[0], args[1], dirInfo.Name.ToString(), VSVersion))  // returns true - mismatched
                                            {
                                                objLog.FolderStructureReport(@"You have referred a Project instead of Assembly", @"Missed", "projectreference", VSVersion, dirInfo.Name.ToString(), "",sourceType);
                                                isMismatched = true;
                                                isProjReference = true;
                                            }

                                            // Check Syncfusion.Core reference
                                            if (args[1].Contains("EJ.LightSwitch"))
                                            {
                                                try
                                                {
                                                    string[] Projfiles = Directory.GetFiles(path + @"\Web\Src\", "*.csproj", SearchOption.AllDirectories);
                                                    isMismatched = false;
                                                    foreach (string file in Projfiles)
                                                    {
                                                        string filename = Path.GetFileName(file);
                                                        string utility = Path.GetFileNameWithoutExtension(file);

                                                        if (ProjectCoreReferenceCheck.getProjectCoreReference(file))
                                                        {
                                                            objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, utility, "", sourceType);
                                                            isMismatched = true;
                                                            isCoreReferred = true;
                                                        }

                                                    }

                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                            }
                                            else if (objCoreReference.isVSProjectCoreReferenceEnable(args[0], args[1], dirInfo.Name.ToString(), VSVersion, sourceType))
                                            {
                                                objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                isMismatched = true;
                                                isCoreReferred = true;
                                            }

                                            #region Check UWP Projects Visual Studio Version

                                            if (args[1].ToLower().Equals("uwp") && !objSlnConfig.CheckSolutionFileVersion(args[0], args[1], dirInfo.Name.ToString(), VSVersion))
                                            {
                                                objLog.FolderStructureReport(@"Visual Studio 2015 RTM", @"Visual Studio 2015 RC", "vs2015rtmversion", VSVersion, dirInfo.Name.ToString(), "", sourceType);
												isMismatched = true;
                                                isRTMVersionNotUsed = true;
                                            }

                                            #endregion

                                            #region Checking Solution configurations and conditional symbols
                                            // check solution configurations such as Debug, Release and Release-Xml
                                            #region Other than MVC platforms
                                            if (!args[1].ToLower().Equals("mvc"))
                                            {
                                                switch (VSVersion)
                                                {
                                                    case "VS2010":
                                                        ConditionalCompilationSymbol = "SyncfusionFramework4_0";
                                                        break;
                                                    case "VS2012":
                                                        ConditionalCompilationSymbol = "SyncfusionFramework4_5";
                                                        break;
                                                    case "VS2013":
                                                        ConditionalCompilationSymbol = "SyncfusionFramework4_5_1";
                                                        break;
                                                    case "VS2015":
                                                        ConditionalCompilationSymbol = "SyncfusionFramework4_6";
                                                        break;
                                                }
                                                // Checking Debug configuration
                                                if (objSlnConfig.isDebugAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false)) //retun true - present
                                                {
                                                    // Check conditional symbol
                                                    if (objConditional.getDebugConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                    {
                                                        // Mismatched
                                                        isMismatched = true;
                                                        isConditionalSymbol = true;
                                                        if (args[1].ToLower().Equals("silverlight") && !dirInfo.Name.ToString().ToLower().Contains("design"))
                                                        {
                                                            objLog.FolderStructureReport(@"SL4 Project : Under Debug|AnyCpu", @"Missed", "conditionalsymbol", "2010", dirInfo.Name.ToString(), "SyncfusionFramework4_0;Silverlight4",sourceType);
                                                        }
                                                        else
                                                        {
                                                            objLog.FolderStructureReport(@"Under Debug|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    objLog.FolderStructureReport(@"Debug|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    isMismatched = true;
                                                    isSlnconfig = true;
                                                }

                                                // Checking Release Configuration
                                                if (objSlnConfig.isReleaseAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                {
                                                    // Check conditional symbol
                                                    if (objConditional.getReleaseConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                    {
                                                        // Mismatched
                                                        isMismatched = true;
                                                        isConditionalSymbol = true;
                                                        if (args[1].ToLower().Equals("silverlight") && !dirInfo.Name.ToString().ToLower().Contains("design"))
                                                        {
                                                            objLog.FolderStructureReport(@"SL4 Project : Under Release|AnyCpu", @"Missed", "conditionalsymbol", "2010", dirInfo.Name.ToString(), "SyncfusionFramework4_0;Silverlight4", sourceType);
                                                        }
                                                        else
                                                        {
                                                            objLog.FolderStructureReport(@"Under Release|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    isMismatched = true;
                                                    isSlnconfig = true;
                                                    objLog.FolderStructureReport(@"Release|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);

                                                }

                                                // Checking Release-Xml configuration
                                                if (objSlnConfig.isReleaseXmlAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                {
                                                    // Check conditional symbol
                                                    if (objConditional.getReleaseXmlConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "", false))
                                                    {
                                                        // Mismatched
                                                        isMismatched = true;
                                                        isConditionalSymbol = true;
                                                        if (args[1].ToLower().Equals("silverlight") && !dirInfo.Name.ToString().ToLower().Contains("design"))
                                                        {
                                                            objLog.FolderStructureReport(@"SL4 Project : Under Release-Xml|AnyCpu", @"Missed", "conditionalsymbol", "2010", dirInfo.Name.ToString(), "SyncfusionFramework4_0;Silverlight4", sourceType);
                                                        }
                                                        else
                                                        {
                                                            objLog.FolderStructureReport(@"Under Release-Xml|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    isMismatched = true;
                                                    isSlnconfig = true;
                                                    objLog.FolderStructureReport(@"Release-Xml|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);

                                                }
                                            }
                                            #endregion

                                            #region MVC platform
                                            if (args[1].ToLower().Equals("mvc"))
                                            {
                                                #region MVC3 configuration
                                                if (VSVersion.Equals("2010"))
                                                    ConditionalCompilationSymbol = "SyncfusionFramework4_0; MVC3";
                                                else if (VSVersion.Equals("2012"))
                                                    ConditionalCompilationSymbol = "SyncfusionFramework4_5; MVC3";
                                                if (!VSVersion.Equals("2015") && !VSVersion.Equals("2013") && !VSVersion.Equals("2012"))
                                                {
                                                    #region Debug-MVC3
                                                    // Checking Debug configuration
                                                    if (objSlnConfig.isDebugAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc3", false)) //retun true - present
                                                    {
                                                        if (objConditional.getDebugConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc3", false))
                                                        {
                                                            // Mismatched
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Debug-MVC3|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Debug-MVC3|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-MVC3
                                                    // Checking Release Configuration
                                                    if (objSlnConfig.isReleaseAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc3", false))
                                                    {
                                                        if (objConditional.getReleaseConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc3", false))
                                                        {
                                                            isConditionalSymbol = true;
                                                            isMismatched = true;
                                                            objLog.FolderStructureReport(@"Under Release-MVC3|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isSlnconfig = true;
                                                        isMismatched = true;
                                                        objLog.FolderStructureReport(@"Release-MVC3|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-Xml
                                                    // Checking Release-Xml configuration
                                                    if (objSlnConfig.isReleaseXmlAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc3", false))
                                                    {
                                                        if (objConditional.getReleaseXmlConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc3", false))
                                                        {
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Release-Xml-MVC3|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Release-Xml-MVC3|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                                #region MVC4 configuration
                                                if (VSVersion.Equals("2010"))
                                                    ConditionalCompilationSymbol = "SyncfusionFramework4_0; MVC4";
                                                else if (VSVersion.Equals("2012"))
                                                    ConditionalCompilationSymbol = "SyncfusionFramework4_5; MVC4";
                                                if (!VSVersion.Equals("2015") && !VSVersion.Equals("2013") && !VSVersion.Equals("2012"))
                                                {
                                                    #region Debug-MVC4
                                                    // Checking Debug configuration
                                                    if (objSlnConfig.isDebugAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc4", false)) //retun true - present
                                                    {
                                                        if (objConditional.getDebugConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc4", false))
                                                        {
                                                            // Mismatched
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Debug-MVC4|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Debug-MVC4|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-MVC4
                                                    // Checking Release Configuration
                                                    if (objSlnConfig.isReleaseAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc4", false))
                                                    {
                                                        if (objConditional.getReleaseConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc4", false))
                                                        {
                                                            isConditionalSymbol = true;
                                                            isMismatched = true;
                                                            objLog.FolderStructureReport(@"Under Release-MVC4|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isSlnconfig = true;
                                                        isMismatched = true;
                                                        objLog.FolderStructureReport(@"Release-MVC4|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-Xml
                                                    // Checking Release-Xml configuration
                                                    if (objSlnConfig.isReleaseXmlAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc4", false))
                                                    {
                                                        if (objConditional.getReleaseXmlConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc4", false))
                                                        {
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Release-Xml-MVC4|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Release-Xml-MVC4|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion
                                                }
                                                #endregion
                                                #region MVC5 configuration

                                                if (VSVersion.Equals("2012"))
                                                {
                                                    ConditionalCompilationSymbol = "SyncfusionFramework4_5; MVC5";
                                                    #region Debug-MVC5
                                                    // Checking Debug configuration
                                                    if (objSlnConfig.isDebugAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5", false)) //retun true - present
                                                    {
                                                        if (objConditional.getDebugConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5", false))
                                                        {
                                                            // Mismatched
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Debug-MVC5|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Debug-MVC5|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-MVC5
                                                    // Checking Release Configuration
                                                    if (objSlnConfig.isReleaseAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5", false))
                                                    {
                                                        if (objConditional.getReleaseConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5", false))
                                                        {
                                                            isConditionalSymbol = true;
                                                            isMismatched = true;
                                                            objLog.FolderStructureReport(@"Under Release-MVC5|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isSlnconfig = true;
                                                        isMismatched = true;
                                                        objLog.FolderStructureReport(@"Release-MVC5|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-Xml
                                                    // Checking Release-Xml configuration
                                                    if (objSlnConfig.isReleaseXmlAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5", false))
                                                    {
                                                        if (objConditional.getReleaseXmlConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5", false))
                                                        {
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Release-Xml-MVC5|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Release-Xml-MVC5|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                }
                                                #endregion
                                                #region MVC5.1 configuration

                                                if (VSVersion.Equals("2012"))
                                                {
                                                    ConditionalCompilationSymbol = "SyncfusionFramework4_5; MVC5_1";
                                                    #region Debug-MVC5.1
                                                    // Checking Debug configuration
                                                    if (objSlnConfig.isDebugAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5_1", false)) //retun true - present
                                                    {
                                                        if (objConditional.getDebugConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5_1", false))
                                                        {
                                                            // Mismatched
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Debug-MVC5_1|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Debug-MVC5_1|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-MVC5.1
                                                    // Checking Release Configuration
                                                    if (objSlnConfig.isReleaseAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5_1", false))
                                                    {
                                                        if (objConditional.getReleaseConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5_1", false))
                                                        {
                                                            isConditionalSymbol = true;
                                                            isMismatched = true;
                                                            objLog.FolderStructureReport(@"Under Release-MVC5_1|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        isSlnconfig = true;
                                                        isMismatched = true;
                                                        objLog.FolderStructureReport(@"Release-MVC5_1|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                    #region Release-Xml
                                                    // Checking Release-Xml configuration
                                                    if (objSlnConfig.isReleaseXmlAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5_1", false))
                                                    {
                                                        if (objConditional.getReleaseXmlConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc5_1", false))
                                                        {
                                                            isMismatched = true;
                                                            isConditionalSymbol = true;
                                                            objLog.FolderStructureReport(@"Under Release-Xml-MVC5_1|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        isMismatched = true;
                                                        isSlnconfig = true;
                                                        objLog.FolderStructureReport(@"Release-Xml-MVC5_1|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    }
                                                    #endregion

                                                }
                                                #endregion
                                                #region MVC6 configuration

                                            if (VSVersion.Equals("2015"))
                                            {
                                                ConditionalCompilationSymbol = "SyncfusionFramework4_6; MVC6";
                                                #region Debug-MVC6
                                                // Checking Debug configuration
                                                if (objSlnConfig.isDebugAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc6", false)) //retun true - present
                                                {
                                                    if (objConditional.getDebugConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc6", false))
                                                    {
                                                        // Mismatched
                                                        isMismatched = true;
                                                        isConditionalSymbol = true;
                                                        objLog.FolderStructureReport(@"Under Debug-MVC6|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                    }
                                                }
                                                else
                                                {
                                                    isMismatched = true;
                                                    isSlnconfig = true;
                                                    objLog.FolderStructureReport(@"Debug-MVC6|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                }
                                                #endregion

                                                #region Release-MVC6
                                                // Checking Release Configuration
                                                if (objSlnConfig.isReleaseAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc6", false))
                                                {
                                                    if (objConditional.getReleaseConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc6", false))
                                                    {
                                                        isConditionalSymbol = true;
                                                        isMismatched = true;
                                                        objLog.FolderStructureReport(@"Under Release-MVC6|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                    }
                                                }
                                                else
                                                {
                                                    isSlnconfig = true;
                                                    isMismatched = true;
                                                    objLog.FolderStructureReport(@"Release-MVC6|AnyCpu", @"Missed", "solutionconfig", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                }
                                                #endregion

                                                #region Release-Xml
                                                // Checking Release-Xml configuration
                                                if (objSlnConfig.isReleaseXmlAnycpuPresent(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc6", false))
                                                {
                                                    if (objConditional.getReleaseXmlConditionalSymbol(args[0], args[1], dirInfo.Name.ToString(), VSVersion, "mvc6", false))
                                                    {
                                                        isMismatched = true;
                                                        isConditionalSymbol = true;
                                                        objLog.FolderStructureReport(@"Under Release-Xml-MVC6|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), ConditionalCompilationSymbol, sourceType);
                                                    }
                                                }
                                                else
                                                {
                                                    isMismatched = true;
                                                    isSlnconfig = true;
                                                    objLog.FolderStructureReport(@"Release-Xml-MVC6|AnyCpu", @"Missed", "conditionalsymbol", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                }
                                                #endregion

                                            }
                                            #endregion
                                            }
                                            #endregion
                                            #endregion

                                            if (!args[1].ToLower().Equals("winrt") && !args[1].ToLower().Equals("uwp"))
                                                if (VSVersion.Equals("2008"))
                                                    TargetFrameworkVersion = "v3.5";
                                                else if (VSVersion.Equals("2010"))
                                                    TargetFrameworkVersion = "v4.0";
                                                else if (VSVersion.Equals("2012"))
                                                    TargetFrameworkVersion = "v4.5";
												else if (VSVersion.Equals("2015"))
                                                    TargetFrameworkVersion = "v4.6";
                                                else
                                                {
                                                    if (args[1].ToLower().Equals("universal"))
                                                        TargetFrameworkVersion = "v4.6";
                                                    else if (args[1].ToLower().Equals("wp81sl") || args[1].ToLower().Equals("wp81winrt"))
                                                        TargetFrameworkVersion = "v8.1";
                                                    else
                                                        TargetFrameworkVersion = "v4.5.1";
                                                }

                                            // Checking target framework version
                                            if (!VSVersion.Equals("2005") && !args[1].ToLower().Equals("uwp"))
                                            {
                                                if (objTargetFramework.isVSTargetFWMismatch(args[0], args[1], dirInfo.Name.ToString(), args[2]))
                                                {
                                                    objLog.FolderStructureReport(TargetFrameworkVersion, "Missed", "targetframework", VSVersion, dirInfo.Name.ToString(), "",sourceType);
                                                    isMismatched = true;
                                                    isTargetFW = true;
                                                }
                                            }

                                            if (args[1].ToLower().Equals("silverlight") && VSVersion.Equals("2010") && !dirInfo.Name.ToString().Contains("VisualStudio.Design") && !dirInfo.Name.ToString().Contains("Expression.Design"))
                                                // Validating Xaml

                                                if (objValidateXaml.isValidateXamlMismatch(args[0], args[1], dirInfo.Name.ToString()))
                                                {
                                                    objLog.FolderStructureReport("<ValidateXaml>false</ValidateXaml>", "Missed", "validatexaml", VSVersion, dirInfo.Name.ToString(), "", sourceType);
                                                    isMismatched = true;
                                                    isValidateMismatch = true;
                                                }
                                        }
                                    }
                                }
                                if (isMismatched)
                                {
                                    if (args[1].ToLower().Equals("silverlight") && VSVersion.Equals("2010"))
                                    {
                                        fillEmptyCell(isFolderStruc, isAssemblyName, isProjName, isSlnconfig, isConditionalSymbol, isDelaySign, isTargetFW, isValidateMismatch, isFileDescription, isProductName, isProjReference, isCoreReferred, isOutputPathMismatch, "2010", dirInfo.Name.ToString(), sourceType, isRTMVersionNotUsed);
                                    }

                                }


                            }

                        }
                        // Generate main log
                        if (isMismatched && sourceType.Equals("Source"))
                        {
                            objLog.generateMainLog(args[1], dirInfo.Name.ToString());
                        }

                    }
                }

            }
            else if (sourceType.Equals("Utilities"))
            {
                try
                {
                    //Check csproj files
                    string[] Projfiles = Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories);
                    foreach (string file in Projfiles)
                    {
                        string filename = Path.GetFileName(file);
                        string platformName = Path.GetFileNameWithoutExtension(file);
                        
                        if (ProjectCoreReferenceCheck.getProjectCoreReference(file))
                        {
                            isMismatched = false;
                            if (!platformName.Contains("License") && !platformName.Contains("Dashboard") && !platformName.Contains("KeyGen") && !platformName.Contains("Prerequisites") && !platformName.Contains("KeyChecker") && !platformName.Contains("Patch") && !platformName.Contains("DocToHtml") && !platformName.Contains("InstallInfo"))
                            {
                                objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, platformName, "", sourceType);
                                isMismatched = true;
                                isCoreReferred = true;
                            }
                            if (isMismatched)
                            {
                                objLog.generateMainLog(platformName, filename);
                            }
                        }
                        
                    }

                    //Check Web.config files
                    string[] WebConfigFiles = Directory.GetFiles(path, "*.config", SearchOption.AllDirectories);
                    foreach (string file in Projfiles)
                    {
                        string filename = Path.GetFileName(file);
                        string platformName = Path.GetFileNameWithoutExtension(file);

                        if (ProjectCoreReferenceCheck.getProjectCoreReference(file))
                        {
                            isMismatched = false;
                            if (!platformName.Contains("License") && !platformName.Contains("Dashboard") && !platformName.Contains("KeyGen") && !platformName.Contains("Prerequisites") && !platformName.Contains("KeyChecker") && !platformName.Contains("Patch") && !platformName.Contains("DocToHtml") && !platformName.Contains("InstallInfo"))
                            {
                                objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, platformName, "", sourceType);
                                isMismatched = true;
                                isCoreReferred = true;
                            }
                            if (isMismatched)
                            {
                                objLog.generateMainLog(platformName, filename);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {  }
            }
            else if (sourceType.Equals("Samples"))
            {
                di = new DirectoryInfo(path);
                string[] dirInfoList = Directory.GetDirectories(path);
                foreach (string dirName in dirInfoList)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                    string filepath = objCoreReference.SourceFilePath(args[0], args[1], dirInfo.Name.ToString(), sourceType);
                    if (Directory.Exists(filepath))
                    {
                        //check csproj files
                        string[] Projfiles = Directory.GetFiles(filepath, "*.csproj", SearchOption.AllDirectories);
                        foreach (string file in Projfiles)
                        {
                            isMismatched = false;
                            string filename = Path.GetFileName(file);
                            string platformName = dirInfo.Name.ToString();
                            string utility = platformName + "_" + Path.GetFileNameWithoutExtension(file);
                            if (!platformName.Contains("DashboardViewer.MVC"))
                            {
                                if (ProjectCoreReferenceCheck.getProjectCoreReference(file))
                                {
                                    objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, utility, "", sourceType);
                                    isMismatched = true;
                                    isCoreReferred = true;
                                }
                                if (isMismatched)
                                {
                                    objLog.generateMainLog(platformName, filename);
                                }
                            }
                        }
                    }
                    //check web.config files
                    string[] WebConfigFiles = Directory.GetFiles(dirName, "*.config", SearchOption.AllDirectories);
                    foreach (string file in WebConfigFiles)
                    {
                        isMismatched = false;
                        string filename = Path.GetFileName(file);
                        string platformName = dirInfo.Name.ToString();
                        string utility = platformName + "_" + Path.GetFileNameWithoutExtension(file);
                        if (!platformName.Contains("DashboardViewer.MVC"))
                        {
                            if (ProjectCoreReferenceCheck.getProjectCoreReference(file))
                            {
                                objLog.FolderStructureReport(@"Syncfusion.Core should not be referred in the project file.", @"Referred", "syncfusioncorereferred", VSVersion, utility, "", sourceType);
                                isMismatched = true;
                                isCoreReferred = true;
                            }
                            if (isMismatched)
                            {
                                if (platformName.Equals("Samples"))
                                    platformName = "EJ.MVC_" + platformName;
                                objLog.generateMainLog(platformName, filename);
                            }
                        }
                    }

                }
                
            }
        }
        

        /// <summary>
        /// Function for checking the directory is in Excludde list or not.
        /// </summary>
        /// <param name="excludeFile"></param>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        public static bool ExcludeDirectory(string excludeFile, DirectoryInfo directoryName)
        {
            StreamReader sr;
            FileInfo info = new FileInfo(excludeFile);
            if (info.Length != 0)
            {
                using (sr = new StreamReader(excludeFile))
                {

                    string[] s = sr.ReadLine().Split(',');
                    foreach (string ch in s)
                    {
                        if (directoryName.FullName.ToLower().Contains(ch.ToLower()))
                        {
                            return false;
                        }
                        else if (!Directory.Exists(ch) && directoryName.FullName.Contains(ch))
                            return false;
                    }
                }
            }

            return true;
        }

        public static string getExcludeListVSVersion(string _VSVersion)
        {
            string filename = string.Empty;

            if (_VSVersion.ToLower().Equals("2005"))
            {
                filename = excludeDir + @"\VS2005List.txt";
            }
            else if (_VSVersion.ToLower().Equals("2008"))
            {
                filename = excludeDir + @"\VS2008List.txt";
            }
            else if (_VSVersion.ToLower().Equals("2010"))
            {
                filename = excludeDir + @"\VS2010List.txt";
            }
            else if (_VSVersion.ToLower().Equals("2012"))
            {
                filename = excludeDir + @"\VS2012List.txt";
            }
            else if (_VSVersion.ToLower().Equals("2013"))
            {
                filename = excludeDir + @"\VS2013List.txt";
            }
            else if (_VSVersion.ToLower().Equals("2015"))
            {
                filename = excludeDir + @"\VS2015List.txt";
            }
            
            return filename;
        }

        public static string getExcludeListFilename(string _platform)
        {
            string filename = string.Empty;

            // Set excludde list file name based on platform passed
            if (_platform.ToLower().Equals("base"))
            {
                filename = excludeDir + @"\BaseList.txt";
            }
            else if (_platform.ToLower().Equals("mvc"))
            {
                filename = excludeDir + @"\MVCList.txt";
            }
            else if (_platform.ToLower().Equals("wp8"))
            {
                filename = excludeDir + @"\WP8List.txt";
            }
            else if (_platform.ToLower().Equals("silverlight"))
            {
                filename = excludeDir + @"\SilverlightList.txt";
            }
            else if (_platform.ToLower().Equals("wpf"))
            {
                filename = excludeDir + @"\WPFList.txt";
            }
            else if (_platform.ToLower().Equals("web"))
            {
                filename = excludeDir + @"\WebList.txt";
            }
            else if (_platform.ToLower().Equals("winrt"))
            {
                filename = excludeDir + @"\WinRTList.txt";
            }
            else if (_platform.ToLower().Equals("windows"))
            {
                filename = excludeDir + @"\WindowsList.txt";
            }
            else if (_platform.ToLower().Equals("universal"))
            {
                filename = excludeDir + @"\UniversalList.txt";
            }
            else if (_platform.ToLower().Equals("uwp"))
            {
                filename = excludeDir + @"\UWPList.txt";
            }
            else if (_platform.ToLower().Equals("wp81sl"))
            {
                filename = excludeDir + @"\WP81SilverlightList.txt";
            }
            else if (_platform.ToLower().Equals("wp81winrt"))
            {
                filename = excludeDir + @"\WP81WinRTList.txt";
            }
            else if (_platform.ToLower().Equals("ej.mvc"))
            {
                filename=excludeDir+@"\EJMVCList.txt";
            }
            else if (_platform.ToLower().Equals("ej.web"))
            {
                filename = excludeDir + @"\EJWebList.txt";
            }
            else if (_platform.ToLower().Equals("ej.lightswitch"))
            {
                filename = excludeDir + @"\EJLightSwitchList.txt";
            }
            return filename;

        }


        public static void fillEmptyCell(bool _folderstruc, bool _assemblyname, bool _projname, bool _slnconfig, bool _conditionalsymbol, bool _delaysign, bool _targetfw, bool _validatexaml, bool _filedescription, bool _productname, bool _projreference, bool _corereferred, bool is_outpathmismatch, string _vsversion, string filename, string sourceType, bool _isRTMVersionNotUsed)
        {
            
            GenerateReportLog objEmptyLog = new GenerateReportLog();
            if (_folderstruc || _assemblyname || _projname || _slnconfig || _conditionalsymbol || _delaysign || _targetfw || _validatexaml || _filedescription || _productname || _corereferred || _isRTMVersionNotUsed)
            {
                if (!_folderstruc)
                {
                    objEmptyLog.FolderStructureReport(@" -", @" -", "folderstructure", _vsversion, filename, "",sourceType);
                }
                if (!_assemblyname)
                {
                    objEmptyLog.FolderStructureReport(@" -", @" -", "assemblyname", _vsversion, filename, "", sourceType);
                }
                if (!_projname)
                {
                    objEmptyLog.FolderStructureReport(@" -", @" -", "projectname", _vsversion, filename, "", sourceType);
                }
                if (!_slnconfig)
                {
                    objEmptyLog.FolderStructureReport(@"-", @"-", "solutionconfig", _vsversion, filename, "", sourceType);
                }
                if (!_conditionalsymbol)
                {
                    objEmptyLog.FolderStructureReport(@"-", @"-", "conditionalsymbol", _vsversion, filename, "", sourceType);
                }
                if (!_delaysign)
                {
                    objEmptyLog.FolderStructureReport(@" -", @" -", "delaysign", _vsversion, filename, "", sourceType);
                }
                if (!_targetfw)
                {
                    objEmptyLog.FolderStructureReport(" -", " -", "targetframework", _vsversion, filename, "", sourceType);
                }
                if (!_validatexaml)
                {
                    objEmptyLog.FolderStructureReport(@"-", @"-", "validatexaml", _vsversion, filename, "", sourceType);
                }
                if (!_filedescription )
                {
                    objEmptyLog.FolderStructureReport(@"-", @"-", "filedescription", _vsversion, filename, "", sourceType);
                }
                if (!_productname)
                {
                    objEmptyLog.FolderStructureReport(@"-", @"-", "productname", _vsversion, filename, "", sourceType);
                }
                if (!_corereferred)
                {
                    objEmptyLog.FolderStructureReport(@"-", @"-", "syncfusioncorereferred", _vsversion, filename, "", sourceType);
                }
                if (!_isRTMVersionNotUsed)
                {
                    objEmptyLog.FolderStructureReport(@"-", @"-", "vs2015rtmversion", _vsversion, filename, "", sourceType);
                }
                objEmptyLog.FolderStructureReport("", "", "projectname_title", _vsversion, filename, "", sourceType);
            }
        }
    }
}
#endregion 