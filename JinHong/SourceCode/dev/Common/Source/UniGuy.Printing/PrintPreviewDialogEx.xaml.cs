using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UniGuy.Printing
{
    /// <summary>
    /// PrintPreviewDialog.xaml 的交互逻辑
    /// </summary>
    /// <remarks>
    /// 参见PageRangeDocumentPaginator;
    /// 本类是为了解决PrintPreviewDialog的打印按钮弹出的PrintDialog不能选择页面范围而制作的(通过重写DocumentView的ControlTemplate, 有点Dirty了);
    /// 当然即使能够选择了页面范围, 打印的结果还总是全部页, 具体原因参考PageRangeDocumentPaginator.cs中的注释,
    /// 所以这里还要借助该类;
    /// </remarks>
    public partial class PrintPreviewDialogEx : Window
    {
        #region Dependency properties
        private static readonly DependencyProperty DocumentProperty
            = DependencyProperty.Register("Document", typeof(IDocumentPaginatorSource), typeof(PrintPreviewDialogEx));
        #endregion //   Dependency properties

        #region Properties
        /// <summary>
        /// 获得或者设置打印文档
        /// </summary>
        public IDocumentPaginatorSource Document
        {
            get { return (IDocumentPaginatorSource)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }
        #endregion //   Properties

        #region Contructors

        public PrintPreviewDialogEx()
        {
            InitializeComponent();
        }

        public PrintPreviewDialogEx(IDocumentPaginatorSource document)
            :this()
        {
            this.Document = document;
        }

        #endregion //   Constructors

        #region Methods
        #region Event handlers
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PrintDialog();

            // Allow the user to select a PageRange
            dlg.UserPageRangeEnabled = true;

            if (dlg.ShowDialog() == true)
            {
                DocumentPaginator paginator = Document.DocumentPaginator;

                if (dlg.PageRangeSelection == PageRangeSelection.UserPages)
                {
                    paginator = new PageRangeDocumentPaginator(
                                     Document.DocumentPaginator,
                                     dlg.PageRange);
                }

                dlg.PrintDocument(paginator, null);
            }

        }
        #endregion
        #endregion
    }
}
