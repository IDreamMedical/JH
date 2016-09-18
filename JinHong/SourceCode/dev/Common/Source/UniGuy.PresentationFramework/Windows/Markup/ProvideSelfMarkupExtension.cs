using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace UniGuy.Windows.Markup
{
    /// <summary>
    /// 一个提供自己的Xaml扩展, 许多转换器都可以继承此类
    /// </summary>
    public class ProvideSelfMarkupExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
