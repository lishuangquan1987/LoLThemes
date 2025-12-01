using System.Windows;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// 通用圆角辅助类
    /// </summary>
    public static class CornerRadiusHelper
    {
        /// <summary>
        /// 圆角附加属性
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(CornerRadiusHelper),
                new PropertyMetadata(new CornerRadius(0)));

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }
    }
}
