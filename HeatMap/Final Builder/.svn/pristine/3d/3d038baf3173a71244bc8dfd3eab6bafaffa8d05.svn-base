using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;

namespace Syncfusion.ProjectSettings.Checker
{
    public static class FolderStructure
    {
        /// <summary>
        /// This function checks whther the project folder structure is in correct format or not.
        /// Return boolean value (True) is the location not found
        /// </summary>
        /// <param name="svnpath"></param>
        /// <param name="platform"></param>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public static bool isFolderStructureMismatch(string svnpath, string platform, string projectname)
        {
            bool isFolderExist = false;
            bool isFolderStructureMismatched = false;
            string projectfilePath = string.Empty;

            if (platform.ToLower().Equals("wp8"))
            {
                projectfilePath = svnpath + "\\Phone\\" + platform + "\\" + projectname;
            }
            else if (platform.ToLower().Equals("wp81sl"))
            {
                projectfilePath = svnpath + "\\Phone\\Silverlight\\" + projectname;
            }
            else if (platform.ToLower().Equals("wp81winrt"))
            {
                projectfilePath = svnpath + "\\Phone\\WinRT\\" + projectname;
            }
            else
            {
                projectfilePath = svnpath + "\\" + platform + "\\" + projectname;
            }
            if (Directory.Exists(projectfilePath + "\\Src"))
            {
                return false;
            }
            else
            {
                if (Directory.Exists(projectfilePath + "src"))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
