using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace lab10Variant8
{
    public partial class MainWindow : Window
    {
        private bool isDragging;
        private Point startPoint;

        public MainWindow()
        {
            InitializeComponent();

            // Заполнение значений для выбора шрифта
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
            FontWeightComboBox.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold };
        }

        private void EditGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Visible;
            TextEditor.Visibility = Visibility.Collapsed;
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
                isDragging = true;
                startPoint = e.GetPosition(this); // Запоминаем начальную позицию
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

                // Получаем текущее положение панели
                double currentLeft = Canvas.GetLeft(EditPanel);
                double currentTop = Canvas.GetTop(EditPanel);

                // Устанавливаем новое положение
                Canvas.SetLeft(EditPanel, currentLeft + offsetX);
                Canvas.SetTop(EditPanel, currentTop + offsetY);

                // Обновляем стартовую точку
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
    }
}
