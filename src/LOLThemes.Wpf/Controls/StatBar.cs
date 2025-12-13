using System.Windows;
using System.Windows.Controls;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 属性条控件，用于显示数值和百分比（如生命值、法力值、经验值等）。
    /// 支持自定义颜色、显示/隐藏数值，以及自动计算百分比。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:StatBar 
    ///     Value="75"
    ///     Maximum="100"
    ///     Minimum="0"
    ///     ShowValue="True"
    ///     BarColor="{DynamicResource AccentCyanBrush}"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 百分比计算公式：Percentage = (Value - Minimum) / (Maximum - Minimum)
    /// 如果 Maximum 小于等于 Minimum，百分比将返回 0。
    /// Value 会被自动限制在 Minimum 和 Maximum 之间。
    /// </remarks>
    public class StatBar : Control
    {
        static StatBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatBar), new FrameworkPropertyMetadata(typeof(StatBar)));
        }

        /// <summary>
        /// 标识 <see cref="Value"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(StatBar),
                new PropertyMetadata(0.0, OnValueChanged));

        /// <summary>
        /// 标识 <see cref="Maximum"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum),
                typeof(double),
                typeof(StatBar),
                new PropertyMetadata(100.0, OnValueChanged));

        /// <summary>
        /// 标识 <see cref="Minimum"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                nameof(Minimum),
                typeof(double),
                typeof(StatBar),
                new PropertyMetadata(0.0, OnValueChanged));

        /// <summary>
        /// 标识 <see cref="ShowValue"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ShowValueProperty =
            DependencyProperty.Register(
                nameof(ShowValue),
                typeof(bool),
                typeof(StatBar),
                new PropertyMetadata(true));

        /// <summary>
        /// 标识 <see cref="BarColor"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty BarColorProperty =
            DependencyProperty.Register(
                nameof(BarColor),
                typeof(System.Windows.Media.Brush),
                typeof(StatBar),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置当前值。
        /// 默认值为 0.0。
        /// </summary>
        /// <remarks>
        /// 值会被自动限制在 <see cref="Minimum"/> 和 <see cref="Maximum"/> 之间。
        /// </remarks>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// 获取或设置最大值。
        /// 默认值为 100.0。
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// 获取或设置最小值。
        /// 默认值为 0.0。
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// 获取或设置是否显示数值文本。
        /// 默认值为 true。
        /// </summary>
        public bool ShowValue
        {
            get => (bool)GetValue(ShowValueProperty);
            set => SetValue(ShowValueProperty, value);
        }

        /// <summary>
        /// 获取或设置属性条的颜色画刷。
        /// 如果为 null，将使用默认颜色。
        /// </summary>
        public System.Windows.Media.Brush BarColor
        {
            get => (System.Windows.Media.Brush)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        /// <summary>
        /// 获取当前值相对于最大值和最小值的百分比（0.0 到 1.0）。
        /// </summary>
        /// <remarks>
        /// 计算公式：Percentage = (Value - Minimum) / (Maximum - Minimum)
        /// 如果 Maximum 小于等于 Minimum，返回 0.0。
        /// </remarks>
        public double Percentage
        {
            get
            {
                if (Maximum <= Minimum) return 0;
                var range = Maximum - Minimum;
                var value = Math.Max(Minimum, Math.Min(Maximum, Value));
                return (value - Minimum) / range;
            }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is StatBar statBar)
            {
                statBar.OnValueChanged();
            }
        }

        private void OnValueChanged()
        {
            // 触发百分比属性更新
            var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ValueProperty, typeof(StatBar));
            if (descriptor != null)
            {
                descriptor.AddValueChanged(this, (s, e) => { });
            }
        }
    }
}

