using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ModernUI.Common.Utility.ValidationBlock
{
    /// <summary>
    /// 方法参数验证器
    /// </summary>
    public class ValidationParameter : IDisposable
    {
        /// <summary>
        /// 方法参数验证缓存
        /// </summary>
        private Dictionary<MethodInfo, Dictionary<string, IEnumerable<Validator>>> methodValidatorDic
           = new Dictionary<MethodInfo, Dictionary<string, IEnumerable<Validator>>>();

        /// <summary>
        /// 方法参数验证
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="methodInfo">方法</param>
        /// <param name="parameterValue">参数值</param>
        /// <param name="parameterExpression">参数名称表达式</param>
        /// <returns>验证错误集合</returns>
        public List<ValidationError> Validate<T>(object parameterValue, Expression<Func<T>> parameterExpression, int frameIndex = 1)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            MethodInfo methodInfo = stackTrace.GetFrame(frameIndex).GetMethod() as MethodInfo;
            string parameterName = string.Empty;
            MemberExpression memberExpression = parameterExpression.Body as MemberExpression;
            if (memberExpression != null)
            {
                parameterName = memberExpression.Member.Name;
            }
            return this.Validate(parameterValue, parameterName, methodInfo);
        }

        public List<ValidationError> Validate(object parameterValue, string parameterName, int frameIndex = 1)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            MethodInfo methodInfo = stackTrace.GetFrame(frameIndex).GetMethod() as MethodInfo;
            return this.Validate(parameterValue, parameterName, methodInfo);
        }


        private List<ValidationError> Validate(object parameterValue, string parameterName, MethodInfo methodInfo)
        {
            List<ValidationError> errorList = new List<ValidationError>();
            IEnumerable<Validator> vs = null;
            lock (this)
            {
                if (this.methodValidatorDic.ContainsKey(methodInfo))
                {
                    Dictionary<string, IEnumerable<Validator>> parameterValidatorDic = this.methodValidatorDic[methodInfo];
                    if (parameterValidatorDic.ContainsKey(parameterName))
                    {
                        IEnumerable<Validator> vds = parameterValidatorDic[parameterName];
                        if (vds != null && vds.Count() > 0)
                        {
                            vs = vds;
                        }
                    }
                }
                else
                {
                    Dictionary<string, IEnumerable<Validator>> parameterValidatorDic = new Dictionary<string, IEnumerable<Validator>>();
                    foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
                    {
                        object[] attributes = parameterInfo.GetCustomAttributes(typeof(ValidatorAttribute), true);
                        if (attributes != null && attributes.Length > 0)
                        {
                            List<Validator> vds = new List<Validator>();
                            foreach (ValidatorAttribute validatorAttribute in attributes)
                            {
                                vds.Add(validatorAttribute.CreateValidator());
                            }
                            if (vds.Count > 0)
                            {
                                parameterValidatorDic.Add(parameterInfo.Name, vds);
                                vs = vds;
                            }
                        }
                    }
                    this.methodValidatorDic.Add(methodInfo, parameterValidatorDic);
                }
            }

            if (vs != null)
            {
                foreach (Validator validator in vs)
                {
                    ValidationError error = validator.Validate(parameterValue);
                    if (error != null)
                    {
                        errorList.Add(error);
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
                this.methodValidatorDic.Clear();
            }
        }
    }
}
