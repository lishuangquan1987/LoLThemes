using System.ComponentModel;
using System.Runtime.CompilerServices;
using LOLThemes.Wpf.Samples.Models;
using LOLThemes.Wpf.Samples.Services;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    public class Screenshot5ViewModel : INotifyPropertyChanged
    {
        public PlayerData PlayerData { get; set; }

        public Screenshot5ViewModel()
        {
            PlayerData = DataService.GetPlayerData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

