using System.Windows;
using System.Windows.Media;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// Button 控件的附加属性辅助类。
    /// 提供按钮形状、图标、图标位置和圆角等附加属性的支持。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Button helpers:ButtonHelper.Shape="Rounded"
    ///         helpers:ButtonHelper.Icon="{StaticResource IconImage}"
    ///         helpers:ButtonHelper.IconPlacement="Left"
    ///         helpers:ButtonHelper.CornerRadius="4"/&gt;
    /// </code>
    /// </example>
    public static class ButtonHelper
    {
        #region Shape 附加属性

        /// <summary>
        /// 标识 <see cref="Shape"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty ShapeProperty =
            DependencyProperty.RegisterAttached(
                "Shape",
                typeof(ButtonShape),
                typeof(ButtonHelper),
                new PropertyMetadata(ButtonShape.Rectangle));

        /// <summary>
        /// 获取按钮形状。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <returns>按钮形状枚举值</returns>
        public static ButtonShape GetShape(DependencyObject obj)
        {
            return (ButtonShape)obj.GetValue(ShapeProperty);
        }

        /// <summary>
        /// 设置按钮形状。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <param name="value">按钮形状枚举值</param>
        public static void SetShape(DependencyObject obj, ButtonShape value)
        {
            obj.SetValue(ShapeProperty, value);
        }

        #endregion

        #region Icon 附加属性

        /// <summary>
        /// 标识 <see cref="Icon"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(ImageSource),
                typeof(ButtonHelper),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取按钮图标。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <returns>图标图像源</returns>
        public static ImageSource GetIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(IconProperty);
        }

        /// <summary>
        /// 设置按钮图标。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <param name="value">图标图像源</param>
        public static void SetIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion


        #region IconPlacement 附加属性

        /// <summary>
        /// 标识 <see cref="IconPlacement"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty IconPlacementProperty =
            DependencyProperty.RegisterAttached(
                "IconPlacement",
                typeof(IconPlacement),
                typeof(ButtonHelper),
                new PropertyMetadata(IconPlacement.Left));

        /// <summary>
        /// 获取图标位置。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <returns>图标位置枚举值</returns>
        public static IconPlacement GetIconPlacement(DependencyObject obj)
        {
            return (IconPlacement)obj.GetValue(IconPlacementProperty);
        }

        /// <summary>
        /// 设置图标位置。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <param name="value">图标位置枚举值</param>
        public static void SetIconPlacement(DependencyObject obj, IconPlacement value)
        {
            obj.SetValue(IconPlacementProperty, value);
        }

        #endregion

        #region CornerRadius 附加属性

        /// <summary>
        /// 标识 <see cref="CornerRadius"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(ButtonHelper),
                new PropertyMetadata(new CornerRadius(0)));

        /// <summary>
        /// 获取按钮圆角。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <returns>圆角值</returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// 设置按钮圆角。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Button）</param>
        /// <param name="value">圆角值</param>
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
