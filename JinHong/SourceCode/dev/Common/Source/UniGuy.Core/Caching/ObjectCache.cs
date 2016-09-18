using System;
using System.Collections;
using UniGuy.Core;
using UniGuy.Core.Helpers;

namespace UniGuy.Core.Cache
{
    /// <summary>
    /// 对象缓存
    /// </summary>
    public class ObjectCache
    {
        #region Fields
        /// <summary>
        /// 对象缓存表
        /// </summary>
        private static Hashtable objectTable = new Hashtable();
        #endregion

        #region Delegates
        public delegate void GetProcessDelegate(Type type, string id, ref IIdObject obj);
        public static GetProcessDelegate GetProcessHandler;
        public delegate void SetProcessDelegate(Type type, string id, IIdObject obj);
        public static SetProcessDelegate SetProcessHandler;
        #endregion

        #region Properties
        /// <summary>
        /// 获得对象缓存表
        /// </summary>
        public static Hashtable ObjectTable
        {
            get { return objectTable; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 获得指定类型对象的缓存表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Hashtable GetTable(Type type)
        {
            return objectTable[type] as Hashtable;
        }
        /// <summary>
        /// 添加对象到缓存
        /// </summary>
        /// <param name="obj">要添加的对象</param>
        public static void Add(IIdObject obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                Hashtable table = objectTable[type] as Hashtable;
                if (table == null)
                    objectTable[type] = table = new Hashtable();
                table[obj.Id] = obj;
            }
        }
        /// <summary>
        /// 从缓存移除对象
        /// </summary>
        /// <param name="obj">要移除的对象</param>
        public static void Remove(IIdObject obj)
        {
            if (obj != null)
            {
                Hashtable table = objectTable[obj.GetType()] as Hashtable;
                if (table != null)
                    Helper.SetDictionaryValue(table, obj.Id, obj);
            }
        }
        /// <summary>
        /// 移除指定类型指定id的对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="id">标识</param>
        public static void Remove(Type type, string id)
        {
            Hashtable table = objectTable[type] as Hashtable;
            if (table != null)
                if (table.ContainsKey(id))
                    table.Remove(id);
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void Clear()
        {
            objectTable.Clear();
        }
        /// <summary>
        /// 清空指定类型对象的缓存
        /// </summary>
        /// <param name="type"></param>
        public static void Clear(Type type)
        {
            Hashtable table = objectTable[type] as Hashtable;
            if (table != null)
                table.Clear();
        }
        /// <summary>
        /// 获得对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象标识</param>
        /// <returns>对象</returns>
        public static IIdObject Get(Type type, string id, bool refetch)
        {
            IIdObject obj = null;
            Hashtable table = objectTable[type] as Hashtable;
            if (table != null)
                obj = Helper.GetDictionaryValue(table, id) as IIdObject;

            //  获得对象处理委托，建议如果缓存中没有，则载入
            if (refetch || obj == null || IsTimeout(obj))
            {
                if (GetProcessHandler != null)
                    GetProcessHandler(type, id, ref obj);
                else
                    throw new Exception(string.Format("'{0}' not implemented.", GetProcessHandler.Method.Name));

                //  设置到缓存
                if (obj != null)
                    Add(obj);
            }
            return obj;
        }

        public static IIdObject Get(Type type, string id)
        {
            return Get(type, id, false);
        }

        public static T Get<T>(string id, bool refetch) where T : IIdObject
        {
            IIdObject obj = Get(typeof(T), id, refetch);
            return obj is T ? (T)obj : default(T);
        }

        public static T Get<T>(string id) where T : IIdObject
        {
            return Get<T>(id, false);
        }

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="obj">对象</param>
        public static void Set(IIdObject obj)
        {
            if (obj != null)
            {
                //  设置对象处理委托，建议同时更改存储源
                if (SetProcessHandler != null)
                    SetProcessHandler(obj.GetType(), obj.Id, obj);
                else
                    throw new Exception(string.Format("'{0}' not implemented.", SetProcessHandler.Method.Name));

                Add(obj);
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象标识</param>
        public static void Delete(Type type, string id)
        {
            Remove(type, id);

            //  设置对象处理委托，建议同时删除存储源
            if (SetProcessHandler != null)
                SetProcessHandler(type, id, null);
            else
                throw new Exception(string.Format("'{0}' not implemented.", SetProcessHandler.Method.Name));
        }
        public static void Delete<T>(string id) where T : IIdObject
        {
            Delete(typeof(T), id);
        }
        public static void Delete(IIdObject obj)
        {
            Delete(obj.GetType(), obj.Id);
        }
        /// <summary>
        /// 测试缓存是否包含指定类型指定标识的对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象标识</param>
        /// <returns>测试结果</returns>
        public static bool Contains(Type type, string id)
        {
            return Get(type, id) != null;
        }
        public static bool Contains<T>(string id) where T : IIdObject
        {
            return Contains(typeof(T), id);
        }
        public static bool Contains(IIdObject obj)
        {
            return Contains(obj.GetType(), obj.Id);
        }
        /// <summary>
        /// 判断某个对象是否已经超时
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsTimeout(IIdObject obj)
        {
            PropertiesObject po = obj as PropertiesObject;
            if (po != null)
            {
                Type t = obj.GetType();
                CacheTimeoutAttribute ctAttr = Attribute.GetCustomAttribute(t, typeof(CacheTimeoutAttribute)) as CacheTimeoutAttribute;
                if (ctAttr != null)
                {
                    TimeSpan duration = ctAttr.TimeSpan;
                    DateTime? loadTime = po.GetPropertyValue("loadTime") as DateTime?;
                    return DateTime.Now - loadTime > duration;
                }
            }
            return false;
        }
        /// <summary>
        /// 设置类型的超时时间
        /// </summary>
        /// <param name="type"></param>
        /// <param name="duration"></param>
        /// <returns>是否设置成功</returns>
        public static bool SetTimeout(Type type, TimeSpan duration)
        {
            CacheTimeoutAttribute ctAttr = Attribute.GetCustomAttribute(type, typeof(CacheTimeoutAttribute)) as CacheTimeoutAttribute;
            if (ctAttr != null)
            {
                ctAttr.TimeSpan = duration;
                return true;
            }
            return false;
        }
        #endregion
    }
}
