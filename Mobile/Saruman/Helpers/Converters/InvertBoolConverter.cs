using System;
using System.Globalization;
using Xamarin.Forms;

namespace Saruman.Helpers.Converters
{
    public class InvertBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool @bool)
                return !@bool;

            throw new ArgumentException($"Valor do tipo inválido: {value.GetType()}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
