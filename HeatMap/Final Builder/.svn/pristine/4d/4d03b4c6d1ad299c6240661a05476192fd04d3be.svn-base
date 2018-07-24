using System;
using System.Text.RegularExpressions;
using System.IO;

namespace ExtractErrorsandWarnings
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string fileContent = args[0];
                string errorOrWarning = args[1];
                string errorContent = File.ReadAllText(fileContent);
                Regex errorFormat;
                if(errorOrWarning.ToLower().Equals("error"))
                    errorFormat = new Regex("Build FAILED.*?(\r\n.*?)*$", RegexOptions.Multiline);
                else
                    errorFormat = new Regex("Build succeeded.*?(\r\n.*?)*$", RegexOptions.Multiline);
                MatchCollection errorBlock = errorFormat.Matches(errorContent);
                if (errorBlock.Count > 0)
                {
                    foreach (var errors in errorBlock)
                    {
                        Regex errorFormat1;
                        if(errorOrWarning.ToLower().Equals("error"))
                            errorFormat1 = new Regex(".*?\\:\\s*error.*?\\]");
                        else
                            errorFormat1 = new Regex(".*?\\:\\s*warning.*?\\]");
                        MatchCollection errorlines = errorFormat1.Matches(errors.ToString());
                        foreach (var errorline in errorlines)
                        {
                            Console.WriteLine(errorline.ToString().Replace("\"",""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
