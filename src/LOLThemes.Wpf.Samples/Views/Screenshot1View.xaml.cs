using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class Screenshot1View : UserControl
    {
        public Screenshot1View()
        {
            InitializeComponent();
            DataContext = new Screenshot1ViewModel();
        }
    }
}
