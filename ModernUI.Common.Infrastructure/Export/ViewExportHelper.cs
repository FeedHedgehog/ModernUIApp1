namespace ModernUI.Common.Infrastructure.Export
{
    using System.Linq;
    using System.Windows;

    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// 视图导出帮助类
    /// </summary>
    public static class ViewExportHelper
    {
        /// <summary>
        /// 根据视图名称查找对应的视图
        /// </summary>
        /// <typeparam name="T">视图的具体类型</typeparam>
        /// <param name="viewName">视图名称</param>
        /// <returns>查找到相应视图返回true,否则返回null</returns>
        public static T FindExportViewByName<T>(string viewName)
        {
            var views = ServiceLocator.Current.GetAllInstances(typeof(FrameworkElement));
            var view = (T)views.FirstOrDefault(v => v.GetType().Name.Contains(viewName));

            return view;
        }
    }
}