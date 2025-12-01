using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// TextBox 控件的附加属性辅助类
    /// </summary>
    public static class TextBoxHelper
    {
        #region Placeholder 附加属性

        /// <summary>
        /// 占位符文本附加属性
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(TextBoxHelper),
                new PropertyMetadata(string.Empty, OnPlaceholderChanged));

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                // 将占位符文本设置到Tag属性，供样式使用
                textBox.Tag = e.NewValue;
            }
        }

        #endregion

        #region ShowClearButton 附加属性

        /// <summary>
        /// 显示清除按钮附加属性
        /// </summary>
        public static readonly DependencyProperty ShowClearButtonProperty =
            DependencyProperty.RegisterAttached(
                "ShowClearButton",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(false, OnShowClearButtonChanged));

        public static bool GetShowClearButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowClearButtonProperty);
        }

        public static void SetShowClearButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowClearButtonProperty, value);
        }

        private static void OnShowClearButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.Loaded -= TextBox_Loaded;
                textBox.Loaded += TextBox_Loaded;
            }
        }

        private static void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // 查找模板中的清除按钮
                var clearButton = FindVisualChild<ButtonBase>(textBox, "PART_ClearButton");
                if (clearButton != null)
                {
                    clearButton.Click -= ClearButton_Click;
                    clearButton.Click += ClearButton_Click;
                }
            }
        }

        private static void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ButtonBase button)
            {
                // 查找父级TextBox
                var textBox = FindVisualParent<TextBox>(button);
                if (textBox != null)
                {
                    textBox.Clear();
                    textBox.Focus();
                }
            }
        }

        #endregion


        #region Icon 附加属性

        /// <summary>
        /// 图标附加属性
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(ImageSource),
                typeof(TextBoxHelper),
                new PropertyMetadata(null));

        public static ImageSource GetIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion

        #region CornerRadius 附加属性

        /// <summary>
        /// 圆角附加属性
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(TextBoxHelper),
                new PropertyMetadata(new CornerRadius(0)));

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// 在可视化树中查找指定类型和名称的子元素
        /// </summary>
        private static T FindVisualChild<T>(DependencyObject parent, string name = null) where T : DependencyObject
        {
            if (parent == null)
                return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (string.IsNullOrEmpty(name) || (child is FrameworkElement fe && fe.Name == name)))
                {
                    return typedChild;
                }

                var result = FindVisualChild<T>(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }

        /// <summary>
        /// 在可视化树中查找指定类型的父元素
        /// </summary>
        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);

            if (parent == null)
                return null;

            if (parent is T typedParent)
                return typedParent;

            return FindVisualParent<T>(parent);
        }

        #endregion
    }
}
