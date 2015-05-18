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
    public partial class LogManagementForm : DevComponents.DotNetBar.OfficeForm
    {
        public LogManagementForm()
        {
            //_currectOperatorId = currectOperatorId;
            InitializeComponent();
        }
        public ObjectId _currectOperatorId;
        public MongoClient client;
        public IMongoDatabase database;
        public IMongoCollection<Person> collectionPerson;
        public IMongoCollection<Log> collectionLog;
        public List<Person> listOperator;



        private async void superGridControl2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                ObjectId a = listOperator[e.RowIndex]._id;
                var listLog = await collectionLog.Find(x => x.operatorId == a).ToListAsync();
                
                superGridControl1.PrimaryGrid.DataSource = listLog;
                
            }
        }

        private async void LogManagementFormLoad(object sender, EventArgs e)
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("DCS001");
            collectionPerson = database.GetCollection<Person>("User");
            collectionLog = database.GetCollection<Log>("Log");

            listOperator = await collectionPerson.Find(x => x._name != "波风小蒙@").ToListAsync();
            superGridControl2.PrimaryGrid.DataSource = listOperator;
            

            var listLog = await collectionLog.Find(x => x.datetime != DateTime.Now).ToListAsync();
            superGridControl1.PrimaryGrid.DataSource = listLog;
            
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            superGridControl1.PrimaryGrid.Columns["id"].Visible = false;
            superGridControl1.PrimaryGrid.Columns["operatorId"].Visible = false;
            superGridControl1.PrimaryGrid.Columns["lastModified"].Visible = false;
            superGridControl1.PrimaryGrid.Columns["operatorName"].HeaderText = "操作员名字";
            superGridControl1.PrimaryGrid.Columns["operationName"].HeaderText = "操作内容";
            superGridControl1.PrimaryGrid.Columns["remark"].HeaderText = "备注";
            superGridControl1.PrimaryGrid.Columns["datetime"].HeaderText = "操作时间";
        }

        private async void superGridControl2_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            GridCell cell = e.GridCell;
            int ruleIndex = cell.GridRow.RowIndex;
            if (ruleIndex > -1)
            {
                ObjectId a = listOperator[ruleIndex]._id;
                var listLog = await collectionLog.Find(x => x.operatorId == a).ToListAsync();

                superGridControl1.PrimaryGrid.DataSource = listLog;

            }
        }

        private void superGridControl2_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            superGridControl2.PrimaryGrid.Columns["_id"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_secret"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_identity"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_UserName"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_department"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_phoneNo"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_IDcardNo"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_email"].Visible = false;
            superGridControl2.PrimaryGrid.Columns["_name"].HeaderText = "操作员名单";
        }
    
    }
}