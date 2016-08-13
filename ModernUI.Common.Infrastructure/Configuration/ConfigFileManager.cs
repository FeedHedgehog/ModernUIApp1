namespace ModernUI.Common.Infrastructure.Configuration
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// .config文件的配置管理器
    /// </summary>
    public class ConfigFileManager : IConfigurationManager
    {
        #region Singleton

        private static readonly ConfigFileManager Manager = new ConfigFileManager();

        private ConfigFileManager()
        {
        }

        public static ConfigFileManager Instance
        {
            get
            {
                return Manager;
            }
        }

        #endregion Singleton

        #region IConfigurationManager

        /// <summary>
        /// 刷新配置信息,从文件中读取
        /// </summary>
        /// <param name="configuration">配置信息</param>
        public void Refresh(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (!File.Exists(configuration.FilePath))
            {
                throw new FileNotFoundException("文件不存在", configuration.FilePath);
            }

            var configurationInfo =
                ConfigurationManager.OpenMappedMachineConfiguration(new ConfigurationFileMap(configuration.FilePath));
            var settings = configurationInfo.AppSettings.Settings;
            var keys = configurationInfo.AppSettings.Settings.AllKeys;
            foreach (var key in keys)
            {
                string value = settings[key].Value;

                ////配置文件必须设置默认值,否则容易出错
                //if (string.IsNullOrEmpty(value))
                //{
                //    throw new Exception(string.Format("{0}未设置默认值", key));
                //}
                var node = new ConfigurationNode { ConfigurationName = key, Value = value };
                configuration.SetField(node);
            }
        }

        /// <summary>
        /// 持久化配置信息
        /// </summary>
        /// <param name="configuration">配置信息</param>
        public void Persist(IConfiguration configuration)
        {
            var nodes = configuration.GetConfigurationNodes();
            var configurationInfo =
                ConfigurationManager.OpenMappedMachineConfiguration(new ConfigurationFileMap(configuration.FilePath));
            var settings = configurationInfo.AppSettings.Settings;
            foreach (var node in nodes)
            {
                if (settings.AllKeys.Contains(node.ConfigurationName))
                {
                    settings[node.ConfigurationName].Value = node.Value;
                }
                else
                {
                    settings.Add(node.ConfigurationName, node.Value);
                }
            }
            configurationInfo.Save(ConfigurationSaveMode.Modified);
        }

        #endregion IConfigurationManager
    }
}