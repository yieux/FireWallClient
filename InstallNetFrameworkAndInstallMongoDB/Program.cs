using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Management;

namespace InstallNetFrameworkAndInstallMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Version ver = System.Environment.OSVersion.Version;
            Console.WriteLine("major:" + ver.Major.ToString() + "  minor:" + ver.Minor.ToString()+" packMajor:"+ver.Build.ToString()+" a "+ver.Revision.ToString());
            Process proc = null;
            DoSomeHotfixSelect();
            try
            {
                string targetDir;
                targetDir = Process.GetCurrentProcess().MainModule.FileName;
                int end = targetDir.LastIndexOf(@"\");
                targetDir = targetDir.Substring(0, end);
                Console.WriteLine(targetDir);
                proc = new Process();
                //proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WorkingDirectory = targetDir+@"\net";
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.UseShellExecute = true;

                proc.StartInfo.FileName = "NDP451-KB2858728-x86-x64.3505182529.exe";
                proc.StartInfo.Arguments = string.Format("");//this is argument
                //proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();

                //Version ver = System.Environment.OSVersion.Version;
                //Console.WriteLine("major:" +ver.Major.ToString()+"  minor:"+ver.Minor.ToString());


                string targetDir2;
                targetDir2 = Process.GetCurrentProcess().MainModule.FileName;
                int end2 = targetDir.LastIndexOf(@"\");
                targetDir2 = targetDir2.Substring(0, end);
                Console.WriteLine(targetDir2);
                proc = new Process();
                //proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WorkingDirectory = targetDir + @"\MongoDB";
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.UseShellExecute = true;

                proc.StartInfo.FileName = "InstallMongoDB.exe";
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

        public bool IsSys64bit()
        {
            if (IntPtr.Size == 8)
                return true;
            else
                return false;
 
        }

        static public void DoSomeHotfixSelect()
        {
            var searchOS = new ManagementObjectSearcher("Select * from Win32_OperatingSystem");
            Console.Write("本机最新安装补丁信息：\n");
            foreach (var item in searchOS.Get())
            {
                foreach (var itemPro in item.Properties)
                {
                    Console.Write(itemPro.Name + ":" + itemPro.Value + " \n");

                }
                Console.Write(" \n");
            }
            Console.Write("\n");
        }
    }
}
