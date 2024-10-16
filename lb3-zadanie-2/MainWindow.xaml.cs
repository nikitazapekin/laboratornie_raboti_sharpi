using System;
using System.Data;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace lb3_zadanie_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DataTable table;
   
       
        private void GenerateMatrix_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                int rows = int.Parse(matrixRows.Text);
                int columns = int.Parse(matrixColumns.Text);

                if (rows < 0 || columns < 0)
                {
                    MessageBox.Show("Введите положительные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    table = new DataTable();
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
                    matrixGridCurrent.ItemsSource = table.DefaultView;
                    Sum();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
          try
            {
                int rows = int.Parse(matrixRows.Text);
                int columns = int.Parse(matrixColumns.Text);


                if (rows < 0 || columns < 0)
                {
                    MessageBox.Show("Пожалуйста введите положительные значения размерности", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {


                    MatrixInputWindow inputWindow = new MatrixInputWindow(rows, columns);
                    if (inputWindow.ShowDialog() == true)
                    {
                        DataTable table = new DataTable();
                        for (int i = 0; i < columns; i++)
                        {
                            table.Columns.Add("Col " + (i + 1));
                        }

                        for (int i = 0; i < rows; i++)
                        {
                            DataRow newRow = table.NewRow();
                            for (int j = 0; j < columns; j++)
                            {

                                string value = inputWindow.InputFields[i][j].Text;
                                if (int.TryParse(value, out int parsedValue))
                                {
                                    newRow[j] = parsedValue;
                                }
                                else
                                {
                                    newRow[j] = 0;
                                }
                            }
                            table.Rows.Add(newRow);
                        }

                        matrixGrid.ItemsSource = table.DefaultView;
                        matrixGridCurrent.ItemsSource = table.DefaultView;
                        Sum();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }  

        }
     

        private void Sum()
        {


            valuesList.Items.Clear();
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

                    bool hasNegative = false;
                    for (int row = 0; row < rowCount; row++)
                    {
                        count++;
                        if (matrix[row, col] < 0)
                        {
                            hasNegative = true;
                        }
                        sum += matrix[row, col];
                    }

                    //    characteristicsValuesList.Items.Add($"Колонка {col + 1}, сумма модулей отрицательных нечетных элементов {sum}, количество  {count} ");
                    if (hasNegative)
                    {

                        valuesList.Items.Add($"Колонка {col + 1}, sum={sum}");
                    }

                    negativeSums[col] = sum;
                }
            }

        }




        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DataView dataView = matrixGrid.ItemsSource as DataView;
            characteristicsValuesList.Items.Clear();
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
                    int countOfNegative = 0;
                    for (int row = 0; row < rowCount; row++)
                    {
                        count++;
                        if (matrix[row, col] < 0 && count % 2 != 0)
                        {
                            sum += Math.Abs(matrix[row, col]);
                            countOfNegative++;
                        }
                    }

                    characteristicsValuesList.Items.Add($"Колонка {col + 1}, сумма модулей отрицательных нечетных элементов {sum}, количество  {countOfNegative} ");
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
                Sum();
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



        


        private void displayCharacters()
        {
            characteristicsValuesList.Items.Clear();
        }
    }
}




        /*
        private void DataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
          if (matrixGrid.CurrentCell != null)
            {

                matrixGridCurrent.ItemsSource = ((DataView)matrixGrid.ItemsSource).ToTable().DefaultView;
            }

            if (matrixGrid.CurrentCell != null)
            {
                var currentCell = matrixGrid.CurrentCell;
                var dataRowView = currentCell.Item as DataRowView;

                if (dataRowView != null)
                {
                    matrixGrid.BeginEdit();
                    matrixGridCurrent.BeginEdit();
                }
            }  


            Sum(); 
        }
     
        private void matrixGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editingElement = e.EditingElement as TextBox;
                if (editingElement != null && !int.TryParse(editingElement.Text, out int value))
                {
                    MessageBox.Show("Введите корректное целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    editingElement.Text = "0";
                    e.Cancel = true;
                }
            }

            matrixGridCurrent.ItemsSource = table.DefaultView;  
        }
        private void matrixGridCurrent_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
           if (e.EditAction == DataGridEditAction.Commit)
            {
                var editingElement = e.EditingElement as TextBox;
                if (editingElement != null && !int.TryParse(editingElement.Text, out int value))
                {
                    MessageBox.Show("Введите корректное целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    editingElement.Text = "0";
                    e.Cancel = true;
                }
            }

            matrixGrid.ItemsSource = table.DefaultView;  
            
        }
        */