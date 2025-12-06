using System.Configuration;
using System.Data;
using System.Windows;
using LOLThemes.Wpf.Helpers;
using ShowMeTheXAML;

namespace LOLThemes.Wpf.Samples;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        XamlDisplay.Init();
        
        // 初始化主题管理器，默认使用暗黑主题
        ThemeManager.Initialize(Theme.Dark, this);
        
        // 初始化尺寸主题管理器，默认使用中等尺寸
        ThemeManager.InitializeSizeTheme(SizeTheme.Medium, this);
        
        base.OnStartup(e);
    }
}

