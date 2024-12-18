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

        private void EditTextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Collapsed;
            TextEditor.Visibility = Visibility.Visible;
        }

        private void EditPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isDraggingPanel = true;
                panelStartPoint = e.GetPosition(this);
                EditPanel.CaptureMouse();
            }
        }

        private void EditPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingPanel)
            {
                Point currentPoint = e.GetPosition(this);
                double offsetX = currentPoint.X - panelStartPoint.X;
                double offsetY = currentPoint.Y - panelStartPoint.Y;

                double currentLeft = Canvas.GetLeft(EditPanel);
                double currentTop = Canvas.GetTop(EditPanel);

                Canvas.SetLeft(EditPanel, currentLeft + offsetX);
                Canvas.SetTop(EditPanel, currentTop + offsetY);

                panelStartPoint = currentPoint;
            }
        }

        private void EditPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDraggingPanel)
            {
                isDraggingPanel = false;
                EditPanel.ReleaseMouseCapture();
            }
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


        private void GraphScaleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(GraphScaleTextBox.Text, out double newScale))
            {
                scale = newScale;
                DrawBernoulliLemniscate();
            }
            else
            {
                if (GraphScaleTextBox.Text.Length > 0)
                {

                    MessageBox.Show("Введите корректное значение!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void LineColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = (LineColorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            lineColor = selectedColor switch
            {
                "Синий" => Brushes.Blue,
                "Красный" => Brushes.Red,
                "Зеленый" => Brushes.Green,
                _ => Brushes.Blue
            };
            DrawBernoulliLemniscate();
        }

        private void LineWidthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(LineWidthTextBox.Text, out double newLineWidth))
            {
                lineWidth = newLineWidth;
                DrawBernoulliLemniscate();
            }
        }

        private void BuildGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {

            DrawBernoulliLemniscate();
            MessageBox.Show("График построен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void TextColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = (TextColorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            GraphTitle.Foreground = selectedColor switch
            {
                "Черный" => Brushes.Black,
                "Синий" => Brushes.Blue,
                "Красный" => Brushes.Red,
                _ => Brushes.Black
            };
        }

        private void FontSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(FontSizeTextBox.Text, out double newSize))
            {
                GraphTitle.FontSize = newSize;
            }
            else
            {
                if (FontSizeTextBox.Text.Length > 0)
                {
                    MessageBox.Show("Введите корректное значение!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void GraphTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GraphTitle.Text = GraphTitleTextBox.Text;
        }

        private void AddBackgroundMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var imageSource = new ImageSourceConverter().ConvertFromString(openFileDialog.FileName) as ImageSource;
                    if (imageSource != null)
                    {
                        BackgroundImage.Source = imageSource;
                        BackgroundImage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка загрузки изображения.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem is FontFamily selectedFont)
            {
                GraphTitle.FontFamily = selectedFont;
            }
        }

        private void FontWeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontWeightComboBox.SelectedItem is FontWeight selectedWeight)
            {
                GraphTitle.FontWeight = selectedWeight;
            }
        }

        private void SaveToPngMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveCanvasAsPng(MainCanvas);
        }

        private void SaveCanvasAsPng(Canvas canvas)
        {
            RenderTargetBitmap renderTarget = new RenderTargetBitmap(
                (int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTarget.Render(canvas);

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PNG Files|*.png",
                FileName = "Graph.png"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                    encoder.Save(fs);
                }

                MessageBox.Show("График сохранен как PNG", "Сохранение завершено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}