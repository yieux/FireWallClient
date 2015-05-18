using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;




namespace DCS硬件防火墙
{
    public partial class MainForm : Office2007RibbonForm
    {
        public MainForm(Person currectOperator,SerialPort sp,bool realorTest)
        {
            RealOrTest = realorTest;
            comm = new SerialPort(sp.PortName,sp.BaudRate);
            comm.NewLine = "\r\n";
            comm.RtsEnable = true;//根据实际情况吧。
            try
            {
                comm.Open();
                
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                comm = new SerialPort();
                //现实异常信息给客户。
                MessageBox.Show(ex.Message);
            }

            _currectOperator = currectOperator;
           
            InitializeComponent();
            InitializeRuleGroupPanel();
            InitFormData();
            
        }

        #region my Windows Form Designer generated code --
        /// <summary>
        /// 部分我们自己定义的控件
        /// </summary>
        private void InitializeRuleGroupPanel()
        {
            this.groupPanelOneRule = new DevComponents.DotNetBar.Controls.GroupPanel[31];
            this.buttonXOneRuleOneByte = new DevComponents.DotNetBar.ButtonX[31, 32];
            this.buttonXOneRuleClear = new DevComponents.DotNetBar.ButtonX[31];
            this.buttonXOneRuleCheck = new DevComponents.DotNetBar.ButtonX[31];
            //one rule
            for (int i = 1; i <31; i++)
            {
                //groupPanel of one rule
                this.groupPanelOneRule[i] = new DevComponents.DotNetBar.Controls.GroupPanel();

                this.groupPanelOneRule[i].BackColor = System.Drawing.Color.Transparent;
                this.groupPanelOneRule[i].CanvasColor = System.Drawing.SystemColors.Control;
                this.groupPanelOneRule[i].ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;

                this.groupPanelOneRule[i].DisabledBackColor = System.Drawing.Color.Empty;
                this.groupPanelOneRule[i].Dock = System.Windows.Forms.DockStyle.Top;
                this.groupPanelOneRule[i].Location = new System.Drawing.Point(0, 0);
                this.groupPanelOneRule[i].Name = "groupPanelOneRule[" + i.ToString() + "]";
                this.groupPanelOneRule[i].Size = new System.Drawing.Size(204, 80);
                // 
                // 
                // 
                this.groupPanelOneRule[i].Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
                this.groupPanelOneRule[i].Style.BackColorGradientAngle = 90;
                this.groupPanelOneRule[i].Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
                this.groupPanelOneRule[i].Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
                this.groupPanelOneRule[i].Style.BorderBottomWidth = 1;
                this.groupPanelOneRule[i].Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
                this.groupPanelOneRule[i].Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
                this.groupPanelOneRule[i].Style.BorderLeftWidth = 1;
                this.groupPanelOneRule[i].Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
                this.groupPanelOneRule[i].Style.BorderRightWidth = 1;
                this.groupPanelOneRule[i].Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
                this.groupPanelOneRule[i].Style.BorderTopWidth = 1;
                this.groupPanelOneRule[i].Style.CornerDiameter = 4;
                this.groupPanelOneRule[i].Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
                this.groupPanelOneRule[i].Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
                this.groupPanelOneRule[i].Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
                this.groupPanelOneRule[i].Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
                // 
                // 
                // 
                this.groupPanelOneRule[i].StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                // 
                // 
                // 
                this.groupPanelOneRule[i].StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                this.groupPanelOneRule[i].TabIndex = i;
                this.groupPanelOneRule[i].Text = "规则" + i.ToString();


                //byte button
                for (int j = 0; j < 32; j++)
                {
                    int row, column;
                    row = j / 8;
                    column = j - row * 8;
                    this.buttonXOneRuleOneByte[i, j] = new DevComponents.DotNetBar.ButtonX();
                    this.buttonXOneRuleOneByte[i, j].CausesValidation = false;
                    this.buttonXOneRuleOneByte[i, j].AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
                    this.buttonXOneRuleOneByte[i, j].BackColor = _colorInFree;
                    this.buttonXOneRuleOneByte[i, j].ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
                    this.buttonXOneRuleOneByte[i, j].Location = new System.Drawing.Point(3 + column * 13, 3 + row * 13);
                    this.buttonXOneRuleOneByte[i, j].Name = "buttonXOneRuleOneByte[" + i.ToString() + "," + j.ToString() + "]";
                    this.buttonXOneRuleOneByte[i, j].Size = new System.Drawing.Size(10, 10);
                    this.buttonXOneRuleOneByte[i, j].Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    this.buttonXOneRuleOneByte[i, j].TabIndex = j;
                    this.buttonXOneRuleOneByte[i, j].FocusCuesEnabled = false;
                    this.buttonXOneRuleOneByte[i, j].Click += new System.EventHandler(this.groupPanelRule_Click);
                    this.groupPanelOneRule[i].Controls.Add(this.buttonXOneRuleOneByte[i, j]);
                    ButtonXDoubleBuffered(ref buttonXOneRuleOneByte[i, j], true);

                }


                //clear button
                this.buttonXOneRuleClear[i] = new DevComponents.DotNetBar.ButtonX();
                this.buttonXOneRuleClear[i].AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
                this.buttonXOneRuleClear[i].BackColor = System.Drawing.Color.Transparent;
                this.buttonXOneRuleClear[i].ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
                this.buttonXOneRuleClear[i].Location = new System.Drawing.Point(135, 0);
                this.buttonXOneRuleClear[i].Name = "buttonXOneRuleClear[" + i.ToString() + "]";
                this.buttonXOneRuleClear[i].Size = new System.Drawing.Size(34, 25);
                this.buttonXOneRuleClear[i].Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                this.buttonXOneRuleClear[i].Symbol = "";
                this.buttonXOneRuleClear[i].TabIndex = 36;
                this.buttonXOneRuleClear[i].FocusCuesEnabled = false;
                this.groupPanelOneRule[i].Controls.Add(buttonXOneRuleClear[i]);
                this.buttonXOneRuleClear[i].Click += buttonXGroupPanelClearRule_Click;
                ButtonXDoubleBuffered(ref buttonXOneRuleClear[i], true);
                //check button 实际上用于发送
                this.buttonXOneRuleCheck[i] = new DevComponents.DotNetBar.ButtonX();
                this.buttonXOneRuleCheck[i].AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
                this.buttonXOneRuleCheck[i].BackColor = System.Drawing.Color.Transparent;
                this.buttonXOneRuleCheck[i].ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
                this.buttonXOneRuleCheck[i].Location = new System.Drawing.Point(135, 30);
                this.buttonXOneRuleCheck[i].Name = "buttonXOneRuleCheck["+i.ToString()+"]";
                this.buttonXOneRuleCheck[i].Size = new System.Drawing.Size(34, 25);
                this.buttonXOneRuleCheck[i].Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                this.buttonXOneRuleCheck[i].Symbol = "";
                this.buttonXOneRuleCheck[i].TabIndex = 37;
                this.buttonXOneRuleCheck[i].FocusCuesEnabled = false;
                this.groupPanelOneRule[i].Controls.Add(buttonXOneRuleCheck[i]);
                this.buttonXOneRuleCheck[i].Click += buttonXGroupPanelSendRule_Click;
                ButtonXDoubleBuffered(ref buttonXOneRuleCheck[i], true);


                this.groupPanelOneRule[i].Click += new System.EventHandler(this.groupPanelRule_Click);

                this.groupPanelAllRule.Controls.Add(this.groupPanelOneRule[i]);
                GroupPanelDoubleBuffered(ref groupPanelOneRule[i], true);
            }
            this.groupPanelAllRule.Controls.Add(this.buttonXAddRule);
            GroupPanelDoubleBuffered(ref groupPanelAllRule, true);



            //////RuleEditChooseByte groupPanel ButtonX[32]
            this.buttonXEditRuleChooseByte = new ButtonX[32];
            for (int i= 0; i < 32; i++)
            {
                int row, column;
                row = i / 16;
                column = i - row * 16;
                this.buttonXEditRuleChooseByte[i] = new DevComponents.DotNetBar.ButtonX();
                this.buttonXEditRuleChooseByte[i].CausesValidation = false;
                this.buttonXEditRuleChooseByte[i].AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
                this.buttonXEditRuleChooseByte[i].BackColor = _colorInFree;
                this.buttonXEditRuleChooseByte[i].ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
                this.buttonXEditRuleChooseByte[i].Location = new System.Drawing.Point(4 + column * 39, 4 + row * 39);
                this.buttonXEditRuleChooseByte[i].Name = "buttonXEditRuleChooseByte[" + i.ToString() + "]";
                this.buttonXEditRuleChooseByte[i].Text = i.ToString();
                this.buttonXEditRuleChooseByte[i].Size = new System.Drawing.Size(30, 30);
                this.buttonXEditRuleChooseByte[i].Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                this.buttonXEditRuleChooseByte[i].TabIndex = i;
                this.buttonXEditRuleChooseByte[i].Focus();
                this.buttonXEditRuleChooseByte[i].Click += ButtonXEditRuleChooseByte_Click;
                this.groupPanelEditRuleChooseByte.Controls.Add(buttonXEditRuleChooseByte[i]);
                ButtonXDoubleBuffered(ref buttonXEditRuleChooseByte[i], true);

            }
        }
        //DevComponents.DotNetBar.Controls.DataGridViewX
        public static void GroupPanelDoubleBuffered(ref DevComponents.DotNetBar.Controls.GroupPanel dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public static void ButtonXDoubleBuffered(ref DevComponents.DotNetBar.ButtonX dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        #endregion

        private void colorCombControl4_SelectedColorChanged(object sender, EventArgs e)
        {

                ((ButtonX)sender).Symbol = "";
                p = false;
                //ribbonControl1.SelectedRibbonTabItem ;
                ribbonControl1.SelectedRibbonTabItem = ribbonTabItem2;

        }


        #region variable
        public bool p = true;
        public Person _currectOperator;

        private SerialPort comm;// = new SerialPort();
        private StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private long received_count = 0;//接收计数
        private long send_count = 0;//发送计数

        System.Timers.Timer timer;
        System.Timers.Timer timer1;

        DevComponents.DotNetBar.ToolTip tooltip = new DevComponents.DotNetBar.ToolTip();//未使用

        public static string[, ,] uset = new string[31, 32, 3];//uset[,,0] 有时候是起始字节号 uset[,,1] 表示mask  uset[,,2] 表示value
        public string[, ,] uset_d = new string[31, 32, 3];//防火墙上的
        private int urule = 1;//用户当前操作的规则
        private int useq = 0;//用户当前操作的字节序号   uset[,,0] 有时候是起始字节号
        private int useqLength = 1;
        private int useqlayer = 0;// 0 mac 1 ip 2 tcp/udp 3/data
        private int[] led = new int[31];//主机端 该号规则是否存在
        private int[] led_d = new int[31];
        private int[] chk = new int[31];//防火墙该规则是否使能
        private int[] chk_d = new int[31];//防火墙的使能状态
        private string[] len = new string[2];
        private string[] len_d = new string[2];
        private bool _currentByteOrField = true;//描述当前编辑状态是选字节的专家模式还是选字段的用户模式
        private int[,] _fieldLength;//每个字段的长度统一

        private bool flag55 = false;
        string str55 = "";
        //private Mutex mut;
        Semaphore sem = new Semaphore(0, 1);

        Thread thdSub1;
        Thread thdSub2;
        Thread threadUserManagement;
        Thread threadLogManagement;
        SetLenghRuleForm setLengthRuleForm = new SetLenghRuleForm();

        /// <summary>
        /// when real that we can get data from device
        /// when test thta we make some data as from device
        /// </summary>
        bool RealOrTest = false;
        private Button[] btn = new Button[32];
        public int nbyte;
        //private SSet serialSet;//原连接串口
        //private ViewLog logview;//原原 查看日志
        //private Send serialSend;//原原发送窗口
        //private RuleView viewrule;//原 使能配置窗口
        //process bar
        //private loadBar myProcessBar = null;//原发送信息时使用的进度条
        private delegate bool IncreaseHandle(int nValue);
        private IncreaseHandle myIncrease = null;
        private bool NotBindingDeviceData = true;
        private bool TcpOrUdp = true;

        Semaphore semNotGettingDataFromDevice = new Semaphore(0, 1);
        private bool NotGettingDataFromDevice = true;

        public ObjectId _currectOperatorId;
        public MongoClient client;
        public IMongoDatabase database;
        public IMongoCollection<Person> collectionPerson;
        public IMongoCollection<Log> collectionLog;
        public string _currectOperatorName;
#endregion
        ///static
        public static Color _colorInFree = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
        public static Color _colorInSeted = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(128)))));

        /// <summary>
        /// for test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void groupPanelRule_Click(object sender, EventArgs e)
        {
            Control a = (Control)sender;
            string controlName = a.Name;
            int begin, end;
            if (controlName.Substring(0, 18) == "groupPanelOneRule[")
            {
                begin = 18;
                end = controlName.IndexOf("]", 18);
                nbyte = -1;
            }
            else
            {
                begin = 22;
                end = controlName.IndexOf(",",22);
                int end_b = controlName.IndexOf("]",end);
                nbyte = int.Parse(controlName.Substring(end + 1, end_b - end - 1));
                ChooseByteHappen();
            }

            urule = int.Parse(controlName.Substring(begin,end-begin));
            //MessageBox.Show(useq.ToString());
            UpdateSuperTabItemEditRule();
        }

        private void buttonXGroupPanelClearRule_Click(object sender, EventArgs e)
        {
            Control a = (Control)sender;
            string controlName = a.Name;
            int begin, end;
            begin = controlName.IndexOf("[",1);
            end = controlName.IndexOf("]", begin+1);
            urule = int.Parse(controlName.Substring(begin + 1, end-begin-1));

            ClearOneRule(urule);
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();
            UpdateRuleViewLocalRule();
        }
        private void buttonXGroupPanelSendRule_Click(object sender, EventArgs e)
        {
            Control a = (Control)sender;
            string controlName = a.Name; 
            int begin, end;
            begin = controlName.IndexOf("[", 1);
            end = controlName.IndexOf("]", begin + 1);
            urule = int.Parse(controlName.Substring(begin + 1, end - begin - 1));

            SendOneRule(urule);
            UpdateRuleViewDeviceRule();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// add rule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonXAddRule_Click(object sender, EventArgs e)
        {
            ///for test
            //this.buttonXEditRuleChooseByte[1].Focus();
            //this.buttonXEditRuleChooseByte[2].Focus();
            int i;
            for (i = 1; i < 31; i++)
                if (led[i] == 0)
                    break;
            if (i == 31)
            {
                MessageBox.Show("30条规则已满！\n请删除后再添加");
                return;
            }
            urule = i;
            useq = -1;
            ClearOneRule(i);
            led[i] = 1;
            chk[i] = 1;
            nbyte = -1;
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();


        }

        private void InitFormData()
        {
            for (int i = 1; i < 31; i++)
            {
                led[i] = 0;
                chk[i] = 0;
                for (int j = 0; j < 32; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        uset[i, j, k] = "";
                    }
                }
            }
            urule = 0;
            useq = -1;
            useqLength = 1;
            _currentByteOrField = true;
            buttonXEditRuleChooseFieldUDP2.Visible = false;
            buttonXEditRuleChooseFieldUDP3.Visible = false;
            
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();
            UpdatePersonInformation();
            UpdateSuperTabItemViewRule();
            labelX4.Text = _currectOperator._name;
            //初始化SerialPort对象
            comm.NewLine = "\r\n";
            comm.RtsEnable = true;//根据实际情况吧。

            //添加事件注册
            comm.DataReceived += comm_DataReceived;

            //madin 
            if (_currectOperator._identity != 0)
                ribbonTabItem5.Visible = false;
            else
                ribbonTabItem5.Visible = true;

            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("DCS001");
            collectionPerson = database.GetCollection<Person>("User");
            collectionLog = database.GetCollection<Log>("Log");
            //var list = await collectionPerson.Find(x => x.Id == _currectOperatorId).ToListAsync();
            _currectOperatorName =_currectOperator._name;
            _currectOperatorId = _currectOperator._id;
            //WriteLog("登录", "成功");
            len[0] = "";
            len[1] = "";

            gridColumnViewLocalRuleButtonSend.EditorType = typeof(MyGridButtonXEditControl);
            gridColumnViewLocalRuleButtonClear.EditorType = typeof(MyGridButtonXEditControl);
            gridColumnViewDeviceRuleButtonGet.EditorType = typeof(MyGridButtonXEditControl);
            //gridColumnViewDeviceRuleButtonEN.EditorType = typeof(MyGridButtonXEditControl);
            gridColumnViewDeviceRuleButtonDelete.EditorType = typeof(MyGridButtonXEditControl);
            gridColumnViewDeviceRuleButtonGetNew.EditorType = typeof(MyGridButtonXEditControl);
            ///device
            for (int i = 1; i < 31; i++)
            {
                chk_d[i] = 0;
                led_d[i] = 0;
                for (int j = 0; j < 32; j++)
                    for (int k = 0; k < 3; k++)
                        uset_d[i, j, k] = "";
            }
            len_d[0] = "";
            len_d[1] = "";

            if (!RealOrTest)
            {
                for (int i = 1; i < 3; i++)
                {
                    led_d[i] = 1;
                    chk_d[i] = ((i / 2) * 2 == i) ? 1 : 0;
                    for (int j = 0; j < 5 - i; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            uset_d[i, j, k] = "0"+(i + j).ToString();
                        }
                    }
                }
            }
            
        }

        private void UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte()
        {
            

            for (int i = 1; i < 31; i++)
            {
                if (led[i] == 0)
                    groupPanelOneRule[i].Visible = false;
                else
                    groupPanelOneRule[i].Visible = true;

                for (int j = 0; j < 32; j++)
                {
                    if (uset[i, j, 0] == "")
                        buttonXOneRuleOneByte[i, j].BackColor = _colorInFree;
                    else
                        buttonXOneRuleOneByte[i, j].BackColor = _colorInSeted;
                }
            }
        }

        private bool ClearOneRule(int ruleIndex)
        { 
            led[ruleIndex] = 0;
            chk[ruleIndex] = 0;
            for(int j=0;j<32;j++)
                for(int k=0;k<3;k++)
                    uset[ruleIndex,j,k] = "";
            return true;
        }


        private void UpdateSuperTabItemEditRule()
        {
            if (urule > 0 && urule < 31 && led[urule] == 1)
            {//superTabItemEditRule.Enabled = true;
                superTabControlPanelEditRule.Enabled = true;
                superTabControl2.SelectedTab = superTabItemEditRule;
                labelXEditRuleIndex.Text = "当前操作规则号： " + urule.ToString();
            }
            else
            {
                //superTabItemEditRule.Enabled = false;
                superTabControlPanelEditRule.Enabled = false;
                labelXEditRuleIndex.Text = "当前操作规则号： ";
                //superTabControl2.SelectedTab = superTabItemEditRule;
            }


            if (nbyte > -1 && nbyte < 32)
            {
                groupPanelEditRuleSet.Enabled = true;
                labelXEditByteIndex.Text = "当前操作字节编号： " + nbyte.ToString();
                textBoxXEditRuleSetByte.Text = uset[urule, nbyte, 0];
                textBoxXEditRuleSetMask.Text = uset[urule, nbyte, 1];
                textBoxXEditRuleSetValue.Text = uset[urule, nbyte, 1];
            }
            else
            {
                labelXEditByteIndex.Text = "当前操作字节编号： ";
                if (superTabControl1.SelectedTab == superTabItem4)
                    groupPanelEditRuleSet.Enabled = true;
                else
                    groupPanelEditRuleSet.Enabled = false;
            }
            //field
            buttonXEditRuleChooseFieldMACGoalMAC.BackColor = _colorInFree;
            buttonXEditRuleChooseFieldMACSourceMAC.BackColor = _colorInFree;
            buttonXEditRuleChooseFieldMACProtocol.BackColor = _colorInFree;
            for (int i = 0; i < 10;i++)
                superTabControlPanel2.Controls["buttonXEditRuleChooseFieldIP"+i.ToString()].BackColor = _colorInFree;
            for (int i = 0; i < 8; i++)
                superTabControlPanel3.Controls["buttonXEditRuleChooseFieldTCP" + i.ToString()].BackColor = _colorInFree;
            for (int i = 2; i < 4; i++)
                superTabControlPanel3.Controls["buttonXEditRuleChooseFieldUDP" + i.ToString()].BackColor = _colorInFree;
            switchButton1.Value = TcpOrUdp;
            //for test
            //urule = 1;
            //uset[1, 1, 0] = "1";
            listViewEx1.Items.Clear();
            if(urule>0&&urule<31)
            for (int i = 0; i < 32; i++)
            {
                if (uset[urule, i, 0] == "")
                    buttonXEditRuleChooseByte[i].BackColor = _colorInFree;
                else
                {
                    buttonXEditRuleChooseByte[i].BackColor = _colorInSeted;
                    int protocollayerNO = -1, fieldinprotocolNO =-1, fieldinprotocoloffset =-1;
                    AnalyzeFieldNOByForm1useq(int.Parse(uset[urule, i, 0]), TcpOrUdp, ref protocollayerNO,ref fieldinprotocolNO,ref fieldinprotocoloffset);
                    //string protocollayer = "", fieldinprotocol = "";
                    SuperTabControlPanel s = superTabControlPanel1;
                    ButtonX b = new ButtonX();
                    AnalyzeFiledControlNameByFieldNO(protocollayerNO, fieldinprotocolNO, ref s, ref b);

                    
                    if (TcpOrUdp)
                    {
                        if (int.Parse(uset[urule, i, 0]) > 53)
                        {
                            ListViewItem listrecord = new ListViewItem(i.ToString());//这个是第一行第一列
                            listrecord.SubItems.Add(uset[urule, i, 0]);//第一行第二列
                            listrecord.SubItems.Add(uset[urule, i, 2]);//第一行第二列
                            listrecord.SubItems.Add(uset[urule, i, 1]);//第一行第三列
                            listViewEx1.Items.Add(listrecord);//把第一行添加上
                        }
                        else
                            b.BackColor = _colorInSeted;
                    }
                    else
                    {
                        if (int.Parse(uset[urule, i, 0]) > 41)
                        {
                            ListViewItem listrecord = new ListViewItem(i.ToString());//这个是第一行第一列
                            listrecord.SubItems.Add(uset[urule, i, 0]);//第一行第二列
                            listrecord.SubItems.Add(uset[urule, i, 2]);//第一行第二列
                            listrecord.SubItems.Add(uset[urule, i, 1]);//第一行第三列
                            listViewEx1.Items.Add(listrecord);//把第一行添加上
                        }
                        else
                            b.BackColor = _colorInSeted;
                    }
                }
            }

        }

        private void ButtonXEditRuleChooseByte_Click(object sender, EventArgs e)
        {
            _currentByteOrField = true;
            ButtonX btx = (ButtonX)sender;
            groupPanelEditRuleSetByteAndValueAndMaskLabelTextByByteChoose();
            groupPanelEditRuleSet.Enabled = true;
            textBoxXEditRuleSetByte.ReadOnly = false;
            
            nbyte = int.Parse(btx.Text);
            ChooseByteHappen();
        }

        private void ChooseByteHappen()
        {
            useqLength = 1;
            if (nbyte > -1 && nbyte < 32)
                labelXEditByteIndex.Text = "当前操作字节编号：" + nbyte;
            if (uset[urule, nbyte, 0] != "")
            {
                textBoxXEditRuleSetByte.Text = uset[urule, nbyte, 0];
                useq = int.Parse(uset[urule, nbyte, 0]);
                textBoxXEditRuleSetMask.Text = uset[urule, nbyte, 1];
                textBoxXEditRuleSetValue.Text = uset[urule, nbyte, 2];
            }
            else
            {
                useq = -1;
                textBoxXEditRuleSetByte.Text = "";
                textBoxXEditRuleSetMask.Text = "";
                textBoxXEditRuleSetValue.Text = "";
            }

            ///联动 superTabControl1 selectedTabItem
            if (useq > -1)
            {
                int a = -1, b = -1, c = -1;
                AnalyzeFieldNOByForm1useq(useq, TcpOrUdp, ref a, ref b, ref c);
                switch (a)
                {
                    case 0:
                        superTabControl1.SelectedTab = superTabItem1;
                        break;
                    case 1:
                        superTabControl1.SelectedTab = superTabItem2;
                        break;
                    case 2:
                        superTabControl1.SelectedTab = superTabItem3;
                        break;
                    case 3:
                        superTabControl1.SelectedTab = superTabItem3;
                        break;
                    case 4:
                        superTabControl1.SelectedTab = superTabItem4;
                        break;
                    case 5:
                        superTabControl1.SelectedTab = superTabItem4;
                        break;
                }

            }
        }



        public static void AnalyzeFieldNOByForm1useq(int useq, bool tcporudp, ref int protocollayerNO, ref int fieldinprotocolNO, ref int fieldinprotocoloffset)
        {
            //int protocollayerNO, fieldinprotocolNO, useqoffset, protocoloffset, fieldoffset;
            int useqoffset, protocoloffset, fieldoffset;
            useqoffset = useq;
            if (useq < 14 && useq >= 0)
            {
                //mac header
                protocoloffset = 0;
                protocollayerNO = 0;
                useqoffset -= protocoloffset;
                if (useqoffset < 6)
                {
                    fieldinprotocolNO = 0;
                    fieldoffset = 0;
                    fieldinprotocoloffset = useqoffset - fieldoffset;
                }
                else
                {
                    if (useqoffset < 12)
                    {
                        fieldinprotocolNO = 1;
                        fieldoffset = 6;
                        fieldinprotocoloffset = useqoffset - fieldoffset;
                    }
                    else
                    {
                        fieldinprotocolNO = 2;
                        fieldoffset = 12;
                        fieldinprotocoloffset = useqoffset - fieldoffset;
                    }
                }

            }// end if mac header
            else
            {
                if (useq > 13 && useq < 34)
                {
                    //ip header
                    protocollayerNO = 1;
                    protocoloffset = 14;
                    useqoffset -= protocoloffset;
                    if (useqoffset == 0)
                    {
                        fieldinprotocolNO = 0;
                        fieldoffset = 0;
                        fieldinprotocoloffset = useqoffset - fieldoffset;
                    }
                    else
                    {
                        if (useqoffset == 1)
                        {
                            fieldinprotocolNO = 1;
                            fieldoffset = 1;
                            fieldinprotocoloffset = useqoffset - fieldoffset;
                        }
                        else
                        {
                            if (useqoffset < 4)
                            {
                                fieldinprotocolNO = 2;
                                fieldoffset = 2;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }
                            else
                            {
                                if (useqoffset < 6)
                                {
                                    fieldinprotocolNO = 3;
                                    fieldoffset = 4;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    if (useqoffset < 8)
                                    {
                                        fieldinprotocolNO = 4;
                                        fieldoffset = 6;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                    else
                                    {
                                        if (useqoffset == 8)
                                        {
                                            fieldinprotocolNO = 5;
                                            fieldoffset = 8;
                                            fieldinprotocoloffset = useqoffset - fieldoffset;
                                        }
                                        else
                                        {
                                            if (useqoffset == 9)
                                            {
                                                fieldinprotocolNO = 6;
                                                fieldoffset = 9;
                                                fieldinprotocoloffset = useqoffset - fieldoffset;
                                            }
                                            else
                                            {
                                                if (useqoffset < 12)
                                                {
                                                    fieldinprotocolNO = 7;
                                                    fieldoffset = 10;
                                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                                }
                                                else
                                                {
                                                    if (useqoffset < 16)
                                                    {
                                                        fieldinprotocolNO = 8;
                                                        fieldoffset = 12;
                                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                                    }
                                                    else
                                                    {
                                                        fieldinprotocolNO = 9;
                                                        fieldoffset = 16;
                                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }//end if ip header
                else
                {
                    if (tcporudp && useq > 33 && useq < 54)
                    {
                        //tcp header
                        protocollayerNO = 2;
                        protocoloffset = 34;
                        useqoffset -= protocoloffset;
                        if (useqoffset < 2)
                        {
                            fieldinprotocolNO = 0;
                            fieldoffset = 0;
                            fieldinprotocoloffset = useqoffset - fieldoffset;
                        }
                        else
                        {
                            if (useqoffset < 4)
                            {
                                fieldinprotocolNO = 1;
                                fieldoffset = 2;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }
                            else
                            {
                                if (useqoffset < 8)
                                {
                                    fieldinprotocolNO = 2;
                                    fieldoffset = 4;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    if (useqoffset < 12)
                                    {
                                        fieldinprotocolNO = 3;
                                        fieldoffset = 8;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                    else
                                    {
                                        if (useqoffset < 14)
                                        {
                                            fieldinprotocolNO = 4;
                                            fieldoffset = 12;
                                            fieldinprotocoloffset = useqoffset - fieldoffset;
                                        }
                                        else
                                        {
                                            if (useqoffset < 16)
                                            {
                                                fieldinprotocolNO = 5;
                                                fieldoffset = 14;
                                                fieldinprotocoloffset = useqoffset - fieldoffset;
                                            }
                                            else
                                            {
                                                if (useqoffset < 18)
                                                {
                                                    fieldinprotocolNO = 6;
                                                    fieldoffset = 16;
                                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                                }
                                                else
                                                {
                                                    fieldinprotocolNO = 7;
                                                    fieldoffset = 18;
                                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!tcporudp && useq > 33 && useq < 42)
                        {
                            //udp header
                            protocollayerNO = 3;
                            protocoloffset = 34;
                            useqoffset -= protocoloffset;

                            if (useqoffset < 2)
                            {
                                fieldinprotocolNO = 0;
                                fieldoffset = 0;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }
                            else
                            {
                                if (useqoffset < 4)
                                {
                                    fieldinprotocolNO = 1;
                                    fieldoffset = 2;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    if (useqoffset < 6)
                                    {
                                        fieldinprotocolNO = 2;
                                        fieldoffset = 4;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                    else
                                    {
                                        fieldinprotocolNO = 3;
                                        fieldoffset = 6;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (tcporudp && useq > 53 && useq < 2047)
                            {
                                //tcp data
                                protocollayerNO = 4;
                                protocoloffset = 54;
                                useqoffset -= protocoloffset;
                                fieldinprotocolNO = 0;
                                fieldoffset = 0;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }

                            else
                            {
                                //udp data
                                if (!tcporudp && useq > 41 && useq < 2047)
                                {
                                    protocollayerNO = 5;
                                    protocoloffset = 42;
                                    useqoffset -= protocoloffset;
                                    fieldinprotocolNO = 0;
                                    fieldoffset = 0;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    //error
                                    protocollayerNO = -1;
                                    fieldinprotocolNO = -1;
                                }
                            }
                        }

                    }
                }
            }//end analyze

            

        }

        private void AnalyzeFiledNameByFieldNO(int protocollayerNO, int fieldinprotocolNO, ref string protocollayer, ref string fieldinprotocol)
        {
            switch (protocollayerNO)
            {
                case 0:
                    protocollayer = "MAC协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "目的MAC";
                            break;
                        case 1:
                            fieldinprotocol = "源MAC";
                            break;
                        case 2:
                            fieldinprotocol = "协议";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 1:
                    protocollayer = "IP协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "版本和首部长度";
                            break;
                        case 1:
                            fieldinprotocol = "区分服务";
                            break;
                        case 2:
                            fieldinprotocol = "总长度";
                            break;
                        case 3:
                            fieldinprotocol = "标识";
                            break;
                        case 4:
                            fieldinprotocol = "标志和片偏移";
                            break;
                        case 5:
                            fieldinprotocol = "生存时间";
                            break;
                        case 6:
                            fieldinprotocol = "协议";
                            break;
                        case 7:
                            fieldinprotocol = "首部校验和";
                            break;
                        case 8:
                            fieldinprotocol = "源地址";
                            break;
                        case 9:
                            fieldinprotocol = "目的地址";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 2:
                    protocollayer = "TCP协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "源端口号";
                            break;
                        case 1:
                            fieldinprotocol = "目的端口号";
                            break;
                        case 2:
                            fieldinprotocol = "顺序号";
                            break;
                        case 3:
                            fieldinprotocol = "确认号";
                            break;
                        case 4:
                            fieldinprotocol = "头部长度及保留位控制位";
                            break;
                        case 5:
                            fieldinprotocol = "窗口大小";
                            break;
                        case 6:
                            fieldinprotocol = "校验和";
                            break;
                        case 7:
                            fieldinprotocol = "紧急指针";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 3:
                    protocollayer = "UDP协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "源端口号";
                            break;
                        case 1:
                            fieldinprotocol = "目的端口号";
                            break;
                        case 2:
                            fieldinprotocol = "长度";
                            break;
                        case 3:
                            fieldinprotocol = "校验和";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 4:
                    protocollayer = "TCP数据";
                    fieldinprotocol = "部分前段数据";
                    break;
                case 5:
                    protocollayer = "UDP数据";
                    fieldinprotocol = "部分前段数据";
                    break;
                default:
                    protocollayer = "";
                    break;
            }
        }

        private void AnalyzeFiledControlNameByFieldNO(int protocollayerNO, int fieldinprotocolNO, ref SuperTabControlPanel protocollayerControl, ref ButtonX fieldinprotocolControl)
        {
            switch (protocollayerNO)
            {
                    //本来case1以后可以使用类似于 superTabControlPanel2.Controls[".."+fieldinprotocolNO.tostring()]操作的，但是为了历史命名残留原因，保存代码一致性，故如下，没时间了啊
                case 0:
                    protocollayerControl = superTabControlPanel1;
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldMACGoalMAC;
                            break;
                        case 1:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldMACSourceMAC;
                            break;
                        case 2:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldMACProtocol;
                            break;
                        default:
                            
                            break;
                    }
                    break;
                case 1:
                    protocollayerControl = superTabControlPanel2;
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP0;
                            break;
                        case 1:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP1;
                            break;
                        case 2:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP2;
                            break;
                        case 3:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP3;
                            break;
                        case 4:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP4;
                            break;
                        case 5:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP5;
                            break;
                        case 6:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP6;
                            break;
                        case 7:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP7;
                            break;
                        case 8:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP8;
                            break;
                        case 9:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldIP9;
                            break;
                        default:
                            //fieldinprotocolControl = "";
                            break;
                    }
                    break;
                case 2:
                    protocollayerControl = superTabControlPanel3;
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP0;
                            break;
                        case 1:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP1;
                            break;
                        case 2:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP2;
                            break;
                        case 3:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP3;
                            break;
                        case 4:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP4;
                            break;
                        case 5:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP5;
                            break;
                        case 6:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP6;
                            break;
                        case 7:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP7;
                            break;
                        default:
                            //fieldinprotocolControl = "";
                            break;
                    }
                    break;
                case 3:
                    protocollayerControl = superTabControlPanel3;
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP0;
                            break;
                        case 1:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldTCP1;
                            break;
                        case 2:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldUDP2;
                            break;
                        case 3:
                            fieldinprotocolControl = buttonXEditRuleChooseFieldUDP3;
                            break;
                        default:
                            //fieldinprotocolControl = "";
                            break;
                    }
                    break;
                case 4:
                    protocollayerControl = superTabControlPanel4;
                    fieldinprotocolControl = new ButtonX();
                    break;
                case 5:
                    protocollayerControl = superTabControlPanel4;
                    fieldinprotocolControl = new ButtonX();
                    break;
                default:
                    //protocollayerControl = "";
                    break;
            }
        }

        public static void AnalyzeFieldNameByForm1useq(int useq, bool tcporudp, ref string protocollayer, ref string fieldinprotocol, ref int fieldinprotocoloffset)
        {
            int protocollayerNO, fieldinprotocolNO, useqoffset, protocoloffset, fieldoffset;
            useqoffset = useq;
            if (useq < 14 && useq >= 0)
            {
                //mac header
                protocoloffset = 0;
                protocollayerNO = 0;
                useqoffset -= protocoloffset;
                if (useqoffset < 6)
                {
                    fieldinprotocolNO = 0;
                    fieldoffset = 0;
                    fieldinprotocoloffset = useqoffset - fieldoffset;
                }
                else
                {
                    if (useqoffset < 12)
                    {
                        fieldinprotocolNO = 1;
                        fieldoffset = 6;
                        fieldinprotocoloffset = useqoffset - fieldoffset;
                    }
                    else
                    {
                        fieldinprotocolNO = 2;
                        fieldoffset = 12;
                        fieldinprotocoloffset = useqoffset - fieldoffset;
                    }
                }

            }// end if mac header
            else
            {
                if (useq > 13 && useq < 34)
                {
                    //ip header
                    protocollayerNO = 1;
                    protocoloffset = 14;
                    useqoffset -= protocoloffset;
                    if (useqoffset == 0)
                    {
                        fieldinprotocolNO = 0;
                        fieldoffset = 0;
                        fieldinprotocoloffset = useqoffset - fieldoffset;
                    }
                    else
                    {
                        if (useqoffset == 1)
                        {
                            fieldinprotocolNO = 1;
                            fieldoffset = 1;
                            fieldinprotocoloffset = useqoffset - fieldoffset;
                        }
                        else
                        {
                            if (useqoffset < 4)
                            {
                                fieldinprotocolNO = 2;
                                fieldoffset = 2;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }
                            else
                            {
                                if (useqoffset < 6)
                                {
                                    fieldinprotocolNO = 3;
                                    fieldoffset = 4;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    if (useqoffset < 8)
                                    {
                                        fieldinprotocolNO = 4;
                                        fieldoffset = 6;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                    else
                                    {
                                        if (useqoffset == 8)
                                        {
                                            fieldinprotocolNO = 5;
                                            fieldoffset = 8;
                                            fieldinprotocoloffset = useqoffset - fieldoffset;
                                        }
                                        else
                                        {
                                            if (useqoffset == 9)
                                            {
                                                fieldinprotocolNO = 6;
                                                fieldoffset = 9;
                                                fieldinprotocoloffset = useqoffset - fieldoffset;
                                            }
                                            else
                                            {
                                                if (useqoffset < 12)
                                                {
                                                    fieldinprotocolNO = 7;
                                                    fieldoffset = 10;
                                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                                }
                                                else
                                                {
                                                    if (useqoffset < 16)
                                                    {
                                                        fieldinprotocolNO = 8;
                                                        fieldoffset = 12;
                                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                                    }
                                                    else
                                                    {
                                                        fieldinprotocolNO = 9;
                                                        fieldoffset = 16;
                                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }//end if ip header
                else
                {
                    if (tcporudp && useq > 33 && useq < 54)
                    {
                        //tcp header
                        protocollayerNO = 2;
                        protocoloffset = 34;
                        useqoffset -= protocoloffset;
                        if (useqoffset < 2)
                        {
                            fieldinprotocolNO = 0;
                            fieldoffset = 0;
                            fieldinprotocoloffset = useqoffset - fieldoffset;
                        }
                        else
                        {
                            if (useqoffset < 4)
                            {
                                fieldinprotocolNO = 1;
                                fieldoffset = 2;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }
                            else
                            {
                                if (useqoffset < 8)
                                {
                                    fieldinprotocolNO = 2;
                                    fieldoffset = 4;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    if (useqoffset < 12)
                                    {
                                        fieldinprotocolNO = 3;
                                        fieldoffset = 8;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                    else
                                    {
                                        if (useqoffset < 14)
                                        {
                                            fieldinprotocolNO = 4;
                                            fieldoffset = 12;
                                            fieldinprotocoloffset = useqoffset - fieldoffset;
                                        }
                                        else
                                        {
                                            if (useqoffset < 16)
                                            {
                                                fieldinprotocolNO = 5;
                                                fieldoffset = 14;
                                                fieldinprotocoloffset = useqoffset - fieldoffset;
                                            }
                                            else
                                            {
                                                if (useqoffset < 18)
                                                {
                                                    fieldinprotocolNO = 6;
                                                    fieldoffset = 16;
                                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                                }
                                                else
                                                {
                                                    fieldinprotocolNO = 7;
                                                    fieldoffset = 18;
                                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!tcporudp && useq > 33 && useq < 42)
                        {
                            //udp header
                            protocollayerNO = 3;
                            protocoloffset = 34;
                            useqoffset -= protocoloffset;

                            if (useqoffset < 2)
                            {
                                fieldinprotocolNO = 0;
                                fieldoffset = 0;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }
                            else
                            {
                                if (useqoffset < 4)
                                {
                                    fieldinprotocolNO = 1;
                                    fieldoffset = 2;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    if (useqoffset < 6)
                                    {
                                        fieldinprotocolNO = 2;
                                        fieldoffset = 4;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                    else
                                    {
                                        fieldinprotocolNO = 3;
                                        fieldoffset = 6;
                                        fieldinprotocoloffset = useqoffset - fieldoffset;
                                    }
                                }
                            }
                        }

                        else
                        {
                            if (tcporudp && useq > 53 && useq < 2047)
                            {
                                //tcp data
                                protocollayerNO = 4;
                                protocoloffset = 54;
                                useqoffset -= protocoloffset;
                                fieldinprotocolNO = 0;
                                fieldoffset = 0;
                                fieldinprotocoloffset = useqoffset - fieldoffset;
                            }

                            else
                            {
                                //udp data
                                if (!tcporudp && useq > 41 && useq < 2047)
                                {
                                    protocollayerNO = 5;
                                    protocoloffset = 42;
                                    useqoffset -= protocoloffset;
                                    fieldinprotocolNO = 0;
                                    fieldoffset = 0;
                                    fieldinprotocoloffset = useqoffset - fieldoffset;
                                }
                                else
                                {
                                    //error
                                    protocollayerNO = -1;
                                    fieldinprotocolNO = -1;
                                }
                            }
                        }

                    }
                }
            }//end analyze

            switch (protocollayerNO)
            {
                case 0:
                    protocollayer = "MAC协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "目的MAC";
                            break;
                        case 1:
                            fieldinprotocol = "源MAC";
                            break;
                        case 2:
                            fieldinprotocol = "协议";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 1:
                    protocollayer = "IP协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "版本和首部长度";
                            break;
                        case 1:
                            fieldinprotocol = "区分服务";
                            break;
                        case 2:
                            fieldinprotocol = "总长度";
                            break;
                        case 3:
                            fieldinprotocol = "标识";
                            break;
                        case 4:
                            fieldinprotocol = "标志和片偏移";
                            break;
                        case 5:
                            fieldinprotocol = "生存时间";
                            break;
                        case 6:
                            fieldinprotocol = "协议";
                            break;
                        case 7:
                            fieldinprotocol = "首部校验和";
                            break;
                        case 8:
                            fieldinprotocol = "源地址";
                            break;
                        case 9:
                            fieldinprotocol = "目的地址";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 2:
                    protocollayer = "TCP协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "源端口号";
                            break;
                        case 1:
                            fieldinprotocol = "目的端口号";
                            break;
                        case 2:
                            fieldinprotocol = "顺序号";
                            break;
                        case 3:
                            fieldinprotocol = "确认号";
                            break;
                        case 4:
                            fieldinprotocol = "头部长度及保留位控制位";
                            break;
                        case 5:
                            fieldinprotocol = "窗口大小";
                            break;
                        case 6:
                            fieldinprotocol = "校验和";
                            break;
                        case 7:
                            fieldinprotocol = "紧急指针";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 3:
                    protocollayer = "UDP协议头";
                    switch (fieldinprotocolNO)
                    {
                        case 0:
                            fieldinprotocol = "源端口号";
                            break;
                        case 1:
                            fieldinprotocol = "目的端口号";
                            break;
                        case 2:
                            fieldinprotocol = "长度";
                            break;
                        case 3:
                            fieldinprotocol = "校验和";
                            break;
                        default:
                            fieldinprotocol = "";
                            break;
                    }
                    break;
                case 4:
                    protocollayer = "TCP数据";
                    fieldinprotocol = "部分前段数据";
                    break;
                case 5:
                    protocollayer = "UDP数据";
                    fieldinprotocol = "部分前段数据";
                    break;
                default:
                    protocollayer = "";
                    break;
            }



        }

        private void switchButton1_ValueChanged(object sender, EventArgs e)
        {
            TcpOrUdp = switchButton1.Value;
            if (switchButton1.Value)
            {
                buttonXEditRuleChooseFieldTCP0.Visible = true;
                buttonXEditRuleChooseFieldTCP1.Visible = true;
                buttonXEditRuleChooseFieldTCP2.Visible = true;
                buttonXEditRuleChooseFieldTCP3.Visible = true;
                buttonXEditRuleChooseFieldTCP4.Visible = true;
                buttonXEditRuleChooseFieldTCP5.Visible = true;
                buttonXEditRuleChooseFieldTCP6.Visible = true;
                buttonXEditRuleChooseFieldTCP7.Visible = true;
                buttonXEditRuleChooseFieldUDP2.Visible = false;
                buttonXEditRuleChooseFieldUDP3.Visible = false;
            }
            else
            {
                buttonXEditRuleChooseFieldTCP0.Visible = true;
                buttonXEditRuleChooseFieldTCP1.Visible = true;
                buttonXEditRuleChooseFieldTCP2.Visible = false;
                buttonXEditRuleChooseFieldTCP3.Visible = false;
                buttonXEditRuleChooseFieldTCP4.Visible = false;
                buttonXEditRuleChooseFieldTCP5.Visible = false;
                buttonXEditRuleChooseFieldTCP6.Visible = false;
                buttonXEditRuleChooseFieldTCP7.Visible = false;
                buttonXEditRuleChooseFieldUDP2.Visible = true;
                buttonXEditRuleChooseFieldUDP3.Visible = true;
            }

            for (int i = 0; i < 32; i++)
            {
                if (uset[urule, i, 0] != "")
                {
                    if (int.Parse(uset[urule, i, 0]) >= 34)
                    {
                        uset[urule, i, 0] = "";
                        uset[urule, i, 1] = "";
                        uset[urule, i, 2] = "";
                    }
                }
            }
        }

        private void macField_Click(object sender, EventArgs e)
        {
            ButtonX btx = (ButtonX)sender;
            groupPanelEditRuleSet.Enabled = true;
            textBoxXEditRuleSetByte.ReadOnly = true;
            _currentByteOrField = false;
            useqlayer = 0;
            labelXEditByteIndex.Text = "当前操作字节编号：";
            switch (btx.Text)
            {
                case "目的MAC":
                    labelXEditRuleSetValue.Text = "目的MAC(H6B)";
                    labelXEditRuleSetMask.Text = "Mask (H6B)";
                    useq = 0;
                    useqLength = 6;
                    updatemac(useq, 6);
                    break;
                case "源MAC":
                    labelXEditRuleSetValue.Text = "源MAC(H6B)";
                    labelXEditRuleSetMask.Text = "Mask (H6B)";
                    useq = 6;
                    useqLength = 6;
                    updatemac(useq, 6);
                    break;

                case "协议":
                    labelXEditRuleSetValue.Text = "协议(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 12;
                    useqLength = 2;
                    updatemac(useq, 2);
                    break;
            }
            if (useqLength > 1)
                textBoxXEditRuleSetByte.Text = useq.ToString() + "~" + (useq + useqLength - 1).ToString();
            else
                textBoxXEditRuleSetByte.Text = useq.ToString();
        }

        private void updatemac(int seq, int len)
        {
            string seqstr = "";
            seqstr = findseqstr(seq, len);
            if (seqstr != "")
            {
                string mstr = seqstr.Substring(0, len * 2);
                string rstr = seqstr.Substring(len * 2, len * 2);
                textBoxXEditRuleSetMask.Text = mstr;
                textBoxXEditRuleSetValue.Text = rstr;
            }
            else
            {
                textBoxXEditRuleSetMask.Text = "";
                textBoxXEditRuleSetValue.Text = "";
            }
        }

        private void ipField_Click(object sender, EventArgs e)
        {
            ButtonX btx = (ButtonX)sender;
            string seqstr = "";
            groupPanelEditRuleSet.Enabled = true;
            textBoxXEditRuleSetByte.ReadOnly = true;
            _currentByteOrField = false;
            useqlayer = 1;
            labelXEditByteIndex.Text = "当前操作字节编号：";
            switch (btx.Text)
            {
                case "版本和首部长度":

                    labelXEditRuleSetValue.Text = "版本首部长(H1B)";
                    labelXEditRuleSetMask.Text = "Mask (H1B)";
                    useq = 14;
                    useqLength = 1;
                    ipupdate(useq, 1);
                    break;
                case "区分服务":
                    labelXEditRuleSetValue.Text = "区分服务(H1B)";
                    labelXEditRuleSetMask.Text = "Mask (H1B)";
                    useq = 15;
                    useqLength = 1;
                    ipupdate(useq, 1);
                    break;
                case "总长度":
                    labelXEditRuleSetValue.Text = "总长度(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 16;
                    useqLength = 2;
                    ipupdate(useq, 2);
                    break;
                case "标识":
                    labelXEditRuleSetValue.Text = "标识(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 18;
                    useqLength = 2;
                    ipupdate(useq, 2);
                    break;
                case "标志和片偏移":
                    labelXEditRuleSetValue.Text = "标志和片偏移(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 20;
                    useqLength = 2;
                    ipupdate(useq, 2);
                    break;
                case "生存时间":
                    labelXEditRuleSetValue.Text = "生存时间(H1B)";
                    labelXEditRuleSetMask.Text = "Mask (H1B)";
                    useq = 22;
                    useqLength = 1;
                    ipupdate(useq, 1);
                    break;
                case "协议":
                    labelXEditRuleSetValue.Text = "协议(H1B)";
                    labelXEditRuleSetMask.Text = "Mask (H1B)";
                    useq = 23;
                    useqLength = 1;
                    ipupdate(useq, 1);
                    break;
                case "首部校检和":
                    labelXEditRuleSetValue.Text = "首部校检和(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 24;
                    useqLength = 2;
                    ipupdate(useq, 2);
                    break;
                case "源地址":
                    labelXEditRuleSetValue.Text = "源地址(D4B)";
                    labelXEditRuleSetMask.Text = "Mask (D4B)";
                    useq = 26;
                    seqstr = "";
                    useqLength = 4;
                    seqstr = findseqstr(useq, 4);
                    if (seqstr != "")
                    {
                        string mstr = seqstr.Substring(0, 8);
                        string rstr = seqstr.Substring(8, 8);
                        string ipm = "";
                        string ipv = "";
                        for (int i = 0; i < 4; i++)
                        {
                            string dots = ".";
                            if (i == 3)
                            {
                                dots = "";
                            }
                            ipm += (Convert.ToInt32(mstr.Substring(i * 2, 2), 16)).ToString() + dots;
                            ipv += (Convert.ToInt32(rstr.Substring(i * 2, 2), 16)).ToString() + dots;
                        }
                        textBoxXEditRuleSetMask.Text = ipm;
                        textBoxXEditRuleSetValue.Text = ipv;
                    }
                    else
                    {
                        textBoxXEditRuleSetMask.Text = "";
                        textBoxXEditRuleSetValue.Text = "";
                    }
                    break;
                case "目的地址":
                    labelXEditRuleSetValue.Text = "目的地址(D4B)";
                    labelXEditRuleSetMask.Text = "Mask (D4B)";
                    useq = 30;
                    useqLength = 4;
                    seqstr = "";
                    seqstr = findseqstr(useq, 4);
                    //MessageBox.Show(seqstr);
                    if (seqstr != "")
                    {
                        string mstr = seqstr.Substring(0, 8);
                        string rstr = seqstr.Substring(8, 8);
                        string ipm = "";
                        string ipv = "";
                        for (int i = 0; i < 4; i++)
                        {
                            string dots = ".";
                            if (i == 3)
                            {
                                dots = "";
                            }
                            ipm += (Convert.ToInt32(mstr.Substring(i * 2, 2), 16)).ToString() + dots;
                            ipv += (Convert.ToInt32(rstr.Substring(i * 2, 2), 16)).ToString() + dots;
                        }
                        textBoxXEditRuleSetMask.Text = ipm;
                        textBoxXEditRuleSetValue.Text = ipv;
                    }
                    else
                    {
                        textBoxXEditRuleSetMask.Text = "";
                        textBoxXEditRuleSetValue.Text = "";
                    }
                    break;

            }

            if (useqLength > 1)
                textBoxXEditRuleSetByte.Text = useq.ToString() + "~" + (useq + useqLength - 1).ToString();
            else
                textBoxXEditRuleSetByte.Text = useq.ToString();
        }

        private void ipupdate(int seq, int len)
        {
            string seqstr = "";
            seqstr = findseqstr(seq, len);
            if (seqstr != "")
            {
                string mstr = seqstr.Substring(0, len * 2);
                string rstr = seqstr.Substring(len * 2, len * 2);
                textBoxXEditRuleSetMask.Text = mstr;
                textBoxXEditRuleSetValue.Text = rstr;
            }
            else
            {
                textBoxXEditRuleSetMask.Text = "";
                textBoxXEditRuleSetValue.Text = "";
            }
        }


        private void tcpField_Click(object sender, EventArgs e)
        {
            ButtonX btx = (ButtonX)sender;
            groupPanelEditRuleSet.Enabled = true;
            textBoxXEditRuleSetByte.ReadOnly = true;
            _currentByteOrField = false;
            useqlayer = 2;
            labelXEditByteIndex.Text = "当前操作字节编号：";
            switch (btx.Text)
            {
                case "源端口号":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "源端口号(D2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 34;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
                case "目的端口号":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "目的端口号(D2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 36;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
                case "顺序号":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "顺序号(H4B)";
                    labelXEditRuleSetMask.Text = "Mask (H4B)";
                    useq = 38;
                    useqLength = 4;
                    tupdate(useq, 4);
                    break;
                case "确认号":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "确认号(H4B)";
                    labelXEditRuleSetMask.Text = "Mask (H4B)";
                    useq = 42;
                    useqLength = 4;
                    tupdate(useq, 4);
                    break;
                case "头部长度保留位控制位":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "头部长度保留位控制位(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 46;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
                case "窗口大小":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "窗口大小(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 48;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
                case "校验和":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "校检和(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 50;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
                case "紧急指针":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "紧急指针(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 52;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
            }
            if (useqLength > 1)
                textBoxXEditRuleSetByte.Text = useq.ToString() + "~" + (useq + useqLength - 1).ToString();
            else
                textBoxXEditRuleSetByte.Text = useq.ToString();
        }


        private void udpField_Click(object sender, EventArgs e)
        {
            ButtonX btx = (ButtonX)sender;
            groupPanelEditRuleSet.Enabled = true;
            textBoxXEditRuleSetByte.ReadOnly = true;
            _currentByteOrField = false;
            useqlayer = 2;
            labelXEditByteIndex.Text = "当前操作字节编号：";
            switch (btx.Text)
            {
                case "校验和":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "校检和(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 40;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
                case "长度":
                    //MessageBox.Show("hah");
                    labelXEditRuleSetValue.Text = "长度(H2B)";
                    labelXEditRuleSetMask.Text = "Mask (H2B)";
                    useq = 38;
                    useqLength = 2;
                    tupdate(useq, 2);
                    break;
            }
            if (useqLength > 1)
                textBoxXEditRuleSetByte.Text = useq.ToString() + "~" + (useq + useqLength - 1).ToString();
            else
                textBoxXEditRuleSetByte.Text = useq.ToString();
        }

        //呈现对目标规则单元已有的规则
        private void tupdate(int seq, int len)
        {

            string seqstr = "";
            seqstr = findseqstr(seq, len);
            bool flag = false;// 用来标注端口号，是十进制数
            if (seq == 34 || seq == 36)
            {
                flag = true;
            }
            if (seqstr != "")
            {
                string mstr = seqstr.Substring(0, len * 2);
                string rstr = seqstr.Substring(len * 2, len * 2);
                if (flag == true)
                {
                    rstr = Convert.ToInt32(rstr, 16).ToString();
                }
                textBoxXEditRuleSetMask.Text = mstr;
                textBoxXEditRuleSetValue.Text = rstr;
            }
            else
            {
                textBoxXEditRuleSetMask.Text = "";
                textBoxXEditRuleSetValue.Text = "";
            }
        }




       /// <summary>
        ///  find useq len  字段 是否存在 若存在，返回mask+value字串，否则，返回"";
       /// </summary>
       /// <param name="startseq"></param>
       /// <param name="len"></param>
       /// <returns></returns>
        private string findseqstr(int startseq, int len)
        {
            string mstr = "";
            string vstr = "";
            string emstr = "";
            for (int i = 0; i < len; i++)
            {
                int x = findseq(startseq);
                if (x != -1)
                {
                    mstr += uset[urule, x, 1];
                    vstr += uset[urule, x, 2];
                }
                else
                {
                    mstr += "00";
                    vstr += "00";
                }
                emstr += "00";
                startseq++;
            }
            if (mstr == emstr && vstr == emstr)
            {
                return "";
            }
            else
            {
                return mstr + vstr;
            }
        }

        //find in uset[,,]  return j
        public int findseq(int seq)
        {

            for (int j = 0; j < 32; j++)
            {
                if (uset[urule, j, 0] != "")
                {
                    int xx = int.Parse(uset[urule, j, 0]);
                    if (xx == seq)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }







        private void buttonXEditRuleSet_Click(object sender, EventArgs e)
        {
            if (_currentByteOrField)
                buttonXEditRuleSetByByte_Click(sender, e);
            else
                buttonXEditRuleSetByField_Click(sender,e);
        }

        private void buttonXEditRuleSetByByte_Click(object sender, EventArgs e)
        {
            if (textBoxXEditRuleSetByte.Text == String.Empty)
            {
                MessageBox.Show("seq值不能为空");
                return;
            }

            bool flag = false;
            if (uset[urule, nbyte, 0] != "")     //相同NO. 可以任意覆盖修改,但如果其他地方设置过改seq，还是会提示
            {
                if (MessageBox.Show("已经设置过该NO，确定被覆盖，或取消", "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //确定按钮的方法
                    flag = true;
                }
                else
                {
                    //取消按钮的方法
                    return;
                }
            }

            bool flagseq = false;
            for (int i = 0; i < 32; i++)
            {
                if (textBoxXEditRuleSetByte.Text == uset[urule, i, 0])
                {
                    flagseq = true;
                    break;
                }
            }
            if (flagseq == true && uset[urule, nbyte, 0] != textBoxXEditRuleSetByte.Text)
            {
                MessageBox.Show("已经设置过该seq，同一seq，只能设置一次");
                return;
            }
            if (textBoxXEditRuleSetMask.Text.Length != 2 || textBoxXEditRuleSetValue.Text.Length != 2)
            {
                MessageBox.Show("输入字节长度不对");
                return;
            }
            uset[urule, nbyte, 0] = textBoxXEditRuleSetByte.Text;
            uset[urule, nbyte, 1] = textBoxXEditRuleSetMask.Text;
            uset[urule, nbyte, 2] = textBoxXEditRuleSetValue.Text;

            if (flag)
            {
                MessageBox.Show("Replaced 规则字节号" + urule + "." + nbyte + "\n掩码" + uset[urule, nbyte, 1] + "\n匹配值" + uset[urule, nbyte, 2]);
            }
            else
            {
                MessageBox.Show("Added 规则字节号" + urule + "." + nbyte + "\n掩码" + uset[urule, nbyte, 1] + "\n匹配值" + uset[urule, nbyte, 2]);
            }
            ChooseByteHappen();
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();

        }
        
        private void buttonXEditRuleSetByField_Click(object sender, EventArgs e)
        {
            switch (useqlayer)
            {
                case 0:
                    buttonsetmac_Click(sender,e);
                    break;
                case 1:
                    buttonsetip_Click(sender, e);
                    break;
                case 2:
                    buttonsettu_Click(sender, e);
                    break;
                case 3:
                    buttonsetdata_Click(sender, e);
                    break;
            }
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();
        }

        private void buttonsetmac_Click(object sender, EventArgs e)
        {

            if (labelXEditRuleSetValue.Text == "目的MAC(H6B)")
            {
                useq = 0;
            }

            int n = int.Parse(labelXEditRuleSetValue.Text.Substring((labelXEditRuleSetValue.Text.Length - 3), 1));
            if (textBoxXEditRuleSetValue.Text.Length != n * 2 || textBoxXEditRuleSetMask.Text.Length != n * 2)
            {
                MessageBox.Show("输入字节长度不对");
                return;
            }
            fillset(textBoxXEditRuleSetMask.Text, textBoxXEditRuleSetValue.Text, useq, n);

        }

        private void buttonsetip_Click(object sender, EventArgs e)
        {
            if (labelXEditRuleSetValue.Text == "版本首部长(H1B)")
            {
                useq = 14;
            }
            if (useq == 26 || useq == 30)
            {
                string[] pieces1 = new string[4];
                pieces1 = textBoxXEditRuleSetMask.Text.Split(".".ToCharArray(), 4);
                string[] pieces2 = new string[4];
                pieces2 = textBoxXEditRuleSetValue.Text.Split(".".ToCharArray(), 4);
                string ipm = "";
                string ipv = "";
                for (int i = 0; i < 4; i++)
                {
                    ipm += String.Format("{0:X2}", int.Parse(pieces1[i]));
                    ipv += String.Format("{0:X2}", int.Parse(pieces2[i]));
                }
                fillset(ipm, ipv, useq, 4);
            }
            else
            {
                int n = int.Parse(labelXEditRuleSetValue.Text.Substring((labelXEditRuleSetValue.Text.Length - 3), 1));
                if (textBoxXEditRuleSetMask.Text.Length != n * 2 || textBoxXEditRuleSetValue.Text.Length != n * 2)
                {
                    MessageBox.Show("输入字节长度不对");
                    return;
                }
                fillset(textBoxXEditRuleSetMask.Text, textBoxXEditRuleSetValue.Text, useq, n);
            }
        }

        private void buttonsettu_Click(object sender, EventArgs e)
        {
            if (labelXEditRuleSetValue.Text == "源端口号(D2B)")
            {
                useq = 34;
            }
            int n = int.Parse(labelXEditRuleSetValue.Text.Substring((labelXEditRuleSetValue.Text.Length - 3), 1));
            if (labelXEditRuleSetValue.Text.Substring(0, 1) == "源" || labelXEditRuleSetValue.Text.Substring(0, 1) == "目")
            {
                if ((!Regex.IsMatch(textBoxXEditRuleSetValue.Text, @"\d+"))||textBoxXEditRuleSetMask.Text.Length != n * 2 || int.Parse(textBoxXEditRuleSetValue.Text) < 0 || int.Parse(textBoxXEditRuleSetValue.Text) > 65535)// || (!Regex.IsMatch(textBoxXEditRuleSetValue.Text, @"\d+")))
                {
                    MessageBox.Show("输入不正确");
                    return;
                }
            }
            else
            {
                if (textBoxXEditRuleSetMask.Text.Length != n * 2 || textBoxXEditRuleSetValue.Text.Length != n * 2)
                {
                    MessageBox.Show("输入字节长度不对");
                    return;
                }
            }

            string value2 = textBoxXEditRuleSetValue.Text;
            if (labelXEditRuleSetValue.Text.Substring(0, 1) == "源" || labelXEditRuleSetValue.Text.Substring(0, 1) == "目")
            {
                value2 = String.Format("{0:X4}", int.Parse(textBoxXEditRuleSetValue.Text));
                fillset(textBoxXEditRuleSetMask.Text, value2, useq, 2);
            }
            else
            {
                fillset(textBoxXEditRuleSetMask.Text, textBoxXEditRuleSetValue.Text, useq, n);
            }


        }

        private void buttonsetdata_Click(object sender, EventArgs e)
        {
            if (textBoxXEditRuleSetByte.Text == String.Empty || textBoxXEditRuleSetMask.Text == String.Empty || textBoxXEditRuleSetValue.Text == String.Empty)
            {
                MessageBox.Show("输入不能为空");
                return;
            }
            int xx = 0;
            if (TcpOrUdp)
            {
                xx = 53;
            }
            else
            {
                xx = 41;
            }
            if (int.Parse(textBoxXEditRuleSetByte.Text) <= xx || int.Parse(textBoxXEditRuleSetByte.Text) > 2047 || textBoxXEditRuleSetMask.Text.Length != 2 || textBoxXEditRuleSetValue.Text.Length != 2)
            {
                MessageBox.Show("输入不正确");
                return;
            }



            int x = findseq(int.Parse(textBoxXEditRuleSetByte.Text));
            //if (x != -1)
            //{
            //    MessageBox.Show("已存在该字段，不能重复添加，请更新或删除");
            //    return;
            //}
            //else
            //{
            if (fillset(textBoxXEditRuleSetMask.Text, textBoxXEditRuleSetValue.Text, int.Parse(textBoxXEditRuleSetByte.Text), 1) == 0)
                {
                    int iseq = findseq(int.Parse(textBoxXEditRuleSetByte.Text));
                    ListViewItem listrecord = new ListViewItem(iseq.ToString());//这个是第一行第一列
                    listrecord.SubItems.Add(textBoxXEditRuleSetByte.Text);//第一行第二列
                    listrecord.SubItems.Add(textBoxXEditRuleSetValue.Text);//第一行第二列
                    listrecord.SubItems.Add(textBoxXEditRuleSetMask.Text);//第一行第三列
                    listViewEx1.Items.Add(listrecord);//把第一行添加上
                    labelXEditRuleSetByte.Text = "当前编辑的字节编号为NO." + iseq;
                }
            //}
            superTabItem4_Click(sender,e);
        }

        // mask & value   int length   fill uset[]  如果已存在，则先清空再写入
        private int fillset(string mask, string value, int seq, int len)
        {
            if (findseqstr(seq, len) != "")
            {
                if (MessageBox.Show("已经存在该字段，确认覆盖", "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    for (int i = 0; i < len; i++)
                    {
                        int x = findseq(seq);
                        if (x != -1)
                        {
                            uset[urule, x, 0] = "";
                            uset[urule, x, 1] = "";
                            uset[urule, x, 2] = "";
                        }
                        seq++;
                    }
                    seq = seq - len;
                }
                else
                {
                    return -1;
                }

            }
            if (findemn() >= len)
            {
                string mstr = "";
                string vstr = "";
                for (int i = 0; i < len; i++)
                {
                    mstr = mask.Substring(i * 2, 2);
                    vstr = value.Substring(i * 2, 2);
                    if (mstr != "00" || vstr != "00")
                    {
                        string em = findempty(1);
                        if (em != "-1")
                        {
                            int iem = int.Parse(em);
                            uset[urule, iem, 0] = seq.ToString();
                            uset[urule, iem, 1] = mstr;
                            uset[urule, iem, 2] = vstr;
                        }
                        //MessageBox.Show(em+"\n"+mstr+"\n"+vstr);
                    }
                    seq++;
                }
                return 0;
            }
            else
            {
                MessageBox.Show("可设置空间受限，不允许设置");
                return -1;
            }

        }

        //find empty num
        private int findemn()
        {
            int count = 0;
            for (int i = 0; i < 32; i++)
            {
                if (uset[urule, i, 0] == "")
                {
                    count++;
                }
            }
            return count;
        }

        //alloc empty in uset 2047=="" ? 
        private string findempty(int n)
        {
            string emp = "";
            int count = 0;
            for (int i = 0; i < 32; i++)
            {
                if (uset[urule, i, 0] == "")
                {
                    count++;
                    emp += String.Format("{0:D2}", i);
                    if (count >= n)
                    {
                        return emp;
                    }
                }
            }
            return "";
        }

        private void groupPanelEditRuleSet_EnabledChanged(object sender, EventArgs e)
        {
            if (groupPanelEditRuleSet.Enabled == false)
            {
                groupPanelEditRuleSetByteAndValueAndMaskLabelTextByByteChoose();
            }
        }

        private void groupPanelEditRuleSetByteAndValueAndMaskLabelTextByByteChoose()
        {
            labelXEditRuleSetByte.Text = "当前编辑的字节号(D)";
            textBoxXEditRuleSetByte.Text = "";
            labelXEditRuleSetValue.Text = "匹配值   (H1B)";
            textBoxXEditRuleSetValue.Text = "";
            labelXEditRuleSetMask.Text = "掩码(H1B)";
            textBoxXEditRuleSetMask.Text = "";
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            //switch(superTabControl1.SelectedTab)
            //    case superTabItem1:
            //
            //labelXEditRuleSetByte.Text = "当前编辑的字节编号为NO.";
            if (!_currentByteOrField)
            {
                groupPanelEditRuleSet.Enabled = false;
                textBoxXEditRuleSetByte.ReadOnly = true;
                labelXEditByteIndex.Text = "当前操作字节编号： ";
            }
            if (superTabControl1.SelectedTab == superTabItem1)
                useqlayer = 0;
            else
            {
                if (superTabControl1.SelectedTab == superTabItem2)
                    useqlayer = 1;
                else
                {
                    if (superTabControl1.SelectedTab == superTabItem3)
                        useqlayer = 2;
                    else 
                    {
                        groupPanelEditRuleSet.Enabled = true;
                        textBoxXEditRuleSetByte.ReadOnly = false;
                        useqlayer = 3;
                        //_currentByteOrField = false;
                        if (TcpOrUdp)
                            labelXEditRuleSetByte.Text = "字节号D 53<D<2047";
                        else
                            labelXEditRuleSetByte.Text = "字节号D 41<D<2047";
                    }
                }
            }
        }

        private void listViewEx1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                ListViewItem myItem = listViewEx1.SelectedItems[0];
                labelXEditRuleSetByte.Text = "当前编辑的字节号为NO." + myItem.SubItems[0].Text;
                textBoxXEditRuleSetByte.Text = myItem.SubItems[1].Text;
                textBoxXEditRuleSetValue.Text = myItem.SubItems[2].Text;
                textBoxXEditRuleSetMask.Text = myItem.SubItems[3].Text;
            }
        }

        private void buttonXEditRuleClearRule_Click(object sender, EventArgs e)
        {
            ClearOneRule(urule);
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();
            
        }

        private void buttonXEditRuleSendRule_Click(object sender, EventArgs e)
        {
            SendOneRule(urule);
        }

        // 注册为串口接受数据事件 写入 str55
        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (flag55 == false)
            {
                int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                received_count += n;//增加接收计数
                comm.Read(buf, 0, n);//读取缓冲数据
                builder.Clear();//清除字符串构造器的内容
                //因为要访问ui资源，所以需要使用invoke方式同步ui。
                this.Invoke((EventHandler)(delegate
                {
                    //直接按ASCII规则转换成字符串
                    builder.Append(Encoding.ASCII.GetString(buf));
                    //追加的形式添加到文本框末端，并滚动到最后。no log no texGet
                    //logview.txGet.AppendText(builder.ToString());
                    //修改接收计数
                    //20150427注释
                    //logview.labelGetCount.Text = "Get:" + received_count.ToString();
                }));
            }
            else
            {
                int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                comm.Read(buf, 0, n);//读取缓冲数据
                builder.Clear();//清除字符串构造器的内容
                //因为要访问ui资源，所以需要使用invoke方式同步ui。

                this.Invoke((EventHandler)(delegate
                {
                    //直接按ASCII规则转换成字符串
                    builder.Append(Encoding.ASCII.GetString(buf));
                    str55 += builder.ToString();

                    if (str55.Length >= 128 * 64 * 2)
                    {
                        flag55 = false;
                        sem.Release();
                    }
                }));
            }
        }

        private bool SendOneRule(int ruleIndex)
        {


            if (comm.IsOpen)
            {
                string ruleDDstr = "";
                string ruleEEstr = "";
                string iseq;
                string imask;
                string ivalue;
                bool ruleLed = false;
                for (int i = 0; i < 32; i++)
                {
                    iseq = uset[ruleIndex, i, 0];
                    imask = uset[ruleIndex, i, 1];
                    ivalue = uset[ruleIndex, i, 2];
                    ruleDDstr += buildDD(ruleIndex, i, iseq);
                    ruleEEstr += buildEE(ruleIndex, i, imask, ivalue);
                    if (iseq != "")
                        ruleLed = true;
                }
                string ruleStr = "";
                string a = "";
                string b = "";
                int c = 0;
                for (int i = 0; i < 32; i++)
                {
                    //int c1 = 0, c2 = 0;
                    if (uset[ruleIndex, i, 0] == "")
                        continue;
                    AnalyzeFieldNameByForm1useq(int.Parse(uset[ruleIndex, i, 0]), TcpOrUdp, ref a, ref b, ref c);
                    //AnalyzeFiledNameByFieldNO(c1,c2,ref a,ref b);
                    ruleStr += "配置" + a + "的" + b + "第" + c + "个字节  掩码为" + uset[ruleIndex, i, 1] + " 匹配值为" + uset[ruleIndex, i, 2] + "\n";
                }
                bool sendornot = false;
                if (ruleStr == "")
                    sendornot = true;
                else
                if (MessageBox.Show("规则如下，确定发送\n" + ruleStr, "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    sendornot = true;

                if (sendornot)//if (MessageBox.Show("规则如下，确定发送\n" + "66\n" + "DD\n" + ruleDDstr + "\nEE\n" + ruleEEstr, "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    for (int j = 0; j < 32; j++)
                        for (int k = 0; k < 3; k++)
                            uset_d[ruleIndex, j, k] = uset[ruleIndex, j, k];
                    if(ruleLed)
                        led_d[ruleIndex] = led[ruleIndex] = 1;
                    else
                        led_d[ruleIndex] = led[ruleIndex] = 0;

                    chk_d[ruleIndex] = chk[ruleIndex] = 1;

                    

                    string dd66 = "66";
                    string ddstr66 = buildEN_D();
                    ddstr66 = ddstr66.ToUpper();

                    comm.Write(dd66);
                    send_count += dd66.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(ddstr66);
                    send_count += ddstr66.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面

                    Thread.Sleep(10);

                    //确定按钮的方法
                    string dd = "DD";
                    string ee = "EE";
                    string ddstr = ruleDDstr;
                    string eestr = ruleEEstr;

                    comm.Write(dd);
                    send_count += dd.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(ddstr);
                    send_count += ddstr.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面

                    Thread.Sleep(10);

                    comm.Write(ee);
                    send_count += ee.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(eestr);
                    send_count += eestr.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面

                    ///copy rule data from rule to rule_d
                    Thread.Sleep(10);

                        WriteLog("发送规则", "发送字节" + send_count.ToString());
                }
                else
                {
                    //取消按钮的方法
                    return false;
                }
            }
            else
            {
                MessageBox.Show("请先打开端口！");
                return false;
            }
            return true;
        }

        private bool SendOneRule_D(int ruleIndex)
        {


            if (comm.IsOpen)
            {
                string ruleDDstr = "";
                string ruleEEstr = "";
                string iseq;
                string imask;
                string ivalue;
                bool emptyOrNot = true;
                for (int i = 0; i < 32; i++)
                {
                    iseq = uset_d[ruleIndex, i, 0];
                    imask = uset_d[ruleIndex, i, 1];
                    ivalue = uset_d[ruleIndex, i, 2];
                    ruleDDstr += buildDD(ruleIndex, i, iseq);
                    ruleEEstr += buildEE(ruleIndex, i, imask, ivalue);
                    if (iseq != "")
                        emptyOrNot = false;
                }
                if (emptyOrNot)
                {
                    led_d[ruleIndex] = 0;
                    chk_d[ruleIndex] = 0;
                }
                else
                {
                    led_d[ruleIndex] = 1;
                    chk_d[ruleIndex] = 1;
                }
                string ruleStr = "";
                //bool tcporudp = true;
                //if (!switchButton3.Value)
                //    tcporudp = false;
                string a = "";
                string b = "";
                int c = 0;
                //for (int i = 0; i < 32; i++)
                //{
                //    //int c1 = 0, c2 = 0;
                //    if (uset[ruleIndex, i, 0] == "")
                //        continue;
                //    AnalyzeFieldNameByForm1useq(int.Parse(uset[ruleIndex, i, 0]), tcporudp, ref a, ref b, ref c);
                //    //AnalyzeFiledNameByFieldNO(c1,c2,ref a,ref b);
                //    ruleStr += "配置" + a + "的" + b + "第" + c + "个字节  掩码为" + uset[ruleIndex, i, 1] + " 匹配值为" + uset[ruleIndex, i, 2] + "\n";
                //}

               
                    //led[ruleIndex] = 1;
                    //ledcheck(ruleIndex, 1);

                    string dd66 = "66";
                    string ddstr66 = buildEN_D();
                    ddstr66 = ddstr66.ToUpper();

                    comm.Write(dd66);
                    send_count += dd66.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(ddstr66);
                    send_count += ddstr66.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面

                    Thread.Sleep(10);

                    //确定按钮的方法
                    string dd = "DD";
                    string ee = "EE";
                    string ddstr = ruleDDstr;
                    string eestr = ruleEEstr;

                    comm.Write(dd);
                    send_count += dd.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(ddstr);
                    send_count += ddstr.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面

                    Thread.Sleep(10);

                    comm.Write(ee);
                    send_count += ee.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(eestr);
                    send_count += eestr.Length;//累加发送字节数
                    //20150427注释
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    Thread.Sleep(10);
                    //WriteLog("发送防火墙规则", ruleIndex.ToString());
                
                //else
                //{
                //    //取消按钮的方法
                //    return false;
                //}
            }
            else
            {
                MessageBox.Show("请先打开端口！");
                return false;
            }
            return true;
        }
        public string buildDD(int xrule, int xbyte, string xseq)
        {
            if (xseq == "")
            {
                xseq = "2047";
            }
            int seq = int.Parse(xseq);
            string seqstr = String.Format("{0:X4}", seq);
            seq = int.Parse(seqstr.Substring(0, 2));
            int xbytetmp = (xbyte << 3) & 0xF8;
            seq = seq + xbytetmp;
            string bytestr = String.Format("{0:X2}", seq);
            seqstr = seqstr.Substring(2, 2);
            string rulestr = String.Format("{0:X2}", xrule);
            string strDD = rulestr + bytestr + seqstr;
            return strDD;
        }

        public string buildEE(int xrule, int xbyte, string xmask, string xvalue)
        {
            if (xmask == "")
            {
                xmask = "00";
            };
            if (xvalue == "")
            {
                xvalue = "00";
            };
            int xruletmp = (xrule << 5) & 0xE0;
            xbyte += xruletmp;
            string bytestr = String.Format("{0:X2}", xbyte);

            int xruletmp1 = (xrule >> 3) & 0x03;
            string rulestr = String.Format("{0:X2}", xruletmp1);

            string strDD = rulestr + bytestr + xmask + xvalue;
            return strDD;
        }

        private string buildEN()
        {
            string enstr = "";
            int ien = 0;
            for (int i = 1; i < 31; i++)
            {
                if (chk[i] == 1)
                {
                    ien += chk[i] << (32 - i);
                }
            }
            enstr = String.Format("{0:X8}", ien);
            //MessageBox.Show(enstr);
            return enstr;
        }

        private string buildEN_D()
        {
            string enstr = "";
            int ien = 0;
            for (int i = 1; i < 31; i++)
            {
                if (chk_d[i] == 1)
                {
                    ien += chk_d[i] << (32 - i);
                }
            }
            enstr = String.Format("{0:X8}", ien);
            //MessageBox.Show(enstr);
            return enstr;
        }


        private async void WriteLog(string operatorname, string remark = null)
        {
            await collectionLog.InsertOneAsync(new Log { operatorId = _currectOperatorId, operatorName = _currectOperator._name, operationName = operatorname, datetime = DateTime.Now.AddHours(8), lastModified = DateTime.Now.AddHours(8), remark = remark });

        }
        /// <summary>
        /// 切换用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        


        private void UpdateSuperTabItemViewRule()
        {
            UpdateRuleViewLocalRule();
            thdSub2 = new Thread(new ThreadStart(GetDRuleFromDeviceToHostAndUpdateRuleViewDeviceRule));
            thdSub2.Start();
            
            
   
        }

        private void GetDRuleFromDeviceToHostAndUpdateRuleViewDeviceRule()
        {
            if (RealOrTest)
            {
                Thread threadGetRuleFromDevicetoHost;
                threadGetRuleFromDevicetoHost = new Thread(new ThreadStart(threadFucGetRuleFromDevicetoHost));
            threadGetRuleFromDevicetoHost.Start();

            }
                
            else
            {
                
                //for (int i = 1; i < 3; i++)
                //{
                //    led_d[i] = 1;
                //    chk_d[i] = ((i/2)*2==i)?1:0;
                //    for (int j = 0; j < 5-i; j++)
                //    {
                //        for (int k = 0; k < 3; k++)
                //        {
                //            uset_d[i, j, k] = (i + j).ToString();
                //        }
                //    }
                //}
                UpdateRuleViewDeviceRule();
                UpdateRuleViewDeviceRule_1();
            }
            //UpdateRuleViewDeviceRule();
            //UpdateRuleViewDeviceRule_1();

        }
        private void threadFucGetRuleFromDevicetoHost()
        {
            GetDRuleFromDeviceToHost();
            UpdateRuleViewDeviceRule();
        }

        private void superTabControl2_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (superTabControl2.SelectedTab == superTabItemViewRule)
                UpdateSuperTabItemViewRule();
            if (superTabControl2.SelectedTab == superTabItemEditRule)
                UpdateSuperTabItemEditRule();
            if (superTabControl2.SelectedTab == superTabItemPersonInformation)
            {
                ribbonControl1.SelectedRibbonTabItem = ribbonTabItem3;
                UpdatePersonInformation();
            }

        }

        /// <summary>
        /// 在valuetextbox和masktexbox回车时触发设置事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxXEditRuleSet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                buttonXEditRuleSet_Click(sender, e);
        }


        private void superTabItem4_Click(object sender, EventArgs e)
        {

            groupPanelEditRuleSet.Enabled = true;
            textBoxXEditRuleSetByte.ReadOnly = false;
            useqlayer = 3;
            labelXEditRuleSetByte.Text = "当前编辑的字节编号为NO.";
            _currentByteOrField = false;
            nbyte = -1;
            UpdateSuperTabItemEditRule();
            if (TcpOrUdp)
                labelXEditRuleSetByte.Text = "字节号D 53<D<2047";
            else
                labelXEditRuleSetByte.Text = "字节号D 41<D<2047";
        }




        #region save and import rule and some function 
        private void saveRule()
        {
            //1-30
            string dd = "";
            string ee = "";
            string ddee = "";
            for (int i = 1; i < 31; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    dd += buildDD(i, j, uset[i, j, 0]);
                    ee += buildEE(i, j, uset[i, j, 1], uset[i, j, 2]);
                }
                for (int k = 0; k < 64; k++)
                {
                    dd += "0";
                }
                ddee += dd + ee;
                dd = "";
                ee = "";

            }
            //31
            int irule = 31;
            string lenDDstr = "";
            //len[0] = textBoxlens.Text;
            //len[1] = textBoxlenb.Text;
            lenDDstr += buildDD(irule, 0, len[0]);
            lenDDstr += buildDD(irule, 1, len[1]);
            for (int i = 2; i < 32; i++)
            {
                lenDDstr += buildDD(irule, i, "00");
            }
            for (int k = 0; k < 64; k++)
            {
                lenDDstr += "0";
            }
            ddee += lenDDstr;

            string enstr = buildEN();
            for (int i = 0; i < 256 - 8; i++)
            {
                enstr += "0";
            }

            ddee += enstr;

            //32
            for (int i = 0; i < 512; i++)
            {
                ddee += "0";
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt|*.txt";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fsw = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fsw);
                sw.WriteLine(ddee);
                sw.Close(); fsw.Close();

                MessageBox.Show("规则已经保存到" + saveFileDialog.FileName);
            }

        }

        private void importRule()
        {
            //openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String file = openFileDialog1.FileName;
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string erule = sr.ReadLine();

                //0-30 ddee
                for (int i = 0; i < 30; i++)
                {
                    string ddee = erule.Substring(i * 512, 512);

                    //dd
                    string dd = ddee.Substring(0, 192);
                    //ReverseDD_toUser(dd,i);
                    ReverseDD_toExpert(dd, i + 1);
                    //ee
                    string ee = ddee.Substring(256, 256);
                    //ReverseEE_toUser(ee,i);
                    ReverseEE_toExpert(ee, i + 1);
                }
                for (int i = 1; i < 31; i++)
                {
                    led[i] = 0;
                    for (int j = 0; j < 32; j++)
                    {
                        if (uset[i, j, 0] == "2047")
                        {
                            uset[i, j, 0] = "";
                            uset[i, j, 1] = "";
                            uset[i, j, 2] = "";
                        }
                        else
                            led[i] = 1;
                    }
                }

                // 第三十一个 512字节断 用来表示 len规则  也就是目前没有长规则
                //31
                string enlen = erule.Substring(30 * 512, 512);

                //len
                string lenstr = enlen.Substring(0, 192);
                for (int i = 0; i < 2; i++)
                {
                    string ch6 = lenstr.Substring(i * 6, 6);
                    string bytestr = ch6.Substring(2, 2);
                    int ibyte = Convert.ToInt32(bytestr, 16);
                    int mbyte = (ibyte >> 3) & 0x1F;

                    string seq = String.Format("{0:X2}", (ibyte & 0x07));
                    seq += ch6.Substring(4, 2);
                    int mseq = Convert.ToInt32(seq, 16);

                    len[i] = mseq.ToString();
                }
                //更新长度
                //textBoxlens.Text = len[0];
                //textBoxlenb.Text = len[1];

                // 规则使能？
                //en
                string enstr = enlen.Substring(256, 8);
                uint en = Convert.ToUInt32(enstr, 16);
                en = en >> 2;
                string led30 = "";
                for (int i = 0; i < 30; i++)
                {
                    if (en % 2 == 1)
                    {
                        chk[30 - i] = 1;
                    }
                    else
                    {
                        chk[30 - i] = 0;
                    }
                    en = en >> 1;
                    led30 += chk[30 - i].ToString();
                }
                //更新 check 更新led
                //textBoxendis.Text = enstr;
                //MessageBox.Show(led30 + "\n" + enstr);


                //for (int i = 1; i < 31; i++)
                //{
                //    //string checkled = "checkBox" + String.Format("{0:D3}", i);
                //    //Control[] cc = viewrule.ledPane.Controls.Find(checkled, true);
                //    //CheckBox btc = (CheckBox)cc[0];
                    
                //    if (chk[i] == 0)
                //    {
                //        //btc.CheckStateChanged -= new EventHandler(led_CheckedStateChanged);
                //        //btc.Checked = false;
                //        led[i] = 0;
                //        //btc.CheckStateChanged += new EventHandler(led_CheckedStateChanged);
                //    }
                //    else
                //    {
                //        //btc.CheckStateChanged -= new EventHandler(led_CheckedStateChanged);
                //        //btc.Enabled = true;
                //        //btc.Checked = true;
                //        led[i] = 1;
                //        //btc.CheckStateChanged += new EventHandler(led_CheckedStateChanged);
                //    }
                //}


                //for (int i = 1; i < 31; i++)
                //{
                //    if (chk[i] == 0)
                //    {
                //        for (int j = 0; j < 32; j++)
                //        {
                //            uset[i, j, 0] = "";
                //            uset[i, j, 1] = "";
                //            uset[i, j, 2] = "";
                //        }
                //    }
                //}
                for (int i = 1; i < 31; i++)
                    for (int j = 0; j < 32; j++)
                    { 
                    }

                        sr.Close(); fs.Close();
            }

            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();
        }

        private void ReverseDD_toExpert(string dds, int rule)
        {
            for (int i = 0; i < 32; i++)
            {
                string ch6 = dds.Substring(i * 6, 6);
                string bytestr = ch6.Substring(2, 2);
                int ibyte = Convert.ToInt32(bytestr, 16);
                int mbyte = (ibyte >> 3) & 0x1F;

                string seq = String.Format("{0:X2}", (ibyte & 0x07));
                seq += ch6.Substring(4, 2);
                int mseq = Convert.ToInt32(seq, 16);

                uset[rule, mbyte, 0] = mseq.ToString();
                //if (mseq == 2047)
                //{
                //    uset[rule, mbyte, 0] = "";
                //}
                //else
                //{
                //    uset[rule, mbyte, 0] = mseq.ToString();
                //}
            }
        }


        private void ReverseEE_toExpert(string ees, int rule)
        {
            for (int i = 0; i < 32; i++)
            {
                string ch8 = ees.Substring(i * 8, 8);
                string bytestr = ch8.Substring(2, 2);
                int ibyte = Convert.ToInt32(bytestr, 16);
                int mbyte = ibyte & 0x1F;

                string maskstr = ch8.Substring(4, 2);
                string valuestr = ch8.Substring(6, 2);
                uset[rule, mbyte, 1] = maskstr;
                uset[rule, mbyte, 2] = valuestr;
            }

        }

        private void ReverseDD_toExpert_D(string dds, int rule)
        {
            for (int i = 0; i < 32; i++)
            {
                string ch6 = dds.Substring(i * 6, 6);
                string bytestr = ch6.Substring(2, 2);
                int ibyte = Convert.ToInt32(bytestr, 16);
                int mbyte = (ibyte >> 3) & 0x1F;

                string seq = String.Format("{0:X2}", (ibyte & 0x07));
                seq += ch6.Substring(4, 2);
                int mseq = Convert.ToInt32(seq, 16);

                uset_d[rule, mbyte, 0] = mseq.ToString();
                //if (mseq == 2047)
                //{
                //    uset[rule, mbyte, 0] = "";
                //}
                //else
                //{
                //    uset[rule, mbyte, 0] = mseq.ToString();
                //}
            }
        }


        private void ReverseEE_toExpert_D(string ees, int rule)
        {
            for (int i = 0; i < 32; i++)
            {
                string ch8 = ees.Substring(i * 8, 8);
                string bytestr = ch8.Substring(2, 2);
                int ibyte = Convert.ToInt32(bytestr, 16);
                int mbyte = ibyte & 0x1F;

                string maskstr = ch8.Substring(4, 2);
                string valuestr = ch8.Substring(6, 2);
                uset_d[rule, mbyte, 1] = maskstr;
                uset_d[rule, mbyte, 2] = valuestr;
            }

        }


        #endregion

        #region ViewRule
        private void UpdateRuleViewLocalRule()
        {
            DataSet ds = new DataSet();
            DataTable dataTableRule =  new DataTable("规则表");
            dataTableRule.Columns.Add("gridColumnViewLocalRuleIndex");
            dataTableRule.Columns.Add("gridColumnViewLocalRuleByteCount");
            DataRow dr;
            int ruleByteCount;

            for (int i = 1; i < 31; i++)
            {
                ruleByteCount = 0;
                for(int j=0;j<32;j++)
                {
                    if(uset[i,j,0]!="")
                        ruleByteCount++;
                }
                if (ruleByteCount > 0 && led[i] > 0)
                {
                    dr = dataTableRule.NewRow();
                    dr[0] = i.ToString();
                    dr[1] = ruleByteCount.ToString();
                    dataTableRule.Rows.Add(dr);
                }
            }

            DataTable dataTableByte = new DataTable("字节表");
            dataTableByte.Columns.Add("规则编号");
            dataTableByte.Columns.Add("字节编号");
            dataTableByte.Columns.Add("所处协议层");
            dataTableByte.Columns.Add("所处协议字段");
            dataTableByte.Columns.Add("字段偏移字节数");
            dataTableByte.Columns.Add("掩码");
            dataTableByte.Columns.Add("匹配值");

            for (int i = 1; i < 31; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (uset[i, j, 0] != "")
                    {
                        dr = dataTableByte.NewRow();
                        string protocollayer = "";
                        string fieldinprotocol = "";
                        int fieldinprotocoloffset = -1;
                        AnalyzeFieldNameByForm1useq(int.Parse(uset[i, j, 0]), switchButton2.Value, ref protocollayer, ref fieldinprotocol, ref fieldinprotocoloffset);
                        dr[0] = i.ToString();
                        dr[1] = j.ToString();
                        dr[2] = protocollayer;
                        dr[3] = fieldinprotocol;
                        dr[4] = fieldinprotocoloffset;
                        dr[5] = uset[i, j, 1];
                        dr[6] = uset[i, j, 2];

                        dataTableByte.Rows.Add(dr);
                    }

                }
            }
            ds.Tables.Add(dataTableRule);
            ds.Tables.Add(dataTableByte);
            ds.Relations.Add("relation1", ds.Tables["规则表"].Columns["gridColumnViewLocalRuleIndex"], ds.Tables["字节表"].Columns["规则编号"], true);
            superGridControlLocalRule.PrimaryGrid.DataSource = ds;
            //superGridControlLocalRule.PrimaryGrid.AllowEdit = false;

        }
        /// <summary>
        /// viewdeviceRule和 viewlocalRule 本来是应该提取一个公用的代码模块，变量输入一个输出1个。 版本时间不足，故直接复制代码
        /// </summary>
        private void UpdateRuleViewDeviceRule()
        {

            
            DataSet ds = new DataSet();
            DataTable dataTableRule = new DataTable("规则表");
            dataTableRule.Columns.Add("gridColumnViewDeviceRuleIndex");
            dataTableRule.Columns.Add("gridColumnViewDeviceRuleByteCount");
            DataRow dr;
            int ruleByteCount;

            for (int i = 1; i < 31; i++)
            {
                ruleByteCount = 0;
                for (int j = 0; j < 32; j++)
                {
                    if (uset_d[i, j, 0] != "")
                        ruleByteCount++;
                }
                if (ruleByteCount > 0 && led_d[i] > 0)
                {
                    dr = dataTableRule.NewRow();
                    dr[0] = i.ToString();
                    dr[1] = ruleByteCount.ToString();
                    dataTableRule.Rows.Add(dr);
                }
            }

            DataTable dataTableByte = new DataTable("字节表");
            dataTableByte.Columns.Add("规则编号");
            dataTableByte.Columns.Add("字节编号");
            dataTableByte.Columns.Add("所处协议层");
            dataTableByte.Columns.Add("所处协议字段");
            dataTableByte.Columns.Add("字段偏移字节数");
            dataTableByte.Columns.Add("掩码");
            dataTableByte.Columns.Add("匹配值");

            for (int i = 1; i < 31; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (uset_d[i, j, 0] != "")
                    {
                        dr = dataTableByte.NewRow();
                        string protocollayer = "";
                        string fieldinprotocol = "";
                        int fieldinprotocoloffset = -1;
                        AnalyzeFieldNameByForm1useq(int.Parse(uset_d[i, j, 0]), switchButton3.Value, ref protocollayer, ref fieldinprotocol, ref fieldinprotocoloffset);
                        dr[0] = i.ToString();
                        dr[1] = j.ToString();
                        dr[2] = protocollayer;
                        dr[3] = fieldinprotocol;
                        dr[4] = fieldinprotocoloffset;
                        dr[5] = uset_d[i, j, 1];
                        dr[6] = uset_d[i, j, 2];

                        dataTableByte.Rows.Add(dr);
                    }

                }
            }
            ds.Tables.Add(dataTableRule);
            ds.Tables.Add(dataTableByte);
            ds.Relations.Add("relation1", ds.Tables["规则表"].Columns["gridColumnViewDeviceRuleIndex"], ds.Tables["字节表"].Columns["规则编号"], true);

            //superGridControlDeviceRule.BeginUpdate();
            superGridControlDeviceRule.PrimaryGrid.DataSource = ds;
            //Application.DoEvents();
            //superGridControlDeviceRule.EndUpdate();

            //BindingSource bs = new BindingSource();
            //bs.DataSource = ds;
            //bs.ResetBindings(false);
            //superGridControlDeviceRule.PrimaryGrid.DataSource = bs;
            //bs.ResetBindings(false);

            //superGridControlDeviceRule.PrimaryGrid
            //BindingSource bsCustomers = new BindingSource(ds,"haha");
           // superGridControlDeviceRule.PrimaryGrid.DataSource = bsCustomers;
            //superGridControlDeviceRule.PrimaryGrid.Rows.GetEnumerator();
            //superGridControlDeviceRule.PrimaryGrid.
               //superGridControlDeviceRule.PrimaryGrid.Rows[0].
        }


        /// <summary>
        /// this function exit just because in UpdateRuleViewDeviceRule,we set the dataSource ,
        /// but we don't know when the data bind ,there is no interface named bind .
        /// we try to used winform Control  BindingSource, but we get nothing so..
        /// we bulid the UpdateRuleViewDeviceRule_1 to change the vaule of control in superGridControlDeviceRule
        /// but it will occus the superGridControlDeviceRule_CellValueChanged
        /// </summary>
        private void UpdateRuleViewDeviceRule_1()
        {
            NotBindingDeviceData = false;
            for (int i = 0; i < superGridControlDeviceRule.PrimaryGrid.Rows.Count; i++)
            {
                GridRow gr = superGridControlDeviceRule.PrimaryGrid.Rows[i] as GridRow;
                int j = int.Parse(gr.Cells[0].Value.ToString());
                if (chk_d[j] != 0)
                    gr.Cells[2].Value = true;
                else
                    gr.Cells[2].Value = false;
 
            }
            NotBindingDeviceData = true;
        }

        /// <summary>
        /// get data from device to uset_d chk_d len_d led_d
        /// </summary>
        /// <returns></returns>
        private bool GetDRuleFromDeviceToHost()
        {
            if (comm.IsOpen)
            {
                semNotGettingDataFromDevice.WaitOne();
                if (NotGettingDataFromDevice)
                {
                    NotGettingDataFromDevice = false;
                    semNotGettingDataFromDevice.Release();
                    str55 = "";
                    string dd55 = "55";
                    flag55 = true;
                    Thread.Sleep(10);
                    comm.Write(dd55);
                    sem.WaitOne();
                    //MessageBox.Show(str55);
                    //保存文件
                    if (str55.Length > 128 * 64 * 2)
                        this.Invoke(new MethodInvoker(delegate
                        {
                            string tmp = str55.Substring(21, 128 * 64 * 2);
                            //0-30 ddee
                            for (int i = 0; i < 30; i++)
                            {
                                string ddee = tmp.Substring(i * 512, 512);

                                //dd
                                string dd = ddee.Substring(0, 192);
                                //ReverseDD_toUser(dd,i);
                                ReverseDD_toExpert_D(dd, i + 1);
                                //ee
                                string ee = ddee.Substring(256, 256);
                                //ReverseEE_toUser(ee,i);
                                ReverseEE_toExpert_D(ee, i + 1);
                            }

                            for (int i = 1; i < 31; i++)
                            {
                                for (int j = 0; j < 32; j++)
                                {
                                    if (uset_d[i, j, 0] == "2047")
                                    {
                                        uset_d[i, j, 0] = "";
                                        uset_d[i, j, 1] = "";
                                        uset_d[i, j, 2] = "";
                                    }

                                }
                            }


                            //set led
                            bool a;
                            for (int i = 1; i < 31; i++)
                            {
                                a = false;
                                for (int j = 0; j < 32; j++)
                                {
                                    if (uset_d[i, j, 0] != "")
                                    { a = true; break; }

                                }
                                led_d[i] = (a) ? 1 : 0;
                            }


                            // 第三十一个 512字节断 用来表示 len规则  也就是目前没有长规则
                            //31
                            string enlen = tmp.Substring(30 * 512, 512);

                            //len
                            string lenstr = enlen.Substring(0, 192);
                            for (int i = 0; i < 2; i++)
                            {
                                string ch6 = lenstr.Substring(i * 6, 6);
                                string bytestr = ch6.Substring(2, 2);
                                int ibyte = Convert.ToInt32(bytestr, 16);
                                int mbyte = (ibyte >> 3) & 0x1F;

                                string seq = String.Format("{0:X2}", (ibyte & 0x07));
                                seq += ch6.Substring(4, 2);
                                int mseq = Convert.ToInt32(seq, 16);

                                len_d[i] = mseq.ToString();
                            }
                            //更新长度
                            //textBoxlens.Text = len[0];
                            //textBoxlenb.Text = len[1];

                            // 规则使能？
                            //en
                            string enstr = enlen.Substring(256, 8);
                            uint en = Convert.ToUInt32(enstr, 16);
                            en = en >> 2;
                            string led30 = "";
                            for (int i = 0; i < 30; i++)
                            {
                                if (en % 2 == 1)
                                {
                                    chk_d[30 - i] = 1;
                                }
                                else
                                {
                                    chk_d[30 - i] = 0;
                                }
                                en = en >> 1;
                                led30 += chk_d[30 - i].ToString();
                            }
                            bool led;
                            for (int i = 1; i < 31; i++)
                            {
                                led = false;
                                for (int j = 0; j < 32; j++)
                                {
                                    if (uset_d[i, j, 0] != "")
                                    {
                                        led = true;
                                        break;
                                    }
                                }
                                led_d[i] = (led) ? 1 : 0;
                            }



                        }));


                    semNotGettingDataFromDevice.WaitOne();
                    NotGettingDataFromDevice = true;
                    semNotGettingDataFromDevice.Release();
                    return true;
                }
                return false;
            }
            else
            {
                MessageBox.Show("请打开串口");
                return false;
            }
        }


        /// <summary>
        /// get data from  uset_d chk_d len_d led_d to uset chk len led
        /// </summary>
        private void GetOneRuleFromDRuleToRule(int ruleIndex_d, int ruleIndex)
        {
            bool ledb = false;
            for (int j = 0; j < 32; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    uset[ruleIndex, j, k] = uset_d[ruleIndex_d, j, k];

                }
                if (uset[ruleIndex, j, 0] != "")
                    ledb = true;
            }
            chk[ruleIndex] = chk_d[ruleIndex_d];
            led[ruleIndex] = led_d[ruleIndex_d] = (ledb) ? 1 : 0;
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            
 
        }
        #region Class MyGridButtonXEditControl

        /// <summary>
        /// GridButtonXEditControl Class that controls the
        /// ButtonX color initialization and user button clicks.
        /// </summary>
        private class MyGridButtonXEditControl : GridButtonXEditControl
        {
            /// <summary>
            /// Constructor
            /// </summary>
            public MyGridButtonXEditControl()
            {
                // We want to be notified when the user clicks the button
                // so that we can change the underlying cell value to reflect
                // the mouse click.

                Click += MyGridButtonXEditControlClick;
            }

            #region InitializeContext

            /// <summary>
            /// Initializes the color table for the button
            /// </summary>
            /// <param name="cell"></param>
            /// <param name="style"></param>
            public override void
                InitializeContext(GridCell cell, CellVisualStyle style)
            {
                base.InitializeContext(cell, style);

                //bool running = Text.Equals("Stop") == false;

                //ColorTable = (running == true)
                //    ? eButtonColor.OrangeWithBackground
                //    : eButtonColor.BlueOrb;
            }

            #endregion

            #region MyGridButtonXEditControlClick

            /// <summary>
            /// Handles user clicks of the button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            void MyGridButtonXEditControlClick(object sender, EventArgs e)
            {
                //bool running = (EditorCell.Value != null &&
                //    EditorCell.Value.Equals("Start"));

               string a = EditorCell.GridColumn.Name;
               switch (a)
               {
                   case "gridColumnViewLocalRuleButtonSend":
                       EditorCell.Value = "发送中";
                       break;
                   case "gridColumnViewLocalRuleButtonClear":
                       EditorCell.Value = "清空中";
                       break;
                   case "gridColumnViewDeviceRuleButtonGet":
                       EditorCell.Value = "获取中";
                       break;
                   case "gridColumnViewDeviceRuleButtonDelete":
                       EditorCell.Value = "删除中";
                       break;
                   case "gridColumnViewDeviceRuleButtonGetNew":
                       EditorCell.Value = "添加中";
                       break;
                   default:
                       break;
               }
                //EditorCell.Value = (running == true) ? "Stop" : "Start";
                //MessageBox.Show("chengong");

            }

            #endregion
        }

        #endregion

        private void superGridControlLocalRule_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            GridCell cell = e.GridCell;
            //if (cell.GridColumn.Name.Equals("gridColumnViewLocalRuleButtonSend") == true && e.NewValue.ToString() == "发送中")
            //{
            //    //int rowIndex = cell.RowIndex;
            //    int a = int.Parse(cell.GridRow.Cells[0].Value.ToString());
            //    if (SendOneRule(a))
            //        cell.Value = "已发送";
            //    else
            //        cell.Value = "重新发送";
            //}
            string eNewValue = e.NewValue.ToString();

            switch (eNewValue)
            {
                case "发送中":
                    if (cell.GridColumn.Name.Equals("gridColumnViewLocalRuleButtonSend") == true)
                    {
                        int a = int.Parse(cell.GridRow.Cells[0].Value.ToString());
                        if (SendOneRule(a))
                            cell.Value = "已发送";
                        else
                            cell.Value = "发送本规则";
                    }
                    break;
                case "清空中":
                    if (cell.GridColumn.Name.Equals("gridColumnViewLocalRuleButtonClear") == true)
                    {
                        int a = int.Parse(cell.GridRow.Cells[0].Value.ToString());
                        if (MessageBox.Show("确认清空规则 " + a.ToString(), "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            ClearOneRule(a);
                            cell.Value = "已清空";
                        }
                        else
                            cell.Value = "清空本规则";
                    }
                    UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
                    UpdateSuperTabItemEditRule();
                    UpdateSuperTabItemViewRule();
                    break;
                   
            }

                
        }

        private void superGridControlDeviceRule_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            
            string eNewValue = e.NewValue.ToString();
            GridCell cell = e.GridCell;
            int ruleIndex = int.Parse(cell.GridRow.Cells[0].Value.ToString());
            switch (eNewValue)
            {
                case "获取中":
                    GetOneRuleFromDRuleToRule(ruleIndex,ruleIndex);
                    cell.Value = "以获取";
                    break;
                case "删除中":
                    DeleteOneDeviceRule(ruleIndex);
                    UpdateRuleViewDeviceRule();
                    break;
                case "True":
                    if (NotBindingDeviceData)
                    {
                        if (comm.IsOpen)
                        {
                            chk_d[ruleIndex] = 1;
                            setenable();
                        }
                        else
                        {
                            MessageBox.Show("请先打开端口！");
                        }
                    }
                    break;
                case "False":
                    if (comm.IsOpen)
                    {
                        if (NotBindingDeviceData)
                        {
                            chk_d[ruleIndex] = 0;
                            setenable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先打开端口！");
                    }
                    break;
                case "添加中":
                    int i;
                    for (i = 1; i < 31; i++)
                        if (led[i] == 0)
                            break;
                    if (i == 31)
                    {
                        MessageBox.Show("30条规则已满！\n请删除后再添加");
                        break;
                    }
                    GetOneRuleFromDRuleToRule(ruleIndex,i);
                    cell.Value = "以获取";

                    break;
                default:
                    break;

                    

            }
        }

        private void DeleteOneDeviceRule(int ruleIndex)
        {
            /// if comm open but send erro   uset_d changed ,but different from device rule
            if (comm.IsOpen)
            {
                for (int j = 0; j < 32; j++)
                {
                    for (int k = 0; k < 3; k++)
                        uset_d[ruleIndex, j, k] = "";
                }
                led_d[ruleIndex] = 0;
                chk_d[ruleIndex] = 0;
                SendOneRule_D(ruleIndex);
                WriteLog("删除防火墙规则", "删除规则" + ruleIndex.ToString());
                
            }
            else
            {
                MessageBox.Show("设备未打开！");
            }
        }

        private void switchButton2_ValueChanged(object sender, EventArgs e)
        {
            UpdateRuleViewLocalRule();
        }

        private void switchButton3_ValueChanged(object sender, EventArgs e)
        {
            UpdateRuleViewDeviceRule();
        }
        #   endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(superGridControlDeviceRule.PrimaryGrid.Rows.Count.ToString());
            

        }

        private void superTabControl3_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (superTabControl3.SelectedTab == superTabItemLocalAllRule)
            {
                ribbonControl1.SelectedRibbonTabItem = ribbonTabItem2;
                UpdateRuleViewLocalRule();
            }
            else
            {
                ribbonControl1.SelectedRibbonTabItem = ribbonTabItem1;
                UpdateRuleViewDeviceRule();
            }
        }

        private void superGridControlDeviceRule_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            //MessageBox.Show(superGridControlDeviceRule.PrimaryGrid.Rows.Count.ToString());
            NotBindingDeviceData = false;
            string a = superGridControlDeviceRule.PrimaryGrid.Name;
            GridPanel gp =  superGridControlDeviceRule.FindGridPanel("",true);
            bool b = gp.IsSubPanel;

            NotBindingDeviceData = true;
            UpdateRuleViewDeviceRule_1();
        }

        /// <summary>
        /// only rule _d can set enable
        /// </summary>
        private void setenable()
        {
            if (comm.IsOpen)
            {
                string dd = "66";
                string ddstr = buildEN_D();
                ddstr = ddstr.ToUpper();

                if (ddstr.Length != 8)
                {
                    MessageBox.Show("EN_DIS长度不对");
                    return;
                }

                //if (MessageBox.Show("配置使能如下，确定发送\n"  + ddstr, "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                //{
                    comm.Write(dd);
                    send_count += dd.Length;//累加发送字节数
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(ddstr);
                    send_count += ddstr.Length;//累加发送字节数
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面

                    for (int i = 1; i < 31; i++)
                    {
                        bool p = false;
                        for (int j = 0; j < 32; j++)
                        {
                            if (uset_d[i, j, 0] != "")
                            {
                                p = true;
                                break;
                            }
                        }
                        led_d[i] = (p) ? 1 : 0;
                    }
                        //for (int i = 1; i < 31; i++)
                        //{
                        //    if (chk_d[i] == 1)
                        //    {
                        //        led_d[i] = 1;
                        //    }
                        //    else
                        //    {
                        //        led_d[i] = 0;
                        //    }
                        //}
                //}
                //else
                //{
                //    return;
                //}

            }
            else
            {
                MessageBox.Show("请先打开端口！");
            }

        }

        #region RibbonControl Tab 文件

        /// <summary>
        /// ribbon button text = "打开本地规则"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            importRule();
            UpdateSuperTabItemViewRule();
        }

        /// <summary>
        /// ribbon button text = "保存规则到本地"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            saveRule();
        }
       

        /// <summary>
        /// close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            if (this.threadLogManagement.IsAlive)
                this.threadLogManagement.Abort();
            if (this.threadUserManagement.IsAlive)
                this.threadUserManagement.Abort();
            this.Close();
        }
        #endregion

        #region RibbonControl Tab 设备
        /// <summary>
        ///  ribbon button text = "连接硬件防火墙"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem14_Click(object sender, EventArgs e)
        {
            if (comm.IsOpen)
            {
                MessageBox.Show("设备已经连接！");
            }
            else
            {
                try
                {
                    comm.Open();
                    if (comm.IsOpen)
                    {
                        MessageBox.Show("设备已成功连接！");
                        WriteLog("连接设备", "成功");
                    }
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    comm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// ribbon button text = "断开硬件防火墙"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem15_Click(object sender, EventArgs e)
        {
            if (!comm.IsOpen)
            {
                MessageBox.Show("设备尚未连接！");
            }
            else
            {
                try
                {
                    comm.Close();
                    if (!comm.IsOpen)
                    {
                        MessageBox.Show("设备已断开！");
                        WriteLog("断开设备", "断开");
                    }
                }
                catch (Exception ex)
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。
                    comm = new SerialPort();
                    //现实异常信息给客户。
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// ribbon button text = "查看防火墙规则"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            superTabControl2.SelectedTab = superTabItemViewRule;
            superTabControl3.SelectedTab = superTabItemDeviceAllRule;

            
        }

        /// <summary>
        /// ribbon button text = "获取全部防火墙规则"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem24_Click(object sender, EventArgs e)
        {
            GetDRuleFromDeviceToHostAndUpdateRuleViewDeviceRule();
            for (int i = 1; i < 31; i++)
                GetOneRuleFromDRuleToRule(i,i);
        }


        /// <summary>
        /// ribbon button text = "防火墙全阻塞"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem25_Click(object sender, EventArgs e)
        {
            if (comm.IsOpen)
            {
                Thread thdSub1;
                thdSub1 = new Thread(new ThreadStart(thread_buttonItem25_Click));
                thdSub1.Start();
            }
            else
            {
                MessageBox.Show("设备未打开！");
            }
        }

        private void thread_buttonItem25_Click()
        {
            for (int i = 1; i < 31; i++)
            {
                for (int j = 0; j < 32; j++)
                    for (int k = 0; k < 3; k++)
                        uset_d[i, j, k] = "";
                led_d[i] = 0;
                chk_d[i] = 0;
                SendOneRule_D(i);

            }
            UpdateRuleViewDeviceRule();
            WriteLog("配置防火墙全阻塞", "全部阻塞");
        }

        /// <summary>
        /// ribbon button text = "防火墙全通"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem26_Click(object sender, EventArgs e)
        {
            if (comm.IsOpen)
            {
                Thread thdSub1;
                thdSub1 = new Thread(new ThreadStart(thread_buttonItem26_Click));
                thdSub1.Start();
            }
            else
            {
                MessageBox.Show("设备未打开！");
            }
        }

        private void thread_buttonItem26_Click()
        {
            uset_d[1, 0, 0] = "0";
            uset_d[1, 0, 1] = "00";
            uset_d[1, 0, 2] = "00";
            for (int j = 1; j < 32; j++)
                for (int k = 0; k < 3; k++)
                    uset_d[1, j, k] = "";
            led_d[1] = 1;
            chk_d[1] = 1;
            SendOneRule_D(1);
            for (int i = 2; i < 31; i++)
            {
                for (int j = 0; j < 32; j++)
                    for (int k = 0; k < 3; k++)
                        uset_d[i, j, k] = "";
                led_d[i] = 0;
                chk_d[i] = 0;
                SendOneRule_D(i);
            }
            UpdateRuleViewDeviceRule();
            WriteLog("配置防火墙全通", "1号全通规则");
        }

        #endregion

        #region RibbonControl Tab 规则
        /// <summary>
         //ribbon button text = "发送所有规则"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem16_Click(object sender, EventArgs e)
        {
            Thread threadButtonItem16_Click = new Thread(new ThreadStart(Thread_buttonItem16_Click));
            threadButtonItem16_Click.Start();
            UpdateRuleViewDeviceRule();
        }
        private void Thread_buttonItem16_Click()
        {
            for (int i = 1; i < 31; i++)
                SendOneRule(i);
            UpdateRuleViewDeviceRule();
        }
        /// <summary>
        //ribbon button text = "查看详情"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem17_Click(object sender, EventArgs e)
        {
            superTabControl2.SelectedTab = superTabItemViewRule;
            superTabControl3.SelectedTab = superTabItemLocalAllRule;
        }

        /// <summary>
        //ribbon button text = "清空所有规则"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem23_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定清空所有规则？" , "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                for (int i = 1; i < 31; i++)
                    ClearOneRule(i);
            }
            UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
            UpdateSuperTabItemEditRule();
            UpdateSuperTabItemViewRule();
        }

        /// <summary>
        //ribbon button text = "设置长度"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem29_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("设置长度并发送？");
            if (setLengthRuleForm.ShowDialog() == DialogResult.OK)
            {
                setlength();
            }
        }

        private void setlength()
        {
            if (setLengthRuleForm.textBoxX1.Text == String.Empty || setLengthRuleForm.textBoxX2.Text == String.Empty)
            {
                MessageBox.Show("LEN不能为空");
                return;
            }
            string lena = setLengthRuleForm.textBoxX1.Text;
            string lenb = setLengthRuleForm.textBoxX2.Text;
            if ((int.Parse(lena) < 0 || int.Parse(lena) > 2047) || (int.Parse(lenb) < 0 && int.Parse(lenb) > 2047) || int.Parse(lena) > int.Parse(lenb))
            {
                MessageBox.Show("LEN设置错误");
                return;
            }
            len_d[0] =len[0] = lena;
            len_d[1] =len[1] = lenb;
            int irule = 31;
            string ruleDDstr = "";
            ruleDDstr += buildDD(irule, 0, lena);
            ruleDDstr += buildDD(irule, 1, lenb);
            for (int i = 2; i < 32; i++)
            {
                ruleDDstr += buildDD(irule, i, "00");
            }

            if (comm.IsOpen)
            {
                string dd = "DD";
                string ddstr = ruleDDstr;

                if (MessageBox.Show("确定发送长度限制规则如下 ：\n 最小长度" + len_d[0] + "\n最大长度" + len_d[1], "notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    comm.Write(dd);
                    send_count += dd.Length;//累加发送字节数
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    comm.Write(ddstr);
                    send_count += ddstr.Length;//累加发送字节数
                    //serialSend.labelSendCount.Text = "Send:" + send_count.ToString();//更新界面
                    WriteLog("设置长度规则",len[0]+"~"+len[1]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先打开端口！");
            }
        }

        #endregion

        #region RibbonControl Tab 用户中心

        /// <summary>
        //ribbon button text = "查看个人信息"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem20_Click(object sender, EventArgs e)
        {
            superTabControl2.SelectedTab = superTabItemPersonInformation;
            UpdatePersonInformation();
        }

        /// <summary>
        //ribbon button text = "编辑个人信息"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem21_Click(object sender, EventArgs e)
        {
            buttonItem20_Click(sender, e);
            buttonXEditPersonInfromation_Click(sender, e);
        }

        /// <summary>
        //ribbon button text = "修改密码"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem22_Click(object sender, EventArgs e)
        {
            buttonItem20_Click(sender, e);
            buttonXEditPersonSecret_Click(sender, e);
        }

        /// <summary>
        //ribbon button text = "切换用户"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem30_Click(object sender, EventArgs e)
        {
            comm.Close();
            this.FindForm().Hide();
            SerialPortSetForm dialog = new SerialPortSetForm();
            dialog.ShowDialog();//注意这里要模态显示对话框
            this.Close();
        }


        #endregion

        #region RibbonControl Tab 帮助
        /// <summary>
        /// ribbon button text = "关于"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem19_Click(object sender, EventArgs e)
        {
            string about = "DCS001" + "\n防火墙系统v1" + "\n" + "版权所有 侵权必究";
            MessageBox.Show(about);
        }
        #endregion

        #region RibbonControl Tab 管理员功能
        /// <summary>
        /// ribbon button text = "用户管理"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem28_Click(object sender, EventArgs e)
        {
            threadUserManagement = new Thread(new ThreadStart(_threadProcNewUserManagementForm));
            threadUserManagement.Start();
        }

        private void NewUserManagementForm()
        {

            UserManagementForm newForm = new UserManagementForm();
            newForm.Show(this);
            newForm.Activate();

        }

        void _threadProcNewUserManagementForm()
        {
            MethodInvoker mi = new MethodInvoker(NewUserManagementForm);
            BeginInvoke(mi);

        }


        /// <summary>
        /// ribbon button text = "日志管理"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem27_Click(object sender, EventArgs e)
        {
            threadLogManagement = new Thread(new ThreadStart(_threadProcNewLogManagementForm));
            threadLogManagement.Start();
        }

        private void NewLogManagementForm()
        {

            LogManagementForm newForm = new LogManagementForm();
            newForm.Show(this);
            newForm.Activate();

        }

        void _threadProcNewLogManagementForm()
        {
            MethodInvoker mi = new MethodInvoker(NewLogManagementForm);
            BeginInvoke(mi);

        }

        #endregion
        //

        #region PersonInformation
        private void UpdatePersonInformation()
        {
            textBoxXUserName.Text = _currectOperator._UserName;
            textBoxXName.Text = _currectOperator._name;
            textBoxXDepartment.Text = _currectOperator._department;
            textBoxXIDcardNo.Text = _currectOperator._IDcardNo;

            textBoxXphoneNo.Text = _currectOperator._phoneNo;
            textBoxXEmail.Text = _currectOperator._email;
            textBoxXIdentify.Text = (_currectOperator._identity==0)? "管理员":"操作员";

            textBoxXOldSecret.Text = "";
            textBoxXNewSecret1.Text = "";
            textBoxXNewSecret2.Text = "";

            labelXEditPersonInformationMessage.Text = "";


            labelXPersonInformaton8.Visible = false;
            labelXPersonInformaton9.Visible = false;
            labelXPersonInformaton10.Visible = false;
            textBoxXOldSecret.Visible = false;
            textBoxXNewSecret1.Visible = false;
            textBoxXNewSecret2.Visible = false;

            buttonXPersonInformationEditOK.Visible = false;
            buttonXPersonInformationEditFail.Visible = false;

            textBoxXUserName.ReadOnly = true;
            textBoxXDepartment.ReadOnly = true;
            textBoxXphoneNo.ReadOnly = true;
            textBoxXEmail.ReadOnly = true;
        }

        private void buttonXEditPersonInfromation_Click(object sender, EventArgs e)
        {
            textBoxXUserName.ReadOnly = false;
            textBoxXDepartment.ReadOnly = false;
            textBoxXphoneNo.ReadOnly = false;
            textBoxXEmail.ReadOnly = false;

            buttonXPersonInformationEditOK.Visible = true;
            buttonXPersonInformationEditFail.Visible = true;
        }

        private void buttonXEditPersonSecret_Click(object sender, EventArgs e)
        {
            labelXPersonInformaton8.Visible = true;
            labelXPersonInformaton9.Visible = true;
            labelXPersonInformaton10.Visible = true;
            textBoxXOldSecret.Visible = true;
            textBoxXNewSecret1.Visible = true;
            textBoxXNewSecret2.Visible = true;

            buttonXPersonInformationEditOK.Visible = true;
            buttonXPersonInformationEditFail.Visible = true;
        }

        private async void buttonXPersonInformationEditOK_Click(object sender, EventArgs e)
        {
            bool verification = true;
            string verificationFailMessage = "";
            #region verification some information and sercet 
            ///....
            textBoxXUserName.Text = textBoxXUserName.Text.Trim();
            if (textBoxXUserName.Text.Length < 3 || textBoxXUserName.Text.Length > 15)
            {
                verification = false;
                verificationFailMessage += "用户名长度应该在3~15个字符之间\n";
            }
            textBoxXDepartment.Text = textBoxXDepartment.Text.Trim();
            if (textBoxXDepartment.Text.Length < 0 || textBoxXDepartment.Text.Length > 30)
            {
                verification = false;
                verificationFailMessage += "部门名称长度应该在30个字符之间\n";
            }

            textBoxXphoneNo.Text = textBoxXphoneNo.Text.Trim();
            if (textBoxXphoneNo.Text.Length>0)
            if ((!Regex.IsMatch(textBoxXphoneNo.Text, @"\d+")) || textBoxXphoneNo.Text.Length < 2 || textBoxXphoneNo.Text.Length>14)
            {
                verification = false;
                verificationFailMessage += "电话号码格式不正确\n";
            }

            textBoxXEmail.Text = textBoxXEmail.Text.Trim();
            string EmailPattern = @"^([A-Za-z0-9]{1}[A-Za-z0-9_]*)@([A-Za-z0-9_]+)[.]([A-Za-z0-9_]*)$";//E-Mail地址格式的正则表达式
            if(textBoxXEmail.Text.Length>0)
                if ((!Regex.IsMatch(textBoxXEmail.Text, EmailPattern)) || textBoxXEmail.Text.Length>100)
            {
                verification = false;
                verificationFailMessage += "邮箱格式不正确\n";
            }

            if (textBoxXNewSecret1.Visible)
            {
                if (textBoxXOldSecret.Text != _currectOperator._secret)
                {
                    verification = false;
                    verificationFailMessage += "原密码错误\n";
                }
                if (textBoxXNewSecret1.Text == _currectOperator._secret)
                {
                    verification = false;
                    verificationFailMessage += "新密码和原密码相同\n";
                }
                if (textBoxXNewSecret1.Text != textBoxXNewSecret2.Text)
                {
                    verification = false;
                    verificationFailMessage += "两次输入的新密码不一致\n";
                }
                if (textBoxXNewSecret1.Text.Length < 5 || textBoxXNewSecret1.Text.Length>20)
                {
                    verification = false;
                    verificationFailMessage += "密码长度应该在5到20个字符之间\n";
                }
                else
                if (textBoxXNewSecret2.Text.Length < 5 || textBoxXNewSecret2.Text.Length > 20)
                {
                    verification = false;
                    verificationFailMessage += "密码长度应该在5到20个字符之间\n";
                }


            }

            #endregion
            if (verification)
            {
                if (comm.IsOpen)
                {
                    _currectOperator._UserName = textBoxXUserName.Text;
                    _currectOperator._department = textBoxXDepartment.Text;
                    _currectOperator._phoneNo = textBoxXphoneNo.Text;
                    _currectOperator._email = textBoxXEmail.Text;

                    _currectOperator._secret = (textBoxXNewSecret1.Visible) ? textBoxXNewSecret1.Text : _currectOperator._secret;
                    var filter = Builders<Person>.Filter.Eq("_id",_currectOperator._id);
                   // var filter = Builders<Person>.Filter.Eq("_name", _currectOperator._name);
                    var update = Builders<Person>.Update
                .Set("_UserName", _currectOperator._UserName).Set("_department", _currectOperator._department)
                .Set("_phoneNo", _currectOperator._phoneNo).Set("_email", _currectOperator._email)
                .Set("_secret", _currectOperator._secret);

                    var result = await collectionPerson.UpdateOneAsync(filter, update);

                    UpdatePersonInformation();
                   
                    ///then edit the device
                }
                else
                {
                    MessageBox.Show("请先打开端口");
                }
            }
            else
            {
                labelXEditPersonInformationMessage.Text = verificationFailMessage;
            }
        }

        private void buttonXPersonInformationEditFail_Click(object sender, EventArgs e)
        {
            UpdatePersonInformation();

            
        }


        #endregion

        private void buttonXEditRuleClear_Click(object sender, EventArgs e)
        {
            if (urule > -1)
            {//
                for (int jj = 0; jj < useqLength; jj++)
                {
                    int p;
                    p = findseq(useq+jj);
                    if (p != -1)
                    {
                        uset[urule, p, 0] = "";
                        uset[urule, p, 1] = "";
                        uset[urule, p, 2] = "";
                    }
                }
                bool ruleIsEmpty = true;
                for (int j = 0; j < 32; j++)
                {
                    if (uset[urule, j, 0] != "")
                        ruleIsEmpty = false;
                }

                if (ruleIsEmpty)
                    ClearOneRule(urule);
                UpdateGroupPanelRuleVisibleByledAndByteBackColorByByte();
                UpdateSuperTabItemEditRule();
            }
        }

        /// <summary>
        /// ribbon button text = "全规则不使能"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem31_Click(object sender, EventArgs e)
        {
            if (comm.IsOpen)
            {
                for (int i = 1; i < 31; i++)
                {
                    chk_d[i] = 0;
                }
                setenable();
                UpdateRuleViewDeviceRule();
            }
            else
            {
                MessageBox.Show("请先打开端口！");
            }
            
        }
        /// <summary>
        /// ribbon button text = "全规则使能"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem32_Click(object sender, EventArgs e)
        {
            if (comm.IsOpen)
            {
                for (int i = 1; i < 31; i++)
                {
                    bool p = false;
                    for (int j = 0; j < 32; j++)
                    {
                        if (uset_d[i, j, 0] != "")
                        {
                            p = true;
                            break;
                        }
                    }
                    if(p)
                        chk_d[i] = 1;
                }
                setenable();
                UpdateRuleViewDeviceRule();
            }
            else
            {
                MessageBox.Show("请先打开端口！");
            }
        }

        private void superTabItem3_Click(object sender, EventArgs e)
        {
            _currentByteOrField = false;
            SuperTabStripSelectedTabChangedEventArgs ee = new SuperTabStripSelectedTabChangedEventArgs(superTabItem1, superTabItem1,new eEventSource());
            superTabControl1_SelectedTabChanged(sender, ee);
        }

        




    }


}
