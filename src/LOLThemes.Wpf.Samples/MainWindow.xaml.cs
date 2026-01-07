using System.Windows;
using System.Windows.Controls;
using LOLThemes.Wpf.Helpers;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        // 订阅主题变更事件，刷新UI
        ThemeManager.ThemeChanged += (s, e) =>
        {
            ThemeManager.RefreshVisualTree(this);
        };
        
        // 订阅尺寸主题变更事件，刷新UI
        ThemeManager.SizeThemeChanged += (s, e) =>
        {
            ThemeManager.RefreshVisualTree(this);
        };
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (DataContext is MainViewModel viewModel && e.NewValue is NavigationItem selectedItem)
        {
            if (!string.IsNullOrEmpty(selectedItem.ViewName))
            {
                viewModel.CurrentView = selectedItem.ViewName;
            }
        }
    }
}