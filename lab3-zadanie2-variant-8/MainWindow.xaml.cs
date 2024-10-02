using System;
using System.Data;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace lab3_zadanie2_variant_8
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = int.Parse(((TextBox)this.FindName("matrixRows")).Text);
                int columns = int.Parse(((TextBox)this.FindName("matrixColumns")).Text);

                DataTable table = new DataTable();

                for (int i = 0; i < columns; i++)
                {
                    table.Columns.Add("Col " + (i + 1));
                }

                Random random = new Random();

                for (int i = 0; i < rows; i++)
                {
                    DataRow newRow = table.NewRow();
                    for (int j = 0; j < columns; j++)
                    {
                        newRow[j] = random.Next(-10, 11);
                    }
                    table.Rows.Add(newRow);
                }

                matrixGrid.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void InputButton_Click(object sender, RoutedEventArgs e)
        {

            InputDialog inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {

            }
                /*   if (int.TryParse(InputN.Text, out int n) && n > 0)
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
                   } */

            }



        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DataView dataView = matrixGrid.ItemsSource as DataView;

            if (dataView != null)
            {
                int rowCount = dataView.Count;
                int columnCount = dataView.Table.Columns.Count;

                int[,] matrix = new int[rowCount, columnCount];

                for (int i = 0; i < rowCount; i++)
                {
                    DataRowView rowView = dataView[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(rowView[j]);
                    }
                }

                double[] negativeSums = new double[columnCount];

                for (int col = 0; col < columnCount; col++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int row = 0; row < rowCount; row++)
                    {
                        count++;
                        if (matrix[row, col] < 0 && count % 2 != 0)
                        {
                            sum += Math.Abs(matrix[row, col]);
                        }
                    }
                    negativeSums[col] = sum;
                }

                int[] indices = Enumerable.Range(0, columnCount).ToArray();
                Array.Sort(negativeSums, indices);

                int[,] sortedMatrix = new int[rowCount, columnCount];

                for (int col = 0; col < columnCount; col++)
                {
                    int originalCol = indices[col];
                    for (int row = 0; row < rowCount; row++)
                    {
                        sortedMatrix[row, col] = matrix[row, originalCol];
                    }
                }

                DataTable sortedTable = new DataTable();
                for (int i = 0; i < columnCount; i++)
                {
                    sortedTable.Columns.Add("Col " + (i + 1));
                }

                for (int i = 0; i < rowCount; i++)
                {
                    DataRow newRow = sortedTable.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        newRow[j] = sortedMatrix[i, j];
                    }
                    sortedTable.Rows.Add(newRow);
                }

                matrixGrid.ItemsSource = sortedTable.DefaultView;
            }
            else
            {
                MessageBox.Show("Матрица пуста или не существует.", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void matrixRows_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Логика, связанная с изменением текста, если потребуется
        }

        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (matrixGrid.CurrentCell != null)
            {
                var currentCell = matrixGrid.CurrentCell;
                var dataRowView = currentCell.Item as DataRowView;

                if (dataRowView != null)
                {
                    matrixGrid.BeginEdit();
                }
            }
        }

        private void matrixGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editingElement = e.EditingElement as TextBox;
                if (editingElement != null)
                {
                    if (!int.TryParse(editingElement.Text, out int value))
                    {
                        MessageBox.Show("Пожалуйста, введите корректное целое число.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                        editingElement.Text = "0"; // Устанавливаем значение ячейки в 0
                        e.Cancel = true; // Отменяем изменение ячейки
                        return;
                    }
                    // Если число корректное, значение будет применено к DataGrid
                }
            }
        }
    }
}


/*
 * using System;
using System.Collections.Generic;
using System.Data;
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

namespace lab3_zadanie2_variant_8
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


        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int rows = int.Parse(((TextBox)this.FindName("matrixRows")).Text);
                int columns = int.Parse(((TextBox)this.FindName("matrixColumns")).Text);


                DataTable table = new DataTable();

                for (int i = 0; i < columns; i++)
                {
                    table.Columns.Add("Col " + (i + 1));
                }

                Random random = new Random();


                for (int i = 0; i < rows; i++)
                {
                    DataRow newRow = table.NewRow();
                    for (int j = 0; j < columns; j++)
                    {
                        newRow[j] = random.Next(-10, 11);
                    }
                    table.Rows.Add(newRow);
                }


                matrixGrid.ItemsSource = table.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

              
            }
        }




        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DataView dataView = matrixGrid.ItemsSource as DataView;

            if (dataView != null)
            {
                int rowCount = dataView.Count;
                int columnCount = dataView.Table.Columns.Count;

                int[,] matrix = new int[rowCount, columnCount];


                for (int i = 0; i < rowCount; i++)
                {
                    DataRowView rowView = dataView[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(rowView[j]);
                    }
                }


                double[] negativeSums = new double[columnCount];

                for (int col = 0; col < columnCount; col++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int row = 0; row < rowCount; row++)
                    {
                        count++;
                        if (matrix[row, col] < 0 && count%2!=0)
                        {
                            sum += Math.Abs(matrix[row, col]);
                        }
                    }
                    negativeSums[col] = sum;
                }

                int[] indices = Enumerable.Range(0, columnCount).ToArray();
                Array.Sort(negativeSums, indices);

                int[,] sortedMatrix = new int[rowCount, columnCount];

                for (int col = 0; col < columnCount; col++)
                {
                    int originalCol = indices[col];
                    for (int row = 0; row < rowCount; row++)
                    {
                        sortedMatrix[row, col] = matrix[row, originalCol];
                    }
                }

                DataTable sortedTable = new DataTable();
                for (int i = 0; i < columnCount; i++)
                {
                    sortedTable.Columns.Add("Col " + (i + 1));
                }

                for (int i = 0; i < rowCount; i++)
                {
                    DataRow newRow = sortedTable.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        newRow[j] = sortedMatrix[i, j];
                    }
                    sortedTable.Rows.Add(newRow);
                }

                matrixGrid.ItemsSource = sortedTable.DefaultView;
               
            }
            else
            {


                MessageBox.Show("Матрица пуста или не существует.", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
             
            }
        }

        private void matrixRows_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

*/