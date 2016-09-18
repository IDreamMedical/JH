using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Net;

namespace UniGuy.Controls.Converters
{
    public class IPAddressConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IPAddress address = value as IPAddress;
            if (address == null)
                return null;
            return address.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ip = value as string;
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
                return address;
            return IPAddress.None;
        }

        #endregion
    }
}
