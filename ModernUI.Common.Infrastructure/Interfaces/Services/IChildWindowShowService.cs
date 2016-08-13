namespace ModernUI.Common.Infrastructure.Interfaces
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// 在控件内部展示对话框的服务
    /// </summary>
    public interface IChildWindowShowService
    {
        /// <summary>
        /// 在控件内部展示对话框
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="content">对话框内容</param>
        /// <param name="target">对话框所在控件</param>
        /// <param name="margin">与各边距离</param>
        /// <param name="isModal">是否为模态</param>
        /// <returns>添加了内容的对话框</returns>
        ContentControl ShowChildWindow(string title, object content, UIElement target, Thickness margin, bool isModal);
    }
}