using System;
using System.Globalization;
using InterviewApp.Data.Entities;
using Xamarin.Forms;

namespace InterviewApp.Converters
{
    public class CurrencyScaleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Currency currency)
                return string.Empty;
            return $"{currency.Scale} {currency.Name}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}