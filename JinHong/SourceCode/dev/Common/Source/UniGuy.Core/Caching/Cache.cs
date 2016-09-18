using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 这是一个简易的Cache,一般类中使用;和CacheManager不同的是本实现不是全局的和静态的;
    /// WeakCache与此类似.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <example>
    /// 获得缓存项的操作方法:
    /// object value = null;
    /// 
    /// if(cache.Contains(key))
    ///     value = cache.GetData(key);
    /// else 
    ///     ... 说明缓存项还没有缓存,执行从外部给value赋值,并且设置缓存项
    /// </example>
    public class Cache<T>
    {
        #region Fields
        /// <summary>
        /// 使用一个泛型Dictionary来作为内存中的对象容器
        /// </summary>
        private Dictionary<string, T> memoryCache;
        /// <summary>
        /// 线程安全使用
        /// </summary>
        private readonly object _syncRoot = new object();
        #endregion //   Fields

        #region Constructors
        public Cache()
        {
            //  构造其中初始化缓存容器
            memoryCache = new Dictionary<string, T>();
        }
        #endregion //   Constructors

        #region Properties
        /// <summary>
        /// 获得缓存项数目
        /// </summary>
        public int Count
        {
            get { return memoryCache.Count; }
        }
        #endregion //   Properties

        #region Methods
        /// <summary>
        /// 是否缓存了指定键的缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            ValidateKey(key);

            return memoryCache.ContainsKey(key);
        }
        /// <summary>
        /// 新增缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, T value)
        {
            ValidateKey(key);

            lock (_syncRoot)
            {
                memoryCache[key] = value;
            }
        }
        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            lock (_syncRoot)
            {
                memoryCache.Remove(key);
            }
        }
        /// <summary>
        /// 获得缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetData(string key)
        {
            lock (_syncRoot)
            {
                return memoryCache.ContainsKey(key) ? memoryCache[key] : default(T);
            }
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Flush()
        {
            lock (_syncRoot)
            {
                memoryCache.Clear();
            }
        }
        /// <summary>
        /// 验证缓存项键
        /// </summary>
        /// <param name="key"></param>
        private static void ValidateKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(string.Format("Parameter '{0}' can not be null.", "key"));
            }
        }
        #endregion //   Methods
    }
}
