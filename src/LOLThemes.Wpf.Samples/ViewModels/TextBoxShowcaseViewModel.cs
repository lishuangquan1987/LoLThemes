using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LOLThemes.Wpf.Samples.ViewModels
{
    /// <summary>
    /// 文本框展示视图模型
    /// </summary>
    public partial class TextBoxShowcaseViewModel : ObservableObject
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
