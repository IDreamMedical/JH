using System;
using System.Collections.Generic;
using System.Text;

namespace UniGuy.Core.DataStructures.C5.Wj
{
    public class Tree<T>
    {
        private T value;
        private IList<Tree<T>> children;

        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public IList<Tree<T>> Children
        {
            get { return children; }
            set { children = value; }
        }

        public Tree(T val) { this.value = val; }
        public Tree(T val, IList<Tree<T>> children)
            :this(val)
        {
            this.children = children;
        }
        public Tree(T val, params Tree<T>[] children)
            : this(val)
        { 
            this.children = new ArrayList<Tree<T>>();
            foreach (Tree<T> child in children)
                this.children.Add(child);
        }

        /// <summary>
        /// 判断树中是否有Value为t的节点树
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Contains(T t)
        {
            if (EqualityComparer<T>.Default.Equals(t, value))
                return true;

            for (int i = 0; i < children.Count; i++)
                if (children[i].Contains(t))
                    return true;

            return false;
        }

        public static void DepthFirst(Tree<T> t, Act<T> act)
        {
            IStack<Tree<T>> work = new ArrayList<Tree<T>>();
            work.Push(t);
            while (!work.IsEmpty)
            {
                Tree<T> cur = work.Pop();
                if (cur != null)
                {
                    if (cur.children != null)
                        for (int i = cur.children.Count - 1; i >= 0;i-- )
                            work.Push(cur.children[i]);
                    act(cur.value);
                }
            }
        }

        public static void BreadthFirst(Tree<T> t, Act<T> act)
        {
            IQueue<Tree<T>> work = new CircularQueue<Tree<T>>();
            work.Enqueue(t);
            while (!work.IsEmpty)
            {
                Tree<T> cur = work.Dequeue();
                if (cur != null)
                {
                    if (cur.children != null)
                        for (int i = 0; i <cur.children.Count; i++)
                            work.Enqueue(cur.children[i]);
                    act(cur.value);
                }
            }
        }

        public static void Traverse(Tree<T> t, Act<T> act, IList<Tree<T>> work)
        {
            work.Clear();
            work.Add(t);
            while (!work.IsEmpty)
            {
                Tree<T> cur = work.Remove();
                if (cur != null)
                {
                    if (work.FIFO)
                    {
                        if (cur.children != null)
                            for (int i = 0; i < cur.children.Count;i++ )
                                work.Add(cur.children[i]);
                    }
                    else
                    {
                        if (cur.children != null)
                            for (int i = cur.children.Count-1; i >=0; i--)
                                work.Add(cur.children[i]);
                    }
                    act(cur.value);
                }
            }
        }
    }
}
