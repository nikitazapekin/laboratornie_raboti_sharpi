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
        private double[] randomNumbers;

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
                    randomNumbers = inputDialog.Numbers.Cast<double>().ToArray();
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
                randomNumbers = new double[n];
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = Math.Round(random.NextDouble() * 121 - 20, 3);
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
            dt.Columns.Add("Число", typeof(double));

            for (int i = 0; i < array.Length; i++)
            {
                DataRow row = dt.NewRow();
                row["Число"] = Math.Round(array[i], 3);
                dt.Rows.Add(row);
            }

            dataGrid.ItemsSource = dt.DefaultView;

            // Заблокировать последнюю строку
            DisableLastRow(dataGrid);
        }

        private void DisableLastRow(DataGrid dataGrid)
        {
            // Находим последнюю строку и устанавливаем для нее режим "только чтение"
            if (dataGrid.Items.Count > 0)
            {
                var lastRow = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.Items.Count - 1) as DataGridRow;
                if (lastRow != null)
                {
                    lastRow.IsEnabled = false; // отключаем последнюю строку
                    lastRow.IsHitTestVisible = false; // предотвращаем взаимодействие с ней
                }
            }
        }

        private void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            DataView dv = (DataView)DataGridView.ItemsSource;
            randomNumbers = dv.Table.Rows.Cast<DataRow>().Select(row => Math.Round(Convert.ToDouble(row["Число"]), 3)).ToArray();

            UpdateValues();
        }

        private void UpdateValues()
        {
            double minElement = GetMinimalElement();
            MinNumber.Text = Math.Round(minElement, 3).ToString();

            double sumBetweenNegatives = GetSumBetweenFirstTwoNegatives();
            Sum.Text = Math.Round(sumBetweenNegatives, 3).ToString();

            double[] transformedArray = TransformArray();
            FillDataGrid(transformedArray, TransformedDataGridView);

            // Заблокировать последнюю строку в преобразованной таблице
            DisableLastRow(TransformedDataGridView);
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

            return Math.Round(randomNumbers.Skip(firstNegIndex + 1).Take(secondNegIndex - firstNegIndex - 1).Sum(), 3);
        }

        private double[] TransformArray()
        {
            double[] lessThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) <= 1);
            double[] greaterThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) > 1);
            return lessThanOne.Concat(greaterThanOne).ToArray();
        }
    }
}
*/


 using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab_3_variant_8_zadanie_1
{
    public partial class MainWindow : Window
    {
        private double[] randomNumbers; 

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
                    randomNumbers = inputDialog.Numbers.Cast<double>().ToArray();  
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
                randomNumbers = new double[n];  
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = Math.Round(random.NextDouble() * 121 - 20, 3);  
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
            dt.Columns.Add("Число", typeof(double)); 
            foreach (var num in array)
            {
                dt.Rows.Add(Math.Round(num, 3));  
            }
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            DataView dv = (DataView)DataGridView.ItemsSource;
            randomNumbers = dv.Table.Rows.Cast<DataRow>().Select(row => Math.Round(Convert.ToDouble(row["Число"]), 3)).ToArray();  

            UpdateValues();
        }

        private void UpdateValues()
        {
            double minElement = GetMinimalElement(); 
            MinNumber.Text = Math.Round(minElement, 3).ToString(); 

            double sumBetweenNegatives = GetSumBetweenFirstTwoNegatives();  
            Sum.Text = Math.Round(sumBetweenNegatives, 3).ToString(); 

            double[] transformedArray = TransformArray(); 
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

            return Math.Round(randomNumbers.Skip(firstNegIndex + 1).Take(secondNegIndex - firstNegIndex - 1).Sum(), 3); 
        }

        private double[] TransformArray()
        {
            double[] lessThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) <= 1);
            double[] greaterThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) > 1);
            return lessThanOne.Concat(greaterThanOne).ToArray();
        }
    }
}
 