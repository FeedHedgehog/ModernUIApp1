using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock
{
    public static class Guard
    {
        /// <summary>
        /// 返回参数为空的异常
        /// </summary>
        /// <param name="argumentValue">值</param>
        /// <param name="argumentName">名称</param>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// 返回参数为空或者长度为零的异常
        /// </summary>
        /// <param name="argumentValue">值</param>
        /// <param name="argumentName">名称</param>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException("Argument must not be empty", argumentName);
            }
        }
    }
}
