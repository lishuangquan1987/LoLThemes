using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// Null值到可见性的转换器。
    /// 当值为null时返回Collapsed，否则返回Visible。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Border Visibility="{Binding SkillIcon, Converter={StaticResource NullToVisibilityConverter}}"/&gt;
    /// </code>
    /// </example>
    public class NullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 将值转换为可见性。
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>如果值为null返回Collapsed，否则返回Visible</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// 不支持反向转换。
        /// </summary>
        /// <param name="value">要转换的值（未使用）</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>总是抛出NotImplementedException</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}