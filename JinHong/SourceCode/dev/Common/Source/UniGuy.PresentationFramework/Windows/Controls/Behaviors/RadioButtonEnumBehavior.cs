using System;
using System.Collections;
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
    /// 用于绑定Radio组到某个属性
    /// </summary>
    public static class RadioButtonEnumBehavior
    {
        /// <summary>
        /// 要绑定的属性
        /// </summary>
        public static readonly DependencyProperty EnumBindingProperty
            = DependencyProperty.RegisterAttached("EnumBinding", typeof(object), typeof(RadioButtonEnumBehavior),
           new PropertyMetadata(OnEnumBindingPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        public static object GetEnumBinding(DependencyObject d)
        {
            return (object)d.GetValue(EnumBindingProperty);
        }
        public static void SetEnumBinding(DependencyObject d, object value)
        {
            d.SetValue(EnumBindingProperty, value);
        }

        /// <summary>
        /// 如果EnumBinding和EnumValue相等，则IsChecked为true
        /// </summary>
        public static readonly DependencyProperty EnumValueProperty
            = DependencyProperty.RegisterAttached("EnumValue", typeof(object), typeof(RadioButtonEnumBehavior));

        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        public static object GetEnumValue(DependencyObject d)
        {
            return d.GetValue(EnumValueProperty);
        }
        public static void SetEnumValue(DependencyObject d, string value)
        {
            d.SetValue(EnumValueProperty, value);
        }

        private static void OnEnumBindingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            RadioButton rb = d as RadioButton;
            if (rb != null)
            {
                if (args.NewValue!=null && args.OldValue==null)
                {
                    rb.Checked += OnChecked;
                }
                else if(args.NewValue==null && args.OldValue!=null)
                {
                    rb.Checked -= OnChecked;
                }
                SetChecked(rb);
            }
        }

        private static void OnChecked(object sender, RoutedEventArgs args)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.IsChecked ?? false)
                {
                    SetEnumBinding(rb, GetEnumValue(rb));
                }
            }
        }

        private static void SetChecked(RadioButton rb)
        {
            rb.IsChecked = Comparer.Default.Compare(GetEnumBinding(rb), GetEnumValue(rb))==0;
        }

        /*
        <Window x:Class="RadioButtonEnumBehaviorExample.Window1"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:pc="http://schemas.philan.com/controls"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            Title="Window1" Height="300" Width="300"
                DataContext="{Binding RelativeSource={RelativeSource Self}}">
            <StackPanel>
                <TextBox x:Name="l" Text="e1"/>
                <RadioButton pc:RadioButtonEnumBehavior.EnumBinding="{Binding ElementName=l, Path=Text, Mode=TwoWay}" pc:RadioButtonEnumBehavior.EnumValue="e1"
                   Content="1"/>
                <RadioButton pc:RadioButtonEnumBehavior.EnumBinding="{Binding ElementName=l, Path=Text, Mode=TwoWay}" pc:RadioButtonEnumBehavior.EnumValue="e2"
                   Content="2"/>
                <RadioButton pc:RadioButtonEnumBehavior.EnumBinding="{Binding ElementName=l, Path=Text, Mode=TwoWay}" pc:RadioButtonEnumBehavior.EnumValue="e3"
                   Content="3"/>
            </StackPanel>
        </Window>
        */
    }
}
