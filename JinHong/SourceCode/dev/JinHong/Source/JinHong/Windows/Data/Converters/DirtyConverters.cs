using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;
using UniGuy.Core.Extensions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Markup;
using System.Data;

namespace JinHong.Windows.Data
{
    // 此处放所有应用程序要用到但可能不可复用的转换器, 集中营

    //  TODO, 这是一个示例模板; 一般制作之后, 新增到Themes\Shared.xaml下, 即可在Xaml中用StaticResource引用
    public class DummyConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class BirthDateToAgeConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime?)
            {
                DateTime? birthDate = (DateTime?)value;
                if (birthDate.HasValue)
                    return (DateTime.Now - birthDate.Value).TotalDays / 365;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuItemNameToImageSourceConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageSource = null;
            if (value is string)
            {
                switch ((string)value)
                {
                    // 财务模块
                    case "收入日报表":
                        imageSource = "/JinHong;component/Resources/Images/财务/收入报表.png";
                        break;
                    case "未交的房租_押金明细表":
                        imageSource = "/JinHong;component/Resources/Images/财务/未交房租.png";
                        break;
                    case "已交的房租_押金明细表":
                        imageSource = "/JinHong;component/Resources/Images/财务/已交房租.png";
                        break;
                    case "未转的物业管理费明细表":
                        imageSource = "/JinHong;component/Resources/Images/财务/未转费用.png";
                        break;
                    case "已转的物业管理费明细表":
                        imageSource = "/JinHong;component/Resources/Images/财务/已转费用.png";
                        break;
                    case "月度收支明细表":
                        imageSource = "/JinHong;component/Resources/Images/财务/月度收支.png";
                        break;
                    case "半年度收支明细表":
                        imageSource = "/JinHong;component/Resources/Images/财务/半年收支.png";
                        break;
                    case "年度收支明细表":
                        imageSource = "/JinHong;component/Resources/Images/财务/年度收支.png";
                        break;
                    case "月度收支汇总表":
                        imageSource = "/JinHong;component/Resources/Images/财务/月收汇总.png";
                        break;

                    // 物业模块
                    case "人事管理":
                        imageSource = "/JinHong;component/Resources/Images/物业/人事管理.png";
                        break;
                    case "物业管理":
                        imageSource = "/JinHong;component/Resources/Images/物业/物业管理.png";
                        break;
                    case "消防管理":
                        imageSource = "/JinHong;component/Resources/Images/物业/消防管理.png";
                        break;

                    // 招商模块
                    case "楼宇区域位置图示":
                        imageSource = "/JinHong;component/Resources/Images/招商/楼宇图示.png";
                        break;
                    case "租赁户企业基本情况":
                        imageSource = "/JinHong;component/Resources/Images/招商/租户情况.png";
                        break;
                    case "租金_物业费缴纳情况记录管理业务":
                        imageSource = "/JinHong;component/Resources/Images/招商/缴费情况.png";
                        break;
                    case "租赁期限变更_续租_退租一览业务":
                        imageSource = "/JinHong;component/Resources/Images/招商/租更续退.png";
                        break;
                    case "退租流程展示表格":
                        imageSource = "/JinHong;component/Resources/Images/招商/入退租表.png";
                        break;
                    case "仓库租赁情况":
                        imageSource = "/JinHong;component/Resources/Images/招商/仓库租赁.png";
                        break;
                    case "合同管理":
                        imageSource = "/JinHong;component/Resources/Images/招商/合同管理.png";
                        break;

                    default:
                        break;

                }
            }

            return imageSource;//??value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class MenuItemNameToAbbreviationConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string abbr = null;
            if (value is string)
            {
                switch ((string)value)
                {
                    case "财务模块":
                        abbr = "财务";
                        break;
                    case "收入日报表":
                        abbr = "收入报表";
                        break;
                    case "未交的房租_押金明细表":
                        abbr = "未交房租";
                        break;
                    case "已交的房租_押金明细表":
                        abbr = "已交房租";
                        break;
                    case "未转的物业管理费明细表":
                        abbr = "未转费用";
                        break;
                    case "已转的物业管理费明细表":
                        abbr = "已转费用";
                        break;
                    case "月度收支明细表":
                        abbr = "月度收支";
                        break;
                    case "半年度收支明细表":
                        abbr = "半年收支";
                        break;
                    case "年度收支明细表":
                        abbr = "年度收支";
                        break;
                    case "月度收支汇总表":
                        abbr = "月收汇总";
                        break;

                    case "物业模块":
                        abbr = "物业";
                        break;
                    case "人事管理":
                        abbr = "人事管理";
                        break;
                    case "物业管理":
                        abbr = "物业管理";
                        break;
                    case "消防管理":
                        abbr = "消防管理";
                        break;

                    case "招商模块":
                        abbr = "招商";
                        break;
                    case "楼宇区域位置图示":
                        abbr = "楼宇图示";
                        break;
                    case "租赁户企业基本情况":
                        abbr = "租户情况";
                        break;
                    case "租金_物业费缴纳情况记录管理业务":
                        abbr = "缴纳费用";
                        break;
                    case "租赁期限变更_续租_退租一览业务":
                        abbr = "租更续退";
                        break;
                    case "退租流程展示表格":
                        abbr = "入退租表";
                        break;
                    case "仓库租赁情况":
                        abbr = "仓库租赁";
                        break;
                    case "合同管理":
                        abbr = "合同管理";
                        break;

                    default:
                        break;
                }
            }

            return abbr ?? value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DirectoryToNameConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string directory = (string)value;
                return new System.IO.DirectoryInfo(directory).Name;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class FileToNameConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string file = (string)value;
                return new System.IO.FileInfo(file).Name;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class IsNullOrEmptyConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;
            if (value is DBNull)
                return true;
            if (value is string)
                return ((string)value).Length == 0;
            return value.IsDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //  TODO
            throw new NotSupportedException();
        }
    }

    public class IsNotCurrentMonthConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int && parameter is int)
                return (int)value !=(int)parameter;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //  TODO
            throw new NotSupportedException();
        }
    }

    public class PathToImageSourceConverter : IValueConverter
    {
        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                //  TODO...有Bug, 下载了也不能更新, OnLoad拥有缓存, 文件名相同不会再次载入
                string path = (string)value;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(path);
                bi.CacheOption = BitmapCacheOption.OnLoad; //增加这一行
                bi.EndInit();
                return bi;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //  TODO
            throw new NotSupportedException();
        }
    }

    public class DataTableComputeConverter :MarkupExtension, IValueConverter
    {
        public string Expression{get;set;}
        public string Filter { get; set; }

        public DataTableComputeConverter() { }
        public DataTableComputeConverter(string expression = null, string filter = null)
        {
            this.Expression = expression;
            this.Filter = filter;
        }

        // Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataTable table = value as DataTable;
            if (table != null)
                return table.Compute(Expression, Filter);

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //  TODO
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
