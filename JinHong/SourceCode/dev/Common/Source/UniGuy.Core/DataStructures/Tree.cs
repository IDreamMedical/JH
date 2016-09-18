using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

// wj
//  Created 20120415
//  LastModified 20120415
namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 一个简单的树结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>有些时候Tree只有叶子才有值, 这个在程序代码中控制, 本类不做控制; 下面几个类相同.</remarks>
    public class Tree<T> : Collection<Tree<T>>
    {
        public T Value { get; set; }
        public Tree<T> Parent { get; set; }

        #region Methods

        #region Generic
        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(Action<Tree<T>> action)
        {
            if (this.Count != 0)
                for (int i = 0; i < this.Count; i++)
                    this[i].Traverse(action);
            action(this);
        }
        #endregion

        #region Overrides
        protected override void InsertItem(int index, Tree<T> item)
        {
            item.Parent = this;
            base.InsertItem(index, item);
        }
        protected override void SetItem(int index, Tree<T> item)
        {
            item.Parent = this;
            base.SetItem(index, item);
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 这个类同上, 但是用于被继承, 比如需要在Tree上加一些属性等;
    /// 一般继承方法:
    /// public class SomeTree: Tree<T, SomeTree>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public abstract class Tree<T, U> : Collection<U> where U:Tree<T, U>
    {
        public T Value { get; set; }
        public U Parent { get; set; }

        #region Methods

        #region Generic
        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(Action<Tree<T, U>> action)
        {
            if (this.Count != 0)
                for (int i = 0; i < this.Count; i++)
                    this[i].Traverse(action);
            action(this);
        }

        #endregion

        #region Overrides
        protected override void InsertItem(int index, U item)
        {
            item.Parent = (U)this;
            base.InsertItem(index, item);
        }
        protected override void SetItem(int index, U item)
        {
            item.Parent = (U)this;
            base.SetItem(index, item);
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 一个简单的树结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableTree<T> : ObservableCollection<ObservableTree<T>>
    {
        #region Fields
        private T value;
        #endregion

        #region Properties
        public virtual T Value 
        {
            get { return value; }
            set
            {
                if (!EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    this.value = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Value"));
                }
            }
        }
        public ObservableTree<T> Parent { get; set; }
        #endregion

        #region Methods

        #region Generic
        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(Action<ObservableTree<T>> action)
        {
            if (this.Count != 0)
                for (int i = 0; i < this.Count; i++)
                    this[i].Traverse(action);
            action(this);
        }
        #endregion

        #region Overrides
        protected override void InsertItem(int index, ObservableTree<T> item)
        {
            item.Parent = this;
            base.InsertItem(index, item);
        }
        protected override void SetItem(int index, ObservableTree<T> item)
        {
            item.Parent = this;
            base.SetItem(index, item);
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 这个类同上, 但是用于被继承, 比如需要在Tree上加一些属性等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ObservableTree<T, U> : ObservableCollection<U> where U: ObservableTree<T, U>
    {
        #region Fields
        private T value;
        #endregion

        #region Properties
        public virtual T Value
        {
            get { return value; }
            set
            {
                if (!EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    this.value = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Value"));
                }
            }
        }
        public U Parent { get; set; }
        #endregion

        #region Methods

        #region Generic
        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="action"></param>
        public void Traverse(Action<ObservableTree<T, U>> action)
        {
            if (this.Count != 0)
                for (int i = 0; i < this.Count; i++)
                    this[i].Traverse(action);
            action(this);
        }
        #endregion

        #region Overrides
        protected override void ClearItems()
        {
            base.ClearItems();
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        }
        protected override void InsertItem(int index, U item)
        {
            item.Parent = (U)this;
            base.InsertItem(index, item);
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        }
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        }
        protected override void SetItem(int index, U item)
        {
            item.Parent = (U)this;
            base.SetItem(index, item);
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        }
        #endregion
        #endregion
    }
}
