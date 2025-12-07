using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// 主题枚举
    /// </summary>
    public enum Theme
    {
        /// <summary>
        /// 暗黑主题
        /// </summary>
        Dark,
        
        /// <summary>
        /// 白色主题
        /// </summary>
        Light
    }

    /// <summary>
    /// 尺寸主题枚举
    /// </summary>
    public enum SizeTheme
    {
        /// <summary>
        /// 紧凑尺寸（0.8倍）
        /// </summary>
        Compact,
        
        /// <summary>
        /// 中等尺寸（1.0倍，默认）
        /// </summary>
        Medium,
        
        /// <summary>
        /// 宽大尺寸（1.2倍）
        /// </summary>
        Large
    }

    /// <summary>
    /// 主题管理器，用于在运行时切换应用主题。
    /// </summary>
    /// <example>
    /// <code>
    /// // 切换到暗黑主题
    /// ThemeManager.SwitchTheme(Theme.Dark);
    /// 
    /// // 切换到白色主题
    /// ThemeManager.SwitchTheme(Theme.Light);
    /// 
    /// // 获取当前主题
    /// Theme currentTheme = ThemeManager.CurrentTheme;
    /// </code>
    /// </example>
    public static class ThemeManager
    {
        private static Theme _currentTheme = Theme.Dark;
        private static SizeTheme _currentSizeTheme = SizeTheme.Medium;
        private const string DarkColorsUri = "pack://application:,,,/LOLThemes.Wpf;component/Themes/Colors.Dark.xaml";
        private const string LightColorsUri = "pack://application:,,,/LOLThemes.Wpf;component/Themes/Colors.Light.xaml";
        private const string CompactSizesUri = "pack://application:,,,/LOLThemes.Wpf;component/Themes/Sizes.Compact.xaml";
        private const string MediumSizesUri = "pack://application:,,,/LOLThemes.Wpf;component/Themes/Sizes.Medium.xaml";
        private const string LargeSizesUri = "pack://application:,,,/LOLThemes.Wpf;component/Themes/Sizes.Large.xaml";

        /// <summary>
        /// 当前主题变更事件
        /// </summary>
        public static event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

        /// <summary>
        /// 当前尺寸主题变更事件
        /// </summary>
        public static event EventHandler<SizeThemeChangedEventArgs>? SizeThemeChanged;

        /// <summary>
        /// 获取或设置当前主题
        /// </summary>
        public static Theme CurrentTheme
        {
            get => _currentTheme;
            private set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnThemeChanged(value);
                }
            }
        }

        /// <summary>
        /// 获取或设置当前尺寸主题
        /// </summary>
        public static SizeTheme CurrentSizeTheme
        {
            get => _currentSizeTheme;
            private set
            {
                if (_currentSizeTheme != value)
                {
                    _currentSizeTheme = value;
                    OnSizeThemeChanged(value);
                }
            }
        }

        /// <summary>
        /// 切换到指定主题
        /// 新逻辑：直接在App.Resources.MergedDictionaries中查找并替换Colors.xxx.xaml
        /// 这种方式比通过占位符文件更可靠，可以立即生效
        /// </summary>
        /// <param name="theme">要切换到的主题</param>
        /// <param name="application">应用程序实例，如果为null则使用Application.Current</param>
        public static void SwitchTheme(Theme theme, Application? application = null)
        {
            var app = application ?? Application.Current;
            if (app == null)
            {
                throw new InvalidOperationException("无法获取Application实例。请确保在Application启动后调用此方法。");
            }

            var colorsUri = theme == Theme.Dark ? DarkColorsUri : LightColorsUri;

            // 直接在App.Resources.MergedDictionaries中查找Colors.xxx.xaml资源字典
            ResourceDictionary? existingColorsDict = FindColorsResourceDictionary(app.Resources);
            if (existingColorsDict != null)
            {
                app.Resources.MergedDictionaries.Remove(existingColorsDict);
            }

            // 添加新的Colors.xxx.xaml资源字典
            var newColorsDict = new ResourceDictionary
            {
                Source = new Uri(colorsUri, UriKind.RelativeOrAbsolute)
            };
            app.Resources.MergedDictionaries.Add(newColorsDict);

            // 更新当前主题（这会触发ThemeChanged事件）
            CurrentTheme = theme;
            
            // 强制刷新所有窗口以应用新主题
            foreach (Window window in app.Windows)
            {
                RefreshVisualTree(window);
            }
        }

        /// <summary>
        /// 在App.Resources.MergedDictionaries中查找Colors.xxx.xaml资源字典
        /// </summary>
        private static ResourceDictionary? FindColorsResourceDictionary(ResourceDictionary resources)
        {
            foreach (var dict in resources.MergedDictionaries)
            {
                if (dict.Source != null)
                {
                    var sourceStr = dict.Source.OriginalString;
                    // 查找 Colors.Dark.xaml 或 Colors.Light.xaml
                    if (sourceStr.Contains("Colors.Dark.xaml") || sourceStr.Contains("Colors.Light.xaml"))
                    {
                        return dict;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 在App.Resources.MergedDictionaries中查找Sizes.xxx.xaml资源字典
        /// </summary>
        private static ResourceDictionary? FindSizesResourceDictionary(ResourceDictionary resources)
        {
            foreach (var dict in resources.MergedDictionaries)
            {
                if (dict.Source != null)
                {
                    var sourceStr = dict.Source.OriginalString;
                    // 查找 Sizes.Compact.xaml、Sizes.Medium.xaml 或 Sizes.Large.xaml
                    if (sourceStr.Contains("Sizes.Compact.xaml") || 
                        sourceStr.Contains("Sizes.Medium.xaml") || 
                        sourceStr.Contains("Sizes.Large.xaml"))
                    {
                        return dict;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 初始化主题管理器，设置默认主题
        /// 注意：此方法现在只是同步当前主题状态，实际的资源加载应该在App.xaml中完成
        /// </summary>
        /// <param name="defaultTheme">默认主题，默认为Dark</param>
        /// <param name="application">应用程序实例，如果为null则使用Application.Current</param>
        public static void Initialize(Theme defaultTheme = Theme.Dark, Application? application = null)
        {
            var app = application ?? Application.Current;
            if (app == null)
            {
                throw new InvalidOperationException("无法获取Application实例。请确保在Application启动后调用此方法。");
            }

            // 检查是否已经加载了主题资源字典
            bool themeLoaded = FindColorsResourceDictionary(app.Resources) != null;

            // 如果没有加载，则加载默认主题
            if (!themeLoaded)
            {
                SwitchTheme(defaultTheme, app);
            }
            else
            {
                // 如果已加载，根据当前资源确定主题并更新状态
                var colorsDict = FindColorsResourceDictionary(app.Resources);
                if (colorsDict?.Source != null)
                {
                    var sourceStr = colorsDict.Source.OriginalString;
                    _currentTheme = sourceStr.Contains("Colors.Dark.xaml") ? Theme.Dark : Theme.Light;
                }
                else
                {
                    _currentTheme = defaultTheme;
                }
            }
        }

        /// <summary>
        /// 切换到指定尺寸主题
        /// 新逻辑：直接在App.Resources.MergedDictionaries中查找并替换Sizes.xxx.xaml
        /// 这种方式比通过占位符文件更可靠，可以立即生效
        /// </summary>
        /// <param name="sizeTheme">要切换到的尺寸主题</param>
        /// <param name="application">应用程序实例，如果为null则使用Application.Current</param>
        public static void SwitchSizeTheme(SizeTheme sizeTheme, Application? application = null)
        {
            var app = application ?? Application.Current;
            if (app == null)
            {
                throw new InvalidOperationException("无法获取Application实例。请确保在Application启动后调用此方法。");
            }

            string sizesUri = sizeTheme switch
            {
                SizeTheme.Compact => CompactSizesUri,
                SizeTheme.Medium => MediumSizesUri,
                SizeTheme.Large => LargeSizesUri,
                _ => MediumSizesUri
            };

            // 直接在App.Resources.MergedDictionaries中查找Sizes.xxx.xaml资源字典
            ResourceDictionary? existingSizesDict = FindSizesResourceDictionary(app.Resources);
            if (existingSizesDict != null)
            {
                app.Resources.MergedDictionaries.Remove(existingSizesDict);
            }

            // 添加新的Sizes.xxx.xaml资源字典
            var newSizesDict = new ResourceDictionary
            {
                Source = new Uri(sizesUri, UriKind.RelativeOrAbsolute)
            };
            app.Resources.MergedDictionaries.Add(newSizesDict);

            // 更新当前尺寸主题（这会触发SizeThemeChanged事件）
            CurrentSizeTheme = sizeTheme;
            
            // 强制刷新所有窗口以应用新尺寸主题
            foreach (Window window in app.Windows)
            {
                RefreshVisualTree(window);
            }
        }


        /// <summary>
        /// 初始化尺寸主题管理器，设置默认尺寸主题
        /// 注意：此方法现在只是同步当前尺寸主题状态，实际的资源加载应该在App.xaml中完成
        /// </summary>
        /// <param name="defaultSizeTheme">默认尺寸主题，默认为Medium</param>
        /// <param name="application">应用程序实例，如果为null则使用Application.Current</param>
        public static void InitializeSizeTheme(SizeTheme defaultSizeTheme = SizeTheme.Medium, Application? application = null)
        {
            var app = application ?? Application.Current;
            if (app == null)
            {
                throw new InvalidOperationException("无法获取Application实例。请确保在Application启动后调用此方法。");
            }

            bool sizesLoaded = FindSizesResourceDictionary(app.Resources) != null;

            if (!sizesLoaded)
            {
                SwitchSizeTheme(defaultSizeTheme, app);
            }
            else
            {
                // 如果已加载，根据当前资源确定尺寸主题并更新状态
                var sizesDict = FindSizesResourceDictionary(app.Resources);
                if (sizesDict?.Source != null)
                {
                    var sourceStr = sizesDict.Source.OriginalString;
                    if (sourceStr.Contains("Sizes.Compact.xaml"))
                    {
                        _currentSizeTheme = SizeTheme.Compact;
                    }
                    else if (sourceStr.Contains("Sizes.Large.xaml"))
                    {
                        _currentSizeTheme = SizeTheme.Large;
                    }
                    else
                    {
                        _currentSizeTheme = SizeTheme.Medium;
                    }
                }
                else
                {
                    _currentSizeTheme = defaultSizeTheme;
                }
            }
        }

        /// <summary>
        /// 递归刷新视觉树中的所有元素
        /// </summary>
        public static void RefreshVisualTree(DependencyObject obj)
        {
            if (obj == null) return;

            // 刷新当前元素
            if (obj is FrameworkElement fe)
            {
                fe.InvalidateVisual();
                fe.InvalidateMeasure();
                fe.InvalidateArrange();
            }

            // 递归刷新子元素
            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                RefreshVisualTree(child);
            }
        }

        /// <summary>
        /// 触发主题变更事件
        /// </summary>
        private static void OnThemeChanged(Theme newTheme)
        {
            ThemeChanged?.Invoke(null, new ThemeChangedEventArgs(newTheme));
        }

        /// <summary>
        /// 触发尺寸主题变更事件
        /// </summary>
        private static void OnSizeThemeChanged(SizeTheme newSizeTheme)
        {
            SizeThemeChanged?.Invoke(null, new SizeThemeChangedEventArgs(newSizeTheme));
        }
    }

    /// <summary>
    /// 主题变更事件参数
    /// </summary>
    public class ThemeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 新的主题
        /// </summary>
        public Theme NewTheme { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="newTheme">新的主题</param>
        public ThemeChangedEventArgs(Theme newTheme)
        {
            NewTheme = newTheme;
        }
    }

    /// <summary>
    /// 尺寸主题变更事件参数
    /// </summary>
    public class SizeThemeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 新的尺寸主题
        /// </summary>
        public SizeTheme NewSizeTheme { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="newSizeTheme">新的尺寸主题</param>
        public SizeThemeChangedEventArgs(SizeTheme newSizeTheme)
        {
            NewSizeTheme = newSizeTheme;
        }
    }
}

