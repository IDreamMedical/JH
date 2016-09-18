using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections;
using System.Windows.Data;

namespace UniGuy.Controls.Behaviors
{
    /// <summary>
    /// A behavior that allows an adorner for the content to
    /// be defined in XAML.
    /// </summary>
    public static class AdornerBehavior
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency properties.
        /// </summary>
        public static readonly DependencyProperty IsAdornerVisibleProperty =
            DependencyProperty.RegisterAttached("IsAdornerVisible", typeof(bool), typeof(AdornerBehavior),
                new FrameworkPropertyMetadata(OnIsAdornerVisiblePropertyChanged));
        /// <summary>
        /// Shows or hides the adorner.
        /// Set to 'true' to show the adorner or 'false' to hide the adorner.
        /// </summary>
        public static bool GetIsAdornerVisible(DependencyObject d)
        {
            return (bool)d.GetValue(IsAdornerVisibleProperty);
        }
        public static void SetIsAdornerVisible(DependencyObject d, bool value)
        {
            d.SetValue(IsAdornerVisibleProperty, value);
        }

        public static readonly DependencyProperty AdornerProperty =
            DependencyProperty.RegisterAttached("Adorner", typeof(FrameworkElementAdorner), typeof(AdornerBehavior));

        public static readonly DependencyProperty AdornerContentProperty =
            DependencyProperty.RegisterAttached("AdornerContent", typeof(FrameworkElement), typeof(AdornerBehavior)
            , new PropertyMetadata(OnAdornerContentPropertyChanged));
        /// <summary>
        /// Used in XAML to define the UI content of the adorner.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static FrameworkElement GetAdornerContent(DependencyObject d)
        {
            return (FrameworkElement)d.GetValue(AdornerContentProperty);
        }
        public static void SetAdornerContent(DependencyObject d, FrameworkElement value)
        {
            d.SetValue(AdornerContentProperty, value);
        }

        public static readonly DependencyProperty HorizontalAdornerPlacementProperty =
            DependencyProperty.RegisterAttached("HorizontalAdornerPlacement", typeof(AdornerPlacement), typeof(AdornerBehavior),
                new FrameworkPropertyMetadata(AdornerPlacement.Inside));
        /// <summary>
        /// Specifies the horizontal placement of the adorner relative to the adorned control.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static AdornerPlacement GetHorizontalAdornerPlacement(DependencyObject d)
        {
            return (AdornerPlacement)d.GetValue(HorizontalAdornerPlacementProperty);
        }
        public static void SetHorizontalAdornerPlacement(DependencyObject d, AdornerPlacement value)
        {
            d.SetValue(HorizontalAdornerPlacementProperty, value);
        }

        public static readonly DependencyProperty VerticalAdornerPlacementProperty =
            DependencyProperty.RegisterAttached("VerticalAdornerPlacement", typeof(AdornerPlacement), typeof(AdornerBehavior),
                new FrameworkPropertyMetadata(AdornerPlacement.Inside));
        /// <summary>
        /// Specifies the vertical placement of the adorner relative to the adorned control.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static AdornerPlacement GetVerticalAdornerPlacement(DependencyObject d)
        {
            return (AdornerPlacement)d.GetValue(VerticalAdornerPlacementProperty);
        }
        public static void SetVerticalAdornerPlacement(DependencyObject d, AdornerPlacement value)
        {
            d.SetValue(VerticalAdornerPlacementProperty, value);
        }

        public static readonly DependencyProperty AdornerOffsetXProperty =
            DependencyProperty.RegisterAttached("AdornerOffsetX", typeof(double), typeof(AdornerBehavior));
        /// <summary>
        /// X offset of the adorner.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double GetAdornerOffsetX(DependencyObject d)
        {
            return (double)d.GetValue(AdornerOffsetXProperty);
        }
        public static void SetAdornerOffsetX(DependencyObject d, double value)
        {
            d.SetValue(AdornerOffsetXProperty, value);
        }

        public static readonly DependencyProperty AdornerOffsetYProperty =
            DependencyProperty.RegisterAttached("AdornerOffsetY", typeof(double), typeof(AdornerBehavior));
        /// <summary>
        /// Y offset of the adorner.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double GetAdornerOffsetY(DependencyObject d)
        {
            return (double)d.GetValue(AdornerOffsetYProperty);
        }
        public static void SetAdornerOffsetY(DependencyObject d, double value)
        {
            d.SetValue(AdornerOffsetYProperty, value);
        }

