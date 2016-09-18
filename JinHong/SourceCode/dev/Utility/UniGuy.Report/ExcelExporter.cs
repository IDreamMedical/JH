using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using System.Data;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using OfficeOpenXml.Drawing.Chart;
using System.IO;

namespace UniGuy.Report
{
    public class ExcelExporter
    {
        public ExcelExporter() { }

        #region 1
        /// <summary>
        /// Exports Data from Gridview  to Excel 2007/2010/2013 format
        /// </summary>
        /// <param name="title">Title to be shown on Top of Exported Excel File</param>
        /// <param name="headerBackgroundColor">Background Color of Title</param>
        /// <param name="headerForeColor">Fore Color of Title</param>
        /// <param name="headerFont">Font size of Title</param>
        /// <param name="dateRange">Specify if Date Range is to be shown or not.</param>
        /// <param name="fromDate">Value to be stored in From Date of Date Range</param>
        /// <param name="toDate">Value to be stored in To Date of Date Range</param>
        /// <param name="dateRangeBackgroundColor">Background Color of Date Range</param>
        /// <param name="dateRangeForeColor">Fore Color of Date Range</param>
        /// <param name="dateRangeFont">Font Size of Date Range</param>
        /// <param name="table">GridView Containing Data. Should not be a templated Gridview</param>
        /// <param name="columnBackgroundColor">Background Color of Columns</param>
        /// <param name="columnForeColor">Fore Color of Columns</param>
        /// <param name="sheetName">Name of Excel WorkSheet</param>
        /// <param name="fileName">Name of Excel File to be Created</param>
        public void Export2Excel(string title, XLColor headerBackgroundColor, XLColor headerForeColor, int headerFont,
            bool dateRange, string fromDate, string toDate, XLColor dateRangeBackgroundColor, XLColor dateRangeForeColor, int dateRangeFont,
            DataTable table, XLColor columnBackgroundColor, XLColor columnForeColor, string sheetName, string fileName)
        {
            if (table == null)
                throw new ArgumentNullException("table");

            //creating a new Workbook
            var wb = new XLWorkbook();
            // adding a new sheet in workbook
            var ws = wb.Worksheets.Add(sheetName);
            //adding content
            //Title
            ws.Cell("A1").Value = title;
            if (dateRange)
            {
                ws.Cell("A2").Value = "Date Range :" + fromDate + " - " + toDate;
            }
            else
            {
                ws.Cell("A2").Value = "";
            }

            //add columns
            string[] cols = new string[table.Columns.Count];
            for (int c = 0; c < table.Columns.Count; c++)
            {
                var a = table.Columns[c].ToString();
                cols[c] = table.Columns[c].ToString().Replace('_', ' ');
            }


            char startCharCols = 'A';
            int startIndexCols = 3;

            #region CreatingColumnHeaders
            for (int i = 1; i <= cols.Length; i++)
            {
                if (i == cols.Length)
                {
                    string dataCell = startCharCols.ToString() + startIndexCols.ToString();
                    ws.Cell(dataCell).Value = cols[i - 1];
                    ws.Cell(dataCell).WorksheetColumn().Width = cols[i - 1].ToString().Length + 10;
                    ws.Cell(dataCell).Style.Font.Bold = true;
                    ws.Cell(dataCell).Style.Fill.BackgroundColor = columnBackgroundColor;
                    ws.Cell(dataCell).Style.Font.FontColor = columnForeColor;
                }
                else
                {
                    string dataCell = startCharCols.ToString() + startIndexCols.ToString();
                    ws.Cell(dataCell).Value = cols[i - 1];
                    ws.Cell(dataCell).WorksheetColumn().Width = cols[i - 1].ToString().Length + 10;
                    ws.Cell(dataCell).Style.Font.Bold = true;
                    ws.Cell(dataCell).Style.Fill.BackgroundColor = columnBackgroundColor;
                    ws.Cell(dataCell).Style.Font.FontColor = columnForeColor;
                    startCharCols++;
                }
            }
            #endregion

            //Merging Header

            string range = "A1:" + startCharCols.ToString() + "1";

            ws.Range(range).Merge();
            ws.Range(range).Style.Font.FontSize = headerFont;
            ws.Range(range).Style.Font.Bold = true;

            ws.Range(range).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            ws.Range(range).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            if (headerBackgroundColor != null && headerForeColor != null)
            {
                ws.Range(range).Style.Fill.BackgroundColor = headerBackgroundColor;
                ws.Range(range).Style.Font.FontColor = headerForeColor;
            }


            //Style definitions for Date range
            range = "A2:" + startCharCols.ToString() + "2";

            ws.Range(range).Merge();
            ws.Range(range).Style.Font.FontSize = 10;
            ws.Range(range).Style.Font.Bold = true;
            ws.Range(range).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Bottom);
            ws.Range(range).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            //border definitions for Columns
            range = "A3:" + startCharCols.ToString() + "3";
            ws.Range(range).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Range(range).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Range(range).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Range(range).Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            char startCharData = 'A';
            int startIndexData = 4;

