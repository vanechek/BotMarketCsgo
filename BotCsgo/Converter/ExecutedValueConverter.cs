using System;
using System.Globalization;
using System.Windows.Data;

namespace BotCsgo.Converter
{
    class ExecutedValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var localDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)value).DateTime.ToLocalTime();
            return localDateTimeOffset;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
