using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.DynamicAccess
{
    public interface ISetAccessor
    {
        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">属性值</param>
        void SetValue(string propertyName, object value);
    }
}
