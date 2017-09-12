using System;
using System.Windows.Data;

namespace Greenbacks
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return string.Format("{0:c}", (decimal)value);
            }
            else
            {
                return string.Format("{0:c}", 0.0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal currency = 0.0M;

            if (value != null)
            {
                decimal.TryParse((string)value, out currency);
            }

            return currency;
        }
    }
}
