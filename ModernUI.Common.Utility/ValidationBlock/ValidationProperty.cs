using System;
using System.Collections.Generic;
using System.Reflection;
using ModernUI.Common.Utility.DynamicAccess;

namespace ModernUI.Common.Utility.ValidationBlock
{
    /// <summary>
    /// 属性验证
    /// </summary>  
    [Serializable]
    public class ValidationProperty : IDisposable
    {
        private readonly static Dictionary<string, ValidationProperty> ValidationCache = new Dictionary<string, ValidationProperty>();

        public static ValidationProperty GetInstance(Type type)
        {
            ValidationProperty validationProperty = null;
            if (type != null && type.IsClass)
            {
                lock (ValidationCache)
                {
                    string className = type.FullName;
                    if (ValidationCache.ContainsKey(className))
                    {
                        validationProperty = ValidationCache[className];
                    }
                    else
                    {
                        validationProperty = new ValidationProperty(type);
                        ValidationCache.Add(type.FullName, validationProperty);
                    }
                }
            }
            return validationProperty;
        }

        private ValidationProperty(Type type)
        {
            if (type != null && type.IsClass)
            {
                this.t = type;
            }
            else
            {
                throw new ArgumentException("Type must be Class");
            }
        }

        private Type t = null;

        /// <summary>
        /// 验证器缓存
        /// </summary>
        private Dictionary<string, IEnumerable<Validator>> validatorDic = new Dictionary<string, IEnumerable<Validator>>();

        /// <summary>
        /// 描述类属性缓存
        /// </summary>
        private Dictionary<string, PropertyInfo> propertyDic = null;

        /// <summary>
        /// 根据属性名验证
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="validateObj">待验证对象</param>
        /// <returns>验证错误集合</returns>
        public List<ValidationError> ValidateProperty(string propertyName, object validateObj)
        {
            this.InitializePropertyCache();
            List<ValidationError> errorList = new List<ValidationError>();
            if (validateObj != null)
            {
                IEnumerable<Validator> vs = null;

                lock (this)
                {
                    if (this.propertyDic.ContainsKey(propertyName))
                    {
                        PropertyInfo propertyInfo = this.propertyDic[propertyName];

                        if (this.validatorDic.ContainsKey(propertyName))
                        {
                            vs = this.validatorDic[propertyName];
                        }
                        else
                        {
                            object[] attributes = propertyInfo.GetCustomAttributes(typeof(ValidatorAttribute), true);
                            if (attributes != null && attributes.Length > 0)
                            {
                                List<Validator> vds = new List<Validator>();
                                foreach (ValidatorAttribute validatorAttribute in attributes)
                                {
                                    vds.Add(validatorAttribute.CreateValidator());
                                }
                                this.validatorDic.Add(propertyName, vds);
                                vs = vds;
                            }
                        }
                    }
                }

                if (vs != null)
                {
                    IGetAccessor getAccessor = null;
                    if (validateObj is IGetAccessor)
                    {
                        getAccessor = validateObj as IGetAccessor;
                    }
                    foreach (Validator validator in vs)
                    {
                        ValidationError error = null;
                        if (getAccessor == null)
                        {
                            error = validator.Validate(validateObj.GetType().GetProperty(propertyName).GetValue(validateObj, null), validateObj);
                        }
                        else
                        {
                            error = validator.Validate(getAccessor.GetValue(propertyName), validateObj);
                        }
                        if (error != null)
                        {
                            errorList.Add(error);
                        }
                    }
                }
            }
            return errorList;
        }

        /// <summary>
        /// 验证类中所有需要验证的属性
        /// </summary>
        /// <param name="validateObj">待验证对象</param>
        /// <returns>验证错误集合</returns>
        public List<ValidationError> ValidatePropertys(object validateObj)
        {
            this.InitializePropertyCache();
            List<ValidationError> errorList = new List<ValidationError>();
            if (validateObj != null)
            {
                foreach (PropertyInfo propertyInfo in this.propertyDic.Values)
                {
                    IEnumerable<Validator> vs = null;

                    lock (this)
                    {
                        if (this.validatorDic.ContainsKey(propertyInfo.Name))
                        {
                            vs = this.validatorDic[propertyInfo.Name];
                        }
                        else
                        {
                            object[] attributes = propertyInfo.GetCustomAttributes(typeof(ValidatorAttribute), true);
                            if (attributes != null && attributes.Length > 0)
                            {
                                List<Validator> vds = new List<Validator>();
                                foreach (ValidatorAttribute validatorAttribute in attributes)
                                {
                                    vds.Add(validatorAttribute.CreateValidator());
                                }
                                this.validatorDic.Add(propertyInfo.Name, vds);
                                vs = vds;
                            }
                        }
                    }

                    if (vs != null)
                    {
                        IGetAccessor getAccessor = null;
                        if (validateObj is IGetAccessor)
                        {
                            getAccessor = validateObj as IGetAccessor;
                        }
                        foreach (Validator validator in vs)
                        {
                            ValidationError error = null;
                            if (getAccessor == null)
                            {
                                error = validator.Validate(validateObj.GetType().GetProperty(propertyInfo.Name).GetValue(validateObj, null), validateObj);
                            }
                            else
                            {
                                error = validator.Validate(getAccessor.GetValue(propertyInfo.Name), validateObj);
                            }
                            if (error != null)
                            {
                                errorList.Add(error);
                            }
                        }
                    }
                }
            }
            return errorList;
        }

        /// <summary>
        /// 释放缓存
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                this.validatorDic.Clear();
            }
        }

        /// <summary>
        /// 初始化属性缓存
        /// </summary>
        private void InitializePropertyCache()
        {
            lock (this)
            {
                if (this.propertyDic == null)
                {
                    this.propertyDic = new Dictionary<string, PropertyInfo>();
                    PropertyInfo[] propertyInfos = this.t.GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        this.propertyDic.Add(propertyInfo.Name, propertyInfo);
                    }
                }
            }
        }
    }
}
