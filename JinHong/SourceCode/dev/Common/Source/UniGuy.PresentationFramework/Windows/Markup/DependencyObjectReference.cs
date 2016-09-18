using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows;
using System.Reflection;

namespace UniGuy.Controls.Markup
{
    /// <summary>
    /// 依赖对象引用
    /// </summary>
    public class DependencyObjectReference:MarkupExtension
    {
        #region Declaration Attached Property

        /// <summary>
        /// 设定依赖对象键
        /// </summary>
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.RegisterAttached("Key", typeof(object), typeof(DependencyObjectReference),
                new FrameworkPropertyMetadata(KeyPropertyChangedCallback));

        private static void KeyPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            DependencyObjects[(string)args.NewValue] = d;
        }

        public static object GetKey(DependencyObject d)
        {
            return (object)d.GetValue(KeyProperty);
        }

        public static void SetKey(DependencyObject d, object value)
        {
            d.SetValue(KeyProperty, value);
        }

        #endregion

        #region Fields
        /// <summary>
        /// 键
        /// </summary>
        private string key;
        /// <summary>
        /// 设定键的依赖对象字典
        /// </summary>
        private static Dictionary<string, DependencyObject> dependencyObjects;
        #endregion

        #region Properties
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private static Dictionary<string, DependencyObject> DependencyObjects
        {
            get { return dependencyObjects ?? (dependencyObjects = new Dictionary<string, DependencyObject>()); }
        }
        #endregion

        #region Ctor
        public DependencyObjectReference() { }

        public DependencyObjectReference(string key)
        {
            this.key = key;
        }
        #endregion

        #region Methods
        #region Overrides
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // ensure that a key was specified
            if (key == null)
            {
                throw new InvalidOperationException("The Key has not been specified for the Reference.");
            }

            return DependencyObjects.ContainsKey(key) ? DependencyObjects[key] : null;
        }
        #endregion
        #endregion
    }
}
