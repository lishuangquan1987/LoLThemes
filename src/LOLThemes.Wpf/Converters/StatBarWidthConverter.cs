using System;
using System.Globalization;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// StatBar 宽度转换器，用于计算进度条的宽度。
    /// </summary>
    public class StatBarWidthConverter : IMultiValueConverter
    {
        /// <summary>
        /// 将值、最大值和总宽度转换为进度条宽度。
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 3)
                return 0.0;

            if (values[0] is double value && 
                values[1] is double maximum && 
                values[2] is double totalWidth)
            {
                if (maximum <= 0) return 0.0;
                var percentage = Math.Max(0, Math.Min(1, value / maximum));
                return totalWidth * percentage;
            }

            return 0.0;
        }

        /// <summary>
        /// 不支持反向转换。
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

