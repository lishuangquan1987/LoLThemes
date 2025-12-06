using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LOLThemes.Wpf.Samples.Views
{
    /// <summary>
    /// DataGridShowcaseView.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridShowcaseView : UserControl
    {
        public DataGridShowcaseView()
        {
            InitializeComponent();
            Loaded += DataGridShowcaseView_Loaded;
        }

        private void DataGridShowcaseView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                var items = new ObservableCollection<DataItem>
                {
                    new DataItem { Name = "物品1", Type = "武器", Price = 1000, Available = true },
                    new DataItem { Name = "物品2", Type = "防具", Price = 800, Available = true },
                    new DataItem { Name = "物品3", Type = "消耗品", Price = 50, Available = false }
                };
                DataContext = items;
            }
        }
    }

    public class DataItem
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
        public bool Available { get; set; }
    }
}

