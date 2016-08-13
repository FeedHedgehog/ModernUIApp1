using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock
{
    public class ValidationError
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// 目标
        /// </summary>
        public object Target { get; internal set; }

        /// <summary>
        /// 验证器
        /// </summary>
        public Validator Validator { get; internal set; }

        public ValidationError(string message, object target, Validator validator)
        {
            Guard.ArgumentNotNull(message, "message");
            //Guard.ArgumentNotNull(target, "target");
            Guard.ArgumentNotNull(validator, "validator");
            this.Message = message;
            this.Target = target;
            this.Validator = validator;
        }
    }
}
