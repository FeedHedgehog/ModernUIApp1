using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 复制实例指定类型的属性值，并返回指定类型的新实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyProperties<T>(this T obj, bool copyGenericType = false)
        {
            //Type type = obj.GetType();
            //T result = (T)Activator.CreateInstance(type);
            //var plist = type.GetProperties();
            //foreach (var item in plist)
            //{
            //    var value = item.GetValue(obj, null);
            //    if (item.CanWrite)
            //        item.SetValue(result, value, null);
            //}
            //return result;

            Type targetType = typeof(T);
            Type sourceType = obj.GetType();
            T result = (T)Activator.CreateInstance(targetType);
            var tplist = targetType.GetProperties();
            var splist = sourceType.GetProperties();
            foreach (var tp in tplist)
            {
                var sp = splist.SingleOrDefault(r => r.Name.Equals(tp.Name));
                if (sp != null)
                {
                    var value = sp.GetValue(obj, null);
                    if (tp.CanWrite)
                        tp.SetValue(result, value, null);
                    else
                    {
                        if (copyGenericType)
                            CheckAndCopyGenericType<T>(result, tp, value);
                    }
                }
            }
            return result;
        }

        public static object CopyProperties(this object obj, bool copyGenericType = false)
        {
            Type objType = obj.GetType();
            var result = Activator.CreateInstance(objType);
            var tplist = objType.GetProperties();
            foreach (var tp in tplist)
            {
                var value = tp.GetValue(obj, null);
                if (tp.CanWrite)
                    tp.SetValue(result, value, null);
                else
                {
                    if (copyGenericType)
                        CheckAndCopyGenericType(result, tp, value);
                }
            }
            return result;
        }

        private static void CheckAndCopyGenericType<T>(T result, Reflection.PropertyInfo tp, object value)
        {
            if (tp.PropertyType.IsGenericType)
            {
                var lst = tp.GetValue(result, null);
                if (lst != null)
                {
                    if (lst is IList)
                    {
                        foreach (var item in value as IList)
                        {
                            (lst as IList).Add(item.CopyProperties(true));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将source中Property赋值到对应的target Property中
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        public static void CopyProperties(this object source, object target, bool copyGenericType = false)
        {
            Type targetType = target.GetType();
            Type sourceType = source.GetType();
            var tplist = targetType.GetProperties();
            var splist = sourceType.GetProperties();
            foreach (var tp in tplist)
            {
                //必须只读只写才复制
                if (!tp.CanRead || !tp.CanWrite)
                {
                    continue;
                }
                var sp = splist.SingleOrDefault(r => r.Name.Equals(tp.Name));
                if (sp != null)
                {
                    var value = sp.GetValue(source, null);
                    if (tp.CanWrite)
                        tp.SetValue(target, value, null);
                    else
                    {
                        if (copyGenericType)
                            CheckAndCopyGenericType(target, tp, value);
                    }
                }
            }
        }

        /// <summary>
        /// 将source中Property赋值到对应的target Property中
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        /// <param name="except">排除的属性</param>
        public static void CopyProperties(this object source, object target, string[] except, bool copyGenericType = false)
        {
            Type targetType = target.GetType();
            Type sourceType = source.GetType();
            var tplist = targetType.GetProperties();
            var splist = sourceType.GetProperties();
            foreach (var tp in tplist)
            {
                if (except.Contains(tp.Name))
                    continue;
                var sp = splist.SingleOrDefault(r => r.Name.Equals(tp.Name));
                if (sp != null)
                {
                    var value = sp.GetValue(source, null);
                    if (tp.CanWrite)
                        tp.SetValue(target, value, null);
                    else
                    {
                        if (copyGenericType)
                            CheckAndCopyGenericType(target, tp, value);
                    }
                }
            }
        }


        /// <summary>
        /// 将source中Property赋值到对应的target Property中
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        /// <param name="matchType">匹配属性的对象类型</param>
        public static void CopyProperties(this object source, object target, Type matchType, bool copyGenericType = false)
        {
            Type targetType = target.GetType();
            Type sourceType = source.GetType();
            var tplist = targetType.GetProperties();
            var splist = sourceType.GetProperties();
            var mplist = matchType.GetProperties();

            foreach (var tp in tplist)
            {
                if (mplist.Any(r => r.Name.Equals(tp.Name)))
                {
                    var sp = splist.SingleOrDefault(r => r.Name.Equals(tp.Name));
                    if (sp != null)
                    {
                        var value = sp.GetValue(source, null);
                        if (tp.CanWrite)
                            tp.SetValue(target, value, null);
                        else
                        {
                            if (copyGenericType)
                                CheckAndCopyGenericType(target, tp, value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将source中Property赋值到对应的target Property中
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="target">目标对象</param>
        /// <param name="matchType">匹配属性的对象类型</param>
        /// <param name="except">排除的属性</param>
        public static void CopyProperties(this object source, object target, Type matchType, string[] except, bool copyGenericType = false)
        {
            Type targetType = target.GetType();
            Type sourceType = source.GetType();
            var tplist = targetType.GetProperties();
            var splist = sourceType.GetProperties();
            var mplist = matchType.GetProperties();

            foreach (var tp in tplist)
            {
                if (except.Contains(tp.Name))
                    continue;
                if (mplist.Any(r => r.Name.Equals(tp.Name)))
                {
                    var sp = splist.SingleOrDefault(r => r.Name.Equals(tp.Name));
                    if (sp != null)
                    {
                        var value = sp.GetValue(source, null);
                        if (tp.CanWrite)
                            tp.SetValue(target, value, null);
                        else
                        {
                            if (copyGenericType)
                                CheckAndCopyGenericType(target, tp, value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 深拷贝当前实例并返回一个新实例。
        ///     注：不能拷贝只读属性
        /// </summary>
        /// <param name="source">当前实例</param>
        /// <returns></returns>
        public static object CloneProperties(this object source)
        {
            if (source == null)
                return null;
            if (source.GetType().GetConstructor(new Type[0]) == null || source == null)
                return source;
            var result = Activator.CreateInstance(source.GetType());
            Type sourceType = source.GetType();
            if (result.GetType().IsGenericType)
            {
                foreach (var item in source as IList)
                    (result as IList).Add(item.CloneProperties());
            }
            else
            {
                System.Reflection.PropertyInfo[] tplist = result.GetType().GetProperties();
                if (tplist.Count() == 0)
                    result = source;
                foreach (var tp in tplist)
                {
                    System.Reflection.PropertyInfo sp = sourceType.GetProperty(tp.Name);
                    //必须只读只写才复制
                    if (!tp.CanRead || !tp.CanWrite)
                    {
                        continue;
                    }
                    if (tp.PropertyType.IsGenericType)
                    {
                        var lst = tp.GetValue(result, null);
                        if (lst != null)
                        {
                            if (lst is IList)
                            {
                                var value = sp.GetValue(source, null);
                                foreach (var item in value as IList)
                                    (lst as IList).Add(item.CloneProperties());
                            }
                        }
                    }
                    else if (sp != null)
                    {
                        var value = sp.GetValue(source, null);
                        if (tp.CanWrite)
                            tp.SetValue(result, value.CloneProperties(), null);
                    }
                }
            }
            return result;
        }
    }
}
