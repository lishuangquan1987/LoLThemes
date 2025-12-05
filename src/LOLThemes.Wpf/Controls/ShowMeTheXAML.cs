using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// ShowMeTheXAML 控件，用于自动显示包裹在其中的控件的 XAML 代码。
    /// 使用悬浮框显示，点击图标打开，失去焦点关闭。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:ShowMeTheXAML&gt;
    ///     &lt;Button Content="示例按钮" Style="{StaticResource LOLButtonStyle}"/&gt;
    /// &lt;/controls:ShowMeTheXAML&gt;
    /// </code>
    /// </example>
    public class ShowMeTheXAML : ContentControl
    {
        private RichTextBox? _codeRichTextBox;
        private Button? _copyButton;
        private Popup? _codePopup;
        private Button? _toggleButton;

        static ShowMeTheXAML()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ShowMeTheXAML),
                new FrameworkPropertyMetadata(typeof(ShowMeTheXAML)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _codeRichTextBox = GetTemplateChild("PART_CodeRichTextBox") as RichTextBox;
            _copyButton = GetTemplateChild("PART_CopyButton") as Button;
            _codePopup = GetTemplateChild("PART_CodePopup") as Popup;
            _toggleButton = GetTemplateChild("PART_ToggleButton") as Button;

            if (_copyButton != null)
            {
                _copyButton.Click += CopyButton_Click;
            }

            if (_toggleButton != null)
            {
                _toggleButton.Click += ToggleButton_Click;
            }

            if (_codePopup != null)
            {
                // 设置 PlacementTarget
                _codePopup.PlacementTarget = _toggleButton;
                _codePopup.Placement = PlacementMode.Bottom;
                _codePopup.Closed += CodePopup_Closed;
            }

            // 监听窗口失去焦点事件
            this.Loaded += ShowMeTheXAML_Loaded;
            this.Unloaded += ShowMeTheXAML_Unloaded;

            UpdateXamlCode();
        }

        private void CodePopup_MouseLeave(object sender, MouseEventArgs e)
        {
            // 延迟检查，允许鼠标移动到 Popup 内的按钮上
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_codePopup != null && !_codePopup.IsKeyboardFocusWithin)
                {
                    var mousePos = Mouse.GetPosition(_codePopup.Child);
                    if (_codePopup.Child != null)
                    {
                        var rect = new Rect(0, 0, _codePopup.Child.RenderSize.Width, _codePopup.Child.RenderSize.Height);
                        if (!rect.Contains(mousePos))
                        {
                            _codePopup.IsOpen = false;
                        }
                    }
                }
            }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void ShowMeTheXAML_Loaded(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is Window window)
            {
                window.Deactivated += Window_Deactivated;
            }
        }

        private void ShowMeTheXAML_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is Window window)
            {
                window.Deactivated -= Window_Deactivated;
            }
        }

        private void Window_Deactivated(object? sender, EventArgs e)
        {
            if (_codePopup != null && _codePopup.IsOpen)
            {
                _codePopup.IsOpen = false;
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (_codePopup != null)
            {
                _codePopup.IsOpen = !_codePopup.IsOpen;
                if (_codePopup.IsOpen)
                {
                    // 确保代码已更新
                    UpdateXamlCode();
                    
                    // 设置 Popup 内容的鼠标离开事件
                    if (_codePopup.Child != null)
                    {
                        _codePopup.Child.MouseLeave += CodePopupContent_MouseLeave;
                    }
                    
                    // 延迟设置焦点，确保 Popup 已完全打开
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        _codePopup.Child?.Focus();
                    }), System.Windows.Threading.DispatcherPriority.Loaded);
                }
            }
        }

        private void CodePopupContent_MouseLeave(object sender, MouseEventArgs e)
        {
            // 延迟检查，允许鼠标移动到 Popup 内的按钮上
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_codePopup != null && _codePopup.IsOpen)
                {
                    try
                    {
                        if (_codePopup.Child != null)
                        {
                            var mousePos = Mouse.GetPosition(_codePopup.Child);
                            var rect = new Rect(0, 0, _codePopup.Child.RenderSize.Width, _codePopup.Child.RenderSize.Height);
                            if (!rect.Contains(mousePos))
                            {
                                _codePopup.IsOpen = false;
                            }
                        }
                    }
                    catch
                    {
                        // 忽略错误
                    }
                }
            }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void CodePopup_Closed(object? sender, EventArgs e)
        {
            // Popup 关闭时移除事件监听器
            if (_codePopup?.Child != null)
            {
                _codePopup.Child.MouseLeave -= CodePopupContent_MouseLeave;
            }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            UpdateXamlCode();
        }

        private void UpdateXamlCode()
        {
            if (_codeRichTextBox == null || Content == null)
                return;

            try
            {
                string xaml = XamlWriter.Save(Content);
                // 格式化 XAML 代码
                string formattedXaml = FormatXaml(xaml);
                
                // 应用语法高亮
                ApplySyntaxHighlighting(formattedXaml);

                // 更新复制按钮的 Tag
                if (_copyButton != null)
                {
                    _copyButton.Tag = formattedXaml;
                }
            }
            catch (Exception ex)
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run($"// 无法生成 XAML 代码: {ex.Message}")
                {
                    Foreground = new SolidColorBrush(Colors.Red)
                });
                _codeRichTextBox.Document = new FlowDocument(paragraph);
            }
        }

        private string FormatXaml(string xaml)
        {
            if (string.IsNullOrWhiteSpace(xaml))
                return string.Empty;

            // 使用正则表达式进行基本的 XAML 格式化
            // 将标签之间的内容添加换行和缩进
            var sb = new StringBuilder();
            int indentLevel = 0;
            const int indentSize = 4;
            bool inTag = false;
            bool inEndTag = false;

            for (int i = 0; i < xaml.Length; i++)
            {
                char c = xaml[i];
                char? nextChar = i + 1 < xaml.Length ? xaml[i + 1] : null;

                if (c == '<')
                {
                    // 检查是否是结束标签
                    if (nextChar == '/')
                    {
                        inEndTag = true;
                        indentLevel = Math.Max(0, indentLevel - 1);
                        if (sb.Length > 0 && sb[sb.Length - 1] != '\n')
                        {
                            sb.AppendLine();
                        }
                        sb.Append(new string(' ', indentLevel * indentSize));
                        sb.Append(c);
                    }
                    else
                    {
                        if (sb.Length > 0 && sb[sb.Length - 1] != '\n' && sb[sb.Length - 1] != '\r')
                        {
                            sb.AppendLine();
                        }
                        sb.Append(new string(' ', indentLevel * indentSize));
                        sb.Append(c);
                        inTag = true;
                    }
                }
                else if (c == '>')
                {
                    sb.Append(c);
                    // 检查是否是自闭合标签（前一个字符是 /）
                    if (i > 0 && xaml[i - 1] == '/')
                    {
                        sb.AppendLine();
                    }
                    else if (inEndTag)
                    {
                        sb.AppendLine();
                        inEndTag = false;
                    }
                    else if (inTag)
                    {
                        sb.AppendLine();
                        indentLevel++;
                        inTag = false;
                    }
                }
                else if (c == '/' && inTag && nextChar == '>')
                {
                    // 自闭合标签
                    indentLevel = Math.Max(0, indentLevel - 1);
                    sb.Append(c);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().TrimEnd();
        }

        private void ApplySyntaxHighlighting(string xaml)
        {
            if (_codeRichTextBox == null)
                return;

            var document = new FlowDocument
            {
                Background = new SolidColorBrush(Color.FromRgb(0x1E, 0x1E, 0x1E)),
                FontFamily = new FontFamily("Consolas"),
                FontSize = 13,
                LineHeight = 20
            };

            var paragraph = new Paragraph
            {
                Margin = new Thickness(0)
            };

            // XAML 语法高亮颜色定义（类似 VS Code 的 Dark+ 主题）
            var tagColor = new SolidColorBrush(Color.FromRgb(0x86, 0x9A, 0xB0)); // 标签名 - 蓝灰色
            var attributeColor = new SolidColorBrush(Color.FromRgb(0x9C, 0xDC, 0xFE)); // 属性名 - 浅蓝色
            var valueColor = new SolidColorBrush(Color.FromRgb(0xCE, 0x91, 0x78)); // 属性值 - 橙色
            var stringColor = new SolidColorBrush(Color.FromRgb(0xCE, 0x91, 0x78)); // 字符串 - 橙色
            var commentColor = new SolidColorBrush(Color.FromRgb(0x6A, 0x99, 0x59)); // 注释 - 绿色
            var defaultColor = new SolidColorBrush(Color.FromRgb(0xD4, 0xD4, 0xD4)); // 默认文本 - 浅灰色
            var operatorColor = new SolidColorBrush(Color.FromRgb(0xD4, 0xD4, 0xD4)); // 操作符 - 浅灰色
            var keywordColor = new SolidColorBrush(Color.FromRgb(0x56, 0x9C, 0xD6)); // 关键字 - 蓝色

            // 使用正则表达式进行更准确的语法高亮
            var lines = xaml.Split(new[] { '\r', '\n' }, StringSplitOptions.None);
            
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    paragraph.Inlines.Add(new Run("\n"));
                    continue;
                }

                int pos = 0;
                while (pos < line.Length)
                {
                    // 检查注释
                    if (pos < line.Length - 3 && line.Substring(pos, 4) == "<!--")
                    {
                        int endPos = line.IndexOf("-->", pos);
                        if (endPos > pos)
                        {
                            paragraph.Inlines.Add(new Run(line.Substring(pos, endPos - pos + 3))
                            {
                                Foreground = commentColor
                            });
                            pos = endPos + 3;
                            continue;
                        }
                    }

                    // 检查标签开始 <
                    if (line[pos] == '<')
                    {
                        paragraph.Inlines.Add(new Run("<") { Foreground = operatorColor });
                        pos++;

                        // 检查是否是结束标签
                        if (pos < line.Length && line[pos] == '/')
                        {
                            paragraph.Inlines.Add(new Run("/") { Foreground = operatorColor });
                            pos++;
                        }

                        // 提取标签名（可能包含命名空间前缀）
                        int tagStart = pos;
                        while (pos < line.Length && line[pos] != ' ' && line[pos] != '>' && line[pos] != '/')
                        {
                            pos++;
                        }
                        if (pos > tagStart)
                        {
                            string tagName = line.Substring(tagStart, pos - tagStart);
                            // 检查是否有命名空间前缀
                            if (tagName.Contains(":"))
                            {
                                var parts = tagName.Split(':');
                                paragraph.Inlines.Add(new Run(parts[0]) { Foreground = defaultColor });
                                paragraph.Inlines.Add(new Run(":") { Foreground = operatorColor });
                                paragraph.Inlines.Add(new Run(parts[1]) { Foreground = tagColor });
                            }
                            else
                            {
                                paragraph.Inlines.Add(new Run(tagName) { Foreground = tagColor });
                            }
                        }
                    }
                    // 检查属性
                    else if (line[pos] == ' ' && pos + 1 < line.Length)
                    {
                        // 跳过空格
                        int spaceCount = 0;
                        while (pos < line.Length && line[pos] == ' ')
                        {
                            spaceCount++;
                            pos++;
                        }
                        paragraph.Inlines.Add(new Run(new string(' ', spaceCount)) { Foreground = defaultColor });

                        if (pos >= line.Length)
                            break;

                        // 检查是否是属性名（以字母或下划线开头）
                        if (char.IsLetter(line[pos]) || line[pos] == '_')
                        {
                            int attrStart = pos;
                            while (pos < line.Length && line[pos] != '=' && line[pos] != '>' && line[pos] != '/')
                            {
                                pos++;
                            }

                            if (pos > attrStart)
                            {
                                string attrName = line.Substring(attrStart, pos - attrStart);
                                paragraph.Inlines.Add(new Run(attrName) { Foreground = attributeColor });
                            }

                            // 检查属性值
                            if (pos < line.Length && line[pos] == '=')
                            {
                                paragraph.Inlines.Add(new Run("=") { Foreground = operatorColor });
                                pos++;

                                if (pos < line.Length && (line[pos] == '"' || line[pos] == '\''))
                                {
                                    char quote = line[pos];
                                    paragraph.Inlines.Add(new Run(quote.ToString()) { Foreground = stringColor });
                                    pos++;

                                    int valueStart = pos;
                                    while (pos < line.Length && line[pos] != quote)
                                    {
                                        pos++;
                                    }

                                    if (pos > valueStart)
                                    {
                                        string value = line.Substring(valueStart, pos - valueStart);
                                        paragraph.Inlines.Add(new Run(value) { Foreground = valueColor });
                                    }

                                    if (pos < line.Length)
                                    {
                                        paragraph.Inlines.Add(new Run(quote.ToString()) { Foreground = stringColor });
                                        pos++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // 其他字符
                            paragraph.Inlines.Add(new Run(line[pos].ToString()) { Foreground = defaultColor });
                            pos++;
                        }
                    }
                    // 其他字符
                    else
                    {
                        paragraph.Inlines.Add(new Run(line[pos].ToString()) { Foreground = defaultColor });
                        pos++;
                    }
                }

                paragraph.Inlines.Add(new Run("\n"));
            }

            document.Blocks.Add(paragraph);
            _codeRichTextBox.Document = document;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_codeRichTextBox != null && _codeRichTextBox.Document != null)
            {
                try
                {
                    string text = new TextRange(
                        _codeRichTextBox.Document.ContentStart,
                        _codeRichTextBox.Document.ContentEnd).Text;
                    Clipboard.SetText(text);
                }
                catch
                {
                    // 忽略剪贴板错误
                }
            }
        }
    }
}

