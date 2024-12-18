

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab10Variant8
{
    public partial class MainWindow : Window
    {
        private bool isDraggingPanel = false;
        private Point panelStartPoint;

        private bool isDraggingGraph = false;
        private Point graphStartPoint;
        private Polyline selectedGraph;
        private TranslateTransform graphTransform = new TranslateTransform();

        private const double c = 0.5;
        private double scale = 100;
        private Brush lineColor = Brushes.Blue;
        private double lineWidth = 2;

        public delegate void GraphBuiltEventHandler(object sender, EventArgs e);
        public event GraphBuiltEventHandler GraphBuilt;


        private bool isDragging = false;
        private bool isScaling = false;
        private Point lastMousePosition;
        private Point previousMousePosition;
        //  private Polyline selectedGraph;


        private double minX, minY, maxX, maxY;


        public MainWindow()
        {
            InitializeComponent();

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
            FontWeightComboBox.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold };


            GraphBuilt += (sender, args) => DrawBernoulliLemniscate();

        }
        private void DrawBernoulliLemniscate()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            Polyline lemniscate = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                RenderTransform = new ScaleTransform(1, 1),
                Cursor = Cursors.SizeAll
            };

            int numPoints = 1000;
            double scale = 1; // Масштабирование графика
            double c = 50; // Константа лемнискаты

            double maxX = double.MinValue;
            double maxY = double.MinValue;

            double minX = double.MaxValue;
            double minY = double.MaxValue;



            for (int i = 0; i <= numPoints; i++)
            {
                double phi = 2 * Math.PI * i / numPoints;
                double cos2phi = Math.Cos(2 * phi);
                if (cos2phi < 0) continue;

                double radius = Math.Sqrt(2 * c * c * cos2phi);
                double x = radius * Math.Cos(phi) * scale + centerX;
                double y = -radius * Math.Sin(phi) * scale + centerY;



                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);

                minX = Math.Min(minX, x);
                minY = Math.Min(minY, y);


                lemniscate.Points.Add(new Point(x, y));
            }

            lemniscate.MouseDown += Graph_MouseDown;
            lemniscate.MouseMove += Graph_MouseMove;
            lemniscate.MouseUp += Graph_MouseUp;

            selectedGraph = lemniscate;
            MainCanvas.Children.Add(lemniscate);
            AddDiagonalPoints(maxX, maxY, minX, minY);

        }

        /*  private void AddDiagonalPoints(double maxX, double maxY, double minX, double minY)
          {
              AddPoint(maxX, maxY, Brushes.Red, graphTransform, MainCanvas);
              AddPoint(minX, minY, Brushes.Blue, graphTransform, MainCanvas);
          }

          */


        private void AddDiagonalPoints(double maxX, double maxY, double minX, double minY)
        {
            AddPoint(maxX, maxY, Brushes.Red, graphTransform, MainCanvas, "MaxPoint");
            AddPoint(minX, minY, Brushes.Blue, graphTransform, MainCanvas, "MinPoint");
        }

        /*

        private void AddPoint(double x, double y, Brush color, Transform transform, Canvas canvas)
        {
            Ellipse point = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = color
            };

            // Привязка к координатам графика
            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);
            point.RenderTransform = transform;

            // Добавление обработчиков событий
            point.MouseDown += StartScaling;
            point.MouseMove += PerformScaling;
            point.MouseUp += StopScaling;

            // Добавление на канвас
            canvas.Children.Add(point);
        }
        */
        private void AddPoint(double x, double y, Brush color, Transform transform, Canvas canvas, string tag)
        {
            Ellipse point = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = color,
                Tag = tag // Добавляем тег для идентификации
            };

            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);
            point.RenderTransform = transform;

            point.MouseDown += StartScaling;
            point.MouseMove += PerformScaling;
            point.MouseUp += StopScaling;

            canvas.Children.Add(point);
        }






        private void StartScaling(object sender, MouseButtonEventArgs e)
        {
            isScaling = true;
            previousMousePosition = e.GetPosition(MainCanvas);
            Mouse.Capture((UIElement)sender);
        }
        private void PerformScaling(object sender, MouseEventArgs e)
        {
            if (isScaling && selectedGraph != null)
            {
                Point currentMousePosition = e.GetPosition(MainCanvas);
                Vector delta = currentMousePosition - previousMousePosition;

                double scaleDelta = 1 + delta.Y / 100; // Изменение масштаба
                scaleDelta = Math.Max(0.5, scaleDelta); // Ограничение минимального масштаба

                RescaleGraph(scaleDelta);
                previousMousePosition = currentMousePosition;
            }
        }

        /*
        private void RescaleGraph(double scaleDelta)
        {
            if (selectedGraph != null)
            {
                Point center = new Point(MainCanvas.ActualWidth / 2, MainCanvas.ActualHeight / 2);
                var updatedPoints = new PointCollection();

                foreach (var point in selectedGraph.Points)
                {
                    double offsetX = point.X - center.X;
                    double offsetY = point.Y - center.Y;

                    double newX = center.X + offsetX * scaleDelta;
                    double newY = center.Y + offsetY * scaleDelta;

                    updatedPoints.Add(new Point(newX, newY));
                }

                selectedGraph.Points = updatedPoints;
            }
        }
        */


        private void RescaleGraph(double scaleDelta)
        {
            if (selectedGraph != null)
            {
                Point center = new Point(MainCanvas.ActualWidth / 2, MainCanvas.ActualHeight / 2);
                var updatedPoints = new PointCollection();

                foreach (var point in selectedGraph.Points)
                {
                    double offsetX = point.X - center.X;
                    double offsetY = point.Y - center.Y;

                    double newX = center.X + offsetX * scaleDelta;
                    double newY = center.Y + offsetY * scaleDelta;

                    updatedPoints.Add(new Point(newX, newY));
                }

                selectedGraph.Points = updatedPoints;

                // Обновление крайних точек
                UpdateEdgePoints(scaleDelta);
            }
        }




        private void StopScaling(object sender, MouseButtonEventArgs e)
        {
            isScaling = false;
            Mouse.Capture(null);
        }



        private void UpdateEdgePoints(double scaleDelta)
        {
            Point center = new Point(MainCanvas.ActualWidth / 2, MainCanvas.ActualHeight / 2);

            // Пересчет крайних точек
            minX = center.X + (minX - center.X) * scaleDelta;
            minY = center.Y + (minY - center.Y) * scaleDelta;

            maxX = center.X + (maxX - center.X) * scaleDelta;
            maxY = center.Y + (maxY - center.Y) * scaleDelta;

            // Перемещение графических представлений крайних точек
            foreach (UIElement child in MainCanvas.Children)
            {
                if (child is Ellipse point)
                {
                    if ((string)point.Tag == "MinPoint")
                    {
                        Canvas.SetLeft(point, minX - point.Width / 2);
                        Canvas.SetTop(point, minY - point.Height / 2);
                    }
                    else if ((string)point.Tag == "MaxPoint")
                    {
                        Canvas.SetLeft(point, maxX - point.Width / 2);
                        Canvas.SetTop(point, maxY - point.Height / 2);
                    }
                }
            }
        }





        private void Graph_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition(MainCanvas);
            Mouse.Capture((UIElement)sender);
        }

        private void Graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedGraph != null)
            {
                Point currentMousePosition = e.GetPosition(MainCanvas);
                Vector delta = currentMousePosition - lastMousePosition;

                TranslateTransform translateTransform = new TranslateTransform(delta.X, delta.Y);
                selectedGraph.RenderTransform = new TransformGroup
                {
                    Children = { selectedGraph.RenderTransform, translateTransform }
                };

                lastMousePosition = currentMousePosition;
            }
        }

        private void Graph_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Mouse.Capture(null);
        }

        private void EditGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Visible;
            TextEditor.Visibility = Visibility.Collapsed;

            DrawBernoulliLemniscate();
        }



        private void BuildGraphButton_Click(object sender, RoutedEventArgs e)
        {
            OnGraphBuilt();
            MessageBox.Show("График построен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnGraphBuilt()
        {
            GraphBuilt?.Invoke(this, EventArgs.Empty);
        }



        private void BuildGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {

            DrawBernoulliLemniscate();
            MessageBox.Show("График построен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}




/*
 * using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab10Variant8
{
    public partial class MainWindow : Window
    {
        private bool isDraggingPanel = false;
        private Point panelStartPoint;

        private bool isDraggingGraph = false;
        private Point graphStartPoint;
        private Polyline selectedGraph;
        private TranslateTransform graphTransform = new TranslateTransform();

        private const double c = 0.5;
        private double scale = 100;
        private Brush lineColor = Brushes.Blue;
        private double lineWidth = 2;

        public delegate void GraphBuiltEventHandler(object sender, EventArgs e);
        public event GraphBuiltEventHandler GraphBuilt;


        private bool isDragging = false;
        private bool isScaling = false;
        private Point lastMousePosition;
        private Point previousMousePosition;
        //  private Polyline selectedGraph;




        public MainWindow()
        {
            InitializeComponent();

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
            FontWeightComboBox.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold };


            GraphBuilt += (sender, args) => DrawBernoulliLemniscate();

        }
        private void DrawBernoulliLemniscate()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            Polyline lemniscate = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                RenderTransform = new ScaleTransform(1, 1),
                Cursor = Cursors.SizeAll
            };

            int numPoints = 1000;
            double scale = 1; // Масштабирование графика
            double c = 50; // Константа лемнискаты

            double maxX = double.MinValue;
            double maxY = double.MinValue;

            double minX = double.MaxValue;
            double minY = double.MaxValue;



            for (int i = 0; i <= numPoints; i++)
            {
                double phi = 2 * Math.PI * i / numPoints;
                double cos2phi = Math.Cos(2 * phi);
                if (cos2phi < 0) continue;

                double radius = Math.Sqrt(2 * c * c * cos2phi);
                double x = radius * Math.Cos(phi) * scale + centerX;
                double y = -radius * Math.Sin(phi) * scale + centerY;



                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);

                minX = Math.Min(minX, x);
                minY = Math.Min(minY, y);


                lemniscate.Points.Add(new Point(x, y));
            }

            lemniscate.MouseDown += Graph_MouseDown;
            lemniscate.MouseMove += Graph_MouseMove;
            lemniscate.MouseUp += Graph_MouseUp;

            selectedGraph = lemniscate;
            MainCanvas.Children.Add(lemniscate);
            AddDiagonalPoints(maxX, maxY, minX, minY);

        }

        private void AddDiagonalPoints(double maxX, double maxY, double minX, double minY)
        {
            AddPoint(maxX, maxY, Brushes.Red, graphTransform, MainCanvas);
            AddPoint(minX, minY, Brushes.Blue, graphTransform, MainCanvas);
        }


        private void AddPoint(double x, double y, Brush color, Transform transform, Canvas canvas)
        {
            Ellipse point = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = color
            };

            // Привязка к координатам графика
            Canvas.SetLeft(point, x - point.Width / 2);
            Canvas.SetTop(point, y - point.Height / 2);
            point.RenderTransform = transform;

            // Добавление обработчиков событий
            point.MouseDown += StartScaling;
            point.MouseMove += PerformScaling;
            point.MouseUp += StopScaling;

            // Добавление на канвас
            canvas.Children.Add(point);
        }

        private void StartScaling(object sender, MouseButtonEventArgs e)
        {
            isScaling = true;
            previousMousePosition = e.GetPosition(MainCanvas);
            Mouse.Capture((UIElement)sender);
        }

        private void PerformScaling(object sender, MouseEventArgs e)
        {
            if (isScaling && selectedGraph != null)
            {
                Point currentMousePosition = e.GetPosition(MainCanvas);
                Vector delta = currentMousePosition - previousMousePosition;

                double scaleDelta = 1 + delta.Y / 100; // Изменение масштаба
                scaleDelta = Math.Max(0.5, scaleDelta); // Ограничение минимального масштаба

                ScaleTransform scaleTransform = (ScaleTransform)selectedGraph.RenderTransform;
                scaleTransform.ScaleX *= scaleDelta;
                scaleTransform.ScaleY *= scaleDelta;

                previousMousePosition = currentMousePosition;
            }
        }

        private void StopScaling(object sender, MouseButtonEventArgs e)
        {
            isScaling = false;
            Mouse.Capture(null);
        }


        private void Graph_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition(MainCanvas);
            Mouse.Capture((UIElement)sender);
        }

        private void Graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedGraph != null)
            {
                Point currentMousePosition = e.GetPosition(MainCanvas);
                Vector delta = currentMousePosition - lastMousePosition;

                TranslateTransform translateTransform = new TranslateTransform(delta.X, delta.Y);
                selectedGraph.RenderTransform = new TransformGroup
                {
                    Children = { selectedGraph.RenderTransform, translateTransform }
                };

                lastMousePosition = currentMousePosition;
            }
        }

        private void Graph_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Mouse.Capture(null);
        }

        private void EditGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Visible;
            TextEditor.Visibility = Visibility.Collapsed;

            DrawBernoulliLemniscate();
        }

    
     
        private void BuildGraphButton_Click(object sender, RoutedEventArgs e)
        {
            OnGraphBuilt();
            MessageBox.Show("График построен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnGraphBuilt()
        {
            GraphBuilt?.Invoke(this, EventArgs.Empty);
        }


     
        private void BuildGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {

            DrawBernoulliLemniscate();
            MessageBox.Show("График построен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}

*/