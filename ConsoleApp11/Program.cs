using System;

class Program
{
    static void Main()
    {
        // Step 1: Create a 2D matrix
        int[,] matrix = {
            { 1, -2, 3 },
            { -4, -15, 6 },
            { -7, 8, -9 }
        };

        // Step 2: Calculate the sum of absolute values of negative numbers for each column
        int columnCount = matrix.GetLength(1);
        double[] negativeSums = new double[columnCount];

        for (int col = 0; col < columnCount; col++)
        {
            double sum = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                if (matrix[row, col] < 0)
                {
                    sum += Math.Abs(matrix[row, col]);
                }
            }

            negativeSums[col] = sum;
        }

        // Step 3: Sort columns based on the calculated sums
        int[] indices = new int[columnCount];
        for (int i = 0; i < columnCount; i++)
        {
            indices[i] = i;
        }

        Array.Sort(negativeSums, indices);

        // Step 4: Create a new sorted matrix based on the sorted indices
        int[,] sortedMatrix = new int[matrix.GetLength(0), columnCount];

        for (int col = 0; col < columnCount; col++)
        {
            int originalCol = indices[col];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                sortedMatrix[row, col] = matrix[row, originalCol];
            }
        }

        // Output the sorted matrix
        Console.WriteLine("Sorted Matrix:");
        for (int row = 0; row < sortedMatrix.GetLength(0); row++)
        {
            for (int col = 0; col < sortedMatrix.GetLength(1); col++)
            {
                Console.Write(sortedMatrix[row, col] + "\t");
            }
            Console.WriteLine();
        }
    }
}
