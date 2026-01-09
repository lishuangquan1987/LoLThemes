using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LOLThemes.Wpf.Helpers
{
    /// <summary>
    /// TreeView 帮助类，提供层级计算等功能
    /// </summary>
    public static class TreeViewHelper
    {
        /// <summary>
        /// Level 附加属性标识
        /// </summary>
        public static readonly DependencyProperty LevelProperty = 
            DependencyProperty.RegisterAttached(
                "Level",
                typeof(int),
                typeof(TreeViewHelper),
                new PropertyMetadata(0, OnLevelChanged)
            );

        /// <summary>
        /// 获取节点的层级
        /// </summary>
        /// <param name="obj">TreeViewItem 对象</param>
        /// <returns>节点的层级（从0开始）</returns>
        public static int GetLevel(DependencyObject obj)
        {
            return (int)obj.GetValue(LevelProperty);
        }

        /// <summary>
        /// 设置节点的层级
        /// </summary>
        /// <param name="obj">TreeViewItem 对象</param>
        /// <param name="value">节点的层级</param>
        public static void SetLevel(DependencyObject obj, int value)
        {
            obj.SetValue(LevelProperty, value);
        }

        /// <summary>
        /// 当层级属性变化时的处理函数
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">依赖属性变化事件参数</param>
        private static void OnLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 层级变化时自动更新子节点的层级
            if (d is TreeViewItem item)
            {
                UpdateChildLevels(item);
            }
        }

        /// <summary>
        /// 为 TreeView 添加层级计算支持
        /// </summary>
        /// <param name="treeView">TreeView 控件</param>
        public static void InitializeLevelSupport(TreeView treeView)
        {
            treeView.ItemContainerGenerator.StatusChanged += (sender, args) =>
            {
                if (treeView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                {
                    UpdateAllLevels(treeView);
                }
            };
        }

        /// <summary>
        /// 更新 TreeView 中所有节点的层级
        /// </summary>
        /// <param name="treeView">TreeView 控件</param>
        public static void UpdateAllLevels(TreeView treeView)
        {
            // 更新顶层节点的层级
            for (int i = 0; i < treeView.Items.Count; i++)
            {
                if (treeView.ItemContainerGenerator.ContainerFromIndex(i) is TreeViewItem item)
                {
                    SetLevel(item, 0);
                }
            }
        }

        /// <summary>
        /// 更新子节点的层级
        /// </summary>
        /// <param name="parentItem">父节点</param>
        public static void UpdateChildLevels(TreeViewItem parentItem)
        {
            int parentLevel = GetLevel(parentItem);
            
            // 更新所有子节点的层级
            for (int i = 0; i < parentItem.Items.Count; i++)
            {
                if (parentItem.ItemContainerGenerator.ContainerFromIndex(i) is TreeViewItem childItem)
                {
                    SetLevel(childItem, parentLevel + 1);
                }
            }
        }

        /// <summary>
        /// 获取父 TreeViewItem
        /// </summary>
        /// <param name="item">当前 TreeViewItem</param>
        /// <returns>父 TreeViewItem，如果没有则返回 null</returns>
        public static TreeViewItem? GetParentItem(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            
            while (parent != null && !(parent is TreeViewItem))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            
            return parent as TreeViewItem;
        }
    }
}