
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


namespace lab2_zadanie3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void EvaluateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {

                double xmin = Convert.ToDouble(TextBoxXMin.Text);
                double xmax = Convert.ToDouble(TextBoxXMax.Text);
                double dx = Convert.ToDouble(TextBoxDx.Text);
                double epsilon = Convert.ToDouble(TextBoxEpsilon.Text);


                if (xmax < xmin)
                {
                    MessageBox.Show("xmax не может быть меньше xmin");
                    return;
                }


                if (dx <= 0)
                {
                    MessageBox.Show("Dx не может быть отрицательным или равняться нулю");
                    return;
                }

                if (epsilon <= 0)
                {
                    MessageBox.Show("Epsilon не может быть отрицательным или равняться нулю");
                    return;
                }


                valuesList.Items.Clear();
                valuesList.Items.Add("Таблица результатов");

                const int MaxIter = 500;


                for (double x = xmin; x <= xmax; x += dx)
                {
                    double sum = 0;
                    bool done = false;
                    double term;
                    int n;


                    for (n = 0; n < MaxIter; n++)
                    {


                        term = Math.Pow(-1, n+1 )/((2*n+1)*Math.Pow(x, (2*n+1)));
                     //   term = Math.Pow(x - 1, 2 * n + 1) / ((2 * n + 1) * Math.Pow(x + 1, 2 * n + 1));
                        sum += term;

                        if (Math.Abs(term) < epsilon)
                        {
                            done = true;
                            break;
                        }
                    }
                    sum += 3.14/2;

                    //  double f = Math.Log(x);
                    double f = Math.Atan(x);

                    if (done)
                    {
                        valuesList.Items.Add($"x = {x:F4}, Сумма ряда = {sum:F6}, Точное значение = {f:F6}, Членов ряда = {n + 1}");
                    }
                    else
                    {
                        valuesList.Items.Add($"x = {x:F4}, Ряд не сошёлся после {MaxIter} итераций");
                    }
                }
            }
            catch (FormatException)
            {

                MessageBox.Show("Некорректный формат вводимых данных. Дробные числа должны быть указаны через запятую. Поля не должны быть пустыми.");
            }
        }

    }
}
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


namespace lab2_zadanie3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void EvaluateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {

                double xmin = Convert.ToDouble(TextBoxXMin.Text);
                double xmax = Convert.ToDouble(TextBoxXMax.Text);
                double dx = Convert.ToDouble(TextBoxDx.Text);
                double epsilon = Convert.ToDouble(TextBoxEpsilon.Text);


                if (xmax < xmin)
                {
                    MessageBox.Show("xmax не может быть меньше xmin");
                    return;
                }


                if (dx <= 0)
                {
                    MessageBox.Show("Dx не может быть отрицательным или равняться нулю");
                    return;
                }

                if (epsilon <= 0)
                {
                    MessageBox.Show("Epsilon не может быть отрицательным или равняться нулю");
                    return;
                }


                valuesList.Items.Clear();
                valuesList.Items.Add("Таблица результатов");

                const int MaxIter = 500;


                for (double x = xmin; x <= xmax; x += dx)
                {
                    double sum = 0;
                    bool done = false;
                    double term;
                    int n;


                    for (n = 0; n < MaxIter; n++)
                    {
                        term = Math.Pow(x - 1, 2 * n + 1) / ((2 * n + 1) * Math.Pow(x + 1, 2 * n + 1));
                        sum += term;

                        if (Math.Abs(term) < epsilon)
                        {
                            done = true;
                            break;
                        }
                    }
                    sum *= 2;

                    double f = Math.Log(x);


                    if (done)
                    {
                        valuesList.Items.Add($"x = {x:F4}, Сумма ряда = {sum:F6}, Точное значение = {f:F6}, Членов ряда = {n + 1}");
                    }
                    else
                    {
                        valuesList.Items.Add($"x = {x:F4}, Ряд не сошёлся после {MaxIter} итераций");
                    }
                }
            }
            catch (FormatException)
            {
              
                MessageBox.Show("Некорректный формат вводимых данных. Дробные числа должны быть указаны через запятую. Поля не должны быть пустыми.");
            }
        }

    }
}
*/