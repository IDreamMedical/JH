using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures.C5.Wj
{
    public class BinaryTree<T>
    {
        private T value;
        private BinaryTree<T> left, right;

        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public BinaryTree<T> Left
        {
            get { return left; }
            set { left = value; }
        }

        public BinaryTree<T> Right
        {
            get { return right; }
            set { right = value; }
        }

        public BinaryTree(T val) : this(val, null, null) { }
        public BinaryTree(T val, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.value = val; this.left = left; this.right = right;
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

            if (left.Contains(t) || right.Contains(t))
                return true;

            return false;
        }

        public static void DepthFirst(BinaryTree<T> t, Act<T> act)
        {
            IStack<BinaryTree<T>> work = new ArrayList<BinaryTree<T>>();
            work.Push(t);
            while (!work.IsEmpty)
            {
                BinaryTree<T> cur = work.Pop();
                if (cur != null)
                {
                    work.Push(cur.right);
                    work.Push(cur.left);
                    act(cur.value);
                }
            }
        }

        public static void BreadthFirst(BinaryTree<T> t, Act<T> act)
        {
            IQueue<BinaryTree<T>> work = new CircularQueue<BinaryTree<T>>();
            work.Enqueue(t);
            while (!work.IsEmpty)
            {
                BinaryTree<T> cur = work.Dequeue();
                if (cur != null)
                {
                    work.Enqueue(cur.left);
                    work.Enqueue(cur.right);
                    act(cur.value);
                }
            }
        }

        public static void Traverse(BinaryTree<T> t, Act<T> act, IList<BinaryTree<T>> work)
        {
            work.Clear();
            work.Add(t);
            while (!work.IsEmpty)
            {
                BinaryTree<T> cur = work.Remove();
                if (cur != null)
                {
                    if (work.FIFO)
                    {
                        work.Add(cur.left);
                        work.Add(cur.right);
                    }
                    else
                    {
                        work.Add(cur.right);
                        work.Add(cur.left);
                    }
                    act(cur.value);
                }
            }
        }
    }
}
