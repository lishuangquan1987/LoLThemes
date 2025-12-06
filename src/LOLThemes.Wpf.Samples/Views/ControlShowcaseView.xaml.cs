using System.Windows;
using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

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
            DataContext = new ControlShowcaseViewModel();
        }
    }
}
