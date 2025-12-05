using System.Windows;
using System.Windows.Controls;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 属性条控件，用于显示数值和百分比（如生命值、法力值等）。
    /// </summary>
    public class StatBar : Control
    {
        static StatBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatBar), new FrameworkPropertyMetadata(typeof(StatBar)));
        }

        /// <summary>
        /// 当前值
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(StatBar),
                new PropertyMetadata(0.0, OnValueChanged));

        /// <summary>
        /// 最大值
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum),
                typeof(double),
                typeof(StatBar),
                new PropertyMetadata(100.0, OnValueChanged));

        /// <summary>
        /// 最小值
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                nameof(Minimum),
                typeof(double),
                typeof(StatBar),
                new PropertyMetadata(0.0, OnValueChanged));

        /// <summary>
        /// 是否显示数值
        /// </summary>
        public static readonly DependencyProperty ShowValueProperty =
            DependencyProperty.Register(
                nameof(ShowValue),
                typeof(bool),
                typeof(StatBar),
                new PropertyMetadata(true));

        /// <summary>
        /// 属性条颜色
        /// </summary>
        public static readonly DependencyProperty BarColorProperty =
            DependencyProperty.Register(
                nameof(BarColor),
                typeof(System.Windows.Media.Brush),
                typeof(StatBar),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置当前值
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// 获取或设置最大值
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// 获取或设置最小值
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// 获取或设置是否显示数值
        /// </summary>
        public bool ShowValue
        {
            get => (bool)GetValue(ShowValueProperty);
            set => SetValue(ShowValueProperty, value);
        }

        /// <summary>
        /// 获取或设置属性条颜色
        /// </summary>
        public System.Windows.Media.Brush BarColor
        {
            get => (System.Windows.Media.Brush)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        /// <summary>
        /// 获取百分比（0-1）
        /// </summary>
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

