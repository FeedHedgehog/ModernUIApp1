using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility.ValidationBlock
{
    /// <summary>
    /// 验证器基类
    /// </summary>
    public abstract class Validator
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string MessageTemplate { get; protected set; }

        /// <summary>
        /// 验证错误信息
        /// </summary>
        /// <param name="objectToValidate">目标值</param>
        /// <param name="context">上下文对象</param>
        /// <returns>验证错误</returns>
        public abstract ValidationError Validate(object objectToValidate, object context = null);


        public Validator(string messageTemplate)
        {
            this.MessageTemplate = messageTemplate ?? string.Empty;
        }

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="objectToValidate">目标值</param>
        public virtual void FormatMessage(object objectToValidate)
        {
            this.MessageTemplate = this.MessageTemplate.Replace("{PropertyValue}", objectToValidate.ToString());
        }

        /// <summary>
        /// 创建错误信息
        /// </summary>
        /// <param name="objectToValidate">目标值</param>
        /// <returns>验证错误</returns>
        protected ValidationError CreateValidationError(object objectToValidate)
        {
            //Guard.ArgumentNotNull(objectToValidate, "objectToValidate");
            return new ValidationError(this.MessageTemplate, objectToValidate, this);
        }
    }
}
