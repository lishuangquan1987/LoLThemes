using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LOLThemes.Wpf.Samples.Models;
using LOLThemes.Wpf.Samples.Services;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    public class Screenshot3ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Champion> Champions { get; set; }
        public PlayerData PlayerData { get; set; }

        public Screenshot3ViewModel()
        {
            var champions = DataService.GetChampions();
            Champions = new ObservableCollection<Champion>(champions);
            PlayerData = DataService.GetPlayerData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

