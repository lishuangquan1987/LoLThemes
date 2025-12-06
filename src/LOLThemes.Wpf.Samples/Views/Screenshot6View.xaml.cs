using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class Screenshot6View : UserControl
    {
        public Screenshot6View()
        {
            InitializeComponent();
            DataContext = new Screenshot6ViewModel();
        }
    }
}
