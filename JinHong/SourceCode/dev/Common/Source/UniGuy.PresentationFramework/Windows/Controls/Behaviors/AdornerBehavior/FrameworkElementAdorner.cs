using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Collections;

//
// This code based on code available here:
//
//  http://www.codeproject.com/KB/WPF/WPFJoshSmith.aspx
//
namespace UniGuy.Controls.Behaviors
{
    //
    // This class is an adorner that allows a FrameworkElement derived class to adorn another FrameworkElement.
    //
    public class FrameworkElementAdorner:Adorner
    {
        /// <summary>
        /// The framework element that is the adorner.
        /// </summary>
        public static readonly DependencyProperty AdornerContentProperty
            = DependencyProperty.Register("AdornerContent", typeof(FrameworkElement), typeof(FrameworkElementAdorner));

        public FrameworkElement AdornerContent
        {
            get{return (FrameworkElement)GetValue(AdornerContentProperty);}
            set{SetValue(AdornerContentProperty, value);}
        }

        //
        // Placement of the AdornerContent.
        //
        public static readonly DependencyProperty HorizontalAdornerPlacementProperty
            = DependencyProperty.Register("HorizontalAdornerPlacement", typeof(AdornerPlacement), typeof(FrameworkElementAdorner),
            new PropertyMetadata(AdornerPlacement.Inside));
        public AdornerPlacement HorizontalAdornerPlacement
        {
            get { return (AdornerPlacement)GetValue(HorizontalAdornerPlacementProperty); }
            set { SetValue(HorizontalAdornerPlacementProperty, value); }
        }
        public static readonly DependencyProperty VerticalAdornerPlacementProperty
            = DependencyProperty.Register("VerticalAdornerPlacement", typeof(AdornerPlacement), typeof(FrameworkElementAdorner),
            new PropertyMetadata(AdornerPlacement.Inside));
        public AdornerPlacement VerticalAdornerPlacement
        {
            get { return (AdornerPlacement)GetValue(VerticalAdornerPlacementProperty); }
            set { SetValue(VerticalAdornerPlacementProperty, value); }
        }
        //
        // Offset of the AdornerContent.
        //
        public static readonly DependencyProperty AdornerOffsetXProperty
            = DependencyProperty.Register("AdornerOffsetX", typeof(double), typeof(FrameworkElementAdorner),
            new PropertyMetadata(0.0));
        public double AdornerOffsetX
        {
            get { return (double)GetValue(AdornerOffsetXProperty); }
            set { SetValue(AdornerOffsetXProperty, value); }
        }
        public static readonly DependencyProperty AdornerOffsetYProperty
            = DependencyProperty.Register("AdornerOffsetY", typeof(double), typeof(FrameworkElementAdorner),
            new PropertyMetadata(0.0));
        public double AdornerOffsetY
        {
            get { return (double)GetValue(AdornerOffsetYProperty); }
            set { SetValue(AdornerOffsetYProperty, value); }
        }

        //
        // Position of the AdornerContent (when not set to NaN).
        //
        private double positionX = Double.NaN;
        private double positionY = Double.NaN;

        public FrameworkElementAdorner(FrameworkElement adornerChildElement, FrameworkElement adornedElement)
            : base(adornedElement)
        {
            this.AdornerContent = adornerChildElement;

            base.AddLogicalChild(adornerChildElement);
            base.AddVisualChild(adornerChildElement);
        }

        /// <summary>
        /// Event raised when the adorned control's size has changed.
        /// </summary>
        private void adornedElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InvalidateMeasure();
        }

