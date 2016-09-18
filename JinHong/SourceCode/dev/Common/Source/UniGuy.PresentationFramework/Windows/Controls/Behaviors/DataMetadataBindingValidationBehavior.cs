using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Data;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// 对于对Visual的指定属性的Binding提供DataMetadata的Validation
    /// 设置了DataAssemblyProperty或者DataMetadataIdProperty会启用IsEnabledProperty
    /// </summary>
    public class DataMetadataBindingValidationBehavior
    {
        /// <summary>
        /// 是否允许这样的Validation
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty
            = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(DataMetadataBindingValidationBehavior),
            new PropertyMetadata(false, OnIsEnabledPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static bool GetIsEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject d, bool value)
        {
            d.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// 绑定的目标依赖属性
        /// </summary>
        public static readonly DependencyProperty TargetPropertyProperty
            = DependencyProperty.RegisterAttached("TargetProperty", typeof(DependencyProperty), typeof(DataMetadataBindingValidationBehavior));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static DependencyProperty GetTargetProperty(DependencyObject d)
        {
            return (DependencyProperty)d.GetValue(TargetPropertyProperty);
        }

        public static void SetTargetProperty(DependencyObject d, DependencyProperty value)
        {
            d.SetValue(TargetPropertyProperty, value);
        }

        public static readonly DependencyProperty ValidationRuleProperty
            = DependencyProperty.RegisterAttached("ValidationRule", typeof(ValidationRule), typeof(DataMetadataBindingValidationBehavior),
            new PropertyMetadata(OnValidationRulePropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static ValidationRule GetValidationRule(DependencyObject d)
        {
            return (ValidationRule)d.GetValue(ValidationRuleProperty);
        }

        public static void SetValidationRule(DependencyObject d, ValidationRule value)
        {
            d.SetValue(ValidationRuleProperty, value);
        }

        /// <summary>
        /// 数据集
        /// </summary>
        public static readonly DependencyProperty DataAssemblyProperty
            = DependencyProperty.RegisterAttached("DataAssembly", typeof(DataAssembly), typeof(DataMetadataBindingValidationBehavior),
            new PropertyMetadata(OnDataAssemblyPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static DataAssembly GetDataAssembly(DependencyObject d)
        {
            return (DataAssembly)d.GetValue(DataAssemblyProperty);
        }

        public static void SetDataAssembly(DependencyObject d, DataAssembly value)
        {
            d.SetValue(DataAssemblyProperty, value);
        }

        public static readonly DependencyProperty DataMetadataIdProperty
            = DependencyProperty.RegisterAttached("DataMetadataId", typeof(string), typeof(DataMetadataBindingValidationBehavior),
            new PropertyMetadata(OnDataMetadataIdPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static string GetDataMetadataId(DependencyObject d)
        {
            return (string)d.GetValue(DataMetadataIdProperty);
        }

        public static void SetDataMetadataId(DependencyObject d, string value)
        {
            d.SetValue(DataMetadataIdProperty, value);
        }

        private static void OnIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            DependencyProperty targetProperty = GetTargetProperty(d);
            if (targetProperty != null)
            {
                Binding binding = BindingOperations.GetBinding(d, targetProperty);
                if (binding != null)
                {
                    if ((bool)args.NewValue)
                    {
                        binding.ValidationRules.Add(GetValidationRule(d));
                    }
                    else
                    {
                        binding.ValidationRules.Remove(GetValidationRule(d));
                    }
                }
            }
        }

        private static void OnDataAssemblyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            DataAssembly da = GetDataAssembly(d);
            string id = GetDataMetadataId(d);
            if (da != null && id != null)
            {
                SetValidationRule(d, new ValidationRules.FormatValidationRule(da[id].Format));
                SetIsEnabled(d, true);
            }
            else
            {
                SetValidationRule(d, null);
                SetIsEnabled(d, false);
            }
        }

        private static void OnDataMetadataIdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            DataAssembly da = GetDataAssembly(d);
            string id = GetDataMetadataId(d);
            if (da != null && id != null)
            {
                SetValidationRule(d, new ValidationRules.FormatValidationRule(da[id].Format));
                SetIsEnabled(d, true);
            }
            else
            {
                SetValidationRule(d, null);
                SetIsEnabled(d, false);
            }
        }

        private static void OnValidationRulePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (GetIsEnabled(d))
            {
                Binding binding = BindingOperations.GetBinding(d, GetTargetProperty(d));
                if (binding != null)
                {
                    if (args.OldValue != null)
                        binding.ValidationRules.Remove((ValidationRule)args.OldValue);
                    if (args.NewValue != null)
                        binding.ValidationRules.Add((ValidationRule)args.NewValue);
                }
            }
        }
    }
}
