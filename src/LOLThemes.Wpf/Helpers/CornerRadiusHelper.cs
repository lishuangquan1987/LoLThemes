using System.Windows;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// 通用圆角辅助类。
    /// 为任何控件提供圆角附加属性支持。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Border helpers:CornerRadiusHelper.CornerRadius="4"/&gt;
    /// </code>
    /// </example>
    public static class CornerRadiusHelper
    {
        /// <summary>
        /// 标识 <see cref="CornerRadius"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(CornerRadiusHelper),
                new PropertyMetadata(new CornerRadius(0)));

        /// <summary>
        /// 获取圆角值。
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <returns>圆角值</returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// 设置圆角值。
        /// </summary>
        /// <param name="obj">依赖对象</param>
        /// <param name="value">圆角值</param>
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }
    }
}
