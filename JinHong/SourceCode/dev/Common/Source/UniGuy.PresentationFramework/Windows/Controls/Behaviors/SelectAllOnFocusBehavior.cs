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
    public static class SelectAllOnFocusBehavior
    {
        /// <summary>
        /// 1.  This is the boolean attached property with its getter and setter:
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty
            = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(SelectAllOnFocusBehavior),
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

        /// <summary>
        /// 2.  This is the change event of our attached propert value:
        ///     We get in the first parameter the dependency object to which the attached behavior was attached
        ///     We get in the second parameter the value of the attached behavior.
        ///     The implementation of the behavior is to check if we are attached to a TextBoxBase, and if so and the value
        ///     is true, hook to the PreviewKeyboardFocus of the TextBoxBase.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="args"></param>
        private static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            TextBoxBase tbb = d as TextBoxBase;
            if (tbb != null)
            {
                if ((bool)args.NewValue)
                {
                    tbb.PreviewGotKeyboardFocus += OnTextBoxBasePreviewGotKeyboardFocus;
                }
                else
                {
                    tbb.PreviewGotKeyboardFocus -= OnTextBoxBasePreviewGotKeyboardFocus;
                }
            }
        }

        /// <summary>
        /// 3.  The actual implementation: Whenever the textbox gets the keyboard focus, we select its text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnTextBoxBasePreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            TextBoxBase tbb = (TextBoxBase)sender;
            Action action = new Action(tbb.SelectAll);
            tbb.Dispatcher.BeginInvoke(action, DispatcherPriority.ContextIdle);
        }

        /*  <Page
         *      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
         *      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
         *      xmlns:behaviors="your behavior xmlns path">
         *      <Grid>
         *          <TextBox Text="Some Text" behaviors:SelectAllOnFocusBehaviour.SelectAllOnFocus="True"/>
         *      </Grid>
         *  </Page>
         */
    }
}
