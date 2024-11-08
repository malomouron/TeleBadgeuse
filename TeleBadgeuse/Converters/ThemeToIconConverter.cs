using System;
using System.Globalization;
using System.Windows.Data;

namespace TeleBadgeuse.Converters
{

    public class ThemeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isDarkTheme = value is bool && (bool)value;
            return isDarkTheme ? "Resources/Icons/IconDark.ico" : "Resources/Icons/IconLight.ico";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
