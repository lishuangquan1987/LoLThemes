using System.Windows;
using System.Windows.Controls;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 货币显示控件，用于显示游戏中的货币信息（蓝色精粹、点券等）。
    /// </summary>
    public class CurrencyDisplay : Control
    {
        static CurrencyDisplay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CurrencyDisplay), new FrameworkPropertyMetadata(typeof(CurrencyDisplay)));
        }

        /// <summary>
        /// 货币类型
        /// </summary>
        public static readonly DependencyProperty CurrencyTypeProperty =
            DependencyProperty.Register(
                nameof(CurrencyType),
                typeof(CurrencyType),
                typeof(CurrencyDisplay),
                new PropertyMetadata(CurrencyType.BlueEssence));

        /// <summary>
        /// 货币数量
        /// </summary>
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                nameof(Amount),
                typeof(long),
                typeof(CurrencyDisplay),
                new PropertyMetadata(0L));

        /// <summary>
        /// 获取或设置货币类型
        /// </summary>
        public CurrencyType CurrencyType
        {
            get => (CurrencyType)GetValue(CurrencyTypeProperty);
            set => SetValue(CurrencyTypeProperty, value);
        }

        /// <summary>
        /// 获取或设置货币数量
        /// </summary>
        public long Amount
        {
            get => (long)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }
    }

    /// <summary>
    /// 货币类型枚举
    /// </summary>
    public enum CurrencyType
    {
        /// <summary>
        /// 蓝色精粹
        /// </summary>
        BlueEssence,
        /// <summary>
        /// 点券
        /// </summary>
        RiotPoints,
        /// <summary>
        /// 橙色精粹
        /// </summary>
        OrangeEssence,
        /// <summary>
        /// 代币
        /// </summary>
        Tokens
    }
}

