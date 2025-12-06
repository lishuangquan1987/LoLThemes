using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class Screenshot2View : UserControl
    {
        public Screenshot2View()
        {
            InitializeComponent();
            DataContext = new Screenshot2ViewModel();
        }
    }
}
