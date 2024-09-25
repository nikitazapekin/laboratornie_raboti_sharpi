using System;
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
                MessageBox.Show("Ошибка: " + ex.Message);
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
                MessageBox.Show("Матрица пуста или не существует.");
            }
        }

        private void matrixRows_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}