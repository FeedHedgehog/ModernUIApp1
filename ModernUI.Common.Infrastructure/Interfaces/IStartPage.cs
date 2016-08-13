namespace ModernUI.Common.Infrastructure.Interfaces
{
    using System;

    /// <summary>
    /// 启动页的接口
    /// </summary>
    public interface IStartPage
    {
        /// <summary>
        /// 是否启用启动页
        /// </summary>
        bool EnableStartPage { get; set; }

        /// <summary>
        /// 关闭时的响应
        /// </summary>
        Action OnClose { get; set; }
    }
}