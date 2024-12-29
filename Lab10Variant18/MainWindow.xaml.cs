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

            // Обновляем позиции точек
            UpdateScalingPoints(centerX, centerY);
        }

        private void UpdateScalingPoints(double centerX, double centerY)
        {
            double pointOffset = 100; // Начальное смещение точек от центра
         double pointAdjustmentFactor = scale / 250; // Изменение смещения в зависимости от масштаба
          //  double pointAdjustmentFactor = 0.85;
            // Обновляем верхнюю точку
            UpdatePoint(Brushes.Red, centerX - pointOffset * pointAdjustmentFactor, centerY - pointOffset * pointAdjustmentFactor);

            // Обновляем нижнюю точку
            UpdatePoint(Brushes.Blue, centerX + pointOffset * pointAdjustmentFactor, centerY + pointOffset * pointAdjustmentFactor);
        }

        private void UpdatePoint(Brush color, double x, double y)
        {
            Ellipse point = MainCanvas.Children.OfType<Ellipse>().FirstOrDefault(p => p.Fill == color);

            if (point == null)
            {
                point = new Ellipse
                {
                    Width = 40,
                    Height = 40,
                    Fill = color
                };
                point.MouseDown += StartScaling;
                point.MouseMove += PerformScaling;
                point.MouseUp += StopScaling;
                MainCanvas.Children.Add(point);
            }

            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);
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

            double scaleFactor = 1.0 + (deltaY / 100);

            if (Math.Abs(deltaY) > 0.1)
            {
                if (currentScalingPoint.Fill == Brushes.Red) // Верхняя точка
                {
                    if (deltaY < 0) scale *= scaleFactor; // Увеличение
                    else scale /= scaleFactor;           // Уменьшение
                }
                else if (currentScalingPoint.Fill == Brushes.Blue) // Нижняя точка
                {
                    if (deltaY > 0) scale *= scaleFactor; // Увеличение
                    else scale /= scaleFactor;           // Уменьшение
                }

                DrawHyperbolicSpiral(); // Перерисовываем график и обновляем точки
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
