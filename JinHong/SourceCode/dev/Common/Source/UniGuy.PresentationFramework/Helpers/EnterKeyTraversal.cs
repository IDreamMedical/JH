using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace UniGuy.Controls.Helpers
{
    /// <summary>
    /// 回车键转换成Tab键, 如果为Shift+Enter则为反向(由于是Preview, 可以用在父元素上)
    /// </summary>
    public class EnterKeyTraversal
    {
        #region Dependency properties

        /// <summary>
        /// 是否启用
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(EnterKeyTraversal), new UIPropertyMetadata(false, IsEnabledChanged));

        /// <summary>
        /// 必须的修饰键(除Shift键外, 因为Shift键已经限定用来控制方向, 默认为ModifierKeys.None)
        /// </summary>
        public static readonly DependencyProperty ModifierKeysProperty =
            DependencyProperty.RegisterAttached("ModifierKeys", typeof(ModifierKeys), typeof(EnterKeyTraversal), new PropertyMetadata(ModifierKeys.None));

        #endregion Dependency properties

        #region Properties
        [AttachedPropertyBrowsableForType(typeof(FrameworkElement)), AttachedPropertyBrowsableForType(typeof(FrameworkContentElement))]
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(FrameworkElement)), AttachedPropertyBrowsableForType(typeof(FrameworkContentElement))]
        public static ModifierKeys GetModifierKeys(DependencyObject obj)
        {
            return (ModifierKeys)obj.GetValue(ModifierKeysProperty);
        }

        public static void SetModifierKeys(DependencyObject obj, ModifierKeys value)
        {
            obj.SetValue(ModifierKeysProperty, value);
        }
        #endregion //   Properties

        #region Methods

        #region Private

        /// <summary>
        /// 从指定对象上移动焦点
        /// </summary>
        /// <param name="target">指定对象</param>
        /// <param name="tr">焦点移动的请求</param>
        /// <returns>是否成功移动焦点</returns>
        private static bool MoveFocus(object target, TraversalRequest request)
        {
            if (target is FrameworkElement)
                return ((FrameworkElement)target).MoveFocus(request);
            if (target is FrameworkContentElement)
                return ((FrameworkContentElement)target).MoveFocus(request);

            return false;
        }

        #endregion //   Private

        #region Callbacks

        static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = d as FrameworkElement;
            if (fe != null)
            {
                if ((bool)e.NewValue)
                    fe.PreviewKeyDown += Target_PreviewKeyDown;
                else
                    fe.PreviewKeyDown -= Target_PreviewKeyDown;

                return;
            }

            FrameworkContentElement fce = d as FrameworkContentElement;
            if (fce != null)
            {
                if ((bool)e.NewValue)
                    fce.PreviewKeyDown += Target_PreviewKeyDown;
                else
                    fce.PreviewKeyDown -= Target_PreviewKeyDown;
            }
        }

        #endregion //   Callbacks

        #region Event handlers

        static void Target_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ModifierKeys mk = GetModifierKeys((DependencyObject)sender);

            if ((mk&Keyboard.Modifiers)==mk && e.Key == Key.Enter)
            {
                MoveFocus(e.OriginalSource, new TraversalRequest(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift) ? FocusNavigationDirection.Previous : FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }

        #endregion //   Event handlers

        #endregion //   Methods
    }
}
