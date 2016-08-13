using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ModernUI.Common.Utility.FilHelper
{
    public static class MD5Helper
    {
        private const int BufferSize = 1024 * 1024;

        public static string FileCalculate(string fileFullPath)
        {
            StringBuilder strMD = null;
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Open))
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                md5.Initialize();
                byte[] buffer = null;
                if (fs.Length > BufferSize)
                {
                    buffer = new byte[BufferSize];
                }
                else
                {
                    buffer = new byte[(int)fs.Length];
                }
                while (fs.Position < fs.Length)
                {
                    int readSize = BufferSize;
                    if (fs.Position + readSize > fs.Length)
                    {
                        readSize = (int)(fs.Length - fs.Position);
                    }

                    fs.Read(buffer, 0, readSize);
                    if (fs.Position < fs.Length)
                    {
                        md5.TransformBlock(buffer, 0, readSize, buffer, 0);
                    }
                    else
                    {
                        md5.TransformFinalBlock(buffer, 0, readSize);
                    }
                }
                fs.Close();
                byte[] result = md5.Hash;
                md5.Clear();
                strMD = new StringBuilder(32);
                foreach (byte b in result)
                {
                    strMD.Append(b.ToString("x2"));
                }
            }
            return strMD == null ? null : strMD.ToString();
        }
    }
}
