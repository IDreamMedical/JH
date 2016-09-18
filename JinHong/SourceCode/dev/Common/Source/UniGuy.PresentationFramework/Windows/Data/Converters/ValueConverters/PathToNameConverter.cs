using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows.Data;
using System.Globalization;

namespace UniGuy.Controls.Converters
{
    /// <summary>
    /// a.b.cf  ->  cf
    /// fd_fd_df    ->  df
    /// 用于完整路径取文件名或类似场合
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class PathToNameConverter:MarkupExtension, IValueConverter
    {
        #region Fields
        /// <summary>
        /// 字符串分割符(默认为".")
        /// </summary>
        private string separator=".";
        #endregion

        #region Properties
        /// <summary>
        /// 获得或者设置字符串分割符
        /// </summary>
        public string Separator
        {
            get { return separator; }
            set { separator = value; }
        }
        #endregion

        #region Ctor
        public PathToNameConverter() { }
        public PathToNameConverter(string separator)
        {
            this.separator = separator;
        }
        #endregion

        #region IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = value as string;

            if (source != null)
            {
                //  从source中寻找separator，找不到则返回整个字符串，否则返回最后一个separator后的字符串
                int index = source.LastIndexOf(separator);
                return source.Substring(index < 0 ? 0 : index+separator.Length);
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region Overrides
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //  爽快
            return this;
        }
        #endregion

        /*
         <TextBox x:Name="tb" DockPanel.Dock="Top"/>
        <TextBlock DockPanel.Dock="Top" Text="{Binding Path=Text, ElementName=tb, Converter={pc:PathToNameConverter}}"/>
        <TextBlock DockPanel.Dock="Top" Text="{Binding Path=Text, ElementName=tb, Converter={pc:PathToNameConverter _}}"/>
         * result:
         * a.b_c
         * b_c
         * c
         */
    }
}
