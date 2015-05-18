using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InstallInstallMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Process p = Process.Start(@"D:\安全芯片组\硬件防火墙\DCS硬件防火墙v1.0\InstallMongoDB\bin\Release\InstallMongoDB.exe");
            p.WaitForExit();//关键，等待外部程序退出后才能往下执行
        }
    }
}
