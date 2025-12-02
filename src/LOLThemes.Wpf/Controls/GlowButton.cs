using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    public class GlowButton : Button
    {
        static GlowButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(GlowButton),
                new FrameworkPropertyMetadata(typeof(GlowButton)));
        }

        // 发光颜色
        public static readonly DependencyProperty GlowColorProperty =
            DependencyProperty.Register(
                nameof(GlowColor),
                typeof(Color),
                typeof(GlowButton),
                new PropertyMetadata(Colors.Gold));

        public Color GlowColor
        {
            get => (Color)GetValue(GlowColorProperty);
            set => SetValue(GlowColorProperty, value);
        }

        // 发光强度
        public static readonly DependencyProperty GlowIntensityProperty =
            DependencyProperty.Register(
                nameof(GlowIntensity),
                typeof(double),
                typeof(GlowButton),
                new PropertyMetadata(10.0));

        public double GlowIntensity
        {
            get => (double)GetValue(GlowIntensityProperty);
            set => SetValue(GlowIntensityProperty, value);
        }
    }
}
