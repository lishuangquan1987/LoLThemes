using System.Windows;
using System.Windows.Input;

namespace LOLThemes.Wpf.Helpers
{
    public static class WindowHelper
    {
        public static readonly DependencyProperty EnableCustomChromeProperty =
            DependencyProperty.RegisterAttached(
                "EnableCustomChrome",
                typeof(bool),
                typeof(WindowHelper),
                new PropertyMetadata(false, OnEnableCustomChromeChanged));

        public static bool GetEnableCustomChrome(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableCustomChromeProperty);
        }

        public static void SetEnableCustomChrome(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableCustomChromeProperty, value);
        }

        private static void OnEnableCustomChromeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && (bool)e.NewValue)
            {
                window.Loaded += Window_Loaded;
            }
        }

        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Window window)
            {
                window.Loaded -= Window_Loaded;
                AttachWindowEvents(window);
            }
        }

        private static void AttachWindowEvents(Window window)
        {
            // Find buttons in the template
            if (window.Template?.FindName("MinimizeButton", window) is System.Windows.Controls.Button minimizeButton)
            {
                minimizeButton.Click += (s, e) => window.WindowState = WindowState.Minimized;
            }

            if (window.Template?.FindName("MaximizeButton", window) is System.Windows.Controls.Button maximizeButton)
            {
                maximizeButton.Click += (s, e) =>
                {
                    window.WindowState = window.WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                };
            }

            if (window.Template?.FindName("CloseButton", window) is System.Windows.Controls.Button closeButton)
            {
                closeButton.Click += (s, e) => window.Close();
            }

            // Find title bar for dragging
            if (window.Template?.FindName("TitleBar", window) is UIElement titleBar)
            {
                titleBar.MouseLeftButtonDown += (s, e) =>
                {
                    if (e.ClickCount == 2)
                    {
                        window.WindowState = window.WindowState == WindowState.Maximized
                            ? WindowState.Normal
                            : WindowState.Maximized;
                    }
                    else
                    {
                        window.DragMove();
                    }
                };
            }
        }
    }
}
