using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace UniGuy.Printing
{
    /// <summary>
    /// Encapsulates a DocumentPaginator and allows
    /// to paginate just some specific pages (a "PageRange")
    /// of the encapsulated DocumentPaginator
    ///  (c) Thomas Claudius Huber 2010
    ///      http://www.thomasclaudiushuber.com
    /// </summary>
    /// <remarks>
    /// 问题描述:
    /// wpf中的PrintDialog不管选择全部还是指定某些页的范围, 打印出来的都是全部页面;
    /// 也就是PrintDialog只是允许设置打印页面范围, 但是没有具体打印的逻辑;
    /// 解决方案:
    /// The first thing you have to know is that the pages used for the output are created by a DocumentPaginator. This was passed to the PrintDocument-Method of the PrintDialog some lines above. The DocumentPaginator-class itself is abstract. Beside some members it defines a Method GetPage(int pageNumber) that returns the DocumentPage for the passed in pageNumber. Now the idea is to create a wrapper-class for the DocumentPaginator that is itself also of type DocumentPaginator. And now guys, what pattern is that?  Doesn’t matter. 
    /// So we create a DocumentPaginator and let’s call it PageRangeDocumentPaginator. It encapsulates a DocumentPaginator and can return a specific (page)range of the encapsulated DocumentPaginator. Therefore the constructor takes two parameters: a DocumentPaginator to encapsulate and a PageRange-object that contains PageFrom and PageTo-Properties, both of type int. The PageRange-structure exists in the Namespace System.Windows.Controls.
    /// The PageRangeDocumentPaginator-class is listed below. In the constructor the passed in DocumentPaginator is stored in the _paginator-field. The pageRange-values are stored in the fields _startIndex and _endIndex. As the PageRange starts with 1, we substract 1 to get the zero-based index. In the final line of the constructor the _endIndex is adjusted. If the user enters a higher pageNumber than the Document contains, _endIndex will point to the index of the last page. 
    /// 同时请参考PrintPreviewDialogEx的实现; 默认DocumentViewer的打印按钮的实现是封装好的, 并且选定页面范围的RadioButton不可用, 所以需要重新定义模板;
    /// </remarks>
    public class PageRangeDocumentPaginator : DocumentPaginator
    {
        private int _startIndex;
        private int _endIndex;
        private DocumentPaginator _paginator;
        public PageRangeDocumentPaginator(
          DocumentPaginator paginator,
          PageRange pageRange)
        {
            _startIndex = pageRange.PageFrom - 1;
            _endIndex = pageRange.PageTo - 1;
            _paginator = paginator;

            // Adjust the _endIndex
            if (_paginator.IsPageCountValid)
                _endIndex = Math.Min(_endIndex, _paginator.PageCount - 1);
        }

        /// <remarks>
        /// 这个方法在打印到Xps文档的时候报"FixedPage cannot contain another FixedPage"错误, 调整为下面的方法
        /// <see cref="http://social.msdn.microsoft.com/Forums/en/wpf/thread/841e804b-9130-4476-8709-0d2854c11582"/>
        /// </remarks>
        /*
        public override DocumentPage GetPage(int pageNumber)
        {
            // Just return the page from the original
            // paginator by using the "startIndex"
            return _paginator.GetPage(pageNumber + _startIndex);
        }*/

        public override DocumentPage GetPage(int pageNumber)
        {
            var page = _paginator.GetPage(pageNumber + _startIndex);

            // Create a new ContainerVisual as a new parent for page children
            var cv = new ContainerVisual();
            if (page.Visual is FixedPage)
            {
                foreach (var child in ((FixedPage)page.Visual).Children)
                {
                    // Make a shallow clone of the child using reflection
                    var childClone = (UIElement)child.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(child, null);

                    // Setting the parent of the cloned child to the created ContainerVisual by using Reflection.
                    // WARNING: If we use Add and Remove methods on the FixedPage.Children, for some reason it will
                    //          throw an exception concerning event handlers after the printing job has finished.
                    var parentField = childClone.GetType().GetField("_parent",
                                                                    BindingFlags.Instance | BindingFlags.NonPublic);
                    if (parentField != null)
                    {
                        parentField.SetValue(childClone, null);
                        cv.Children.Add(childClone);
                    }
                }

                return new DocumentPage(cv, page.Size, page.BleedBox, page.ContentBox);
            }

            return page;
        }

        public override bool IsPageCountValid
        {
            get { return true; }
        }

        public override int PageCount
        {
            get
            {
                //  不知道什么原因, 在使用实际打印机的时候, _paginator.PageCount返回0而不是真实值, 所以这里先注释掉;    //  WJ
                /*
                if (_startIndex > _paginator.PageCount - 1)
                    return 0;
                if (_startIndex > _endIndex)
                    return 0;
                 */

                return _endIndex - _startIndex + 1;
            }
        }

        public override Size PageSize
        {
            get { return _paginator.PageSize; }
            set { _paginator.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return _paginator.Source; }
        }
    }
}
