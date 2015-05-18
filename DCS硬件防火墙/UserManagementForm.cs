using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using MongoDB.Bson;
using MongoDB.Driver;

using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DCS硬件防火墙
{
    public partial class UserManagementForm : DevComponents.DotNetBar.OfficeForm
    {
        public Person _currectOperator;
        public MongoClient client;
        public IMongoDatabase database;
        public IMongoCollection<Person> collectionPerson;
        public IMongoCollection<Log> collectionLog;
        public List<Person> listOperator;
        object _currentSelect;
        Random ra = new Random();
        public UserManagementForm()
        {
            InitializeComponent();
            UpdateUserData();
            UpdateNormalFormState();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            UpdateUserData();
            UpdateNormalFormState();
        }

        private async void UpdateUserData()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("DCS001");
            collectionPerson = database.GetCollection<Person>("User");
            collectionLog = database.GetCollection<Log>("Log");
            listOperator = await collectionPerson.Find(x => x._name != "波风小蒙@").ToListAsync();
            //var filter = Builders<Person>.Filter.All("_id", "");// Eq("_id", _currectOperator._id);
            superGridControl1.PrimaryGrid.DataSource = listOperator;

        }

        private void UpdateNormalFormState()
        {
            labelX1.Visible = false;
            labelX2.Visible = false;
            labelX3.Visible = false;
            labelX4.Visible = false;

            textBoxX1.Visible = false;
            textBoxX2.Visible = false;
            textBoxX3.Visible = false;
            textBoxX4.Visible = false;

            buttonX3.Visible = false;
            buttonX4.Visible = false;
            buttonX5.Visible = false;

            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX3.Text = "";
            textBoxX4.Text = "";
            _currentSelect = -2;

            labelX5.Text = "";
        }

        private void UpdateEditFormState()
        {
            labelX1.Visible = true;
            labelX2.Visible = true;
            labelX3.Visible = true;
            labelX4.Visible = true;

            textBoxX1.Visible = true;
            textBoxX2.Visible = true;
            textBoxX3.Visible = true;
            textBoxX4.Visible = true;

            buttonX3.Visible = true;
            buttonX4.Visible = true;
            buttonX5.Visible = true;

            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX3.Text = "";
            textBoxX4.Text = "";

            labelX5.Text = "";
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {

            superGridControl1.PrimaryGrid.Columns["_id"].Visible = false;
            superGridControl1.PrimaryGrid.Columns["_secret"].Visible = false;

            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {
                GridRow gr = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                int j = int.Parse(gr.Cells["_identity"].Value.ToString());
                gr.Cells["identify"].Value = (j == 0) ? "管理员" : "操作员";
            }

            superGridControl1.PrimaryGrid.Columns["_identity"].Visible = false;
        }

        /// <summary>
        /// 用户名生成方法是权宜之计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonX2_Click(object sender, EventArgs e)
        {
            UpdateEditFormState();
            _currentSelect = -1;
            textBoxX4.Text = "操作员";
            //textBoxX1.Text 
            string newUserName = "user10000";
            bool noNewName = true;
            while (noNewName)
            {
                newUserName = "user" + ra.Next(0, 10000).ToString();
                var filter = Builders<Person>.Filter.Eq("_id", _currentSelect);
                var result = await collectionPerson.Find(filter).ToListAsync();
                noNewName = false;
                foreach (var person in result)
                {
                    noNewName = true;
                }
            }
            textBoxX1.Text = newUserName;
            buttonX3.Text = "确认新增";

        }

        private void superGridControl1_RowClick(object sender, GridRowClickEventArgs e)
        {
            UpdateEditFormState();
            GridRow gr = e.GridRow as GridRow;
            _currentSelect = gr.Cells["_id"].Value;
            textBoxX1.Text = gr.Cells["_UserName"].Value.ToString();
            textBoxX2.Text = gr.Cells["_name"].Value.ToString();
            textBoxX3.Text = gr.Cells["_IDcardNo"].Value.ToString();
            textBoxX4.Text = gr.Cells["identify"].Value.ToString();
            buttonX3.Text = "确认修改";
        }

        private async void buttonX3_Click(object sender, EventArgs e)
        {
            bool verification = true;
            string verificationFailMessage = "";
            textBoxX2.Text = textBoxX2.Text.Trim();
            if (textBoxX2.Text.Length < 1 || textBoxX2.Text.Length > 16)
            {
                verification = false;
                verificationFailMessage += "姓名长度应该在1~16个字符之间\n";
            }
            textBoxX3.Text = textBoxX3.Text.Trim();
            IDCardValidation card = new IDCardValidation();
            if (!card.CheckIDCard(textBoxX3.Text))
            {
                verification = false;
                verificationFailMessage += "请填写正确的身份证信息";
            }

            if (verification)
            {
                // -1 是新建
                if (_currentSelect.ToString() == (-1).ToString())
                {//没有验证



                    await collectionPerson.InsertOneAsync(new Person
                    {
                        _UserName = textBoxX1.Text,
                        _secret = "",
                        _name = textBoxX2.Text,
                        _department = "",
                        _IDcardNo = textBoxX3.Text,
                        _phoneNo = "",
                        _email = "",
                        _identity = 1
                    });

                    UpdateNormalFormState();
                    UpdateUserData();

                }
                else
                {
                    if (_currentSelect.ToString() != (-2).ToString())
                    {
                        var filter = Builders<Person>.Filter.Eq("_id", _currentSelect);
                        // var filter = Builders<Person>.Filter.Eq("_name", _currectOperator._name);
                        var update = Builders<Person>.Update
                    .Set("_name", textBoxX2.Text).Set("_IDcardNo", textBoxX3.Text);

                        var result = await collectionPerson.UpdateOneAsync(filter, update);
                        UpdateNormalFormState();
                        UpdateUserData();

                    }
                }
            }
            else
            {
                labelX5.Text = verificationFailMessage;
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            //没有验证
            UpdateNormalFormState();
        }

        private async void buttonX5_Click(object sender, EventArgs e)
        {
            var filter = Builders<Person>.Filter.Eq("_id", _currentSelect);
            var result = await collectionPerson.DeleteOneAsync(filter);

            UpdateNormalFormState();
            UpdateUserData();
        }

 

    }

    /// <summary>  
    /// 验证身份证号码类  
    /// </summary>  
    public class IDCardValidation
    {
        /// <summary>  
        /// 验证身份证合理性  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public bool CheckIDCard(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                bool check = CheckIDCard18(idNumber);
                return check;
            }
            else if (idNumber.Length == 15)
            {
                bool check = CheckIDCard15(idNumber);
                return check;
            }
            else
            {
                return false;
            }
        }


        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        private bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }


        /// <summary>  
        /// 16位身份证号码验证  
        /// </summary>  
        private bool CheckIDCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;
        }
    }
}