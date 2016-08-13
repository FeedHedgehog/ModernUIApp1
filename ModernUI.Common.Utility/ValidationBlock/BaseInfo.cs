using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using ModernUI.Common.Utility.DynamicAccess;
using ModernUI.Common.Utility.ValidationBlock;


namespace ModernUI.Common.Utility.ValidationBlock
{
    using System.Linq;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public abstract class BaseInfo : NotificationObject, IGetAccessor, ISetAccessor, IDataErrorInfo, ICloneable
    {
        #region Clone

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns>复制结果对象</returns>
        public virtual object Clone()
        {
            //this.MemberwiseClone();
            Type type = this.GetType();
            BaseInfo result = Activator.CreateInstance(type) as BaseInfo;
            var tplist = type.GetProperties();
            foreach (var tp in tplist)
            {
                if ((tp != null) && tp.CanRead && tp.CanWrite)
                {
                    result.SetValue(tp.Name, this.GetValue(tp.Name));
                }
            }
            return result;
        }

        #endregion

        #region Accessor

        private DelegatedExpressionAccessor accessor;

        public virtual DelegatedExpressionAccessor Accessor
        {
            get
            {
                if (accessor == null)
                {
                    accessor = DelegatedExpressionAccessor.GetInstance(this.GetType());
                }
                return accessor;
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns>值</returns>
        public object GetValue(string propertyName)
        {
            return this.Accessor.GetValue(propertyName, this);
        }

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">属性值</param>
        public void SetValue(string propertyName, object value)
        {
            this.Accessor.SetValue(propertyName, this, value);
        }

        #endregion

        #region Validation

        private ValidationProperty validation;

        /// <summary>
        /// 属性验证缓存
        /// </summary>
        public virtual ValidationProperty Validation
        {
            get
            {
                if (validation == null)
                {
                    validation = ValidationProperty.GetInstance(this.GetType());
                }
                return validation;
            }
        }

        /// <summary>
        /// 获取指示对象何处出错的错误信息,默认值为空字符串
        /// </summary>
        public string Error
        {
            get
            {
                List<ValidationError> errors = this.Validation.ValidatePropertys(this);
                StringBuilder sb = new StringBuilder();
                foreach (ValidationError validationError in errors)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(validationError.Message);
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 获取具有给定名称的属性的错误信息
        /// </summary>
        /// <param name="columnName">要获取其错误信息的属性的名称</param>
        /// <returns>该属性的错误信息。默认值为空字符串</returns>
        public string this[string columnName]
        {
            get
            {
                List<ValidationError> errors = this.Validation.ValidateProperty(columnName, this);
                StringBuilder sb = new StringBuilder();
                foreach (ValidationError validationError in errors)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(validationError.Message);
                }
                return sb.ToString();
            }
        }

        #endregion
    }
}