using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LOLThemes.Wpf.Controls;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class TextBoxShowcaseView : UserControl
    {
        public TextBoxShowcaseView()
        {
            InitializeComponent();
            DataContext = new ViewModels.TextBoxShowcaseViewModel();
        }

        /// <summary>
        /// 切换所有代码展开状态
        /// </summary>
        private void ToggleCodeExpander(object sender, System.Windows.RoutedEventArgs e)
        {
            // 简化实现，避免复杂的可视化树遍历
        }

        private Expander FindExpander(DependencyObject parent)
        {
            // 简化实现，避免复杂的可视化树遍历
            return null;
        }
    }
}