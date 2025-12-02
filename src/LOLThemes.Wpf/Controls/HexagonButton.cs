using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOLThemes.Wpf.Controls
{
    public class HexagonButton : Button
    {
        static HexagonButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HexagonButton),
                new FrameworkPropertyMetadata(typeof(HexagonButton)));
        }

        // 六边形路径几何
        public static readonly DependencyProperty HexagonGeometryProperty =
            DependencyProperty.Register(
                nameof(HexagonGeometry),
                typeof(Geometry),
                typeof(HexagonButton),
                new PropertyMetadata(CreateHexagonGeometry()));

        public Geometry HexagonGeometry
        {
            get => (Geometry)GetValue(HexagonGeometryProperty);
            set => SetValue(HexagonGeometryProperty, value);
        }

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
