using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace UniGuy.Controls.AnimatedPanels
{
    //  IScrollInfo 是wj 于20120421参照UniGuy.Controls.Panels.Animated.AnimatedWrapPanel新增的
    //  Learning:   Panel如果作为ItemsControl的ItemsPanel, 其IScrollInfo是不会用到的, 因为隔了一层ItemsPresenter, ScrollOwner也为null
    public class AnimatedWrapPanel : AnimatedPanel, IScrollInfo
    {
        private static Size infiniteSize =
            new Size(double.PositiveInfinity, double.PositiveInfinity);

        protected override Size MeasureOverride(Size availableSize)
        {
            /*
            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);
            }

            //  wj TODO 这个地方的实现不对的, 导致返回的尺寸为0, 如果这个Panel放ScrollView下, 其不会显示滚动条;
            return base.MeasureOverride(availableSize); 
             */

            //  wj 所以参照UniGuy.Controls.Panels.Animated.AnimatedWrapPanel进行了实现
            double curX = 0, curY = 0, curLineHeight = 0, maxLineWidth = 0;
            foreach (UIElement child in Children)
            {
                child.Measure(infiniteSize);

                if (curX + child.DesiredSize.Width > availableSize.Width)
                { //Wrap to next line
                    curY += curLineHeight;
                    curX = 0;
                    curLineHeight = 0;
                }

                curX += child.DesiredSize.Width;
                if (child.DesiredSize.Height > curLineHeight)
                { curLineHeight = child.DesiredSize.Height; }

                if (curX > maxLineWidth)
                { maxLineWidth = curX; }
            }

            curY += curLineHeight;

            VerifyScrollData(availableSize, new Size(maxLineWidth, curY));
            return _viewport;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0;
            double y = 0;
            double xLine = 0;   //  wj added
            double maxHeight = 0;
            foreach (UIElement child in this.Children)
            {
                if (x + child.DesiredSize.Width >= finalSize.Width)
                {
                    xLine = Math.Max(xLine, x); //  wj added
                    x = 0;
                    y += maxHeight;
                    maxHeight = 0;
                }

                base.AnimatedArrange(child, new Rect(x, y, child.DesiredSize.Width, child.DesiredSize.Height));
                maxHeight = Math.Max(child.DesiredSize.Height, maxHeight);
                x += child.DesiredSize.Width;

            }

            //  wj added
            lineSize = maxHeight;
            double i = Math.Floor(ActualHeight / maxHeight);
            wheelSize = i == 0 ? ActualHeight : i * maxHeight;
            VerifyScrollData(finalSize, new Size(xLine, y+maxHeight));

            return base.ArrangeOverride(finalSize);
        }

        #region IScrollInfo 成员

        private double lineSize = 16;   //  给定了初始值
        private double wheelSize = 48;    //  给定了初始值

        private ScrollViewer _scrollOwner;
        private bool _canHorizontallyScroll;
        private bool _canVerticallyScroll;
        private Size _extent;
        private Vector _offset;
        private Size _viewport;

        public bool CanHorizontallyScroll
        {
            get { return _canHorizontallyScroll; }
            set { _canHorizontallyScroll = value; }
        }

        public bool CanVerticallyScroll
        {
            get { return _canVerticallyScroll; }
            set { _canVerticallyScroll = value; }
        }

        public double ExtentHeight
        {
            get { return _extent.Height; }
        }

        public double ExtentWidth
        {
            get { return _extent.Width; }
        }

        public double HorizontalOffset
        {
            get { return _offset.X; }
        }

        public void LineDown()
        {
            SetVerticalOffset(VerticalOffset + lineSize); 
        }

        public void LineLeft()
        {
            SetHorizontalOffset(HorizontalOffset - lineSize); 
        }

        public void LineRight()
        {
            SetHorizontalOffset(HorizontalOffset + lineSize);
        }

        public void LineUp()
        {
            SetVerticalOffset(VerticalOffset - lineSize);
        }

        private static double CalculateNewScrollOffset(double topView, double bottomView, double topChild, double bottomChild)
        {
            bool offBottom = topChild < topView && bottomChild < bottomView;
            bool offTop = bottomChild > bottomView && topChild > topView;
            bool tooLarge = (bottomChild - topChild) > (bottomView - topView);

            if (!offBottom && !offTop)
            { return topView; } //Don't do anything, already in view

            if ((offBottom && !tooLarge) || (offTop && tooLarge))
            { return topChild; }

            return (bottomChild - (bottomView - topView));
        }

        public Rect MakeVisible(System.Windows.Media.Visual visual, Rect rectangle)
        {
            if (rectangle.IsEmpty || visual == null
                || visual == this || !base.IsAncestorOf(visual))
            { return Rect.Empty; }

            rectangle = visual.TransformToAncestor(this).TransformBounds(rectangle);

            Rect viewRect = new Rect(HorizontalOffset,
              VerticalOffset, ViewportWidth, ViewportHeight);
            rectangle.X += viewRect.X;
            rectangle.Y += viewRect.Y;
            viewRect.X = CalculateNewScrollOffset(viewRect.Left,
              viewRect.Right, rectangle.Left, rectangle.Right);
            viewRect.Y = CalculateNewScrollOffset(viewRect.Top,
              viewRect.Bottom, rectangle.Top, rectangle.Bottom);
            SetHorizontalOffset(viewRect.X);
            SetVerticalOffset(viewRect.Y);
            rectangle.Intersect(viewRect);
            rectangle.X -= viewRect.X;
            rectangle.Y -= viewRect.Y;

            return rectangle;
        }

        public void MouseWheelDown()
        {
            SetVerticalOffset(VerticalOffset + wheelSize);
        }

        public void MouseWheelLeft()
        {
            SetHorizontalOffset(HorizontalOffset - wheelSize);
        }

        public void MouseWheelRight()
        {
            SetHorizontalOffset(HorizontalOffset + wheelSize); 
        }

        public void MouseWheelUp()
        {
            SetVerticalOffset(VerticalOffset - wheelSize);
        }

        public void PageDown()
        {
            SetVerticalOffset(VerticalOffset + ViewportHeight);
        }

        public void PageLeft()
        {
            SetHorizontalOffset(HorizontalOffset - ViewportWidth);
        }

        public void PageRight()
        {
            SetHorizontalOffset(HorizontalOffset + ViewportWidth);
        }

        public void PageUp()
        {
            SetVerticalOffset(VerticalOffset - ViewportHeight); 
        }

        public ScrollViewer ScrollOwner
        {
            get { return _scrollOwner; }
            set { _scrollOwner = value; }
        }

        public void SetHorizontalOffset(double offset)
        {
            offset = Math.Max(0,Math.Min(offset, ExtentWidth - ViewportWidth));
            if (offset != _offset.Y)
            {
                _offset.X = offset;
                InvalidateArrange();
            }
        }

        public void SetVerticalOffset(double offset)
        {
            offset = Math.Max(0, Math.Min(offset, ExtentHeight - ViewportHeight));
            if (offset != _offset.Y)
            {
                _offset.Y = offset;
                InvalidateArrange();
            }
        }

        protected void VerifyScrollData(Size viewport, Size extent)
        {
            if (double.IsInfinity(viewport.Width))
            { viewport.Width = extent.Width; }

            if (double.IsInfinity(viewport.Height))
            { viewport.Height = extent.Height; }

            _extent = extent;
            _viewport = viewport;

            _offset.X = Math.Max(0,
              Math.Min(_offset.X, ExtentWidth - ViewportWidth));
            _offset.Y = Math.Max(0,
              Math.Min(_offset.Y, ExtentHeight - ViewportHeight));

            if (ScrollOwner != null)
            { ScrollOwner.InvalidateScrollInfo(); }
        }

        public double VerticalOffset
        {
            get { return _offset.Y; }
        }

        public double ViewportHeight
        {
            get { return _viewport.Height; }
        }

        public double ViewportWidth
        {
            get { return _viewport.Width; }
        }

        #endregion
    }
}
