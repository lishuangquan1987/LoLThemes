using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LOLThemes.Wpf.Samples.Models;
using LOLThemes.Wpf.Samples.Services;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    public class Screenshot1ViewModel : INotifyPropertyChanged
    {
        private Champion? _selectedChampion;
        private string _selectedPosition = "中单";
        private ObservableCollection<string> _selectedTypes = new() { "刺客", "法师" };
        private string _searchText = string.Empty;

        public ObservableCollection<Champion> Champions { get; set; }
        public ObservableCollection<string> Positions { get; set; }
        public ObservableCollection<string> Types { get; set; }
        public PlayerData PlayerData { get; set; }

        public Champion? SelectedChampion
        {
            get => _selectedChampion;
            set
            {
                if (_selectedChampion != value)
                {
                    // 取消之前的选中状态
                    if (_selectedChampion != null)
                    {
                        _selectedChampion.IsSelected = false;
                    }
                    
                    _selectedChampion = value;
                    
                    // 设置新的选中状态
                    if (_selectedChampion != null)
                    {
                        _selectedChampion.IsSelected = true;
                    }
                    
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                if (_selectedPosition != value)
                {
                    _selectedPosition = value;
                    OnPropertyChanged();
                    FilterChampions();
                }
            }
        }

        public ObservableCollection<string> SelectedTypes
        {
            get => _selectedTypes;
            set
            {
                _selectedTypes = value;
                OnPropertyChanged();
                FilterChampions();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterChampions();
                }
            }
        }

        public ICommand SelectChampionCommand { get; set; }

        public Screenshot1ViewModel()
        {
            var allChampions = DataService.GetChampions();
            Champions = new ObservableCollection<Champion>(allChampions);
            Positions = new ObservableCollection<string> { "上单", "打野", "中单", "ADC", "辅助" };
            Types = new ObservableCollection<string> { "刺客", "法师", "战士", "坦克", "射手" };
            PlayerData = DataService.GetPlayerData();

            SelectChampionCommand = new RelayCommand<Champion>(champion =>
            {
                if (champion != null)
                {
                    SelectedChampion = champion;
                }
            });

            if (Champions.Count > 0)
            {
                SelectedChampion = Champions[0];
            }
        }

        private void FilterChampions()
        {
            var allChampions = DataService.GetChampions();
            var filtered = allChampions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(c => c.Name.Contains(SearchText) || c.Title.Contains(SearchText));
            }

            if (!string.IsNullOrEmpty(SelectedPosition))
            {
                filtered = filtered.Where(c => c.Tags.Contains(SelectedPosition));
            }

            if (SelectedTypes.Count > 0)
            {
                filtered = filtered.Where(c => c.Roles.Any(r => SelectedTypes.Contains(r)));
            }

            Champions.Clear();
            foreach (var champion in filtered)
            {
                Champions.Add(champion);
            }

            if (Champions.Count > 0 && SelectedChampion != null && !Champions.Contains(SelectedChampion))
            {
                SelectedChampion = Champions[0];
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

