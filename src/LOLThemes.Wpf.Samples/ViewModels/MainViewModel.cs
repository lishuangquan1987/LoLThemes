using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LOLThemes.Wpf.Helpers;

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
                new NavigationItem { Name = "控件展示", ViewName = "ControlShowcaseView", Icon = "🎨" },
                new NavigationItem { Name = "截图 1", ViewName = "Screenshot1View", Icon = "📷" },
                new NavigationItem { Name = "截图 2", ViewName = "Screenshot2View", Icon = "📷" },
                new NavigationItem { Name = "截图 3", ViewName = "Screenshot3View", Icon = "📷" },
                new NavigationItem { Name = "截图 4", ViewName = "Screenshot4View", Icon = "📷" },
                new NavigationItem { Name = "截图 5", ViewName = "Screenshot5View", Icon = "📷" },
                new NavigationItem { Name = "截图 6", ViewName = "Screenshot6View", Icon = "📷" }
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
                var newTheme = CurrentTheme == Theme.Dark ? Theme.Light : Theme.Dark;
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
    }
}
