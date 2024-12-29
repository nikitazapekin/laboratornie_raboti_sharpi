using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab10Variant18
{
    public partial class MainWindow : Window
    {
        private double scale = 50; // Масштаб графика
        private Brush lineColor = Brushes.Orange;
        private double lineWidth = 4;

        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += (_, __) => DrawHyperbolicSpiral(); // Перерисовка при изменении размера окна
        }

        private void DrawHyperbolicSpiral()
        {
            MainCanvas.Children.Clear();

            double canvasWidth = MainCanvas.ActualWidth;
            double canvasHeight = MainCanvas.ActualHeight;
            double centerX = (canvasWidth / 2) -200;
            double centerY = canvasHeight / 2;

            Polyline hyperbolicSpiral = new Polyline
            {
                Stroke = lineColor,
                StrokeThickness = lineWidth
            };

            const double a = 1.0; // Константа "a" в уравнении гиперболической спирали
            const int numPoints = 1000; // Количество точек для построения графика

            for (int i = 1; i <= numPoints; i++) // Начинаем с 1, чтобы избежать деления на 0
            {
                double phi = i * 0.01; // Угол (увеличиваем равномерно)
                double radius = a / phi; // Радиус для данного угла

                // Преобразуем координаты в систему окна
                double x = radius * Math.Cos(phi) * scale + centerX;
                double y = -radius * Math.Sin(phi) * scale + centerY; // Инверсия по Y для соответствия экранной системе

                hyperbolicSpiral.Points.Add(new Point(x, y));
            }

            MainCanvas.Children.Add(hyperbolicSpiral);
        }
    }
}

/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


 
namespace Lab10Variant18
{

    public partial class MainWindow : Window
    {

        private const double c = 0.5;
        private double scale = 100;
        private Brush lineColor = Brushes.Orange;
        private double lineWidth = 4;

        public MainWindow()
        {
            InitializeComponent();

            DrawBernoulliLemniscate();
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
                StrokeThickness = lineWidth,
           //     RenderTransform = graphTransform,
                Cursor = Cursors.SizeAll
            };

            int numPoints = 1000;
      

            for (int i = 0; i <= numPoints; i++)
            {
                double phi = 2 * Math.PI * i / numPoints;
                double cos2phi = Math.Cos(2 * phi);
                if (cos2phi < 0) continue;

                double radius = Math.Sqrt(2 * c * c * cos2phi);
                double x = radius * Math.Cos(phi) * scale + centerX;
                double y = -radius * Math.Sin(phi) * scale + centerY;
 

                lemniscate.Points.Add(new Point(x, y));
            }

       
            MainCanvas.Children.Add(lemniscate);
 
        }



    }
}
*/