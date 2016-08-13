using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    /// <summary>
    /// 字符串长度验证
    /// </summary>
    public class StringLengthValidator : Validator
    {
        /// <summary>
        /// 长度下限
        /// </summary>
        public int LowerBound { get; private set; }

        /// <summary>
        /// 长度上限
        /// </summary>
        public int UpperBound { get; private set; }

        /// <summary>
        /// 允许前后空白
        /// </summary>
        public bool AllowHeadAndEndSpace { get; private set; }

        public StringLengthValidator(string messageTemplate, int lowerBound, int upperBound, bool allowHeadAndEndSpace)
            : base(messageTemplate)
        {
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.AllowHeadAndEndSpace = allowHeadAndEndSpace;
        }

        public override ValidationError Validate(object objectToValidate, object context = null)
        {
            string strValue = objectToValidate as string;
            if (strValue == null)
            {
                strValue = string.Empty;
            }
            if (strValue.Length >= this.LowerBound && strValue.Length <= this.UpperBound)
            {
                return null;
            }
            return this.CreateValidationError(objectToValidate);
        }

        public override void FormatMessage(object objectToValidate)
        {
            base.FormatMessage(objectToValidate);
            this.MessageTemplate = this.MessageTemplate.Replace("{LowerBound}", this.LowerBound.ToString()).Replace("{UpperBound}", this.UpperBound.ToString());
        }
    }
}
