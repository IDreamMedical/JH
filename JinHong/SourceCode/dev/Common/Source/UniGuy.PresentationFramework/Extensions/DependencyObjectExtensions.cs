using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UniGuy.Controls;
using UniGuy.Controls.Converters;
using System.Globalization;
using System.Windows.Data;

namespace UniGuy.Controls.Extensions
{
    /// <summary>
    /// 关于DependencyObject的扩展方法
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Sets the given dependency property, applying type conversion where required
        /// </summary>
        /// <remarks>
        /// 参见Documents: A Universal Value Converter for WPF - CodeProject.htm
        /// </remarks>
        public static void SetValueAutoUniversalConverted(this DependencyObject element, DependencyProperty property, object value)
        {
            var conv = new UniversalValueConverter();
            var convertedValue = conv.Convert(value, property.PropertyType, null, CultureInfo.InvariantCulture);
            element.SetValue(property, convertedValue);
        }

        /// <summary>
        /// 设置@this所在VisualTree中到根的路径中所有UIElment为获得焦点,只要可以设置焦点
        /// </summary>
        /// <param name="this"></param>
        public static void FocusVisualTreePath(this DependencyObject @this, DependencyObject root)
        {
            if (@this != null && @this!=root)
            {
                FocusVisualTreePath(VisualTreeHelper.GetParent(@this), root);

                if (@this is UIElement)
                    if (((UIElement)@this).Focusable)
                        ((UIElement)@this).Focus();
            }
        }

        /// <summary>
        /// 枚举所有指定依赖对象上的绑定
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerable<Binding> EnumerateBindings(this DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            LocalValueEnumerator lve = element.GetLocalValueEnumerator();

            while (lve.MoveNext())
            {
                LocalValueEntry entry = lve.Current;

                if (BindingOperations.IsDataBound(element, entry.Property))
                {
                    Binding binding = (entry.Value as BindingExpression).ParentBinding;
                    yield return binding;
                }
            }
        }

        /// <summary>
        /// 枚举所有指定依赖对象上的绑定
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static IEnumerable<BindingExpression> EnumerateBindingExpressions(this DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            LocalValueEnumerator lve = element.GetLocalValueEnumerator();

            while (lve.MoveNext())
            {
                LocalValueEntry entry = lve.Current;

                if (BindingOperations.IsDataBound(element, entry.Property))
                {
                    yield return entry.Value as BindingExpression;
                }
            }
        }
    }
}
