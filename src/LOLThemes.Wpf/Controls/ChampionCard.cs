using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 英雄卡片控件，用于显示英雄联盟中的英雄信息。
    /// 包含英雄图片、名称和选中状态。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:ChampionCard 
    ///     ChampionName="亚索"
    ///     ChampionImage="{StaticResource YasuoImage}"
    ///     IsSelected="True"/&gt;
    /// </code>
    /// </example>
    public class ChampionCard : ContentControl
    {
        static ChampionCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ChampionCard),
                new FrameworkPropertyMetadata(typeof(ChampionCard)));
        }

        /// <summary>
        /// 标识 <see cref="ChampionName"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ChampionNameProperty =
            DependencyProperty.Register(
                nameof(ChampionName),
                typeof(string),
                typeof(ChampionCard),
                new PropertyMetadata(string.Empty));

        /// <summary>
        /// 获取或设置英雄名称。
        /// </summary>
        public string ChampionName
        {
            get => (string)GetValue(ChampionNameProperty);
            set => SetValue(ChampionNameProperty, value);
        }

        /// <summary>
        /// 标识 <see cref="ChampionImage"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ChampionImageProperty =
            DependencyProperty.Register(
                nameof(ChampionImage),
                typeof(ImageSource),
                typeof(ChampionCard),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取或设置英雄图片。
        /// </summary>
        public ImageSource ChampionImage
        {
            get => (ImageSource)GetValue(ChampionImageProperty);
            set => SetValue(ChampionImageProperty, value);
        }

        /// <summary>
        /// 标识 <see cref="IsSelected"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(ChampionCard),
                new PropertyMetadata(false));

        /// <summary>
        /// 获取或设置是否选中该英雄卡片。
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }
}
