using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfLabs
{
    static class PointExtensions
    {
        public static Point ToMathCoordinates(this Point point, Canvas canvas, double scale)
        {
            return new Point(
                    (point.X - canvas.ActualWidth / 2) / scale,
                    (canvas.ActualHeight / 2 - point.Y) / scale
                );
        }

        public static Point ToUiCoordinates(this Point point, Canvas canvas, double scale)
        {
            return new Point(
                    (point.X * scale + canvas.ActualWidth / 2),
                    (canvas.ActualHeight / 2 - point.Y * scale)
                );
        }
    }

    class CanvasDrawer
    {
        private readonly Canvas _canvas;
        private readonly double _scale;
        private readonly Point _xAxisStart, _xAxisEnd, _yAxisStart, _yAxisEnd;

        public CanvasDrawer(Canvas canvas, double xStart, double xEnd, double step, double scale)
        {
            _canvas = canvas;
            _xAxisStart = new Point((int)(_canvas.ActualWidth / 2), 0);
            _xAxisEnd = new Point((int)_canvas.ActualWidth / 2, (int)_canvas.ActualHeight);

            _yAxisStart = new Point(0, (int)(_canvas.ActualHeight / 2));
            _yAxisEnd = new Point((int)_canvas.ActualWidth, (int)(_canvas.ActualHeight / 2));

            _scale = scale;
        }

        public void DrawAxis()
        {
            DrawLine(_xAxisStart, _xAxisEnd, Brushes.Black, 2);
            DrawLine(_yAxisStart, _yAxisEnd, Brushes.Black, 2);
        }

        private void DrawLine(Point start, Point end, Brush color, double thickness)
        {
            Line line = new Line
            {
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                Stroke = color,
                StrokeThickness = thickness
            };

            _canvas.Children.Add(line);
        }

        public void DrawGraph(List<Point> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                DrawLine(points[i].ToUiCoordinates(_canvas, _scale), points[i + 1].ToUiCoordinates(_canvas, _scale), Brushes.Red, 2);
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                DrawPoint(points[i].ToUiCoordinates(_canvas, _scale), Brushes.DarkRed);
            }
            DrawPoint(points[^1].ToUiCoordinates(_canvas, _scale), Brushes.DarkRed);
        }

        private void DrawPoint(Point point, Brush color)
        {
            double thickness = 1;
            double size = 4;

            Ellipse ellipse = new Ellipse()
            {
                Stroke = color,
                StrokeThickness = thickness,
                Fill = color,
                Width = size,
                Height = size
            };

            Canvas.SetLeft(ellipse, point.X - size / 2);
            Canvas.SetTop(ellipse, point.Y - size / 2);

            _canvas.Children.Add(ellipse);
        }
    }
}