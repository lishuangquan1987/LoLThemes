using System.Configuration;
using System.Data;
using System.Windows;
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
        base.OnStartup(e);
    }
}

