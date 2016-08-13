using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    /// <summary>
    /// 字符串长度验证属性
    /// </summary>
    public class StringLengthValidatorAttribute : ValidatorAttribute
    {
        /// <summary>
        /// 长度下限
        /// </summary>
        public int LowerBound { get; set; }

        /// <summary>
        /// 长度上限
        /// </summary>
        public int UpperBound { get; set; }


        /// <summary>
        /// 允许前后空白
        /// </summary>
        public bool AllowHeadAndEndSpace { get; set; }

        public StringLengthValidatorAttribute(string messageTemplate)
            : base(messageTemplate)
        {
            this.LowerBound = int.MinValue;
            this.UpperBound = int.MaxValue;
        }

        public override Validator CreateValidator()
        {
            return new StringLengthValidator(this.MessageTemplate, this.LowerBound, this.UpperBound, this.AllowHeadAndEndSpace);
        }
    }
}
