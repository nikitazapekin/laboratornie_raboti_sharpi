﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab10Variant18
{
    public partial class MainWindow : Window
    {
        private double scale = 250;
        private Brush lineColor = Brushes.Orange;
        private double lineWidth = 4;
        private Ellipse currentScalingPoint = null;
        private Point previousMousePosition;
        private double offsetX = 0, offsetY = 0;
        private double greenPointX, greenPointY;



        public delegate void GraphBuiltEventHandler(object sender, EventArgs e);
        public event GraphBuiltEventHandler GraphBuilt;



        public MainWindow()
        {
            InitializeComponent();


            GraphBuilt += (sender, args) => DrawHyperbolicSpiral();
            TextFont.ItemsSource = Fonts.SystemFontFamilies;
            TextWeight.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold, FontWeights.UltraBold };


        }

        private void DrawHyperbolicSpiral()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = (canvasWidth / 2) - 200 + offsetX;
            double centerY = canvasHeight / 2 + offsetY;


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
            MainCanvas.Children.Add(BackgroundImage);
            MainCanvas.Children.Add(hyperbolicSpiral);
            MainCanvas.Children.Add(GraphTitle);

            UpdateScalingPoints(centerX, centerY);
        }

        private void UpdateScalingPoints(double centerX, double centerY)
        {

            double phi1 = 2.0;
            double phi2 = 4.0;


            const double a = 1.0;
            double radius1 = a / phi1;
            double radius2 = a / phi2;


            double point1X = radius1 * Math.Cos(phi1) * scale + centerX;
            double point1Y = -radius1 * Math.Sin(phi1) * scale + centerY;

            double point2X = radius2 * Math.Cos(phi2) * scale + centerX;
            double point2Y = -radius2 * Math.Sin(phi2) * scale + centerY;

            UpdatePoint(Brushes.Red, point1X, point1Y);
            UpdatePoint(Brushes.Blue, point2X, point2Y);


            double greenPointPhi = 3.0;
            double greenPointRadius = a / greenPointPhi;
            greenPointX = greenPointRadius * Math.Cos(greenPointPhi) * scale + centerX;
            greenPointY = -greenPointRadius * Math.Sin(greenPointPhi) * scale + centerY;


            UpdatePoint(Brushes.Green, greenPointX, greenPointY);
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
                if (currentScalingPoint.Fill == Brushes.Red)
                {
                    if (deltaY < 0) scale *= scaleFactor;
                    else scale /= scaleFactor;
                    lineColor = Brushes.Orange;
                }
                else if (currentScalingPoint.Fill == Brushes.Blue)
                {
                    if (deltaY > 0) scale *= scaleFactor;
                    else scale /= scaleFactor;
                    lineColor = Brushes.Orange;
                }

                if (currentScalingPoint.Fill == Brushes.Green)
                {
                    offsetX += currentMousePosition.X - previousMousePosition.X;
                    offsetY += currentMousePosition.Y - previousMousePosition.Y;
                    lineColor = Brushes.Red;
                }

                DrawHyperbolicSpiral();
                previousMousePosition = currentMousePosition;
            }
        }

        private void StopScaling(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released) return;


            lineColor = Brushes.Orange;

            currentScalingPoint?.ReleaseMouseCapture();
            currentScalingPoint = null;
            DrawHyperbolicSpiral();
        }







        private void handleDraw(object sender, RoutedEventArgs e)
        {
            DrawHyperbolicSpiral();
        }



        private void drawDelegate(object sender, RoutedEventArgs e)
        {
            OnGraphBuilt();
            MessageBox.Show("График построен успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnGraphBuilt()
        {
            GraphBuilt?.Invoke(this, EventArgs.Empty);
        }





        private void graphScale(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(GraphScaleTextBox.Text, out double newScale))
            {
                scale = newScale;
                DrawHyperbolicSpiral();
            }
            else
            {
                if (GraphScaleTextBox.Text.Length > 0)
                {

                    MessageBox.Show("Введите корректное значение!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void graphWidth(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(GraphWidthTextBox.Text, out double newWidth))
            {
                lineWidth = newWidth;
                DrawHyperbolicSpiral();
            }
            else
            {
                if (GraphWidthTextBox.Text.Length > 0)
                {
                    MessageBox.Show("Введите корректное значение для толщины!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }



        private void graphColor(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = (LineColorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedColor == "Синий")
            {
                lineColor = Brushes.Blue;
            }
            else if (selectedColor == "Красный")
            {
                lineColor = Brushes.Red;
            }
            else if (selectedColor == "Черный")
            {
                lineColor = Brushes.Black;
            }
            else
            {
                lineColor = Brushes.Orange;
            }

            DrawHyperbolicSpiral();
        }

        private void graphText(object sender, TextChangedEventArgs e)
        {
            GraphTitle.Text = TextValue.Text;
        }





        private void textFont(object sender, SelectionChangedEventArgs e)
        {
            if (TextFont.SelectedItem is FontFamily selectedFont)
            {
                GraphTitle.FontFamily = selectedFont;
            }
        }

        private void textWeight(object sender, SelectionChangedEventArgs e)
        {
            if (TextWeight.SelectedItem is FontWeight selectedWeight)
            {
                GraphTitle.FontWeight = selectedWeight;
            }
        }



        private void textColor(object sender, SelectionChangedEventArgs e)
        {
            string selectedColor = (TextColor.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedColor == "Черный")
            {
                GraphTitle.Foreground = Brushes.Black;
            }
            else if (selectedColor == "Синий")
            {
                GraphTitle.Foreground = Brushes.Blue;
            }
            else if (selectedColor == "Красный")
            {
                GraphTitle.Foreground = Brushes.Red;
            }
            else
            {
                GraphTitle.Foreground = Brushes.Black;
            }
        }



        private void AddBackground(object sender, RoutedEventArgs e)
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


      private void  handleDownload(object sender, RoutedEventArgs e) {
            saveCanvas(MainCanvas);
            }
        private void saveCanvas(Canvas canvas)
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


    }
}
