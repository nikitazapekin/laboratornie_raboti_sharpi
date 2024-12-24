using System;
using System.IO;
using System.Linq;
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
        //private double scale = 100;
  
        //private double lineWidth = 2;



        private double scale = 1;  
        private Brush lineColor = Brushes.Orange;  
        private double lineWidth = 40;  
        private double a = 5.0; 





        public delegate void GraphBuiltEventHandler(object sender, EventArgs e);
        public event GraphBuiltEventHandler GraphBuilt;

        private double maxX;
        private double maxY;
        private double minX;
        private double minY;

        public MainWindow()
        {
            InitializeComponent();

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
       //     FontWeightComboBox.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold };

            GraphBuilt += (sender, args) => DrawSpiralOfGalileo();
            MainCanvas.Children.Clear();
        }

        private void EditGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
           // EditPanel.Visibility = Visibility.Visible;
         //   GraphEditor.Visibility = Visibility.Visible;
         //   TextEditor.Visibility = Visibility.Collapsed;

            DrawSpiralOfGalileo();
        }

        private void EditTextMenuItem_Click(object sender, RoutedEventArgs e)
        {
         /*   EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Collapsed;
            TextEditor.Visibility = Visibility.Visible;
         */
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

 
        private void DrawSpiralOfGalileo()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            Polyline spiral = new Polyline
            {
                Stroke = lineColor,
                StrokeThickness = lineWidth
            };

            int numPoints = 1000; // Количество точек для графика
            double maxPhi = 10 * Math.PI; // Максимальное значение φ

            for (int i = 0; i <= numPoints; i++)
            {
                double phi = i * maxPhi / numPoints; // Текущий угол φ
                double radius = a * phi; // Радиус r = aφ - l (l >= 0)

                if (radius < 0) continue; // Пропускаем отрицательные значения радиуса

                double x = radius * Math.Cos(phi) * scale + centerX;
                double y = -radius * Math.Sin(phi) * scale + centerY;

                spiral.Points.Add(new Point(x, y));
            }

          spiral.MouseDown += Graph_MouseDown;
          spiral.MouseMove += Graph_MouseMove;
        spiral.MouseUp += Graph_MouseUp;
            selectedGraph = spiral; // Назначаем текущий график переменной selectedGraph

            MainCanvas.Children.Add(BackgroundImage);
            MainCanvas.Children.Add(spiral);
            MainCanvas.Children.Add(GraphTitle);
        }




        private bool isScalingTopRight = false;
        private bool isScalingBottomLeft = false;
        private Point previousMousePosition;
   

        private Ellipse currentScalingPoint = null;


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

            double scaleFactor = 1.0 + (deltaY / 25);

            if (Math.Abs(deltaY) > 0.1)
            {
                bool isTopRight = currentScalingPoint.Fill == Brushes.Red;
                if (isTopRight)
                {
                    scale *= scaleFactor;
                }
                else
                {
                    scale /= scaleFactor;
                }


                DrawSpiralOfGalileo();

                previousMousePosition = currentMousePosition;
            }
        }

        private void UpdateDiagonalPoints(double maxX, double maxY, double minX, double minY)
        {
        
            double centerX = MainCanvas.ActualWidth / 2;
            double centerY = MainCanvas.ActualHeight / 2;
 
            double adjustmentFactor = 0.85;
 
            Ellipse point1 = MainCanvas.Children.OfType<Ellipse>().FirstOrDefault(p => p.Fill == Brushes.Red);
            if (point1 == null)
            {
                point1 = new Ellipse
                {
                    Width = 40,
                    Height = 40,
                    Fill = Brushes.Red,
                    RenderTransform = graphTransform
                };
                point1.MouseDown += StartScaling;
                point1.MouseMove += PerformScaling;
                point1.MouseUp += StopScaling;
                MainCanvas.Children.Add(point1);
            }
        
            double point1X = centerX + (maxX - centerX) * adjustmentFactor;
            double point1Y = centerY + (maxY - centerY) * adjustmentFactor;
            Canvas.SetLeft(point1, point1X - point1.Width / 2);
            Canvas.SetTop(point1, point1Y - point1.Height / 2);

           
            Ellipse point2 = MainCanvas.Children.OfType<Ellipse>().FirstOrDefault(p => p.Fill == Brushes.Blue);
            if (point2 == null)
            {
                point2 = new Ellipse
                {
                    Width = 40,
                    Height = 40,
                    Fill = Brushes.Blue,
                    RenderTransform = graphTransform
                };
                point2.MouseDown += StartScaling;
                point2.MouseMove += PerformScaling;
                point2.MouseUp += StopScaling;
                MainCanvas.Children.Add(point2);
            }
        
            double point2X = centerX + (minX - centerX) * adjustmentFactor;
            double point2Y = centerY + (minY - centerY) * adjustmentFactor;
            Canvas.SetLeft(point2, point2X - point2.Width / 2);
            Canvas.SetTop(point2, point2Y - point2.Height / 2);
        }


        private void StopScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released) return;


            currentScalingPoint?.ReleaseMouseCapture();
            currentScalingPoint = null;
        }



        private Brush originalGraphColor;

        private void Graph_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                originalGraphColor = selectedGraph.Stroke;
                selectedGraph.Stroke = Brushes.Red;

                isDraggingGraph = true;
                graphStartPoint = e.GetPosition(MainCanvas);
                selectedGraph.CaptureMouse();
            }
        }

        private void Graph_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingGraph)
            {
                Point currentPoint = e.GetPosition(MainCanvas);
                double offsetX = currentPoint.X - graphStartPoint.X;
                double offsetY = currentPoint.Y - graphStartPoint.Y;

                graphTransform.X += offsetX;
                graphTransform.Y += offsetY;

                graphStartPoint = currentPoint;

                selectedGraph.Stroke = Brushes.Green;
            }
        }

        private void Graph_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDraggingGraph)
            {
                selectedGraph.Stroke = originalGraphColor;

                isDraggingGraph = false;
                selectedGraph.ReleaseMouseCapture();
            }
        }

        private void GraphScaleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(GraphScaleTextBox.Text, out double newScale))
            {
                scale = newScale;
                DrawSpiralOfGalileo();
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
            DrawSpiralOfGalileo();
        }

        private void LineWidthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(LineWidthTextBox.Text, out double newLineWidth))
            {
                lineWidth = newLineWidth;
                DrawSpiralOfGalileo();
            }
        }

        private void BuildGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DrawSpiralOfGalileo();
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