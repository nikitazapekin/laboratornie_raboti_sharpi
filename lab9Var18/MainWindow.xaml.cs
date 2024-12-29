using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace lab9Var18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Combinational comb;
        public Memory memory;
        public Register register;
        public MainWindow()
        {
            InitializeComponent();
           comb  = new Combinational(8);
          memory = new Memory();
            register = new Register(10);
        }

        private void ComputeComb_Click(object sender, RoutedEventArgs e)
        {


            try
            {

                int[] inputs = new int[8];
                inputs[0] = ParseInput(Input1.Text, "Вход 1");
                inputs[1] = ParseInput(Input2.Text, "Вход 2");
                inputs[2] = ParseInput(Input3.Text, "Вход 3");
                inputs[3] = ParseInput(Input4.Text, "Вход 4");
                inputs[4] = ParseInput(Input1.Text, "Вход 5");
                inputs[5] = ParseInput(Input2.Text, "Вход 6");
                inputs[6] = ParseInput(Input3.Text, "Вход 7");
                inputs[7] = ParseInput(Input4.Text, "Вход 8");

                comb.SetInputs(inputs);
                comb.SetInputs(inputs);
                int result = comb.ComputeOutput();
                ResultComb.Text = $"Выход: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void InvertComb_Click(object sender, RoutedEventArgs e)
        {
            comb.Invert();

            int[] inputs = new int[8];
            inputs = comb.GetInputs();
            Input1.Text = inputs[0].ToString();
            Input2.Text = inputs[1].ToString();
            Input3.Text = inputs[2].ToString();
            Input4.Text = inputs[3].ToString();
            Input5.Text = inputs[4].ToString();
            Input6.Text = inputs[5].ToString();
            Input7.Text = inputs[6].ToString();
            Input8.Text = inputs[7].ToString();
        }

        private int ParseInput(string input, string fieldName)
        {
            if (!int.TryParse(input, out int value) || (value != 0 && value != 1))
            {
                throw new ArgumentException($"Поле \"{fieldName}\" должно содержать только 0 или 1.");
            }
            return value;
        }

     


        private void SetTrigger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             
                var inputs = TriggerInputs.Text.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
 
                int[] parsedInputs = new int[inputs.Length];

           
                for (int i = 0; i < inputs.Length; i++)
                {
                   
                    if (!int.TryParse(inputs[i], out parsedInputs[i]) || (parsedInputs[i] != 0 && parsedInputs[i] != 1))
                    {
                        MessageBox.Show("Все значения должны быть либо 0, либо 1.");
                        return;
                    }
                }

              
                memory.SetInput(parsedInputs[0]);
              
                TriggerValues.Text = $"Входы: {string.Join(" ", parsedInputs)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ComputeOutput_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                memory.ComputeOutput();
                int state = memory.GetState();
                int inputValue = memory.GetTInput();
                TriggerOutput.Text = $"Результат: Вход: {inputValue}, Состояние: {state}";

          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void InvertMemory_Click(object sender, RoutedEventArgs e)
        {
            memory.Invert();
            memory.ComputeOutput();
            int state = memory.GetState();
            int inputValue = memory.GetTInput();
            TriggerOutput.Text = $"Результат: Вход: {inputValue}, Состояние: {state}";
        }







    private void  GenerateNums(object sender, RoutedEventArgs e)
    {
            try
            {
                Random random = new Random();
                int[][] parsedInputs = new int[10][];

                for (int i = 0; i < parsedInputs.Length; i++)
                {

                    parsedInputs[i] = new int[]
                    {
                random.Next(0, 2), 
                random.Next(0, 2), 
            
                    };
                }


                register.SetInputs(parsedInputs);


                string formattedInputs = "";
                for (int i = 0; i < parsedInputs.Length; i++)
                {
                    formattedInputs += $"[{parsedInputs[i][0]}, {parsedInputs[i][1]}] ";
                }


                TriggerArray.Text = "Регистры: " + formattedInputs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }


        }

        /*
        private void SetByIndexClick(object sender, RoutedEventArgs e)
        {

            try
            {

                int triggerIndex = int.Parse(TriggerIndex.Text);

                string[] triggerValues = TriggerRegisterValues.Text.Split(' ');


                if (triggerValues.Length != 2)
                {
                    MessageBox.Show("Ошибка: Введите 1 значения (0 или 1).");
                    return;
                }


                int[] parsedValues = new int[3];
                for (int i = 0; i < 2; i++)
                {
                    parsedValues[i] = int.Parse(triggerValues[i]);


                    if (parsedValues[i] != 0 && parsedValues[i] != 1)
                    {
                        MessageBox.Show("Ошибка: Значение должны быть 0 или 1.");
                        return;
                    }
                }


                if (triggerIndex < 0 || triggerIndex >= register.GetInputs().Length)
                {
                    MessageBox.Show("Ошибка: Индекс выходит за пределы массива.");
                    return;
                }


                var currentInputs = register.GetInputs();


                currentInputs[triggerIndex] = parsedValues;


                register.SetInputs(currentInputs);


                string formattedInputs = "";
                foreach (var input in currentInputs)
                {
                    formattedInputs += $"[{input[0]}, {input[1]}, {input[2]}] ";
                }


                TriggerArray.Text = "Регистры: " + formattedInputs;
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Неверный формат ввода.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }

        }

        */



        private void SetByIndexClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int triggerIndex = int.Parse(TriggerIndex.Text);

                string[] triggerValues = TriggerRegisterValues.Text.Split(' ');

                // Проверяем, что вводятся только два значения
                if (triggerValues.Length != 2)
                {
                    MessageBox.Show("Ошибка: Введите два значения (состояние и вход).");
                    return;
                }

                int[] parsedValues = new int[2]; // Массив из двух значений
                for (int i = 0; i < 2; i++)
                {
                    parsedValues[i] = int.Parse(triggerValues[i]);

                    if (parsedValues[i] != 0 && parsedValues[i] != 1)
                    {
                        MessageBox.Show("Ошибка: Значения должны быть 0 или 1.");
                        return;
                    }
                }

                // Проверка на корректный индекс
                if (triggerIndex < 0 || triggerIndex >= register.GetInputs().Length)
                {
                    MessageBox.Show("Ошибка: Индекс выходит за пределы массива.");
                    return;
                }

                var currentInputs = register.GetInputs();

                // Корректно присваиваем новые значения для состояния и входа
                currentInputs[triggerIndex][0] = parsedValues[0]; // Состояние
                currentInputs[triggerIndex][1] = parsedValues[1]; // Вход

                // Устанавливаем обновленные данные
                register.SetInputs(currentInputs);

                // Форматируем вывод
                string formattedInputs = "";
                foreach (var input in currentInputs)
                {
                    formattedInputs += $"[{input[0]}, {input[1]}] "; // Отображаем состояние и вход
                }

                TriggerArray.Text = "Регистры: " + formattedInputs;
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка: Неверный формат ввода.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }







    }
}

