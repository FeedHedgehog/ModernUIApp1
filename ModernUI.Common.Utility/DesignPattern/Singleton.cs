namespace ModernUI.Common.Utility
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// 单例泛型类
    /// generic for singletons
    /// </summary>
    /// <typeparam name="T">要单例的类</typeparam>
    public class Singleton<T> where T : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton{T}"/> class.
        /// </summary>
        /// <exception cref="Exception">
        ///  You have tried to create a new singleton class where you should have instanced it. Replace your \"new class()\" with \"class.Instance\""
        /// </exception>
        protected Singleton()
        {
            if (Instance != null)
            {
                throw new Exception("You have tried to create a new singleton class where you should have instanced it. Replace your \"new class()\" with \"class.Instance\"");
            }
        }

        public static T Instance
        {
            get
            {
                if (SingletonCreator.CreatingException != null)
                {
                    throw SingletonCreator.CreatingException;
                }
                return SingletonCreator.Instance;
            }
        }

        /// <summary>
        /// 单例实例化器
        /// </summary>
        public class SingletonCreator
        {
            internal static readonly T Instance;
            internal static readonly Exception CreatingException;

            static SingletonCreator()
            {
                try
                {
                    Instance = new T();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        CreatingException = ex.InnerException;
                    }
                    else
                    {
                        CreatingException = ex;
                    }
                    Trace.WriteLine("Singleton: " + CreatingException);
                }
            }
        }
    }
}
