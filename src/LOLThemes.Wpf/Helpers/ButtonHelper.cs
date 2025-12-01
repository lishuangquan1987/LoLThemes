using System.Windows;
using System.Windows.Media;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// Button 控件的附加属性辅助类
    /// </summary>
    public static class ButtonHelper
    {
        #region Shape 附加属性

        /// <summary>
        /// 按钮形状附加属性
        /// </summary>
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.RegisterAttached(
                "Shape",
                typeof(ButtonShape),
                typeof(ButtonHelper),
                new PropertyMetadata(ButtonShape.Rectangle));

        public static ButtonShape GetShape(DependencyObject obj)
        {
            return (ButtonShape)obj.GetValue(ShapeProperty);
        }

        public static void SetShape(DependencyObject obj, ButtonShape value)
        {
            obj.SetValue(ShapeProperty, value);
        }

        #endregion

        #region Icon 附加属性

        /// <summary>
        /// 按钮图标附加属性
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(ImageSource),
                typeof(ButtonHelper),
                new PropertyMetadata(null));

        public static ImageSource GetIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion


        #region IconPlacement 附加属性

        /// <summary>
        /// 图标位置附加属性
        /// </summary>
        public static readonly DependencyProperty IconPlacementProperty =
            DependencyProperty.RegisterAttached(
                "IconPlacement",
                typeof(IconPlacement),
                typeof(ButtonHelper),
                new PropertyMetadata(IconPlacement.Left));

        public static IconPlacement GetIconPlacement(DependencyObject obj)
        {
            return (IconPlacement)obj.GetValue(IconPlacementProperty);
        }

        public static void SetIconPlacement(DependencyObject obj, IconPlacement value)
        {
            obj.SetValue(IconPlacementProperty, value);
        }

        #endregion

        #region CornerRadius 附加属性

        /// <summary>
        /// 按钮圆角附加属性
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(ButtonHelper),
                new PropertyMetadata(new CornerRadius(0)));

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        #endregion
    }

    /// <summary>
    /// 按钮形状枚举
    /// </summary>
    public enum ButtonShape
    {
        /// <summary>
        /// 矩形
        /// </summary>
        Rectangle,

        /// <summary>
        /// 圆角矩形
        /// </summary>
        Rounded,

        /// <summary>
        /// 圆形
        /// </summary>
        Circle,

        /// <summary>
        /// 六边形
        /// </summary>
        Hexagon
    }

    /// <summary>
    /// 图标位置枚举
    /// </summary>
    public enum IconPlacement
    {
        /// <summary>
        /// 左侧
        /// </summary>
        Left,

        /// <summary>
        /// 右侧
        /// </summary>
        Right,

        /// <summary>
        /// 顶部
        /// </summary>
        Top,

        /// <summary>
        /// 底部
        /// </summary>
        Bottom
    }
}
