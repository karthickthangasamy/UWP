#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace AssemblyReference
{
    class Program
    {
        public static string content = string.Empty;
        static void Main(string[] args)
        {
            string studioVersion, studioVersionMVC=null, filePath,outReport,isLicensed,path;
            bool isLicense_NotEnforced = true, isLicense_Enforced = false;
            studioVersion = args[0];
            filePath = args[1];
            outReport = args[2];
            isLicensed = args[3];
            path = args[4];
            string licenseExcludeFilePath = path + @"\Build\Internal Tools\ProjectSettingsChecker\Tools\Excludelist\ExcludeLicensing.txt";            

            if (filePath.Contains("MVC"))
            {
                int mvcPosition = filePath.IndexOf("\\MVC");
                string mvcVersion = filePath.Substring(mvcPosition+4, 1);
                studioVersionMVC = args[0];
                studioVersion = null;
                switch (Int32.Parse(mvcVersion))
                {
                    case 3:
                    case 4:
                        for (int versionCount = 0; versionCount < args[0].Length; versionCount++)
                        {
                            if (versionCount == 4)
                                studioVersion = studioVersion + 4;
                            else if (versionCount == 5)
                                studioVersion = studioVersion + 0;
                            else
                                studioVersion = studioVersion + args[0][versionCount];
                        }
                        break;
                    case 5:
                        for (int versionCount = 0; versionCount < args[0].Length; versionCount++)
                        {
                            if (versionCount == 4)
                                studioVersion = studioVersion + 4;
                            else if (versionCount == 5)
                                studioVersion = studioVersion + 5;
                            else
                                studioVersion = studioVersion + args[0][versionCount];
                        }
                        break;
                }
                
            }

            try
            {
                Assembly asam = Assembly.LoadFrom(filePath);
                string dllName = Path.GetFileName(filePath);
                AssemblyName[] s = asam.GetReferencedAssemblies();
                foreach (AssemblyName a in s)
                {
                    if ((a.FullName.ToLower().StartsWith("syncfusion") | a.FullName.ToLower().StartsWith("democommon")) && !a.FullName.ToUpperInvariant().StartsWith("SYNCFUSION.LICENSING"))
                    {
                        if (a.Version.ToString() != studioVersionMVC && a.Name.ToLower().EndsWith("mvc"))
                        {
                            content = content + Environment.NewLine + "Assembly " + a.Name + " was compiled with " + a.Version + " instead of " + studioVersionMVC;
                            //File.AppendAllText(outReport,Environment.NewLine +filePath + Environment.NewLine + "Assembly " + a.Name + " was compiled with " + a.Version + " instead of " + studioVersion);
                        }
                        else if (a.Version.ToString() != studioVersion && !(a.Name.ToLower().EndsWith("mvc")))
                        {
                            content = content + Environment.NewLine + "Assembly " + a.Name + " was compiled with " + a.Version + " instead of " + studioVersion;
                        }
                    }

                    if (!isInLicenseExcludeList(licenseExcludeFilePath, dllName, outReport) && Path.GetExtension(filePath).ToLower().Equals(".dll")
                        && !dllName.ToUpperInvariant().Contains("DESIGN.DLL") && !dllName.ToUpperInvariant().Contains("RESOURCES.DLL"))
                    {
                        if (!dllName.ToUpperInvariant().Contains("SYNCFUSION.LICENSING") && a.FullName.ToUpperInvariant().StartsWith("SYNCFUSION.LICENSING") && ((isLicensed.ToUpperInvariant().Equals("TRUE") && !isLicense_Enforced) || (!isLicensed.ToUpperInvariant().Equals("TRUE") && isLicense_NotEnforced)))
                        {
                            if (a.FullName.ToUpperInvariant().Contains("SYNCFUSION.LICENSING"))
                            {
                                isLicense_NotEnforced = false;
                                isLicense_Enforced = true;
                            }
                        }
                    }
                }

                if (!isInLicenseExcludeList(licenseExcludeFilePath, dllName, outReport))
                {
                    if (!dllName.ToUpperInvariant().Contains("SYNCFUSION.LICENSING") && Path.GetExtension(filePath).ToLower().Equals(".dll")
                        && !dllName.ToUpperInvariant().Contains("DESIGN.DLL") && !dllName.ToUpperInvariant().Contains("RESOURCES.DLL"))
                    {
                        if (isLicensed.ToUpperInvariant().Equals("TRUE") && !isLicense_Enforced)
                        {
                            content = content + Environment.NewLine + "Assembly " + dllName + " was compiled without license enforcement.";                           
                        }

                        else if (!isLicensed.ToUpperInvariant().Equals("TRUE") && isLicense_Enforced)
                        {
                            content = content + Environment.NewLine + "Assembly " + dllName + " was compiled with license enforcement.";                          
                        }
                    }
                }

                if (Path.GetExtension(filePath).ToLower().Equals(".dll") && !dllName.ToLower().Contains("xmlserializers"))
                {
                    if (isLicensed.ToUpperInvariant().Equals("TRUE") && !isLicense_Enforced)
                    {
                        if (!FileVersionInfo.GetVersionInfo(filePath).FileDescription.ToUpperInvariant().Contains("(LR)") && !FileVersionInfo.GetVersionInfo(filePath).ProductName.ToUpperInvariant().Contains("(LR)"))
                            content = content + Environment.NewLine + "(LR) not exist in File Description or Product Name" + Environment.NewLine;
                    }

                    else if (!isLicensed.ToUpperInvariant().Equals("TRUE") && isLicense_Enforced)
                    {
                        if (FileVersionInfo.GetVersionInfo(filePath).FileDescription.ToUpperInvariant().Contains("(LR)") || FileVersionInfo.GetVersionInfo(filePath).ProductName.ToUpperInvariant().Contains("(LR)"))
                            content = content + Environment.NewLine + "(LR) exist in File Description or Product Name" + Environment.NewLine;
                    }
                }

                if (!string.IsNullOrEmpty(content))
                    File.AppendAllText(outReport, Environment.NewLine + filePath + content);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Strong name validation failed"))
                {
                    if (filePath.Contains("MVC"))
                        content = "Strong name validation failed... " + studioVersionMVC;
                    else
                        content = "Strong name validation failed... " + studioVersion;
                }
                else
                {
                    if (filePath.Contains("MVC"))
                        content = "Error in " + studioVersionMVC +  " version ==>" + "\r\n" + ex.ToString() + "Completed ==>\r\n";
                    else
                        content = "Error in " + studioVersion + " version ==>" + "\r\n" + ex.ToString() + "Completed ==>\r\n";

                }
                File.AppendAllText(outReport, Environment.NewLine + filePath + Environment.NewLine + content);

            }
        }


        /// <summary>
        /// Check whether assembly name exist in licensing exclude file
        /// </summary>
        /// <param name="excludeFilePath">Exclude List file</param>
        /// <param name="assemblyName">Current Processing assembly name</param>
        /// <param name="outputPath">Report file path</param>
        /// <returns>true or false</returns>
        private static bool isInLicenseExcludeList(string excludeFilePath, string assemblyName, string outputPath)
        {            
            // Read Licensing assembly exclude list
            try
            {
                foreach (string excludeAssembly in File.ReadAllText(excludeFilePath).Split(','))
                {
                    if (!string.IsNullOrEmpty(excludeAssembly))
                    {
                        if (assemblyName.ToUpperInvariant().Contains(excludeAssembly.ToUpperInvariant()))
                            return true;
                    }
                }                
            }
            catch (Exception ex)
            {
                content =  Environment.NewLine + ex.Message;
                Console.WriteLine(ex.Message);
                File.AppendAllText(outputPath, Environment.NewLine + ex.Message);
                return false;
            }
            return false;
        }
    }
}
