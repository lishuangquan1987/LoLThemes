using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LOLThemes.Wpf.Samples.Models;
using LOLThemes.Wpf.Samples.Services;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    public class Screenshot2ViewModel : INotifyPropertyChanged
    {
        private Champion? _selectedChampion;

        public Champion? SelectedChampion
        {
            get => _selectedChampion;
            set
            {
                if (_selectedChampion != value)
                {
                    _selectedChampion = value;
                    OnPropertyChanged();
                }
            }
        }

        public PlayerData PlayerData { get; set; }

        public Screenshot2ViewModel()
        {
            var champions = DataService.GetChampions();
            SelectedChampion = champions.FirstOrDefault(c => c.Name == "亚索");
            PlayerData = DataService.GetPlayerData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

