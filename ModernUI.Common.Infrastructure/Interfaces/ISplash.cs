namespace ModernUI.Common.Infrastructure.Interfaces
{
    using System;

    /// <summary>
    /// Splash的接口
    /// </summary>
    public interface ISplash
    {
        /// <summary>
        /// 是否启用Splash
        /// </summary>
        bool EnableSplash { get; set; }

        /// <summary>
        /// 更新Splash的展示信息
        /// </summary>
        Action<string> UpdateSplashInfo { get;}
    }
}