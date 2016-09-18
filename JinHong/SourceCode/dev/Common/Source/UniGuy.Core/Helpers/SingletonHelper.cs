using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.Helpers
{
    /// <summary>
    /// 单态对象助手类
    /// </summary>
    public class SingletonHelper
    {
        private static Hashtable singletonTable;
        public static void Set<T>(T t)
        {
            if (singletonTable == null)
                singletonTable = new Hashtable();
            if (t != null)
                singletonTable[typeof(T)] = t;
            else
                singletonTable.Remove(typeof(T));
        }
        public static T Get<T>()
        {
            if (singletonTable == null)
                return default(T);
            return (T)singletonTable[typeof(T)];
        }
    }
}
