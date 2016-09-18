using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Markup;
using System.Xml;

namespace UniGuy.Printing
{
    public class PrintHelper
    {
        public static PageMediaSize A4 = new PageMediaSize(816, 1248);
        public static PageMediaSize Large = new PageMediaSize(2000, 4000);
        public static PageMediaSize Small = new PageMediaSize(200, 400);
        //  TODO Other PageMedicSizes
        public static void PrintPreview(Window owner, Visual visual, PageMediaSize pageMediaSize)
        {
            using (MemoryStream xpsStream = new MemoryStream())
            {
                using (Package package = Package.Open(xpsStream, FileMode.Create, FileAccess.ReadWrite))
                {
                    string packageUriString = "memorystream://data.xps";
                    Uri packageUri = new Uri(packageUriString);

                    PackageStore.AddPackage(packageUri, package);

                    XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Maximum, packageUriString);
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                    PrintTicket printTicket = new PrintTicket();
                    printTicket.PageMediaSize = pageMediaSize;
                    writer.Write(visual, printTicket);
                    FixedDocumentSequence document = xpsDocument.GetFixedDocumentSequence();
                    xpsDocument.Close();

                    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog(document);
                    printPreviewDialog.Owner = owner;
                    printPreviewDialog.ShowDialog();
                    PackageStore.RemovePackage(packageUri);
                }
            }
        }
        public static void PrintPreview(Window owner, Visual visual)
        {
            PrintPreview(owner, visual, A4);
        }

