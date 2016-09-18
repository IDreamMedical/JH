using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;
using UniGuy.Core;

//  WJ 20110228
namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 弱引用缓存管理器(主要用于创建不便宜但耗费内存不多的对象;如果是创建便宜并且耗费内存不多,建议不用缓存;如果是创建便宜但是耗费内存较多,则可以考虑使用WeakCacheManager)
    /// 目前线程不安全,可能要做扩展处理//   TODO
    /// </summary>
    public class WeakCacheManager
    {
        #region Fields
        /// <summary>
        /// 默认缓存保留时间,这个静态值可以在外部调整
        /// </summary>
        public static TimeSpan DefaultDuration;
        /// <summary>
        /// 如果为True,对缓存的访问会重置对象的缓存时间.默认为False.
        /// </summary>
        public static bool CacheTimeResurrect;
        /// <summary>
        /// 缓存容器,这是一个静态的二级字典
        /// </summary>
        private static Dictionary<Type, Dictionary<string, WeakCacheItem>> cacheContainer;

        /// <summary>
        /// 通放处理器,在使用ThroughPut方法的时候需要首先在外部定义这个处理方法
        /// </summary>
        public static ThroughPutDelegate ThroughPutHandler;

        #endregion

        #region Methods

        #region Ensure
        /// <summary>
        /// 获得确保存在的缓存容器
        /// </summary>
        /// <returns></returns>
        private static Dictionary<Type, Dictionary<string, WeakCacheItem>> EnsureContainer()
        {
            return cacheContainer ?? (cacheContainer = new Dictionary<Type, Dictionary<string, WeakCacheItem>>());
        }
        /// <summary>
        /// 获得确保存在的类缓存容器
        /// </summary>
        /// <param name="type">针对的类型</param>
        /// <returns></returns>
        private static Dictionary<string, WeakCacheItem> EnsureTypeContainer(Type type)
        {
            Dictionary<Type, Dictionary<string, WeakCacheItem>> ensureContainer = EnsureContainer();
            if (!(ensureContainer.ContainsKey(type) && ensureContainer[type] != null))
                ensureContainer[type] = new Dictionary<string, WeakCacheItem>();
            return ensureContainer[type];
        }
        /// <summary>
        /// 获得确保存在的类缓存容器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Dictionary<string, WeakCacheItem> EnsureTypeContainer<T>()
        {
            return EnsureTypeContainer(typeof(T));
        }
        #endregion

        #region Exists
        /// <summary>
        /// 是否有缓存容器
        /// </summary>
        /// <returns></returns>
        private static bool HasContainer()
        {
            return cacheContainer != null;
        }
        /// <summary>
        /// 是否有类缓存容器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool HasTypeContainer(Type type)
        {
            if (HasContainer())
                return cacheContainer.ContainsKey(type);
            return false;
        }
        /// <summary>
        /// 是否有类缓存容器(类用泛型参数)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static bool HasTypeContainer<T>()
        {
            return HasTypeContainer(typeof(T));
        }
        #endregion

        #region Clear
        /// <summary>
        /// 清空缓存
        /// </summary>
        private static void ClearContainer()
        {
            cacheContainer = null;
        }
        /// <summary>
        /// 清空类缓存
        /// </summary>
        /// <param name="type"></param>
        private static void ClearTypeContainer(Type type)
        {
            if (HasTypeContainer(type))
                cacheContainer[type] = null;
        }
        /// <summary>
        /// 清空类缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static void ClearTypeContainer<T>()
        {
            ClearTypeContainer(typeof(T));
        }
        /// <summary>
        /// Dump Dump All.
        /// </summary>
        public static void Dump()
        {
            ClearContainer();
            //  手动调用垃圾回收
            GC.Collect();
        }
        #endregion

        #region Put
        /// <summary>
        /// 直接放到object缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheObject"></param>
        /// <param name="duration"></param>
        public static void DirectPut(string key, object cacheObject, TimeSpan duration)
        {
            Put<object>(key, cacheObject, duration);
        }
        public static void DirectPut(string key, object cacheObject)
        {
            DirectPut(key, cacheObject, DefaultDuration);
        }

        public static void Put(Type type, string key, object cacheObject, TimeSpan duration)
        {
            //  空对象不需要缓存
            if (cacheObject == null)
                return;

            EnsureTypeContainer(type)[key] = new WeakCacheItem(cacheObject, duration);
        }
        /// <summary>
        /// 把任意对象放到缓存,自动识别对象的类型
        /// 用这种方式放的对象在Get的时候必须用给对象的完全匹配类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheObject"></param>
        /// <param name="duration"></param>
        public static void Put(string key, object cacheObject, TimeSpan duration)
        {
            //  空对象不需要缓存
            if (cacheObject == null)
                return;

            Type type = cacheObject.GetType();

            EnsureTypeContainer(type)[key] = new WeakCacheItem(cacheObject, duration);
        }
        /// <summary>
        /// 把指定类型的对象放到缓存中,cacheObject可以是T的SubClass
        /// 用这种方式放的对象在Get的时候仍要用T类型
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cacheObject">缓存对象</param>
        /// <param name="duration">生存时间</param>
        public static void Put<T>(string key, T cacheObject, TimeSpan duration)
        {
            //  如果为默认值,不需要缓存
            if ((object)cacheObject == (object)default(T))
                return;

            EnsureTypeContainer<T>()[key] = new WeakCacheItem(cacheObject, duration);
        }

        public static void Put(Type type, string key, object cacheObject)
        {
            Put(type, key, cacheObject, DefaultDuration);
        }
        /// <summary>
        /// 把任意对象放到缓存,自动识别对象的类型(使用默认生存时间)
        /// 用这种方式放的对象在Get的时候必须用给对象的完全匹配类型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheObject"></param>
        public static void Put(string key, object cacheObject)
        {
            Put(key, cacheObject, DefaultDuration);
        }
        /// <summary>
        /// 把对象放到缓存中(使用默认生存时间)
        /// 用这种方式放的对象在Get的时候仍要用T类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheObject"></param>
        public static void Put<T>(string key, T cacheObject)
        {
            Put<T>(key, cacheObject, DefaultDuration);
        }

        public static void Put(Type type, IIdObject cacheObject, TimeSpan duration)
        {
            Put(type, cacheObject.Id, cacheObject, duration);
        }

        /// <summary>
        /// 把IIdObject放缓存(Key使用其Id,类型自动识别)
        /// </summary>
        /// <param name="cacheObject"></param>
        /// <param name="duration"></param>
        public static void Put(IIdObject cacheObject, TimeSpan duration)
        {
            Put(cacheObject.Id, cacheObject, duration);
        }
        /// <summary>
        /// 把IIdObject放缓存(Key使用其Id,类型给定)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheObject"></param>
        /// <param name="duration"></param>
        public static void Put<T>(IIdObject cacheObject, TimeSpan duration) where T:IIdObject
        {
            Put<T>(cacheObject.Id, (T)cacheObject, duration);
        }
        public static void Put(Type type, IIdObject cacheObject)
        {
            Put(type, cacheObject, DefaultDuration);
        }
        /// <summary>
        /// 把IIdObject放缓存(Key使用其Id,类型自动识别)
        /// </summary>
        /// <param name="cacheObject"></param>
        public static void Put(IIdObject cacheObject)
        {
            Put(cacheObject, DefaultDuration);
        }
        /// <summary>
        /// 把IIdObject放缓存(Key使用其Id,类型给定)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheObject"></param>
        public static void Put<T>(IIdObject cacheObject)
        {
            Put<T>(cacheObject.Id, (T)cacheObject, DefaultDuration);
        }
        #endregion

        #region Get
        /// <summary>
        /// 直接从object类获得对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object DirectGet(string key)
        {
            return Get<object>(key);
        }
        /// <summary>
        /// 根据类型和键获得缓存对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(Type type, string key)
        {
            if (HasTypeContainer(type))
            {
                if (cacheContainer[type].ContainsKey(key))
                {
                    if (CacheTimeResurrect && cacheContainer[type][key].IsValid)
                        cacheContainer[type][key].ResurrectCacheTime();
                    return cacheContainer[type][key].GetObject();
                }
            }
            return null;
        }
        /// <summary>
        /// 根据类型和键获得缓存对象(泛型方式)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            if (HasTypeContainer<T>())
            {
                if (cacheContainer[typeof(T)].ContainsKey(key))
                {
                    if (CacheTimeResurrect && cacheContainer[typeof(T)][key].IsValid)
                        cacheContainer[typeof(T)][key].ResurrectCacheTime();
                    return cacheContainer[typeof(T)][key].GetObject<T>();
                }
            }
            return default(T);
        }
        #endregion

        #region ThroughPut
        public static object DirectThroughPut(string key, TimeSpan duration)
        {
            return ThroughPut(typeof(object), key, duration);
        }
        public static object DirectThroughPut(string key)
        {
            return DirectThroughPut(key, DefaultDuration);
        }
        /// <summary>
        /// 根据给定的类型和键获得对象,如果为null,则调用委托获得对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object ThroughPut(Type type, string key, TimeSpan duration)
        {
            object cacheObject = Get(type, key);

            //  如果没有缓存
            if (cacheObject == null)
            {
                if (ThroughPutHandler == null)
                    throw new NotImplementedException("ThroughPutHandler not implemented.");
                Put(key, cacheObject = ThroughPutHandler(type, key), duration);
            }

            return cacheObject;
        }
        /// <summary>
        /// 根据给定的类型和键获得对象,如果为null,则调用委托获得对象(泛型方式)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ThroughPut<T>(string key, TimeSpan duration)
        {
            object cacheObject = ThroughPut(typeof(T), key, duration);
            return cacheObject == null ? default(T) : (T)cacheObject;
        }
        /// <summary>
        /// 根据给定的类型和键获得对象,如果为null,则调用委托获得对象(使用默认缓存生存时间)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object ThroughPut(Type type, string key)
        {
            return ThroughPut(type, key, DefaultDuration);
        }
        /// <summary>
        /// 根据给定的类型和键获得对象,如果为null,则调用委托获得对象(泛型方式),并使用默认生存时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ThroughPut<T>(string key)
        {
            return ThroughPut<T>(key, DefaultDuration);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 根据类型和键从缓存中清除一个缓存对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        public static void Remove(Type type, string key)
        {
            if (HasTypeContainer(type))
            {
                if (cacheContainer[type].ContainsKey(key))
                    cacheContainer[type].Remove(key);
                if (cacheContainer[type].Count == 0)
                    cacheContainer.Remove(type);
            }
        }
        /// <summary>
        /// 根据类型和键从缓存中清除一个缓存对象(使用泛型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public static void Remove<T>(string key)
        {
            Remove(typeof(T), key);
        }
        #endregion

        #endregion
    }
}
