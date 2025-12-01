using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// 通用发光效果辅助类
    /// </summary>
    public static class GlowEffectHelper
    {
        /// <summary>
        /// 启用发光附加属性
        /// </summary>
        public static readonly DependencyProperty EnableGlowProperty =
            DependencyProperty.RegisterAttached(
                "EnableGlow",
                typeof(bool),
                typeof(GlowEffectHelper),
                new PropertyMetadata(false, OnEnableGlowChanged));

        public static bool GetEnableGlow(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableGlowProperty);
        }

        public static void SetEnableGlow(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableGlowProperty, value);
        }

        /// <summary>
        /// 发光颜色附加属性
        /// </summary>
        public static readonly DependencyProperty GlowColorProperty =
            DependencyProperty.RegisterAttached(
                "GlowColor",
                typeof(Color),
                typeof(GlowEffectHelper),
                new PropertyMetadata(Colors.Gold, OnGlowColorChanged));

        public static Color GetGlowColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(GlowColorProperty);
        }

        public static void SetGlowColor(DependencyObject obj, Color value)
        {
            obj.SetValue(GlowColorProperty, value);
        }

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

        private static void OnGlowColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetEnableGlow(d) && d is UIElement element && element.Effect is DropShadowEffect effect)
            {
                effect.Color = (Color)e.NewValue;
            }
        }
    }
}
