﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UniGuy.Controls.Panels.Animated
{
  public class AnimatedWrapPanel : Panel, IScrollInfo
  {
    private static Size InfiniteSize =
      new Size(double.PositiveInfinity, double.PositiveInfinity);
    private const double LineSize = 16;
    private const double WheelSize = 3 * LineSize;

    private bool _CanHorizontallyScroll;
    private bool _CanVerticallyScroll;
    private ScrollViewer _ScrollOwner;
    private Vector _Offset;
    private Size _Extent;
    private Size _Viewport;

    private TimeSpan _AnimationLength = TimeSpan.FromMilliseconds(200);

    protected override Size MeasureOverride(Size availableSize)
    {
      double curX = 0, curY = 0, curLineHeight = 0, maxLineWidth = 0;
      foreach (UIElement child in Children)
      {
        child.Measure(InfiniteSize);

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

      return _Viewport;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      if (this.Children == null || this.Children.Count == 0)
      { return finalSize; }

      TranslateTransform trans = null;
      double curX = 0, curY = 0, curLineHeight = 0, maxLineWidth = 0;

      foreach (UIElement child in Children)
      {
        trans = child.RenderTransform as TranslateTransform;
        if (trans == null)
        {
          child.RenderTransformOrigin = new Point(0, 0);
          trans = new TranslateTransform();
          child.RenderTransform = trans;
        }

        if (curX + child.DesiredSize.Width > finalSize.Width)
        { //Wrap to next line
          curY += curLineHeight;
          curX = 0;
          curLineHeight = 0;
        }

        child.Arrange(new Rect(0, 0, 
          child.DesiredSize.Width, child.DesiredSize.Height));

        trans.BeginAnimation(TranslateTransform.XProperty, 
          new DoubleAnimation(curX - HorizontalOffset, _AnimationLength), 
          HandoffBehavior.Compose);
        trans.BeginAnimation(TranslateTransform.YProperty, 
          new DoubleAnimation(curY - VerticalOffset, _AnimationLength), 
          HandoffBehavior.Compose);

        curX += child.DesiredSize.Width;
        if (child.DesiredSize.Height > curLineHeight)
        { curLineHeight = child.DesiredSize.Height; }

        if (curX > maxLineWidth)
        { maxLineWidth = curX; }
      }

      curY += curLineHeight;
      VerifyScrollData(finalSize, new Size(maxLineWidth, curY));

      return finalSize;
    }
    
    #region Movement Methods
    public void LineDown()
    { SetVerticalOffset(VerticalOffset + LineSize); }

    public void LineUp()
    { SetVerticalOffset(VerticalOffset - LineSize); }

    public void LineLeft()
    { SetHorizontalOffset(HorizontalOffset - LineSize); }

    public void LineRight()
    { SetHorizontalOffset(HorizontalOffset + LineSize); }

    public void MouseWheelDown()
    { SetVerticalOffset(VerticalOffset + WheelSize); }

    public void MouseWheelUp()
    { SetVerticalOffset(VerticalOffset - WheelSize); }

    public void MouseWheelLeft()
    { SetHorizontalOffset(HorizontalOffset - WheelSize); }

    public void MouseWheelRight()
    { SetHorizontalOffset(HorizontalOffset + WheelSize); }

    public void PageDown()
    { SetVerticalOffset(VerticalOffset + ViewportHeight); }

    public void PageUp()
    { SetVerticalOffset(VerticalOffset - ViewportHeight); }

    public void PageLeft()
    { SetHorizontalOffset(HorizontalOffset - ViewportWidth); }

    public void PageRight()
    { SetHorizontalOffset(HorizontalOffset + ViewportWidth); }
    #endregion

    public ScrollViewer ScrollOwner
    {
      get { return _ScrollOwner; }
      set { _ScrollOwner = value; }
    }

    public bool CanHorizontallyScroll
    {
      get { return _CanHorizontallyScroll; }
      set { _CanHorizontallyScroll = value; }
    }

    public bool CanVerticallyScroll
    {
      get { return _CanVerticallyScroll; }
      set { _CanVerticallyScroll = value; }
    }

    public double ExtentHeight
    { get { return _Extent.Height; } }

    public double ExtentWidth
    { get { return _Extent.Width; } }

    public double HorizontalOffset
    { get { return _Offset.X; } }

    public double VerticalOffset
    { get { return _Offset.Y; } }

    public double ViewportHeight
    { get { return _Viewport.Height; } }

    public double ViewportWidth
    { get { return _Viewport.Width; } }

    public Rect MakeVisible(Visual visual, Rect rectangle)
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

    private static double CalculateNewScrollOffset(double topView, 
      double bottomView, double topChild, double bottomChild)
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

    protected void VerifyScrollData(Size viewport, Size extent)
    {
      if (double.IsInfinity(viewport.Width))
      { viewport.Width = extent.Width; }

      if (double.IsInfinity(viewport.Height))
      { viewport.Height = extent.Height; }

      _Extent = extent;
      _Viewport = viewport;

      _Offset.X = Math.Max(0, 
        Math.Min(_Offset.X, ExtentWidth - ViewportWidth));
      _Offset.Y = Math.Max(0, 
        Math.Min(_Offset.Y, ExtentHeight - ViewportHeight));

      if (ScrollOwner != null)
      { ScrollOwner.InvalidateScrollInfo(); }
    }

    public void SetHorizontalOffset(double offset)
    {
      offset = Math.Max(0, 
        Math.Min(offset, ExtentWidth - ViewportWidth));
      if (offset != _Offset.Y)
      {
        _Offset.X = offset;
        InvalidateArrange();
      }
    }

    public void SetVerticalOffset(double offset)
    {
      offset = Math.Max(0, 
        Math.Min(offset, ExtentHeight - ViewportHeight));
      if (offset != _Offset.Y)
      {
        _Offset.Y = offset;
        InvalidateArrange();
      }
    }
  }
}
