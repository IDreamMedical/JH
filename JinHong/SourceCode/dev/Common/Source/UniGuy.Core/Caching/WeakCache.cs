using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 一个简单有用的泛型WeakCache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <example>
    /// 获取缓存项的操作方法:
    /// object value = null;
    /// 
    /// if(!TryGetData(key, out value))
    ///     ...说明没有缓存或者缓存项已被GC回收,从外部重新获得缓存项并新增的缓存
    /// </example>
    public class WeakCache<T>
    {
        #region Fields
        /// <summary>
        /// 使用Value为弱引用的字典作为内存中的缓存项容器
        /// </summary>
        private Dictionary<object, WeakReference> memoryCache;
        /// <summary>
        /// 验证安全使用
        /// </summary>
        private readonly object _syncRoot = new object();
        #endregion //   Fields

        #region Constructors
        public WeakCache()
        {
            // 初始化内存中缓存项容器
            memoryCache = new Dictionary<object, WeakReference>();
        }
        #endregion //   Constructors

        #region Properties
        /// <summary>
        /// 获得缓存项数目(包含已被GC回收的项)
        /// </summary>
        public int Count
        {
            get { return memoryCache.Count; }
        }
        #endregion //   Properties

        #region Methods
        /// <summary>
        /// 是否缓存了指定键的缓存项(包含已被GC回收的项)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(object key)
        {
            ValidateKey(key);

            return memoryCache.ContainsKey(key);
        }
        /// <summary>
        /// 新增缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(object key, T value)
        {
            ValidateKey(key);

            lock (_syncRoot)
            {
                memoryCache[key] = new WeakReference(value);
            }
        }
        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
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
        public T GetData(object key)
        {
            lock (_syncRoot)
            {
                return memoryCache.ContainsKey(key) ? (T)memoryCache[key].Target : default(T);
            }
        }

        /// <summary>
        /// 获得缓存项(如果没有缓存则重新根据委托获取数据)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="getObjectDelegate"></param>
        /// <returns></returns>
        public T GetData(object key, GetObjectDelegate getObjectHandler)
        {
            T t = GetData(key);
            if (object.Equals(t, default(T)))
            {
                t = getObjectHandler(key);
                if (!object.Equals(t, default(T)))
                    Add(key, t);
            }

            return t;
        }

        /// <summary>
        /// 获得缓存项,同时测试是否缓存内容有效(如果得到的是对象类型的默认值,将认为该缓存项无效);
        /// 注意对象类型的默认值进行缓存是无效的;
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetData(object key, out T value)
        {
            // start:old
            //return !ReferenceEquals((value = GetData(key)), default(T));
            //  end:old

            // start:wj 20111115 altered
            if (Contains(key))
            {
                value = GetData(key);
                return memoryCache[key].IsAlive;
            }
            value = default(T);
            return false;
            //  end
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
        private static void ValidateKey(object key)
        {
            if (key == null)
            {
                throw new ArgumentException(string.Format("Parameter '{0}' can not be null.", "key"));
            }
        }
        #endregion //   Methods

        #region Internal Types
        public delegate T GetObjectDelegate(object key);
        #endregion //   Internal Types
    }
}
