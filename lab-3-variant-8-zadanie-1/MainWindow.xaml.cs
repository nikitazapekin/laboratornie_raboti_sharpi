using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab_3_variant_8_zadanie_1
{
    public partial class MainWindow : Window
    {
        private double[] randomNumbers; // Изменено на double[]

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                InputDialog inputDialog = new InputDialog();
                if (inputDialog.ShowDialog() == true)
                {
                    randomNumbers = inputDialog.Numbers.Cast<double>().ToArray(); // Приведение к double
                    if (randomNumbers.Length > n)
                    {
                        MessageBox.Show($"Вы ввели недопустимое количество чисел. Максимальное количество чисел: {n}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        FillDataGrid(randomNumbers, DataGridView);
                        UpdateValues();
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                randomNumbers = new double[n]; // Изменено на double[]
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = Math.Round(random.NextDouble() * 121 - 20, 3); // Генерация чисел от -20 до 100 с плавающей точкой, округленных до 3 знаков
                }

                FillDataGrid(randomNumbers, DataGridView);
                UpdateValues();
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillDataGrid(double[] array, DataGrid dataGrid)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Число", typeof(double)); // Изменено на double
            foreach (var num in array)
            {
                dt.Rows.Add(Math.Round(num, 3)); // Округление до 3 знаков при добавлении в DataTable
            }
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            DataView dv = (DataView)DataGridView.ItemsSource;
            randomNumbers = dv.Table.Rows.Cast<DataRow>().Select(row => Math.Round(Convert.ToDouble(row["Число"]), 3)).ToArray(); // Изменено на Convert.ToDouble с округлением

            UpdateValues();
        }

        private void UpdateValues()
        {
            double minElement = GetMinimalElement(); // Изменено на double
            MinNumber.Text = Math.Round(minElement, 3).ToString(); // Округление до 3 знаков

            double sumBetweenNegatives = GetSumBetweenFirstTwoNegatives(); // Изменено на double
            Sum.Text = Math.Round(sumBetweenNegatives, 3).ToString(); // Округление до 3 знаков

            double[] transformedArray = TransformArray(); // Изменено на double[]
            FillDataGrid(transformedArray, TransformedDataGridView);
        }

        private double GetMinimalElement()
        {
            return randomNumbers.Min();
        }

        private double GetSumBetweenFirstTwoNegatives()
        {
            int firstNegIndex = Array.FindIndex(randomNumbers, num => num < 0);
            int secondNegIndex = Array.FindIndex(randomNumbers, firstNegIndex + 1, num => num < 0);

            if (firstNegIndex == -1 || secondNegIndex == -1)
                return 0;

            return Math.Round(randomNumbers.Skip(firstNegIndex + 1).Take(secondNegIndex - firstNegIndex - 1).Sum(), 3); // Округление до 3 знаков
        }

        private double[] TransformArray()
        {
            double[] lessThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) <= 1);
            double[] greaterThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) > 1);
            return lessThanOne.Concat(greaterThanOne).ToArray();
        }
    }
}

/*
 * using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab_3_variant_8_zadanie_1
{
    public partial class MainWindow : Window
    {
        private int[] randomNumbers;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {

            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                InputDialog inputDialog = new InputDialog();
                if (inputDialog.ShowDialog() == true)
                {
                    randomNumbers = inputDialog.Numbers;
                    if (randomNumbers.Length > n)
                    {
                        MessageBox.Show($"Вы ввели недопустимое количество чисел. Максимальное количество чисел: {n}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    } else
                    {

                        FillDataGrid(randomNumbers, DataGridView);
                        UpdateValues();
                    }
                }

            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                randomNumbers = new int[n];
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = random.Next(-20, 101);
                }

                FillDataGrid(randomNumbers, DataGridView);
                UpdateValues();
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void FillDataGrid(int[] array, DataGrid dataGrid)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Число", typeof(int));
            foreach (var num in array)
            {
              

                    dt.Rows.Add(num);
               
            }
            dataGrid.ItemsSource = dt.DefaultView;
        }
        

        private void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
 
            DataView dv = (DataView)DataGridView.ItemsSource;
            randomNumbers = dv.Table.Rows.Cast<DataRow>().Select(row => Convert.ToInt32(row["Число"])).ToArray();

          
            UpdateValues();
        }

        private void UpdateValues()
        {
        
            int minElement = GetMinimalElement();
            MinNumber.Text = minElement.ToString();

           
            int sumBetweenNegatives = GetSumBetweenFirstTwoNegatives();
            Sum.Text = sumBetweenNegatives.ToString();

           
            int[] transformedArray = TransformArray();
            FillDataGrid(transformedArray, TransformedDataGridView);
        }

        private int GetMinimalElement()
        {
            return randomNumbers.Min();
        }

        private int GetSumBetweenFirstTwoNegatives()
        {
            int firstNegIndex = Array.FindIndex(randomNumbers, num => num < 0);
            int secondNegIndex = Array.FindIndex(randomNumbers, firstNegIndex + 1, num => num < 0);

            if (firstNegIndex == -1 || secondNegIndex == -1)
                return 0;

            return randomNumbers.Skip(firstNegIndex + 1).Take(secondNegIndex - firstNegIndex - 1).Sum();
        }

        private int[] TransformArray()
        {
            int[] lessThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) <= 1);
            int[] greaterThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) > 1);
            return lessThanOne.Concat(greaterThanOne).ToArray();
        }
    }
}
 */