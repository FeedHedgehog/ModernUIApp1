using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    /// <summary>
    /// 大于验证属性
    /// </summary>
    public class GreaterThanValidatorAttribute : ValidatorAttribute
    {
        /// <summary>
        /// 下限
        /// </summary>
        public IComparable LowerBound { get; private set; }

        /// <summary>
        /// 允许相等
        /// </summary>
        public bool AllowEqual { get; private set; }

        public GreaterThanValidatorAttribute(string messageTemplate, int lowerBound, bool allowEqual = false)
            : base(messageTemplate)
        {
            this.LowerBound = lowerBound;
            this.AllowEqual = allowEqual;
        }

        public GreaterThanValidatorAttribute(string messageTemplate, double lowerBound, bool allowEqual = false)
            : base(messageTemplate)
        {
            this.LowerBound = lowerBound;
            this.AllowEqual = allowEqual;
        }

        public GreaterThanValidatorAttribute(string messageTmeplate, DateTime lowerBound, bool allowEqual = false)
            : base(messageTmeplate)
        {
            this.LowerBound = lowerBound;
            this.AllowEqual = allowEqual;
        }

        public override Validator CreateValidator()
        {
            return new GreaterThanValidator(this.MessageTemplate, this.LowerBound, this.AllowEqual);
        }
    }
}
