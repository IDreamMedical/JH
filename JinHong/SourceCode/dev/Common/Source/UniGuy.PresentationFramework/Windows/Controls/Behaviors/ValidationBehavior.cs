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
    /// <summary>
    /// 用于验证数据的行为(数据改变将自动重新验证)
    /// </summary>
    public static class ValidationBehavior
    {
        /// <summary>
        /// 验证规则集合
        /// </summary>
        public static readonly DependencyProperty ValidationRulesProperty
            = DependencyProperty.RegisterAttached("ValidationRules", typeof(IEnumerable<ValidationRule>), typeof(ValidationBehavior));

        public static IEnumerable<ValidationRule> GetValidationRules(DependencyObject d)
        {
            return (IEnumerable<ValidationRule>)d.GetValue(ValidationRulesProperty);
        }
        public static void SetValidationRules(DependencyObject d, IEnumerable<ValidationRule> value)
        {
            d.SetValue(ValidationRulesProperty, value);
        }

        /// <summary>
        /// 验证错误结果集合(如果没有错误返回null)
        /// </summary>
        public static readonly DependencyProperty ErrorsProperty
            = DependencyProperty.RegisterAttached("Errors", typeof(List<ValidationResult>), typeof(ValidationBehavior)
            , new PropertyMetadata(new PropertyChangedCallback(OnErrorsPropertyChanged)));

        public static List<ValidationResult> GetErrors(DependencyObject d)
        {
            return (List<ValidationResult>)d.GetValue(ErrorsProperty);
        }
        public static void SetErrors(DependencyObject d, List<ValidationResult> value)
        {
            d.SetValue(ErrorsProperty, value);
        }

        /// <summary>
        /// 指示是否包含错误
        /// </summary>
        public static readonly DependencyProperty HasErrorsProperty
            = DependencyProperty.RegisterAttached("HasErrors", typeof(bool), typeof(ValidationBehavior));

        public static bool GetHasErrors(DependencyObject d)
        {
            return (bool)d.GetValue(HasErrorsProperty);
        }
        public static void SetHasErrors(DependencyObject d, bool value)
        {
            d.SetValue(HasErrorsProperty, value);
        }

        /// <summary>
        /// 验证的数据
        /// </summary>
        public static readonly DependencyProperty SourceProperty
            = DependencyProperty.RegisterAttached("Source", typeof(object), typeof(ValidationBehavior)
            , new PropertyMetadata(new PropertyChangedCallback(OnSourcePropertyChanged)));

        public static object GetSource(DependencyObject d)
        {
            return d.GetValue(SourceProperty);
        }
        public static void SetSource(DependencyObject d, object value)
        {
            d.SetValue(SourceProperty, value);
        }

        /// <summary>
        /// 错误列表发生改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="args"></param>
        private static void OnErrorsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            List<ValidationResult> errors = GetErrors(d);
            SetHasErrors(d, errors != null && errors.Count != 0);
        }

        /// <summary>
        /// 验证数据发生改变(将重新验证)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="args"></param>
        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            IEnumerable<ValidationRule> rules = GetValidationRules(d);
            if (rules != null)
            {
                List<ValidationResult> errors = new List<ValidationResult>();
                object source = GetSource(d);

                foreach (ValidationRule rule in rules)
                {
                    ValidationResult result = rule.Validate(source, System.Globalization.CultureInfo.CurrentCulture);
                    if (!result.IsValid)
                        errors.Add(result);
                }

                SetErrors(d, errors);
            }
            else
            {
                SetErrors(d, null);
            }
        }
    }
}
