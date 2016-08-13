using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ExceptionEx
{
    public class MsgException : Exception
    {
        public MsgException(string message)
            : base(message)
        { }
    }
}
