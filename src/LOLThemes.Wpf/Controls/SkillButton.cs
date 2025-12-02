using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    public class SkillButton : Button
    {
        static SkillButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SkillButton),
                new FrameworkPropertyMetadata(typeof(SkillButton)));
        }

        // 冷却进度 (0-1)
        public static readonly DependencyProperty CooldownProgressProperty =
            DependencyProperty.Register(
                nameof(CooldownProgress),
                typeof(double),
                typeof(SkillButton),
                new PropertyMetadata(0.0, OnCooldownProgressChanged));

        public double CooldownProgress
        {
            get => (double)GetValue(CooldownProgressProperty);
            set => SetValue(CooldownProgressProperty, value);
        }

        private static void OnCooldownProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SkillButton button && e.NewValue is double progress)
            {
                // 确保值在0-1范围内
                if (progress < 0) button.CooldownProgress = 0;
                if (progress > 1) button.CooldownProgress = 1;
            }
        }

        // 技能图标
        public static readonly DependencyProperty SkillIconProperty =
            DependencyProperty.Register(
                nameof(SkillIcon),
                typeof(ImageSource),
                typeof(SkillButton),
                new PropertyMetadata(null));

        public ImageSource SkillIcon
        {
            get => (ImageSource)GetValue(SkillIconProperty);
            set => SetValue(SkillIconProperty, value);
        }

        // 快捷键文本
        public static readonly DependencyProperty HotkeyTextProperty =
            DependencyProperty.Register(
                nameof(HotkeyText),
                typeof(string),
                typeof(SkillButton),
                new PropertyMetadata(string.Empty));

        public string HotkeyText
        {
            get => (string)GetValue(HotkeyTextProperty);
            set => SetValue(HotkeyTextProperty, value);
        }
    }
}
