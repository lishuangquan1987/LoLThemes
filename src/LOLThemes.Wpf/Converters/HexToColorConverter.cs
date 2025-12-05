using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// 十六进制字符串到颜色的转换器。
    /// 将十六进制颜色字符串（如 "#C8AA6E" 或 "#FFC8AA6E"）转换为 <see cref="Color"/> 对象。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;TextBlock Foreground="{Binding ColorHex, Converter={StaticResource HexToColorConverter}}"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 如果转换失败（格式错误或空值），将返回 <see cref="Colors.Transparent"/>。
    /// </remarks>
    public class HexToColorConverter : IValueConverter
    {
        /// <summary>
        /// 将十六进制字符串转换为颜色。
        /// </summary>
        /// <param name="value">要转换的值，应为字符串类型</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>转换后的颜色对象，如果转换失败则返回透明色</returns>
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

        /// <summary>
        /// 将颜色转换回十六进制字符串。
        /// </summary>
        /// <param name="value">要转换的值，应为 <see cref="Color"/> 类型</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>颜色的十六进制字符串表示，如果转换失败则返回 "#00000000"</returns>
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
