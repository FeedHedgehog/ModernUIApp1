namespace ModernUI.Common.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 可枚举类型的扩展
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// 当查找结果不为空时进行相关操作
        /// </summary>
        /// <typeparam name="T">枚举器元素具体类型</typeparam>
        /// <param name="enumerable">枚举器</param>
        /// <param name="condtion">查找条件</param>
        /// <param name="operation">对查找到的元素进行操作</param>
        /// <remarks>对查找到的元素进行的操作不能改变枚举器集合,否则会发生异常</remarks>
        public static void DoWhenFindIsNotNull<T>(this IEnumerable<T> enumerable, Predicate<T> condtion, Action<T> operation) where T : class
        {
            if (enumerable == null)
            {
                return;
            }

            var first = enumerable.FirstOrDefault(t => condtion(t));
            if (first != null)
            {
                operation(first);
            }
        }

        /// <summary>
        /// 当查找结果为空时进行相关操作
        /// </summary>
        /// <typeparam name="T">枚举器元素具体类型</typeparam>
        /// <param name="enumerable">枚举器</param>
        /// <param name="condtion">查找条件</param>
        /// <param name="operation">进行的操作</param>
        public static void DoWhenFindIsNull<T>(this IEnumerable<T> enumerable, Predicate<T> condtion, Action operation) where T : class
        {
            if (enumerable == null)
            {
                return;
            }

            var first = enumerable.FirstOrDefault(t => condtion(t));
            if (first != null)
            {
                operation();
            }
        }

        /// <summary>
        /// 对枚举器中的每个元素进行操作
        /// </summary>
        /// <typeparam name="T">枚举器元素具体类型</typeparam>
        /// <param name="enumerable">枚举器</param>
        /// <param name="operation">对元素进行的操作</param>
        /// <remarks>对元素进行的操作不能改变枚举器集合,否则会发生异常</remarks>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> operation)
        {
            if (enumerable == null)
            {
                return;
            }

            foreach (var item in enumerable)
            {
                operation(item);
            }
        }

        /// <summary>
        /// 删除第一个符合条件的元素
        /// </summary>
        /// <typeparam name="T">集合具体类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="condtion">条件</param>
        public static void RemoveFirst<T>(this ICollection<T> collection, Predicate<T> condtion) where T : class
        {
            DoWhenFindIsNotNull(collection, condtion, c => collection.Remove(c));
        }

        /// <summary>
        /// 判断集合是否存在有效数据
        /// </summary>
        /// <typeparam name="T">集合具体类型</typeparam>
        /// <param name="collection">集合</param>
        public static bool HaveData<T>(this ICollection<T> collection)
        {
            return (collection != null) && (collection.Count > 0);
        }

        /// <summary>
        /// 在列表不存在元素时进行添加
        /// </summary>
        /// <typeparam name="T">列表具体类型</typeparam>
        /// <param name="lst">列表</param>
        /// <param name="condition">存在判断条件</param>
        /// <param name="data">要添加的数据</param>
        public static void AddWhenNonExist<T>(this List<T> lst, Predicate<T> condition, T data)
        {
            if (!lst.Exists(condition))
            {
                lst.Add(data);
            }
        }

        /// <summary>
        /// 在列表不包含元素时进行添加
        /// </summary>
        /// <typeparam name="T">列表具体类型</typeparam>
        /// <param name="lst">列表</param>
        /// <param name="data">要添加的数据</param>
        public static void AddWhenNonContain<T>(this List<T> lst, T data)
        {
            if (!lst.Contains(data))
            {
                lst.Add(data);
            }
        }
    }
}