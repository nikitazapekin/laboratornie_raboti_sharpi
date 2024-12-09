using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System;

namespace lab10Variant8
{
    public partial class MainWindow : Window
    {
        private bool isDragging;
        private Point startPoint;


        private const double c = 0.5; // Константа для лемнискаты
        private double scale = 100;  // Масштабирование в процентах
        private Brush lineColor = Brushes.Blue; // Цвет линии
        private double lineWidth = 2; // Ширина линии


        public MainWindow()
        {
            InitializeComponent();

        
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
            FontWeightComboBox.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold };
        }
        /*
        private void EditGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Visible;
            TextEditor.Visibility = Visibility.Collapsed;
        }
        */
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
                isDragging = true;
                startPoint = e.GetPosition(this);  
                EditPanel.CaptureMouse();
            }
        }

        private void EditPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(this);
                double offsetX = currentPoint.X - startPoint.X;
                double offsetY = currentPoint.Y - startPoint.Y;

              
                double currentLeft = Canvas.GetLeft(EditPanel);
                double currentTop = Canvas.GetTop(EditPanel);

            
                Canvas.SetLeft(EditPanel, currentLeft + offsetX);
                Canvas.SetTop(EditPanel, currentTop + offsetY);

               
                startPoint = currentPoint;
            }
        }

        private void EditPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                EditPanel.ReleaseMouseCapture();
            }
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
                Stroke = lineColor,
                StrokeThickness = lineWidth
            };

            double maxRadius = Math.Sqrt(2) * c * scale / 100; // Учитываем масштаб
            int numPoints = 1000;

            for (int i = 0; i <= numPoints; i++)
            {
                double phi = 2 * Math.PI * i / numPoints;
                double cos2phi = Math.Cos(2 * phi);
                if (cos2phi < 0) continue; // Исключаем недопустимые значения

                double radius = Math.Sqrt(2 * c * c * cos2phi);
                double x = radius * Math.Cos(phi) * scale + centerX;
                double y = -radius * Math.Sin(phi) * scale + centerY;

                lemniscate.Points.Add(new Point(x, y));
            }

            MainCanvas.Children.Add(lemniscate);
        }

        private void GraphScaleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(GraphScaleTextBox.Text, out double newScale))
            {
                scale = newScale;
                DrawBernoulliLemniscate();
            }
        }

        private void EditGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Visible;
            TextEditor.Visibility = Visibility.Collapsed;

            DrawBernoulliLemniscate();
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


    }
}
