using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.IO
{
    public class FileUtil
    {
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否存在</returns>
        public static bool IsFileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        public static FileStream ReadFileStream(string filePath)
        {
            FileStream fs = null;

            //确保文件存在 ，并且参数合法
            if (IsFileExist(filePath))
            {
                try
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                }
                catch (Exception ex)
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                    throw ex;
                }
            }

            return fs;
        }

        public static byte[] ReadFileByte(string filePath)
        {
            using (var fs = ReadFileStream(filePath))
            {
                if (fs == null)
                    return null;
                var bytes = new byte[fs.Length];
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        /// <summary>
        /// 打开文件夹并默认选中文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void OpenFolderWithSelectFile(string filePath)
        {
            System.Diagnostics.Process.Start("Explorer", "/select," + filePath);
        }

        public static void OpenFile(string filePath)
        {
            System.Diagnostics.Process.Start(filePath);
        }

        /// <summary>
        /// 判断进程是否已经被启动
        /// </summary>
        /// <param name="name">进程名称</param>
        /// <returns>是否已经启动</returns>
        public static bool ProcessHasStarted(string name)
        {
            var result = false;
            Process[] app = Process.GetProcessesByName(name);
            if (app.Length>0)
            {   
                result = true;
            }
            return result;
        }
    }
}
