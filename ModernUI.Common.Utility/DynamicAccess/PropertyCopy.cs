using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModernUI.Common.Utility.DynamicAccess
{
    /// <summary>
    /// 复制器
    /// </summary>
    public class PropertyCopy
    {
        /// <summary>
        /// 可读写属性缓存
        /// </summary>
        private Dictionary<string, PropertyInfo> propertyDic = null;

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="obj">源对象</param>
        /// <param name="value">目的对象</param>
        public void Copy(object obj, object value)
        {
            lock (this)
            {
                if (this.propertyDic == null)
                {
                    this.propertyDic = new Dictionary<string, PropertyInfo>();
                    if (obj != null)
                    {
                        Type t = obj.GetType();
                        foreach (PropertyInfo propertyInfo in t.GetProperties())
                        {
                            if (propertyInfo.CanWrite && propertyInfo.CanRead)
                            {
                                this.propertyDic.Add(propertyInfo.Name, propertyInfo);
                            }
                        }
                    }
                }
            }
            IGetAccessor valueGetAccessor = null;
            if (value is IGetAccessor)
            {
                valueGetAccessor = value as IGetAccessor;
            }
            ISetAccessor objSetAccessor = null;
            if (obj is ISetAccessor)
            {
                objSetAccessor = obj as ISetAccessor;
            }

            foreach (KeyValuePair<string, PropertyInfo> kp in this.propertyDic)
            {
                if (objSetAccessor != null && valueGetAccessor != null)
                {
                    objSetAccessor.SetValue(kp.Key, valueGetAccessor.GetValue(kp.Key));
                }
                else if (objSetAccessor != null && valueGetAccessor == null)
                {
                    objSetAccessor.SetValue(kp.Key, kp.Value.GetValue(value, null));
                }
                else if (objSetAccessor == null && valueGetAccessor == null)
                {
                    kp.Value.SetValue(obj, kp.Value.GetValue(value, null), null);
                }
            }
        }
    }
}
