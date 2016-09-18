using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

//  WJ 20110228
namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 保存缓存对象弱引用和相关的信息
    /// </summary>
    internal class WeakCacheItem
    {
        #region Fields
        /// <summary>
        /// 缓存对象的弱引用(这个引用的Target如果为null,外部一经发现应将整个CachedObjectItem置null)
        /// </summary>
        private WeakReference wrefObject;
        /// <summary>
        /// 对象被缓存的时间
        /// </summary>
        private DateTime cacheTime;
        /// <summary>
        /// 缓存的持续时间(默认很久很久)
        /// </summary>
        private TimeSpan duration = TimeSpan.MaxValue;
        #endregion  //  Fields

        #region Properties
        /// <summary>
        /// 获得或者设置对象强引用
        /// </summary>
        public object CacheObject
        {
            get 
            {
                return wrefObject.Target; 
            }
            set 
            { 
                wrefObject = new WeakReference(value);
            }
        }
        /// <summary>
        /// 获得缓存时间
        /// </summary>
        public DateTime CacheTime
        {
            get { return cacheTime; }
        }
        /// <summary>
        /// 设置缓存持续时间
        /// </summary>
        public TimeSpan Duration
        {
            set { duration = value; }
        }
        /// <summary>
        /// 获得是否还有效
        /// </summary>
        public bool IsValid
        {
            get { return DateTime.Now - cacheTime < duration && wrefObject.Target!=null; }
        }
        #endregion  //  Properties

        #region Ctor
        public WeakCacheItem(object cacheObject)
        {
            this.CacheObject = cacheObject;
            this.cacheTime = DateTime.Now;
        }
        public WeakCacheItem(object content, TimeSpan duration)
            : this(content)
        {
            if (duration > TimeSpan.Zero)
                this.duration = duration;
        }
        #endregion //   Ctor

        #region Methods
        /// <summary>
        /// 获得缓存的对象,如果失败则为null
        /// </summary>
        /// <returns></returns>
        public object GetObject()
        {
            return IsValid ? CacheObject : null;
        }
        /// <summary>
        /// 获得缓存的泛型对象,入股失败则为默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObject<T>()
        {
            return IsValid ? CacheObject is T ? (T)CacheObject : default(T) : default(T);
        }
        /// <summary>
        /// 重置缓存时间,这将延长缓存生存期
        /// </summary>
        public void ResurrectCacheTime()
        {
            cacheTime = DateTime.Now;
        }
        #endregion  //  Methods
    }
}
