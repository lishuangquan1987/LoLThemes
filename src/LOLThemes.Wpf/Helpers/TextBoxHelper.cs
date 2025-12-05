using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// TextBox 控件的附加属性辅助类。
    /// 提供占位符文本、清除按钮、图标和圆角等附加属性的支持。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;TextBox helpers:TextBoxHelper.Placeholder="请输入文本"
    ///          helpers:TextBoxHelper.ShowClearButton="True"
    ///          helpers:TextBoxHelper.Icon="{StaticResource SearchIcon}"
    ///          helpers:TextBoxHelper.CornerRadius="4"/&gt;
    /// </code>
    /// </example>
    public static class TextBoxHelper
    {
        #region Placeholder 附加属性

        /// <summary>
        /// 标识 <see cref="Placeholder"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(TextBoxHelper),
                new PropertyMetadata(string.Empty, OnPlaceholderChanged));

        /// <summary>
        /// 获取占位符文本。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <returns>占位符文本</returns>
        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        /// <summary>
        /// 设置占位符文本。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <param name="value">占位符文本</param>
        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// 当 <see cref="Placeholder"/> 属性值改变时调用。
        /// 将占位符文本设置到 TextBox 的 Tag 属性，供样式使用。
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性变更事件参数</param>
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
        /// 标识 <see cref="ShowClearButton"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty ShowClearButtonProperty =
            DependencyProperty.RegisterAttached(
                "ShowClearButton",
                typeof(bool),
                typeof(TextBoxHelper),
                new PropertyMetadata(false, OnShowClearButtonChanged));

        /// <summary>
        /// 获取是否显示清除按钮。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <returns>如果显示清除按钮返回 true，否则返回 false</returns>
        public static bool GetShowClearButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowClearButtonProperty);
        }

        /// <summary>
        /// 设置是否显示清除按钮。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <param name="value">是否显示</param>
        public static void SetShowClearButton(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowClearButtonProperty, value);
        }

        /// <summary>
        /// 当 <see cref="ShowClearButton"/> 属性值改变时调用。
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性变更事件参数</param>
        private static void OnShowClearButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.Loaded -= TextBox_Loaded;
                textBox.Loaded += TextBox_Loaded;
            }
        }

        /// <summary>
        /// TextBox 加载时的事件处理程序。
        /// 查找模板中的清除按钮并附加点击事件。
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">事件参数</param>
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

        /// <summary>
        /// 清除按钮点击事件处理程序。
        /// 清除 TextBox 的文本内容并重新获得焦点。
        /// </summary>
        /// <param name="sender">事件发送者（应为 ButtonBase）</param>
        /// <param name="e">事件参数</param>
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
        /// 标识 <see cref="Icon"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(ImageSource),
                typeof(TextBoxHelper),
                new PropertyMetadata(null));

        /// <summary>
        /// 获取图标。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <returns>图标图像源</returns>
        public static ImageSource GetIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(IconProperty);
        }

        /// <summary>
        /// 设置图标。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <param name="value">图标图像源</param>
        public static void SetIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion

        #region CornerRadius 附加属性

        /// <summary>
        /// 标识 <see cref="CornerRadius"/> 附加属性。
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(TextBoxHelper),
                new PropertyMetadata(new CornerRadius(0)));

        /// <summary>
        /// 获取圆角。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <returns>圆角值</returns>
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// 设置圆角。
        /// </summary>
        /// <param name="obj">依赖对象（应为 TextBox）</param>
        /// <param name="value">圆角值</param>
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// 在可视化树中查找指定类型和名称的子元素。
        /// </summary>
        /// <typeparam name="T">要查找的元素类型</typeparam>
        /// <param name="parent">父元素</param>
        /// <param name="name">元素名称（可选）</param>
        /// <returns>找到的元素，如果未找到则返回 null</returns>
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
        /// 在可视化树中查找指定类型的父元素。
        /// </summary>
        /// <typeparam name="T">要查找的元素类型</typeparam>
        /// <param name="child">子元素</param>
        /// <returns>找到的父元素，如果未找到则返回 null</returns>
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
