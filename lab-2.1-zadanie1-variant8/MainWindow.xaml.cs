/*
 * using System;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;

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

        private void PlotGraphs()
        {
            var plotModel = new PlotModel { Title = "Graphs" };

            var series1 = new LineSeries { Title = "y = sin(x)" };
            for (double x = xMin; x <= xMax; x += step)
            {
                series1.Points.Add(new DataPoint(x, Math.Sin(x)));
            }

            var series2 = new LineSeries { Title = "y = cos(x)" };
            for (double x = xMin; x <= xMax; x += step)
            {
                series2.Points.Add(new DataPoint(x, Math.Cos(x)));
            }

            var series3 = new LineSeries { Title = "y = (x-6)^2" };
            for (double x = xMin; x <= xMax; x += step)
            {
                series3.Points.Add(new DataPoint(x, Math.Pow(x - 6, 2)));
            }

            plotModel.Series.Add(series1);
            plotModel.Series.Add(series2);
            plotModel.Series.Add(series3);

            plotView.Model = plotModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(XMinTextBox.Text, out xMin) &&
                double.TryParse(XMaxTextBox.Text, out xMax) &&
                double.TryParse(StepTextBox.Text, out step))
            {
                PlotGraphs();
            }
            else
            {
                MessageBox.Show("Please enter valid numerical values.", "Input Error");
            }
        }
    }
}
*/

/*
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


       private void PlotGraphs()
       {
           var plotModel = new PlotModel { Title = "Graphs" };


           var series1 = new LineSeries { Title = "y = sin(x)" };
           for (double x = -Math.PI; x <= Math.PI; x += 0.1)
           {
               series1.Points.Add(new DataPoint(x, Math.Sin(x)));
           }

           var series2 = new LineSeries { Title = "y = cos(x)" };
           for (double x = -Math.PI; x <= Math.PI; x += 0.1)
           {
               series2.Points.Add(new DataPoint(x, Math.Cos(x)));
           }

            var series3 = new LineSeries { Title = "y = (x-6)^2" };

           for (double x = -Math.PI; x <= Math.PI; x += 0.1)
           {
               series3.Points.Add(new DataPoint(x, x * x));
           }

           plotModel.Series.Add(series1);
           plotModel.Series.Add(series2);
           plotModel.Series.Add(series3);

           plotView.Model = plotModel;
       }

       private void Button_Click(object sender, RoutedEventArgs e)
       {

           if (double.TryParse(XMinTextBox.Text, out xMin) &&
               double.TryParse(XMaxTextBox.Text, out xMax) &&
               double.TryParse(StepTextBox.Text, out step))
           {
               MessageBox.Show($"X Min: {xMin}\nX Max: {xMax}\nStep: {step}", "Input Values");
               PlotGraphs();
           }
           else
           {
               MessageBox.Show("Please enter valid numerical values.", "Input Error");
           }
       }
   }
}
*/

/*
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
using System;

namespace lab_2._1_zadanie1_variant8
{
   public partial class MainWindow : Window
   {


       static void Main(string[] args)
       {
           double f, xstart, xend, dx;
           const double eps = 0.00000000001;

           Console.Write("Enter xstart: ");
           xstart = Convert.ToDouble(Console.ReadLine());

           Console.Write("Enter xend: ");
           xend = Convert.ToDouble(Console.ReadLine());

           Console.Write("Enter dx: ");
           dx = Convert.ToDouble(Console.ReadLine());

           Console.WriteLine("    TABL 1");
           Console.WriteLine("    x      f(x)");

           for (double i = xstart; i <= xend; i += dx)
           {
               if (i >= -10 && i <= -6)
               {
                   f = Math.Sqrt(4 - Math.Pow(i + 8, 2)) - 2;
                   Console.WriteLine($"{i,6:F2} {f:F5}");
                   f = -Math.Sqrt(4 - Math.Pow(i + 8, 2)) - 2;
                   Console.WriteLine($"{i,6:F2} {f:F5}");
               }
               else if (i > -6 && i <= 2)
               {
                   f = (0.5) * i + 1;
                   Console.WriteLine($"{i,6:F2} {f:F5}");
               }
               else if (i > 6 && i <= 8)
               {
                   f = Math.Pow(i - 6, 2);
                   Console.WriteLine($"{i,6:F2} {f:F5}");
               }
           }

           Console.ReadKey();
       }
   }
}
*/



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


        private void PlotGraphs()
        {
     
        }



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
                    output.AppendLine($"{i,6:F2} {f:F5}");
                    f = -Math.Sqrt(4 - Math.Pow(i + 8, 2)) - 2;
                    output.AppendLine($"{i,6:F2} {f:F5}");
                }
                else if (i > -6 && i <= 2)
                {
                    double f = (0.5) * i + 1;
                    output.AppendLine($"{i,6:F2} {f:F5}");
                }
                else if (i > 6 && i <= 8)
                {
                    double f = Math.Pow(i - 6, 2);
                    output.AppendLine($"{i,6:F2} {f:F5}");
                }
            }

            OutputTextBlock.Text = output.ToString();
        }




    }
}