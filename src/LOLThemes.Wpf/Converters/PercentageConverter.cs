using System;
using System.Globalization;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// 数值到百分比字符串的转换器
    /// </summary>
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return $"{doubleValue:P0}";
            }
            if (value is float floatValue)
            {
                return $"{floatValue:P0}";
            }
            if (value is int intValue)
            {
                return $"{intValue:P0}";
            }
            return "0%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
