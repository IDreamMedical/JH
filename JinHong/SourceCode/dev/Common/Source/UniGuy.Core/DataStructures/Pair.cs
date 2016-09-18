using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 对
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class Pair<T, U>
    {
        /// <summary>
        /// 第一个成员
        /// </summary>
        private T first;
        /// <summary>
        /// 第二个成员
        /// </summary>
        private U second;

        /// <summary>
        /// 获得或者设置第一个成员
        /// </summary>
        public T First
        {
            get { return first; }
            set { first = value; }
        }

        /// <summary>
        /// 获得或者设置第二个成员
        /// </summary>
        public U Second
        {
            get { return second; }
            set { second = value; }
        }

        public Pair() { }
        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }
    }

    public class Pair<T> : Pair<T, T>
    {
        public Pair() { }
        public Pair(T first, T second)
        {
            this.First = first;
            this.Second = second;
        }
    }

    public class StringPair : Pair<string>
    {
        public StringPair():base() { }
        public StringPair(string first, string second):base(first, second)
        {
        }
    }

    public class Pair3<T1, T2, T3>
    {
        /// <summary>
        /// 第一个成员
        /// </summary>
        private T1 item1;
        /// <summary>
        /// 第二个成员
        /// </summary>
        private T2 item2;
        /// <summary>
        /// 第三个成员
        /// </summary>
        private T3 item3;

        /// <summary>
        /// 获得或者设置第一个成员
        /// </summary>
        public T1 Item1
        {
            get { return item1; }
            set { item1 = value; }
        }

        /// <summary>
        /// 获得或者设置第二个成员
        /// </summary>
        public T2 Item2
        {
            get { return item2; }
            set { item2 = value; }
        }

        /// <summary>
        /// 获得或者设置第三个成员
        /// </summary>
        public T3 Item3
        {
            get { return item3; }
            set { item3 = value; }
        }

        public Pair3() { }
        public Pair3(T1 item1, T2 item2, T3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }
    }

    public class Pair3<T> : Pair3<T, T, T>
    {
        public Pair3() :base(){ }
        public Pair3(T item1, T item2, T item3)
            : base(item1, item2, item3)
        {
        }
    }
}
