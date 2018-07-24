using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace XamarinSampleProjectVersionSwitcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectFile = args[0];
            string SyncVersion = args[1];
            string input = File.ReadAllText(projectFile);
            List<string> sourceType = new List<string>();
            string[] sourceArgs = args[3].Split(',');

            foreach (string Arg in sourceArgs)
            {
                sourceType.Add(Arg);
                sourceType.Add(Arg.ToLower());
            }

            foreach (string source in sourceType)
            {
                if (projectFile.ToLower().Contains(".csproj"))
                {
                    string frameworkVersion = args[2];
                    string[] splittedVersion = SyncVersion.Split('.');

                    int i = 0;
                    string fullVersion = string.Empty;
                    foreach (string version in splittedVersion)
                    {
                        string temp = version;
                        i++;
                        if (i == 2)
                        {
                            temp = version + frameworkVersion;
                        }
                        fullVersion += temp + '.';
                    }

                    fullVersion = fullVersion.TrimEnd('.');

                    string modifiedReferrenceContent = ", Version=" + fullVersion + ", ";
                    Regex xamarinReferenceRegex = new Regex("<\\s*[R|r]eference\\s*[I|i]nclude\\s*=\\s*\\\"" + source + ".*?\\s*>");
                    MatchCollection xamarinReference = xamarinReferenceRegex.Matches(input);

                    foreach (Match reference in xamarinReference)
                    {
                        Regex assemblyNamePattern = new Regex(source + ".*?\\.[^,]*");
                        Match assemblyName = assemblyNamePattern.Match(reference.ToString());

                        Regex assemblyCulturePattern = new Regex("[C|c]ulture=.*?\\s*>");
                        Match assemblyCulture = assemblyCulturePattern.Match(reference.ToString());

                        Regex assemblyVersionPattern = new Regex("\\s*[V|v]ersion=.*?\\s*>");
                        Match assemblyVersion = assemblyVersionPattern.Match(reference.ToString());

                        if (assemblyVersion.Success)
                        {
                            input = input.Replace(reference.ToString(), "<Reference Include=\"" + assemblyName + modifiedReferrenceContent + assemblyCulture);
                        }
                    }

                    Regex XamarinHintRegex = new Regex("<\\s*HintPath>.*?packages\\s*\\\\s*" + source + ".*?\\s*HintPath>");
                    MatchCollection xamarinHint = XamarinHintRegex.Matches(input);

                    foreach (Match reference in xamarinHint)
                    {

                        Regex hintNamePattern = new Regex(source + ".*?\\.[^0-9]*");
                        Match hintNameMatch = hintNamePattern.Match(reference.ToString());

                        Regex hintLibPattern = new Regex("lib.*?HintPath>*");
                        Match hintLibMatch = hintLibPattern.Match(reference.ToString());

                        Regex hintPackagePattern = new Regex("<HintPath>.*?packages*");
                        Match hintPackageMatch = hintPackagePattern.Match(reference.ToString());

                        input = input.Replace(reference.ToString(), hintPackageMatch + "\\" + hintNameMatch + SyncVersion + "\\" + hintLibMatch);

                    }
                    File.Delete(projectFile);
                    File.AppendAllText(projectFile, input);
                }
                else if (projectFile.ToLower().Contains("packages.config"))
                {
                    Regex XamarinPackageIdRegex = new Regex("<\\s*[P|p]ackage\\s*[I|i]d\\s*=\\s*\\\"" + source + ".*?\\s*>");
                    MatchCollection xamarinPackageIdMatch = XamarinPackageIdRegex.Matches(input);

                    foreach (Match reference in xamarinPackageIdMatch)
                    {
                        Regex assemblyNamePatten = new Regex(source + ".*?\\.[^\"]*");

                        Match assemblyName = assemblyNamePatten.Match(reference.ToString());

                        Regex targetFrameworkRegex = new Regex("[T|t]argetFramework=.*?\\s*>");
                        Match targetFrameworkMatch = targetFrameworkRegex.Match(reference.ToString());


                        input = input.Replace(reference.ToString(), "<package id=\"" + assemblyName + "\" version=\"" + SyncVersion + "\" " + targetFrameworkMatch);

                    }
                    File.Delete(projectFile);
                    File.AppendAllText(projectFile, input);
                }

                else if (projectFile.ToLower().Contains("project.json"))
                {
                    Regex XamarinPackageIdRegex = new Regex("\\s*\\\"" + source + ".*?\\s*:\\s*?\".*?\"");
                    MatchCollection xamarinPackageIdMatch = XamarinPackageIdRegex.Matches(input);

                    foreach (Match reference in xamarinPackageIdMatch)
                    {
                        Regex assemblyNamePatten = new Regex(source + ".*?\\.[^\"]*");

                        Match assemblyName = assemblyNamePatten.Match(reference.ToString());

                        Regex assemblyVersionPattern = new Regex(":\\s*?\".*?\"");
                        Match assemblyVersion = assemblyVersionPattern.Match(reference.ToString());

                        if (assemblyVersion.Success)
                        {
                            input = input.Replace(reference.ToString(), "\"" + assemblyName + "\": \"" + SyncVersion + "\"");
                        }

                    }
                    File.Delete(projectFile);
                    File.AppendAllText(projectFile, input);
                }
            }
        }
    }
}
