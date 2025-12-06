using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    /// <summary>
    /// 控件展示视图模型
    /// </summary>
    public class ControlShowcaseViewModel : INotifyPropertyChanged
    {
        private string? _selectedControl;

        public string? SelectedControl
        {
            get => _selectedControl;
            set
            {
                if (_selectedControl != value)
                {
                    _selectedControl = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ControlItem> ControlItems { get; set; }

        public ControlShowcaseViewModel()
        {
            ControlItems = new ObservableCollection<ControlItem>
            {
                new ControlItem { Name = "Button", DisplayName = "按钮", Icon = "🔘", ViewName = "ButtonShowcaseView" },
                new ControlItem { Name = "TextBox", DisplayName = "文本框", Icon = "📝", ViewName = "TextBoxShowcaseView" },
                new ControlItem { Name = "PasswordBox", DisplayName = "密码框", Icon = "🔒", ViewName = "PasswordBoxShowcaseView" },
                new ControlItem { Name = "ComboBox", DisplayName = "下拉框", Icon = "📋", ViewName = "ComboBoxShowcaseView" },
                new ControlItem { Name = "CheckBox", DisplayName = "复选框", Icon = "☑️", ViewName = "CheckBoxShowcaseView" },
                new ControlItem { Name = "RadioButton", DisplayName = "单选按钮", Icon = "🔘", ViewName = "RadioButtonShowcaseView" },
                new ControlItem { Name = "ToggleButton", DisplayName = "切换按钮", Icon = "🔄", ViewName = "ToggleButtonShowcaseView" },
                new ControlItem { Name = "Slider", DisplayName = "滑块", Icon = "🎚️", ViewName = "SliderShowcaseView" },
                new ControlItem { Name = "ProgressBar", DisplayName = "进度条", Icon = "📊", ViewName = "ProgressBarShowcaseView" },
                new ControlItem { Name = "ListBox", DisplayName = "列表框", Icon = "📜", ViewName = "ListBoxShowcaseView" },
                new ControlItem { Name = "ListView", DisplayName = "列表视图", Icon = "📋", ViewName = "ListViewShowcaseView" },
                new ControlItem { Name = "TabControl", DisplayName = "标签页", Icon = "📑", ViewName = "TabControlShowcaseView" },
                new ControlItem { Name = "GroupBox", DisplayName = "分组框", Icon = "📦", ViewName = "GroupBoxShowcaseView" },
                new ControlItem { Name = "Menu", DisplayName = "菜单", Icon = "☰", ViewName = "MenuShowcaseView" },
                new ControlItem { Name = "ToolTip", DisplayName = "提示框", Icon = "💡", ViewName = "ToolTipShowcaseView" },
                new ControlItem { Name = "GlowButton", DisplayName = "发光按钮", Icon = "✨", ViewName = "GlowButtonShowcaseView" },
                new ControlItem { Name = "HexagonButton", DisplayName = "六边形按钮", Icon = "⬡", ViewName = "HexagonButtonShowcaseView" },
                new ControlItem { Name = "SkillButton", DisplayName = "技能按钮", Icon = "⚔️", ViewName = "SkillButtonShowcaseView" },
                new ControlItem { Name = "ChampionCard", DisplayName = "英雄卡片", Icon = "🃏", ViewName = "ChampionCardShowcaseView" },
                new ControlItem { Name = "RankBadge", DisplayName = "段位徽章", Icon = "🏆", ViewName = "RankBadgeShowcaseView" },
                new ControlItem { Name = "CurrencyDisplay", DisplayName = "货币显示", Icon = "💰", ViewName = "CurrencyDisplayShowcaseView" },
                new ControlItem { Name = "StatBar", DisplayName = "属性条", Icon = "📊", ViewName = "StatBarShowcaseView" },
                new ControlItem { Name = "DataGrid", DisplayName = "数据网格", Icon = "📊", ViewName = "DataGridShowcaseView" },
                new ControlItem { Name = "TreeView", DisplayName = "树形视图", Icon = "🌳", ViewName = "TreeViewShowcaseView" },
                new ControlItem { Name = "Expander", DisplayName = "展开器", Icon = "📂", ViewName = "ExpanderShowcaseView" },
                new ControlItem { Name = "RichTextBox", DisplayName = "富文本框", Icon = "📄", ViewName = "RichTextBoxShowcaseView" },
                new ControlItem { Name = "Calendar", DisplayName = "日历", Icon = "📅", ViewName = "CalendarShowcaseView" },
                new ControlItem { Name = "DatePicker", DisplayName = "日期选择器", Icon = "📆", ViewName = "DatePickerShowcaseView" },
                new ControlItem { Name = "ContextMenu", DisplayName = "上下文菜单", Icon = "☰", ViewName = "ContextMenuShowcaseView" },
                new ControlItem { Name = "StatusBar", DisplayName = "状态栏", Icon = "📊", ViewName = "StatusBarShowcaseView" },
                new ControlItem { Name = "Window", DisplayName = "窗口", Icon = "🪟", ViewName = "WindowShowcaseView" }
            };

            // 默认选中第一个
            if (ControlItems.Count > 0)
            {
                SelectedControl = ControlItems[0].ViewName;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// 控件项数据模型
    /// </summary>
    public class ControlItem
    {
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string ViewName { get; set; } = string.Empty;
    }
}

