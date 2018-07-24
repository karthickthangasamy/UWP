namespace CopyrightInfoUpdater
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using VSoft.CustomActionApi;

    public class CopyrightInfoUpdater : StandardAction
    {
        public override bool Execute()
        {
            try
            {
                string from = base.ExpandProperty("SourcePath");
                this.NavigateDirectories(from);
                return true;
            }
            catch (Exception exception)
            {
                base.SendMessage("Copyright Update Failed : " + exception.StackTrace, MessageType.Error);
                return false;
            }
        }

        private void NavigateDirectories(string From)
        {
            DirectoryInfo info = new DirectoryInfo(From);
            //string[] ExcludeFolder = new string[] { "Mobile.Shared.MVC" };
            string[] ExcludeFolder = base.ExpandProperty("ExcludeFolder").Split(',');
            foreach (FileInfo info2 in info.GetFiles("*.cs", SearchOption.AllDirectories))
            {
                bool flag = false;
                string path = info2.Directory.ToString();
                foreach (string name in ExcludeFolder)
                {
                    if (path.Contains(name))
                        flag = true;
                }
                if (!flag)
                    this.ReplaceCSFile(info2.FullName);
            }
        }

        private void ReplaceCSFile(string path)
        {
            string input = File.ReadAllText(path);
            string contents = null;
            string companyName = "[assembly: AssemblyCompany(\"Syncfusion Inc.\")]";
            string format = "#region Copyright Syncfusion Inc. 2001-{0:yyyy}.\r\n// Copyright Syncfusion Inc. 2001-{0:yyyy}. All rights reserved.\r\n// Use of this code is subject to the terms of our license.\r\n// A copy of the current license can be obtained at any time by e-mailing\r\n// licensing@syncfusion.com. Any infringement will be prosecuted under\r\n// applicable laws. \r\n#endregion\r\n";
            format = string.Format(format, DateTime.Now);
            string copyrightInfo = "[assembly: AssemblyCopyright(\"Copyright © 2001-{0:yyyy} Syncfusion Inc.\")]";
            copyrightInfo = string.Format(copyrightInfo, DateTime.Now);
            string replacement = "${1}" + DateTime.Now.ToString("yy");
            try
            {
                if (!Regex.IsMatch(input, "('|//|#region)[\\s]+(<)?(C|c)opyright|#Region \"(C|c)opyright", RegexOptions.IgnoreCase))
                {
                    contents = format + input;
                }
                else
                {
                    contents = Regex.Replace(input, @"(('|//|#region).*?2001[\s]+-[\s]+(20))([\d]{2})", replacement, RegexOptions.IgnoreCase);
                }
                if (Regex.IsMatch(input, "\\[\\s*assembly:\\s*AssemblyCopyright(.*?)\\]", RegexOptions.IgnoreCase))
                {
                    contents = Regex.Replace(contents, "\\[\\s*assembly:\\s*AssemblyCopyright(.*?)\\]", copyrightInfo, RegexOptions.IgnoreCase);
                }
                if (Regex.IsMatch(input, "\\[\\s*assembly:\\s*AssemblyCompany(.*?)\\]", RegexOptions.IgnoreCase))
                {
                    contents = Regex.Replace(contents, "\\[\\s*assembly:\\s*AssemblyCompany(.*?)\\]", companyName, RegexOptions.IgnoreCase);
                }
                File.WriteAllText(path, contents);
                base.SendMessage("Copyright information added to :" + path, new object[0]);
            }
            catch (ArgumentException exception)
            {
                base.SendMessage("Copyright Update Failed : " + path + " " + exception.StackTrace, MessageType.Error);
            }
        }

        

        public override void Validate()
        {
            base.ValidateNonEmptyProperty("SourcePath");
        }
    }
}

