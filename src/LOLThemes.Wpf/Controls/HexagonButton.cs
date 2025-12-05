using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 六边形按钮控件，显示为六边形形状的按钮。
    /// 常用于英雄联盟风格的界面设计。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:HexagonButton 
    ///     Content="技能"
    ///     Width="100"
    ///     Height="100"/&gt;
    /// </code>
    /// </example>
    public class HexagonButton : Button
    {
        static HexagonButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HexagonButton),
                new FrameworkPropertyMetadata(typeof(HexagonButton)));
        }

        /// <summary>
        /// 标识 <see cref="HexagonGeometry"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty HexagonGeometryProperty =
            DependencyProperty.Register(
                nameof(HexagonGeometry),
                typeof(Geometry),
                typeof(HexagonButton),
                new PropertyMetadata(CreateHexagonGeometry()));

        /// <summary>
        /// 获取或设置六边形的几何路径。
        /// 默认值为一个标准的六边形路径。
        /// </summary>
        public Geometry HexagonGeometry
        {
            get => (Geometry)GetValue(HexagonGeometryProperty);
            set => SetValue(HexagonGeometryProperty, value);
        }

        /// <summary>
        /// 创建默认的六边形几何路径。
        /// </summary>
        /// <returns>冻结的六边形几何路径</returns>
        private static Geometry CreateHexagonGeometry()
        {
            // 创建六边形路径
            var geometry = new PathGeometry();
            var figure = new PathFigure { StartPoint = new Point(50, 0) };
            figure.Segments.Add(new LineSegment(new Point(100, 25), true));
            figure.Segments.Add(new LineSegment(new Point(100, 75), true));
            figure.Segments.Add(new LineSegment(new Point(50, 100), true));
            figure.Segments.Add(new LineSegment(new Point(0, 75), true));
            figure.Segments.Add(new LineSegment(new Point(0, 25), true));
            figure.IsClosed = true;
            geometry.Figures.Add(figure);
            geometry.Freeze();
            return geometry;
        }
    }
}
