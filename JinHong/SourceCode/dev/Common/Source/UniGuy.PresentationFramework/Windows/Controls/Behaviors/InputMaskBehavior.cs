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
    public enum InputMask
    {
        None,
        Digit,
        Letter,
        Number,
        LetterOrDigit,
        CustomMask
    }
    /// <summary>
    /// 比如你想过滤一个TextBox的录入
    /// </summary>
    public static class InputMaskBehavior
    {
        public static readonly DependencyProperty MaskTypeProperty
            = DependencyProperty.RegisterAttached("MaskType", typeof(InputMask), typeof(InputMaskBehavior),
           new PropertyMetadata(InputMask.None));

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static InputMask GetMaskType(DependencyObject d)
        {
            return (InputMask)d.GetValue(MaskTypeProperty);
        }
        public static void SetMaskType(DependencyObject d, InputMask value)
        {
            d.SetValue(MaskTypeProperty, value);
        }

        public static readonly DependencyProperty MaskCharsProperty
            = DependencyProperty.RegisterAttached("MaskChars", typeof(string), typeof(InputMaskBehavior));

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static string GetMaskChars(DependencyObject d)
        {
            return (string)d.GetValue(MaskCharsProperty);
        }
        public static void SetMaskChars(DependencyObject d, string value)
        {
            d.SetValue(MaskCharsProperty, value);
        }

        /// <summary>
        /// 1.  This is the boolean attached property with its getter and setter:
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty
            = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(InputMaskBehavior),
            new PropertyMetadata(false, OnIsEnabledPropertyChanged));

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
                    tbb.PreviewTextInput+=OnPreviewTextInput;
                }
                else
                {
                    tbb.PreviewTextInput -= OnPreviewTextInput;
                }
            }
        }

        private static void OnPreviewTextInput(object sender, TextCompositionEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;
            InputMask im = GetMaskType(tbb);
            string mc = GetMaskChars(tbb);
            if (string.IsNullOrEmpty(args.Text))
                return;
            char c = args.Text[0];
            if (im == InputMask.Digit && !char.IsDigit(c))
                args.Handled=true;
            if (im == InputMask.Letter && !char.IsLetter(c))
                args.Handled = true;
            if (im == InputMask.LetterOrDigit && !char.IsLetter(c))
                args.Handled = true;
            if (im == InputMask.Number && !char.IsNumber(c))
                args.Handled = true;
            if (im == InputMask.CustomMask && mc != null && !mc.Contains(c))
                args.Handled = true;
        }
    }
}
