using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LOLThemes.Wpf.Samples.Models;
using LOLThemes.Wpf.Samples.Services;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    public class Screenshot4ViewModel : INotifyPropertyChanged
    {
        private string _searchText = string.Empty;
        private ShopItem? _selectedItem;

        public ObservableCollection<ShopItem> ShopItems { get; set; }
        public ObservableCollection<ShopItem> CartItems { get; set; }
        public PlayerData PlayerData { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterItems();
                }
            }
        }

        public ShopItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TotalCost => CartItems.Sum(item => item.GoldCost);

        public Screenshot4ViewModel()
        {
            var allItems = DataService.GetShopItems();
            ShopItems = new ObservableCollection<ShopItem>(allItems);
            CartItems = new ObservableCollection<ShopItem>();
            PlayerData = DataService.GetPlayerData();
        }

        private void FilterItems()
        {
            var allItems = DataService.GetShopItems();
            var filtered = allItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(item => item.Name.Contains(SearchText) || item.Description.Contains(SearchText));
            }

            ShopItems.Clear();
            foreach (var item in filtered)
            {
                ShopItems.Add(item);
            }
        }

        public void AddToCart(ShopItem item)
        {
            if (!CartItems.Contains(item))
            {
                CartItems.Add(item);
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        public void RemoveFromCart(ShopItem item)
        {
            CartItems.Remove(item);
            OnPropertyChanged(nameof(TotalCost));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

