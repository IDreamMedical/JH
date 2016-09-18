using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Shapes;

namespace UniGuy.Printing.Wj
{
    public class VisualDocumentPaginator:DocumentPaginator
    {
        private Visual _visual;
        /// <summary>
        /// 页面大小
        /// </summary>
        private Size _pageSize;
        /// <summary>
        /// 边距大小
        /// </summary>
        private Size _margin;

        private Size _visualSize;
        /// <summary>
        /// 行数
        /// </summary>
        //int _rowCount;

        private Size ContentSize
        {
            get { return new Size(_pageSize.Width - 2 * _margin.Width, _pageSize.Height - 2 * _margin.Height); }
        }
        /// <summary>
        /// 获得列数
        /// </summary>
        private int ColCount
        {
            get { return (int)Math.Ceiling(_visualSize.Width / ContentSize.Width); }
        }

        /// <summary>
        /// 获得行数
        /// </summary>
        private int RowCount
        {
            get { return (int)Math.Ceiling(_visualSize.Height / ContentSize.Height); }
        }

        public VisualDocumentPaginator(Visual visual, Size pageSize, Size margin)
        {
            _visual = visual;
            _pageSize = pageSize;
            _margin = margin;

            if (visual is FrameworkElement)
            {
                //  这里得到的时ActualWidth, 对于Image, 其和PreviousConstraint不一样, 建立VisualBrush用的是PresviousConstraint;
                _visualSize = new Size(((FrameworkElement)visual).ActualWidth, ((FrameworkElement)visual).ActualHeight);
            }
            else
                throw new Exception("Visual must be FrameworkElement.");
        }

        public VisualDocumentPaginator(Visual visual, Size pageSize)
            : this(visual, pageSize, new Size(0, 0)) { }

        public override DocumentPage GetPage(int pageNumber)
        {
            int col = pageNumber % ColCount;
            int row = pageNumber / ColCount;

            VisualBrush vb = new VisualBrush(_visual) { Stretch = Stretch.None, TileMode = TileMode.None, AlignmentX= AlignmentX.Left, AlignmentY = AlignmentY.Top, Viewbox = new Rect(col * ContentSize.Width, row * ContentSize.Height, ContentSize.Width, ContentSize.Height), ViewboxUnits = BrushMappingMode.Absolute };
            //vb.Transform = new TranslateTransform(-col * ContentSize.Width, -row * ContentSize.Height);

            Rectangle rectangle = new Rectangle
            { 
                Fill = vb, 
                Width = PageSize.Width, Height = PageSize.Height,
                Margin = new Thickness(_margin.Width, _margin.Height, _margin.Width, _margin.Height) 
            };
            rectangle.Measure(PageSize);
            rectangle.Arrange(new Rect(new Point(0, 0), PageSize));

            return new DocumentPage(rectangle);
        }

        public override bool IsPageCountValid
        {
            get { return true; }
        }

        public override int PageCount
        {
            get { return ColCount * RowCount; }
        }

        public override Size PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }
    }
}
