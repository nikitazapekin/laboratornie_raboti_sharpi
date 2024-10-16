using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lb3_zadanie_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DataTable table;

        private int[,] originalMatrix; 

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
                    originalMatrix = new int[rows, columns]; 
                    for (int i = 0; i < rows; i++)
                    {
                        DataRow newRow = table.NewRow();
                        for (int j = 0; j < columns; j++)
                        {
                            int value = random.Next(-10, 11);
                            newRow[j] = value;
                            originalMatrix[i, j] = value; 
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

                        originalMatrix = new int[rows, columns];  
                        for (int i = 0; i < rows; i++)
                        {
                            DataRow newRow = table.NewRow();
                            for (int j = 0; j < columns; j++)
                            {
                                string value = inputWindow.InputFields[i][j].Text;
                                if (int.TryParse(value, out int parsedValue))
                                {
                                    newRow[j] = parsedValue;
                                    originalMatrix[i, j] = parsedValue;  
                                }
                                else
                                {
                                    newRow[j] = 0;
                                    originalMatrix[i, j] = 0;
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
            DataView dataView = matrixGridCurrent.ItemsSource as DataView;

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

                for (int j = 0; j < columnCount; j++)
                {
                    int columnSum = 0;
                    bool hasNegative = false;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (matrix[i, j] < 0)
                        {
                            hasNegative = true;
                            break;
                        }
                    }

                    if (hasNegative)
                    {
                        for (int i = 0; i < rowCount; i++)
                        {
                            columnSum += matrix[i, j];
                        }
                        valuesList.Items.Add($"Колонка {j + 1}, сумма: {columnSum}");
                    }
                }
            }
        }
        /*
         private void MatrixGridCurrent_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
         {
             if (e.EditAction == DataGridEditAction.Commit)
             {
                 DataRowView editedRow = e.Row.Item as DataRowView;
                 int columnIndex = e.Column.DisplayIndex;
                 int rowIndex = e.Row.GetIndex();

                 if (editedRow != null)
                 {
                     var newValue = ((TextBox)e.EditingElement).Text;

                     if (int.TryParse(newValue, out int parsedValue))
                     {
                         Sum();
                         originalMatrix[rowIndex, columnIndex] = parsedValue;  
                     }
                 }
             }
         }
        */

        /*
        private void MatrixGridCurrent_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                DataRowView editedRow = e.Row.Item as DataRowView;
                int columnIndex = e.Column.DisplayIndex;
                int rowIndex = e.Row.GetIndex();

                if (editedRow != null)
                {
                    var newValue = ((TextBox)e.EditingElement).Text;

                    if (int.TryParse(newValue, out int parsedValue))
                    {
                        editedRow[columnIndex] = parsedValue;  
                        originalMatrix[rowIndex, columnIndex] = parsedValue;
                    }
                    else
                    {
                      
                        editedRow[columnIndex] = 0;
                        originalMatrix[rowIndex, columnIndex] = 0;
                        MessageBox.Show("Введите целое число. Значение установлено в 0.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    Sum(); 
                }
            }
        }
        */
        /*
        private void MatrixGridCurrent_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editingElement = e.EditingElement as TextBox;
                if (editingElement != null)
                {
                    if (!int.TryParse(editingElement.Text, out int value))
                    {
                        MessageBox.Show("Пожалуйста, введите корректное целое число.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                        editingElement.Text = "0";
                        e.Cancel = true;
                        return;
                    } else
                    {
                        editingElement.Text = value.ToString();
                    }

                }
            }

            matrixGridCurrent.ItemsSource = ((DataView)matrixGrid.ItemsSource).ToTable().DefaultView;
        }
        */
        private void MatrixGridCurrent_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editingElement = e.EditingElement as TextBox;
                if (editingElement != null)
                {
                    if (!int.TryParse(editingElement.Text, out int value))
                    {
                        MessageBox.Show("Пожалуйста, введите корректное целое число.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                       
                        editingElement.Text = "0";
                        e.Cancel = true;   
                    }
                    else
                    {
                     
                        editingElement.Text = value.ToString();
                    }
                }
            }
        }




        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
      
            DataView currentDataView = matrixGridCurrent.ItemsSource as DataView;
            characteristicsValuesList.Items.Clear();

            if (currentDataView != null)
            {
                int rowCount = currentDataView.Count;
                int columnCount = currentDataView.Table.Columns.Count;

                int[,] matrix = new int[rowCount, columnCount];
 
                for (int i = 0; i < rowCount; i++)
                {
                    DataRowView rowView = currentDataView[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(rowView[j]);
                    }
                }

                double[] negativeSums = new double[columnCount];

             
                for (int col = 0; col < columnCount; col++)
                {
                    double sum = 0;
                    int countOfNegative = 0;
                    int[] negativeNumbers = new int[rowCount];
                    int negativeIndex = 0;

                    for (int row = 0; row < rowCount; row++)
                    {
                        if (matrix[row, col] < 0 && (row + 1) % 2 != 0) 
                        {
                            sum += Math.Abs(matrix[row, col]);
                            negativeNumbers[negativeIndex++] = matrix[row, col];
                            countOfNegative++;
                        }
                    }
                    string negativeNumbersStr = string.Join(", ", negativeNumbers.Take(negativeIndex));

                    characteristicsValuesList.Items.Add($"Колонка {col + 1}, \n  сумма модулей отрицательных нечетных элементов {sum}, \n количество  отрицательных элементов {countOfNegative}, \n элементы: {negativeNumbersStr}  \n ========== ");
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
                MessageBox.Show("matrixGridCurrent пуста или не существует.", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
