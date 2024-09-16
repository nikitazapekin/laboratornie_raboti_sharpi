using System;
using System.Text;
using System.Windows;

namespace lab_3_variant_8_zadanie_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что введено корректное число n
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                StringBuilder sb = new StringBuilder();

                // Генерация n случайных чисел
                for (int i = 0; i < n; i++)
                {
                    sb.Append(random.Next(1, 101)-20); // Генерация числа от 1 до 100
                    sb.Append(" ");
                }

                // Вывод чисел
                OutputNumbers.Text = sb.ToString();
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

namespace lab_3_variant_8_zadanie_1
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
    }
}
*/