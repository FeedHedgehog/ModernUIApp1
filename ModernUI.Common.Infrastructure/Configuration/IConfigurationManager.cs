namespace ModernUI.Common.Infrastructure.Configuration
{
    /// <summary>
    /// 配置管理的接口
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// 刷新配置信息,从文件中读取
        /// </summary>
        /// <param name="configuration">配置信息</param>
        void Refresh(IConfiguration configuration);

        /// <summary>
        /// 持久化配置信息
        /// </summary>
        /// <param name="configuration">配置信息</param>
        void Persist(IConfiguration configuration);
    }
}