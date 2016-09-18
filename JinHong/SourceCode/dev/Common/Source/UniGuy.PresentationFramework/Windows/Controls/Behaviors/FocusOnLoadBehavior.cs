using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// FrameworkElement在载入后自动获得焦点的行为
    /// </summary>
    /// <example>
    /// <TextBox Width="200"/>
	///	<TextBox Width="200" pc:FocusOnLoadBehavior.IsEnabled="True"/>
	///	<TextBox Width="200"/>
    /// </example>
    public static class FocusOnLoadBehavior
    {
        /// <summary>
        /// 1.  This is the boolean attached property with its getter and setter:
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty
            = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(FocusOnLoadBehavior),
            new PropertyMetadata(false, OnIsEnabledPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement))]
        public static bool GetIsEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(IsEnabledProperty);
        }
        public static void SetIsEnabled(DependencyObject d, bool value)
        {
            d.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement fe = d as TextBoxBase;
            if (fe != null)
            {
                if ((bool)args.NewValue)
                {
                    fe.Loaded += OnFrameworkElementLoaded;
                }
                else
                {
                    fe.Unloaded -= OnFrameworkElementLoaded;
                }
            }
        }

        private static void OnFrameworkElementLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = (FrameworkElement)sender;
            Action action = new Action(delegate() { fe.Focus(); });
            fe.Dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle);
        }
    }
}
