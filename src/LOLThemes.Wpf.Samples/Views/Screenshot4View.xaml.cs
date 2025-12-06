using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class Screenshot4View : UserControl
    {
        public Screenshot4View()
        {
            InitializeComponent();
            DataContext = new Screenshot4ViewModel();
        }
    }
}
