using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.IO.Ports;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Management;

namespace DCS硬件防火墙
{
    public partial class SerialPortSetForm : Office2007RibbonForm
    {
        private SerialPort _serialPort = new SerialPort();
        string[] ss;
        string[] ports;
        public SerialPortSetForm()
        {
            InitializeComponent();
        }

        private void SerialPortSetForm_Load(object sender, EventArgs e)
        {
            textBoxX1.Focus();
            UpdateSerialPorts();
        }

        public void UpdateSerialPorts()
        {
            //通过WMI获取COM端口
            ss = MulGetHardwareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            //初始化下拉串口名称列表框
            ports = SerialPort.GetPortNames();
            //Array.Sort(ports);
            
            comboBoxPortNames.Items.Clear();
            comboBoxPortNames.Items.AddRange(ports);
            comboBoxPortNames.SelectedIndex = comboBoxPortNames.Items.Count > 0 ? 0 : -1;
            comboBoxBaudrate.SelectedIndex = comboBoxBaudrate.Items.IndexOf(comboItem3);
            //combox  .Items.AddRange(ports);
            //serialSet.comboPortName.SelectedIndex = serialSet.comboPortName.Items.Count > 0 ? 0 : -1;
            //serialSet.comboBaudrate.SelectedIndex = serialSet.comboBaudrate.Items.IndexOf("9600");
            if (comboBoxPortNames.SelectedIndex>-1)
            labelX9.Text = "串口描述：" + ss[comboBoxPortNames.SelectedIndex];

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            UpdateSerialPorts();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void buttonX1_Click(object sender, EventArgs e)
        {

            if (!openSrialPort(ref _serialPort, comboBoxPortNames.Text, int.Parse(comboBoxBaudrate.Text)))
                return;
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("DCS001");
            var collection = database.GetCollection<Person>("User");
            var collectionLog = database.GetCollection<Log>("Log");

            var list = await collection.Find(x => x._UserName == textBoxX1.Text)
                .ToListAsync();
            int p1 = 0;
            foreach (var person in list)
            {
                p1 += 1;
                if (person._secret == textBoxX2.Text)
                {
                    p1 += 2;
                    if (person._identity == 0)
                        p1 += 4;
                    break;
                }
                //string a = person.Id.ToString();
                //ObjectId b =new ObjectId("a");
            }
            if (p1 == 3||p1==7)
            {
                DateTime dt = DateTime.Now;
                dt.ToUniversalTime().AddHours(-8);
                dt = dt.AddHours(8);
                await collectionLog.InsertOneAsync(new Log { operatorId = list[0]._id, operatorName = textBoxX1.Text, operationName = "登录", datetime = dt, lastModified = dt, remark = "成功了" });
                this.FindForm().Hide();
                bool admin = false;
                if(p1==7)
                    admin = true;
                MainForm dialog = new MainForm(list[0],_serialPort,switchButton1.Value);
                dialog.ShowDialog();//注意这里要模态显示对话框
                this.Close();
                //this.Visible = false;
            }
            else
            {

                    if (p1 == 1)
                    {
                        labelX5.Text = "";
                        labelX6.Text = "";
                        labelX7.Text = "用户名错误或者密码错误";
                    }
                    else
                    {
                        labelX5.Text = "";
                        labelX7.Text = "";
                        labelX6.Text = "用户名不存在";
                    }

            }



        }

        private void textBoxX_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
                buttonX1_Click(sender, e);
        }



        private bool openSrialPort(ref SerialPort comm,string portName , int Baudrate)
        {

            //根据当前串口对象，来判断操作
            if (comm.IsOpen)
            {
                MessageBox.Show("设备已经连接！");
                return false;
                //打开时点击，则关闭串口
                //pictureBoxport.Image = SerialportSample.Properties.Resources.LedOff;
            }
            else
            {
                if (comboBoxPortNames.Text == String.Empty)
                {
                    MessageBox.Show("端口号不能为空！");
                    return false;
                }
                //关闭时点击，则设置好端口，波特率后打开
                comm.PortName = portName;
                comm.BaudRate = Baudrate;
                try
                {
                    comm.Open();
                }
                catch (Exception ex)
                {
                    labelX5.Text = "设备连接失败";
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    comm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show(ex.Message);
                    return false;
                }
                comm.Close();
                
            }
            //设置按钮的状态
            //ruleViewToolStripMenuItem.Enabled = comm.IsOpen;
            //serialSend.buttonSend.Enabled = comm.IsOpen;
            //buttonuset.Enabled = comm.IsOpen;
            //button1.Enabled = comm.IsOpen;
            //button2.Enabled = comm.IsOpen;
            //if (comm.IsOpen)
            //{
            //    toolStripLabel1.Image = SerialportSample.Properties.Resources.LedOn;
            //    WriteLog("连接设备", "成功");
            //}投矛兵 +3点 对弓箭兵 精锐投矛兵 +4点 对弓箭兵 轻型中型重型冲撞车石 VS攻城器具+40  +50 +65

            return true;
        }


        /// <summary>
        /// 枚举win32 api
        /// </summary>
        public enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }
        /// <summary>
        /// WMI取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {

            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value != null)
                            if (hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
                            {
                                strs.Add(hardInfo.Properties[propKey].Value.ToString());
                            }

                    }
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            { strs = null; }
        }

        private void comboBoxPortNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPortNames.SelectedIndex > -1)
                labelX9.Text = "串口描述：" + ss[comboBoxPortNames.SelectedIndex];
        }
 
        
        }//end class SerialPortSetForm

    public class Person
    {
        public ObjectId _id { get; set; }
        public string _UserName { get; set; }
        public string _secret { get; set; }
        public string _name { get; set; }
        public string _department { get; set; }
        public string _IDcardNo { get; set; }
        public string _phoneNo { get; set; }
        public string _email { get; set; }
        public int _identity { get; set; }
    }

    public class Log
    {
        public ObjectId id { get; set; }
        public ObjectId operatorId { get; set; }
        public string operatorName { get; set; }
        public string operationName { get; set; }
        public string remark { get; set; }
        public DateTime datetime { get; set; }
        public DateTime lastModified { get; set; }
    }


   


    }








