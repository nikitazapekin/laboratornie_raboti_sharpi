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

        private int[,] originalMatrix; // Матрица для хранения оригинальных значений

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
                    originalMatrix = new int[rows, columns]; // Инициализация оригинальной матрицы
                    for (int i = 0; i < rows; i++)
                    {
                        DataRow newRow = table.NewRow();
                        for (int j = 0; j < columns; j++)
                        {
                            int value = random.Next(-10, 11);
                            newRow[j] = value;
                            originalMatrix[i, j] = value; // Заполнение оригинальной матрицы
                        }
                        table.Rows.Add(newRow);
                    }

                    matrixGrid.ItemsSource = table.DefaultView;
                    matrixGridCurrent.ItemsSource = table.DefaultView; // Отображение начальных значений
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

                        originalMatrix = new int[rows, columns]; // Инициализация оригинальной матрицы
                        for (int i = 0; i < rows; i++)
                        {
                            DataRow newRow = table.NewRow();
                            for (int j = 0; j < columns; j++)
                            {
                                string value = inputWindow.InputFields[i][j].Text;
                                if (int.TryParse(value, out int parsedValue))
                                {
                                    newRow[j] = parsedValue;
                                    originalMatrix[i, j] = parsedValue; // Заполнение оригинальной матрицы
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
                        valuesList.Items.Add($"Сумма {j + 1} колонки: {columnSum}");
                    }
                }
            }
        }

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
                        originalMatrix[rowIndex, columnIndex] = parsedValue; // Обновление оригинальной матрицы
                    }
                }
            }
        }
        /*
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DataView dataViewCurrent = matrixGridCurrent.ItemsSource as DataView;

            if (dataViewCurrent != null)
            {
                int rowCount = dataViewCurrent.Count;
                int columnCount = dataViewCurrent.Table.Columns.Count;

                int[,] matrix = new int[rowCount, columnCount];

                for (int i = 0; i < rowCount; i++)
                {
                    DataRowView rowView = dataViewCurrent[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(rowView[j]);
                    }
                }

                // Сортировка
                  for (int i = 0; i < rowCount; i++)
                   {
                       for (int j = 0; j < columnCount - 1; j++)
                       {
                           for (int k = j + 1; k < columnCount; k++)
                           {
                               if (matrix[i, j] > matrix[i, k])
                               {
                                   int temp = matrix[i, j];
                                   matrix[i, j] = matrix[i, k];
                                   matrix[i, k] = temp;
                               }
                           }
                       }
                   }
              

              
                    // Обновление matrixGrid
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
                        newRow[j] = matrix[i, j]; // Вставьте отсортированные значения
                    }
                    sortedTable.Rows.Add(newRow);
                }

                matrixGrid.ItemsSource = sortedTable.DefaultView; // Обновление matrixGrid с отсортированными значениями
            }
            else
            {
                MessageBox.Show("Матрица пуста или не существует.", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        */
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из matrixGridCurrent
            DataView currentDataView = matrixGridCurrent.ItemsSource as DataView;
            characteristicsValuesList.Items.Clear();

            if (currentDataView != null)
            {
                int rowCount = currentDataView.Count;
                int columnCount = currentDataView.Table.Columns.Count;

                int[,] matrix = new int[rowCount, columnCount];

                // Заполнение матрицы из matrixGridCurrent
                for (int i = 0; i < rowCount; i++)
                {
                    DataRowView rowView = currentDataView[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(rowView[j]);
                    }
                }

                double[] negativeSums = new double[columnCount];

                // Подсчет суммы модулей отрицательных нечетных элементов
                for (int col = 0; col < columnCount; col++)
                {
                    double sum = 0;
                    int countOfNegative = 0;

                    for (int row = 0; row < rowCount; row++)
                    {
                        if (matrix[row, col] < 0 && (row + 1) % 2 != 0) // Индексация с 0
                        {
                            sum += Math.Abs(matrix[row, col]);
                            countOfNegative++;
                        }
                    }

                    characteristicsValuesList.Items.Add($"Колонка {col + 1}, сумма модулей отрицательных нечетных элементов {sum}, количество {countOfNegative}");
                    negativeSums[col] = sum;
                }

                // Индексы для сортировки
                int[] indices = Enumerable.Range(0, columnCount).ToArray();
                Array.Sort(negativeSums, indices);

                // Создание отсортированной матрицы
                int[,] sortedMatrix = new int[rowCount, columnCount];

                for (int col = 0; col < columnCount; col++)
                {
                    int originalCol = indices[col];
                    for (int row = 0; row < rowCount; row++)
                    {
                        sortedMatrix[row, col] = matrix[row, originalCol];
                    }
                }

                // Обновление DataTable и DataGrid
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

                // Обновление источника данных для matrixGrid
                matrixGrid.ItemsSource = sortedTable.DefaultView; // Обновление grid
            }
            else
            {
                MessageBox.Show("matrixGridCurrent пуста или не существует.", "Ошибка:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
