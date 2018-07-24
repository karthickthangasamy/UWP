using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = new Process();
            ProcessStartInfo ps=new ProcessStartInfo();
            ps.FileName = @"C:\Users\anandharamans\Desktop\Migration\AssemblyReference\bin\Debug\AssemblyReference.exe";
            ps.Arguments = "8.103.0.30" + " " + "\"" + @"C:\Program Files (x86)\Syncfusion\Essential Studio\8.1.0.30\Infrastructure\Dashboard\4.0\Dashboard.exe" + "\"" + " \"" + @"C:\Users\anandharamans\Desktop\Migration\ErrorReport.txt" + "\"";
            p.StartInfo = ps;
            p.Start();
            p.WaitForExit();
            string s = File.ReadAllText(@"C:\Users\anandharamans\Desktop\Migration\ErrorReport.txt");
            ps.Arguments = "8.103.0.30" + " " + "\"" + @"C:\Program Files (x86)\Syncfusion\Essential Studio\8.1.0.30\Infrastructure\Dashboard\2.0\Dashboard.exe" + "\"" + " \"" + @"C:\Users\anandharamans\Desktop\Migration\ErrorReport.txt" + "\"";
            p.StartInfo = ps;
            p.Start();
            p.WaitForExit();
        }
    }
}
