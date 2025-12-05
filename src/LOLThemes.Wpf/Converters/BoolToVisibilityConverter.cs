using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// 布尔值到可见性的转换器。
    /// 将布尔值转换为 <see cref="Visibility"/> 枚举值。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Button Visibility="{Binding IsEnabled, Converter={StaticResource BoolToVisibilityConverter}}"/&gt;
    /// &lt;Button Visibility="{Binding IsVisible, Converter={StaticResource BoolToVisibilityConverter}, ConverterParameter=Invert}"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 如果参数为 "Invert"，则反转转换逻辑（true 对应 Collapsed，false 对应 Visible）。
    /// 如果值不是布尔类型，默认返回 <see cref="Visibility.Collapsed"/>。
    /// </remarks>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 将布尔值转换为可见性。
        /// </summary>
        /// <param name="value">要转换的值，应为布尔类型</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数，如果为 "Invert" 则反转逻辑</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>true 对应 <see cref="Visibility.Visible"/>，false 对应 <see cref="Visibility.Collapsed"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                bool invert = parameter?.ToString() == "Invert";
                bool result = invert ? !boolValue : boolValue;
                return result ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        /// <summary>
        /// 将可见性转换回布尔值。
        /// </summary>
        /// <param name="value">要转换的值，应为 <see cref="Visibility"/> 类型</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数，如果为 "Invert" 则反转逻辑</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns><see cref="Visibility.Visible"/> 对应 true，其他值对应 false</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                bool invert = parameter?.ToString() == "Invert";
                bool result = visibility == Visibility.Visible;
                return invert ? !result : result;
            }
            return false;
        }
    }
}
