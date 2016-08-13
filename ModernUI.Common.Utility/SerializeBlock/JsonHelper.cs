using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ModernUI.Common.Utility.SerializeBlock
{
    /// <summary>
    /// json序列化、反序列化帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="encoding">字符串编码格式</param>
        /// <returns>结果对象</returns>
        public static T Deserialize<T>(string jsonStr, Encoding encoding)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(encoding.GetBytes(jsonStr)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="jsonObject">序列化对象</param>
        /// <param name="encoding">字符串编码格式</param>
        /// <returns>json字符串</returns>
        public static string Serialize(object jsonObject, Encoding encoding)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return encoding.GetString(ms.ToArray());
            }
        }
    }
}
