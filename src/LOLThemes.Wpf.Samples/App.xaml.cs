using System;
using System.Globalization;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;
using LOLThemes.Wpf.Helpers;
using ShowMeTheXAML;

namespace LOLThemes.Wpf.Samples;

/// <summary>
/// 枚举值相等性转换器（用于显示当前选中的菜单项）
/// </summary>
public class EnumEqualityConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null || values.Length < 2)
            return false;

        // 比较两个枚举值
        return values[0]?.ToString() == values[1]?.ToString();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // 添加全局异常处理
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        
        XamlDisplay.Init();
        
        // 初始化主题管理器，默认使用暗黑主题
        ThemeManager.Initialize(Theme.Dark, this);
        
        // 初始化尺寸主题管理器，默认使用中等尺寸
        ThemeManager.InitializeSizeTheme(SizeTheme.Medium, this);
        
        base.OnStartup(e);
    }
    
    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        Console.WriteLine($"UI线程未处理异常: {e.Exception.Message}");
        Console.WriteLine($"堆栈跟踪: {e.Exception.StackTrace}");
        if (e.Exception.InnerException != null)
        {
            Console.WriteLine($"内部异常: {e.Exception.InnerException.Message}");
            Console.WriteLine($"内部异常堆栈: {e.Exception.InnerException.StackTrace}");
        }
        e.Handled = true;
        Environment.Exit(1);
    }
    
    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            Console.WriteLine($"非UI线程未处理异常: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"内部异常: {ex.InnerException.Message}");
                Console.WriteLine($"内部异常堆栈: {ex.InnerException.StackTrace}");
            }
        }
        Environment.Exit(1);
    }
    
    private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        Console.WriteLine($"任务未处理异常: {e.Exception.Message}");
        Console.WriteLine($"堆栈跟踪: {e.Exception.StackTrace}");
        if (e.Exception.InnerException != null)
        {
            Console.WriteLine($"内部异常: {e.Exception.InnerException.Message}");
            Console.WriteLine($"内部异常堆栈: {e.Exception.InnerException.StackTrace}");
        }
        e.SetObserved();
        Environment.Exit(1);
    }
}

