using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections;
using UniGuy.Core.Extensions;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 这个类用于定义TextBox的行为:
    /// 1. 可以给定ItemsSource, 用于自动完成;
    /// 2. 如果文本框中输入了值, 输入指定分隔符后, ItemsSource自动转到分类的子ItemsSource;
    /// 3.有一个ActualText属性用于保存带有分隔符的路径文本, 文本框获得焦点时用ActualText替换Text, 失去焦点的时候则设置ActualText为Text, 同时去掉Text中的分隔符;
    /// </summary>
    [Obsolete]//TODO 还不能用, 有很多问题
    public static class PathCompleteService
    {
        /// <summary>
        /// 实际字符串
        /// </summary>
        public static readonly DependencyProperty ActualTextProperty
            = DependencyProperty.RegisterAttached("ActualText", typeof(string), typeof(PathCompleteService), new PropertyMetadata(null, new PropertyChangedCallback(OnActualTextPropertyChanged)));

        /// <summary>
        /// 自动完成UI
        /// </summary>
        public static readonly DependencyProperty PopupListBoxProperty
            = DependencyProperty.RegisterAttached("PopupListBox", typeof(ListBoxForPopup), typeof(PathCompleteService), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPopupListBoxChanged)));

        /// <summary>
        /// 跟项列表
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty
            = DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable), typeof(PathCompleteService), new PropertyMetadata(null, new PropertyChangedCallback(OnItemsSourcePropertyChanged)));

        /// <summary>
        /// 值路径(只能录入Key)
        /// </summary>
        public static readonly DependencyProperty KeyPathProperty = DependencyProperty.RegisterAttached("KeyPath", typeof(string), typeof(PathCompleteService));

        /// <summary>
        /// 搜索路径(Hierarchical路径, 比如常见"Items")
        /// </summary>
        public static readonly DependencyProperty SearchPathProperty = DependencyProperty.RegisterAttached("SearchPath", typeof(string), typeof(PathCompleteService));

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static string GetActualText(DependencyObject d)
        {
            return (string)d.GetValue(ActualTextProperty);
        }
        public static void SetActualText(DependencyObject d, string value)
        {
            d.SetValue(ActualTextProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static ListBoxForPopup GetPopupListBox(DependencyObject d)
        {
            return (ListBoxForPopup)d.GetValue(PopupListBoxProperty);
        }
        public static void SetPopupListBox(DependencyObject d, ListBoxForPopup value)
        {
            d.SetValue(PopupListBoxProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static IEnumerable GetItemsSource(DependencyObject d)
        {
            return (IEnumerable)d.GetValue(ItemsSourceProperty);
        }
        public static void SetItemsSource(DependencyObject d, IEnumerable value)
        {
            d.SetValue(ItemsSourceProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static string GetKeyPath(DependencyObject d)
        {
            return (string)d.GetValue(KeyPathProperty);
        }
        public static void SetKeyPath(DependencyObject d, string value)
        {
            d.SetValue(KeyPathProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static IEnumerable GetSearchPath(DependencyObject d)
        {
            return (string)d.GetValue(SearchPathProperty);
        }
        public static void SetSearchPath(DependencyObject d, string value)
        {
            d.SetValue(SearchPathProperty, value);
        }

        private static void OnPopupListBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxBase tbb = d as TextBoxBase;
            if (tbb != null)
            {
                if (e.NewValue != null)
                {
                    tbb.PreviewKeyDown += OnPreviewKeyDown;
                    tbb.PreviewKeyUp += OnPreviewKeyUp;
                    tbb.GotFocus += OnGotFocus;
                    tbb.LostFocus += OnLostFocus;
                }
                else
                {
                    tbb.PreviewKeyDown -= OnPreviewKeyDown;
                    tbb.PreviewKeyUp -= OnPreviewKeyUp;
                    tbb.GotFocus -= OnGotFocus;
                    tbb.LostFocus -= OnLostFocus;
                }
            }

            ListBoxForPopup lbfpOld = e.OldValue as ListBoxForPopup;
            if (lbfpOld != null)
                lbfpOld.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(OnPopupListBoxIsVisiblePropertyChanged);

            ListBoxForPopup lbfpNew = e.NewValue as ListBoxForPopup;
            if (lbfpNew != null)
                lbfpNew.IsVisibleChanged += new DependencyPropertyChangedEventHandler(OnPopupListBoxIsVisiblePropertyChanged);

            IEnumerable items = (IEnumerable)GetItemsSource(tbb);
            if (items != null)
                foreach (object item in items)
                    lbfpNew.Items.Add(item);
        }

        private static void OnPopupListBoxIsVisiblePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ListBoxForPopup lbfp = (ListBoxForPopup)sender;
            TextBoxBase tbb = lbfp.PlacementTarget as TextBoxBase;

            if (tbb != null && lbfp.SelectedValue != null)
            {
                string keyPath = (string)GetKeyPath(tbb);

                string newString = (string)DataBinder.Eval(lbfp.SelectedValue, keyPath);
                string oldString = (string)GetActualText(tbb);
                int index = oldString!=null?oldString.LastIndexOf('.'):-1;
                if (index >= 0)
                    newString = oldString.Substring(0, index) + "." +  newString;
                SetActualText(tbb, newString);
                if (tbb is TextBox)
                {
                    TextBox tb = (TextBox)tbb;
                    tb.SelectionStart = tb.Text.Length;
                    tb.SelectionLength = 0;
                }
                tbb.Focus();
            }
        }

        private static void OnActualTextPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;
            string text = GetActualText(tbb);
            if (!tbb.IsFocused)
                text = text.Replace(".", string.Empty);
            tbb.SetValue(TextBox.TextProperty, text);
        }

        private static void OnItemsSourcePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;
            ListBoxForPopup lbfp = (ListBoxForPopup)GetPopupListBox(tbb);
            if (lbfp != null)
            {
                foreach (object item in (IEnumerable)args.NewValue)
                    lbfp.Items.Add(item);
            }
        }

        private static void OnGotFocus(object sender, RoutedEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;
            tbb.SetValue(TextBox.TextProperty, GetActualText(tbb));
            ListBoxForPopup lbfp = (ListBoxForPopup)GetPopupListBox(tbb);
            if (lbfp != null)
            {
                lbfp.PlacementTarget = tbb;
                lbfp.IsOpen = true;
            }
        }

        private static void OnLostFocus(object sender, RoutedEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;
            string text = (string)GetActualText(tbb);
            SetActualText(tbb, text);
            if (text != null)
                text = text.Replace(".", string.Empty);
            tbb.SetValue(TextBox.TextProperty, text);
            GetPopupListBox(tbb).IsOpen = false;
        }

        private static void OnPreviewKeyDown(object sender, KeyEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;
            ListBoxForPopup lbfp = GetPopupListBox(tbb);
            if (lbfp.IsOpen)
            {
                string keyPath = GetKeyPath(tbb);

                switch (args.Key)
                {
                    case Key.Enter:
                        lbfp.IsOpen = false;
                        if (lbfp.SelectedItem != null)
                        {
                            string keySeg = (string)DataBinder.Eval(lbfp.SelectedValue, keyPath);
                            string text = (string)GetActualText(tbb);
                            if (text != null)
                            {
                                int index = text.LastIndexOf('.');
                                if (index >= 0)
                                    text = text.Substring(0, index) + keySeg;
                                else
                                    text = keySeg;
                            }
                            SetActualText(tbb, text);

                            //  设置光标位置
                            if (tbb is TextBox)
                            {
                                TextBox tb = (TextBox)tbb;
                                tb.SelectionStart = tb.Text.Length;
                                tb.SelectionLength = 0;
                            }
                        }
                        args.Handled = true;
                        break;
                    case Key.Up:
                        if (lbfp.SelectedIndex > 0)
                            lbfp.ScrollIntoView(lbfp.Items[--lbfp.SelectedIndex]);
                        args.Handled = true;
                        break;
                    case Key.Down:
                        if (lbfp.SelectedIndex < lbfp.Items.Count - 1)
                            lbfp.ScrollIntoView(lbfp.Items[++lbfp.SelectedIndex]); ;
                        args.Handled = true;
                        break;
                }

            }
        }

        private static void OnPreviewKeyUp(object sender, KeyEventArgs args)
        {
            if (new Key[] { Key.Enter, Key.Up, Key.Down }.Contains<Key>(args.Key))
                return;
            TextBoxBase tbb = (TextBoxBase)sender;
            string searchPath = (string)GetSearchPath(tbb);
            string keyPath = (string)GetKeyPath(tbb);
            ListBoxForPopup lbfp = GetPopupListBox(tbb);
            lbfp.PlacementTarget = tbb;

            lbfp.Items.Clear();
            string text = (string)tbb.GetValue(TextBox.TextProperty);
            if (text == null)
                lbfp.IsOpen = false;
            else
            {
                IEnumerable items = GetItemsSource(tbb);

                int pos = 0;
            opku0:
                int newPos = text.IndexOf('.', pos);
                if (newPos >= 0)
                {
                    string keySeg = text.Substring(pos, newPos - pos);
                    if (items != null)
                    {
                        foreach (object item in items)
                        {
                            string itemValue = (string)DataBinder.Eval(item, keyPath);
                            if (itemValue == keySeg)
                            {
                                items = DataBinder.Eval(item, searchPath) as IEnumerable;
                                pos = newPos + 1;
                                if (pos < text.Length)
                                    goto opku0;
                            }
                        }
                    }
                }

                string keyTail = text.Substring(pos);
                if(items!=null)
                {
                    foreach (object item in items)
                    {
                        string itemValue = (string)DataBinder.Eval(item, keyPath);
                        if (itemValue != null && itemValue.StartsWith(keyTail))
                        {
                            lbfp.Items.Add(item);
                        }
                    }
                    if ((lbfp.IsOpen = lbfp.Items.Count > 0))
                        if (lbfp.SelectedIndex == -1)
                            lbfp.ScrollIntoView(lbfp.Items[lbfp.SelectedIndex = 0]);
                }
            }
        }
    }
}
