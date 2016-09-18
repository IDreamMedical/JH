using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    //  20140605 Not tested
    /// <summary>
    /// 使用Composite Pattern的一个简单例子
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CompositeEnumerable<T>:IEnumerable<T>
    {
        private readonly List<IEnumerable<T>> children = new List<IEnumerable<T>>();

        public List<IEnumerable<T>> Children
        {
            get { return children; }
        }

        public CompositeEnumerable(IEnumerable<IEnumerable<T>> children)
        {
            this.Children.AddRange(children);
        }

        public CompositeEnumerable(params IEnumerable<T>[] children):this((IEnumerable<IEnumerable<T>>)children)
        {
        }

        public void AddChild(IEnumerable<T> child)
        {
            this.Children.Add(child);
        }

        public void RemoveChild(IEnumerable<T> child)
        {
            this.Children.Remove(child);
        }

        public void ClearChildren()
        {
            this.Children.Clear();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new CompositeEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new CompositeEnumerator(this);
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
