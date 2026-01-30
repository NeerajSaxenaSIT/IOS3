using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOS.Configuration.EntityModel
{
    class SandBoxUser
    {
        public bool IsValidUser
        {
            get { return m_IsValidUser; }
            set { m_IsValidUser = value; }
        }

        private bool m_IsValidUser;
        public int LicenseID
        {
            get { return m_LicenseID; }
            set { m_LicenseID = value; }
        }
        private int m_LicenseID;
        public string LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        private string m_LicenseType;
        public string LicenseCompany
        {
            get { return m_LicenseCompany; }
            set { m_LicenseCompany = value; }
        }
        private string m_LicenseCompany;
        public string LicenseUser
        {
            get { return m_LicenseUser; }
            set { m_LicenseUser = value; }
        }
        private string m_LicenseUser;
        public DateTime ExpirationDate
        {
            get { return m_ExpirationDate; }
            set { m_ExpirationDate = value; }
        }
        private DateTime m_ExpirationDate;

    }
}
