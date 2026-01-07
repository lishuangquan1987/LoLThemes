using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LOLThemes.Wpf.Helpers;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    /// <summary>
    /// 主窗口视图模型
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentView = "ControlShowcaseView";
        private Theme _currentTheme;
        private SizeTheme _currentSizeTheme;

        public string CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public Theme CurrentTheme
        {
            get => _currentTheme;
            private set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ThemeIcon));
                    OnPropertyChanged(nameof(ThemeToolTip));
                }
            }
        }

        public SizeTheme CurrentSizeTheme
        {
            get => _currentSizeTheme;
            private set
            {
                if (_currentSizeTheme != value)
                {
                    _currentSizeTheme = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SizeIcon));
                    OnPropertyChanged(nameof(SizeToolTip));
                }
            }
        }

        /// <summary>
        /// 主题图标（Material.Icons 图标类型）
        /// </summary>
        public Material.Icons.MaterialIconKind ThemeIcon => CurrentTheme == Theme.Dark
            ? Material.Icons.MaterialIconKind.WeatherSunny
            : Material.Icons.MaterialIconKind.WeatherNight;

        /// <summary>
        /// 主题切换提示文本
        /// </summary>
        public string ThemeToolTip => CurrentTheme == Theme.Dark ? "切换到白色主题" : "切换到暗黑主题";

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
                    Name = "控件展示",
                    Icon = "🎨",
                    IsExpanded = true,
                    Children = new ObservableCollection<NavigationItem>
                    {
                        // 将ControlItems的子项转换为NavigationItems
                        new NavigationItem
                        {
                            Name = "基础控件",
                            Icon = "🎛️",
                            IsExpanded = true,
                            Children = new ObservableCollection<NavigationItem>
                            {
                                new NavigationItem
                                {
                                    Name = "基础输入控件",
                                    Icon = "⌨️",
                                    IsExpanded = true,
                                    Children = new ObservableCollection<NavigationItem>
                                    {
                                        new NavigationItem { Name = "按钮", ViewName = "ButtonShowcaseView", Icon = "🔘" },
                                        new NavigationItem { Name = "文本框", ViewName = "TextBoxShowcaseView", Icon = "📝" },
                                        new NavigationItem { Name = "密码框", ViewName = "PasswordBoxShowcaseView", Icon = "🔒" }
                                    }
                                },
                                new NavigationItem
                                {
                                    Name = "选择控件",
                                    Icon = "📋",
                                    IsExpanded = true,
                                    Children = new ObservableCollection<NavigationItem>
                                    {
                                        new NavigationItem { Name = "下拉框", ViewName = "ComboBoxShowcaseView", Icon = "📋" },
                                        new NavigationItem { Name = "复选框", ViewName = "CheckBoxShowcaseView", Icon = "☑️" },
                                        new NavigationItem { Name = "单选按钮", ViewName = "RadioButtonShowcaseView", Icon = "🔘" },
                                        new NavigationItem { Name = "切换按钮", ViewName = "ToggleButtonShowcaseView", Icon = "🔄" },
                                        new NavigationItem { Name = "滑块", ViewName = "SliderShowcaseView", Icon = "🎚️" },
                                        new NavigationItem { Name = "日历", ViewName = "CalendarShowcaseView", Icon = "📅" },
                                        new NavigationItem { Name = "日期选择器", ViewName = "DatePickerShowcaseView", Icon = "📆" }
                                    }
                                },
                                new NavigationItem
                                {
                                    Name = "列表控件",
                                    Icon = "📜",
                                    IsExpanded = true,
                                    Children = new ObservableCollection<NavigationItem>
                                    {
                                        new NavigationItem { Name = "列表框", ViewName = "ListBoxShowcaseView", Icon = "📜" },
                                        new NavigationItem { Name = "列表视图", ViewName = "ListViewShowcaseView", Icon = "📋" },
                                        new NavigationItem { Name = "树形视图", ViewName = "TreeViewShowcaseView", Icon = "🌳" },
                                        new NavigationItem { Name = "数据网格", ViewName = "DataGridShowcaseView", Icon = "📊" },
                                        new NavigationItem { Name = "富文本框", ViewName = "RichTextBoxShowcaseView", Icon = "📄" }
                                    }
                                },
                                new NavigationItem
                                {
                                    Name = "容器控件",
                                    Icon = "📦",
                                    IsExpanded = true,
                                    Children = new ObservableCollection<NavigationItem>
                                    {
                                        new NavigationItem { Name = "标签页", ViewName = "TabControlShowcaseView", Icon = "📑" },
                                        new NavigationItem { Name = "分组框", ViewName = "GroupBoxShowcaseView", Icon = "📦" },
                                        new NavigationItem { Name = "展开器", ViewName = "ExpanderShowcaseView", Icon = "📂" }
                                    }
                                },
                                new NavigationItem
                                {
                                    Name = "特殊控件",
                                    Icon = "✨",
                                    IsExpanded = true,
                                    Children = new ObservableCollection<NavigationItem>
                                    {
                                        new NavigationItem { Name = "菜单", ViewName = "MenuShowcaseView", Icon = "☰" },
                                        new NavigationItem { Name = "提示框", ViewName = "ToolTipShowcaseView", Icon = "💡" },
                                        new NavigationItem { Name = "状态栏", ViewName = "StatusBarShowcaseView", Icon = "📊" },
                                        new NavigationItem { Name = "上下文菜单", ViewName = "ContextMenuShowcaseView", Icon = "☰" }
                                    }
                                },
                                new NavigationItem
                                {
                                    Name = "游戏控件",
                                    Icon = "🎮",
                                    IsExpanded = true,
                                    Children = new ObservableCollection<NavigationItem>
                                    {
                                        new NavigationItem { Name = "发光按钮", ViewName = "GlowButtonShowcaseView", Icon = "✨" },
                                        new NavigationItem { Name = "六边形按钮", ViewName = "HexagonButtonShowcaseView", Icon = "⬡" },
                                        new NavigationItem { Name = "技能按钮", ViewName = "SkillButtonShowcaseView", Icon = "⚔️" },
                                        new NavigationItem { Name = "英雄卡片", ViewName = "ChampionCardShowcaseView", Icon = "🃏" },
                                        new NavigationItem { Name = "段位徽章", ViewName = "RankBadgeShowcaseView", Icon = "🏆" },
                                        new NavigationItem { Name = "货币显示", ViewName = "CurrencyDisplayShowcaseView", Icon = "💰" },
                                        new NavigationItem { Name = "属性条", ViewName = "StatBarShowcaseView", Icon = "📊" },
                                        new NavigationItem { Name = "进度条", ViewName = "ProgressBarShowcaseView", Icon = "📊" }
                                    }
                                }
                            }
                        }
                    }
                },
                new NavigationItem
                {
                    Name = "截图",
                    Icon = "📷",
                    IsExpanded = true,
                    Children = new ObservableCollection<NavigationItem>
                    {
                        new NavigationItem { Name = "截图 1", ViewName = "Screenshot1View", Icon = "📷" },
                        new NavigationItem { Name = "截图 2", ViewName = "Screenshot2View", Icon = "📷" },
                        new NavigationItem { Name = "截图 3", ViewName = "Screenshot3View", Icon = "📷" },
                        new NavigationItem { Name = "截图 4", ViewName = "Screenshot4View", Icon = "📷" },
                        new NavigationItem { Name = "截图 5", ViewName = "Screenshot5View", Icon = "📷" },
                        new NavigationItem { Name = "截图 6", ViewName = "Screenshot6View", Icon = "📷" }
                    }
                }
            };

            NavigateCommand = new RelayCommand<string>(Navigate);
            ToggleThemeCommand = new RelayCommand(ToggleTheme);
            ToggleSizeCommand = new RelayCommand(ToggleSize);
        }

        private void Navigate(string? viewName)
        {
            if (!string.IsNullOrEmpty(viewName))
            {
                CurrentView = viewName;
            }
        }

        private void ToggleTheme()
        {
            try
            {
                var newTheme =  Theme.Dark ;
                ThemeManager.SwitchTheme(newTheme);
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// 导航项数据模型
    /// </summary>
    public class NavigationItem
    {
        public string Name { get; set; } = string.Empty;
        public string ViewName { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public ObservableCollection<NavigationItem> Children { get; set; } = new ObservableCollection<NavigationItem>();
        public bool IsGroup => !string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(ViewName);
        public bool IsExpanded { get; set; } = true;
    }
}
