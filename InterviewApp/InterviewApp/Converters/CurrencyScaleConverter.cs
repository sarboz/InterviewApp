using System;
using System.Globalization;
using InterviewApp.Core.Models;
using InterviewApp.Data.Entities;
using Xamarin.Forms;

namespace InterviewApp.Converters
{
    public class CurrencyScaleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not CurrencyModel currency ? string.Empty : $"{currency.Scale} {currency.Name}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}