using System.Windows;
using LOLThemes.Wpf.Helpers;

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
}