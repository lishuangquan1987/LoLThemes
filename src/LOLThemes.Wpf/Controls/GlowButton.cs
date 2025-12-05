using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 发光按钮控件，在鼠标悬停时显示发光效果。
    /// 支持自定义发光颜色和强度。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:GlowButton 
    ///     Content="点击我"
    ///     GlowColor="#C8AA6E"
    ///     GlowIntensity="20"/&gt;
    /// </code>
    /// </example>
    public class GlowButton : Button
    {
        static GlowButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(GlowButton),
                new FrameworkPropertyMetadata(typeof(GlowButton)));
        }

        /// <summary>
        /// 标识 <see cref="GlowColor"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty GlowColorProperty =
            DependencyProperty.Register(
                nameof(GlowColor),
                typeof(Color),
                typeof(GlowButton),
                new PropertyMetadata(Colors.Gold));

        /// <summary>
        /// 获取或设置发光效果的颜色。
        /// 默认值为金色（Colors.Gold）。
        /// </summary>
        public Color GlowColor
        {
            get => (Color)GetValue(GlowColorProperty);
            set => SetValue(GlowColorProperty, value);
        }

        /// <summary>
        /// 标识 <see cref="GlowIntensity"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty GlowIntensityProperty =
            DependencyProperty.Register(
                nameof(GlowIntensity),
                typeof(double),
                typeof(GlowButton),
                new PropertyMetadata(10.0));

        /// <summary>
        /// 获取或设置发光效果的强度（模糊半径）。
        /// 值越大，发光效果越明显。默认值为 10.0。
        /// </summary>
        public double GlowIntensity
        {
            get => (double)GetValue(GlowIntensityProperty);
            set => SetValue(GlowIntensityProperty, value);
        }
    }
}
