using System;
using System.Globalization;
using Xamarin.Forms;

namespace Saruman.Helpers.Converters
{
    public class BoolConverter<T> : IValueConverter
    {
        public T TrueValue { get; set; }
        public T FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool @bool)
                return @bool ? TrueValue : FalseValue;

            throw new ArgumentException($"Valor do tipo inválido: {value.GetType()}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
