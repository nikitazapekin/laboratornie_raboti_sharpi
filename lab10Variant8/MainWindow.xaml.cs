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

        public MainWindow()
        {
            InitializeComponent();

            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
            FontWeightComboBox.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold };

            
            GraphBuilt += (sender, args) => DrawBernoulliLemniscate();
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
        /*
        private void DrawBernoulliLemniscate()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            Polyline lemniscate = new Polyline
            {
                Stroke = lineColor,
                StrokeThickness = lineWidth,
                RenderTransform = graphTransform,
                Cursor = Cursors.SizeAll
            };

            int numPoints = 1000;
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
            MainCanvas.Children.Add(BackgroundImage);
            MainCanvas.Children.Add(lemniscate);
            MainCanvas.Children.Add(GraphTitle);

     
          


             AddDiagonalPoints(maxX, maxY, minX, minY);
        }

    
        private void AddDiagonalPoints(double maxX, double maxY, double minX, double minY)
        {
            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            Ellipse point1 = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(point1, maxX + 0);    
            Canvas.SetTop(point1, maxY + 0);    
            MainCanvas.Children.Add(point1);

            Ellipse point2 = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(point2, minX - 10);   
            Canvas.SetTop(point2, minY - 10);    
            MainCanvas.Children.Add(point2);
        }
        */


        private void DrawBernoulliLemniscate()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = canvasWidth / 2;
            double centerY = canvasHeight / 2;

            Polyline lemniscate = new Polyline
            {
                Stroke = lineColor,
                StrokeThickness = lineWidth,
                RenderTransform = graphTransform,
                Cursor = Cursors.SizeAll
            };

            int numPoints = 1000;
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
            MainCanvas.Children.Add(BackgroundImage);
            MainCanvas.Children.Add(lemniscate);
            MainCanvas.Children.Add(GraphTitle);
 
            AddDiagonalPoints(maxX, maxY, minX, minY);
        }
        private bool isScalingTopRight = false;
        private bool isScalingBottomLeft = false;
        private Point previousMousePosition;
        /*
        private void AddDiagonalPoints(double maxX, double maxY, double minX, double minY)
        {
            Ellipse point1 = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red
            };

            // Привязываем координаты maxX и maxY
            Canvas.SetLeft(point1, maxX - point1.Width / 2);
            Canvas.SetTop(point1, maxY - point1.Height / 2);
            point1.RenderTransform = graphTransform; 
            MainCanvas.Children.Add(point1);

            Ellipse point2 = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Blue
            };

            // Привязываем координаты minX и minY
            Canvas.SetLeft(point2, minX - point2.Width / 2);
            Canvas.SetTop(point2, minY - point2.Height / 2);
            point2.RenderTransform = graphTransform;  
            MainCanvas.Children.Add(point2);
        }
        */
        private void AddDiagonalPoints(double maxX, double maxY, double minX, double minY)
        {
            Ellipse point1 = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Red
            };

            Canvas.SetLeft(point1, maxX - point1.Width / 2);
            Canvas.SetTop(point1, maxY - point1.Height / 2);
            point1.RenderTransform = graphTransform;

            point1.MouseDown += StartScaling;
            point1.MouseMove += PerformScaling;
            point1.MouseUp += StopScaling;

            MainCanvas.Children.Add(point1);

            Ellipse point2 = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Blue
            };

            Canvas.SetLeft(point2, minX - point2.Width / 2);
            Canvas.SetTop(point2, minY - point2.Height / 2);
            point2.RenderTransform = graphTransform;

            point2.MouseDown += StartScaling;
            point2.MouseMove += PerformScaling;
            point2.MouseUp += StopScaling;

            MainCanvas.Children.Add(point2);
        }








        private Ellipse currentScalingPoint = null; // Текущая активная точка
       // private Point previousMousePosition;

        private void StartScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            // Запоминаем активную точку и начальное положение мыши
            currentScalingPoint = sender as Ellipse;
            previousMousePosition = e.GetPosition(MainCanvas);

            // Захватываем управление мышью только для активной точки
            currentScalingPoint?.CaptureMouse();
        }

        private void PerformScaling(object sender, MouseEventArgs e)
        {
            if (currentScalingPoint == null || e.LeftButton != MouseButtonState.Pressed) return;

            // Вычисляем смещение мыши
            Point currentMousePosition = e.GetPosition(MainCanvas);
            double deltaY = currentMousePosition.Y - previousMousePosition.Y;

            // Определяем, какая точка активна
            bool isTopRight = currentScalingPoint.Fill == Brushes.Red; // Проверяем по цвету
            double scaleDelta = 1 + (deltaY / 100); // Настройка чувствительности

            // Ограничиваем изменение масштаба
            scaleDelta = Math.Max(0.9, Math.Min(1.1, scaleDelta));

            if (isTopRight)
            {
                scale *= scaleDelta;
            }
            else
            {
                scale /= scaleDelta;
            }

            // Обновляем положение мыши
            previousMousePosition = currentMousePosition;

            // Перерисовка графика
            DrawBernoulliLemniscate();
        }

        private void StopScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released) return;

            // Освобождаем управление мышью
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
                DrawBernoulliLemniscate();
            }
            else
            {
                if(GraphScaleTextBox.Text.Length>0)
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
            } else
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