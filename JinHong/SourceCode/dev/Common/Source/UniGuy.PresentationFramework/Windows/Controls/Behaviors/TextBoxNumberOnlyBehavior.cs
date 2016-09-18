using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace UniGuy.Controls.Behaviors
{
    public class TextBoxNumberOnlyBehavior
    {
        #region IsNumberOnly Property

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsNumberOnly(DependencyObject d)
        {
            return (bool)d.GetValue(IsNumberOnlyProperty);
        }

        public static void SetIsNumberOnly(DependencyObject d, bool value)
        {
            d.SetValue(IsNumberOnlyProperty, value);
        }

        public static readonly DependencyProperty IsNumberOnlyProperty =
            DependencyProperty.RegisterAttached(
                "IsNumberOnly",
                typeof(bool),
                typeof(TextBoxNumberOnlyBehavior),
                new FrameworkPropertyMetadata(IsNumberOnlyPropertyChangedCallback)
                );

        private static void IsNumberOnlyPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = d as TextBox;

            if (d != null)
            {
                if ((bool)e.NewValue)
                    tb.PreviewTextInput += TextBox_PreviewTextInput;
                else
                    tb.PreviewTextInput -= TextBox_PreviewTextInput;
            }
        }

        #endregion

        #region Private Static Methods

        //  这里面有Bug,设置了Mask="Integer"之后,MaxLength不起作用了,不应该在PreviewTextInput里面设置Text然后e.Handled=true;
        private static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text))
                if (!char.IsDigit(e.Text[0]))
                    e.Handled = true;
        }
        #endregion
    }
}