using System;
using System.Globalization;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// 大于零的转换器。
    /// 检查数值是否大于零，返回布尔值。
    /// 常用于条件显示，例如技能冷却遮罩的可见性。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Border Visibility="{Binding CooldownProgress, Converter={StaticResource GreaterThanZeroConverter}, Converter={StaticResource BoolToVisibilityConverter}}"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 只支持 double 类型的输入。
    /// 如果值不是 double 类型，返回 false。
    /// <see cref="ConvertBack"/> 方法未实现。
    /// </remarks>
    public class GreaterThanZeroConverter : IValueConverter
    {
        /// <summary>
        /// 检查数值是否大于零。
        /// </summary>
        /// <param name="value">要检查的值，应为 double 类型</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>如果值大于零返回 true，否则返回 false</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue > 0;
            }
            return false;
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
