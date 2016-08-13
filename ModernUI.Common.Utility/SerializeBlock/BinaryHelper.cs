using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ModernUI.Common.Utility.SerializeBlock
{
    /// <summary>
    /// 对象序列化为二进制
    /// </summary>
    public static class BinaryHelper
    {
        public static byte[] Serialize(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                var bytes = new byte[ms.Length];
                ms.Read(bytes, 0, (int)ms.Length);
                return bytes;
            }
        }

        public static T Deserialize<T>(byte[] buffer)
        {
            return (T)Deserialize(buffer);
        }

        private static object Deserialize(byte[] buffer)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                ms.Write(buffer, 0, buffer.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(ms);
            }
        }
    }
}
