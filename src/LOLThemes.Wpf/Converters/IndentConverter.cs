using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace LOLThemes.Wpf.Converters
{
    /// <summary>
	/// 计算 <see cref="System.Windows.Controls.TreeViewItem"/> 的缩进的转换器。
	/// </summary>
	[ValueConversion(typeof(TreeViewItem), typeof(Thickness))]
    public sealed class IndentConverter : IValueConverter
    {
        /// <summary>
        /// 获取或设置缩进的像素个数。
        /// </summary>
        public double Indent { get; set; }
        /// <summary>
        /// 获取或设置初始的左边距。
        /// </summary>
        public double MarginLeft { get; set; }
        /// <summary>
        /// 转换值。
        /// </summary>
        /// <param name="value">绑定源生成的值。</param>
        /// <param name="targetType">绑定目标属性的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>转换后的值。如果该方法返回 <c>null</c>，则使用有效的 <c>null</c> 值。</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TreeViewItem item = value as TreeViewItem;
            if (item == null)
            {
                return new Thickness(0);
            }
            return new Thickness(this.MarginLeft + this.Indent * item.GetDepth(), 0, 0, 0);
        }
        /// <summary>
        /// 转换值。
        /// </summary>
        /// <param name="value">绑定目标生成的值。</param>
        /// <param name="targetType">要转换到的类型。</param>
        /// <param name="parameter">要使用的转换器参数。</param>
        /// <param name="culture">要用在转换器中的区域性。</param>
        /// <returns>转换后的值。如果该方法返回 <c>null</c>，则使用有效的 <c>null</c> 值。</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
	/// <see cref="System.Windows.Controls.TreeViewItem"/> 的扩展方法。
	/// </summary>
	public static class TreeViewItemExt
    {
        /// <summary>
        /// 返回指定 <see cref="System.Windows.Controls.TreeViewItem"/> 的深度。
        /// </summary>
        /// <param name="item">要获取深度的 <see cref="System.Windows.Controls.TreeViewItem"/> 对象。</param>
        /// <returns><see cref="System.Windows.Controls.TreeViewItem"/> 所在的深度。</returns>
        public static int GetDepth(this TreeViewItem item)
        {
            int depth = 0;
            while ((item = item.GetAncestor<TreeViewItem>()) != null)
            {
                depth++;
            }
            return depth;
        }
    }
    /// <summary>
	/// WPF 可视化树的扩展方法。
	/// </summary>
	internal static class VisualTreeEx
    {
        /// <summary>
        /// 返回指定对象的特定类型的祖先。
        /// </summary>
        /// <typeparam name="T">要获取的祖先的类型。</typeparam>
        /// <param name="source">获取的祖先，如果不存在则为 <c>null</c>。</param>
        /// <returns>获取的祖先对象。</returns>
        public static T GetAncestor<T>(this DependencyObject source)
            where T : DependencyObject
        {
            do
            {
                source = VisualTreeHelper.GetParent(source);
            } while (source != null && !(source is T));
            return source as T;
        }
    }
}
