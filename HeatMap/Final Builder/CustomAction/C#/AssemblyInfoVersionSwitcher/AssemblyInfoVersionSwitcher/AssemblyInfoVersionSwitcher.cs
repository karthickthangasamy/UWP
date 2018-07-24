#region Copyright Syncfusion Inc. 2001 - 2012
// Copyright Syncfusion Inc. 2001 - 2012. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace AssemblyInfoVersionSwitcher
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using VSoft.CustomActionApi;

    public class AssemblyInfoVersionSwitcher : StandardAction
    {
        private string[] includeFilterCollection;
        private string[] versionCollection;
        private string[] fileVersionCollection;

        private void ApplyStrings(string path, string assemblyFileVersion, string assemblyTitle, string assemblyProduct, string copyright, string Platform)
        {
            try
            {
                bool flag = false;
                string framework_35 = "03";
                string framework_40 = "04";
                string framework_35_113 = "035";
                string framework_40_113 = "040";
                string framework_35New = "350";
                string framework_40New = "400";
                string syncfusionLicense = System.Environment.GetEnvironmentVariable("SyncfusionLicensing", EnvironmentVariableTarget.Machine);

                if (!string.IsNullOrEmpty(Platform))
                {
                    if (Platform.ToLower() == "mvc2")
                    {
                        framework_35 = "23";
                        framework_40 = "24";
                    }
                    else if (Platform.ToLower() == "mvc3")
                    {
                        framework_35 = "23";
                        framework_40 = "34";
                    }
                }
                foreach (string str7 in this.includeFilterCollection)
                {
                    if (path.ToLower().Contains(str7.ToLower()))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    string input = File.ReadAllText(path);
                    string contents = null;
                    StringBuilder builder = new StringBuilder();
                    if (Platform.ToLower().Contains("mvc"))
                    {
                        if (((int.Parse(this.versionCollection[0]) >= 13) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 14) && (int.Parse(this.versionCollection[1]) >= 1)))
                        {
                            builder.AppendLine("#if (SyncfusionFramework4_6 && MVC6)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "600." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC5_1)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "510." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC5)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "500." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "300." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#else");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#endif");
                        }

                        else if ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1))
                        {
                            builder.AppendLine("#if (SyncfusionFramework4_5 && MVC5_1)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "510." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC5)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "500." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "300." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#else");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#endif");
                        }
                        else if ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3))
                        {
                            builder.AppendLine("#if (SyncfusionFramework4_5 && MVC5)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "500." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "300." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#else");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#endif");
                        }
                        else if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 1)) && ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3)))
                        {
                            builder.AppendLine("#if (SyncfusionFramework4_5 && MVC4)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "445." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC3)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "345." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "440." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "340." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif SyncfusionFramework3_5");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "235." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#else");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "440." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#endif");
                        }
                        else if (((int.Parse(this.versionCollection[0]) >= 10) && (int.Parse(this.versionCollection[1]) > 2)) || ((int.Parse(this.versionCollection[0]) > 10) && (int.Parse(this.versionCollection[1]) <= 1)))
                        {
                            builder.AppendLine("#if (SyncfusionFramework4_0 && MVC4)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "44." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "34." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif (SyncfusionFramework4_0)");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "24." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif SyncfusionFramework3_5");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "23." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#else");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "44." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#endif");
                        }
                        else
                        {
                            framework_35 = "03";
                            framework_40 = "04";
                            builder.AppendLine("#if SyncfusionFramework4_0");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif SyncfusionFramework3_5");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif SyncfusionFramework2_0");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "02." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#else");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#endif");
                        }
                    }
                    else if (Platform.ToLower().Contains("silverlight") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1))))
                    {
                        builder.AppendLine("#if (SyncfusionFramework4_0 && Silverlight5)");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "500." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif (SyncfusionFramework4_0 && Silverlight4)");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (Platform.ToLower().Contains("silverlight") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3))))
                    {
                        builder.AppendLine("#if (SyncfusionFramework4_0 && Silverlight5)");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "050." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif (SyncfusionFramework4_0 && Silverlight4)");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "040." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "040." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if ((Platform.ToLower().Contains("winrt") && !Platform.ToLower().Contains("wp")) && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1))))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5_1");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if ((Platform.ToLower().Contains("winrt") && !Platform.ToLower().Contains("wp")) && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3))))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "010." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "010." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (Platform.ToLower().Contains("wp7") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1))))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "710." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "710." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (Platform.ToLower().Contains("wp7") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3))))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "071." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "071." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if ((Platform.ToLower().Contains("wp81sl") || Platform.ToLower().Contains("wp81winrt")) && (int.Parse(versionCollection[0] + versionCollection[1]) > 121))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5_1");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (Platform.ToLower().Contains("wp8") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1))))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (Platform.ToLower().Contains("wp8") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3))))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "080." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "080." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "080." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (Platform.ToLower().Trim().Equals("portable") && int.Parse(this.versionCollection[0]) >= 15)
                    {
                        
                        if (int.Parse(this.versionCollection[0]) >= 16 || int.Parse(this.versionCollection[1]) >= 3)
                        {
                            builder.AppendLine("#if NetStandard20");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "200." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif NetStandard14");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "140." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                            builder.AppendLine("#elif NetStandard");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "120." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        }
                        else
                        {
                            builder.AppendLine("#if NetStandard");
                            builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "120." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        }
                        builder.AppendLine("#endif");
                    }
                    else if (((int.Parse(this.versionCollection[0]) >= 13) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 14) && (int.Parse(this.versionCollection[1]) >= 1)))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_6");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "460." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_5_1");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "451." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "450." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework3_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework2_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "200." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }

                    else if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1)))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5_1");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "451." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "450." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework3_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework2_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "200." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) && ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3)))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "045." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40_113 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework3_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35_113 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework2_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "020." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35_113 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else if (Platform.ToLower().Contains("bigdata") && (int.Parse(this.versionCollection[0]) >= 2) && (int.Parse(this.versionCollection[1]) >= 1))
                    {
                        builder.AppendLine("#if SyncfusionFramework4_5_1");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "451." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "450." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    else
                    {
                        builder.AppendLine("#if SyncfusionFramework4_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework3_5");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#elif SyncfusionFramework2_0");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "02." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#else");
                        builder.AppendLine("[assembly: AssemblyVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                        builder.AppendLine("#endif");
                    }
                    #region Changing Assembly File Version
                    if (!string.IsNullOrEmpty(assemblyFileVersion.Trim()))
                    {
                        //File Version for WinRT platform
                        if (Platform.ToLower().Contains("winrt") && !Platform.ToLower().Contains("wp"))
                        {
                            input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                            input = Regex.Replace(input, "#if\\s*\\(\\s*SyncfusionFramework\\d_\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else|&&|MVC|Mvc|mvc)*#endif", "", RegexOptions.Singleline);
                            //V11.4 and later
                            if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1)))
                            {
                                builder.AppendLine("#if SyncfusionFramework4_5_1");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "810." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "800." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "800." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                                //v11.2 and v11.3
                            else if(((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3)))
                            {
                                builder.AppendLine("#if SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "010." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "010." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "010." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "010." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                        }
                            //File Version for Windows Phone platform
                        else if (Platform.ToLower().Contains("wp") && !Platform.ToLower().Contains("wpf"))
                        {
                            input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                            input = Regex.Replace(input, "#if\\s*\\(\\s*SyncfusionFramework\\d_\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else|&&|MVC|Mvc|mvc)*#endif", "", RegexOptions.Singleline);
                            //WP7 File Version
                            if (Platform.ToLower().Contains("wp7"))
                            {
                                //V11.4 and later
                                if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1)))
                                {
                                    builder.AppendLine("#if SyncfusionFramework4_0");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "710." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "710." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#else");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "710." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "710." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#endif");
                                }
                                    //V11.2 and V11.3
                                else if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3)))
                                {
                                    builder.AppendLine("#if SyncfusionFramework4_0");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "071." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "071." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#else");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "071." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "071." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#endif");
                                }
                            }
                                //WP8 Silverlight and WinRT File Version here. V12.2 and later
                       
                            else if ((Platform.ToLower().Contains("wp81sl") || Platform.ToLower().Contains("wp81winrt")) && (int.Parse(versionCollection[0] + versionCollection[1]) > 121))
                            {
                                builder.AppendLine("#if SyncfusionFramework4_5_1");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "810." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "810." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "810." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "810." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "810." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                                //WP8, V11.4 and later
                            else if (Platform.ToLower().Contains("wp8") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1))))
                            {
                                builder.AppendLine("#if SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "800." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "800." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "800." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "800." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                                //WP8, V11.2 and V11.3
                            else if (Platform.ToLower().Contains("wp8") && (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3))))
                            {
                                builder.AppendLine("#if SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "080." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "080." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "080." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "080." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "080." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "080." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                        }
                            //File Version for V11.4 and later
                        else if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) > 3)) || ((int.Parse(this.versionCollection[0]) >= 12) && (int.Parse(this.versionCollection[1]) >= 1)))
                        {
                            if (Platform.ToLower().Contains("mvc"))
                            {
                                if (((int.Parse(this.versionCollection[0]) >= 13) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 14) && (int.Parse(this.versionCollection[1]) >= 1)))
                                {
                                    input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                                    input = Regex.Replace(input, "#if(\\s|\\()*SyncfusionFramework\\d_\\d(\\s|&&|\\)|MVC|Mvc|mvc|\\d|_\\d)(_|\\[|\\n|\\s|assembly|:|AssemblyFileVersion|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|MVC|Mvc|mvc|#else)*#endif", "", RegexOptions.Singleline);
                                    if (!input.ToLower().Contains("assembly: assemblyinformationalversion"))
                                    {
                                        builder.AppendLine("#if (SyncfusionFramework4_6 && MVC6)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "600." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "600." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC5_1)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "510." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "510." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC5)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "500." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "500." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "400." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "300." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "300." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#else");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "400." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#endif");
                                    }
                                }

                                else if (((int.Parse(versionCollection[0]) >= 12) && (int.Parse(versionCollection[1]) >= 1)))
                                {

                                    input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                                    input = Regex.Replace(input, "#if(\\s|\\()*SyncfusionFramework\\d_\\d(\\s|&&|\\)|MVC|Mvc|mvc|\\d|_\\d)(_|\\[|\\n|\\s|assembly|:|AssemblyFileVersion|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|MVC|Mvc|mvc|#else)*#endif", "", RegexOptions.Singleline);
                                    if (!input.ToLower().Contains("assembly: assemblyinformationalversion"))
                                    {
                                        builder.AppendLine("#if (SyncfusionFramework4_5 && MVC5_1)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "510." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "510." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC5)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "500." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "500." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "400." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "300." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "300." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#else");
                                        builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "400." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                        builder.AppendLine("#endif");
                                    }
                                }

                                else
                                {
                                    input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                                    input = Regex.Replace(input, "#if(\\s|\\()*SyncfusionFramework\\d_\\d(\\s|&&|\\)|MVC|Mvc|mvc|\\d|_\\d)(_|\\[|\\n|\\s|assembly|:|AssemblyFileVersion|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|MVC|Mvc|mvc|#else)*#endif", "", RegexOptions.Singleline);

                                    builder.AppendLine("#if (SyncfusionFramework4_5 && MVC5)");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "500." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "500." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "400." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "300." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "300." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#else");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "400." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "400." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("#endif");
                                }
                            }

                            else if (Platform.ToLower().Trim().Equals("portable") && int.Parse(this.versionCollection[0]) >= 15)
                            {
                               
                                if (int.Parse(this.versionCollection[0]) >= 16 || int.Parse(this.versionCollection[1]) >= 3)
                                {
                                    input = Regex.Replace(input, "#if\\s*NetStandard\\d\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|NetStandard|#else)*#endif", "", RegexOptions.Singleline);
                                    
                                    builder.AppendLine("#if NetStandard20");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "200." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "200." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");

                                    builder.AppendLine("#elif NetStandard14");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "140." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "140." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");

                                    builder.AppendLine("#elif NetStandard");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "120." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "120." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                }
                                else
                                {
                                    input = Regex.Replace(input, "#if\\s*NetStandard(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                                    builder.AppendLine("#if NetStandard");
                                    builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "120." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                    builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "120." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                }
                                builder.AppendLine("#endif");
                            }

                            else if (((int.Parse(this.versionCollection[0]) >= 13) && (int.Parse(this.versionCollection[1]) >= 2)) || ((int.Parse(this.versionCollection[0]) >= 14) && (int.Parse(this.versionCollection[1]) >= 1)))
                            {
                                input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                                input = Regex.Replace(input, "#if(\\s|\\()*SyncfusionFramework\\d_\\d(\\s|&&|\\)|MVC|Mvc|mvc|\\d|_\\d)(_|\\[|\\n|\\s|assembly|:|AssemblyFileVersion|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|MVC|Mvc|mvc|#else)*#endif", "", RegexOptions.Singleline);

                                builder.AppendLine("#if SyncfusionFramework4_6");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "460." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "460." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_5_1");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "451." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "451." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "450." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "450." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_40New + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework3_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35New + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework2_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "200." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "200." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35New + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }

                            else
                            {
                                input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                                input = Regex.Replace(input, "#if\\s*\\(\\s*SyncfusionFramework\\d_\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else|&&|MVC|Mvc|mvc)*#endif", "", RegexOptions.Singleline);

                                builder.AppendLine("#if SyncfusionFramework4_5_1");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "451." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "451." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "450." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "450." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_40New + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework3_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35New + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework2_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "200." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "200." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35New + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35New + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                            
                        }
                        // Changing version for v11.2 and above product
                        else if (((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) >= 2)) && ((int.Parse(this.versionCollection[0]) >= 11) && (int.Parse(this.versionCollection[1]) <= 3)))
                        {
                            input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                            input = Regex.Replace(input, "#if(\\s|\\()*SyncfusionFramework\\d_\\d(\\s|&&|\\)|MVC|Mvc|mvc|\\d|_\\d)(_|\\[|\\n|\\s|assembly|:|AssemblyFileVersion|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|MVC|Mvc|mvc|#else)*#endif", "", RegexOptions.Singleline);
                            if (Platform.ToLower().Contains("mvc"))
                            {
                                builder.AppendLine("#if (SyncfusionFramework4_5 && MVC4)");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "445." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "445." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif (SyncfusionFramework4_5 && MVC3)");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "345." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "345." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC4)");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "440." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "440." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "340." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "340." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework3_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "235." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "235." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "440." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "440." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                            }
                            else
                            {
                                builder.AppendLine("#if SyncfusionFramework4_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "045." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "045." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework4_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40_113 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_40_113 + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework3_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35_113 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35_113 + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework2_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "020." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "020." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35_113 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35_113 + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                            }
                            builder.AppendLine("#endif");
                        }
                        //Earlier Version - before from 11.1
                        else
                        {
                            input = Regex.Replace(input, "#if\\s*SyncfusionFramework\\d_\\d(\\[|\\n|\\s|assembly|:|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]\\[|\\n|\\s|assembly|:|AssemblyFileVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", "", RegexOptions.Singleline);
                            input = Regex.Replace(input, "#if(\\s|\\()*SyncfusionFramework\\d_\\d(\\s|&&|\\)|MVC|Mvc|mvc|\\d|_\\d)(_|\\[|\\n|\\s|assembly|:|AssemblyFileVersion|AssemblyInformationalVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|MVC|Mvc|mvc|#else)*#endif", "", RegexOptions.Singleline);
                            if (Platform.ToLower().Contains("mvc"))
                            {
                                builder.AppendLine("#if (SyncfusionFramework4_0 && MVC4)");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "44." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "44." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif (SyncfusionFramework4_0 && MVC3)");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "34." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "34." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework3_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "23." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "23." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "04." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "04." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                            else if(!Platform.ToLower().Contains("bigdata"))
                            {
                                builder.AppendLine("#if SyncfusionFramework4_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_40 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_40 + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework3_5");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35 + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#elif SyncfusionFramework2_0");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + "02." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + "02." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#else");
                                builder.AppendLine("[assembly: AssemblyInformationalVersion(\"" + this.versionCollection[0].ToString() + "." + this.versionCollection[1].ToString() + framework_35 + "." + this.versionCollection[2].ToString() + "." + this.versionCollection[3].ToString() + "\")]");
                                builder.AppendLine("[assembly: AssemblyFileVersion(\"" + this.fileVersionCollection[0].ToString() + "." + this.fileVersionCollection[1].ToString() + framework_35 + "." + this.fileVersionCollection[2].ToString() + "." + this.fileVersionCollection[3].ToString() + "\")]");
                                builder.AppendLine("#endif");
                            }
                        }
                    }
                    #endregion
                    try
                    {
                        if (!path.ToLower().Contains("lightswitch") || !path.ToLower().Contains("ej.lightswitch"))
                        {
                            if (path.ToLower().Contains("src"))
                            {
                                string currentPath = string.Empty;
                                string platformName = string.Empty;
                                if (path.Contains("Src"))
                                {
                                    currentPath = path.Remove(path.LastIndexOf("Src"));
                                }
                                if (path.Contains("src"))
                                {
                                    currentPath = path.Remove(path.LastIndexOf("src"));
                                }
                                currentPath = currentPath.Remove(currentPath.LastIndexOf('\\'));
                                string controlName = new DirectoryInfo(currentPath).Name;
                                string[] arr_ControlName = controlName.Split('.');
                                if (arr_ControlName.Length == 2 || arr_ControlName.Length == 4)
                                {
                                    controlName = arr_ControlName[0];
                                    if (arr_ControlName[0].ToLower().Contains("theming"))
                                    {
                                        platformName = "Silverlight";
                                    }
                                    else
                                        platformName = arr_ControlName[1];                                   
                                }
                                else if (arr_ControlName.Length == 3 || arr_ControlName.Length == 5)
                                {
                                    controlName = arr_ControlName[0] + arr_ControlName[1];
                                    platformName = arr_ControlName[2];
                                }

                                string productName = "Syncfusion Essential " + controlName + " " + platformName;
                                Regex regex = new Regex("\\[\\s*assembly\\s*:\\s*AssemblyProduct\\s*\\(\\s*\"([\r\n]|(.*?))*\"\\s*\\)\\s*]");
                                if (!((!regex.Match(input).Success || string.IsNullOrEmpty(productName.Trim())) || regex.Match(input).Value.Contains(productName)))
                                {
                                    Regex regex2 = new Regex("\\s*\"([\r\n]|(.*?))*\"\\s*\\)\\s*]");
                                    input = regex.Replace(input, regex2.Replace(regex.Match(input).Value, "\"" + productName.Trim() + "\")]"));
                                }
                                File.WriteAllText(path, input);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                    }
                    Regex regex3 = new Regex("\\[\\s*assembly\\s*:\\s*AssemblyTitle\\s*\\(\\s*\"([\r\n]|(.*?))*\"\\s*\\)\\s*]");
                    if (!(!regex3.Match(input).Success || string.IsNullOrEmpty(assemblyTitle.Trim())))
                    {
                        input = regex3.Replace(input, "[assembly: AssemblyTitle(\"" + assemblyTitle + "\")]");
                    }

                    if (syncfusionLicense.ToLower().Equals("syncfusionlicense"))
                    {
                        if (regex3.Match(input).Success && !regex3.Match(input).Value.ToLower().Contains("(lr)"))
                        {
                            string matchValue = regex3.Match(input).Value.Replace("\")", " (LR)\")");
                            input = regex3.Replace(input, matchValue);
                        }
                    }

                    Regex regex4 = new Regex("\\[\\s*assembly\\s*:\\s*AssemblyCopyright\\s*\\(\\s*\"\\s*([\r\n]|(.*?))*\"\\s*\\)\\s*]");
                    if (!(!regex4.Match(input).Success || string.IsNullOrEmpty(copyright.Trim())))
                    {
                        input = regex4.Replace(input, "[assembly: AssemblyCopyright(\"" + copyright.Trim() + "\")]");
                    }
                    if (Platform.ToLower().Contains("mvc"))
                    {
                        contents = Regex.Replace(Regex.Replace(input, "\\s*\\[assembly\\s*\\:\\s*AssemblyFileVersion\\s*\\(\\s*\\\"[\\d]{0,3}\\.[\\d]{0,5}\\.[\\d]{0,3}\\.[\\d]{0,9}\\\"\\s*\\)\\s*\\]", "", RegexOptions.Singleline), "#if(\\s|\\()*SyncfusionFramework\\d_\\d(\\s|&&|\\)|MVC|Mvc|mvc|\\d|_\\d)(_|\\[|\\n|\\s|assembly|:|AssemblyVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|MVC|Mvc|mvc|#else)*#endif", builder.ToString(), RegexOptions.Singleline);
                    }
                    else if (Platform.ToLower().Contains("silverlight"))
                    {
                        contents = Regex.Replace(Regex.Replace(input, "\\s*\\[assembly\\s*\\:\\s*AssemblyFileVersion\\s*\\(\\s*\\\"[\\d]{0,3}\\.[\\d]{0,5}\\.[\\d]{0,3}\\.[\\d]{0,9}\\\"\\s*\\)\\s*\\]", "", RegexOptions.Singleline), "#if(\\s|\\()*SyncfusionFramework\\d_\\d(_|_\\d|\\s|&&|\\)|SILVERLIGHT5|Silverlight5|silverlight5|\\d)(\\[|\\n|\\s|assembly|:|AssemblyVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|\\(|SyncfusionFramework\\d_\\d|&&|SILVERLIGHT|Silverlight|silverlight|#else)*#endif", builder.ToString(), RegexOptions.Singleline);
                    }
                    else if (Platform.ToLower().Trim().Equals("portable") && int.Parse(this.versionCollection[0]) >= 15)
                    {
                        contents = Regex.Replace(Regex.Replace(input, "\\s*\\[assembly\\s*\\:\\s*AssemblyFileVersion\\s*\\(\\s*\\\"[\\d]{0,3}\\.[\\d]{0,5}\\.[\\d]{0,3}\\.[\\d]{0,9}\\\"\\s*\\)\\s*\\]", "", RegexOptions.Singleline), "#if\\s*NetStandard(_|_\\d|\\[|\\n|\\s|assembly|:|AssemblyVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|NetStandard|#else)*#endif", builder.ToString(), RegexOptions.Singleline);
                    }
                    else
                    {
                        contents = Regex.Replace(Regex.Replace(input, "\\s*\\[assembly\\s*\\:\\s*AssemblyFileVersion\\s*\\(\\s*\\\"[\\d]{0,3}\\.[\\d]{0,5}\\.[\\d]{0,3}\\.[\\d]{0,9}\\\"\\s*\\)\\s*\\]", "", RegexOptions.Singleline), "#if\\s*SyncfusionFramework\\d_\\d(_|_\\d|\\[|\\n|\\s|assembly|:|AssemblyVersion|\\(|\\)|\\\"|\\.|\\d|\\]|#elif|SyncfusionFramework\\d_\\d|#else)*#endif", builder.ToString(), RegexOptions.Singleline);
                    }
                    File.WriteAllText(path, contents);
                    base.SendMessage("AssemblySwitching operation completed to file :" + path, new object[0]);
                }
            }
            catch (Exception exception)
            {
                base.SendMessage("AssemblySwitcher operation failed : " + path + " " + exception.StackTrace, 0);
            }
        }

        public override bool Execute()
        {
            try
            {
                string path = base.ExpandProperty("SourcePath");
                string str2 = base.ExpandProperty("Version");
                string assemblyFileVersion = base.ExpandProperty("AssemblyFileVersion");
                string str4 = base.ExpandProperty("IncludeFilter");
                string assemblyTitle = base.ExpandProperty("AssemblyTitle");
                string assemblyProduct = base.ExpandProperty("AssemblyProduct");
                string copyright = base.ExpandProperty("Copyright");
                string mVC = base.ExpandProperty("Platform");
                this.versionCollection = str2.Split(new char[] { '.' });
                this.fileVersionCollection = assemblyFileVersion.Split(new char[] { '.' });
                this.includeFilterCollection = str4.Split(new char[] { ',' });
                this.NavigateDirecotories(path, assemblyFileVersion, assemblyTitle, assemblyProduct, copyright, mVC);
                return true;
            }
            catch (Exception exception)
            {
                base.SendMessage("AssemblySwitcher operation failed : " + exception.StackTrace, 0);
                return false;
            }
        }

        private void NavigateDirecotories(string path, string assemblyFileVersion, string assemblyTitle, string assemblyProduct, string copyright, string MVC)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo info2 in info.GetFiles("assemblyinfo.cs", SearchOption.AllDirectories))
            {
                this.ApplyStrings(info2.FullName, assemblyFileVersion, assemblyTitle, assemblyProduct, copyright, MVC);
            }
        }

        public override void Validate()
        {
            base.ValidateNonEmptyProperty("SourcePath");
            base.ValidateNonEmptyProperty("Version");
        }

     

    }
}

