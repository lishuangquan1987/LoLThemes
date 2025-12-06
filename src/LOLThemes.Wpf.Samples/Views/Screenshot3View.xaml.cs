using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class Screenshot3View : UserControl
    {
        public Screenshot3View()
        {
            InitializeComponent();
            DataContext = new Screenshot3ViewModel();
        }
    }
}
