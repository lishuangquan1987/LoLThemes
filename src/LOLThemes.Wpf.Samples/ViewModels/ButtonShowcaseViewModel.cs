using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    /// <summary>
    /// 按钮展示视图模型
    /// </summary>
    public partial class ButtonShowcaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isCodeExpanded = false;

        /// <summary>
        /// 切换代码展开命令
        /// </summary>
        [RelayCommand]
        private void ToggleCodeExpander()
        {
            IsCodeExpanded = !IsCodeExpanded;
        }
    }
}
