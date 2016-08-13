using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.IO
{
    public class PathUtil
    {
        /// <summary>
        /// 获取应用程序根目录
        /// </summary>
        /// <returns>根目录</returns>
        public static string GetAppRootPath()
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }

        /// <summary>
        /// 获取系统临时文件夹路径
        /// </summary>
        /// <returns>临时文件夹路径</returns>
        public static string GetTempPath()
        {
            return Path.GetTempPath();
        }

        /// <summary>
        /// 检测文件夹是否存在,如果不存在,则递归创建文件夹
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        public static void CheckFolderExist(string folder)
        {
            var stack = new Stack<string>();
            string dir = folder;
            while (!Directory.Exists(dir))
            {
                stack.Push(dir);
                dir = GetFolder(dir);
            }

            while (stack.Count>0)
            {
                dir = stack.Pop();
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// 获取指定路径的可用文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>可用文件夹</returns>
        public static string GetFolder(string path)
        {
            string folder = path;
            while (!Directory.Exists(folder))
            {
                folder = folder.Trim(new[] { Path.DirectorySeparatorChar });
                int lastSeparator = folder.LastIndexOf(Path.DirectorySeparatorChar);
                if (lastSeparator != -1)
                {
                    folder = folder.Substring(0, lastSeparator + 1);
                }
                else
                {
                    break;
                }
            }

            return folder;
        }
    }
}
