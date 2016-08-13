using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    /// <summary>
    /// 小于验证器
    /// </summary>
    public class LessThanValidator : Validator
    {
        /// <summary>
        /// 上限
        /// </summary>
        public IComparable UpperBound { get; private set; }

        /// <summary>
        /// 允许相等
        /// </summary>
        public bool AllowEqual { get; private set; }

        public LessThanValidator(string messageTemplate, IComparable upperBound, bool allowEqual)
            : base(messageTemplate)
        {
            Guard.ArgumentNotNull(upperBound, "upperBound");
            this.UpperBound = upperBound;
            this.AllowEqual = allowEqual;
        }

        public override ValidationError Validate(object objectToValidate, object context = null)
        {
            if (this.UpperBound is System.Int32 && objectToValidate is System.Double)
            {
                this.UpperBound = Convert.ToDouble(this.UpperBound);
            }
            if (!this.AllowEqual && this.UpperBound.CompareTo(objectToValidate) > 0)
            {
                return null;
            }
            else if (this.AllowEqual && this.UpperBound.CompareTo(objectToValidate) >= 0)
            {
                return null;
            }
            else
            {
                return this.CreateValidationError(objectToValidate);
            }
        }

        public override void FormatMessage(object objectToValidate)
        {
            base.FormatMessage(objectToValidate);
            this.MessageTemplate = this.MessageTemplate.Replace("{UpperBound}", this.UpperBound.ToString());
        }
    }
}
