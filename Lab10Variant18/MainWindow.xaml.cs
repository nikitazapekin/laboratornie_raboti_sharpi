using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab10Variant18
{
    public partial class MainWindow : Window
    {
        private double scale = 50;
        private Brush lineColor = Brushes.Orange;
        private double lineWidth = 4;
        private Ellipse currentScalingPoint = null;
        private Point previousMousePosition;

        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += (_, __) => DrawHyperbolicSpiral();
        }

        private void DrawHyperbolicSpiral()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = (canvasWidth / 2) - 200;
            double centerY = canvasHeight / 2;

            // Рисуем гиперболическую спираль
            Polyline hyperbolicSpiral = new Polyline
            {
                Stroke = lineColor,
                StrokeThickness = lineWidth
            };

            const double a = 1.0;
            const int numPoints = 1000;

            for (int i = 1; i <= numPoints; i++)
            {
                double phi = i * 0.01;
                double radius = a / phi;

                double x = radius * Math.Cos(phi) * scale + centerX;
                double y = -radius * Math.Sin(phi) * scale + centerY;

                hyperbolicSpiral.Points.Add(new Point(x, y));
            }

            MainCanvas.Children.Add(hyperbolicSpiral);

            // Добавляем точки
            AddScalingPoint(centerX - 100, centerY - 100, Brushes.Red); // Верхняя точка
            AddScalingPoint(centerX + 100, centerY + 100, Brushes.Blue); // Нижняя точка
        }

        private void AddScalingPoint(double x, double y, Brush color)
        {
            Ellipse point = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill = color
            };

            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);

            point.MouseDown += StartScaling;
            point.MouseMove += PerformScaling;
            point.MouseUp += StopScaling;

            MainCanvas.Children.Add(point);
        }

        private void StartScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            currentScalingPoint = sender as Ellipse;
            previousMousePosition = e.GetPosition(MainCanvas);

            currentScalingPoint?.CaptureMouse();
        }

        private void PerformScaling(object sender, MouseEventArgs e)
        {
            if (currentScalingPoint == null || e.LeftButton != MouseButtonState.Pressed) return;

            Point currentMousePosition = e.GetPosition(MainCanvas);
            double deltaY = currentMousePosition.Y - previousMousePosition.Y;

            double scaleFactor = 1.0 + (deltaY / 100); // Чем больше делитель, тем медленнее масштабирование

            if (Math.Abs(deltaY) > 0.1)
            {
                if (currentScalingPoint.Fill == Brushes.Red) // Верхняя точка
                {
                    if (deltaY < 0) scale *= scaleFactor; // Перетаскивание вверх — увеличение
                    else scale /= scaleFactor;           // Перетаскивание вниз — уменьшение
                }
                else if (currentScalingPoint.Fill == Brushes.Blue) // Нижняя точка
                {
                    if (deltaY > 0) scale *= scaleFactor; // Перетаскивание вниз — увеличение
                    else scale /= scaleFactor;           // Перетаскивание вверх — уменьшение
                }

                DrawHyperbolicSpiral();
                previousMousePosition = currentMousePosition;
            }
        }

        private void StopScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released) return;

            currentScalingPoint?.ReleaseMouseCapture();
            currentScalingPoint = null;
        }
    }
}
