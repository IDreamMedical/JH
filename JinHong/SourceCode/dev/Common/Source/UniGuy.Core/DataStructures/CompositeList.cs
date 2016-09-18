using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    // 20140605
    /// <summary>
    /// 使用Composite Pattern的一个简单例子
    /// </summary>
    public class CompositeList<T> : IList<T>
    {
        private readonly List<IList<T>> children = new List<IList<T>>();

        public List<IList<T>> Children
        {
            get { return children; }
        }

        public CompositeList(IEnumerable<IList<T>> children)
        {
            this.Children.AddRange(children);
        }

        public CompositeList(params IList<T>[] children)
            : this((IEnumerable<IList<T>>)children)
        {
        }

        public void AddChild(IList<T> child)
        {
            this.Children.Add(child);
        }

        public void RemoveChild(IList<T> child)
        {
            this.Children.Remove(child);
        }

        public void ClearChildren()
        {
            this.Children.Clear();
        }

        public int IndexOf(T item)
        {
            int temp = 0;
            foreach (IList<T> child in Children)
            {
                int temp2 = child.IndexOf(item);
                if (temp >= 0)
                    return temp + temp2;
                else
                    temp += child.Count;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
            /*
            foreach (IList<T> child in Children)
            {
                if (index < child.Count)
                {
                    child.Insert(index, item);
                    return;
                }
                index -= child.Count;
            }*/
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
            /*
            foreach (IList<T> child in Children)
            {
                if (index < child.Count)
                {
                    child.RemoveAt(index);
                    return;
                }
                index -= child.Count;
            }*/
        }

        public T this[int index]
        {
            get
            {
                foreach (IList<T> child in Children)
                {
                    if (index < child.Count)
                        return child[index];
                    index -= child.Count;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                throw new NotImplementedException();
            /*
                foreach (IList<T> child in Children)
                {
                    if (index < child.Count)
                    {
                        child[index] = value;
                        return;
                    }
                    index -= child.Count;
                }
                throw new IndexOutOfRangeException();
             */
            }
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
            /*
            foreach (IList<T> child in Children)
            {
                child.Clear();
            }*/
        }

        public bool Contains(T item)
        {
            foreach (IList<T> child in Children)
            {
                if (child.Contains(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < Count && i < array.Length - arrayIndex; i++)
            {
                array[arrayIndex + 1] = this[i];
            }
        }

        public int Count
        {
            get { return Children.Sum(c => c.Count); }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        internal class CompositeEnumerator : IEnumerator<T>
        {
            CompositeEnumerable<T> @this;
            int curIndex = -1;
            IEnumerator<T> curIe = null;
            internal CompositeEnumerator(CompositeEnumerable<T> @this)
            {
                this.@this = @this;
            }

            public T Current
            {
                get { return curIe.Current; }
            }

            public void Dispose()
            {
                
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (curIe != null && curIe.MoveNext())
                    return true;
                else
                {
                    if (++curIndex >= @this.Children.Count)
                        return false;
                    else
                    {
                        curIe = @this.Children[curIndex].GetEnumerator();
                        return MoveNext();
                    }
                }
            }

            public void Reset()
            {
                curIndex = -1;
            }
        }
    }
}
