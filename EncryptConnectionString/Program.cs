using System;
using System.Configuration;

namespace EncryptConnectionString
{
    class Program
    {        
        static void Main(string[] args)
        {
            string configPath = String.Empty;
            const string sectionToEncrypt = "connectionStrings";

            foreach (String arg in args)
            {
                configPath = configPath + " " + arg;
            }

            configPath += "CONFIGNAME.exe.config";

            var fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configPath;
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            ConfigurationSection section = configuration.GetSection(sectionToEncrypt);

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                section.SectionInformation.ForceSave = true;
                configuration.Save(ConfigurationSaveMode.Modified);

            }

        }
    }
}
