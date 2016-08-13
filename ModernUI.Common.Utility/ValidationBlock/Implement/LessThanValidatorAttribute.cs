using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    /// <summary>
    /// 小于验证属性
    /// </summary>
    public class LessThanValidatorAttribute : ValidatorAttribute
    {
        /// <summary>
        /// 上限
        /// </summary>
        public IComparable UpperBound { get; private set; }

        /// <summary>
        /// 允许相等
        /// </summary>
        public bool AllowEqual { get; private set; }

        public LessThanValidatorAttribute(string messageTemplate, int upperBound, bool allowEqual = false)
            : base(messageTemplate)
        {
            this.UpperBound = upperBound;
            this.AllowEqual = allowEqual;
        }

        public LessThanValidatorAttribute(string messageTemplate, double upperBound, bool allowEqual = false)
            : base(messageTemplate)
        {
            this.UpperBound = upperBound;
            this.AllowEqual = allowEqual;
        }

        public LessThanValidatorAttribute(string messageTmeplate, DateTime upperBound, bool allowEqual = false)
            : base(messageTmeplate)
        {
            this.UpperBound = upperBound;
            this.AllowEqual = allowEqual;
        }

        public override Validator CreateValidator()
        {
            return new LessThanValidator(this.MessageTemplate, this.UpperBound, this.AllowEqual);
        }
    }
}
