using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Collections;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// PropertyPaths are a very useful binding concept, and can be useful in other instances as well. However, .NET 3.5 does not support evaluating PropertyPaths against objects directly without using binding. Here's a workaround:
    /// Example:
    /// this.Content = DataBinder.Eval(foo, "Bar.Value");
    /// </summary>
    public static class DataBinder
    {
        /// <summary>
        /// Dummy
        /// </summary>
        private static readonly DependencyProperty DummyProperty =
        DependencyProperty.RegisterAttached(
        "Dummy",
        typeof(Object),
        typeof(DependencyObject),
        new UIPropertyMetadata(null));

        /// <summary>
        /// 通过依赖属性Dummy的帮助使用路径expression绑定到某个对象container,从而获得container下这个路径的属性值
        /// </summary>
        /// <param name="container"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Object Eval(Object container, String expression)
        {
            Binding binding = new Binding(expression) { Source = container };
            DependencyObject dummyDO = new DependencyObject();
            BindingOperations.SetBinding(dummyDO, DummyProperty, binding);
            return dummyDO.GetValue(DummyProperty);
        }

        /// <summary>
        /// 通过依赖属性Dummy的帮助使用路径expression绑定到某个对象container,从而设置container下这个路径对应的属性的值
        /// </summary>
        /// <param name="container"></param>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        public static void Feed(Object container, string expression, object value)
        {
            Binding binding = new Binding(expression) { Source = container, Mode = BindingMode.OneWayToSource };
            DependencyObject dummyDO = new DependencyObject();
            BindingOperations.SetBinding(dummyDO, DummyProperty, binding);
            dummyDO.SetValue(DummyProperty, value);
        }

        /// <summary>
        /// 计算一个容器列表的某个路径的绑定值
        /// </summary>
        /// <param name="containers"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IEnumerable EvalAll(IEnumerable containers, string expression)
        {
            Binding binding = new Binding(expression);
            DependencyObject dummyDO = new DependencyObject();
            BindingOperations.SetBinding(dummyDO, DummyProperty, binding);
            foreach (object container in containers)
            {
                binding.Source = container;
                yield return dummyDO.GetValue(DummyProperty);
            }
        }
    }
}
