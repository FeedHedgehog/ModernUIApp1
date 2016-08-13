using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace ModernUI.Common.Utility.FilHelper
{
    /// <summary>
    /// 压缩帮助类
    /// </summary>
    public class CompressHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="zipFilePath">压缩目的文件名</param>
        public static void CompressFile(string filePath, string zipFilePath)
        {
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(zipFilePath)))
            {
                WriteFileToZipStream(zipOutputStream, filePath);
            }
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="directory">文件夹名称</param>
        /// <param name="zipFilePath">压缩目的文件名</param>
        public static void CompressDirectory(string directory, string zipFilePath)
        {
            //如果不存在目的文件夹，就创建
            string destDirectoryName = System.IO.Path.GetDirectoryName(zipFilePath);         
            if (!System.IO.Directory.Exists(destDirectoryName))
            {
                System.IO.Directory.CreateDirectory(destDirectoryName);
            }

            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(directory);
            string parentDirectory = rootDirectoryInfo.Parent == null ? string.Empty : (rootDirectoryInfo.Parent.FullName + "\\");
            Stack<string> directorys = new Stack<string>();
            directorys.Push(directory);
            using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(zipFilePath)))
            {
                while (directorys.Count > 0)
                {
                    string directoryName = directorys.Pop();
                    DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
                    WriteDirectoryToZipStream(zipOutputStream, directoryInfo.FullName, directoryInfo.FullName.Substring(parentDirectory.Length, directoryInfo.FullName.Length - parentDirectory.Length));
                    foreach (string childDirectory in System.IO.Directory.GetDirectories(directoryName))
                    {
                        directorys.Push(childDirectory);
                    }
                    foreach (string childFile in System.IO.Directory.GetFiles(directoryName))
                    {
                        FileInfo fileInfo = new FileInfo(childFile);
                        WriteFileToZipStream(zipOutputStream, fileInfo.FullName, fileInfo.FullName.Substring(parentDirectory.Length, fileInfo.FullName.Length - parentDirectory.Length));
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="zipFilePath">压缩文件名</param>
        /// <param name="targetDirectory">目的文件夹</param>
        public static void Decompress(string zipFilePath, string targetDirectory = "")
        {
            if (string.IsNullOrEmpty(targetDirectory))
            {
                targetDirectory = new System.IO.FileInfo(zipFilePath).DirectoryName;
            }
            FastZip zip = new FastZip();
            zip.ExtractZip(zipFilePath, targetDirectory, string.Empty);
        }

        /// <summary>
        /// 把文件写入压缩流
        /// </summary>
        /// <param name="zipOutputStream">压缩流</param>
        /// <param name="sourceFileName">文件</param>
        /// <param name="zipFileName">压缩路径</param>
        /// <param name="comment">备注</param>
        private static void WriteFileToZipStream(ZipOutputStream zipOutputStream, string sourceFileName, string zipFileName = "", string comment = "")
        {
            using (FileStream fs = File.OpenRead(sourceFileName))
            {
                if (string.IsNullOrEmpty(zipFileName))
                {
                    zipFileName = System.IO.Path.GetFileName(sourceFileName);
                }
                byte[] buffer = new byte[1024 * 4];
                ZipEntry entry = new ZipEntry(zipFileName);
                entry.Comment = comment;
                zipOutputStream.PutNextEntry(entry);
                int sourceBytes;
                do
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                    zipOutputStream.Write(buffer, 0, sourceBytes);
                }
                while (sourceBytes > 0);
            }
        }

        /// <summary>
        /// 把文件夹写入压缩流
        /// </summary>
        /// <param name="zipOutputStream">压缩流</param>
        /// <param name="sourceDirectoryName">文件夹</param>
        /// <param name="zipFileName">压缩路径</param>
        /// <param name="comment">备注</param>
        private static void WriteDirectoryToZipStream(ZipOutputStream zipOutputStream, string sourceDirectoryName, string zipFileName = "", string comment = "")
        {
            if (string.IsNullOrEmpty(zipFileName))
            {
                zipFileName = new System.IO.DirectoryInfo(sourceDirectoryName).Name;
            }
            ZipEntry entry = new ZipEntry(zipFileName + "/");
            entry.Comment = comment;
            zipOutputStream.PutNextEntry(entry);
            zipOutputStream.Flush();
        }
    }
}
