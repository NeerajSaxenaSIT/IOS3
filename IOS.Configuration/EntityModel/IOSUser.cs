using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOS.Configuration.EntityModel
{
    public class IOSUser
    {
        public bool IsValidUser { get; set; }
        public int LicenseID { get; set; }
        public string LicenseType { get; set; }
        public string LicenseCompany { get; set; }
        public string LicenseUser { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string WebClientUserName { get; set; }
        public string WebClientPassword { get; set; }
        public bool IsPowerUser { get; set; }
        public string UserMarket { get; set; }
        public bool IsUserLocked { get; set; } = false;
        public bool IsUserEnabled { get; set; } = true;
    }
}
