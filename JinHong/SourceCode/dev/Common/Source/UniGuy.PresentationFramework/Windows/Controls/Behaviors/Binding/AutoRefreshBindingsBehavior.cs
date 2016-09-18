using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Data;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 1. 在使用绑定的时候有时候会有这样一种情况:一个CLR属性P1绑定到一个依赖属性DP1上, 但是P1没有实现INotifyPropertyChanged接口, 或者由于某种原因没有通知, 但是知道其值随时都可能变动;
    /// 那么DP1如果及时更新绑定;
    /// 2. 还有一种情况, 假设一个CLR属性(可能有INotifyPropertyChanged)或者依赖属性P1绑定到依赖属性DP1上, 并且可能使用了转换器, 属性的值可能是复杂类型, 其内部的属性的属性的改变等不会引起属性本身发生改变;
    /// 对于1: 使用RefreshMode.UpdateTarget; 对于2: 使用RefreshMode.UpdateSource或者UpdateTarget(根据具体情况);
    /// </summary>
    public static class AutoRefreshBindingsBehavior
    {
        #region Fields
        private static Dictionary<BindingRefreshInfo, DispatcherTimer> dict;
        #endregion

        #region DependencyProperties
        /// <summary>
        /// 绑定刷新信息枚举
        /// </summary>
        public static readonly DependencyProperty BindingRefreshInfosProperty
            = DependencyProperty.RegisterAttached("BindingRefreshInfos", typeof(IEnumerable<BindingRefreshInfo>), typeof(AutoRefreshBindingsBehavior),
            new PropertyMetadata(null, OnBindingRefreshInfosPropertyChanged));

        #endregion

        #region Properties
        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static IEnumerable<BindingRefreshInfo> GetBindingRefreshInfos(DependencyObject d)
        {
            return (IEnumerable<BindingRefreshInfo>)d.GetValue(BindingRefreshInfosProperty);
        }
        public static void SetBindingRefreshInfos(DependencyObject d, IEnumerable<BindingRefreshInfo> value)
        {
            d.SetValue(BindingRefreshInfosProperty, value);
        }
        #endregion

        #region Methods

        #region Callbacks
        private static void OnBindingRefreshInfosPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            IEnumerable<BindingRefreshInfo> bris = (IEnumerable<BindingRefreshInfo>)args.OldValue;

            if (bris != null)
            {
                foreach (BindingRefreshInfo bri in bris)
                {
                    if (dict != null && dict.ContainsKey(bri))
                    {
                        dict[bri].Stop();
                        dict.Remove(bri);
                        if (dict.Count == 0)
                            dict = null;
                    }
                }
            }


            bris = (IEnumerable<BindingRefreshInfo>)args.NewValue;

            if (bris != null)
            {
                foreach (BindingRefreshInfo bri in bris)
                {
                    Debug.Assert(bri.Duration!=0);
                    DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromMilliseconds(bri.Duration), DispatcherPriority.DataBind, new EventHandler(
                        delegate(object obj, EventArgs e)
                        {
                            //  这里如果只对Binding操作, 则遇到MultiBinding不起作用;
                            BindingExpressionBase be = ((BindingExpressionBase)BindingOperations.GetBindingExpression(d, bri.Dp))??BindingOperations.GetMultiBindingExpression(d, bri.Dp);
                            if (be != null)
                            {
                                switch (bri.Mode)
                                {
                                    case RefreshMode.UpdateTarget:
                                        be.UpdateTarget();
                                        break;
                                    case RefreshMode.UpdateSource:
                                        be.UpdateSource();
                                        break;
                                }
                            }
                        }), d.Dispatcher);
                    timer.Start(); //   timer will not be GC collected even if no reference to it is explicit set; as reference to it is implicit maintained, or the tick event has no means can be invoked.

                    (dict ?? (dict = new Dictionary<BindingRefreshInfo, DispatcherTimer>())).Add(bri, timer);
                }
            }
        }

        #endregion

        #endregion

        #region Internal types
        /// <summary>
        /// 刷新绑定的模式
        /// </summary>
        public enum RefreshMode
        {
            UpdateTarget,
            UpdateSource
        }
        #endregion
    }

    public class BindingRefreshInfo
    {
        public DependencyProperty Dp { get; set; }
        public int Duration { get; set; }
        public AutoRefreshBindingsBehavior.RefreshMode Mode { get; set; }

        public BindingRefreshInfo() { }
    }
}
