using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 保存缓存对象和相关的信息
    /// </summary>
    internal class CacheItem
    {
        #region Fields
        /// <summary>
        /// 缓存对象的强引用(这个引用永远不为null,如果为null则不应该存在整个CachedObjectItem)
        /// </summary>
        private object cacheObject;
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
            get { return cacheObject; }
            set { cacheObject = value; }
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
            get { return DateTime.Now - cacheTime < duration; }
        }
        #endregion  //  Properties

        #region Ctor
        public CacheItem(object cacheObject)
        {
            this.cacheObject = cacheObject;
            this.cacheTime = DateTime.Now;
        }
        public CacheItem(object content, TimeSpan duration)
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
            return IsValid ? cacheObject : null;
        }
        /// <summary>
        /// 获得缓存的泛型对象,入股失败则为默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObject<T>()
        {
            return IsValid?cacheObject is T?(T)cacheObject:default(T):default(T);
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
