using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Collections;

namespace UniGuy.Controls.Panels
{
    /// <summary>
    /// From PixelLab
    /// This is the one I’m most excited about.  It’s a smarter version of a WrapPanel that tries to enforce a little more order on how it arranges its items.  It does this by maintaining a sense of columns and row height as it places the items and then snaps the items to those columns and rows.  Its also smart enough to place items into “empty” spaces that get created by extra tall or extra short items in the panel.  Think of it as WrapPanel without the jaggies.
    /// </summary>
    /// <code>
    /// Here’s some XAML:
    /*
    <c:ColumnWrapPanel MinColumnWidth="200" RowHeightIncrement="50">
      <Rectangle c:ColumnWrapPanel.ColumnSpan="3" Height="100" Fill="YellowGreen" />
      <Rectangle c:ColumnWrapPanel.ColumnSpan="2" Height="250" Fill="Orange" />
      <Rectangle c:ColumnWrapPanel.ColumnSpan="1" Height="50" Fill="LightBlue" />
      <Rectangle c:ColumnWrapPanel.ColumnSpan="1" Height="50" Fill="Tomato" />
    </c:ColumnWrapPanel>*/
    /// </code>
    /// This solves a problem that I’ve run into over and over and to which I’ve never had a good solution,  especially when laying out a data entry form or something else where you have a set of items that have dissimilar sizing requirements but which need to fill an area efficiently.  In the case of a traditional form, you could think of each color square as being a StackPanel or Grid that contains grouped elements in the form (e.g. use green for a name, orange for a set of address fields and blue and red for notes or a single checkbox).
    ///By the way, there are some obvious efficiencies to be gained in this code (mostly with how I maintain the cell table) so if anyone finds the time to clean it up, I’d love your updated and better code!
    public class ColumnWrapPanel : Panel
    {
        #region Fields

        private int _columnCount = 0;
        double _columnWidth = 0;
        private int _maxRowCount = 0;
        private int _rowCount = 0;
        private bool[,] cells;

        #endregion Fields

        #region Protected Methods

        protected override Size ArrangeOverride(Size finalSize)
        {
            double top = 0;
            double left = 0;
            double width = 0;
            double height = 0;

            double totalheight = 0;

            foreach (UIElement element in this.Children)
            {
                Point location = (Point)element.GetValue(ComputedLocationProperty);

                left = (location.X * _columnWidth);
                top = (location.Y * RowHeightIncrement);

                width = Math.Min(((int)element.GetValue(ColumnSpanProperty) * _columnWidth), finalSize.Width);
                //height = element.DesiredSize.Height;
                height = Math.Ceiling(element.DesiredSize.Height / RowHeightIncrement) * RowHeightIncrement;

                if (top + height > totalheight) totalheight = top + height;

                element.Arrange(new Rect(left, top, width, height));
            }

            return new Size(finalSize.Width, totalheight);
        }

        protected override Size MeasureOverride(Size availableSize)
        {

            // calculate the column width
            _columnWidth = this.MinColumnWidth;
            _columnCount = 1000;
            int maxRowSpan = 1;

            // compute the actual column widths (remeber that we just specified a minimum)
            if (!double.IsInfinity(availableSize.Width))
            {
                _columnCount = Math.Max((int)Math.Floor(availableSize.Width / this.MinColumnWidth), 1);
                _columnWidth = (availableSize.Width / _columnCount) + ((availableSize.Width % _columnCount) / _columnCount);
            }

            // measure each of the children at a fixed width and unlimited height
            foreach (UIElement element in this.Children)
            {
                int columnSpan = (int)element.GetValue(ColumnSpanProperty);
                Size childAvailableSize = new Size(_columnWidth * columnSpan, availableSize.Height);
                element.Measure(childAvailableSize);

                int rowSpan = (int)Math.Ceiling(element.DesiredSize.Height / this.RowHeightIncrement);
                if (rowSpan > maxRowSpan) maxRowSpan = rowSpan;
            }

            // we should now be able to perform pseudo arrange on everything
            // (which is necessary in order to return a correct size)

            // initialize cells to be the right number of columns by the total
            // number of elements * maxRowSpan (which is the maximum number of
            // rows that all elements combined could span)

            _rowCount = 0;
            _maxRowCount = maxRowSpan * this.Children.Count;
            cells = new bool[_columnCount, _maxRowCount];
            cells.Initialize();

            double top = 0;
            double height = 0;
            double totalheight = 0;

            foreach (UIElement element in this.Children)
            {
                int columnSpan = (int)element.GetValue(ColumnSpanProperty);
                int rowSpan = (int)Math.Ceiling(element.DesiredSize.Height / this.RowHeightIncrement);

                Point p = GetNextAvailabeCell(columnSpan, rowSpan);
                element.SetValue(ComputedLocationProperty, p);

                height = element.DesiredSize.Height;
                top = (p.Y * RowHeightIncrement);

                if (top + height > totalheight) totalheight = top + height;

            }

            return new Size(Math.Min(availableSize.Width, _columnCount * _columnWidth), totalheight);
        }

