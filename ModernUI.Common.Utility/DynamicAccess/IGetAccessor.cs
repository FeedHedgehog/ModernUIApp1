using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.DynamicAccess
{
    public interface IGetAccessor
    {
        /// <summary>
        /// 获得属性的值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns>值</returns>
        object GetValue(string propertyName);
    }
}
