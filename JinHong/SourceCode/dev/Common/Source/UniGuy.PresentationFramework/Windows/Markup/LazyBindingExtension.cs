/*  See Data.Docs.OnBinding.txt;
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

using System.Threading;
using System.Threading.Tasks;

namespace UniGuy.Windows.Markup
{
    /// <summary>
    /// LazyBinding
    /// </summary>
    /// <remarks>
    /// LazyMultiBindingExtension类似,TODO
    /// -----
    /// 本类仅仅探测IsVisibleChanged事件，在UI显示元素时动态新增绑定。在该类的真正实现中，以何种方式延迟功能则是需要根据需求决定；后来已经修改,新增了Delayed;
    /// 本类只是一次性延迟给定绑定
    /// </remarks>
    [MarkupExtensionReturnType(typeof(object))]
    public class LazyBindingExtension:MarkupExtension
    {
        #region Fields
        private FrameworkElement mTarget = null;
        private DependencyProperty mProperty = null;

        private LazyBindingMode mode = LazyBindingMode.Delayed;
        private TimeSpan delayDuration = TimeSpan.FromMilliseconds(1000);
        #endregion

        #region Properties
        public object Source { get; set; }
        public RelativeSource RelativeSource { get; set; }
        public string ElementName { get; set; }
        public PropertyPath Path { get; set; }
        public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }

        /// <summary>
        /// 只能在Xaml中设置，否则不生效，这里不做动态更新
        /// </summary>
        public LazyBindingMode Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        public TimeSpan DelayDuration
        {
            get { return delayDuration; }
            set { delayDuration = value; }
        }
        #endregion

        #region Constructors
        public LazyBindingExtension() { }
        public LazyBindingExtension(string path)
        {
            Path = new PropertyPath(path);
        }
        #endregion

        #region Interface implementations
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (service == null)
                return null;
            mTarget = service.TargetObject as FrameworkElement;
            mProperty = service.TargetProperty as DependencyProperty;
            if (mTarget != null && mProperty != null)
            {
                switch (Mode)
                {
                    case LazyBindingMode.OnVisible:
                        mTarget.IsVisibleChanged += OnIsVisibleChanged;
                        break;
                    case LazyBindingMode.Delayed:
                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(DelayDuration);
                            Binding binding = CreateBinding();
                            return binding.ProvideValue(serviceProvider);
                        });
                        break;
                }
                return null;
            }
            else
            {
                Binding binding = CreateBinding();
                return binding.ProvideValue(serviceProvider);
            }
        }
        #endregion

        #region Methods
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            Binding binding = CreateBinding();
            BindingOperations.SetBinding(mTarget, mProperty, binding); 
        }

        private Binding CreateBinding()
        {
            Binding binding = new Binding(Path.Path);
            if (Source != null)
                binding.Source = Source;
            if (RelativeSource != null)
                binding.RelativeSource = RelativeSource;
            if (ElementName != null)
                binding.ElementName = ElementName;
            binding.Converter = Converter;
            binding.ConverterParameter = ConverterParameter;
            return binding;
        }
        #endregion

        #region Internal
        public enum LazyBindingMode
        {
            OnVisible,
            Delayed,
        }
        #endregion
    }
}
