using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 技能按钮控件，用于显示英雄联盟中的技能图标和冷却状态。
    /// 支持显示技能图标、快捷键文本和冷却进度动画。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:SkillButton 
    ///     SkillIcon="{StaticResource QSkillImage}"
    ///     HotkeyText="Q"
    ///     CooldownProgress="0.5"/&gt;
    /// </code>
    /// </example>
    public class SkillButton : Button
    {
        static SkillButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SkillButton),
                new FrameworkPropertyMetadata(typeof(SkillButton)));
        }

        /// <summary>
        /// 标识 <see cref="CooldownProgress"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty CooldownProgressProperty =
            DependencyProperty.Register(
                nameof(CooldownProgress),
                typeof(double),
                typeof(SkillButton),
                new PropertyMetadata(0.0, OnCooldownProgressChanged));

        /// <summary>
        /// 获取或设置冷却进度，范围从 0.0 到 1.0。
        /// 0.0 表示技能已冷却完成，1.0 表示技能正在冷却中。
        /// </summary>
        /// <remarks>
        /// 如果设置的值超出范围，会自动限制在 0.0 到 1.0 之间。
        /// </remarks>
        public double CooldownProgress
        {
            get => (double)GetValue(CooldownProgressProperty);
            set => SetValue(CooldownProgressProperty, value);
        }

        /// <summary>
        /// 当 <see cref="CooldownProgress"/> 属性值改变时调用。
        /// 确保值在 0.0 到 1.0 的有效范围内。
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性变更事件参数</param>
        private static void OnCooldownProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SkillButton button && e.NewValue is double progress)
            {
                // 确保值在0-1范围内
                if (progress < 0) button.CooldownProgress = 0;
                if (progress > 1) button.CooldownProgress = 1;
            }
        }

        /// <summary>
        /// 标识 <see cref="SkillIcon"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty SkillIconProperty =
            DependencyProperty.Register(
                nameof(SkillIcon),
                typeof(ImageSource),
                typeof(SkillButton),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置技能图标。
        /// </summary>
        public ImageSource SkillIcon
        {
            get => (ImageSource)GetValue(SkillIconProperty);
            set => SetValue(SkillIconProperty, value);
        }

        /// <summary>
        /// 标识 <see cref="HotkeyText"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HotkeyTextProperty =
            DependencyProperty.Register(
                nameof(HotkeyText),
                typeof(string),
                typeof(SkillButton),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// 获取或设置快捷键文本（如 "Q"、"W"、"E"、"R"）。
        /// </summary>
        public string HotkeyText
        {
            get => (string)GetValue(HotkeyTextProperty);
            set => SetValue(HotkeyTextProperty, value);
        }
    }
}
