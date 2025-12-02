using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LOLThemes.Wpf.Samples;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        // 添加窗口控制按钮事件处理
        this.Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // 获取窗口模板中的按钮
        var template = this.Template;
        if (template != null)
        {
            var minimizeButton = template.FindName("MinimizeButton", this) as Button;
            var maximizeButton = template.FindName("MaximizeButton", this) as Button;
            var closeButton = template.FindName("CloseButton", this) as Button;
            var titleBar = template.FindName("TitleBar", this) as Border;

            if (minimizeButton != null)
            {
                minimizeButton.Click += (s, args) => this.WindowState = WindowState.Minimized;
            }

            if (maximizeButton != null)
            {
                maximizeButton.Click += (s, args) =>
                {
                    this.WindowState = this.WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                };
            }

            if (closeButton != null)
            {
                closeButton.Click += (s, args) => this.Close();
            }

            if (titleBar != null)
            {
                titleBar.MouseLeftButtonDown += (s, args) =>
                {
                    if (args.ClickCount == 2)
                    {
                        // 双击标题栏最大化/还原
                        this.WindowState = this.WindowState == WindowState.Maximized
                            ? WindowState.Normal
                            : WindowState.Maximized;
                    }
                    else
                    {
                        // 拖动窗口
                        this.DragMove();
                    }
                };
            }
        }
    }
}