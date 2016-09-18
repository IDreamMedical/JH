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
    public partial class PrintPreviewDialog : Window
    {
        #region Dependency properties
        private static readonly DependencyProperty DocumentProperty
            = DependencyProperty.Register("Document", typeof(IDocumentPaginatorSource), typeof(PrintPreviewDialog));
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

        public PrintPreviewDialog()
        {
            InitializeComponent();
        }

        public PrintPreviewDialog(IDocumentPaginatorSource document)
            :this()
        {
            this.Document = document;
        }

        #endregion //   Constructors
    }
}
