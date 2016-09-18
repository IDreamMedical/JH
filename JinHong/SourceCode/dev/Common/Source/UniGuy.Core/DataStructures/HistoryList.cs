using System;
using System.Collections.Generic;
//  using System.Linq;
using System.Text;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 历史记录列表
    /// 主要是支持前进和后退
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HistoryList<T>: List<T>
    {
        #region Fields
        /// <summary>
        /// 当前项的索引
        /// </summary>
        private int currentIndex = -1;

        public event CurrentItemChangedDelegate CurrentItemChanged;
        #endregion //   Fields

        #region Properties
        /// <summary>
        /// 获得或者设置当前索引
        /// </summary>
        public int CurrentIndex
        {
            get { return currentIndex; }
            set 
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    OnCurrentItemChanged(CurrentItem);
                }
            }
        }
        /// <summary>
        /// 获得当前项
        /// </summary>
        public T CurrentItem
        {
            get { return this[currentIndex]; }
            set { CurrentIndex = this.IndexOf(value); }
        }
        #endregion //   Properties

        #region Methods
        private void OnCurrentItemChanged(T value)
        {
            if (CurrentItemChanged != null)
                CurrentItemChanged(value);
        }
        /// <summary>
        /// 是否可以后退
        /// </summary>
        /// <returns></returns>
        public bool CanBackward() 
        {
            return currentIndex > 0;
        }
        /// <summary>
        /// 是否可以前进
        /// </summary>
        /// <returns></returns>
        public bool CanForward() 
        {
            return currentIndex < this.Count - 1;
        }
        /// <summary>
        /// 后退
        /// </summary>
        public void Backward()
        {
            CurrentIndex--;
        }
        /// <summary>
        /// 前进
        /// </summary>
        public void Forward() 
        {
            CurrentIndex++;
        }
        /// <summary>
        /// 前进到新项
        /// </summary>
        /// <param name="value"></param>
        public void ForwardNew(T value) 
        {
            while (currentIndex < this.Count - 1)
                this.RemoveAt(currentIndex + 1);
            this.Add(value);
            this.CurrentIndex = this.Count - 1;
        }
        #endregion //   Methods

        #region Internal types
        public delegate void CurrentItemChangedDelegate(T currentItem);
        #endregion //   Internal types
    }
}
