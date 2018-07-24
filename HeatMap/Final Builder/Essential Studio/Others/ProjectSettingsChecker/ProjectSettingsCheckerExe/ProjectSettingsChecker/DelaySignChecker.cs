using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Syncfusion.ProjectSettings.Checker
{
    public static class DelaySignChecker
    {

        public static bool isDelaysignExists(string svnpath, string platform, string projectname)
        {
            ProjectLocation objlocation = new ProjectLocation();
            string projectfileLocation = string.Empty;
            bool isExists = true;
            projectfileLocation = objlocation.projectPath(svnpath, platform, projectname);
            if(!platform.ToLower().Equals("silverlight") || (platform.ToLower().Equals("silverlight") && !projectname.ToLower().Contains("dll.design")))
            if (File.Exists((projectfileLocation + "\\AssemblyInfo.cs")))
            {
                isExists = searchDelaySign(projectfileLocation + "\\AssemblyInfo.cs");
            }
            else if (File.Exists(projectfileLocation + "\\Properties\\AssemblyInfo.cs"))
            {
                isExists = searchDelaySign(projectfileLocation + "\\Properties\\AssemblyInfo.cs");
            }
            return isExists;
        }

        public static bool searchDelaySign(string filelocation)
        {
            bool isKeyMatch = false;
            bool isDelaySignMatch = false;
            StreamReader reader = new StreamReader(filelocation);
            string currentLine = string.Empty;
            do
            {
                currentLine = reader.ReadLine();
                if (!string.IsNullOrEmpty(currentLine))
                {
                    if (currentLine.ToLower().Contains("assemblydelaysign(false)"))
                    {
                        isDelaySignMatch = true;
                    }
                        if (currentLine.ToLower().Contains("assemblykeyfile(\"../../../../../common/keys/sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(\"../../../../../../common/keys/sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(@\"../../../../../common/keys/sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(@\"../../../../../../common/keys/sf.snk\")") ||
                            currentLine.ToLower().Contains("assemblykeyfile(\"..\\..\\..\\..\\..\\common\\keys\\sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(\"..\\..\\..\\..\\..\\..\\common\\keys\\sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(@\"..\\..\\..\\..\\..\\common\\keys\\sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(@\"..\\..\\..\\..\\..\\..\\common\\keys\\sf.snk\")")
                             || currentLine.ToLower().Contains("assemblykeyfile(@\"..\\..\\..\\common/keys/sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(@\"..\\..\\..\\common\\keys\\sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(@\"../../../common/keys/sf.snk\")") || currentLine.ToLower().Contains("assemblykeyfile(\"../../../common/keys/sf.snk\")"))
                        {
                            isKeyMatch = true;
                        }
                }
                if (isDelaySignMatch && isKeyMatch)
                    break;
            } while (currentLine != null);
            if (isDelaySignMatch && isKeyMatch)
                return true;
            else
                return false;
        }
    }
}
