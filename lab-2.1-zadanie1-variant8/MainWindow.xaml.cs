 


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

/*
 
        <TextBlock Text="Enter xend:" Grid.Row="1" Grid.Column="0" Margin="0,10,0,0" VerticalAlignment="Center"/>
        <TextBox x:Name="XEndTextBox" Grid.Row="1" Grid.Column="1"  Width="100px" Margin="44,5,1574,5"/>

        <TextBlock Text="Enter dx:" Grid.Row="2" Grid.Column="0" Margin="0,10,0,0" VerticalAlignment="Center"/>
        <TextBox x:Name="DxTextBox" Grid.Row="2" Grid.Column="1"  Width="100px" Margin="44,6,1574,4"/>

        <Button Content="Calculate" Click="OnCalculateClick" Width="100" HorizontalAlignment="Left" Margin="256,0,0,1115"/>

        <TextBlock Text="TABL 1" Grid.Column="0" Margin="140,154,-140,-134"/>
        <TextBlock x:Name="OutputTextBlock" FontFamily="Courier New" Margin="156,54,332,-55"/>
 <Image Grid.Row="0" Grid.Column="2" Grid.RowSpan="4" Margin="10" Width="200" Height="200" Source="your_image_path_here.jpg"/>
 
*/