using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ModernUI.Common.Infrastructure.Interfaces
{
    public interface IThemeService
    {
        /// <summary>
        /// 当前应用程序资源
        /// </summary>
        ResourceDictionary MainResource { get; }
        
        /// <summary>
        /// 已检测到的主题列表
        /// </summary>
        List<ITheme> Themes { get; }
        
        /// <summary>
        /// 当前主题
        /// </summary>
        ITheme CurrentTheme { get; }
        
        /// <summary>
        /// 更改主题
        /// </summary>
        /// <param name="theme">主题对象</param>
        void ChangeTheme(ITheme theme);

        /// <summary>
        /// 通过主题名来更改更改主题
        /// </summary>
        /// <param name="themeName">主题名</param>
        void ChangeTheme(string themeName);
    }
}
