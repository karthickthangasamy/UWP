#region Copyright Syncfusion Inc. 2001 - 2018
// Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace SVNUnversionedFilesRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1) return;
                string directory = args[0].TrimEnd('\"');

                /* Product team committed some folder name as src instead of Src in their source folder (like 
                C:\Work\SVN\RP_14.1.0.41_SP1\WPF\SfChart.WPF\src), due to this SVNUnversionedFilesRemover.exe was crashed.
                So overcome this issue,handled code in this. 

                Note: We have passed all the source folder name as "Src" from final builder*/

                DirectoryInfo dirInfo = new DirectoryInfo(directory);
                if (dirInfo.Name == "Src")
                {
                    string caseDirectrty = string.Empty;
                    caseDirectrty = directory.Remove(directory.Length - 3);

                    dirInfo = new DirectoryInfo(caseDirectrty);
                    foreach (DirectoryInfo subDir in dirInfo.GetDirectories())
                    {
                        if (subDir.Name == "src")
                        {
                            directory = caseDirectrty + subDir.Name;
                            break;
                        }
                    }
                }
                Console.WriteLine("SVN cleaning directory {0}", directory);

                Directory.SetCurrentDirectory(directory);
                ProcessStartInfo psi = new ProcessStartInfo("svn.exe", "status --non-interactive");
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.WorkingDirectory = directory;

                using (Process process = Process.Start(psi))
                {
                    string line = process.StandardOutput.ReadLine();
                    while (line != null)
                    {
                        if (line.Length > 7)
                        {
                            if (line[0] == '?')
                            {
                                string relativePath = line.Substring(8);
                                Console.WriteLine(relativePath);

                                string path = Path.Combine(directory, relativePath);

                                if (Directory.Exists(path))
                                {
                                    DeleteDir(path);
                                }
                                else if (File.Exists(path))
                                {
                                    FileInfo file = new FileInfo(path);
                                    file.IsReadOnly = false;
                                    try
                                    {
                                        File.Delete(path);
                                    }
                                    catch (IOException)
                                    {
                                        File.AppendAllText("SVNUnversionedFilesRemover.log", "\nCould not delete the file : " + path.ToString());
                                    }
                                }
                            }
                        }
                        line = process.StandardOutput.ReadLine();
                    }
                }
            }
            catch (IOException ex)
            {
                File.AppendAllText("SVNUnversionedFilesRemover.log", "\nCould not delete the file : " + ex.Message.ToString());
            }
        }

        static void DeleteDir(string root)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(root);
                FileInfo[] fis = di.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    fi.IsReadOnly = false;
                    try
                    {
                        File.Delete(fi.FullName);
                    }
                    catch (IOException)
                    {
                        File.AppendAllText("SVNUnversionedFilesRemover.log", "\nCould not delete the file : " + fi.FullName.ToString());
                    }
                }


                foreach (DirectoryInfo dinfo in di.GetDirectories())
                {
                    DeleteDir(dinfo.FullName);
                }
                if (di.GetDirectories().Length == 0)
                {
                    try
                    {
                        Directory.Delete(di.FullName);
                        return;
                    }
                    catch (IOException)
                    {
                        File.AppendAllText("SVNUnversionedFilesRemover.log", "\nCould not delete the directory : " + di.FullName.ToString());
                    }
                }
            }
            catch (IOException ex)
            {
                File.AppendAllText("SVNUnversionedFilesRemover.log", "\nCould not delete the directory : " + ex.Message.ToString());
            }
        }
    }
}
