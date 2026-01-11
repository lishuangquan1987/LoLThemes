using CommunityToolkit.Mvvm.ComponentModel;
using LOLThemes.Wpf.Helpers;
using LOLThemes.Wpf.Samples.Models;
using Material.Icons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LOLThemes.Wpf.Samples.ViewModels
{

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ThemeIcon))]
        [NotifyPropertyChangedFor(nameof(ThemeToolTip))]
        private Theme _currentTheme;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SizeIcon))]
        [NotifyPropertyChangedFor(nameof(SizeToolTip))]
        private SizeTheme _currentSizeTheme;

        /// <summary>
        /// 主题图标（Material.Icons 图标类型）
        /// </summary>
        public Material.Icons.MaterialIconKind ThemeIcon => CurrentTheme == Theme.Dark
            ? Material.Icons.MaterialIconKind.WeatherSunny
            : Material.Icons.MaterialIconKind.WeatherNight;

        /// <summary>
        /// 主题切换提示文本
        /// </summary>
        public string ThemeToolTip => CurrentTheme == Theme.Dark ? "切换到增强版Dark主题" : "切换到暗黑主题";

        /// <summary>
        /// 是否使用V2主题
        /// </summary>
        private bool _isV2Theme = false;

        /// <summary>
        /// 尺寸图标（Material.Icons 图标类型）
        /// </summary>
        public Material.Icons.MaterialIconKind SizeIcon => CurrentSizeTheme switch
        {
            SizeTheme.Compact => Material.Icons.MaterialIconKind.FormatSize,
            SizeTheme.Medium => Material.Icons.MaterialIconKind.FormatSize,
            SizeTheme.Large => Material.Icons.MaterialIconKind.FormatSize,
            _ => Material.Icons.MaterialIconKind.FormatSize
        };

        /// <summary>
        /// 尺寸切换提示文本
        /// </summary>
        public string SizeToolTip => CurrentSizeTheme switch
        {
            SizeTheme.Compact => "当前：紧凑 → 切换到中等",
            SizeTheme.Medium => "当前：中等 → 切换到宽大",
            SizeTheme.Large => "当前：宽大 → 切换到紧凑",
            _ => "切换尺寸"
        };

        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public ICommand NavigateCommand { get; }
        public ICommand ToggleThemeCommand { get; }
        public ICommand ToggleSizeCommand { get; }

        public MainViewModel()
        {
            // 初始化当前主题和尺寸（通过属性设置器以触发通知）
            CurrentTheme = ThemeManager.CurrentTheme;
            CurrentSizeTheme = ThemeManager.CurrentSizeTheme;

            // 订阅主题和尺寸变更事件
            ThemeManager.ThemeChanged += (s, e) =>
            {
                CurrentTheme = e.NewTheme;
            };

            ThemeManager.SizeThemeChanged += (s, e) =>
            {
                CurrentSizeTheme = e.NewSizeTheme;
            };

            NavigationItems = new ObservableCollection<NavigationItem>
            {
                
                
                // 控件展示节点，包含所有控件分类
                new NavigationItem
                {
                    Name = "基础控件",
                    Icon = MaterialIconKind.FormatPaint,
                    IsExpanded = true,
                    Children = new ObservableCollection<NavigationItem>
                    {
                        new NavigationItem { Name = "按钮", ViewName = "ButtonShowcaseView", Icon = MaterialIconKind.Circle },
                        new NavigationItem { Name = "文本框", ViewName = "TextBoxShowcaseView", Icon = MaterialIconKind.FileDocumentEdit },
                        new NavigationItem { Name = "密码框", ViewName = "PasswordBoxShowcaseView", Icon = MaterialIconKind.Lock },
                        new NavigationItem { Name = "下拉框", ViewName = "ComboBoxShowcaseView", Icon = MaterialIconKind.FormatListBulleted },
                        new NavigationItem { Name = "复选框", ViewName = "CheckBoxShowcaseView", Icon = MaterialIconKind.CheckboxMarked },
                        new NavigationItem { Name = "单选按钮", ViewName = "RadioButtonShowcaseView", Icon = MaterialIconKind.Circle },
                        new NavigationItem { Name = "切换按钮", ViewName = "ToggleButtonShowcaseView", Icon = MaterialIconKind.Sync },
                        new NavigationItem { Name = "滑块", ViewName = "SliderShowcaseView", Icon = MaterialIconKind.Slider },
                        new NavigationItem { Name = "日历", ViewName = "CalendarShowcaseView", Icon = MaterialIconKind.Calendar },
                        new NavigationItem { Name = "日期选择器", ViewName = "DatePickerShowcaseView", Icon = MaterialIconKind.CalendarClock },
                        new NavigationItem { Name = "列表框", ViewName = "ListBoxShowcaseView", Icon = MaterialIconKind.FormatListBulleted },
                        new NavigationItem { Name = "列表视图", ViewName = "ListViewShowcaseView", Icon = MaterialIconKind.ViewList },
                        new NavigationItem { Name = "树形视图", ViewName = "TreeViewShowcaseView", Icon = MaterialIconKind.FileTree },
                        new NavigationItem { Name = "数据网格", ViewName = "DataGridShowcaseView", Icon = MaterialIconKind.Table },
                        new NavigationItem { Name = "富文本框", ViewName = "RichTextBoxShowcaseView", Icon = MaterialIconKind.FileDocument },
                        new NavigationItem { Name = "标签页", ViewName = "TabControlShowcaseView", Icon = MaterialIconKind.Tab },
                        new NavigationItem { Name = "分组框", ViewName = "GroupBoxShowcaseView", Icon = MaterialIconKind.Box },
                        new NavigationItem { Name = "展开器", ViewName = "ExpanderShowcaseView", Icon = MaterialIconKind.FolderOpen },
                            new NavigationItem { Name = "菜单", ViewName = "MenuShowcaseView", Icon = MaterialIconKind.Menu },
                        new NavigationItem { Name = "提示框", ViewName = "ToolTipShowcaseView", Icon = MaterialIconKind.Lightbulb },
                        new NavigationItem { Name = "状态栏", ViewName = "StatusBarShowcaseView", Icon = MaterialIconKind.Graph },
                        new NavigationItem { Name = "上下文菜单", ViewName = "ContextMenuShowcaseView", Icon = MaterialIconKind.MenuOpen },
                    }
                },
                new NavigationItem
                {
                    Name = "自定义控件",
                    Icon = MaterialIconKind.Palette,
                    IsExpanded = true,
                    Children = new ObservableCollection<NavigationItem>
                    {
                        new NavigationItem { Name = "发光按钮", ViewName = "GlowButtonShowcaseView", Icon = MaterialIconKind.Star },
                        new NavigationItem { Name = "六边形按钮", ViewName = "HexagonButtonShowcaseView", Icon = MaterialIconKind.Hexagon },
                        new NavigationItem { Name = "技能按钮", ViewName = "SkillButtonShowcaseView", Icon = MaterialIconKind.Sword },
                        new NavigationItem { Name = "英雄卡片", ViewName = "ChampionCardShowcaseView", Icon = MaterialIconKind.CardAccountDetails },
                        new NavigationItem { Name = "段位徽章", ViewName = "RankBadgeShowcaseView", Icon = MaterialIconKind.Trophy },
                        new NavigationItem { Name = "货币显示", ViewName = "CurrencyDisplayShowcaseView", Icon = MaterialIconKind.Coins },
                        new NavigationItem { Name = "属性条", ViewName = "StatBarShowcaseView", Icon = MaterialIconKind.Gauge },
                        new NavigationItem { Name = "进度条", ViewName = "ProgressBarShowcaseView", Icon = MaterialIconKind.ProgressCheck },

                    }
                },
                new NavigationItem
                {
                    Name = "截图",
                    Icon = MaterialIconKind.Camera,
                    IsExpanded = true,
                    Children = new ObservableCollection<NavigationItem>
                    {
                        new NavigationItem { Name = "截图 1", ViewName = "Screenshot1View", Icon = MaterialIconKind.Camera },
                        new NavigationItem { Name = "截图 2", ViewName = "Screenshot2View", Icon = MaterialIconKind.Camera },
                        new NavigationItem { Name = "截图 3", ViewName = "Screenshot3View", Icon = MaterialIconKind.Camera },
                        new NavigationItem { Name = "截图 4", ViewName = "Screenshot4View", Icon = MaterialIconKind.Camera },
                        new NavigationItem { Name = "截图 5", ViewName = "Screenshot5View", Icon = MaterialIconKind.Camera },
                        new NavigationItem { Name = "截图 6", ViewName = "Screenshot6View", Icon = MaterialIconKind.Camera },
                        // 英雄联盟主页面
                        new NavigationItem
                        {
                            Name = "英雄联盟主页面1",
                            ViewName = "LeagueOfLegendsMainView",
                            Icon = MaterialIconKind.Sword,
                            IsExpanded = true
                        },
                        // 英雄联盟游戏主界面
                        new NavigationItem
                        {
                            Name = "英雄联盟主页面2",
                            ViewName = "LeagueOfLegendsGameView",
                            Icon = MaterialIconKind.Sword,
                            IsExpanded = true
                        },
                    }
                }
            };

            ToggleThemeCommand = new RelayCommand(ToggleTheme);
            ToggleSizeCommand = new RelayCommand(ToggleSize);
        }



        private void ToggleTheme()
        {
            try
            {
                // 切换主题变体
                _isV2Theme = !_isV2Theme;
                ThemeManager.SwitchDarkThemeVariant(_isV2Theme);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"切换主题时发生错误：{ex.Message}\n\n{ex.StackTrace}",
                    "错误",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }

        private void ToggleSize()
        {
            try
            {
                var newSizeTheme = CurrentSizeTheme switch
                {
                    SizeTheme.Compact => SizeTheme.Medium,
                    SizeTheme.Medium => SizeTheme.Large,
                    SizeTheme.Large => SizeTheme.Compact,
                    _ => SizeTheme.Medium
                };
                ThemeManager.SwitchSizeTheme(newSizeTheme);
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"切换尺寸时发生错误：{ex.Message}\n\n{ex.StackTrace}",
                    "错误",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error);
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentView))]
        private NavigationItem? _selectedItem;

        public object? CurrentView
        {
            get
            {
                if (SelectedItem == null) return null;

                var view = $"LOLThemes.Wpf.Samples.Views.{SelectedItem.ViewName}";
                try
                {
                    var obj = Activator.CreateInstance("LOLThemes.Wpf.Samples", view);
                    return obj?.Unwrap();
                }
                catch
                {
                    return null;
                }

            }
        }

    }
}
