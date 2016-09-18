using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace UniGuy.Controls.Markup
{
    /// <summary>
    /// 绑定的值的扩展方法
    /// </summary>
    public class BoundValueExtension : MarkupExtension
    {
        private object target;
        private string expression;

        public object Target
        {
            get { return target; }
            set { target = value; }
        }

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        public BoundValueExtension() { }
        public BoundValueExtension(string expression)
        {
            this.Expression = expression;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (target == null || string.IsNullOrEmpty(expression))
                throw new Exception("Binding failed.");

            return Behaviors.DataBinder.Eval(target, expression);
        }
    }
}
