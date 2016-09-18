using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace UniGuy.Controls.Panels
{
    /// <summary>
    /// A Panel that lays out its children in a row
    /// and allows them to stretch to take up all
    /// available space
    /// </summary>
    public class BoxSizer : Panel
    {
        #region Attached Properties for children

        public static readonly DependencyProperty ProportionProperty
            = DependencyProperty.RegisterAttached("Proportion", typeof(double), typeof(BoxSizer),
            new FrameworkPropertyMetadata(.0, FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentArrange |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        [AttachedPropertyBrowsableForChildren]
        [Category("Layout")]
        public static double GetProportion(DependencyObject d)
        {
            return (double)d.GetValue(ProportionProperty);
        }

        public static void SetProportion(DependencyObject d, double value)
        {
            d.SetValue(ProportionProperty, value);
        }

        #endregion

        #region Dependency properties

        //  Using a DependencyProperty as the backing store for Orientation. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty
            = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(BoxSizer),
            new FrameworkPropertyMetadata(Orientation.Vertical,
                FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Determines whether the BoxSizer is laid out horizontally
        /// (left-to-right) or vertically (top-to-bottom)
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        #endregion

        #region Measurement pass

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.Orientation == Orientation.Horizontal)
            {
                Size sizeForChildren = new Size(double.PositiveInfinity, availableSize.Height);
                double totalWidth = 0;
                double maxHeight = 0;
                foreach (UIElement child in InternalChildren)
                {
                    child.Measure(sizeForChildren);
                    totalWidth += child.DesiredSize.Width;
                    maxHeight = Math.Max(maxHeight, child.DesiredSize.Height);
                }
                return new Size
                {
                    Width = Math.Min(totalWidth, availableSize.Width),
                    Height = double.IsInfinity(availableSize.Height) ? maxHeight : availableSize.Height
                };
            }
            else
            {
                Size sizeForChildren = new Size(availableSize.Width, double.PositiveInfinity);
                double totalHeight = 0;
                double maxWidth = 0;
                foreach (UIElement child in InternalChildren)
                {
                    child.Measure(sizeForChildren);
                    totalHeight += child.DesiredSize.Height;
                    maxWidth = Math.Max(maxWidth, child.DesiredSize.Width);
                }
                return new Size
                {
                    Width = double.IsInfinity(availableSize.Width) ? maxWidth : availableSize.Width,
                    Height = Math.Min(totalHeight, availableSize.Height)
                };
            }
        }
        #endregion

        #region Arrange pass

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.Orientation == Orientation.Horizontal)
            {
                double totalProportion = 0;
                double fixedWidth = 0;
                //  The first time we iterate over the children
                //  we're looking for how much space needs to be
                //  allocated for fixed-size elements and what
                //  the total proportion is for sizing the variable-size
                //  elements.
                foreach (UIElement child in InternalChildren)
                {
                    double childProportion = GetProportion(child);
                    if (childProportion == 0)
                    {
                        fixedWidth += child.DesiredSize.Width;
                    }
                    else
                    {
                        totalProportion += childProportion;
                    }
                }
                double variableWidth = Math.Max(finalSize.Width - fixedWidth, 0);

                //  The second time we iterate over the children
                //  we'll tell them the size they're getting.
                Rect childRect = new Rect();
                foreach (UIElement child in InternalChildren)
                {
                    double childProportion = GetProportion(child);
                    if (childProportion == 0)
                    {
                        childRect.Width = child.DesiredSize.Width;
                    }
                    else
                    {
                        childRect.Width = childProportion / totalProportion * variableWidth;
                    }
                    childRect.Height = finalSize.Height;
                    child.Arrange(childRect);
                    childRect.X += childRect.Width;
                }
            }
            else
            {
                double totalProportion = 0;
                double fixedHeight = 0;
                foreach (UIElement child in InternalChildren)
                {
                    double childProportion = GetProportion(child);
                    if (childProportion == 0)
                    {
                        fixedHeight += child.DesiredSize.Height;
                    }
                    else
                    {
                        totalProportion += childProportion;
                    }
                }

                double variableHeight = Math.Max(finalSize.Height - fixedHeight, 0);

                Rect childRect = new Rect();
                foreach (UIElement child in InternalChildren)
                {
                    double childProportion = GetProportion(child);
                    if (childProportion == 0)
                    {
                        childRect.Height = child.DesiredSize.Height;
                    }
                    else
                    {
                        childRect.Height = childProportion / totalProportion * variableHeight;
                    }
                    childRect.Width = finalSize.Width;
                    child.Arrange(childRect);
                    childRect.Y += childRect.Height;
                }
            }
            return finalSize;
        }

        #endregion
    }
}
