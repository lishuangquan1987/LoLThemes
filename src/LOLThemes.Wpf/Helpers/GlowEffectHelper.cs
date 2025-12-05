using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// 通用发光效果辅助类。
    /// 为任何 UIElement 提供发光效果（DropShadowEffect）支持。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Border helpers:GlowEffectHelper.EnableGlow="True"
    ///         helpers:GlowEffectHelper.GlowColor="#C8AA6E"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 发光效果使用 DropShadowEffect 实现，默认模糊半径为 20，不透明度为 0.8。
    /// </remarks>
    public static class GlowEffectHelper
    {
        /// <summary>
        /// 标识 <see cref="EnableGlow"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty EnableGlowProperty =
            DependencyProperty.RegisterAttached(
                "EnableGlow",
                typeof(bool),
                typeof(GlowEffectHelper),
                new PropertyMetadata(false, OnEnableGlowChanged));

        /// <summary>
        /// 获取是否启用发光效果。
        /// </summary>
        /// <param name="obj">依赖对象（应为 UIElement）</param>
        /// <returns>如果启用发光效果返回 true，否则返回 false</returns>
        public static bool GetEnableGlow(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableGlowProperty);
        }

        /// <summary>
        /// 设置是否启用发光效果。
        /// </summary>
        /// <param name="obj">依赖对象（应为 UIElement）</param>
        /// <param name="value">是否启用</param>
        public static void SetEnableGlow(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableGlowProperty, value);
        }

        /// <summary>
        /// 标识 <see cref="GlowColor"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty GlowColorProperty =
            DependencyProperty.RegisterAttached(
                "GlowColor",
                typeof(Color),
                typeof(GlowEffectHelper),
                new PropertyMetadata(Colors.Gold, OnGlowColorChanged));

        /// <summary>
        /// 获取发光颜色。
        /// </summary>
        /// <param name="obj">依赖对象（应为 UIElement）</param>
        /// <returns>发光颜色</returns>
        public static Color GetGlowColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(GlowColorProperty);
        }

        /// <summary>
        /// 设置发光颜色。
        /// </summary>
        /// <param name="obj">依赖对象（应为 UIElement）</param>
        /// <param name="value">发光颜色</param>
        public static void SetGlowColor(DependencyObject obj, Color value)
        {
            obj.SetValue(GlowColorProperty, value);
        }

        /// <summary>
        /// 当 <see cref="EnableGlow"/> 属性值改变时调用。
        /// 根据新值启用或禁用发光效果。
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性变更事件参数</param>
        private static void OnEnableGlowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element && e.NewValue is bool enable)
            {
                if (enable)
                {
                    var color = GetGlowColor(d);
                    var dropShadow = new DropShadowEffect
                    {
                        Color = color,
                        BlurRadius = 20,
                        ShadowDepth = 0,
                        Opacity = 0.8
                    };
                    element.Effect = dropShadow;
                }
                else
                {
                    element.Effect = null;
                }
            }
        }

        /// <summary>
        /// 当 <see cref="GlowColor"/> 属性值改变时调用。
        /// 如果发光效果已启用，更新效果的颜色。
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性变更事件参数</param>
        private static void OnGlowColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetEnableGlow(d) && d is UIElement element && element.Effect is DropShadowEffect effect)
            {
                effect.Color = (Color)e.NewValue;
            }
        }
    }
}
