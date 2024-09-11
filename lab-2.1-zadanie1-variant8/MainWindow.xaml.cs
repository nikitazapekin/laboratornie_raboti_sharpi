 


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

            const double eps = 0.00000000001;
            StringBuilder output = new StringBuilder();
            output.AppendLine("    x      f(x)");

            for (double i = xstart; i <= xend; i += dx)
            {
                if (i >= -10 && i <= -6)
                {
                    double f = Math.Sqrt(4 - Math.Pow(i + 8, 2)) - 2;
                    output.AppendLine($"{i} {f:F5}");
                }
                else if (i > -6 && i <= 2)
                {
                    double f = (0.5) * i + 1;
                    output.AppendLine($"{i} {f:F5}");
                }
                else if (i > 6 && i <= 8)
                {
                    double f = Math.Pow(i - 6, 2);
                    output.AppendLine($"{i} {f:F5}");
                }
            }

            OutputTextBlock.Text = output.ToString();
        }

        /*
        private void OnCalculateClick(object sender, RoutedEventArgs e)
        {
        if (!double.TryParse(XStartTextBox.Text, out double xstart) ||
                !double.TryParse(XEndTextBox.Text, out double xend) ||
                !double.TryParse(DxTextBox.Text, out double dx))
            {
                MessageBox.Show("Please enter valid numbers.");
                return;
            }

            const double eps = 0.00000000001;
            StringBuilder output = new StringBuilder();

            output.AppendLine("    x      f(x)");

            for (double i = xstart; i <= xend; i += dx)
            {
                if (i >= -10 && i <= -6)
                {
                    double f = Math.Sqrt(4 - Math.Pow(i + 8, 2)) - 2;
                  output.AppendLine($"{i} {f:F5}");

                    

                }
                else if (i > -6 && i <= 2)
                {
                    double f = (0.5) * i + 1;
                    output.AppendLine($"{i} {f:F5}");
                }
                else if (i > 6 && i <= 8)
                {
                    double f = Math.Pow(i - 6, 2);
                    output.AppendLine($"{i} {f:F5}");
                }
            }

            OutputTextBlock.Text = output.ToString();
      
        }
        */



    }
}
