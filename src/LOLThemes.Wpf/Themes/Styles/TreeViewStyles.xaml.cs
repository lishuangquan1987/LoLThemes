using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using LOLThemes.Wpf.Helpers;

namespace LOLThemes.Wpf.Themes.Styles
{
    /// <summary>
    /// TreeViewStyles.xaml 的代码隐藏文件
    /// </summary>
    public partial class TreeViewStyles : ResourceDictionary
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeViewStyles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// TreeViewItem 加载时的事件处理程序
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">路由事件参数</param>
        private void TreeViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TreeViewItem item)
            {
                // 找到父 TreeViewItem
                TreeViewItem? parentItem = TreeViewHelper.GetParentItem(item);
                
                // 设置当前节点的层级
                if (parentItem != null)
                {
                    int parentLevel = TreeViewHelper.GetLevel(parentItem);
                    TreeViewHelper.SetLevel(item, parentLevel + 1);
                }
                else
                {
                    // 顶层节点，层级为 0
                    TreeViewHelper.SetLevel(item, 0);
                }
                
                // 订阅父节点的 ItemsChanged 事件，以便在展开/折叠时更新子节点层级
                item.ItemContainerGenerator.StatusChanged += (s, args) =>
                {
                    if (item.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                    {
                        TreeViewHelper.UpdateChildLevels(item);
                    }
                };
            }
        }
    }
}