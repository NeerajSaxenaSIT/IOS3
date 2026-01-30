using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOS.Configuration.EntityModel
{
    public class IOSAdminInput
    {
        public string serverName { get; set; }
        public string DataBaseName { get; set; }
        public string DBUserName { get; set; }
        public string DBPassword { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ExcelPath { get; set; }
        public string EncrytedString { get; set; }
        public static string getConnectionString(string serverName, string database, string username, string password)
        {
            string connString = "Data Source={0};Initial Catalog={1};User Id={2}; Password={3};Connect Timeout=1000;";
            return string.Format(connString,serverName, database, username, password);
        }
    }
}
