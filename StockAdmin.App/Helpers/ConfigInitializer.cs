using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SigiFluent.Helpers
{
    class ConfigInitializer
    {
        public static string DirectoryName;

        public static string FileName
        {
            get
            {
                var localConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                                    .FilePath;

                return Path.GetFileName(localConfig);
            }
        }

        public static string ConfigPath
        {
            get
            {
                var myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                return String.Format("{0}\\{1}\\{2}", myDocsPath, DirectoryName, FileName);
            }
        }

        public static void BackupConfig()
        {
            if (File.Exists(ConfigPath))
            {
                File.Move(ConfigPath, GetBackupFilename());
            }
        }

        public static bool ExistConfig()
        {
            return File.Exists(ConfigPath);
        }

        public static void CreateConfig()
        {

            var configDirectory = string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DirectoryName);

            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            var localConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                        .FilePath;

            File.Copy(localConfig, ConfigPath);
        }

        public static void DeleteConfig()
        {
            if (ExistConfig())
                File.Delete(ConfigPath);
        }

        public static void FixConfig()
        {
            FixConfigSectionType();
        }

        

        private static void FixConfigSectionType()
        {
            var configXml = XDocument.Load(ConfigPath);

            var configSection = (from s in configXml.Descendants("configuration")
                                                    .Descendants("configSections")
                                 select s.Element("section")).FirstOrDefault();

            if (configSection != null)
            {
                var typeAttribute = configSection.Attribute("type");
                 
                configXml.Save(ConfigPath);
            }

        }

        private static string GetBackupFilename()
        {
            string configBakPath;
            var i = 0;

            do
            {
                i++;
                configBakPath = String.Format("{0}.bak{1:000}", ConfigPath, i);
            } while (File.Exists(configBakPath));

            return configBakPath;
        }
    }
}
