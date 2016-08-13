/*----------------------------------------------
 *调用方法
 * var themes = ServiceLocator.Current.GetAllInstances<ITheme>();           
 *         [ImportMany] 
 *         public List<ITheme> themes { get; set; }  
 * ----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ModernUI.Common.Infrastructure.Interfaces
{
    /// <summary>
    /// 主题接口类，每个要实现的主题都从该接口加载
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// 主题的内部标识名，相当于ID
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 主题的显示名
        /// </summary>
        string ThemeName { get; }

        /// <summary>
        /// 主题的颜色体系
        /// </summary>
        string ThemeColor { get; }

        /// <summary>
        /// 主题要用来显示的图片
        /// </summary>
        string ThemeImg { get; }

        /// <summary>
        /// 该主题对应的资源列表
        /// </summary>
        IEnumerable<ResourceDictionary> Resources { get; }
    }

    /// <summary>
    /// 主题扩展方法
    /// </summary>
    public static class ThemeExtension
    {
        public static ResourceDictionary LoadResourceDictionary(this ITheme theme, Uri uri)
        {
            return Application.LoadComponent(uri) as ResourceDictionary;
        }

        public static bool HaveResource(this ITheme theme)
        {
            return theme.Resources != null && theme.Resources.Count() > 0;
        }
    }
}
