using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModernUI.Common.Utility
{
    public class EnumHelper
    {
        #region old
        /// <summary>
        /// 获得枚举值及其描述信息
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <returns>字典</returns>
        //public static Dictionary<int, List<DescriptionAttribute>> GetEnumValuesAndDescriptions(Type t)
        //{
        //    Dictionary<int, List<DescriptionAttribute>> dic = new Dictionary<int, List<DescriptionAttribute>>();
        //    if (t != null && t.IsEnum)
        //    {
        //        FieldInfo[] fieldInfos = t.GetFields(BindingFlags.Public | BindingFlags.Static);
        //        List<string> names = new List<string>();
        //        foreach (string name in t.GetEnumNames())
        //        {
        //            names.Add(name);
        //        }
        //        List<int> values = new List<int>();
        //        foreach (int value in t.GetEnumValues())
        //        {
        //            values.Add(value);
        //        }

        //        foreach (FieldInfo fidldInfo in fieldInfos)
        //        {
        //            int index = names.IndexOf(fidldInfo.Name);
        //            if (index > 0)
        //            {
        //                object[] attributes = fidldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //                List<DescriptionAttribute> descriptionAttributes = new List<DescriptionAttribute>();
        //                foreach (DescriptionAttribute descriptionAttribute in attributes)
        //                {
        //                    descriptionAttributes.Add(descriptionAttribute);
        //                }
        //                dic.Add(values[index], descriptionAttributes);
        //            }
        //        }
        //    }
        //    return dic;
        //} 
        #endregion

        /// <summary>
        /// 获得枚举值及其描述信息
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <returns>字典</returns>
        public static Dictionary<int, DescriptionAttribute> GetEnumValuesAndDescriptions(Type t)
        {
            var dics = new Dictionary<int,DescriptionAttribute>();
            if (t!=null&&t.IsEnum)
            {
                FieldInfo[] fieldInfos = t.GetFields(BindingFlags.Public | BindingFlags.Static);
                for (int i = 0; i < fieldInfos.Count(); i++)
                {
                    var value = fieldInfos[i].GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute;                    
                    var key = i;                    
                    dics.Add(key, value);
                }
            }
            return dics;
        }
    }
}
