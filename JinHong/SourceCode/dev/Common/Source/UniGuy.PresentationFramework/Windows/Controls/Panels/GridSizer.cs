/*
 * FlexGridSizer class
 * Copyright (c) July 2, 2009 by Michael Chansky
 * 
 * The use and redistribution of this code is governed by
 * version 1.02 of The Code Project Open License.  See
 * LICENSE.txt or http://www.codeproject.com/info/cpol10.aspx
 * for the full text of this license.
 */

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace UniGuy.Controls.Panels
{
    /// <summary>
    /// A panel that lays out its children in a grid
    /// and can adapt to the DesiredSize of its children.
    /// </summary>
    public class GridSizer : Panel
    {
        #region Attached properties for children

        public static readonly DependencyProperty HorizontalProportionProperty =
            DependencyProperty.RegisterAttached("HorizontalProportion", typeof(double), typeof(GridSizer),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentArrange |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        [AttachedPropertyBrowsableForChildren]
        [Category("Layout")]
        public static double GetHorizontalProportion(DependencyObject obj)
        {
            return (double)obj.GetValue(HorizontalProportionProperty);
        }

        public static void SetHorizontalProportion(DependencyObject obj, double value)
        {
            obj.SetValue(HorizontalProportionProperty, value);
        }
        
        public static readonly DependencyProperty VerticalProportionProperty =
            DependencyProperty.RegisterAttached("VerticalProportion", typeof(double), typeof(GridSizer),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentArrange |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        [AttachedPropertyBrowsableForChildren]
        [Category("Layout")]
        public static double GetVerticalProportion(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalProportionProperty);
        }

        public static void SetVerticalProportion(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalProportionProperty, value);
        }

        public static readonly DependencyProperty RowProperty =
            DependencyProperty.RegisterAttached("Row", typeof(int), typeof(GridSizer),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentArrange |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        [AttachedPropertyBrowsableForChildren]
        [Category("Layout")]
        public static int GetRow(DependencyObject obj)
        {
            return (int)obj.GetValue(RowProperty);
        }

        public static void SetRow(DependencyObject obj, int value)
        {
            obj.SetValue(RowProperty, value);
        }
        
        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.RegisterAttached("Column", typeof(int), typeof(GridSizer),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentArrange |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        [AttachedPropertyBrowsableForChildren]
        [Category("Layout")]
        public static int GetColumn(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnProperty);
        }

        public static void SetColumn(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnProperty, value);
        }

        #endregion

        protected override Size MeasureOverride(Size availableSize)
        {
            List<double> columnWidths = new List<double>();
            List<double> rowHeights = new List<double>();
            Size sizeForChildren = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (UIElement child in InternalChildren)
            {
                int row = GetRow(child);
                int column = GetColumn(child);
                // Expand the width and height arrays if necessary.
                while ((row + 1) > rowHeights.Count)
                {
                    rowHeights.Add(0.0);
                }
                while ((column + 1) > columnWidths.Count)
                {
                    columnWidths.Add(0.0);
                }
                // Let the Child calculate its desired size
                child.Measure(sizeForChildren);
                // Keep track of the maximums
                rowHeights[row] = Math.Max(child.DesiredSize.Height, rowHeights[row]);
                columnWidths[column] = Math.Max(child.DesiredSize.Width, columnWidths[column]);
            }
            // Return a size that's as big enough to accomedate all
            // of the children's desired sizes, so long as that's
            // not larger than the available size.
            double totalRowHeights = 0;
            double totalColumnWidth = 0;
            foreach (double rowHeight in rowHeights)
                totalRowHeights += rowHeight;
            foreach (double columnWidth in columnWidths)
                totalColumnWidth += columnWidth;
            return new Size()
            {
                Height = Math.Min(totalRowHeights, availableSize.Height),
                Width = Math.Min(totalColumnWidth, availableSize.Width)
            };
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // First pass, get the maximum fixed sizes and
            // maximum proportions
            List<double> columnFixedWidths = new List<double>();
            List<double> rowFixedHeights = new List<double>();
            List<double> columnMaxProportions = new List<double>();
            List<double> rowMaxProportions = new List<double>();
            foreach (UIElement child in InternalChildren)
            {
                int row = GetRow(child);
                int column = GetColumn(child);
                double vertProportion = GetVerticalProportion(child);
                double horzProportion = GetHorizontalProportion(child);

                while ((row + 1) > rowFixedHeights.Count)
                {
                    rowFixedHeights.Add(0.0);
                    rowMaxProportions.Add(0.0);
                }
                while ((column + 1) > columnFixedWidths.Count)
                {
                    columnFixedWidths.Add(0.0);
                    columnMaxProportions.Add(0.0);
                }
                if (vertProportion == 0.0)
                {
                    rowFixedHeights[row] = Math.Max(child.DesiredSize.Height, rowFixedHeights[row]);
                }
                else
                {
                    rowMaxProportions[row] = Math.Max(vertProportion, rowMaxProportions[row]);
                }
                if (horzProportion == 0.0)
                {
                    columnFixedWidths[column] = Math.Max(child.DesiredSize.Width, columnFixedWidths[column]);
                }
                else
                {
                    columnMaxProportions[column] = Math.Max(horzProportion, columnMaxProportions[column]);
                }
            }

            // Now figure out the total proportions,
            // total fixed size, and variable size
            double totalRowProporition = 0;
            foreach (double rowMaxProportion in rowMaxProportions)
                totalRowProporition += rowMaxProportion;
            double totalColumnProportion = 0;
            foreach (double columnMaxProportion in columnMaxProportions)
                totalColumnProportion += columnMaxProportion;
            double fixedHeight = 0.0;
            for (int i = 0; i < rowFixedHeights.Count; i++)
            {
                if (rowMaxProportions[i] == 0.0) { fixedHeight += rowFixedHeights[i]; }
            }
            double fixedWidth = 0.0;
            for (int i = 0; i < columnFixedWidths.Count; i++)
            {
                if (columnMaxProportions[i] == 0.0) { fixedWidth += columnFixedWidths[i]; }
            }

            double variableHeight = Math.Max(finalSize.Height - fixedHeight, 0);
            double variableWidth = Math.Max(finalSize.Width - fixedWidth, 0);

            List<double> rowHeights = new List<double>(rowFixedHeights.Count);
            List<double> columnWidths = new List<double>(columnFixedWidths.Count);
            for (int i = 0; i < rowFixedHeights.Count; i++)
            {
                if (rowMaxProportions[i] == 0.0)
                {
                    rowHeights.Add(rowFixedHeights[i]);
                }
                else
                {
                    rowHeights.Add(rowMaxProportions[i] / totalRowProporition * variableHeight);
                }
            }
            for (int i = 0; i < columnFixedWidths.Count; i++)
            {
                if (columnMaxProportions[i] == 0.0)
                {
                    columnWidths.Add(columnFixedWidths[i]);
                }
                else
                {
                    columnWidths.Add(columnMaxProportions[i] / totalColumnProportion * variableWidth);
                }
            }

            // Finally tell each child where it is and how big it is
            foreach (UIElement child in InternalChildren)
            {
                int row = GetRow(child);
                int column = GetColumn(child);
                double height = rowHeights[row];
                double width = columnWidths[column];
                double x = 0, y = 0;
                for (int i = 0; i < column; i++) { x += columnWidths[i]; }
                for (int i = 0; i < row; i++) { y += rowHeights[i]; }
                child.Arrange(new Rect(x, y, width, height));
            }

            return finalSize;
        }
    }
}