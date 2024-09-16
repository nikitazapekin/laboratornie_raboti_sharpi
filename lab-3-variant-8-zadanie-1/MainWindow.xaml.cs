
using System;
using System.Text;
using System.Windows;

namespace lab_3_variant_8_zadanie_1
{
    public partial class MainWindow : Window
    {

        private int[] randomNumbers;

        public MainWindow()
        {
            InitializeComponent();
        }

   
        private int GetMinimalElement()
        {
            if (randomNumbers == null || randomNumbers.Length == 0)
                return 0;

            int min = randomNumbers[0];
            foreach (var num in randomNumbers)
            {
                if (num < min)
                {
                    min = num;
                }
            }
            return min;
        }

        private int GetSumBetweenFirstTwoNegatives()
        {
            if (randomNumbers == null || randomNumbers.Length == 0)
                return 0;

            int firstNegIndex = -1, secondNegIndex = -1;
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                if (randomNumbers[i] < 0)
                {
                    if (firstNegIndex == -1)
                    {
                        firstNegIndex = i;
                    }
                    else
                    {
                        secondNegIndex = i;
                        break;
                    }
                }
            }

            if (firstNegIndex == -1 || secondNegIndex == -1)
            {
                return 0;
            }


            int sum = 0;
            for (int i = firstNegIndex + 1; i < secondNegIndex; i++)
            {
                sum += randomNumbers[i];
            }
            return sum;
        }

  
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                randomNumbers = new int[n]; 

                StringBuilder sb = new StringBuilder();

              
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = random.Next(-20, 101);  
                    sb.Append(randomNumbers[i]);
                    sb.Append(" ");
                }

              
                OutputNumbers.Text = sb.ToString();
 
                int minElement = GetMinimalElement();
                MinNumber.Text = minElement.ToString();

              
                int sumBetweenNegatives = GetSumBetweenFirstTwoNegatives();
                Sum.Text = sumBetweenNegatives.ToString();
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

/*
using System;
using System.Text;
using System.Windows;

namespace lab_3_variant_8_zadanie_1
{
    public partial class MainWindow : Window
    {
        private int minElement;
        public MainWindow()
        {
            InitializeComponent();
        }
        private int GetMinimalElement()
        {
            return 0;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                StringBuilder sb = new StringBuilder();

              
                for (int i = 0; i < n; i++)
                {
                    sb.Append(random.Next(1, 101)-20);  
                    sb.Append(" ");
                }
 
                OutputNumbers.Text = sb.ToString();
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

*/

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