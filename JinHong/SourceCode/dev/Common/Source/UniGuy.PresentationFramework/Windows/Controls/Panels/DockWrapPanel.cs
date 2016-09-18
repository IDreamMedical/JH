using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace UniGuy.Controls.Panels
{
    public class DockWrapPanel:Panel
    {
        class RowInfo
        {
            public double TotalWidth { get; set; }
            public double Height { get; set; }
            public FrameworkElement StretchItem { get; set; }
            public List<FrameworkElement> LeftItems { get; set; }
            public List<FrameworkElement> RightItems { get; set; }
            public void Add(FrameworkElement child)
            {
                if (DockPanel.GetDock(child) == Dock.Right)
                    RightItems.Add(child);
                else
                    LeftItems.Add(child);
                if (child.HorizontalAlignment == HorizontalAlignment.Stretch && double.IsNaN(child.Width))
                    StretchItem = child;
                TotalWidth += child.DesiredSize.Width;
            }
            public int Count
            {
                get { return LeftItems.Count + RightItems.Count; }
            }
            public RowInfo()
            {
                LeftItems = new List<FrameworkElement>();
                RightItems = new List<FrameworkElement>();
                Height = 0;
                TotalWidth = 0;
            }
        }
        protected override Size MeasureOverride(System.Windows.Size availableSize)
        {
            double xOffset = 0;
            double rowHeight = 0;
            double totalHeight = 0;
            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);
                // move to new row
                if (xOffset + child.DesiredSize.Width > availableSize.Width)
                {
                    totalHeight += rowHeight;
                    xOffset = 0;
                    rowHeight = 0;
                }
                if (child.DesiredSize.Height > rowHeight)
                    rowHeight = child.DesiredSize.Height;
                xOffset += child.DesiredSize.Width;
            }
            totalHeight += rowHeight;
            return new Size(availableSize.Width, totalHeight);
        }
        protected override Size ArrangeOverride(System.Windows.Size finalSize)
        {
            double x = 0;
            List<RowInfo> rows = new List<RowInfo>();
            RowInfo currow = new RowInfo();
            foreach (FrameworkElement child in this.Children)
            {
                if (currow.Count > 0 && x + child.DesiredSize.Width > finalSize.Width)
                {
                    rows.Add(currow);
                    currow = new RowInfo();
                    x = 0;
                }
                if (child.DesiredSize.Height > currow.Height)
                    currow.Height = child.DesiredSize.Height;
                currow.Add(child);
                x += child.DesiredSize.Width;
            }
            rows.Add(currow);
            double y = 0;
            foreach (RowInfo row in rows)
            {
                x = 0;
                foreach (FrameworkElement child in row.LeftItems)
                {
                    Rect r = new Rect();
                    r.X = x;
                    r.Y = y;
                    double childHeight = child.DesiredSize.Height;
                    if (double.IsNaN(child.Height))
                        childHeight = row.Height;
                    if (child.VerticalAlignment == VerticalAlignment.Center)
                        r.Y = y + row.Height / 2 - childHeight / 2;
                    if (child.VerticalAlignment == VerticalAlignment.Bottom)
                        r.Y = y + row.Height - childHeight;
                    if (object.Equals(child, row.StretchItem))
                        r.Width = child.DesiredSize.Width + (finalSize.Width - row.TotalWidth);
                    else
                        r.Width = child.DesiredSize.Width;
                    r.Height = childHeight;
                    child.Arrange(r);
                    x = r.Right;
                }
                row.RightItems.Reverse();
                x = finalSize.Width;
                foreach (FrameworkElement child in row.RightItems)
                {
                    Rect r = new Rect();
                    r.X = x - child.DesiredSize.Width;
                    if (r.X < 0)
                        r.X = 0;
                    r.Y = y;
                    if (child.VerticalAlignment == VerticalAlignment.Center)
                        r.Y = y + row.Height / 2 - child.Height / 2;
                    if (child.VerticalAlignment == VerticalAlignment.Bottom)
                        r.Y = y + row.Height - child.Height;
                    if (object.Equals(child, row.StretchItem))
                    {
                        r.Width = child.DesiredSize.Width + (finalSize.Width - row.TotalWidth);
                        r.X -= (finalSize.Width - row.TotalWidth);
                    }
                    else
                        r.Width = child.DesiredSize.Width;
                    r.Height = child.DesiredSize.Height;
                    child.Arrange(r);
                    x = r.Left;
                }
                y += row.Height;
            }

            //finalSize.Height = y;
            return finalSize;
        }
    }
}
