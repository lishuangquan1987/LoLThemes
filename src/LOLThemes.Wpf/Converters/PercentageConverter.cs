using System;
using System.Globalization;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// 数值到百分比字符串的转换器。
    /// 将数值（double、float 或 int）转换为百分比格式的字符串（如 "50%"）。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;TextBlock Text="{Binding Progress, Converter={StaticResource PercentageConverter}}"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 支持 double、float 和 int 类型的输入。
    /// 如果值不是支持的数值类型，返回 "0%"。
    /// <see cref="ConvertBack"/> 方法未实现，因为百分比字符串无法唯一确定原始数值。
    /// </remarks>
    public class PercentageConverter : IValueConverter
    {
        /// <summary>
        /// 将数值转换为百分比字符串。
        /// </summary>
        /// <param name="value">要转换的值，应为 double、float 或 int 类型</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>百分比格式的字符串（如 "50%"），如果转换失败则返回 "0%"</returns>
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

        /// <summary>
        /// 不支持反向转换。
        /// </summary>
        /// <param name="value">要转换的值（未使用）</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>总是抛出 <see cref="NotImplementedException"/></returns>
        /// <exception cref="NotImplementedException">此方法未实现</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
