using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModernUI.Common.Utility.EnumBlock
{
    public class EnumHelper
    {
        /// <summary>
        /// 空枚举值:0
        /// </summary>
        public const int NullEnumZeroValue = 0;

        ///<summary>
        /// 获得枚举值及其描述信息
        ///</summary>
        ///<param name="t">枚举类型</param>
        /// <returns>字典</returns>
        public static Dictionary<int, DescriptionAttribute> GetEnumValuesAndDescriptions(Type t)
        {
            Dictionary<int, DescriptionAttribute> dic = new Dictionary<int, DescriptionAttribute>();
            if (t != null && t.IsEnum)
            {
                FieldInfo[] fieldInfos = t.GetFields(BindingFlags.Public | BindingFlags.Static);
                List<string> names = new List<string>();
                foreach (string name in t.GetEnumNames())
                {
                    names.Add(name);
                }
                List<int> values = new List<int>();
                foreach (int value in t.GetEnumValues())
                {
                    values.Add(value);
                }

                foreach (FieldInfo fidldInfo in fieldInfos)
                {
                    int index = names.IndexOf(fidldInfo.Name);
                    if (index > -1)
                    {
                        object[] attributes = fidldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attributes != null && attributes.Length > 0)
                        {
                            dic.Add(values[index], attributes[0] as DescriptionAttribute);
                        }
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 获得枚举其信息
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <returns>数据列表</returns>
        public static List<EnumDescriptionData> GetEnumData(Type t)
        {
            Dictionary<int, DescriptionAttribute> dic = GetEnumValuesAndDescriptions(t);
            List<EnumDescriptionData> list = new List<EnumDescriptionData>();
            foreach (KeyValuePair<int, DescriptionAttribute> kp in dic)
            {
                EnumDescriptionData enumDescriptionData = new EnumDescriptionData(kp.Key, kp.Value.Description);
                list.Add(enumDescriptionData);
            }
            return list;
        }
    }
}
