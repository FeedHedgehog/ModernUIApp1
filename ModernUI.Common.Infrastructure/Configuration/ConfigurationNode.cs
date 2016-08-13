namespace ModernUI.Common.Infrastructure.Configuration
{
    /// <summary>
    /// 配置节点信息
    /// </summary>
    public class ConfigurationNode
    {
        /// <summary>
        /// 父结点
        /// </summary>
        public ConfigurationNode Parent { get; set; }

        /// <summary>
        /// 配置节点名称
        /// </summary>
        public string ConfigurationName { get; set; }

        /// <summary>
        /// 配置节点属性
        /// </summary>
        public string Value { get; set; }
    }
}