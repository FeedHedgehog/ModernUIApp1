namespace ModernUI.Common.Infrastructure.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// 配置信息
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// 关联的文件路径
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// 配置信息名称
        /// </summary>
        string ConfigurationName { get; set; }

        /// <summary>
        /// 刷新配置信息,从文件中读取
        /// </summary>
        void Refresh();

        /// <summary>
        /// 持久化配置信息
        /// </summary>
        void Persist();

        /// <summary>
        /// 根据配置节点信息设置字段的值
        /// </summary>
        /// <param name="configurationNode">配置节点的信息</param>
        void SetField(ConfigurationNode configurationNode);

        /// <summary>
        /// 获取所有配置节点信息
        /// </summary>
        /// <returns>所有配置节点信息</returns>
        IEnumerable<ConfigurationNode> GetConfigurationNodes();
    }
}