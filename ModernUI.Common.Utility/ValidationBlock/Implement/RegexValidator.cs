using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    /// <summary>
    /// 正则验证器
    /// </summary>
    public class RegexValidator : Validator
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Rule { get; private set; }

        public RegexValidator(string messageTemplate, string rule)
            : base(messageTemplate)
        {
            Guard.ArgumentNotNull(rule, "rule");
            this.Rule = rule;
        }

        public override ValidationError Validate(object objectToValidate, object context = null)
        {
            string input = null;
            if (objectToValidate==null)
            {
                input = string.Empty;
            }
            if (objectToValidate is string)
            {
                input = objectToValidate.ToString();
            }
            if (input != null)
            {                
                if (Regex.IsMatch(input, this.Rule))
                {
                    return null;
                }
                else
                {
                    return this.CreateValidationError(objectToValidate);
                }
            }
            else
            {
                return this.CreateValidationError(objectToValidate);
            }
        }

        public override void FormatMessage(object objectToValidate)
        {
            base.FormatMessage(objectToValidate);
            this.MessageTemplate = this.MessageTemplate.Replace("{Rule}", this.Rule.ToString());
        }
    }
}
