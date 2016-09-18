using System;
using System.Collections;
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
    /// ComboBox自动记录输入值的行为,输入的Text在控件失去焦点后会记录到列表中,如果列表中已经有该项则会移到第一项;
    /// 设置了本行为默认ComboBox的IsEditable属性为True, 而且也不应该设置该属性为False, 否则本行为没有意义;
    /// 另外, 使用本行为的ComboBox其ItemsSource应该为字符串集合而非其他类型.
    /// </summary>
    public static class ComboBoxAutoTextToItemsBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty
            = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ComboBoxAutoTextToItemsBehavior),
            new PropertyMetadata(false, OnIsEnabledPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
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
            ComboBox cb = d as ComboBox;
            if (cb != null)
            {
                if ((bool)args.NewValue)
                {
                    cb.IsEditable = true;
                    cb.PreviewLostKeyboardFocus += OnComboBoxPreviewLostKeyboardFocus;
                }
                else
                {
                    cb.PreviewLostKeyboardFocus -= OnComboBoxPreviewLostKeyboardFocus;
                }
            }
        }

        private static void OnComboBoxPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            ComboBox cb = (ComboBox)sender;
            Action action = new Action(delegate()
            {
                if (!string.IsNullOrEmpty(cb.Text))
                {
                    ArrayList items = new ArrayList(cb.Items);
                    int index = items.IndexOf(cb.Text);
                    if (index > 0)
                    {
                        items.RemoveAt(index);
                        items.Insert(0, cb.Text);
                    }
                    else if (index == -1)
                    {
                        items.Add(cb.Text);
                    }
                    cb.ItemsSource = items;
                }
            });
            cb.Dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle);
        }
    }
}
