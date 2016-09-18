using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

using UniGuy.Controls.Behaviors;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 这是一个用于ComboBox的行为, ComboBox有DisplayMemberPath和SelectedValuePath;
    /// 不过其SuggestAppend只能用于DisplayMemberPath, 对于中文我们希望可以用首字母缩写等快速录入;
    /// 一个方案是新增一个QuickSelectPath, 输入内容(比如拼音)如果和QuickSelectPath匹配,自动显示下拉框并选中该项
    /// </summary>
    [Obsolete("不能用, ComboBox不能在ListBox中选择一项而不自动设置TextBox")]
    public class ComboBoxItemQuickSelectBehavior
    {
        public static readonly DependencyProperty QuickSelectPathProperty
            = DependencyProperty.RegisterAttached("QuickSelectPath", typeof(string), typeof(ComboBoxItemQuickSelectBehavior),
            new PropertyMetadata(QuickSelectPathPropertyChangedCallback));

        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        public static string GetQuickSelectPath(DependencyObject d)
        {
            return (string)d.GetValue(QuickSelectPathProperty);
        }
        public static void SetQuickSelectPath(DependencyObject d, string value)
        {
            d.SetValue(QuickSelectPathProperty, value);
        }

        private static void QuickSelectPathPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ComboBox cb = d as ComboBox;
            if (cb != null)
            {
                string path = (string)args.NewValue;
                if (path != null)
                    cb.KeyUp += new KeyEventHandler(cb_KeyUp); //.TextInput += OnComboBoxTextInput;
                else
                    cb.KeyUp -= cb_KeyUp;// OnComboBoxTextInput;
            }
        }

        static void cb_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            string text = cb.Text;
            string path = GetQuickSelectPath(cb);

            foreach (object item in cb.Items)
            {
                string s = (string)DataBinder.Eval(item, path);
                if (s.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
                {
                    cb.IsDropDownOpen = true;
                    ((ComboBoxItem)cb.ItemContainerGenerator.ContainerFromItem(item)).IsSelected = true;
                }
            }

            cb.IsDropDownOpen = false;
        }

        private static void OnComboBoxTextInput(object sender, TextCompositionEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            string text = cb.Text;
            string path = GetQuickSelectPath(cb);

            foreach (object item in cb.Items)
            {
                string s = (string)DataBinder.Eval(item, path);
                if (s.StartsWith(path, StringComparison.InvariantCultureIgnoreCase))
                {
                    cb.IsDropDownOpen = true;
                    cb.SelectedItem = item;
                }
            }

            cb.IsDropDownOpen = false;
        }
    }
}
