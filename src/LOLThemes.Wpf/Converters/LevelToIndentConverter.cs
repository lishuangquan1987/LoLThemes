using System;
using System.Globalization;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// 层级到缩进的转换器。
    /// 将整数层级值转换为缩进距离（以像素为单位）。
    /// 常用于树形视图或层级列表的缩进显示。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Border Margin="{Binding Level, Converter={StaticResource LevelToIndentConverter}, 0,0,0,0}"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 每个层级对应 19.0 像素的缩进。
    /// 只支持 int 类型的输入。
    /// 如果值不是 int 类型，返回 0.0。
    /// <see cref="ConvertBack"/> 方法不支持，因为缩进距离无法唯一确定层级。
    /// </remarks>
    public class LevelToIndentConverter : IValueConverter
    {
        /// <summary>
        /// 每个层级对应的缩进大小（像素）。
        /// </summary>
        private const double IndentSize = 19.0;

        /// <summary>
        /// 将层级值转换为缩进距离。
        /// </summary>
        /// <param name="value">要转换的值，应为 int 类型（层级）</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>缩进距离（像素），计算公式：层级 × 19.0。如果转换失败则返回 0.0</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int level)
            {
                return level * IndentSize;
            }
            return 0.0;
        }

        /// <summary>
        /// 不支持反向转换。
        /// </summary>
        /// <param name="value">要转换的值（未使用）</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>总是抛出 <see cref="NotSupportedException"/></returns>
        /// <exception cref="NotSupportedException">此方法不支持</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
