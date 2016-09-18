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
    public static class ExtendedAdornerBehavior
    {
        #region Dependency Properties

        /// <summary>
        /// Dependency properties. 
        /// 用于Adorned FrameworkElement,表示是否显示Adorner
        /// </summary>
        public static readonly DependencyProperty IsAdornerVisibleProperty =
            DependencyProperty.RegisterAttached("IsAdornerVisible", typeof(bool), typeof(ExtendedAdornerBehavior),
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

        /// <summary>
        /// 用于Adorned FrameworkElement,表示保存的当前Adorner
        /// </summary>
        public static readonly DependencyProperty AdornerProperty =
            DependencyProperty.RegisterAttached("Adorner", typeof(ExtendedAdorner), typeof(ExtendedAdornerBehavior));

        /// <summary>
        /// 用于Adorned FrameworkElement,表示Adorner Item的列表
        /// </summary>
        public static readonly DependencyProperty AdornerItemsProperty =
            DependencyProperty.RegisterAttached("AdornerItems", typeof(IEnumerable), typeof(ExtendedAdornerBehavior)
            , new PropertyMetadata(OnAdornerItemsPropertyChanged));
        /// <summary>
        /// Used in XAML to define the UI content of the adorner.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static IEnumerable GetAdornerItems(DependencyObject d)
        {
            return (IEnumerable)d.GetValue(AdornerItemsProperty);
        }
        public static void SetAdornerItems(DependencyObject d, IEnumerable value)
        {
            d.SetValue(AdornerItemsProperty, value);
        }

        /// <summary>
        /// 用在每一个Adorner Child上
        /// </summary>
        public static readonly DependencyProperty HorizontalPlacementProperty =
            DependencyProperty.RegisterAttached("HorizontalPlacement", typeof(AdornerPlacement), typeof(ExtendedAdornerBehavior),
                new FrameworkPropertyMetadata(AdornerPlacement.Across));
        /// <summary>
        /// Specifies the horizontal placement of the adorner relative to the adorned control.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static AdornerPlacement GetHorizontalPlacement(DependencyObject d)
        {
            return (AdornerPlacement)d.GetValue(HorizontalPlacementProperty);
        }
        public static void SetHorizontalPlacement(DependencyObject d, AdornerPlacement value)
        {
            d.SetValue(HorizontalPlacementProperty, value);
        }

        /// <summary>
        /// 用在每一个Adorner Child上
        /// </summary>
        public static readonly DependencyProperty VerticalPlacementProperty =
            DependencyProperty.RegisterAttached("VerticalPlacement", typeof(AdornerPlacement), typeof(ExtendedAdornerBehavior),
                new FrameworkPropertyMetadata(AdornerPlacement.Across));
        /// <summary>
        /// Specifies the vertical placement of the adorner relative to the adorned control.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static AdornerPlacement GetVerticalPlacement(DependencyObject d)
        {
            return (AdornerPlacement)d.GetValue(VerticalPlacementProperty);
        }
        public static void SetVerticalPlacement(DependencyObject d, AdornerPlacement value)
        {
            d.SetValue(VerticalPlacementProperty, value);
        }

        /// <summary>
        /// 用在每一个Adorner Child上
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty =
            DependencyProperty.RegisterAttached("OffsetX", typeof(double), typeof(ExtendedAdornerBehavior),
            new PropertyMetadata(.0));
        /// <summary>
        /// X offset of the adorner.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double GetOffsetX(DependencyObject d)
        {
            return (double)d.GetValue(OffsetXProperty);
        }
        public static void SetOffsetX(DependencyObject d, double value)
        {
            d.SetValue(OffsetXProperty, value);
        }

        /// <summary>
        /// 用在每一个Adorner Child上
        /// </summary>
        public static readonly DependencyProperty OffsetYProperty =
            DependencyProperty.RegisterAttached("OffsetY", typeof(double), typeof(ExtendedAdornerBehavior),
            new PropertyMetadata(.0));
        /// <summary>
        /// Y offset of the adorner.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double GetOffsetY(DependencyObject d)
        {
            return (double)d.GetValue(OffsetYProperty);
        }
        public static void SetOffsetY(DependencyObject d, double value)
        {
            d.SetValue(OffsetYProperty, value);
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
        private static void OnAdornerItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = d as FrameworkElement;
            UpdateAdorner(fe);
        }
        #endregion

        #region EventHandlers
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
                    //fe.DataContextChanged += OnDataContextChanged;
                    fe.Loaded += OnAdornedFrameworkElementLoaded;
                }
                else
                {
                    HideAdorner(fe);
                    //fe.DataContextChanged -= OnDataContextChanged;
                }
            }
        }
        private static void ShowAdorner(FrameworkElement fe)
        {
            IEnumerable adornerChildren = GetAdornerItems(fe);

            if (fe != null && fe.GetValue(AdornerProperty) == null && adornerItems!=null)
            {
                AdornerLayer al = AdornerLayer.GetAdornerLayer(fe);
                if (al != null)
                {
                    ExtendedAdorner adorner = new ExtendedAdorner(adornerItems, fe);
                    al.Add(adorner);
                    BindAdorner(fe, adorner);
                    fe.SetValue(AdornerProperty, adorner);
                }
            }
        }
        private static void HideAdorner(FrameworkElement fe)
        {
            IEnumerable<FrameworkElement> adornerChildren = GetAdornerChildren(fe);

            if (fe != null && fe.GetValue(AdornerProperty) != null)
            {
                AdornerLayer al = AdornerLayer.GetAdornerLayer(fe);
                if (al != null)
                {
                    ExtendedAdorner adorner = fe.GetValue(AdornerProperty) as ExtendedAdorner;
                    if (adorner != null)
                    {
                        al.Remove(adorner);
                        adorner.DisconnectChildren();
                        fe.SetValue(AdornerProperty, null);
                    }
                }
            }
        }
        private static void BindAdorner(FrameworkElement fe, ExtendedAdorner adorner)
        {
            if (fe == null)
            {
                throw new ArgumentNullException("element");
            }
            if (adorner == null)
            {
                throw new ArgumentNullException("adorner");
            }
            Binding binding = new Binding() { Path = new PropertyPath(AdornerChildrenProperty) };
            binding.Mode = BindingMode.OneWay;
            binding.Source = fe;
            adorner.SetBinding(ExtendedAdorner.AdornerChildrenProperty, binding);

            foreach (Visual visual in adorner.visualCollection)
            {
                FrameworkElement child = visual as FrameworkElement;
                if (child != null)
                {
                    binding = new Binding { Path = new PropertyPath(FrameworkElement.DataContextProperty) };
                    binding.Mode = BindingMode.OneWay;
                    binding.Source = fe;
                    child.SetBinding(FrameworkElement.DataContextProperty, binding);
                }
            }
        }
        #endregion
    }
}
