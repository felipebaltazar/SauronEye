using System;
using System.Globalization;
using Saruman.Helpers.Enums;
using Xamarin.Forms;

using static Saruman.Helpers.Enums.Priority;

namespace Saruman.Helpers.Converters
{
    public class PriorityConverter<T> : IValueConverter
    {
        public T P1Value { get; set; }
        public T P2Value { get; set; }
        public T P3Value { get; set; }
        public T P4Value { get; set; }
        public T InfoValue { get; set; }
        public T UnknownValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Priority priority)
                return priority switch
                {
                    P1 => P1Value,
                    P2 => P2Value,
                    P3 => P3Value,
                    P4 => P4Value,
                    Info => InfoValue,
                    Unknown => UnknownValue,
                    _ => throw new ArgumentException($"Prioridade inválida: {priority}")
                };

            throw new ArgumentException($"Valor do tipo inválido: {value.GetType()}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
