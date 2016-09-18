using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core.Common;

namespace UniGuy.Core.Communications
{
    /// <summary>
    /// 全局事件管理器(中介):
    /// 用于一个程序的若干模块间的通信, 当前没有使用WeakEvent;
    /// 比如一个程序有若干松耦合的模块, 各个模块间需要通信, 则可以使用此类, 任何模块可以任意激发或者侦听消息; 
    /// 当然模块如果是.dll的形式, 再里面加上这种事件可以解决偶联问题, 但可能还是有点多余, 因为A.dll和B.dll的通信可以借助于引用此两者的C.exe来通过A, B的委托或者事件来协调;
    /// 所以此类的引用一般只用于应用级.dll, 而不建议用于核心.dll.
    /// </summary>
    /// <remarks>
    /// A模块和B模块通信的几种模式:
    /// 1. A, B相互引用, 直接沟通(A, B都将不太可能重用, 做做死程序可以);
    /// 2. A, B都设置事件, 不相互引用, 有外围C共同引用A, B并设置事件, 谁引用谁垃圾(A, B是可重用的, 推荐);
    /// 3. A, B都采用本方式, 不相互引用, 但是需要各自在代码中调用和实现本中消息, 包含强消息类型字符串, 看上去不爽; 但和1)相比, A, B不必相互引用, 也不必外围C做处理(可以考虑使用在一般应用中, 并在整个应用中该中消息应该自成体系)
    /// </remarks>
    public class GlobalEventManager
    {
        /// <summary>
        /// 指定事件名称的全局事件监听器字典
        /// </summary>
        private static Dictionary<object, List<GlobalEventHandler>> happenHandlers;

        /// <summary>
        /// 全局事件发生事件:
        /// 外部可以直接使用GlobalEventManager.GlobalEventHappen += SomeHandler;
        /// 也可以使用AddEventHappenHandler新增指定事件名称的监听器
        /// </summary>
        public static event GlobalEventHandler GlobalEventHappen;

        static GlobalEventManager()
        {
            //  调用全局事件发生时应该处理的指定名称事件处理方法
            GlobalEventHappen += new GlobalEventHandler(GlobalEventManager_GlobalEventHappen);
        }

        //  指定名称事件处理方法
        static void GlobalEventManager_GlobalEventHappen(object sender, GlobalEventArgs args)
        {
            if (happenHandlers != null)
            {
                foreach (object eventName in happenHandlers.Keys)
                {
                    if (eventName == args.EventName)
                    {
                        foreach (GlobalEventHandler handler in happenHandlers[eventName])
                        {
                            Guard.CheckDelegateNull(GlobalEventHappen);
                            handler(sender, args);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 激发全局事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void Invoke(object sender, GlobalEventArgs args)
        {
            Guard.CheckDelegateNull(GlobalEventHappen);
            GlobalEventHappen(sender, args);
        }

        /// <summary>
        /// 激发全局事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventName"></param>
        /// <param name="eventContent"></param>
        public static void Invoke(object sender, object eventName, object eventContent)
        {
            Invoke(sender, new GlobalEventArgs(eventName, eventContent));
        }

        /// <summary>
        /// 新增全局事件处理方法
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        public static void AddHappenHandler(object eventName, GlobalEventHandler handler)
        {
            if (happenHandlers == null)
                happenHandlers = new Dictionary<object, List<GlobalEventHandler>>();
            if (happenHandlers.ContainsKey(eventName))
                happenHandlers[eventName].Add(handler);
            else
                happenHandlers[eventName] = new List<GlobalEventHandler> { handler };
        }

        /// <summary>
        /// 移除全局事件处理方法
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        public static void RemoveHappenHandler(object eventName, GlobalEventHandler handler)
        {
            if (happenHandlers != null)
            {
                if (happenHandlers[eventName] != null)
                {
                    happenHandlers[eventName].Remove(handler);
                    if (happenHandlers[eventName].Count == 0)
                        happenHandlers.Remove(eventName);
                }
            }
        }

        /// <summary>
        /// 清空某个名称的所有事件处理方法
        /// </summary>
        /// <param name="eventName"></param>
        public static void ClearHanppenHandler(object eventName)
        {
            if (happenHandlers != null)
                happenHandlers.Remove(eventName);
        }

        /// <summary>
        /// 清空所有有名称的事件处理方法, 不包括直接使用GlobalEventManager.GlobalEventHappen += SomeHandler的事件
        /// </summary>
        /// <param name="eventName"></param>
        public static void ClearAllHanppenHandler(object eventName)
        {
            happenHandlers = null;
        }
    }

    public delegate void GlobalEventHandler(object sender, GlobalEventArgs args);
    public class GlobalEventArgs : EventArgs
    {
        private object eventName;
        private object eventContent;

        public object EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }

        public object EventContent
        {
            get { return eventContent; }
            set { eventContent = value; }
        }

        public GlobalEventArgs() : base() { }
        public GlobalEventArgs(object eventName, object eventContent)
            : base()
        {
            this.eventName = eventName;
            this.eventContent = eventContent;
        }
    }
}