        #endregion Dependency Properties

        #region Callbacks
        /// <summary>
        /// Event raised when the value of IsAdornerVisible has changed.
        /// </summary>
        private static void OnIsAdornerVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = d as FrameworkElement;
            UpdateAdorner(fe);
        }
        private static void OnAdornerContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = d as FrameworkElement;
            UpdateAdorner(fe);
        }
        #endregion

        #region EventHandlers
        private static void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                FrameworkElement adornerContent = GetAdornerContent(fe);
                if (adornerContent != null)
                {
                    adornerContent.DataContext = fe.DataContext;
                }
            }
        }
        private static void OnAdornedFrameworkElementLoaded(object sender, RoutedEventArgs args)
        {
            UpdateAdorner((FrameworkElement)sender);
        }
        #endregion

        #region Methods
        private static void UpdateAdorner(FrameworkElement fe)
        {
            if (fe != null)
            {
                if (GetIsAdornerVisible(fe))
                {
                    ShowAdorner(fe);
                    fe.DataContextChanged += OnDataContextChanged;
                    fe.Loaded += OnAdornedFrameworkElementLoaded;
                }
                else
                {
                    HideAdorner(fe);
                    fe.DataContextChanged -= OnDataContextChanged;
                }
            }
        }
        private static void ShowAdorner(FrameworkElement fe)
        {
            FrameworkElement adornerContent = GetAdornerContent(fe);

            if (fe != null && fe.GetValue(AdornerProperty) == null)
            {
                AdornerLayer al = AdornerLayer.GetAdornerLayer(fe);
                if (al != null)
                {
                    FrameworkElementAdorner adorner = new FrameworkElementAdorner(adornerContent, fe);
                    al.Add(adorner);
                    BindAdorner(fe, adorner);
                    fe.SetValue(AdornerProperty, adorner);
                }
            }
        }
        private static void HideAdorner(FrameworkElement fe)
        {
            FrameworkElement adornerContent = GetAdornerContent(fe);

            if (fe != null && fe.GetValue(AdornerProperty) != null)
            {
                AdornerLayer al = AdornerLayer.GetAdornerLayer(fe);
                if (al != null)
                {
                    FrameworkElementAdorner adorner = fe.GetValue(AdornerProperty) as FrameworkElementAdorner;
                    if (adorner != null)
                    {
                        al.Remove(adorner);
                        adorner.DisconnectChild();
                        fe.SetValue(AdornerProperty, null);
                    }
                }
            }
        }
        private static void BindAdorner(FrameworkElement fe, FrameworkElementAdorner adorner)
        {
            if (fe == null)
            {
                throw new ArgumentNullException("element");
            }
            if (adorner == null)
            {
                throw new ArgumentNullException("adorner");
            }
            Binding binding = new Binding() { Path = new PropertyPath(AdornerContentProperty) };
            binding.Mode = BindingMode.OneWay;
            binding.Source = fe;
            adorner.SetBinding(FrameworkElementAdorner.AdornerContentProperty, binding);

            binding = new Binding() { Path = new PropertyPath(HorizontalAdornerPlacementProperty) };
            binding.Mode = BindingMode.OneWay;
            binding.Source = fe;
            adorner.SetBinding(FrameworkElementAdorner.HorizontalAdornerPlacementProperty, binding);

            binding = new Binding() { Path = new PropertyPath(VerticalAdornerPlacementProperty) };
            binding.Mode = BindingMode.OneWay;
            binding.Source = fe;
            adorner.SetBinding(FrameworkElementAdorner.VerticalAdornerPlacementProperty, binding);

            binding = new Binding() { Path = new PropertyPath(AdornerOffsetXProperty) };
            binding.Mode = BindingMode.OneWay;
            binding.Source = fe;
            adorner.SetBinding(FrameworkElementAdorner.AdornerOffsetXProperty, binding);

            binding = new Binding() { Path = new PropertyPath(AdornerOffsetYProperty) };
            binding.Mode = BindingMode.OneWay;
            binding.Source = fe;
            adorner.SetBinding(FrameworkElementAdorner.AdornerOffsetYProperty, binding);
        }
        #endregion
    }
}
