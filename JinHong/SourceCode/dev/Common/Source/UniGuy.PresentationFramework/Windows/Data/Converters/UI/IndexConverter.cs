using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Globalization;

namespace UniGuy.Windows.Data
{
    public class IndexConverter : IValueConverter
    {
        private int offset = 0;
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public IndexConverter() { }
        public IndexConverter(int offset) { this.Offset = offset; }

        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            ListBoxItem item = (ListBoxItem)value;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(item) as ItemsControl;
            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(item) + offset;
            return index.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
