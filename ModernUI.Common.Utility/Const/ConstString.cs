using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.Const
{
    public static class ConstString
    {
        /// <summary>
        /// 验证是否是非法的字符串的正则表达式
        /// </summary>
        public const string InvaidStringRegex = @"^[^~`!@#$%\^&*\(\)\-=+\]\[\{\}\\|'"";:/\?\.>,<，《。》、？‘“；：】｝【｛\、|·~！@#￥%……&*（）]*$";
    }
}
