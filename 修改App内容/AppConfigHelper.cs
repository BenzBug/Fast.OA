using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 修改App内容
{
    public class AppConfigHelper
    {
        public static string GetValueByKey(string key)
        {
            ConfigurationManager.RefreshSection("appSettings");
            return ConfigurationManager.AppSettings[key];
        }
        public static void ModifyAppSettings(string strKey, string value)
        {
            //获得配置文件的全路径  
            var assemblyConfigFile = Assembly.GetEntryAssembly().Location;
            var appDomainConfigFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            ChangeConfiguration(strKey, value, assemblyConfigFile);
            ModifyAppConfig(strKey, value, appDomainConfigFile);
        }

        private static void ModifyAppConfig(string strKey, string value, string configPath)
        {
            var doc = new XmlDocument();
            doc.Load(configPath);

            //找出名称为“add”的所有元素  
            var nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性  
                var xmlAttributeCollection = nodes[i].Attributes;
                if (xmlAttributeCollection != null)
                {
                    var att = xmlAttributeCollection["key"];
                    if (att == null) continue;
                    //根据元素的第一个属性来判断当前的元素是不是目标元素  
                    if (att.Value != strKey) continue;
                    //对目标元素中的第二个属性赋值  
                    att = xmlAttributeCollection["value"];
                    att.Value = value;
                }
                break;
            }

            //保存上面的修改  
            doc.Save(configPath);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ChangeConfiguration(string key, string value, string path)
        {
            var config = ConfigurationManager.OpenExeConfiguration(path);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}

