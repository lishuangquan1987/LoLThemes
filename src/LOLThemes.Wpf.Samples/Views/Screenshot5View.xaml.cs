using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class Screenshot5View : UserControl
    {
        public Screenshot5View()
        {
            InitializeComponent();
            DataContext = new Screenshot5ViewModel();
        }
    }
}
