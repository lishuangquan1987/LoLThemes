using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LOLThemes.Wpf.Samples.Views
{
    public partial class ListViewShowcaseView : UserControl
    {
        public ListViewShowcaseView()
        {
            InitializeComponent();
            Loaded += ListViewShowcaseView_Loaded;
        }

        private void ListViewShowcaseView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                var items = new ObservableCollection<ListViewItem>
                {
                    new ListViewItem { Name = "物品1", Type = "武器", Price = 1000 },
                    new ListViewItem { Name = "物品2", Type = "防具", Price = 800 },
                    new ListViewItem { Name = "物品3", Type = "消耗品", Price = 50 }
                };
                DataContext = items;
            }
        }
    }

    public class ListViewItem
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}