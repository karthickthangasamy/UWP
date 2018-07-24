using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace ConsoleApplication2
{
    class Program
    {
        public void CheckVS2015Samples(string svnpath, string platform)
        {            
            List<string> samplesList = new List<string>();            
            List<string> controlList = new List<string>();
            List<string> platformList = new List<string>();

            if (!platform.ToLower().Contains("base"))
            {
                #region MVC and Web
                if (platform.ToLower().Equals("mvc") || platform.ToLower().Equals("web"))
                {
                    string[] controlDir = Directory.GetDirectories(svnpath + "\\" + platform);
                    foreach (string sampleDir in controlDir)
                    {
                        if (!sampleDir.ToLower().Contains(".svn"))
                        {
                            string[] samplesDirs = Directory.GetDirectories(sampleDir);
                            foreach (string samplesDir in samplesDirs)
                            {
                                if (!samplesDir.ToLower().Contains(".svn"))
                                {
                                    if (samplesDir.ToLower().EndsWith("samples"))
                                    {
                                        string[] samplesFilesList = Directory.GetFiles(samplesDir, "*_2015.sln", SearchOption.AllDirectories);
                                        if (samplesFilesList.Length == 0)
                                        {
                                            controlList.Add(samplesDir);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    GenerateReports(platform, string.Join("<br/>", controlList.ToArray()));
                }
                #endregion

                #region WinRT, WP, Universal, UWP and EJLightSwitch
                else if (platform.ToLower().Equals("winrt") || platform.ToLower().Equals("wp8") || platform.ToLower().Equals("universal") || platform.ToLower().Equals("wp81sl")
                    || platform.ToLower().Equals("wp81winrt") || platform.ToLower().Equals("ejlightswitch") || platform.ToLower().Equals("uwp"))
                {
                    string projectDir = string.Empty;

                    if (platform.ToLower().Equals("ejlightswitch"))
                    {
                        projectDir = svnpath + @"EJ.LightSwitch\Samples\2015";
                        if (!Directory.Exists(projectDir))
                        {
                            platformList.Add(platform);
                        }
                    }
                    else
                    {
                        if (platform.ToLower().Equals("wp8"))
                        {
                            projectDir = svnpath + "\\Phone\\" + platform;
                        }
                        else if (platform.ToLower().Equals("wp81sl"))
                        {
                            projectDir = svnpath + "\\Phone\\Silverlight";
                        }
                        else if (platform.ToLower().Equals("wp81winrt"))
                        {
                            projectDir = svnpath + "\\Phone\\WinRT";
                        }
                        else
                        {
                            projectDir = svnpath + "\\" + platform;
                        }

                        string[] sampleBrowserPath = Directory.GetFiles(projectDir + @"\\SampleBrowser", "*_2015.sln", SearchOption.AllDirectories);
                        if (sampleBrowserPath.Length == 0)
                        {
                            platformList.Add(platform);
                        }
                    }

                    GenerateReports(string.Join("<br/>", platformList.ToArray()), "SampleBrowser");
                }
                #endregion

                #region WPF, Windows, EJMVC, EJ Web and Silverlight
                if (platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows") || platform.ToLower().Equals("ej.mvc") || platform.ToLower().Equals("ej.web") || platform.ToLower().Equals("silverlightsamplebrowserprojects"))
                {
                    string[] controlDir = Directory.GetDirectories(svnpath + "\\" + platform);
                    foreach (string sampleDir in controlDir)
                    {
                        if (!sampleDir.ToLower().Contains(".svn"))
                        {
                            string[] samplesDirs = Directory.GetDirectories(sampleDir);
                            foreach (string samplesDir in samplesDirs)
                            {
                                if (!samplesDir.ToLower().Contains(".svn"))
                                {
                                    if (samplesDir.ToLower().Contains("samples") || platform.ToLower().Equals("silverlightsamplebrowserprojects"))
                                    {
                                        string[] samplesDirList;

                                        if (platform.ToLower().Equals("wpf") || platform.ToLower().Equals("windows"))
                                        {
                                            samplesDirList = Directory.GetDirectories(samplesDir, @"CS", SearchOption.AllDirectories);

                                            foreach (string sampleDirectory in samplesDirList)
                                            {
                                                if (!sampleDirectory.ToLower().Contains(".svn"))
                                                {
                                                    string[] samplesFilesList = Directory.GetFiles(sampleDirectory, "*_2015.sln", SearchOption.AllDirectories);
                                                    if (samplesFilesList.Length == 0)
                                                    {
                                                        controlList.Add(sampleDirectory);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string[] samplesFilesList = Directory.GetFiles(samplesDir, "*_2015.sln", SearchOption.AllDirectories);
                                            if (samplesFilesList.Length == 0)
                                            {
                                                controlList.Add(samplesDir);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    GenerateReports(platform, string.Join("<br/>", controlList.ToArray()));
                }
                #endregion
            }
            samplesList.Clear();
            platformList.Clear();
            controlList.Clear();    
        }

        public void GenerateReports(string platform, string samples)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists("VS2015Samples.htm"))
            {
                doc.Load("VS2015Samples.htm");
                XmlNode reportTable = doc.GetElementsByTagName("table")[3];
                reportTable.AppendChild(doc.CreateElement("tr"));
                reportTable.AppendChild(doc.CreateElement("td")).InnerXml = platform;                
                reportTable.AppendChild(doc.CreateElement("td")).InnerXml = samples;
                doc.Save("VS2015Samples.htm");
            }

        }     

        static void Main(string[] args)
        {
            Program p = new Program();
            //p.CheckVS2015Samples(args[0], "MVC");
            //p.CheckVS2015Samples(args[0], "Web");
            p.CheckVS2015Samples(args[0], "WPF");
            p.CheckVS2015Samples(args[0], "Windows");
            //p.CheckVS2015Samples(args[0] + @"\Build\Utilities", "SilverlightSampleBrowserProjects");
            p.CheckVS2015Samples(args[0], "EJ.MVC");
            p.CheckVS2015Samples(args[0], "EJ.WEB");
            p.CheckVS2015Samples(args[0], "WinRT");
            p.CheckVS2015Samples(args[0], "WP8");
            p.CheckVS2015Samples(args[0], "WP81SL");
            p.CheckVS2015Samples(args[0], "WP81WinRT");
            p.CheckVS2015Samples(args[0], "Universal");
            //p.CheckVS2015Samples(args[0], "UWP");
            p.CheckVS2015Samples(args[0], "EJLightSwitch");
        }
    }
}
