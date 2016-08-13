using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ModernUI.Common.Utility.DynamicAccess
{
    public static class PropertyHepler
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                return string.Empty;
            }
            string propertyName = memberExpression.Member.Name;
            return propertyName;
        }
    }
}
