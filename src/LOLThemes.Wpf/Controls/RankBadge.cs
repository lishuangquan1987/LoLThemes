using System.Windows;
using System.Windows.Controls;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 段位徽章控件，用于显示英雄联盟段位信息。
    /// </summary>
    public class RankBadge : Control
    {
        static RankBadge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RankBadge), new FrameworkPropertyMetadata(typeof(RankBadge)));
        }

        /// <summary>
        /// 段位等级（1-5，表示同一段位内的等级）
        /// </summary>
        public static readonly DependencyProperty RankLevelProperty =
            DependencyProperty.Register(
                nameof(RankLevel),
                typeof(int),
                typeof(RankBadge),
                new PropertyMetadata(1, OnRankPropertyChanged));

        /// <summary>
        /// 段位类型
        /// </summary>
        public static readonly DependencyProperty RankTypeProperty =
            DependencyProperty.Register(
                nameof(RankType),
                typeof(RankType),
                typeof(RankBadge),
                new PropertyMetadata(RankType.Iron, OnRankPropertyChanged));

        /// <summary>
        /// 段位文字
        /// </summary>
        public static readonly DependencyProperty RankTextProperty =
            DependencyProperty.Register(
                nameof(RankText),
                typeof(string),
                typeof(RankBadge),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// 获取或设置段位等级（1-5）
        /// </summary>
        public int RankLevel
        {
            get => (int)GetValue(RankLevelProperty);
            set => SetValue(RankLevelProperty, value);
        }

        /// <summary>
        /// 获取或设置段位类型
        /// </summary>
        public RankType RankType
        {
            get => (RankType)GetValue(RankTypeProperty);
            set => SetValue(RankTypeProperty, value);
        }

        /// <summary>
        /// 获取或设置段位文字
        /// </summary>
        public string RankText
        {
            get => (string)GetValue(RankTextProperty);
            set => SetValue(RankTextProperty, value);
        }

        private static void OnRankPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RankBadge badge)
            {
                badge.UpdateRankText();
            }
        }

        private void UpdateRankText()
        {
            var rankName = RankType.ToString();
            if (RankLevel > 0 && RankLevel <= 5)
            {
                RankText = $"{rankName} {RankLevel}";
            }
            else
            {
                RankText = rankName;
            }
        }
    }

    /// <summary>
    /// 段位类型枚举
    /// </summary>
    public enum RankType
    {
        /// <summary>
        /// 黑铁
        /// </summary>
        Iron,
        /// <summary>
        /// 青铜
        /// </summary>
        Bronze,
        /// <summary>
        /// 白银
        /// </summary>
        Silver,
        /// <summary>
        /// 黄金
        /// </summary>
        Gold,
        /// <summary>
        /// 铂金
        /// </summary>
        Platinum,
        /// <summary>
        /// 钻石
        /// </summary>
        Diamond,
        /// <summary>
        /// 大师
        /// </summary>
        Master,
        /// <summary>
        /// 宗师
        /// </summary>
        Grandmaster,
        /// <summary>
        /// 王者
        /// </summary>
        Challenger
    }
}

