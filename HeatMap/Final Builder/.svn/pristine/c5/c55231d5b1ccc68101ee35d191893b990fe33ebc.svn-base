#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace InstallVerifier   
{
    class Program
    {
        public static string nugetExePath = string.Empty;        
        public static string INIPath = string.Empty;
        static void Main(string[] args)
        {
            InstallChecker ins = new InstallChecker();
            bool returnVal = true;
            string xmlPath = string.Empty;            
            string assemblyVersion = string.Empty;
            string reportPath = string.Empty;
			string currentplatform = string.Empty;
            string platformtype;
            string EJ2Path = string.Empty;
            string isLicensed = string.Empty;

            if (args.Length == 6 || args.Length == 7)
            {
                xmlPath = @args[0];
                INIPath = @args[1];
                assemblyVersion = @args[2];
                reportPath = @args[3];
				currentplatform = @args[4];
                isLicensed = @args[5];
                
                if (args.Length == 7)
                {
                    EJ2Path = @args[6];
                }
            }

            string processName = INIPath + @"\Build\Final Builder\CustomAction\C#\InstallVerifier\AssemblyReference\bin\Debug\AssemblyReference.exe";
            nugetExePath = INIPath + @"\Build\Final Builder\Binaries\nuget.exe";            
            string outputPath = INIPath + @"\ErrorReport.txt";
            string xamOutputPath = INIPath + @"\XamErrorReport.txt";
            string ej2OutputPath = INIPath + @"\EJ2ErrorReport.txt";

            string assemblyFilesPath, filePath, authenticode, sign, assemblyName, version, studioVersion2_0, studioVersion3_5, studioVersion4_0, studioVersion4_5, studioVersion4_5_1, studioVersion4_0MVC, studioVersion4_0SL, studioVersion4_0WP7, studioVersion4_5MVC, studioVersion4_5_1MVC, studioVersion4_5WinRT, studioVersion4_5_1WinRT, studioVersion4_5WP8, studioVersion = "", studioVersion4_6, studioVersion4_6WinRT;
            XmlDocument xdoc = new XmlDocument();
            filePath = string.Empty;
            try
            {
                xdoc.Load(xmlPath);
                string Major, Build, Minor, Revision;
                ins.StudioVersionSplitter(assemblyVersion, out Major, out Minor, out Build, out Revision);
                studioVersion2_0 = Major + "." + Minor + "200" + "." + Build + "." + Revision;
                studioVersion3_5 = Major + "." + Minor + "350" + "." + Build + "." + Revision;
                studioVersion4_0 = Major + "." + Minor + "400" + "." + Build + "." + Revision;
                studioVersion4_5 = Major + "." + Minor + "450" + "." + Build + "." + Revision;
                studioVersion4_5_1 = Major + "." + Minor + "451" + "." + Build + "." + Revision;
                studioVersion4_6 = Major + "." + Minor + "460" + "." + Build + "." + Revision;


                if (xmlPath.Contains(@"\HelperFiles\VerifierFile.xml"))
                {
                    if (File.Exists(outputPath))
                        File.Delete(outputPath);
                    #region AssembliesVersionMatcher
                    assemblyFilesPath = INIPath + xdoc.SelectSingleNode("Syncfusion/Assemblies").Attributes["Path"].Value;
                    XmlNodeList xnodeList = xdoc.SelectNodes("Syncfusion/Assemblies/Assembly");
                    foreach (XmlNode xnode in xnodeList)
                    {
                        platformtype = xnode.Attributes["PlatformType"].Value;
                        if (platformtype.ToLower().Contains(currentplatform.ToLower()))
                        {
                            InstallChecker.report.Append(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                            assemblyName = xnode.Attributes["Name"].Value + ".dll";
                            string assemblyPath = string.Empty;
                            assemblyName = xnode.Attributes["Name"].Value + ".dll";
                            assemblyPath = xnode.Attributes["Path"].Value;
                            authenticode = xnode.Attributes["Authenticode"].Value;
                            sign = xnode.Attributes["Sign"].Value;
                            string requiredVersion = string.Empty;

                            if (assemblyName.ToUpperInvariant().Equals("SYNCFUSION.LICENSING.DLL"))
                            {
                                List<string> filePaths = new List<string>();
                                string licensingAssemblyVersion = assemblyVersion;

                                if (!platformtype.ToUpperInvariant().Contains("DESKTOP"))
                                {
                                    filePaths.Add(@assemblyFilesPath + @"\UWPAssemblies" + assemblyPath + @"\" + @assemblyName);
                                }

                                filePaths.Add(@assemblyFilesPath + @"\netstandard1.2" + assemblyPath + @"\" + @assemblyName);
                                filePaths.Add(@assemblyFilesPath + @"\netstandard1.4" + assemblyPath + @"\" + @assemblyName);
                                filePaths.Add(@assemblyFilesPath + @"\netstandard2.0" + assemblyPath + @"\" + @assemblyName);

                                foreach (string assmPath in filePaths)
                                {
                                    if (ins.CheckFileExistence(@assmPath))
                                    {
                                        if (assmPath.Contains("UWPAssemblies"))
                                        {
                                            licensingAssemblyVersion = studioVersion4_6;
                                        }
                                        else
                                        {
                                            licensingAssemblyVersion = assemblyVersion;
                                        }

                                        InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @assmPath);
                                        Console.WriteLine("File in Process  : " + @assmPath);
                                        ins.AuthenticodeVerifier(assmPath, authenticode, sign, outputPath);
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " +
                                                                         licensingAssemblyVersion);
                                        Console.WriteLine("Assembly version required : " + licensingAssemblyVersion);
                                        if (ins.CompareAssemblyVersion(licensingAssemblyVersion, @assmPath, out version))
                                        {
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                            Console.WriteLine("Assembly version found : " + version);
                                            //ins.CheckReferencedAssemblies(processName, assemblyVersion, assmPath, outputPath, isLicensed);

                                            if (ins.IsAssemblyDebugMode(assmPath, outputPath))
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + assmPath + " was compiled in debug mode");
                                                InstallChecker.error.Append(Environment.NewLine + assmPath + " was compiled in debug mode");
                                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + assmPath + " was compiled in debug mode.");
                                                Console.WriteLine(assmPath + " was compiled in debug mode");
                                            }

                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath,
                                                               Environment.NewLine + assmPath + Environment.NewLine +
                                                               "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                               licensingAssemblyVersion);
                                            InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + assmPath);
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                         Environment.NewLine + "Error ==> " +
                                                                         "Result : Assembly versions mismatch");
                                            Console.WriteLine("Assembly version found : " + version);
                                            Console.WriteLine("Result : Assembly versions mismatch");

                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + assmPath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + assmPath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + assmPath + " not found.");
                                        Console.WriteLine(assmPath + " not found.");

                                    }
                                }
                            }

                            if (currentplatform != "UWP")
                            {
                                if (xnode.Attributes["v2_0"].Value.ToUpper() == "TRUE")
                                {
                                    filePath = @assemblyFilesPath + @"\2.0" + assemblyPath + @"\" + @assemblyName;
                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                        Console.WriteLine("File in Process  : " + @filePath);
                                        ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + studioVersion2_0);
                                        Console.WriteLine("Assembly version required : " + studioVersion2_0);
                                        if (ins.CompareAssemblyVersion(studioVersion2_0, @filePath, out version))
                                        {
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                            Console.WriteLine("Assembly version found : " + version);
                                            ins.CheckReferencedAssemblies(processName, studioVersion2_0, filePath, outputPath, isLicensed);

                                            if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                Console.WriteLine(filePath + " was compiled in debug mode");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion2_0);
                                            InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch");
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                            Console.WriteLine("Assembly version found : " + version);
                                            Console.WriteLine("Result : Assembly versions mismatch");
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");
                                    }
                                }

                                if (xnode.Attributes["v3_5"].Value.ToUpper() == "TRUE")
                                {
                                    filePath = @assemblyFilesPath + @"\3.5" + assemblyPath + @"\" + @assemblyName;
                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                        Console.WriteLine("File in Process  : " + @filePath);
                                        ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);

                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + studioVersion3_5);
                                        Console.WriteLine("Assembly version required : " + studioVersion3_5);
                                        if (ins.CompareAssemblyVersion(studioVersion3_5, @filePath, out version))
                                        {
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                            Console.WriteLine("Assembly version found : " + version);
                                            ins.CheckReferencedAssemblies(processName, studioVersion3_5, filePath, outputPath, isLicensed);

                                            if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                Console.WriteLine(filePath + " was compiled in debug mode");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion3_5);
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " Assembly version mismatch");
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                            Console.WriteLine("Assembly version found : " + version);
                                            Console.WriteLine("Result : Assembly versions mismatch");
                                            returnVal = false;
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");
                                        returnVal = false;
                                    }
                                }

                                if (xnode.Attributes["v4_0"].Value.ToUpper() == "TRUE")
                                {
                                    if (assemblyName.ToLower().Contains("aspnet.core"))
                                    {
                                        assemblyName = assemblyName.Replace("AspNet.Core", "Mvc");
                                    }
                                    if (assemblyName.ToLower().Contains("mvc"))
                                    {
                                        //MVC assemblies check

                                        filePath = @assemblyFilesPath + @"\4.0\MVC3Assemblies" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                            Console.WriteLine("File in Process  : " + @filePath);
                                            ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                            studioVersion4_0MVC = Major + "." + Minor + "300" + "." + Build + "." + Revision;
                                            InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + studioVersion4_0MVC);
                                            Console.WriteLine("Assembly version required : " + studioVersion4_0MVC);
                                            if (ins.CompareAssemblyVersion(studioVersion4_0MVC, @filePath, out version))
                                            {
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                                Console.WriteLine("Assembly version found : " + version);
                                                ins.CheckReferencedAssemblies(processName, studioVersion4_0MVC, filePath, outputPath, isLicensed);

                                                if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                                {
                                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                    Console.WriteLine(filePath + " was compiled in debug mode");
                                                }
                                            }
                                            else
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion4_0MVC);
                                                InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                                Console.WriteLine("Assembly version found : " + version);
                                                Console.WriteLine("Result : Assembly versions mismatch");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                        }

                                        //MVC 4 assemblies check

                                        filePath = @assemblyFilesPath + @"\4.0\MVC4Assemblies" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                            Console.WriteLine("File in Process  : " + @filePath);
                                            ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                            studioVersion4_0MVC = Major + "." + Minor + "400" + "." + Build + "." + Revision;
                                            InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + studioVersion4_0MVC);
                                            Console.WriteLine("Assembly version required : " + studioVersion4_0MVC);
                                            if (ins.CompareAssemblyVersion(studioVersion4_0MVC, @filePath, out version))
                                            {
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                                Console.WriteLine("Assembly version found : " + version);
                                                ins.CheckReferencedAssemblies(processName, studioVersion4_0MVC, filePath, outputPath, isLicensed);

                                                if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                                {
                                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                    Console.WriteLine(filePath + " was compiled in debug mode");
                                                }
                                            }
                                            else
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion4_0MVC);
                                                InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                                Console.WriteLine("Assembly version found : " + version);
                                                Console.WriteLine("Result : Assembly versions mismatch");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                        }
                                    }
                                    else
                                    {
                                        filePath = @assemblyFilesPath + @"\4.0" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                            Console.WriteLine("File in Process  : " + @filePath);
                                            ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                            InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + studioVersion4_0);
                                            Console.WriteLine("Assembly version required : " + studioVersion4_0);
                                            if (ins.CompareAssemblyVersion(studioVersion4_0, @filePath, out version))
                                            {
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                                Console.WriteLine("Assembly version found : " + version);
                                                ins.CheckReferencedAssemblies(processName, studioVersion4_0, filePath, outputPath, isLicensed);

                                                if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                                {
                                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                    Console.WriteLine(filePath + " was compiled in debug mode");
                                                }
                                            }
                                            else
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion4_0);
                                                InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                                Console.WriteLine("Assembly version found : " + version);
                                                Console.WriteLine("Result : Assembly versions mismatch");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                        }
                                    }


                                }
                                if (xnode.Attributes["v4_5"].Value.ToUpper() == "TRUE")
                                {
                                    if (assemblyName.ToLower().Contains("mvc"))
                                    {
                                        //MVC assemblies check

                                        filePath = @assemblyFilesPath + @"\4.5\MVC5Assemblies" + assemblyPath + @"\" +
                                                   @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                            Console.WriteLine("File in Process  : " + @filePath);
                                            ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                            studioVersion4_5MVC = Major + "." + Minor + "500" + "." + Build + "." + Revision;
                                            InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " +
                                                                         studioVersion4_5MVC);
                                            Console.WriteLine("Assembly version required : " + studioVersion4_5MVC);
                                            if (ins.CompareAssemblyVersion(studioVersion4_5MVC, @filePath, out version))
                                            {
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                                Console.WriteLine("Assembly version found : " + version);
                                                ins.CheckReferencedAssemblies(processName, studioVersion4_5MVC, filePath, outputPath, isLicensed);

                                                if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                                {
                                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                    Console.WriteLine(filePath + " was compiled in debug mode");
                                                }

                                            }
                                            else
                                            {
                                                File.AppendAllText(outputPath,
                                                                   Environment.NewLine + filePath + Environment.NewLine +
                                                                   "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                                   studioVersion4_5MVC);
                                                InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                             Environment.NewLine + "Error ==> " +
                                                                             "Result : Assembly versions mismatch");
                                                Console.WriteLine("Assembly version found : " + version);
                                                Console.WriteLine("Result : Assembly versions mismatch");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                        }

                                    }

                                    else
                                    {
                                        filePath = @assemblyFilesPath + @"\4.5" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                            Console.WriteLine("File in Process  : " + @filePath);
                                            ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                            InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " +
                                                                             studioVersion4_5);
                                            Console.WriteLine("Assembly version required : " + studioVersion4_5);
                                            if (ins.CompareAssemblyVersion(studioVersion4_5, @filePath, out version))
                                            {
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " +
                                                                             version);
                                                Console.WriteLine("Assembly version found : " + version);
                                                ins.CheckReferencedAssemblies(processName, studioVersion4_5, filePath, outputPath, isLicensed);

                                                if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                                {
                                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                    Console.WriteLine(filePath + " was compiled in debug mode");
                                                }
                                            }
                                            else
                                            {
                                                File.AppendAllText(outputPath,
                                                                   Environment.NewLine + filePath + Environment.NewLine +
                                                                   "Assembly version mismatch...\n\r Found: " + version +
                                                                   "\tRequired: " + studioVersion4_5);
                                                InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" +
                                                                            filePath);
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " +
                                                                             version + Environment.NewLine + "Error ==> " +
                                                                             "Result : Assembly versions mismatch");
                                                Console.WriteLine("Assembly version found : " + version);
                                                Console.WriteLine("Result : Assembly versions mismatch");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                        }
                                    }

                                }
                                if (xnode.Attributes["v4_5_1"].Value.ToUpper() == "TRUE")
                                {
                                    if (@assemblyName.ToLower().Contains("portable"))
                                    {
                                        string assembly = xnode.Attributes["Name"].Value.Substring(11);

                                        string portableNugetVersion = Major + "." + Minor + "120" + "." + Build + "." + Revision;


                                        // For Portable Assembly
                                        filePath = @assemblyFilesPath + @"\netstandard1.2" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                            Console.WriteLine("File in Process  : " + @filePath);
                                            ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);

                                            InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + portableNugetVersion);
                                            Console.WriteLine("Assembly version required : " + portableNugetVersion);
                                            if (ins.CompareAssemblyVersion(portableNugetVersion, @filePath, out version))
                                            {
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                                Console.WriteLine("Assembly version found : " + version);
                                                ins.CheckReferencedAssemblies(processName, portableNugetVersion, filePath, outputPath, isLicensed);

                                                if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                                {
                                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                    Console.WriteLine(filePath + " was compiled in debug mode");
                                                }

                                            }
                                            else
                                            {
                                                File.AppendAllText(outputPath,
                                                                   Environment.NewLine + filePath + Environment.NewLine +
                                                                   "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                                   portableNugetVersion);
                                                InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                             Environment.NewLine + "Error ==> " +
                                                                             "Result : Assembly versions mismatch");
                                                Console.WriteLine("Assembly version found : " + version);
                                                Console.WriteLine("Result : Assembly versions mismatch");

                                            }

                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");

                                        }


                                    }

                                    else
                                    {

                                        filePath = @assemblyFilesPath + @"\4.5.1" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                            Console.WriteLine("File in Process  : " + @filePath);
                                            ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                            InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " +
                                                                             studioVersion4_5_1);
                                            Console.WriteLine("Assembly version required : " + studioVersion4_5_1);
                                            if (ins.CompareAssemblyVersion(studioVersion4_5_1, @filePath, out version))
                                            {
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                                Console.WriteLine("Assembly version found : " + version);
                                                ins.CheckReferencedAssemblies(processName, studioVersion4_5_1, filePath, outputPath, isLicensed);

                                                if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                                {
                                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                    Console.WriteLine(filePath + " was compiled in debug mode");
                                                }

                                            }
                                            else
                                            {
                                                File.AppendAllText(outputPath,
                                                                   Environment.NewLine + filePath + Environment.NewLine +
                                                                   "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                                   studioVersion4_5_1);
                                                InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                                InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                             Environment.NewLine + "Error ==> " +
                                                                             "Result : Assembly versions mismatch");
                                                Console.WriteLine("Assembly version found : " + version);
                                                Console.WriteLine("Result : Assembly versions mismatch");

                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");

                                        }
                                    }
                                }
                            }

                            if (xnode.Attributes["v4_6"].Value.ToUpper() == "TRUE")
                            {
                                if (assemblyName.ToLower().Contains("aspnet.core"))
                                {
                                    assemblyName = assemblyName.Replace("AspNet.Core", "Mvc");
                                }
                                if (@assemblyName.ToLower().Contains("mvc") || @assemblyName.ToLower().Contains("ej.export") || @assemblyName.ToLower().Contains("ej.pivot") || @assemblyName.ToLower().Contains("syncfusion.licensing"))
                                {
                                    //MVC6 nuget packages check
                                    string assembly = xnode.Attributes["Name"].Value.Substring(11);

                                    string nugetversion = Major + "." + Minor + "." + Build + "." + Revision;

                                    if (@assembly.ToLower().Contains("aspnet.core"))
                                    {
                                        filePath = @INIPath + @"\EJ.MVC\" + assembly.Replace("AspNet.Core", "MVC") + @"\bin\" + xnode.Attributes["Name"].Value + @"\" + nugetversion;
                                    }
                                    else if (@assemblyName.ToLower().Contains("ej.export") || @assemblyName.ToLower().Contains("ej.pivot"))
                                    {
                                        filePath = @INIPath + @"\Base\" + assembly + @"\bin\" + xnode.Attributes["Name"].Value + @"\" + nugetversion;
                                    }
                                    else if (currentplatform.ToLower().Equals("web") && @assemblyName.ToLower().Contains("syncfusion.licensing"))
                                    {
                                        filePath = @assemblyFilesPath + @"\NuGetPackages\" + Path.GetFileNameWithoutExtension(assemblyName) + "\\" + assemblyVersion;
                                    }
                                    else if (!@assemblyName.ToLower().Contains("syncfusion.licensing"))
                                    {
                                        filePath = @INIPath + @"\EJ.MVC\" + assembly + @"\bin\" + xnode.Attributes["Name"].Value + @"\" + nugetversion;
                                    }

                                    FileAttributes fileAttributes = File.GetAttributes(@filePath);

                                    if (fileAttributes.HasFlag(FileAttributes.Directory))
                                    {
                                        if (!Directory.Exists(@filePath))
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                        }
                                    }
                                }
                                else if (@assemblyName.ToLower().Contains("portable"))
                                {
                                    string assembly = xnode.Attributes["Name"].Value.Substring(11);

                                    string portableNugetVersion = Major + "." + Minor + "140" + "." + Build + "." + Revision;


                                    // For Portable Assembly
                                    filePath = @assemblyFilesPath + @"\netstandard1.4" + assemblyPath + @"\" + @assemblyName;
                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                        Console.WriteLine("File in Process  : " + @filePath);
                                        ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);

                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + portableNugetVersion);
                                        Console.WriteLine("Assembly version required : " + portableNugetVersion);
                                        if (ins.CompareAssemblyVersion(portableNugetVersion, @filePath, out version))
                                        {
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                            Console.WriteLine("Assembly version found : " + version);
                                            ins.CheckReferencedAssemblies(processName, portableNugetVersion, filePath, outputPath, isLicensed);

                                            if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                Console.WriteLine(filePath + " was compiled in debug mode");
                                            }

                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath,
                                                               Environment.NewLine + filePath + Environment.NewLine +
                                                               "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                               portableNugetVersion);
                                            InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                         Environment.NewLine + "Error ==> " +
                                                                         "Result : Assembly versions mismatch");
                                            Console.WriteLine("Assembly version found : " + version);
                                            Console.WriteLine("Result : Assembly versions mismatch");

                                        }

                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");

                                    }

                                    string portableNugetVersion20 = Major + "." + Minor + "200" + "." + Build + "." + Revision;


                                    // For Portable Assembly
                                    filePath = @assemblyFilesPath + @"\netstandard2.0" + assemblyPath + @"\" + @assemblyName;
                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                        Console.WriteLine("File in Process  : " + @filePath);
                                        ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);

                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " + portableNugetVersion20);
                                        Console.WriteLine("Assembly version required : " + portableNugetVersion20);
                                        if (ins.CompareAssemblyVersion(portableNugetVersion20, @filePath, out version))
                                        {
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                            Console.WriteLine("Assembly version found : " + version);
                                            ins.CheckReferencedAssemblies(processName, portableNugetVersion20, filePath, outputPath, isLicensed);

                                            if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                Console.WriteLine(filePath + " was compiled in debug mode");
                                            }

                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath,
                                                               Environment.NewLine + filePath + Environment.NewLine +
                                                               "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                               portableNugetVersion);
                                            InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                         Environment.NewLine + "Error ==> " +
                                                                         "Result : Assembly versions mismatch");
                                            Console.WriteLine("Assembly version found : " + version);
                                            Console.WriteLine("Result : Assembly versions mismatch");

                                        }

                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");

                                    }

                                    if (currentplatform == "Web")
                                    {
                                        //Portable nuget packages check                                
                                        filePath = @INIPath + xdoc.SelectSingleNode("Syncfusion/EJ2CorePackages").Attributes["Path"].Value + "\\" + xnode.Attributes["Name"].Value.Replace(".Portable", ".NETStandard") + @"\" + assemblyVersion;

                                        if (!Directory.Exists(@filePath) && !filePath.ToLower().Contains("calculate.netstandard"))
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                        }
                                    }
                                }

                                else
                                {
                                    if (currentplatform.Equals("UWP") && assemblyName.ToLower().Contains("uwp") && !assemblyName.ToLower().Contains("uwp.basewrapper") && !assemblyName.ToLower().Contains("syncfusion.licensing"))
                                    {
                                        filePath = @assemblyFilesPath + @"\UWPAssemblies" + assemblyPath + @"\" + @assemblyName;
                                    }
                                    else
                                    {
                                        filePath = @assemblyFilesPath + @"\4.6" + assemblyPath + @"\" + @assemblyName;
                                    }
                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                        Console.WriteLine("File in Process  : " + @filePath);
                                        ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " +
                                                                         studioVersion4_6);
                                        Console.WriteLine("Assembly version required : " + studioVersion4_6);

                                        if (ins.CompareAssemblyVersion(studioVersion4_6, @filePath, out version))
                                        {
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                            Console.WriteLine("Assembly version found : " + version);
                                            ins.CheckReferencedAssemblies(processName, studioVersion4_6, filePath, outputPath, isLicensed);

                                            if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                            {
                                                File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                                Console.WriteLine(filePath + " was compiled in debug mode");
                                            }
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath,
                                                               Environment.NewLine + filePath + Environment.NewLine +
                                                               "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                               studioVersion4_6);
                                            InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                         Environment.NewLine + "Error ==> " +
                                                                         "Result : Assembly versions mismatch");
                                            Console.WriteLine("Assembly version found : " + version);
                                            Console.WriteLine("Result : Assembly versions mismatch");
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");

                                    }
                                }

                            }
                            if (currentplatform != "UWP")
                            {
                                if (xnode.Attributes["XMLDocumentation2_0"].Value.ToUpper() == "TRUE")
                                {
                                    assemblyName = xnode.Attributes["Name"].Value + ".xml";
                                    filePath = @assemblyFilesPath + @"\2.0" + assemblyPath + @"\" + @assemblyName;
                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        Console.WriteLine("File found :" + filePath);
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");
                                        returnVal = false;
                                    }
                                }

                                if (xnode.Attributes["XMLDocumentation3_5"].Value.ToUpper() == "TRUE")
                                {
                                    assemblyName = xnode.Attributes["Name"].Value + ".xml";
                                    filePath = @assemblyFilesPath + @"\3.5" + assemblyPath + @"\" + @assemblyName;
                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        Console.WriteLine("File found :" + filePath);
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");
                                        returnVal = false;
                                    }
                                }

                                if (xnode.Attributes["XMLDocumentation4_0"].Value.ToUpper() == "TRUE")
                                {
                                    assemblyName = xnode.Attributes["Name"].Value + ".xml";
                                    if (assemblyName.ToLower().Contains("aspnet.core"))
                                    {
                                        assemblyName = assemblyName.Replace("AspNet.Core", "Mvc");
                                    }
                                    if (assemblyName.Contains("Mvc") || assemblyName.Contains("MVC"))
                                    {
                                        assemblyName = xnode.Attributes["Name"].Value + "3.xml";
                                        if (assemblyName.ToLower().Contains("aspnet.core"))
                                        {
                                            assemblyName = assemblyName.Replace("AspNet.Core", "Mvc");
                                        }
                                        filePath = @assemblyFilesPath + @"\4.0\MVC3Assemblies" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            Console.WriteLine("File found :" + filePath);
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                            returnVal = false;
                                        }

                                        assemblyName = xnode.Attributes["Name"].Value + "4.xml";
                                        if (assemblyName.ToLower().Contains("aspnet.core"))
                                        {
                                            assemblyName = assemblyName.Replace("AspNet.Core", "Mvc");
                                        }
                                        filePath = @assemblyFilesPath + @"\4.0\MVC4Assemblies" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            Console.WriteLine("File found :" + filePath);
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                            returnVal = false;
                                        }
                                    }
                                    else
                                    {
                                        filePath = @assemblyFilesPath + @"\4.0" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            Console.WriteLine("File found :" + filePath);
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                            returnVal = false;
                                        }
                                    }
                                }

                                if (xnode.Attributes["XMLDocumentation4_5"].Value.ToUpper() == "TRUE")
                                {
                                    assemblyName = xnode.Attributes["Name"].Value + ".xml";
                                    if (assemblyName.ToLower().Contains("aspnet.core"))
                                    {
                                        assemblyName = assemblyName.Replace("AspNet.Core", "Mvc");
                                    }
                                    if (assemblyName.Contains("Mvc") || assemblyName.Contains("MVC"))
                                    {
                                        assemblyName = xnode.Attributes["Name"].Value + "5.xml";
                                        if (assemblyName.ToLower().Contains("aspnet.core"))
                                        {
                                            assemblyName = assemblyName.Replace("AspNet.Core", "Mvc");
                                        }
                                        filePath = @assemblyFilesPath + @"\4.5\MVC5Assemblies" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            Console.WriteLine("File found :" + filePath);
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                            returnVal = false;
                                        }
                                    }
                                    else
                                    {
                                        filePath = @assemblyFilesPath + @"\4.5" + assemblyPath + @"\" + @assemblyName;
                                        if (ins.CheckFileExistence(@filePath))
                                        {
                                            Console.WriteLine("File found :" + filePath);
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                            Console.WriteLine(filePath + " not found.");
                                            returnVal = false;
                                        }
                                    }
                                }

                                if (xnode.Attributes["XMLDocumentation4_5_1"].Value.ToUpper() == "TRUE")
                                {
                                    assemblyName = xnode.Attributes["Name"].Value + ".xml";

                                    if (assemblyName.ToLower().Contains("portable"))
                                    {
                                        filePath = @assemblyFilesPath + @"\netstandard1.2" + assemblyPath + @"\" + @assemblyName;
                                    }
                                    else
                                        filePath = @assemblyFilesPath + @"\4.5.1" + assemblyPath + @"\" + @assemblyName;

                                    if (ins.CheckFileExistence(@filePath))
                                    {
                                        Console.WriteLine("File found :" + filePath);

                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                        Console.WriteLine(filePath + " not found.");
                                        returnVal = false;

                                    }

                                }
                            }

                            if (xnode.Attributes["XMLDocumentation4_6"].Value.ToUpper() == "TRUE")
                            {
                                assemblyName = xnode.Attributes["Name"].Value + ".xml";
                                string xmlFile20Path = string.Empty;

                                if (assemblyName.ToLower().Contains("portable"))
                                {
                                    filePath = @assemblyFilesPath + @"\netstandard1.4" + assemblyPath + @"\" + @assemblyName;
                                    xmlFile20Path = @assemblyFilesPath + @"\netstandard2.0" + assemblyPath + @"\" + @assemblyName;
                                }
                                else if (currentplatform.Equals("UWP") && assemblyName.ToLower().Contains("uwp") && !assemblyName.ToLower().Contains("uwp.basewrapper"))
                                {
                                    filePath = @assemblyFilesPath + @"\UWPAssemblies" + assemblyPath + @"\" + @assemblyName;
                                }
                                else
                                {
                                    filePath = @assemblyFilesPath + @"\4.6" + assemblyPath + @"\" + @assemblyName;
                                }
                                if (ins.CheckFileExistence(@filePath))
                                {
                                    Console.WriteLine("File found :" + filePath);
                                }
                                else
                                {
                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                    InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                    Console.WriteLine(filePath + " not found.");
                                    returnVal = false;

                                }

                                if (assemblyName.ToLower().Contains("portable"))
                                {
                                    if (ins.CheckFileExistence(xmlFile20Path))
                                    {
                                        Console.WriteLine("File found :" + xmlFile20Path);
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath, Environment.NewLine + xmlFile20Path + " not found");
                                        InstallChecker.error.Append(Environment.NewLine + xmlFile20Path + " not found");
                                        InstallChecker.report.Append(Environment.NewLine + "Error ==> " + xmlFile20Path + " not found.");
                                        Console.WriteLine(xmlFile20Path + " not found.");
                                        returnVal = false;

                                    }
                                }
                            }


                            InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                        }
                    }

                    #endregion

                    #region ReferenceAssemblies

                    string utilitiesPath, manifest, assemblyRef, assemblyRefVersion, utilitytype;
                    //utilitiesPath = INIPath;
                    XmlNodeList utilitiesNodeList = xdoc.SelectNodes("Syncfusion/Utilities/Utility");
                    foreach (XmlNode utilityNode in utilitiesNodeList)
                    {
                        utilitytype = utilityNode.Attributes["PlatformType"].Value;
                        if (utilitytype.ToLower().Contains(currentplatform.ToLower()))
                        {
                            utilitiesPath = INIPath + utilityNode.Attributes["Path"].Value;
                            XmlNodeList applicationsNodeList = utilityNode.SelectNodes("Applications/Application");
                            foreach (XmlNode applicationNode in applicationsNodeList)
                            {
                                InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                                filePath = Path.Combine(utilitiesPath, applicationNode.Attributes["Name"].Value);
                                manifest = applicationNode.Attributes["Manifest"].Value;
                                assemblyRef = applicationNode.Attributes["AssemblyReference"].Value;
                                authenticode = applicationNode.Attributes["Authenticode"].Value;
                                sign = applicationNode.Attributes["Sign"].Value;
                                if (ins.CheckFileExistence(filePath))
                                {
                                    InstallChecker.report.Append(Environment.NewLine + "Utility in process : " + filePath);
                                    Console.WriteLine("Utility in process : " + filePath);
                                    ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);

                                    if (manifest.ToLower() != "false")
                                    {
                                        if (!ins.CheckFileExistence(Path.Combine(Path.GetDirectoryName(filePath), manifest)))
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + "Manifest file " + manifest + " not found");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> Result : Manifest file not found");
                                            Console.WriteLine("Result : Manifest file not found");
                                            returnVal = false;
                                            //Console.WriteLine("Manifest file not found");
                                        }
                                        else
                                        {
                                            InstallChecker.report.Append(Environment.NewLine + "Manifest file " + Path.Combine(Path.GetDirectoryName(filePath), manifest) + " found.");
                                            Console.WriteLine("Manifest file " + Path.Combine(Path.GetDirectoryName(filePath), manifest) + " found.");
                                        }
                                    }
                                    if (assemblyRef.ToLower() == "true")
                                    {
                                        assemblyRefVersion = applicationNode.Attributes["Version"].Value;
                                        if (assemblyRefVersion == "3.5")
                                            studioVersion = studioVersion3_5;
                                        else if (assemblyRefVersion == "2.0")
                                            studioVersion = studioVersion2_0;
                                        else if (assemblyRefVersion == "4.0")
                                            studioVersion = studioVersion4_0;
                                        else if (assemblyRefVersion == "4.5")
                                            studioVersion = studioVersion4_5;
                                        else if (assemblyRefVersion == "4.5.1")
                                            studioVersion = studioVersion4_5_1;
                                        else if (assemblyRefVersion == "4.6")
                                            studioVersion = studioVersion4_6;
                                        ins.CheckReferencedAssemblies(processName, studioVersion, filePath, outputPath, isLicensed);
                                    }
                                    if (assemblyRef.ToLower() == "false")
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "Utility found : " + filePath);
                                        Console.WriteLine("Utility found : " + filePath);
                                    }

                                    //Utility product version checking....
                                    if (applicationNode.Attributes["FileVersion"].Value == "True")
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "Utility version required : " + assemblyVersion);
                                        Console.WriteLine("Utility version required : " + assemblyVersion);
                                        if (ins.CompareAssemblyVersion(assemblyVersion, @filePath, out version))
                                        {
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Utility version found : " + version);
                                            Console.WriteLine("Utility version found : " + version);

                                            //if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                            //{
                                            //    File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                            //    InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                            //    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                            //    Console.WriteLine(filePath + " was compiled in debug mode");
                                            //}
                                        }
                                        else
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + Environment.NewLine + "Utility version mismatch...\n\r Found: " + version + "\tRequired: " + assemblyVersion);
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " Utility version mismatch");
                                            InstallChecker.report.AppendLine(Environment.NewLine + "Utility version found : " + version + Environment.NewLine + "Error ==> " + "Result : Utility versions mismatch");
                                            Console.WriteLine("Utility version found : " + version);
                                            Console.WriteLine("Result : Utility versions mismatch");
                                            returnVal = false;
                                        }
                                    }
                                }
                                else
                                {
                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                    InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                    Console.WriteLine("File not found : " + filePath);
                                    returnVal = false;
                                }

                                InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");

                                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                            }
                        }
                    }
                    #endregion

                    #region EJ2 Core Packages
                    if (currentplatform.ToLower().Equals("ej2"))
                    {
                        string packageFilesPath = INIPath + xdoc.SelectSingleNode("Syncfusion/EJ2CorePackages").Attributes["Path"].Value;
                        string packageName = string.Empty;
                        XmlNodeList xnodePackageList = xdoc.SelectNodes("Syncfusion/EJ2CorePackages/Package");
                        foreach (XmlNode xnode in xnodePackageList)
                        {
                            InstallChecker.report.Append(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                            Console.Write("-------------------------------------------------------------------------------------------------------------");
                            packageName = xnode.Attributes["Name"].Value;
                            filePath = packageFilesPath + "\\" + packageName + "." + assemblyVersion + ".nupkg";
                            if (!ins.CheckFileExistence(@filePath))
                            {
                                File.AppendAllText(ej2OutputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine(filePath + " not found.");
                            }
                            else
                            {
                                if (!currentplatform.ToUpperInvariant().Equals("DESKTOP") && !currentplatform.ToUpperInvariant().Equals("UWP"))
                                    ins.AuthenticodeVerifier(filePath, "false", "true", outputPath);
                            }
                        }
                    }
                    InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    #endregion

                    #region EJ2 Source Packages
                    if (currentplatform.ToLower().Equals("ej2"))
                    {
                        string folderPath = EJ2Path + xdoc.SelectSingleNode("Syncfusion/EJ2JS2Packages").Attributes["Path"].Value;
                        string folderName = string.Empty;
                        XmlNodeList xnodeFolderList = xdoc.SelectNodes("Syncfusion/EJ2JS2Packages/Package");
                        foreach (XmlNode xnode in xnodeFolderList)
                        {
                            InstallChecker.report.Append(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                            Console.Write("-------------------------------------------------------------------------------------------------------------");
                            folderName = xnode.Attributes["Name"].Value;
                            filePath = folderPath + "\\" + folderName;
                            if (!ins.CheckFileExistence(@filePath))
                            {
                                File.AppendAllText(ej2OutputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine(filePath + " not found.");
                            }
                        }
                    }
                    #endregion

                    #region VSIXFilesFinder
                    string vsixPath, vsixtype;
                    XmlNodeList vsixNodeList = xdoc.SelectNodes("Syncfusion/VSIXFiles/VSIX");
                    foreach (XmlNode vsixNode in vsixNodeList)
                    {
                        vsixPath = INIPath + vsixNode.Attributes["Path"].Value;
                        vsixtype = vsixNode.Attributes["PlatformType"].Value;
                        if (vsixtype.ToLower().Contains(currentplatform.ToLower()))
                        {
                            InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                            filePath = Path.Combine(vsixPath, vsixNode.Attributes["Name"].Value);
                            if (ins.CheckFileExistence(filePath))
                            {
                                InstallChecker.report.Append(Environment.NewLine + "VSIX File in process : " + filePath);
                                Console.WriteLine("VSIX File in process : " + filePath);
                            }
                            else
                            {
                                File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine("File not found : " + filePath);
                                returnVal = false;
                            }
                            InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");

                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                        }
                    }
                    #endregion

                }
                else if (xmlPath.Contains(@"\HelperFiles\Xamarin_VerifierFile.xml"))
                {
                    if (File.Exists(xamOutputPath))
                        File.Delete(xamOutputPath);
                    //Getting assemblies path from xml document.
                    #region Xamarin Assemblies
                    assemblyFilesPath = INIPath + xdoc.SelectSingleNode("Syncfusion/Assemblies").Attributes["Path"].Value;
                    XmlNodeList xnodeList = xdoc.SelectNodes("Syncfusion/Assemblies/Assembly");
                    foreach (XmlNode xnode in xnodeList)
                    {
                        InstallChecker.report.Append(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.Write("-------------------------------------------------------------------------------------------------------------");
                        assemblyName = xnode.Attributes["Name"].Value + ".dll";
                        authenticode = xnode.Attributes["Authenticode"].Value;
                        sign = xnode.Attributes["Sign"].Value;
                        if (@assemblyName.ToLower().Contains("uwp") && !@assemblyName.ToLower().Contains("xforms.uwp") && !currentplatform.ToLower().Contains("xamarin"))
                        {
                            filePath = INIPath + @"\UWPAssemblies\" + @assemblyName;
                        }
                        else
                        {
                            filePath = @assemblyFilesPath + @"\" + @xnode.Attributes["Path"].Value + @"\" + @assemblyName;
                        }

                        if (xnode.Attributes["v4_5_1"].Value.ToUpper() == "TRUE") //if v4.5.1 is true
                        {
                            if (ins.CheckFileExistence(@filePath))
                            {
                                InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                Console.WriteLine("File in Process  : " + @filePath);
                                ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                if (assemblyName.ToLower().Contains("wp8") && !assemblyName.ToLower().Contains("xforms"))  //if the assembly name contains wp8
                                {
                                    string studioVersion_wp8 = Major + "." + Minor + "800" + "." + Build + "." + Revision;
                                    if (ins.CompareAssemblyVersion(studioVersion_wp8, @filePath, out version))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version);
                                        Console.WriteLine("Assembly version found : " + version);

                                        if (ins.IsAssemblyDebugMode(filePath, xamOutputPath))
                                        {
                                            File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                            Console.WriteLine(filePath + " was compiled in debug mode");
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion_wp8);
                                        InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch");
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                        Console.WriteLine("Assembly version found : " + version);
                                        Console.WriteLine("Result : Assembly versions mismatch");
                                    }
                                }
                                else if (assemblyName.ToLower().Contains("uwp"))  //if the assembly name contains wp8
                                {
                                    string studioVersion_uwp = Major + "." + Minor + "460" + "." + Build + "." + Revision;
                                    if (ins.CompareAssemblyVersion(studioVersion_uwp, @filePath, out version))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version);
                                        Console.WriteLine("Assembly version found : " + version);
                                        if (ins.IsAssemblyDebugMode(filePath, xamOutputPath))
                                        {
                                            File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                            Console.WriteLine(filePath + " was compiled in debug mode");
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion_uwp);
                                        InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch");
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                        Console.WriteLine("Assembly version found : " + version);
                                        Console.WriteLine("Result : Assembly versions mismatch");
                                    }
                                }
                                else if ((assemblyName.ToLower().Contains("wp") && !assemblyName.ToLower().Contains("wp8") && !assemblyName.ToLower().Contains("uwp")) || assemblyName.ToLower().Contains("winrt") && !assemblyName.ToLower().Contains("xforms"))
                                {
                                    string studioVersion_wp8 = Major + "." + Minor + "810" + "." + Build + "." + Revision;
                                    if (ins.CompareAssemblyVersion(studioVersion_wp8, @filePath, out version))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version);
                                        Console.WriteLine("Assembly version found : " + version);

                                        if (ins.IsAssemblyDebugMode(filePath, xamOutputPath))
                                        {
                                            File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                            Console.WriteLine(filePath + " was compiled in debug mode");
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion_wp8);
                                        InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch");
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                        Console.WriteLine("Assembly version found : " + version);
                                        Console.WriteLine("Result : Assembly versions mismatch");
                                    }
                                }
                                else
                                {
                                    if (ins.CompareAssemblyVersion(studioVersion4_5_1, @filePath, out version))
                                    {
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version);
                                        Console.WriteLine("Assembly version found : " + version);

                                        if (ins.IsAssemblyDebugMode(filePath, xamOutputPath))
                                        {
                                            File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                            Console.WriteLine(filePath + " was compiled in debug mode");
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + Environment.NewLine + "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " + studioVersion4_5_1);
                                        InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch");
                                        InstallChecker.report.Append(Environment.NewLine + "Assembly version found : " + version + Environment.NewLine + "Error ==> " + "Result : Assembly versions mismatch");
                                        Console.WriteLine("Assembly version found : " + version);
                                        Console.WriteLine("Result : Assembly versions mismatch");
                                    }
                                }
                            }
                            else
                            {
                                File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine(filePath + " not found.");
                            }
                        }
                        else
                        {
                            if (assemblyName.ToLower().Contains("syncfusion.licensing"))
                            {
                                if (ins.CheckFileExistence(@filePath))
                                {
                                    if (filePath.ToUpperInvariant().Contains("LIB\\UWP"))
                                    {
                                        assemblyVersion = Major + "." + Minor + "460" + "." + Build + "." + Revision;
                                    }

                                    InstallChecker.report.Append(Environment.NewLine + "File in Process  : " + @filePath);
                                    Console.WriteLine("File in Process  : " + @filePath);
                                    ins.AuthenticodeVerifier(filePath, authenticode, sign, outputPath);
                                    InstallChecker.report.Append(Environment.NewLine + "Assembly version required : " +
                                                                     assemblyVersion);
                                    Console.WriteLine("Assembly version required : " + assemblyVersion);
                                    if (ins.CompareAssemblyVersion(assemblyVersion, @filePath, out version))
                                    {
                                        InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version);
                                        Console.WriteLine("Assembly version found : " + version);
                                        ins.CheckReferencedAssemblies(processName, assemblyVersion, filePath, outputPath, isLicensed);

                                        if (ins.IsAssemblyDebugMode(filePath, outputPath))
                                        {
                                            File.AppendAllText(outputPath, Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.error.Append(Environment.NewLine + filePath + " was compiled in debug mode");
                                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " was compiled in debug mode.");
                                            Console.WriteLine(filePath + " was compiled in debug mode");
                                        }
                                    }
                                    else
                                    {
                                        File.AppendAllText(outputPath,
                                                           Environment.NewLine + filePath + Environment.NewLine +
                                                           "Assembly version mismatch...\n\r Found: " + version + "\tRequired: " +
                                                           assemblyVersion);
                                        InstallChecker.error.Append(Environment.NewLine + "Assembly version mismatch" + filePath);
                                        InstallChecker.report.AppendLine(Environment.NewLine + "Assembly version found : " + version +
                                                                     Environment.NewLine + "Error ==> " +
                                                                     "Result : Assembly versions mismatch");
                                        Console.WriteLine("Assembly version found : " + version);
                                        Console.WriteLine("Result : Assembly versions mismatch");
                                    }
                                }
                                else
                                {
                                    File.AppendAllText(outputPath, Environment.NewLine + filePath + " not found");
                                    InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                    InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                    Console.WriteLine(filePath + " not found.");

                                }
                            }
                        }

                        InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                    }
                    #endregion

                    #region Xamarin Packages
                    string packageFilesPath = INIPath + xdoc.SelectSingleNode("Syncfusion/Packages").Attributes["Path"].Value;
                    string packageName = string.Empty;
                    XmlNodeList xnodePackageList = xdoc.SelectNodes("Syncfusion/Packages/Package");
                    assemblyVersion = Major + "." + Minor + "." + Build + "." + Revision;
                    foreach (XmlNode xnode in xnodePackageList)
                    {
                        InstallChecker.report.Append(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.Write("-------------------------------------------------------------------------------------------------------------");
                        packageName = xnode.Attributes["Name"].Value;

                        if (xnode.Attributes["IsForms"].Value.ToUpper() == "TRUE") //if Xforms is true
                        {

                            filePath = packageFilesPath + "\\Syncfusion.Xamarin." + packageName + "." + assemblyVersion + ".nupkg";
                            if (!ins.CheckFileExistence(@filePath))
                            {
                                File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine(filePath + " not found.");
                            }
                            else
                            {
                                ins.AuthenticodeVerifier(filePath, "false", "true", outputPath);
                            }
                        }

                        InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                        if (xnode.Attributes["IsAndroidBinding"].Value.ToUpper() == "TRUE") //if Android Binding is true
                        {
                            filePath = packageFilesPath + "\\Syncfusion.Xamarin." + packageName + ".Android." + assemblyVersion + ".nupkg";
                            if (!ins.CheckFileExistence(@filePath))
                            {
                                File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine(filePath + " not found.");
                            }
                            else
                            {
                                ins.AuthenticodeVerifier(filePath, "false", "true", outputPath);
                            }
                        }

                        InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                        if (xnode.Attributes["IsiOSBinding"].Value.ToUpper() == "TRUE") //if iOS Binding is true
                        {
                            filePath = packageFilesPath + "\\Syncfusion.Xamarin." + packageName + ".IOS." + assemblyVersion + ".nupkg";
                            if (!ins.CheckFileExistence(@filePath))
                            {
                                File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine(filePath + " not found.");
                            }
                            else
                            {
                                ins.AuthenticodeVerifier(filePath, "false", "true", outputPath);
                            }
                        }

                        InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

                        //Check for syncfusion licensing
                        if (xnode.Attributes["IsAndroidBinding"].Value.ToUpper() == "FALSE" && xnode.Attributes["IsForms"].Value.ToUpper() == "FALSE" && xnode.Attributes["IsiOSBinding"].Value.ToUpper() == "FALSE")
                        {
                            filePath = packageFilesPath + "\\" + packageName + "." + assemblyVersion + ".nupkg";
                            if (!ins.CheckFileExistence(@filePath))
                            {
                                File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " not found");
                                InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                                InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                                Console.WriteLine(filePath + " not found.");
                            }
                            else
                            {
                                ins.AuthenticodeVerifier(filePath, "false", "true", outputPath);
                            }
                        }

                        InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                    }

                    // check whether Xamarin Samples packages exist
                    packageFilesPath = INIPath + xdoc.SelectSingleNode("Syncfusion/SamplesPackages").Attributes["Path"].Value;
                    packageName = string.Empty;
                    xnodePackageList = xdoc.SelectNodes("Syncfusion/SamplesPackages/Package");
                    foreach (XmlNode xnode in xnodePackageList)
                    {
                        InstallChecker.report.Append(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                        Console.Write("-------------------------------------------------------------------------------------------------------------");
                        packageName = xnode.Attributes["Name"].Value;
                        filePath = packageFilesPath + "\\" + packageName + "." + assemblyVersion + ".nupkg";
                        if (!ins.CheckFileExistence(@filePath))
                        {
                            File.AppendAllText(xamOutputPath, Environment.NewLine + filePath + " not found");
                            InstallChecker.error.Append(Environment.NewLine + filePath + " not found");
                            InstallChecker.report.Append(Environment.NewLine + "Error ==> " + filePath + " not found.");
                            Console.WriteLine(filePath + " not found.");
                        }
                    }

                    InstallChecker.report.AppendLine(Environment.NewLine + "-------------------------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
                    #endregion
                }
            }
            catch (Exception ex)
            {
                InstallChecker.report.Append(Environment.NewLine + ex.Message);
                Console.WriteLine(ex.Message);
                File.AppendAllText(outputPath, Environment.NewLine + ex.Message);
            }
            //write the verifier report
            if (File.Exists(reportPath))
                File.WriteAllText(reportPath, "");
            File.AppendAllText(reportPath, InstallChecker.report.ToString());
            InstallChecker.report = null;
            InstallChecker.error = null;
        }
    }
}
