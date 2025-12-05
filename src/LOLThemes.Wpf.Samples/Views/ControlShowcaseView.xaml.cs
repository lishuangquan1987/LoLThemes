using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LOLThemes.Wpf.Samples.Views
{
    /// <summary>
    /// ControlShowcaseView.xaml 的交互逻辑
    /// </summary>
    public partial class ControlShowcaseView : UserControl
    {
        public ControlShowcaseView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 复制代码到剪贴板
        /// </summary>
        private void CopyCodeToClipboard(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string code)
            {
                try
                {
                    Clipboard.SetText(code);
                    // 可以添加提示消息
                }
                catch
                {
                    // 忽略剪贴板错误
                }
            }
        }
    }
}
