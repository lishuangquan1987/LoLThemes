using System.Windows;
using System.Windows.Controls;
using LOLThemes.Wpf.Samples.ViewModels;

namespace LOLThemes.Wpf.Samples.Views
{
    /// <summary>
    /// 控件展示页面模板选择器
    /// </summary>
    public class ControlShowcaseTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is string viewName && container is FrameworkElement element)
            {
                var templateKey = $"{viewName}Template";
                try
                {
                    return element.FindResource(templateKey) as DataTemplate;
                }
                catch
                {
                    // 如果找不到模板，返回空
                    return null;
                }
            }
            return null;
        }
    }
}

