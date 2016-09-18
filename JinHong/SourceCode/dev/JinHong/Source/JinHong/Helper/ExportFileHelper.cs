using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using UniGuy.Report;

namespace JinHong.Helper
{
    /// <summary>
    /// 文件导出
    /// </summary>
    public class ExportFileHelper
    {


        /// <summary>
        /// 导出xlsx
        /// </summary>
        /// <param name="table"></param>
        /// <param name="title"></param>
        public void ExportToExcel(DataTable table, string title)
        {
            if (null != table && table.Rows.Count > 0)
            {
                SaveFileDialog dialog = new SaveFileDialog { DefaultExt = ".xlsx", Filter = "Office 2007 excel file(*.xlsx)|*.xlsx" };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    ExcelExporter exporter = new ExcelExporter();
                    exporter.GenerateExcel(table, title, dialog.FileName);
                    Process.Start(dialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("没有可导出的数据!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        /// <summary>
        /// 导出pdf
        /// </summary>
        /// <param name="table"></param>
        /// <param name="title"></param>

        public void ExportToPdf(DataTable table, string title)
        {
            if (null != table && table.Rows.Count > 0)
            {
                SaveFileDialog dialog = new SaveFileDialog { DefaultExt = ".pdf", Filter = "Pdf file(*.pdf)|*.pdf" };
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    PdfExporter exporter = new PdfExporter();
                    exporter.Export2Pdf(table, title, dialog.FileName);
                    Process.Start(dialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("没有可导出的数据!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);

            }


        }
    }
}
