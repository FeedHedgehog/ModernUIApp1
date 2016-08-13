namespace ModernUI.Common.Infrastructure.Configuration
{
    using System.IO;

    /// <summary>
    /// 配置管理的工厂
    /// </summary>
    public static class ConfigurationManagerFactory
    {
        /// <summary>
        /// 根据文件路径创建配置管理类
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>如果创建成功返回配置管理类,否则返回null</returns>
        public static IConfigurationManager CreateConfigurationManager(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (extension == ".config")
            {
                return ConfigFileManager.Instance;
            }

            return null;
        }
    }
}