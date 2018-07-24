using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Mono.Security.Authenticode;
using System.IO;
using System.Diagnostics;

namespace InstallVerifier
{
    class InstallChecker
    {
        public static StringBuilder report = new StringBuilder();
        public static StringBuilder error = new StringBuilder();
        public bool CheckFileExistence(string filePath)
        {
            return File.Exists(filePath);
        }

        public void CheckReferencedAssemblies(string processName, string studioVersion, string filePath, string outputPath, string isLicensed)
        {
            Process p = new Process();
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = processName;
            ps.Arguments = studioVersion + " " + "\"" + filePath + "\"" + " \"" + outputPath + "\"" + " " + isLicensed + " " + Program.INIPath;
            ps.CreateNoWindow = true;
            ps.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo = ps;
            p.Start();
            p.WaitForExit();
        }
        public bool StudioVersionSplitter(string studioVersion, out string Major, out string Minor, out string Build, out string Revision)
        {
            string[] AssemblyVersionSplitter = studioVersion.Split('.');
            Major = Minor = Build = Revision = "";
            try
            {
                Major = AssemblyVersionSplitter[0];
                Minor = AssemblyVersionSplitter[1];
                Build = AssemblyVersionSplitter[2];
                Revision = AssemblyVersionSplitter[3];
                return true;
            }
            catch (Exception ex)
            {
                InstallChecker.report.AppendLine(Environment.NewLine + ex.StackTrace);
                return false;
            }
        }

        public bool CompareAssemblyVersion(string assemblyVersion, string filePath, out string version)
        {
            FileVersionInfo assVer;
            assVer = FileVersionInfo.GetVersionInfo(filePath);
            version = assVer.ProductVersion.ToString();
            if (version == assemblyVersion)
                return true;
            else
                return false;
        }

