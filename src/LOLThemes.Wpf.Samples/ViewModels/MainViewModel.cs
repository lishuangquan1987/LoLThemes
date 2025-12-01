using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    /// <summary>
    /// 主窗口视图模型
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentView = "ControlShowcaseView";

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

        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public ICommand NavigateCommand { get; }

        public MainViewModel()
        {
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
        }

        private void Navigate(string? viewName)
        {
            if (!string.IsNullOrEmpty(viewName))
            {
                CurrentView = viewName;
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
