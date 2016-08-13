using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock.Implement
{
    public class RegexValidatorAttribute : ValidatorAttribute
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Rule { get; private set; }

        public RegexValidatorAttribute(string messageTemplate, string rule)
            : base(messageTemplate)
        {
            this.Rule = rule;
        }

        public override Validator CreateValidator()
        {
            return new RegexValidator(this.MessageTemplate, this.Rule);
        }
    }
}