        /// <summary>
        ///  check whether assembly was compiled in debug mode or not
        /// </summary>
        /// <param name="assemblyPath">assembly file path</param>
        /// <returns>either true or false</returns>
        public bool IsAssemblyDebugMode(string assemblyPath, string logPath)
        {
            try
            {
                Assembly assemblyInfo = Assembly.LoadFrom(assemblyPath);
                object[] attributesInfo = assemblyInfo.GetCustomAttributes(typeof(DebuggableAttribute), false);

                if (attributesInfo.Length == 0)
                {
                    return false;
                }
                foreach (Attribute attr in attributesInfo)
                {
                    if (attr is DebuggableAttribute)
                    {
                        DebuggableAttribute debugInfo = attr as DebuggableAttribute;
                        if (debugInfo.IsJITOptimizerDisabled == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                //InstallChecker.report.AppendLine(Environment.NewLine + ex.Message);
                //Console.WriteLine(ex.Message);
                //File.AppendAllText(logPath, Environment.NewLine + assemblyPath + ":" + Environment.NewLine + ex.Message);
                return false;
            }
        }

        public bool AuthenticodeVerifier(string dll, string authenticode, string sign, string outputPath)
        {
            try
            {
                AuthenticodeDeformatter a;
                a = new AuthenticodeDeformatter(dll);

                if (dll.ToUpperInvariant().Contains("LIB\\NETSTANDARD1.4") || dll.ToUpperInvariant().Contains("LIB\\NETSTANDARD2.0"))
                {
                    AssemblyName assemblyName = Assembly.LoadFrom(dll).GetName();
                    byte[] key = assemblyName.GetPublicKey();
                    if (key.Length > 0)
                    {
                        InstallChecker.report.AppendLine(Environment.NewLine + "Signed : True");
                        Console.WriteLine("Signed : True");
                        File.AppendAllText(outputPath, Environment.NewLine + dll + Environment.NewLine + "Signed : True");
                    }
                }

                if (dll.ToUpperInvariant().Contains("LICENSING") && (dll.ToUpperInvariant().Contains("NETSTANDARD1.2") || dll.ToUpperInvariant().Contains("NETSTANDARD1.4") || dll.ToUpperInvariant().Contains("NETSTANDARD2.0")))
                {
                    sign = "False";
                    authenticode = "False";
                }

                if (sign.ToLower() == "true")
                {
                    if (Path.GetExtension(dll).ToLower() == ".dll")
                    {
                        AssemblyName assemblyName = Assembly.LoadFrom(dll).GetName();
                        byte[] key = assemblyName.GetPublicKey();
                        if (key.Length > 0)
                        {
                            InstallChecker.report.AppendLine(Environment.NewLine + "Signed : True");
                            Console.WriteLine("Signed : True");
                        }
                        else
                        {
                            InstallChecker.report.AppendLine(Environment.NewLine + "Signed : False");
                            Console.WriteLine("Signed : False");
                            File.AppendAllText(outputPath, Environment.NewLine + dll + Environment.NewLine + "Signed : False");
                        }
                    }
                    else if (Path.GetExtension(dll).ToLower() == ".nupkg")
                    {
                        Process verify = new Process();
                        verify.StartInfo.FileName = Program.nugetExePath;                        
                        verify.StartInfo.WorkingDirectory = Path.GetDirectoryName(Program.nugetExePath);
                        verify.StartInfo.Arguments = "verify -signature \"" + dll + "\"";
                        verify.StartInfo.RedirectStandardInput = true;
                        verify.StartInfo.CreateNoWindow = true;
                        verify.StartInfo.UseShellExecute = false;
                        verify.StartInfo.RedirectStandardOutput = true;
                        verify.Start();
                        verify.WaitForExit();

                        if (!verify.ExitCode.Equals(0))
                        {
                            InstallChecker.report.AppendLine(Environment.NewLine + "Signed : False");
                            Console.WriteLine("Signed : False");
                            File.AppendAllText(outputPath, Environment.NewLine + dll + Environment.NewLine + "Signed : False");
                        }
                    }
                }
                if (authenticode.ToLower() == "true")
                {
                    if (a.SigningCertificate != null)
                    {
                        if (a.SigningCertificate.IsCurrent)
                        {
                            InstallChecker.report.AppendLine(Environment.NewLine + "Authenticode : True");
                            Console.WriteLine("Authenticode : True");
                        }
                        else
                        {
                            InstallChecker.report.AppendLine(Environment.NewLine + "Authenticode certificate was expired.");
                            Console.WriteLine("Authenticode certificate was expired.");
                        }
                    }
                    else
                    {
                        InstallChecker.report.AppendLine(Environment.NewLine + "Authenticode : False");
                        Console.WriteLine("Authenticode : False");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                InstallChecker.report.AppendLine(Environment.NewLine + ex.Message);
                Console.WriteLine(ex.Message);
                File.AppendAllText(outputPath, Environment.NewLine + dll + Environment.NewLine + ex.Message);
                return false;
            }
        }
        /*public bool GetAssemblyReference(string studioVersion, string filePath)
        {
            bool retVal = true;
            Assembly asam = Assembly.LoadFrom(filePath);
            AssemblyName[] s = asam.GetReferencedAssemblies();
            foreach (AssemblyName a in s)
                if (a.FullName.ToLower().StartsWith("syncfusion") | a.FullName.ToLower().StartsWith("democommon"))
                {
                    if (a.Version.ToString() != studioVersion)
                    {
                        InstallChecker.error.Append(Environment.NewLine + "Assembly " + a.Name + " was compiled with " + a.Version + " instead of " + studioVersion);
                        InstallChecker.report.AppendLine(Environment.NewLine+ "Error ==>Assembly " + a.Name + " was compiled with " + a.Version + " instead of " + studioVersion);
                        Console.WriteLine("Assembly " + a.Name + " was compiled with " + a.Version + " instead of " + studioVersion);
                        retVal = false;
                        //Console.WriteLine("File :" + filePath + " " + "was compiled with " + a.Version);
                        //Console.WriteLine(a.Name + " " + a.Version + " with " + studioVersion + " Versions mismatch");
                    }
                }
            return retVal;
        }*/
    }
}
