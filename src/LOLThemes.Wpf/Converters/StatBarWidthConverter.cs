using System;
using System.Globalization;
using System.Windows.Data;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
    /// StatBar 宽度转换器，用于计算进度条的宽度。
    /// 根据当前值、最大值和总宽度计算进度条应该显示的宽度。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Rectangle Width="{Binding ElementName=statBar, Path=ActualWidth, Converter={StaticResource StatBarWidthConverter}, ConverterParameter={Binding Value},{Binding Maximum},{Binding ElementName=statBar, Path=ActualWidth}}"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 此转换器需要三个值（按顺序）：
    /// 1. 当前值（double）
    /// 2. 最大值（double）
    /// 3. 总宽度（double）
    /// 计算公式：宽度 = 总宽度 × (当前值 / 最大值)
    /// 如果最大值小于等于 0，返回 0.0。
    /// 百分比会被限制在 0.0 到 1.0 之间。
    /// </remarks>
    public class StatBarWidthConverter : IMultiValueConverter
    {
        /// <summary>
        /// 将值、最大值和总宽度转换为进度条宽度。
        /// </summary>
        /// <param name="values">值数组，应包含三个 double 值：[当前值, 最大值, 总宽度]</param>
        /// <param name="targetType">目标类型（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>计算后的进度条宽度（像素），如果输入无效则返回 0.0</returns>
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
        /// <param name="value">要转换的值（未使用）</param>
        /// <param name="targetTypes">目标类型数组（未使用）</param>
        /// <param name="parameter">转换参数（未使用）</param>
        /// <param name="culture">区域信息（未使用）</param>
        /// <returns>总是抛出 <see cref="NotImplementedException"/></returns>
        /// <exception cref="NotImplementedException">此方法未实现</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

