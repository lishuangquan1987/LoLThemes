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
        /// 新的逻辑：Generic.xaml -> Colors.xaml -> 替换内部的 Colors.xxx.xaml
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

            // 1. 查找 Generic.xaml 资源字典
            ResourceDictionary? genericDict = FindGenericDictionary(app.Resources);
            if (genericDict == null)
            {
                throw new InvalidOperationException("无法找到 Generic.xaml 资源字典。请确保应用已正确加载主题资源。");
            }

            // 2. 在 Generic.xaml 的 MergedDictionaries 中找到 Colors.xaml
            ResourceDictionary? colorsDict = FindColorsDictionary(genericDict);
            if (colorsDict == null)
            {
                throw new InvalidOperationException("无法找到 Colors.xaml 资源字典。请确保 Generic.xaml 正确引用了 Colors.xaml。");
            }

            // 3. 在 Colors.xaml 的 MergedDictionaries 中替换 Colors.xxx.xaml
            //ReplaceColorsTheme(colorsDict, colorsUri);
            colorsDict.MergedDictionaries.Clear();
            colorsDict.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(colorsUri, UriKind.RelativeOrAbsolute) });

            // 更新当前主题（这会触发ThemeChanged事件）
            CurrentTheme = theme;
            
            // 强制刷新所有窗口以应用新主题
            foreach (Window window in app.Windows)
            {
                RefreshVisualTree(window);
            }
        }

        /// <summary>
        /// 查找Generic.xaml资源字典
        /// </summary>
        private static ResourceDictionary? FindGenericDictionary(ResourceDictionary resources)
        {
            foreach (var dict in resources.MergedDictionaries)
            {
                if (dict.Source != null && dict.Source.OriginalString.Contains("Generic.xaml"))
                {
                    return dict;
                }
                
                // 递归查找嵌套的资源字典
                if (dict.MergedDictionaries.Count > 0)
                {
                    var found = FindGenericDictionary(dict);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 在Generic.xaml的MergedDictionaries中查找Colors.xaml资源字典
        /// </summary>
        private static ResourceDictionary? FindColorsDictionary(ResourceDictionary genericDict)
        {
            foreach (var dict in genericDict.MergedDictionaries)
            {
                if (dict.Source != null)
                {
                    var sourceStr = dict.Source.OriginalString;
                    // 查找 Colors.xaml（不包括 Colors.Dark.xaml 和 Colors.Light.xaml）
                    if (sourceStr.Contains("/Themes/Colors.xaml") && 
                        !sourceStr.Contains("Colors.Dark") && 
                        !sourceStr.Contains("Colors.Light"))
                    {
                        return dict;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 在Generic.xaml的MergedDictionaries中查找Sizes.xaml资源字典
        /// </summary>
        private static ResourceDictionary? FindSizesDictionary(ResourceDictionary genericDict)
        {
            foreach (var dict in genericDict.MergedDictionaries)
            {
                if (dict.Source != null)
                {
                    var sourceStr = dict.Source.OriginalString;
                    // 查找 Sizes.xaml（不包括 Sizes.Compact.xaml、Sizes.Medium.xaml 和 Sizes.Large.xaml）
                    if (sourceStr.Contains("/Themes/Sizes.xaml") && 
                        !sourceStr.Contains("Sizes.Compact") && 
                        !sourceStr.Contains("Sizes.Medium") && 
                        !sourceStr.Contains("Sizes.Large"))
                    {
                        return dict;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 在Colors.xaml的MergedDictionaries中替换Colors.xxx.xaml引用
        /// </summary>
        private static void ReplaceColorsTheme(ResourceDictionary colorsDict, string newColorsUri)
        {
            // 移除旧的 Colors.xxx.xaml 引用
            for (int i = colorsDict.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var mergedDict = colorsDict.MergedDictionaries[i];
                if (mergedDict.Source != null)
                {
                    var sourceStr = mergedDict.Source.OriginalString;
                    if (sourceStr.Contains("Colors.Dark.xaml") || sourceStr.Contains("Colors.Light.xaml"))
                    {
                        colorsDict.MergedDictionaries.RemoveAt(i);
                        break; // 只替换第一个匹配的
                    }
                }
            }

            // 添加新的 Colors.xxx.xaml 引用
            var newColorsResourceDict = new ResourceDictionary
            {
                Source = new Uri(newColorsUri, UriKind.Relative)
            };
            colorsDict.MergedDictionaries.Insert(0, newColorsResourceDict);
        }

        /// <summary>
        /// 在Sizes.xaml的MergedDictionaries中替换Sizes.xxx.xaml引用
        /// </summary>
        private static void ReplaceSizesTheme(ResourceDictionary sizesDict, string newSizesUri)
        {
            // 移除旧的 Sizes.xxx.xaml 引用
            for (int i = sizesDict.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var mergedDict = sizesDict.MergedDictionaries[i];
                if (mergedDict.Source != null)
                {
                    var sourceStr = mergedDict.Source.OriginalString;
                    if (sourceStr.Contains("Sizes.Compact.xaml") || 
                        sourceStr.Contains("Sizes.Medium.xaml") || 
                        sourceStr.Contains("Sizes.Large.xaml"))
                    {
                        sizesDict.MergedDictionaries.RemoveAt(i);
                        break; // 只替换第一个匹配的
                    }
                }
            }

            // 添加新的 Sizes.xxx.xaml 引用
            var newSizesResourceDict = new ResourceDictionary
            {
                Source = new Uri(newSizesUri, UriKind.Relative)
            };
            sizesDict.MergedDictionaries.Insert(0, newSizesResourceDict);
        }

        /// <summary>
        /// 初始化主题管理器，设置默认主题
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

            // 检查是否已经加载了主题资源字典（递归查找）
            bool themeLoaded = HasThemeDictionary(app.Resources);

            // 如果没有加载，则加载默认主题
            if (!themeLoaded)
            {
                SwitchTheme(defaultTheme, app);
            }
            else
            {
                // 如果已加载，更新当前主题状态
                _currentTheme = defaultTheme;
            }
        }

        /// <summary>
        /// 递归查找并移除主题资源字典
        /// </summary>
        private static void RemoveThemeDictionary(ResourceDictionary resources)
        {
            // 在当前层级查找并移除所有匹配的字典
            for (int i = resources.MergedDictionaries.Count - 1; i >= 0; i--)
            {
                var dict = resources.MergedDictionaries[i];
                if (dict.Source != null && 
                    (dict.Source.OriginalString.Contains("Colors.Dark.xaml") || 
                     dict.Source.OriginalString.Contains("Colors.Light.xaml")))
                {
                    resources.MergedDictionaries.RemoveAt(i);
                    // 继续查找，不返回，因为可能有多个匹配的字典
                }
                else
                {
                    // 递归查找嵌套的资源字典
                    if (dict.MergedDictionaries.Count > 0)
                    {
                        RemoveThemeDictionary(dict);
                    }
                }
            }
        }

        /// <summary>
        /// 递归查找是否存在主题资源字典
        /// </summary>
        private static bool HasThemeDictionary(ResourceDictionary resources)
        {
            foreach (var dict in resources.MergedDictionaries)
            {
                if (dict.Source != null && 
                    (dict.Source.OriginalString.Contains("Colors.Dark.xaml") || 
                     dict.Source.OriginalString.Contains("Colors.Light.xaml")))
                {
                    return true;
                }
                
                // 递归查找嵌套的资源字典
                if (dict.MergedDictionaries.Count > 0 && HasThemeDictionary(dict))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 切换到指定尺寸主题
        /// 新的逻辑：Generic.xaml -> Sizes.xaml -> 替换内部的 Sizes.xxx.xaml
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

            // 1. 查找 Generic.xaml 资源字典
            ResourceDictionary? genericDict = FindGenericDictionary(app.Resources);
            if (genericDict == null)
            {
                throw new InvalidOperationException("无法找到 Generic.xaml 资源字典。请确保应用已正确加载主题资源。");
            }

            // 2. 在 Generic.xaml 的 MergedDictionaries 中找到 Sizes.xaml
            ResourceDictionary? sizesDict = FindSizesDictionary(genericDict);
            if (sizesDict == null)
            {
                throw new InvalidOperationException("无法找到 Sizes.xaml 资源字典。请确保 Generic.xaml 正确引用了 Sizes.xaml。");
            }

            // 3. 在 Sizes.xaml 的 MergedDictionaries 中替换 Sizes.xxx.xaml
            ReplaceSizesTheme(sizesDict, sizesUri);

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

            bool sizesLoaded = HasSizesDictionary(app.Resources);

            if (!sizesLoaded)
            {
                SwitchSizeTheme(defaultSizeTheme, app);
            }
            else
            {
                _currentSizeTheme = defaultSizeTheme;
            }
        }

        /// <summary>
        /// 递归查找是否存在尺寸资源字典
        /// </summary>
        private static bool HasSizesDictionary(ResourceDictionary resources)
        {
            foreach (var dict in resources.MergedDictionaries)
            {
                if (dict.Source != null && 
                    (dict.Source.OriginalString.Contains("Sizes.Compact.xaml") || 
                     dict.Source.OriginalString.Contains("Sizes.Medium.xaml") || 
                     dict.Source.OriginalString.Contains("Sizes.Large.xaml")))
                {
                    return true;
                }
                
                if (dict.MergedDictionaries.Count > 0 && HasSizesDictionary(dict))
                {
                    return true;
                }
            }
            return false;
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

