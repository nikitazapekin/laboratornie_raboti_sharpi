using System;
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
            InputDialog inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                randomNumbers = inputDialog.Numbers;
                FillDataGrid(randomNumbers, DataGridView);
                UpdateValues();
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

/*
 * using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;

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
            InputDialog inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
     
                randomNumbers = inputDialog.Numbers;
                FillDataGrid(randomNumbers, DataGridView);



                    int minElement = GetMinimalElement();
                    MinNumber.Text = minElement.ToString();

                    int sumBetweenNegatives = GetSumBetweenFirstTwoNegatives();
                  Sum.Text = sumBetweenNegatives.ToString();

                  int[] transformedArray = TransformArray();
                    FillDataGrid(transformedArray, TransformedDataGridView);


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


                int minElement = GetMinimalElement();
                MinNumber.Text = minElement.ToString();

                int sumBetweenNegatives = GetSumBetweenFirstTwoNegatives();
                Sum.Text = sumBetweenNegatives.ToString();

                int[] transformedArray = TransformArray();
                FillDataGrid(transformedArray, TransformedDataGridView);
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