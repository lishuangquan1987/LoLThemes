using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// TabControl 控件的附加属性辅助类。
    /// 提供下划线流动动画效果。
    /// </summary>
    public static class TabControlHelper
    {
        /// <summary>
        /// 标识是否启用下划线动画的附加属性。
        /// </summary>
        public static readonly DependencyProperty EnableIndicatorAnimationProperty =
            DependencyProperty.RegisterAttached(
                "EnableIndicatorAnimation",
                typeof(bool),
                typeof(TabControlHelper),
                new PropertyMetadata(false, OnEnableIndicatorAnimationChanged));

        /// <summary>
        /// 获取是否启用下划线动画。
        /// </summary>
        public static bool GetEnableIndicatorAnimation(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableIndicatorAnimationProperty);
        }

        /// <summary>
        /// 设置是否启用下划线动画。
        /// </summary>
        public static void SetEnableIndicatorAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableIndicatorAnimationProperty, value);
        }

        private static void OnEnableIndicatorAnimationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabControl tabControl)
            {
                if ((bool)e.NewValue)
                {
                    tabControl.SelectionChanged += TabControl_SelectionChanged;
                    tabControl.Loaded += TabControl_Loaded;
                    // 如果已经加载，立即更新
                    if (tabControl.IsLoaded)
                    {
                        TabControl_Loaded(tabControl, new RoutedEventArgs());
                    }
                }
                else
                {
                    tabControl.SelectionChanged -= TabControl_SelectionChanged;
                    tabControl.Loaded -= TabControl_Loaded;
                }
            }
        }

        // 自动为所有 TabControl 启用动画（通过样式应用）
        static TabControlHelper()
        {
            // 监听所有 TabControl 的加载事件，自动启用动画
            EventManager.RegisterClassHandler(typeof(TabControl), FrameworkElement.LoadedEvent, 
                new RoutedEventHandler((sender, e) =>
                {
                    if (sender is TabControl tabControl)
                    {
                        // 等待模板应用后再检查并更新
                        tabControl.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            // 检查模板中是否有 AnimatedIndicator 元素
                            if (tabControl.Template != null)
                            {
                                var indicator = tabControl.Template.FindName("AnimatedIndicator", tabControl);
                                if (indicator != null)
                                {
                                    // 自动启用动画
                                    if (!GetEnableIndicatorAnimation(tabControl))
                                    {
                                        SetEnableIndicatorAnimation(tabControl, true);
                                    }
                                    // 立即更新位置
                                    UpdateIndicatorPosition(tabControl);
                                }
                            }
                        }), DispatcherPriority.Loaded);
                    }
                }), true);

            // 监听所有 TabControl 的选择变化事件
            EventManager.RegisterClassHandler(typeof(TabControl), Selector.SelectionChangedEvent,
                new SelectionChangedEventHandler((sender, e) =>
                {
                    if (sender is TabControl tabControl)
                    {
                        // 等待布局完成后再更新下划线位置
                        tabControl.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            tabControl.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                UpdateIndicatorPosition(tabControl);
                            }), DispatcherPriority.Render);
                        }), DispatcherPriority.Loaded);
                    }
                }), true);
        }

        private static void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                // 等待布局完成后再更新下划线位置
                tabControl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    // 再次延迟，确保模板完全应用
                    tabControl.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        UpdateIndicatorPosition(tabControl);
                    }), DispatcherPriority.Render);
                }), DispatcherPriority.Loaded);
            }
        }

        private static void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                // 等待布局完成后再更新下划线位置
                tabControl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    // 再次延迟，确保新选中的 TabItem 已布局
                    tabControl.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        UpdateIndicatorPosition(tabControl);
                    }), DispatcherPriority.Render);
                }), DispatcherPriority.Loaded);
            }
        }

        private static void UpdateIndicatorPosition(TabControl tabControl)
        {
            try
            {
                // 查找模板中的下划线元素
                var indicator = tabControl.Template?.FindName("AnimatedIndicator", tabControl) as FrameworkElement;
                if (indicator == null) return;

                // 获取选中项的容器
                TabItem itemContainer = null;
                if (tabControl.SelectedItem != null)
                {
                    itemContainer = tabControl.ItemContainerGenerator.ContainerFromItem(tabControl.SelectedItem) as TabItem;
                }
                
                // 如果 SelectedItem 是 TabItem 本身
                if (itemContainer == null && tabControl.SelectedItem is TabItem tabItem)
                {
                    itemContainer = tabItem;
                }

            if (itemContainer == null)
            {
                // 如果没有选中项，隐藏下划线
                var transformToClear = indicator.RenderTransform as TranslateTransform;
                if (transformToClear != null)
                {
                    transformToClear.BeginAnimation(TranslateTransform.XProperty, null);
                    transformToClear.X = 0;
                }
                indicator.BeginAnimation(FrameworkElement.WidthProperty, null);
                indicator.BeginAnimation(UIElement.OpacityProperty, null);
                indicator.Width = 0;
                indicator.Opacity = 0;
                return;
            }

            // 等待布局完成
            itemContainer.UpdateLayout();
            tabControl.UpdateLayout();

            // 查找 HeaderPanel
            var headerPanel = FindVisualChild<TabPanel>(tabControl);
            if (headerPanel == null) return;

            // 计算相对于 HeaderPanel 的位置（因为下划线在同一个 Grid 中）
            var itemPosition = itemContainer.TransformToAncestor(headerPanel).Transform(new Point(0, 0));
            var itemWidth = itemContainer.ActualWidth;

            // 如果宽度为0，说明还没有布局完成，延迟执行
            if (itemWidth <= 0)
            {
                tabControl.Dispatcher.BeginInvoke(new Action(() =>
                {
                    UpdateIndicatorPosition(tabControl);
                }), DispatcherPriority.Loaded);
                return;
            }

            // 获取或创建 TranslateTransform
            var transform = indicator.RenderTransform as TranslateTransform;
            if (transform == null)
            {
                transform = new TranslateTransform();
                indicator.RenderTransform = transform;
            }

            // 获取当前值作为起始值
            var currentX = double.IsNaN(transform.X) ? 0 : transform.X;
            var currentWidth = indicator.Width;
            if (double.IsNaN(currentWidth) || currentWidth <= 0) currentWidth = 0;
            var currentOpacity = indicator.Opacity;
            if (double.IsNaN(currentOpacity)) currentOpacity = 0;

            // 如果是首次显示（宽度为0），直接设置位置和初始值
            bool isFirstTime = currentWidth <= 0;
            if (isFirstTime)
            {
                // 停止之前的动画
                transform.BeginAnimation(TranslateTransform.XProperty, null);
                indicator.BeginAnimation(FrameworkElement.WidthProperty, null);
                indicator.BeginAnimation(UIElement.OpacityProperty, null);
                    
                // 设置初始位置（考虑 TabPanel 的 Margin 2px）
                transform.X = itemPosition.X;
                indicator.Width = 0;
                indicator.Opacity = 0;
            }

            // 创建动画
            var duration = new Duration(TimeSpan.FromSeconds(0.3));
            var easing = new CubicEase { EasingMode = EasingMode.EaseOut };

            // 位置动画（使用 TranslateTransform.X）
            var xAnimation = new DoubleAnimation
            {
                From = isFirstTime ? itemPosition.X : currentX,
                To = itemPosition.X,
                Duration = duration,
                EasingFunction = easing
            };

            // 宽度动画
            var widthAnimation = new DoubleAnimation
            {
                From = isFirstTime ? 0 : currentWidth,
                To = itemWidth,
                Duration = duration,
                EasingFunction = easing
            };

            // 透明度动画
            var opacityAnimation = new DoubleAnimation
            {
                From = isFirstTime ? 0 : currentOpacity,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.2)),
                EasingFunction = easing
            };

            // 启动动画
            transform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            indicator.BeginAnimation(FrameworkElement.WidthProperty, widthAnimation);
            indicator.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            }
            catch
            {
                // 忽略错误，避免影响 UI
            }
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                {
                    return result;
                }
                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }
    }
}

