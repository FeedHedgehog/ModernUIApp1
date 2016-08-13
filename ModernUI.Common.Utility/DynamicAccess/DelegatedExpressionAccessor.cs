using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ModernUI.Common.Utility.DynamicAccess
{
    /// <summary>
    /// 属性访问器,对访问器委托进行缓存
    /// </summary>  
    [Serializable]
    public class DelegatedExpressionAccessor : IDisposable
    {
        private static readonly Dictionary<string, DelegatedExpressionAccessor> AccessorCache = new Dictionary<string, DelegatedExpressionAccessor>();

        public static DelegatedExpressionAccessor GetInstance(Type type)
        {
            DelegatedExpressionAccessor delegatedExpressionAccessor = null;
            if (type != null && type.IsClass)
            {
                lock (AccessorCache)
                {
                    string className = type.FullName;
                    if (AccessorCache.ContainsKey(className))
                    {
                        delegatedExpressionAccessor = AccessorCache[className];
                    }
                    else
                    {
                        delegatedExpressionAccessor = new DelegatedExpressionAccessor(type);
                        AccessorCache.Add(type.FullName, delegatedExpressionAccessor);
                    }
                }
            }
            return delegatedExpressionAccessor;
        }

        private DelegatedExpressionAccessor(Type type)
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
        /// 属性get委托缓存
        /// </summary>
        private Dictionary<string, Func<object, object>> getValueDelegateDic = new Dictionary<string, Func<object, object>>();

        /// <summary>
        /// 属性set委托缓存
        /// </summary>
        private Dictionary<string, Action<object, object>> setValueDelegateDic = new Dictionary<string, Action<object, object>>();

        /// <summary>
        /// 属性缓存
        /// </summary>
        private Dictionary<string, PropertyInfo> propertyDic = null;

        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="obj">源对象</param>
        /// <returns>值</returns>
        public object GetValue(string propertyName, object obj)
        {
            this.InitializePropertyCache();
            PropertyInfo propertyInfo = null;
            lock (this.propertyDic)
            {
                if (this.propertyDic.ContainsKey(propertyName))
                {
                    propertyInfo = this.propertyDic[propertyName];
                }
            }
            if (propertyInfo != null)
            {
                Func<object, object> getValueDelegate = null;
                lock (this.getValueDelegateDic)
                {
                    this.getValueDelegateDic.TryGetValue(propertyName, out getValueDelegate);
                }
                if (getValueDelegate == null)
                {
                    //创建委托
                    var target = Expression.Parameter(typeof(object), "target");
                    LambdaExpression getter = Expression.Lambda(typeof(Func<object, object>), Expression.Convert(Expression.Property(Expression.Convert(target, this.t), propertyInfo), typeof(object)), target);

                    getValueDelegate = (Func<object, object>)getter.Compile();
                    lock (this.getValueDelegateDic)
                    {
                        if (!this.getValueDelegateDic.ContainsKey(propertyName))
                        {
                            this.getValueDelegateDic.Add(propertyName, getValueDelegate);
                        }
                    }
                }
                return getValueDelegate(obj);
            }
            return null;
        }

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="obj">目标对象</param>
        /// <param name="value">值</param>
        public void SetValue(string propertyName, object obj, object value)
        {
            this.InitializePropertyCache();
            PropertyInfo propertyInfo = null;
            lock (this.propertyDic)
            {
                if (this.propertyDic.ContainsKey(propertyName))
                {
                    propertyInfo = this.propertyDic[propertyName];
                }
            }
            if (propertyInfo != null)
            {
                Action<object, object> setValueDelegate = null;
                lock (this.setValueDelegateDic)
                {
                    this.setValueDelegateDic.TryGetValue(propertyName, out setValueDelegate);
                }
                if (setValueDelegate == null)
                {
                    //创建委托
                    ParameterExpression target = Expression.Parameter(typeof(object), "target");
                    ParameterExpression valueParameterExpression = Expression.Parameter(typeof(object), "value");
                    LambdaExpression setter = Expression.Lambda(typeof(Action<object, object>), Expression.Assign(Expression.Property(Expression.Convert(target, this.t), propertyInfo), Expression.Convert(valueParameterExpression, propertyInfo.PropertyType)), target, valueParameterExpression);
                    setValueDelegate = (Action<object, object>)setter.Compile();
                    lock (this.setValueDelegateDic)
                    {
                        if (!this.setValueDelegateDic.ContainsKey(propertyName))
                        {
                            this.setValueDelegateDic.Add(propertyName, setValueDelegate);
                        }
                    }
                }
                setValueDelegate(obj, value);
            }
        }

        /// <summary>
        /// 释放缓存
        /// </summary>
        public void Dispose()
        {
            lock (this.getValueDelegateDic)
            {
                this.getValueDelegateDic.Clear();
            }

            lock (this.setValueDelegateDic)
            {
                this.setValueDelegateDic.Clear();
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
                    foreach (PropertyInfo propertyInfo in this.t.GetProperties())
                    {
                        this.propertyDic.Add(propertyInfo.Name, propertyInfo);
                    }
                }
            }
        }
    }
}
