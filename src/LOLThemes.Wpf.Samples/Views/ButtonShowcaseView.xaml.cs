using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LOLThemes.Wpf.Controls;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class ButtonShowcaseView : UserControl
    {
        public ButtonShowcaseView()
        {
            InitializeComponent();
            DataContext = new ViewModels.ButtonShowcaseViewModel();
        }

        /// <summary>
        /// 切换所有代码展开状态
        /// </summary>
        private void ToggleCodeExpander(object sender, System.Windows.RoutedEventArgs e)
        {
            // 实现切换所有XamlDisplay控件的代码展开状态
        }
    }
}