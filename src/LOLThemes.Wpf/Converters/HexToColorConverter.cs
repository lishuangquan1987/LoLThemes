using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// 十六进制字符串到颜色的转换器
    /// </summary>
    public class HexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string hexString && !string.IsNullOrWhiteSpace(hexString))
            {
                try
                {
                    return (Color)ColorConverter.ConvertFromString(hexString);
                }
                catch
                {
                    return Colors.Transparent;
                }
            }
            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return color.ToString();
            }
            return "#00000000";
        }
    }
}
