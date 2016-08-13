namespace ModernUI.Common.Infrastructure.Interfaces
{
    /// <summary>
    /// 消息展示服务的接口
    /// </summary>
    public interface IMessageShowService
    {
        /// <summary>
        /// 展示消息
        /// </summary>
        /// <param name="messageCategory">消息类别</param>
        /// <param name="message">消息内容</param>
        /// <param name="messageShowCategory">消息展示类别</param>
        void ShowMessage(MessageCategory messageCategory, string message, MessageShowCategory messageShowCategory);
    }

    /// <summary>
    /// 消息展示类别
    /// </summary>
    public enum MessageShowCategory
    {
        /// <summary>
        /// 状态栏
        /// </summary>
        Status,

        /// <summary>
        /// 模态对话框
        /// </summary>
        ModalDialog,

        /// <summary>
        /// 非模态对话框
        /// </summary>
        NonmodalDialog,

        /// <summary>
        /// 弹出消息
        /// </summary>
        Popup,
    }

    /// <summary>
    /// 消息类别
    /// </summary>
    public enum MessageCategory
    {
        /// <summary>
        /// 消息
        /// </summary>
        Info,

        /// <summary>
        /// 警告
        /// </summary>
        Warning,

        /// <summary>
        /// 错误
        /// </summary>
        Error,
    }
}