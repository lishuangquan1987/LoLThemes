using Material.Icons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOLThemes.Wpf.Samples.Models
{
    public class NavigationItem
    {
        public string Name { get; set; } = string.Empty;
        public string ViewName { get; set; } = string.Empty;
        public MaterialIconKind Icon { get; set; } = MaterialIconKind.QuestionMark;
        public ObservableCollection<NavigationItem> Children { get; set; } = new ObservableCollection<NavigationItem>();
        public bool IsGroup => !string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(ViewName);
        public bool IsExpanded { get; set; } = true;
    }
}
