using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LOLThemes.Wpf.Samples.Models
{
    /// <summary>
    /// 英雄数据模型
    /// </summary>
    public class Champion : INotifyPropertyChanged
    {
        private bool _isSelected;

        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public int Difficulty { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int Cost { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<Skill> Skills { get; set; } = new();
        public bool IsOwned { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// 技能数据模型
    /// </summary>
    public class Skill
    {
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }
}

