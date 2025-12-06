using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LOLThemes.Wpf.Samples.Models;
using LOLThemes.Wpf.Samples.Services;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    public class Screenshot6ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ShopItem> ShopItems { get; set; }
        public PlayerData PlayerData { get; set; }

        public Screenshot6ViewModel()
        {
            var items = DataService.GetShopItems();
            ShopItems = new ObservableCollection<ShopItem>(items);
            PlayerData = DataService.GetPlayerData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

