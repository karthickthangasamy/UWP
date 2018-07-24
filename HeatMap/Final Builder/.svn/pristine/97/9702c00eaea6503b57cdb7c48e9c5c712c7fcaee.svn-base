using System;
using System.Text.RegularExpressions;
using System.IO;

namespace ExtractErrors
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string errorContent = File.ReadAllText(args[0]);
                Regex errorFormat = new Regex("Build FAILED.*?(\r\n.*?)*\\*", RegexOptions.Multiline);
                MatchCollection errorBlock = errorFormat.Matches(errorContent);
                if (errorBlock.Count > 0)
                {
                    foreach (var errors in errorBlock)
                    {
                        Regex errorFormat1 = new Regex(".*?\\:\\s*error.*?\\]");
                        MatchCollection errorlines = errorFormat1.Matches(errors.ToString());
                        foreach (var errorline in errorlines)
                        {
                            Console.WriteLine(errorline.ToString());
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
