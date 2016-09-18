using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 当按下Space时，在不同的Items中切换，针对控件必须是TextBoxBase
    /// </summary>
    public static class ToggleBetweenValues
    {
        #region ItemsProperty
        /// <summary>
        /// 用于切换的Items
        /// </summary>
        public static readonly DependencyProperty ItemsProperty
            = DependencyProperty.RegisterAttached("Items", typeof(IEnumerable), typeof(ToggleBetweenValues), new PropertyMetadata(null, OnItemsPropertyChanged){ CoerceValueCallback = OnItemsCoerceValue});

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static IEnumerable GetItems(DependencyObject d)
        {
            return (IEnumerable)d.GetValue(ItemsProperty);
        }

        public static void SetItems(DependencyObject d, IEnumerable value)
        {
            d.SetValue(ItemsProperty, value);
        }

        /// <summary>
        /// 如果设定Items，将直接启动
        /// </summary>
        /// <param name="d"></param>
        /// <param name="args"></param>
        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            d.SetValue(IsEnabledProperty, args.NewValue != null);
        }

        private static object OnItemsCoerceValue(DependencyObject d, object value)
        {
            if (value == null)
                return null;
            IEnumerable ie = (IEnumerable)value;
            int count = 0;
            foreach (object obj in ie)
                count++;
            object[] objs = new object[count];
            int i = 0;
            foreach (object obj in ie)
                objs[i++] = obj;
            return objs;
        }
        #endregion

        #region ValuePathProperty
        /// <summary>
        /// 值路径，选择的值是下一个Item通过ValuePath获得的值
        /// </summary>
        public static readonly DependencyProperty ValuePathProperty
            = DependencyProperty.RegisterAttached("ValuePath", typeof(string), typeof(ToggleBetweenValues));

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static string GetValuePath(DependencyObject d)
        {
            return (string)d.GetValue(ValuePathProperty);
        }

        public static void SetValuePath(DependencyObject d, string value)
        {
            d.SetValue(ValuePathProperty, value);
        }
        #endregion

        #region IsEnabledProperty
        /// <summary>
        /// 是否启动；如果设定Items，将直接启动
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty
            = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ToggleBetweenValues), new PropertyMetadata(OnIsEnabledPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
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
            TextBoxBase tbb = d as TextBoxBase;
            if (tbb != null)
            {
                if ((bool)args.NewValue)
                {
                    //  Some IME (Input Method Editor) will treat space and backspace as the part of the text composition, the TextInput will only be triggered when the text composition is done and a character has been produced, you can workaround this by using KeyDown event as you already did
                    tbb.PreviewKeyDown += OnPreviewKeyDown;
                }
                else
                {
                    tbb.PreviewKeyDown -= OnPreviewKeyDown;
                }
            }
        }

        #endregion

        #region CurrentIndexProperty
        public static readonly DependencyProperty CurrentIndexProperty
            = DependencyProperty.RegisterAttached("CurrentIndex", typeof(int), typeof(ToggleBetweenValues));
        #endregion

        #region Event Handlers
        private static void OnPreviewKeyDown(object sender, KeyEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;

            if (args.Key == Key.Space)
            {
                args.Handled = true;

                int currentIndex = (int)tbb.GetValue(CurrentIndexProperty);
                IList<object> items = (IList<object>)GetItems(tbb);
                currentIndex = ++currentIndex % (items).Count;
                tbb.SetValue(TextBox.TextProperty, DataBinder.Eval(items[currentIndex], GetValuePath(tbb)).ToString());
                tbb.SetValue(CurrentIndexProperty, currentIndex);
            }
        }
        #endregion

        /*
        <x:Array x:Key="arr" Type="{x:Type s:Object}">
            <s:String>aaa</s:String>
            <s:String>bbb</s:String>
            <s:String>ccc</s:String>
        </x:Array>
        <x:Array x:Key="arr1" Type="s:Object">
            <TextBlock Text="aaa"/>
            <TextBlock Text="bbb"/>
            <TextBlock Text="ccc"/>
        </x:Array>
         * 对于IsReadOnly为True的TextBox可用,如果为x:Array，需要用绑定并设定路径为Items,或者把x:Array放在样式中，Style知道怎样翻译x:Array
        <TextBox DockPanel.Dock="Top" pc:ToggleBetweenValues.Items="{Binding Items, Source={StaticResource arr}}" Text="a"/>
        <TextBox DockPanel.Dock="Top" pc:ToggleBetweenValues.Items="{Binding Items,Source={StaticResource arr}}" IsReadOnly="True" Text="b"/>
        <TextBox DockPanel.Dock="Top" pc:ToggleBetweenValues.Items="{Binding Items,Source={StaticResource arr1}}" pc:ToggleBetweenValues.ValuePath="Text" IsReadOnly="True" Text="c"/>
         */
    }
}
