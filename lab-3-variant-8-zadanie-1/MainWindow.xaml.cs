
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace lab_3_variant_8_zadanie_1
{
    public partial class MainWindow : Window
    {
        // Приватный массив для хранения сгенерированных чисел
        private int[] randomNumbers;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Метод для нахождения минимального элемента
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

        // Метод для нахождения суммы между первым и вторым отрицательными числами
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

            // Если не найдено двух отрицательных чисел
            if (firstNegIndex == -1 || secondNegIndex == -1)
            {
                return 0;
            }

            // Вычисляем сумму элементов между первым и вторым отрицательными числами
            int sum = 0;
            for (int i = firstNegIndex + 1; i < secondNegIndex; i++)
            {
                sum += randomNumbers[i];
            }
            return sum;
        }

        // Метод для преобразования массива
        private int[] TransformArray()
        {
            if (randomNumbers == null || randomNumbers.Length == 0)
                return Array.Empty<int>();

            // Массив для элементов, модуль которых <= 1
            int[] lessThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) <= 1);
            // Массив для всех остальных элементов
            int[] greaterThanOne = Array.FindAll(randomNumbers, num => Math.Abs(num) > 1);

            // Объединяем два массива
            return lessThanOne.Concat(greaterThanOne).ToArray();
        }

        // Обновление интерфейса с новым массивом
        private void DisplayTransformedArray(int[] transformedArray)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var num in transformedArray)
            {
                sb.Append(num);
                sb.Append(" ");
            }
            TransformedArray.Text = sb.ToString();
        }

        // Генерация случайных чисел
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(InputN.Text, out int n) && n > 0)
            {
                Random random = new Random();
                randomNumbers = new int[n]; // Инициализация массива

                StringBuilder sb = new StringBuilder();

                // Генерация n случайных чисел
                for (int i = 0; i < n; i++)
                {
                    randomNumbers[i] = random.Next(-20, 101); // Генерация числа от -20 до 100
                    sb.Append(randomNumbers[i]);
                    sb.Append(" ");
                }

                // Вывод сгенерированных чисел
                OutputNumbers.Text = sb.ToString();

                // Нахождение и вывод минимального элемента
                int minElement = GetMinimalElement();
                MinNumber.Text = minElement.ToString();

                // Нахождение и вывод суммы между первым и вторым отрицательными числами
                int sumBetweenNegatives = GetSumBetweenFirstTwoNegatives();
                Sum.Text = sumBetweenNegatives.ToString();

                // Преобразование массива и вывод
                int[] transformedArray = TransformArray();
                DisplayTransformedArray(transformedArray);
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
*/