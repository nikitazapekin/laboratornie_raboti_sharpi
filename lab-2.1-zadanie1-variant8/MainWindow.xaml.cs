 

 
using System;
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
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System.Globalization;

namespace lab_2._1_zadanie1_variant8
{

    public partial class MainWindow : Window
    {

        private double xMin;
        private double xMax;
        private double step;
        private int amountOfElements = 0;
        public MainWindow()
        {
            InitializeComponent();

        }


        private void OnCalculateClick(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(XStartTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double xstart) ||
                !double.TryParse(XEndTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double xend) ||
                !double.TryParse(DxTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double dx))
            {
                MessageBox.Show("Please enter valid numbers.");
                return;
            }
            if (xstart >= xend)
            {
                MessageBox.Show("Пожалуйста введите корректные промежутки.");
                return;
            }

            StringBuilder output = new StringBuilder();
            output.AppendLine("    x      f(x)");

            for (double i = xstart; i <= xend; i += dx)
            {
                if (i >= -10 && i <= -6)
                {
                    double f = Math.Sqrt(4 - Math.Pow(i + 8, 2)) - 2;
                    output.AppendLine($"{i,7:F2} {f,10:F5}");
                    amountOfElements += 1;
                }
                else if (i >= -6 && i <= 2)
                {
                    double f = (0.5) * i + 1;
                
                     output.AppendLine($"{i,7:F2} {f,10:F5}");
                    amountOfElements += 1;
                }
                else if (i >= 2 && i <= 6)
                {
                   
                    double f = Math.Pow(i - 4, 2) + 2; 
                                                      
                                                       
                    output.AppendLine($"{i,7:F2}  {0,10:F5}");
                    amountOfElements += 1;
                }
                else if (i >= 6 && i <= 8)
                {
                    double f = Math.Pow(i - 6, 2);
                    output.AppendLine($"{i,7:F2} {f,10:F5}");
                    amountOfElements += 1;
                }
                else if (i<-10 || i>8)
                {
                    output.AppendLine($"{i,7:F2} Значение находится вне диапазона");
                }
            }

            if (amountOfElements == 0)
            {
                output.AppendLine("Элементы отсутствуют");
            }

            OutputTextBlock.Text = output.ToString();
        }



    }
}
 