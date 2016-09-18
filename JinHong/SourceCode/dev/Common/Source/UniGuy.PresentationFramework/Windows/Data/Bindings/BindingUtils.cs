// Copyright (C) Michael Agroskin 2010
using System.Windows;
using System.Windows.Data;

namespace UniGuy.Controls.Bindings
{
    public static class Utils
    {
        private static void SafeSetValue(FrameworkElement element, DependencyProperty prop, object value)
        {
            var expression = (BindingExpressionBase)element.GetBindingExpression(prop);
            var binding = expression != null ? expression.ParentBindingBase : null;

            element.SetValue(prop, value);

            if (binding == null)
                return;

            if (expression.Status == BindingStatus.Detached)
            {
                expression = element.SetBinding(prop, binding);
                if (expression.Status != BindingStatus.Active)
                    return;

                expression.UpdateSource();
            }
        }

        private static void SetCurrrentValueAndUpdate(FrameworkElement element, DependencyProperty prop, object value)
        {
            element.SetValue(prop, value);
            var expression = (BindingExpressionBase)element.GetBindingExpression(prop);
            if (expression == null || expression.Status != BindingStatus.Active)
                return;
            expression.UpdateSource();
        }
    }
}
