namespace ModernUI.Common.Infrastructure.Interfaces
{
    /// <summary>
    /// 预加载接口
    /// </summary>
    public interface IPreload
    {
        /// <summary>
        /// 预加载的信息
        /// </summary>
        string PreloadInfo { get; }

        /// <summary>
        /// 预加载
        /// </summary>
        void Preload();
    }
}