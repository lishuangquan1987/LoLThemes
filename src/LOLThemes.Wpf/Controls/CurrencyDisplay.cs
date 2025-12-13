using System.Windows;
using System.Windows.Controls;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 货币显示控件，用于显示游戏中的货币信息（蓝色精粹、点券、橙色精粹、代币等）。
    /// 根据货币类型自动应用相应的颜色和样式。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:CurrencyDisplay 
    ///     CurrencyType="BlueEssence"
    ///     Amount="5000"/&gt;
    /// &lt;controls:CurrencyDisplay 
    ///     CurrencyType="RiotPoints"
    ///     Amount="100"/&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 此控件依赖 CurrencyDisplayStyles.xaml 中定义的样式。
    /// 不同货币类型会使用不同的颜色资源：
    /// - BlueEssence: 使用 CurrencyBlueEssenceBrush（青色）
    /// - RiotPoints: 使用 CurrencyRiotPointsBrush（金色）
    /// - OrangeEssence: 使用 CurrencyOrangeEssenceBrush（橙色）
    /// - Tokens: 使用 CurrencyTokensBrush（紫色）
    /// </remarks>
    public class CurrencyDisplay : Control
    {
        static CurrencyDisplay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CurrencyDisplay), new FrameworkPropertyMetadata(typeof(CurrencyDisplay)));
        }

        /// <summary>
        /// 标识 <see cref="CurrencyType"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty CurrencyTypeProperty =
            DependencyProperty.Register(
                nameof(CurrencyType),
                typeof(CurrencyType),
                typeof(CurrencyDisplay),
                new PropertyMetadata(CurrencyType.BlueEssence));

        /// <summary>
        /// 标识 <see cref="Amount"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                nameof(Amount),
                typeof(long),
                typeof(CurrencyDisplay),
                new PropertyMetadata(0L));

        /// <summary>
        /// 获取或设置货币类型。
        /// 默认值为 <see cref="CurrencyType.BlueEssence"/>。
        /// </summary>
        public CurrencyType CurrencyType
        {
            get => (CurrencyType)GetValue(CurrencyTypeProperty);
            set => SetValue(CurrencyTypeProperty, value);
        }

        /// <summary>
        /// 获取或设置货币数量。
        /// 默认值为 0。
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

