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
    /// <remarks>
    /// 如果一个DependencyObject上有多个依赖属性要做处理, 应使用AutoRefreshBindingsBehavior
    /// </remarks>
    public static class AutoRefreshBindingBehavior
    {
        #region Fields
        private static Dictionary<DependencyProperty, DispatcherTimer> dict;
        #endregion

        #region DependencyProperties

        /// <summary>
        /// 要绑定的依赖属性
        /// </summary>
        public static readonly DependencyProperty DpProperty
            = DependencyProperty.RegisterAttached("Dp", typeof(DependencyProperty), typeof(AutoRefreshBindingBehavior),
            new PropertyMetadata(null, OnDpPropertyChanged));
        /// <summary>
        /// 绑定进行自动更新的周期(毫秒数)
        /// </summary>
        public static readonly DependencyProperty DurationProperty
            = DependencyProperty.RegisterAttached("Duration", typeof(int), typeof(AutoRefreshBindingBehavior),
            new PropertyMetadata(null, OnDurationPropertyChanged));
        /// <summary>
        /// 绑定模式
        /// </summary>
        public static readonly DependencyProperty ModeProperty
            = DependencyProperty.RegisterAttached("Mode", typeof(RefreshMode), typeof(AutoRefreshBindingBehavior));
        #endregion

        #region Properties
        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static DependencyProperty GetDp(DependencyObject d)
        {
            return (DependencyProperty)d.GetValue(DpProperty);
        }
        public static void SetDp(DependencyObject d, DependencyProperty value)
        {
            d.SetValue(DpProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static int GetDuration(DependencyObject d)
        {
            return (int)d.GetValue(DurationProperty);
        }
        public static void SetDuration(DependencyObject d, int value)
        {
            d.SetValue(DurationProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static RefreshMode GetMode(DependencyObject d)
        {
            return (RefreshMode)d.GetValue(ModeProperty);
        }
        public static void SetMode(DependencyObject d, RefreshMode value)
        {
            d.SetValue(ModeProperty, value);
        }
        #endregion

        #region Methods

        private static void SetTimer(DependencyObject d, DependencyProperty dp, int duration, RefreshMode mode)
        {
            Debug.Assert(dp != null && duration > 0);

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromMilliseconds(duration), DispatcherPriority.DataBind, new EventHandler(
                        delegate(object obj, EventArgs e)
                        {
                            //  这里如果只对Binding操作, 则遇到MultiBinding不起作用;
                            BindingExpressionBase be = ((BindingExpressionBase)BindingOperations.GetBindingExpression(d, dp)) ?? BindingOperations.GetMultiBindingExpression(d, dp);
                            if (be != null)
                            {
                                switch (mode)
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

            (dict ?? (dict = new Dictionary<DependencyProperty, DispatcherTimer>())).Add(dp, timer);
        }

        #region Callbacks
        private static void OnDpPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            DependencyProperty dp = (DependencyProperty)args.OldValue;

            if (dp != null)
            {
                if (dict != null && dict.ContainsKey(dp))
                {
                    dict[dp].Stop();
                    dict.Remove(dp);
                    if (dict.Count == 0)
                        dict = null;
                }
            }

            dp = (DependencyProperty)args.NewValue;
            if (dp != null)
            {
                int duration = GetDuration(d);
                if (duration > 0)
                    SetTimer(d, dp, duration, GetMode(d));
            }
        }
        private static void OnDurationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            int oldDuration = (int)args.OldValue;
            int newDuration = (int)args.NewValue;

            if (oldDuration == 0)
            {
                DependencyProperty dp = GetDp(d);
                if (dp != null)
                    SetTimer(d, dp, newDuration, GetMode(d));
            }
            else
            {
                DependencyProperty dp = GetDp(d);
                if (dict != null && dict.ContainsKey(dp))
                {
                    dict[dp].Stop();
                    dict.Remove(dp);
                    if (dict.Count == 0)
                        dict = null;
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
}