        public static void PrintPreviewFlowDocument(Window owner, FlowDocument fd, PageMediaSize pageMediaSize)
        {
            using (MemoryStream xpsStream = new MemoryStream())
            {
                using (Package package = Package.Open(xpsStream, FileMode.Create, FileAccess.ReadWrite))
                {
                    string packageUriString = "memorystream://data.xps";
                    Uri packageUri = new Uri(packageUriString);

                    PackageStore.AddPackage(packageUri, package);

                    XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Maximum, packageUriString);
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

                    PrintTicket printTicket = new PrintTicket();
                    printTicket.PageMediaSize = pageMediaSize;
                    //  Note: Make sure the Dispatcher is idle before write flowdocument to xpsDocument, or the xpsDocument's Bindings will not be evaluated!
                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                        new System.Windows.Threading.DispatcherOperationCallback(arg => null), null);
                    writer.Write(((IDocumentPaginatorSource)fd).DocumentPaginator, printTicket);
                    FixedDocumentSequence document = xpsDocument.GetFixedDocumentSequence();
                    xpsDocument.Close();

                    PrintPreviewDialogEx printPreviewDialog = new PrintPreviewDialogEx(document);
                    printPreviewDialog.Owner = owner;
                    printPreviewDialog.ShowDialog();
                    PackageStore.RemovePackage(packageUri);
                }
            }
        }

        public static void PrintPreviewFlowDocument(Window owner, FlowDocument fd)
        {
            PrintPreviewFlowDocument(owner, fd, Small);
        }

        //Directly find the element in the visual tree by name
        public static Visual GetDescendantByName(Visual element, string name)
        {
            if (element == null) return null;
            if (element is FrameworkElement
                && (element as FrameworkElement).Name == name) return element;
            Visual result = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                result = GetDescendantByName(visual, name);
                if (result != null)
                    break;
            }
            return result;
        }
        //GetDescendantByType
        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();
            for (int i = 0;
                i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }
        //GetAncestorByType
        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);
        }
        //enumerate all the visual tree
        public void EnumVisual(Visual myVisual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);
                Console.WriteLine(childVisual.ToString());
                // Do processing of the child visual object.
                // Enumerate children of the child visual object.
                EnumVisual(childVisual);
            }
        }

        //public static void PrintPreviewFlowDocument(Window owner, Visual visual, PageMediaSize pageMediaSize)
        //{
        //    PrintPreview(owner, PreFindFlowDocument(visual), pageMediaSize);
        //}

        /// <summary>
        /// This will take our FlowDocument XAML and convert it into a DocumentPaginator
        /// </summary>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        public static IDocumentPaginatorSource RenderFlowDocumentTemplate(string templatePath)
        {
            string rawXamlText = "";

            //Create a StreamReader that will read from the document template.  
            using (StreamReader streamReader = File.OpenText(templatePath))
            {
                rawXamlText = streamReader.ReadToEnd();
            }

            //Use the XAML reader to create a FlowDocument from the XAML string.  
            FlowDocument document = XamlReader.Load(new XmlTextReader(new StringReader(rawXamlText))) as FlowDocument;
            return document;
        }

        /// <summary>
        /// create a method that will open the standard print dialog and return to us a valid printer
        /// </summary>
        /// <returns></returns>
        public static PrintDialog GetPrintDialog()
        {
            PrintDialog printDialog = null;

            // Create a Print dialog.
            PrintDialog dlg = new PrintDialog();

            // Show the printer dialog.  If the return is "true",
            // the user made a valid selection and clicked "Ok".
            if (dlg.ShowDialog() == true)
                printDialog = dlg;  // return the dialog the user selections.
            return printDialog;
        }

        /// <summary>
        /// create a method that will write an XPS document
        /// </summary>
        /// <param name="xpsDocumentWriter"></param>
        /// <param name="document"></param>
        private static void PrintDocumentPaginator(XpsDocumentWriter xpsDocumentWriter, DocumentPaginator document)
        {
            xpsDocumentWriter.Write(document);
        }

        /// <summary>
        /// create a pair of methods that will get and print the FlowDocument
        /// </summary>
        /// <param name="printQueue"></param>
        /// <returns></returns>
        private static XpsDocumentWriter GetPrintXpsDocumentWriter(PrintQueue printQueue)
        {
            XpsDocumentWriter xpsWriter = PrintQueue.CreateXpsDocumentWriter(printQueue);
            return xpsWriter;
        }

        /// <summary>
        /// create a pair of methods that will get and print the FlowDocument
        /// </summary>
        /// <param name="printQueue"></param>
        /// <param name="document"></param>
        public static void PrintFlowDocument(PrintQueue printQueue, DocumentPaginator document)
        {
            XpsDocumentWriter xpsDocumentWriter = GetPrintXpsDocumentWriter(printQueue);
            PrintDocumentPaginator(xpsDocumentWriter, document);
        }

        /// <summary>
        /// 直接打印流文档
        /// </summary>
        /// <param name="flowDocument"></param>
        public static void PrintFlowDocument(FlowDocument flowDocument)
        {
            PrintDialog pd = new PrintDialog();

            //PrintFlowDocument(pd.PrintQueue, ((IDocumentPaginatorSource)flowDocument).DocumentPaginator);

            if (pd.ShowDialog().GetValueOrDefault())
            {
                pd.PrintDocument(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, "");
            }
        }

        /// <summary>
        /// 直接打印流文档(支持选择范围)
        /// </summary>
        /// <param name="flowDocument"></param>
        public static void PrintFlowDocumentEx(FlowDocument flowDocument)
        {
            PrintDialog pd = new PrintDialog { UserPageRangeEnabled = true };

            if (pd.ShowDialog().GetValueOrDefault())
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                if(pd.PageRangeSelection == PageRangeSelection.UserPages)
                    paginator = new PageRangeDocumentPaginator(paginator, pd.PageRange);

                pd.PrintDocument(paginator, null);
            }
        }

        /// <summary>
        /// 复制流文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static FlowDocument CopyFlowDocument(FlowDocument document)
        {
            MemoryStream s = new MemoryStream();
            TextRange source = new TextRange(document.ContentStart, document.ContentEnd);
            source.Save(s, DataFormats.Xaml, true);
            //FileStream fs = new FileStream(@"D:\1.xaml", FileMode.OpenOrCreate);
            //source.Save(fs, DataFormats.Xaml, true);
            FlowDocument copy = new FlowDocument();
            TextRange dest = new TextRange(copy.ContentStart, copy.ContentEnd);
            dest.Load(s, DataFormats.Xaml);
            return copy;
        }

        /// <summary>
        /// 复制流文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static FlowDocument CopyFlowDocument2(FlowDocument document)
        {
            string s = XamlWriter.Save(document);
            FlowDocument copy = (FlowDocument)XamlReader.Parse(s);
            return copy;
        }

        /// <summary>
        /// 直接打印复制的流文档
        /// </summary>
        /// <param name="document"></param>
        public static void PrintFlowDocument2(FlowDocument document)
        {
            //  Clone the source document's content into a new FlowDocument.
            //  This is because the pagination for the printer needs to be
            //  done differently than the pagination for the displayed page.
            //  We print the copy, rather that the orinal FlowDocument.
            MemoryStream s = new MemoryStream();
            TextRange source = new TextRange(document.ContentStart, document.ContentEnd);
            source.Save(s, DataFormats.Xaml, true);
            FlowDocument copy = new FlowDocument();
            TextRange dest = new TextRange(copy.ContentStart, copy.ContentEnd);
            dest.Load(s, DataFormats.Xaml);

            //  Create a XpsDocumentWriter object, implicitly opening a Window common print dialog,
            //  and allowing the user to select a printer.

            //  get information about the dimensions of the selected print+media.
            PrintDocumentImageableArea ia = null;
            XpsDocumentWriter docWriter = PrintQueue.CreateXpsDocumentWriter(ref ia);

            if (docWriter != null && ia != null)
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)copy).DocumentPaginator;

                //  Change the PageSize and PagePadding for the document to match the CanvasSize for the printer device.
                paginator.PageSize = new Size(ia.MediaSizeWidth, ia.MediaSizeHeight);
                Thickness t = new Thickness(72);    
                // copy.PagePadding;
                copy.PagePadding = new Thickness(
                    Math.Max(ia.OriginWidth, t.Left),
                    Math.Max(ia.OriginHeight, t.Top),
                    Math.Max(ia.MediaSizeWidth - (ia.OriginWidth + ia.ExtentWidth), t.Right),
                    Math.Max(ia.MediaSizeHeight - (ia.OriginHeight + ia.ExtentHeight), t.Bottom));

                copy.ColumnWidth = double.PositiveInfinity;
                //  copy.PageWidth = 528;
                //  allow the page to be the natural with of the output device

                //  Send content to the printer.
                docWriter.Write(paginator);
            }
        }

        /// <summary>
        /// 先对流文档复制,同时设置列宽,然后直接打印
        /// </summary>
        /// <param name="document"></param>
        /// <param name="columnWidth"></param>
        public static void PrintClonedFlowDocument(FlowDocument document, double columnWidth)
        {
            FlowDocument cloned = CopyFlowDocument(document);
            cloned.ColumnWidth = columnWidth;
            PrintFlowDocument(cloned);
        }

        /// <summary>
        /// 先对流文档复制,然后直接打印
        /// </summary>
        /// <param name="document"></param>
        public static void PrintClonedFlowDocument(FlowDocument document)
        {
            PrintFlowDocument(CopyFlowDocument(document));
        }

        /// <summary>
        /// 先对流文档复制,同时设置列宽,然后打印预览
        /// </summary>
        /// <param name="document"></param>
        /// <param name="columnWidth"></param>
        public static void PrintPreviewClonedFlowDocument(FlowDocument document, double columnWidth)
        {
            FlowDocument cloned = CopyFlowDocument(document);
            cloned.ColumnWidth = columnWidth;
            PrintPreviewFlowDocument(null, cloned);
        }

        /// <summary>
        /// 先对流文档复制,然后打印预览
        /// </summary>
        /// <param name="document"></param>
        public static void PrintPreviewClonedFlowDocument(FlowDocument document)
        {
            PrintPreviewFlowDocument(null, CopyFlowDocument(document));
        }

        /// <summary>
        /// 把DocumentPaginator转换为FixedDocumentSequence的方法;
        /// 这个方法比较重要
        /// </summary>
        /// <param name="paginator">一个强大的DocumentPaginator</param>
        /// <param name="size">页面尺寸</param>
        /// <returns>FixedDocumentSequence</returns>
        /// <remarks>
        /// 使用方法:
        /// 1. paginator不用说, paginator的Size是页面大小, 可以取或者绑定预览界面设置的页面大小(获得取PrintDialog的PrintableAreaWidth和PrintableAreaHeight),或者直接自定义
        /// 2. 返回的FixedDocumentSequence可以直接设置到DocumentViewer的Document上;
        /// 注意:DocumentViewer只能给定一个Size进行预览
        /// </remarks>
        public static FixedDocumentSequence DocumentPaginatorToFixedDocument(DocumentPaginator paginator)
        {
            string tempFileName = System.IO.Path.GetTempFileName();

            //GetTempFileName creates a file, the XpsDocument throws an exception if the file already
            //exists, so delete it. Possible race condition if someone else calls GetTempFileName
            File.Delete(tempFileName);

            using (XpsDocument xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                writer.Write(paginator);
                return xpsDocument.GetFixedDocumentSequence();
            }
        }

        /// <summary>
        /// 对paginator打印预览;
        /// pagination一般应该考虑new PrintDialog()的PrintableAreaWidth和PrintableAreaHeight;
        /// </summary>
        /// <param name="paginatior"></param>
        public static void PrintPreview(DocumentPaginator paginator)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    Package package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);
                    var uri = new Uri(@"memorystream://myXps.xps");
                    PackageStore.AddPackage(uri, package);
                    var xpsDoc = new XpsDocument(package);
                    xpsDoc.Uri = uri;
                    XpsDocument.CreateXpsDocumentWriter(xpsDoc).Write(paginator);

                    PrintPreviewDialog documentViewer = new PrintPreviewDialog { Document = xpsDoc.GetFixedDocumentSequence() };
                    documentViewer.ShowDialog();
                    PackageStore.RemovePackage(uri);
                }
            }
            catch
            {
                //  TODO
            }
        }
    }
}
