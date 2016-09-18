using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 对象缓存
    /// 注意:
    /// 该缓存对象采用类型作为一级键，对象的HashCode作为二级键，如果对象缓存后改变内容而致HashCode改变则缓存失效，
    /// 甚至妨碍GAC的工作，建议重写将缓存类型的GetHashCode方法，将其返回Id。
    /// </summary>
    public class ObjectCache2
    {
        #region Fields
        /// <summary>
        /// 单态实例字段
        /// </summary>
        private static ObjectCache2 oc;
        /// <summary>
        /// 缓存字典
        /// </summary>
        private Dictionary<Type, object> dictType;
        #endregion

        /// <summary>
        /// 获得单态缓存对象
        /// </summary>
        public static ObjectCache2 Singleton
        {
            get { return oc ?? (oc = new ObjectCache2()); }
        }

        #region Methods
        /// <summary>
        /// 新增到缓存
        /// </summary>
        /// <param name="obj"></param>
        public void Put<T>(string key, T obj)
        {
            if (obj == null)
                return;
            if (dictType == null)
                dictType = new Dictionary<Type, object>();
            Dictionary<string, T> dictT=null;
            Type t = typeof(T);
            if (dictType.ContainsKey(t))
                dictT = (Dictionary<string, T>)dictType[t];
            else
                dictType[t]=dictT = new Dictionary<string, T>();
            dictT[key] = obj;
        }
        /// <summary>
        /// 从缓存中获得
        /// </summary>
        /// <param name="t"></param>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        public T Retrieve<T>(string key)
        {
            if (dictType!=null && key!=null && dictType.ContainsKey(typeof(T)))
            {
                Dictionary<string, T> dictT = (Dictionary<string, T>)dictType[typeof(T)];
                if (dictT.ContainsKey(key))
                    return dictT[key];
            }
            return default(T);
        }

        /// <summary>
        /// 从缓存中清除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public void Remove<T>(string key)
        {
            if (dictType != null && dictType.ContainsKey(typeof(T)))
            {
                Dictionary<string, T> dictT = (Dictionary<string, T>)dictType[typeof(T)];
                if (dictT.ContainsKey(key))
                    dictT.Remove(key);
            }
        }
        #endregion
    }
}
