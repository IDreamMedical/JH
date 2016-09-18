using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace UniGuy.Controls.Markup
{
    /// <summary>
    /// 用于Markup，将若干字符串拼接，因为有时需要将Markup变量字符串和常量字符串方便的拼接，比如"{Binding XX}"+"abc";
    /// </summary>
    [MarkupExtensionReturnType(typeof(string))]
    public class StringJoinExtension: MarkupExtension
    {
        public string Separator
        {
            get;
            set;
        }

        public IEnumerable<string> Strings
        {
            get;
            set;
        }

        public StringJoinExtension() { }

        public StringJoinExtension(params string[] strs)
        {
            this.Strings = strs;
        }

        public StringJoinExtension(IEnumerable<string> strs)
        {
            this.Strings = strs;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Strings == null)
                throw new ArgumentException("The strings is not set");
            return string.Join(Separator, Strings);
        } 
    }
}
