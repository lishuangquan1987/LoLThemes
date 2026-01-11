using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// Window 控件的辅助类，提供自定义窗口装饰功能。
    /// 用于启用自定义窗口标题栏按钮（最小化、最大化、关闭）和拖拽功能。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;Window helpers:WindowHelper.EnableCustomChrome="True"&gt;
    ///     ...
    /// &lt;/Window&gt;
    /// </code>
    /// </example>
    /// <remarks>
    /// 使用此辅助类需要窗口模板中包含以下命名元素：
    /// - MinimizeButton: 最小化按钮
    /// - MaximizeButton: 最大化/还原按钮
    /// - CloseButton: 关闭按钮
    /// - TitleBar: 标题栏区域（用于拖拽）
    /// </remarks>
    public static class WindowHelper
    {
        static WindowHelper()
        {
            EventManager.RegisterClassHandler(typeof(Window), Window.LoadedEvent, new RoutedEventHandler(Window_Loaded));
        }
        ///// <summary>
        ///// 标识 <see cref="EnableCustomChrome"/> 附加属性。
        ///// </summary>
        //public static readonly DependencyProperty EnableCustomChromeProperty =
        //    DependencyProperty.RegisterAttached(
        //        "EnableCustomChrome",
        //        typeof(bool),
        //        typeof(WindowHelper),
        //        new PropertyMetadata(true, OnEnableCustomChromeChanged));

        /// <summary>
        /// 标识 <see cref="CustomHeaderContent"/> 附加属性。
        /// 用于在窗口标题栏右侧放置自定义内容（按钮、文本、图标等任意控件）。
        /// </summary>
        public static readonly DependencyProperty CustomHeaderContentProperty =
            DependencyProperty.RegisterAttached(
                "CustomHeaderContent",
                typeof(UIElement),
                typeof(WindowHelper),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取自定义标题栏内容。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Window）</param>
        /// <returns>自定义标题栏内容</returns>
        public static UIElement GetCustomHeaderContent(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(CustomHeaderContentProperty);
        }

        /// <summary>
        /// 设置自定义标题栏内容。
        /// </summary>
        /// <param name="obj">依赖对象（应为 Window）</param>
        /// <param name="value">自定义标题栏内容（可以是按钮、文本、图标等任意UIElement）</param>
        /// <example>
        /// <code>
        /// // 设置单个按钮
        /// var button = new Button { Content = "设置" };
        /// WindowHelper.SetCustomHeaderContent(window, button);
        /// 
        /// // 设置多个控件（使用StackPanel）
        /// var panel = new StackPanel { Orientation = Orientation.Horizontal };
        /// panel.Children.Add(new Button { Content = "按钮1" });
        /// panel.Children.Add(new Button { Content = "按钮2" });
        /// WindowHelper.SetCustomHeaderContent(window, panel);
        /// </code>
        /// </example>
        public static void SetCustomHeaderContent(DependencyObject obj, UIElement value)
        {
            obj.SetValue(CustomHeaderContentProperty, value);
        }

        ///// <summary>
        ///// 获取是否启用自定义窗口装饰。
        ///// </summary>
        ///// <param name="obj">依赖对象（应为 Window）</param>
        ///// <returns>如果启用自定义装饰返回 true，否则返回 false</returns>
        //public static bool GetEnableCustomChrome(DependencyObject obj)
        //{
        //    return (bool)obj.GetValue(EnableCustomChromeProperty);
        //}

        ///// <summary>
        ///// 设置是否启用自定义窗口装饰。
        ///// </summary>
        ///// <param name="obj">依赖对象（应为 Window）</param>
        ///// <param name="value">是否启用</param>
        //public static void SetEnableCustomChrome(DependencyObject obj, bool value)
        //{
        //    obj.SetValue(EnableCustomChromeProperty, value);
        //}

        /// <summary>
        /// 当 <see cref="EnableCustomChrome"/> 属性值改变时调用。
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性变更事件参数</param>
        private static void OnEnableCustomChromeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && (bool)e.NewValue)
            {
                window.Loaded += Window_Loaded;
            }
        }

        /// <summary>
        /// 窗口加载时的事件处理程序。
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Window window)
            {
                window.Loaded -= Window_Loaded;
                AttachWindowEvents(window);
            }
        }

        /// <summary>
        /// 为窗口附加事件处理程序。
        /// 查找模板中的按钮和标题栏，并附加相应的事件处理。
        /// </summary>
        /// <param name="window">要附加事件的窗口</param>
        /// <remarks>
        /// 如果模板中找不到相应的元素，对应功能将不会生效。
        /// </remarks>
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
