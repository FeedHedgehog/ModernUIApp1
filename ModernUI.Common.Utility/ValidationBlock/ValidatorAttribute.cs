using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock
{
    /// <summary>
    /// 验证属性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class ValidatorAttribute : Attribute
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string MessageTemplate { get; private set; }

        /// <summary>
        /// 创建验证器
        /// </summary>
        /// <returns>验证器</returns>
        public abstract Validator CreateValidator();

        public ValidatorAttribute(string messageTmeplate)
        {
            Guard.ArgumentNotNullOrEmpty(messageTmeplate, "messageTmeplate");
            this.MessageTemplate = messageTmeplate;
            this.RuleName = string.Empty;
        }

        /// <summary>
        /// 通过属性格式化错误信息
        /// </summary>
        /// <param name="property">属性</param>
        public virtual void FormatMessage(System.Reflection.PropertyInfo property)
        {
            this.MessageTemplate = this.MessageTemplate.Replace("{[PropertyName}", property.Name).Replace("{Tag}", this.Tag);
        }
    }
}
