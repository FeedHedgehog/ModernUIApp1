using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    /// <summary>
    /// 大于验证器
    /// </summary>
    public class GreaterThanValidator : Validator
    {
        /// <summary>
        /// 下限
        /// </summary>
        public IComparable LowerBound { get; private set; }

        /// <summary>
        /// 允许相等
        /// </summary>
        public bool AllowEqual { get; private set; }

        public GreaterThanValidator(string messageTemplate, IComparable lowerBound, bool allowEqual)
            : base(messageTemplate)
        {
            Guard.ArgumentNotNull(lowerBound, "lowerBound");
            this.LowerBound = lowerBound;
            this.AllowEqual = allowEqual;
        }

        public override ValidationError Validate(object objectToValidate, object context = null)
        {
            if (this.LowerBound is System.Int32 && objectToValidate is System.Double)
            {
                this.LowerBound = Convert.ToDouble(this.LowerBound);
            }
            if (!this.AllowEqual && this.LowerBound.CompareTo(objectToValidate) < 0)
            {
                return null;
            }
            else if (this.AllowEqual && this.LowerBound.CompareTo(objectToValidate) <= 0)
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
            this.MessageTemplate = this.MessageTemplate.Replace("{LowerBound}", this.LowerBound.ToString());
        }
    }
}
