using System;
using System.Globalization;
using System.Windows.Data;

public class BooleanToColumnConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isStart)
        {
            Console.WriteLine(isStart ? 0 : 1);
            return isStart ? 0 : 1;
        }
        return 1;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int column)
        {
            return column == 0;
        }
        return false;
    }
}