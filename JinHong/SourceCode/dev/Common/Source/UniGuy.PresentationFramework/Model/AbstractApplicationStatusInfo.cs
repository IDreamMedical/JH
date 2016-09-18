using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using UniGuy.Core.Model;

namespace UniGuy.Windows.Model
{
    /// <summary>
    /// 应用程序状态信息抽象基类
    /// Note: Not Tested...
    /// </summary>
    /// <example>
    /// .cs
    /// 1. 假设Aasi是Model中AbstractApplicationStatusInfo的单态实例;
    /// 2. 开始任务的时候从UI主线程如有需要则Invoke(不使用BeginInvoke是因为要阻塞等待返回)执行AddBusyTaskContent(), 并保存返回的id引用;
    /// 3. 同步或者异步执行相对耗时的任务;
    /// 4. 任务结束后从UI主线程如有需要则Invoke执行RemoveBusyTaskItem, 这里也可以使用BeginInvoke;
    /// .xaml
    /// 1. 状态条MainStatusBar的DataContext设定为状态条ViewModel属性;
    /// 2. 除了Dock在右方的其他扩展项, 第一个Fill的项绑定ViewModel的ApplicationStatusInfo属性; 
    ///     方法可能是用过样式, 如果BusyTaskItems.Count为0或者通过IsReady为true来判断IsReady, 显示资源中的key为Application_Status_Ready的内容;
    ///     否则设置为绑定BusyTaskItems[0].Content.
    /// </example>
    public abstract class AbstractApplicationStatusInfo:ModelBase
    {
        #region Members
        readonly object _syncRoot = new object();
        //  正在忙碌的应用任务项集合(*注意需要在主界面UI线程上修改该集合,以免WPF绑定报错, 或者改用其他支持异步自动转换到UI线程绑定操作的集合类型)
        private ObservableCollection<ApplicationTaskItem> busyTaskItems;
        #endregion

        #region Properties

        /// <summary>
        /// 忙碌任务项集合
        /// </summary>
        public ObservableCollection<ApplicationTaskItem> BusyTaskItems
        {
            get { return busyTaskItems; }
            private set{
                if(busyTaskItems!=value)
                {
                    busyTaskItems = value;
                    busyTaskItems.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(busyTaskItems_CollectionChanged);
                    OnPropertyChanged("BusyTaskItems", "IsReady");
                }
            }
        }

        void busyTaskItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("IsReady", "CurrentTaskContent");
        }

        /// <summary>
        /// 是否就绪
        /// </summary>
        public bool IsReady
        {
            get { return BusyTaskItems.Count == 0; }
        }

        public object CurrentTaskContent
        {
            get
            {
                lock (_syncRoot)
                    return IsReady ? UniGuy.Windows.Properties.Resources.ResourceManager.GetString("Application_Status_Ready") : BusyTaskItems[0].Content;
            }
        }

        #endregion

        #region Constructors

        protected AbstractApplicationStatusInfo()
        {
            BusyTaskItems = new ObservableCollection<ApplicationTaskItem>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 新增一个忙碌任务内容, 返回的Id用于后续清除忙碌任务
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Guid AddBusyTaskContent(object content)
        {
            ApplicationTaskItem ati = ApplicationTaskItem.Create(content);
            lock (_syncRoot)
                BusyTaskItems.Add(ati);
            return ati.Id;
        }
        /// <summary>
        /// 同上，如果预先知道id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        public void AddBusyTaskItem(Guid id, object content)
        {
            lock (_syncRoot)
                BusyTaskItems.Add(new ApplicationTaskItem(id, content));
        }

        /// <summary>
        /// 清除某个id的忙碌任务内容
        /// Note: 新增和删除同一个Id的忙碌任务必须确保是在外部的同一个线程中先后执行的; 外部调用方控制, 否则可能抛出异常
        /// </summary>
        /// <param name="id"></param>
        public void RemoveBusyTaskItem(Guid id)
        {
            ApplicationTaskItem ati = BusyTaskItems.Single(i => i.Id == id);
            lock (_syncRoot)
                BusyTaskItems.Remove(ati);
        }

        #endregion
    }

    public class ApplicationTaskItem : INotifyPropertyChanged
    {
        #region Interface implementations
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            Debug.Assert(!string.IsNullOrEmpty(propertyName));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Members
        //  应用程序任务唯一的id, 做标记, 以便应用程序任务状态改变时从列表中查询和通知该结构
        private Guid id;
        //  WPF喜欢着用, 那就用object吧; 一般还是string为主.
        private object content;
        #endregion

        #region Properties
        public Guid Id
        {
            get { return id; }
            private set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public object Content
        {
            get { return content; }
            private set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
        #endregion

        #region Constructors
        public ApplicationTaskItem(Guid id, object content)
        {
            this.Id = id;
            this.Content = content;
        }
        public static ApplicationTaskItem Create(object content)
        {
            return new ApplicationTaskItem(Guid.NewGuid(), content);
        }
        #endregion
    }
}
