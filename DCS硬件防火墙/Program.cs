using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace DCS硬件防火墙
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Person a = new Person();
            //SerialPort _serialPort = new SerialPort();
            //Application.Run(new MainForm(a,_serialPort));
            Application.Run(new SerialPortSetForm());
        }
    }
}
