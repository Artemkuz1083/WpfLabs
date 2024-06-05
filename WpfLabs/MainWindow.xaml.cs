using Rpn.Logic;
using System.Windows;
using System.Windows.Input;

namespace WpfLabs
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CanvasGraph.Children.Clear();
            GivePoint();
        }

        private void CanvasGraph_MouseMove(object sender, MouseEventArgs e)
        {
            var uiPoint = Mouse.GetPosition(CanvasGraph);
            double scale = 1;
            if(tbScale.Text != string.Empty)
            {
                scale = Convert.ToDouble(tbScale.Text);
            }           
            var mathPoint = Mouse.GetPosition(CanvasGraph).ToMathCoordinates(CanvasGraph, scale);

            lblUiCoordinates.Content = $"{uiPoint.X:0.#}; {uiPoint.Y:0.#}";
            lblMathCoordinates.Content = $"{mathPoint.X:0.#}; {mathPoint.Y:0.#}";
        }


        private void GivePoint()
        {
            var expression = tbExpInput.Text;
            var start = Convert.ToDouble(tbStart.Text);
            var end = Convert.ToDouble(tbEnd.Text);
            var step = Convert.ToDouble(tbStep.Text);
            var scale = Convert.ToDouble(tbScale.Text);
            var canvasGraph = CanvasGraph;

            var canvasDrawer = new CanvasDrawer(canvasGraph, start, end, step, scale);
            canvasDrawer.DrawAxis();

            List<Point> points = new List<Point>();

            for (double x = start; x <= end; x += step)
            {
                var y = Convert.ToDouble(new RpnCalculator(expression, x).result);
                if (!double.IsInfinity(y))
                {
                    points.Add(new Point(x, y));
                }
            }

            canvasDrawer.DrawGraph(points);
        }
    }
}
