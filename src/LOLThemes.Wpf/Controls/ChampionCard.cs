using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    public class ChampionCard : ContentControl
    {
        static ChampionCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ChampionCard),
                new FrameworkPropertyMetadata(typeof(ChampionCard)));
        }

        // 英雄名称
        public static readonly DependencyProperty ChampionNameProperty =
            DependencyProperty.Register(
                nameof(ChampionName),
                typeof(string),
                typeof(ChampionCard),
                new PropertyMetadata(string.Empty));

        public string ChampionName
        {
            get => (string)GetValue(ChampionNameProperty);
            set => SetValue(ChampionNameProperty, value);
        }

        // 英雄图片
        public static readonly DependencyProperty ChampionImageProperty =
            DependencyProperty.Register(
                nameof(ChampionImage),
                typeof(ImageSource),
                typeof(ChampionCard),
                new PropertyMetadata(null));

        public ImageSource ChampionImage
        {
            get => (ImageSource)GetValue(ChampionImageProperty);
            set => SetValue(ChampionImageProperty, value);
        }

        // 是否选中
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(ChampionCard),
                new PropertyMetadata(false));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }
}
