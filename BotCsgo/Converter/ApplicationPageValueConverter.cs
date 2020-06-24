using BotCsgo.Model;
using BotCsgo.Pages;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace BotCsgo.Converter
{
    class ApplicationPageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.MyInventory:
                    return new MyInventoryPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
