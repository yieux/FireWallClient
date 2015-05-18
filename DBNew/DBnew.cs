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

namespace DBNew
{
    public partial class DBnew: Office2007Form
    {
        public DBnew()
        {
            InitializeComponent();
        }

        private async void buttonX1_Click(object sender, EventArgs e)
        {
            string connectionString = "mongodb://admin:admin@localhost";
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("DCS001");
            var collection = database.GetCollection<Person>("User");
            await collection.DeleteManyAsync(x => x._IDcardNo != "波风小蒙@");

            await collection.InsertOneAsync(new Person
            {
                _UserName = "admin",
                _secret = "admin",
                _name = "波风小蒙",
                _department = "办公室",
                _IDcardNo = "123485896970183938",
                _phoneNo = "13401157627",
                _email = "yangxueyi79315@hotmail.com",
                _identity = 0
            });
            await collection.InsertOneAsync(new Person
            {
                _UserName = "user1",
                _secret = "user1",
                _name = "刘备",
                _department = "操作室",
                _IDcardNo = "123485896556643938",
                _phoneNo = "",
                _email = "",
                _identity = 1
            });
            await collection.InsertOneAsync(new Person
            {
                _UserName = "user2",
                _secret = "user2",
                _name = "曹操",
                _department = "操作室",
                _IDcardNo = "123485822334123938",
                _phoneNo = "13401157333",
                _email = "",
                _identity = 1
            });
        }

        private async void buttonX2_Click(object sender, EventArgs e)
        {
            string connectionString = "mongodb://admin:admin@localhost";
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("DCS001");
            var collection = database.GetCollection<Log>("Log");
            await collection.DeleteManyAsync(x => x.operationName != "波风小蒙@");
        }
    }

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