        #endregion Protected Methods

        #region Private Methods

        private Point GetNextAvailabeCell(int columnSpan, int rowSpan)
        {
            int adjustedColumnSpan = columnSpan;

            if (adjustedColumnSpan > _columnCount) adjustedColumnSpan = _columnCount;

            int y, x = 0;

            for (y = 0; y < _maxRowCount; y++)
            {
                for (x = 0; x < _columnCount; x++)
                {
                    if (TestCells(x, y, adjustedColumnSpan, rowSpan))
                    {
                        SetCells(x, y, adjustedColumnSpan, rowSpan);
                        _rowCount = y + rowSpan;

                        return new Point(x, y);
                    }
                }
            }

            return new Point(0, 1);
        }

        private void SetCells(int x, int y, int spanX, int spanY)
        {
            if (x + spanX > _columnCount) return;

            for (int j = y; j < y + spanY; j++)
            {
                for (int i = x; i < x + spanX; i++)
                {
                    cells[i, j] = true;
                }
            }
        }

        private bool TestCells(int x, int y, int spanX, int spanY)
        {
            if (x + spanX > _columnCount) return false;

            for (int j = y; j < y + spanY; j++)
            {
                for (int i = x; i < x + spanX; i++)
                {
                    if (cells[i, j]) return false;
                }
            }

            return true;
        }

        #endregion Private Methods

        #region MinColumnWidth

        /// <summary>
        /// MinColumnWidth Dependency Property
        /// </summary>
        public static readonly DependencyProperty MinColumnWidthProperty =
            DependencyProperty.Register("MinColumnWidth", typeof(double), typeof(ColumnWrapPanel),
                new FrameworkPropertyMetadata((double)100,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(OnMinColumnWidthChanged)));

        /// <summary>
        /// Gets or sets the MinColumnWidth property.  This dependency property 
        /// indicates ....
        /// </summary>
        public double MinColumnWidth
        {
            get { return (double)GetValue(MinColumnWidthProperty); }
            set { SetValue(MinColumnWidthProperty, value); }
        }

        /// <summary>
        /// Handles changes to the MinColumnWidth property.
        /// </summary>
        private static void OnMinColumnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnWrapPanel)d).OnMinColumnWidthChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the MinColumnWidth property.
        /// </summary>
        protected virtual void OnMinColumnWidthChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion

        #region RowHeightIncrement

        /// <summary>
        /// RowHeightIncrement Dependency Property
        /// </summary>
        public static readonly DependencyProperty RowHeightIncrementProperty =
            DependencyProperty.Register("RowHeightIncrement", typeof(double), typeof(ColumnWrapPanel),
                new FrameworkPropertyMetadata((double)20,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the RowHeightIncrement property.  This dependency property 
        /// indicates ....
        /// </summary>
        public double RowHeightIncrement
        {
            get { return (double)GetValue(RowHeightIncrementProperty); }
            set { SetValue(RowHeightIncrementProperty, value); }
        }

        #endregion

        #region ColumnSpan

        /// <summary>
        /// ColumnSpan Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ColumnSpanProperty =
            DependencyProperty.RegisterAttached("ColumnSpan", typeof(int), typeof(ColumnWrapPanel),
                new FrameworkPropertyMetadata((int)1,
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        /// <summary>
        /// Gets the ColumnSpan property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static int GetColumnSpan(DependencyObject d)
        {
            return (int)d.GetValue(ColumnSpanProperty);
        }

        /// <summary>
        /// Sets the ColumnSpan property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetColumnSpan(DependencyObject d, int value)
        {
            d.SetValue(ColumnSpanProperty, value);
        }

        #endregion

        #region ComputedLocation

        /// <summary>
        /// ComputedLocation Attached Dependency Property
        /// </summary>
        private static readonly DependencyProperty ComputedLocationProperty =
            DependencyProperty.RegisterAttached("ComputedLocation", typeof(Point), typeof(ColumnWrapPanel),
                new FrameworkPropertyMetadata((Point)new Point(),
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        /// <summary>
        /// Gets the ComputedLocation property.  This dependency property 
        /// indicates ....
        /// </summary>
        private static Point GetComputedLocation(DependencyObject d)
        {
            return (Point)d.GetValue(ComputedLocationProperty);
        }

        /// <summary>
        /// Sets the ComputedLocation property.  This dependency property 
        /// indicates ....
        /// </summary>
        private static void SetComputedLocation(DependencyObject d, Point value)
        {
            d.SetValue(ComputedLocationProperty, value);
        }

        #endregion
    }
}