        //
        // Position of the AdornerContent (when not set to NaN).
        //
        public double PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }

        public double PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.AdornerContent.Measure(constraint);
            return this.AdornerContent.DesiredSize;
        }

        /// <summary>
        /// Determine the X coordinate of the AdornerContent.
        /// </summary>
        private double DetermineX()
        {
            switch (AdornerContent.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        if (HorizontalAdornerPlacement == AdornerPlacement.Across)
                        {
                            return -AdornerContent.DesiredSize.Width/2 + AdornerOffsetX;
                        }
                        if (HorizontalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            return -AdornerContent.DesiredSize.Width + AdornerOffsetX;
                        }
                        else
                        {
                            return AdornerOffsetX;
                        }
                    }
                case HorizontalAlignment.Right:
                    {
                        if (HorizontalAdornerPlacement == AdornerPlacement.Across)
                        {
                            double adornerWidth = this.AdornerContent.DesiredSize.Width;
                            double adornedWidth = AdornedElement.ActualWidth;
                            double x = adornedWidth - adornerWidth;
                            return x/2 + AdornerOffsetX;
                        }
                        if (HorizontalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            double adornedWidth = AdornedElement.ActualWidth;
                            return adornedWidth + AdornerOffsetX;
                        }
                        else
                        {
                            double adornerWidth = this.AdornerContent.DesiredSize.Width;
                            double adornedWidth = AdornedElement.ActualWidth;
                            double x = adornedWidth - adornerWidth;
                            return x + AdornerOffsetX;
                        }
                    }
                case HorizontalAlignment.Center:
                    {
                        double adornerWidth = this.AdornerContent.DesiredSize.Width;
                        double adornedWidth = AdornedElement.ActualWidth;
                        double x = (adornedWidth / 2) - (adornerWidth / 2);
                        return x + AdornerOffsetX;
                    }
                case HorizontalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the Y coordinate of the AdornerContent.
        /// </summary>
        private double DetermineY()
        {
            switch (AdornerContent.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    {
                        if (VerticalAdornerPlacement == AdornerPlacement.Across)
                        {
                            return -AdornerContent.DesiredSize.Height / 2 + AdornerOffsetY;
                        }
                        if (VerticalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            return -AdornerContent.DesiredSize.Height + AdornerOffsetY;
                        }
                        else
                        {
                            return AdornerOffsetY;
                        }
                    }
                case VerticalAlignment.Bottom:
                    {
                        if (VerticalAdornerPlacement == AdornerPlacement.Across)
                        {
                            double adornerHeight = this.AdornerContent.DesiredSize.Height;
                            double adornedHeight = AdornedElement.ActualHeight;
                            double x = adornedHeight - adornerHeight;
                            return x/2 + AdornerOffsetY;
                        }
                        if (VerticalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            double adornedHeight = AdornedElement.ActualHeight;
                            return adornedHeight + AdornerOffsetY;
                        }
                        else
                        {
                            double adornerHeight = this.AdornerContent.DesiredSize.Height;
                            double adornedHeight = AdornedElement.ActualHeight;
                            double x = adornedHeight - adornerHeight;
                            return x + AdornerOffsetY;
                        }
                    }
                case VerticalAlignment.Center:
                    {
                        double adornerHeight = this.AdornerContent.DesiredSize.Height;
                        double adornedHeight = AdornedElement.ActualHeight;
                        double x = (adornedHeight / 2) - (adornerHeight / 2);
                        return x + AdornerOffsetY;
                    }
                case VerticalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the width of the AdornerContent.
        /// </summary>
        private double DetermineWidth()
        {
            if (!Double.IsNaN(PositionX))
            {
                return this.AdornerContent.DesiredSize.Width;
            }

            switch (AdornerContent.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        return this.AdornerContent.DesiredSize.Width;
                    }
                case HorizontalAlignment.Right:
                    {
                        return this.AdornerContent.DesiredSize.Width;
                    }
                case HorizontalAlignment.Center:
                    {
                        return this.AdornerContent.DesiredSize.Width;
                    }
                case HorizontalAlignment.Stretch:
                    {
                        return AdornedElement.ActualWidth;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the height of the AdornerContent.
        /// </summary>
        private double DetermineHeight()
        {
            if (!Double.IsNaN(PositionY))
            {
                return this.AdornerContent.DesiredSize.Height;
            }

            switch (AdornerContent.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    {
                        return this.AdornerContent.DesiredSize.Height;
                    }
                case VerticalAlignment.Bottom:
                    {
                        return this.AdornerContent.DesiredSize.Height;
                    }
                case VerticalAlignment.Center:
                    {
                        return this.AdornerContent.DesiredSize.Height;
                    }
                case VerticalAlignment.Stretch:
                    {
                        return AdornedElement.ActualHeight;
                    }
            }

            return 0.0;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = PositionX;
            if (Double.IsNaN(x))
            {
                x = DetermineX();
            }
            double y = PositionY;
            if (Double.IsNaN(y))
            {
                y = DetermineY();
            }
            double adornerWidth = DetermineWidth();
            double adornerHeight = DetermineHeight();
            this.AdornerContent.Arrange(new Rect(x, y, adornerWidth, adornerHeight));
            return finalSize;
        }

        protected override Int32 VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(Int32 index)
        {
            return this.AdornerContent;
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                ArrayList list = new ArrayList();
                list.Add(this.AdornerContent);
                return (IEnumerator)list.GetEnumerator();
            }
        }

        /// <summary>
        /// Disconnect the AdornerContent element from the visual tree so that it may be reused later.
        /// </summary>
        public void DisconnectChild()
        {
            base.RemoveLogicalChild(AdornerContent);
            base.RemoveVisualChild(AdornerContent);
        }

        /// <summary>
        /// Override AdornedElement from base class for less type-checking.
        /// </summary>
        public new FrameworkElement AdornedElement
        {
            get
            {
                return (FrameworkElement)base.AdornedElement;
            }
        }
    }
}
