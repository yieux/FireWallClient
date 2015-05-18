using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InstallMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Process proc = null;
            try
            {
                string targetDir = string.Format(@"D:\adapters\setup");//this is where mybatch.bat lies
                targetDir = System.Environment.CurrentDirectory;
                targetDir = Process.GetCurrentProcess().MainModule.FileName;
                int end  = targetDir.LastIndexOf(@"\");
                targetDir = targetDir.Substring(0,end);
                Console.WriteLine(targetDir);
                proc = new Process();
                //proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.UseShellExecute = true;
                if (Environment.Is64BitOperatingSystem)
                {
                    proc.StartInfo.FileName = "b64.bat";
                }
                else
                    proc.StartInfo.FileName = "b32.bat";
                proc.StartInfo.Arguments = string.Format("");//this is argument
                //proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
            Console.WriteLine("按任意键退出");
            Console.ReadKey(false);
        }
    }
}
