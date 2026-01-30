using System;
using System.Configuration;
namespace IOS.Configuration
{
    public class IOSAppConfigManage
    {
        public static string DriveTestFilePath
        {
            get { return ConfigurationManager.AppSettings["DriveTestFilesPath"].ToString(); }
        }
        public static string DeploymentName
        {
            get { return ConfigurationManager.AppSettings["DeploymentName"].ToString(); }
        }
        public static string IOSServer
        {
            get { return ConfigurationManager.ConnectionStrings["IOSServer"].ToString(); }
        }
        public static string DriveTest
        {
            get { return ConfigurationManager.ConnectionStrings["DriveTest"].ToString(); }
        }
        public static string HostServer
        {
            get { return ConfigurationManager.AppSettings["CellSens Server"].ToString(); }
        }
        public static string Company
        {
            get { return ConfigurationManager.AppSettings["Company"].ToString(); }
        }
        public static string IOSLicenseServer
        {
            get { return ConfigurationManager.ConnectionStrings["IOSLicenseServer"].ToString(); }
        }
        public static Boolean ReloadOnStartup
        {
            get { return (ConfigurationManager.AppSettings["ReloadOnStartup"].ToString() == "1" ? true : false); }
        }
        public static Boolean VersionCheck
        {
            get { return (ConfigurationManager.AppSettings["VersionCheck"].ToString() == "1" ? true : false); }
        }
        public static string AutoUpdateServer
        {
            get { return ConfigurationManager.AppSettings["AutoUpdate Server"].ToString(); }
        }
        public static string SupportWebURL
        {
            get { return ConfigurationManager.AppSettings["SupportWebURL"].ToString(); }
        }
        public static string SupportUserEmail
        {
            get { return ConfigurationManager.AppSettings["SupportUserEmail"].ToString(); }
        }
        public static string SupportUserPswd
        {
            get { return ConfigurationManager.AppSettings["SupportUserPswd"].ToString(); }
        }
        public static string Department
        {
            get { return ConfigurationManager.AppSettings["Department"].ToString(); }
        }
        public static string SandBox_Huawei
        {
            get { return ConfigurationManager.ConnectionStrings["SandBoxHuawei"].ToString(); }
        }
        public static string SandBox_Server
        {
            get { return ConfigurationManager.ConnectionStrings["SandBoxServer"].ToString(); }
        }
        public static string ProxyServer
        {
            get { return ConfigurationManager.AppSettings["Proxy Server"].ToString(); }
        }
        public static bool UseProxyForAutoUpdate
        {
            get { return (ConfigurationManager.AppSettings["UseProxyForAutoUpdate"].ToString() == "1" ? true : false); }
        }
        public static string GetSaveEricssonXmlFilePath
        {
            get { return (ConfigurationManager.AppSettings["SaveXmlEricsson"].ToString()); }
        }
        public static string WebReportServer
        {
            get { return ConfigurationManager.ConnectionStrings["WebReportServer"].ToString(); }
        }
        //public class ImportFile
        //{
        //    public static bool IsImportFile
        //    {
        //        get { return (ConfigurationManager.AppSettings["IsImportFile"].ToString() == "1" ? true : false); }
        //    }
        //    public static string Host
        //    {
        //        get { return ConfigurationManager.AppSettings["ImportFileHost"].ToString(); }
        //    }
        //    public static string User
        //    {
        //        get { return ConfigurationManager.AppSettings["ImportFileHostUser"].ToString(); }
        //    }
        //    public static string Password
        //    {
        //        get { return ConfigurationManager.AppSettings["ImportFileHostPassword"].ToString(); }
        //    }
        //}
    }
}