            //char startCharDataCol = char.MinValue;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {

                    string dataCell = startCharData.ToString() + startIndexData;
                    var a = table.Rows[i][j].ToString();
                    a = a.Replace("&nbsp;", " ");
                    a = a.Replace("&amp;", "&");
                    //check if value is of integer type
                    int val = 0;
                    DateTime dt = DateTime.Now;
                    if (int.TryParse(a, out val))
                    {
                        //    ws.Cell(DataCell).Style.NumberFormat.NumberFormatId = 15;
                        ws.Cell(dataCell).Value = val;
                    }
                    //check if datetime type
                    else if (DateTime.TryParse(a, out dt))
                    {
                        ws.Cell(dataCell).Value = dt.ToShortDateString();
                    }
                    ws.Cell(dataCell).SetValue(a);
                    startCharData++;
                }
                startCharData = 'A';
                startIndexData++;
            }

            char LastChar = Convert.ToChar(startCharData + table.Columns.Count - 1);
            int TotalRows = table.Rows.Count + 3;
            range = "A4:" + LastChar + TotalRows;
            ws.Range(range).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Range(range).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Range(range).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Range(range).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            //Code to save the file
            wb.SaveAs(fileName);
        }

        public static DataTable CopyGenericToDataTable<T>(IEnumerable<T> items)
        {
            var properties = typeof(T).GetProperties();
            var result = new DataTable();

            //Build the columns
            foreach (var prop in properties)
            {
                if (prop.ToString().Contains("Nullable"))
                {
                    result.Columns.Add(prop.Name, typeof(Int32));
                }
                else
                {
                    result.Columns.Add(prop.Name, prop.PropertyType);
                }
            }

            //Fill the DataTable
            foreach (var item in items)
            {
                var row = result.NewRow();

                foreach (var prop in properties)
                {
                    var itemValue = prop.GetValue(item, new object[] { });
                    row[prop.Name] = itemValue;
                }

                result.Rows.Add(row);
            }

            return result;
        }

        #endregion

        #region 2

        public void GenerateExcel(DataTable table, string title,  string fileName)
        {

            //Step 1 : Create object of ExcelPackage class and pass file path to constructor.
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                //Step 2 : Add a new worksheet to ExcelPackage object and give a suitable name
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(string.IsNullOrEmpty(table.TableName) ? "Sheet1" : table.TableName);

                //Step 3 : Start loading datatable form A1 cell of worksheet.
                worksheet.Cells["A1"].LoadFromDataTable(table, true, TableStyles.None);

                //Step 4 : (Optional) Set the file properties like title, author and subject
                package.Workbook.Properties.Title = title;
                package.Workbook.Properties.Author = "WJ";
                package.Workbook.Properties.Subject = @"None";

                //Step 5 : Save all changes to ExcelPackage object which will create Excel 2007 file.
                package.Save();
            }
        }

        public void GenerateExcelWithCharts(DataTable table, string title, string fileName)
        {
            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                //Add a new table from the given datatable and start loading table form A1 cell.
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(string.IsNullOrEmpty(table.TableName) ? "Sheet1" : table.TableName);

                //Add a new table from the given datatable
                worksheet.Cells["A1"].LoadFromDataTable(table, true, TableStyles.None);

                var tbl = worksheet.Tables[0];
                //if you want to show sum of any column then set below variable true
                tbl.ShowTotal = true;
                //we want show sum of salary and in datatable 3rd column is salary column.
                tbl.Columns[2].TotalsRowFunction = RowFunctions.Sum;

                //Set the file properties like title, author and subject
                package.Workbook.Properties.Title = title;
                package.Workbook.Properties.Author = "WJ";
                package.Workbook.Properties.Subject = @"None";

                var chart = worksheet.Drawings.AddChart("PieChart", eChartType.Pie3D);
                chart.Title.Text = "Employee Salary Chart"; // sets the charts title
                //posistion of chart on excel sheet
                chart.SetPosition(Row: 2, RowOffsetPixels: 5, Column: 3, ColumnOffsetPixels: 5);
                //Chart height and width
                chart.SetSize(PixelWidth: 320, PixelHeight: 300);
                //chart data range, first parameter is for legends and second parameter for data.
                chart.Series.Add("C2:C5", "B2:B5");
                chart.Style = eChartStyle.Style26;

                //Save all changes to excel sheet
                package.Save();
            }
        }

        #endregion;
    }
}
