using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
                TextBox[] inputTextBoxes = new TextBox[] { Input1, Input2, Input3, Input4, Input1, Input2, Input3, Input4 };

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i] = ParseInput(inputTextBoxes[i].Text, $"Вход {i + 1}");
                }



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
           
            TextBox[] inputTextBoxes = new TextBox[] { Input1, Input2, Input3, Input4, Input5, Input6, Input7, Input8 };
            int[] newInputs = new int[inputTextBoxes.Length];

            for (int i = 0; i < inputTextBoxes.Length; i++)
            {
                
                if (int.TryParse(inputTextBoxes[i].Text, out int value) && (value == 0 || value == 1))
                {
                    newInputs[i] = value;
                }
                else
                {
                    MessageBox.Show($"Некорректное значение в элементе {i + 1}. Введите 0 или 1.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;  
                }
            }

          
            comb.SetInputs(newInputs);

           
            comb.Invert();

           
            int[] invertedInputs = comb.GetInputs();
            for (int i = 0; i < invertedInputs.Length; i++)
            {
                inputTextBoxes[i].Text = invertedInputs[i].ToString();
            }
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


  
        private void SetByIndexClick(object sender, RoutedEventArgs e)
        {
            try
            {
               
                int triggerIndex = int.Parse(TriggerIndex.Text);

             
                string[] triggerValues = TriggerRegisterValues.Text.Split(' ');

                
                if (triggerValues.Length != 2)
                {
                    MessageBox.Show("Ошибка: Введите два значения (состояние и вход).");
                    return;
                }
 
                int[] parsedValues = new int[2];
                for (int i = 0; i < 2; i++)
                {
                    parsedValues[i] = int.Parse(triggerValues[i]);
 
                    if (parsedValues[i] != 0 && parsedValues[i] != 1)
                    {
                        MessageBox.Show("Ошибка: Значения должны быть 0 или 1.");
                        return;
                    }
                }

                
                if (triggerIndex < 0 || triggerIndex >= register.GetInputs().Length)
                {
                    MessageBox.Show("Ошибка: Индекс выходит за пределы массива.");
                    return;
                }

                
                var currentInputs = register.GetInputs();

             
                currentInputs[triggerIndex][0] = parsedValues[0]; // Состояние
                currentInputs[triggerIndex][1] = parsedValues[1]; // Вход
 
                register.SetInputs(currentInputs);
 
                string formattedInputs = "";
                foreach (var input in currentInputs)
                {
                    formattedInputs += $"[{input[0]}, {input[1]}] ";
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

        /*
        private void  MoveClick(object sender, RoutedEventArgs e)
        {
            register.Shift(1);
            var currentInputs = register.GetInputs();


         

            register.SetInputs(currentInputs);


            string formattedInputs = "";
            foreach (var input in currentInputs)
            {
                formattedInputs += $"[{input[0]}, {input[1]}] ";
            }

            TriggerArray.Text = "Регистры: " + formattedInputs;
        }
        */


        private void MoveClick(object sender, RoutedEventArgs e)
        {
            
            string shiftValueText = TriggerMoveValue.Text;

           
            if (int.TryParse(shiftValueText, out int shiftValue))
            {
             
                register.Shift(shiftValue);

            
                var currentInputs = register.GetInputs();

               
                register.SetInputs(currentInputs);

               
                string formattedInputs = "";
                foreach (var input in currentInputs)
                {
                    formattedInputs += $"[{input[0]}, {input[1]}] ";
                }

              
                TriggerArray.Text = "Регистры: " + formattedInputs;
            }
            else
            {
              
                MessageBox.Show("Введите корректное число для значения сдвига.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }




        private void InvertRegister(object sender, RoutedEventArgs e)
        {
            register.Invert();
            var currentInputs = register.GetInputs();




            register.SetInputs(currentInputs);


            string formattedInputs = "";
            foreach (var input in currentInputs)
            {
                formattedInputs += $"[{input[0]}, {input[1]}] ";
            }

            TriggerArray.Text = "Регистры: " + formattedInputs;

        }

















        private void SaveInputsCombinational(object sender, RoutedEventArgs e)

        {
            int[] inputs = new int[8];


            try
            {
                string fileName = $"comb_save";
                using (var writer = new StreamWriter(fileName))
                {
                    writer.WriteLine(comb.ToBinaryString());
                }
                MessageBox.Show($"Комбинированный элемент сохранен в файл: {fileName}");

            }
            catch
            {
                MessageBox.Show("Ошибка");
            }

        }

        private void LoadInputsCombinational(object sender, RoutedEventArgs e)

        {
            string fileName = $"comb_save";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);






            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл сохранения не найден.");
                return;
            }

            try
            {
                string binaryData = File.ReadAllText(filePath);


                comb.FromBinaryString(binaryData);

                MessageBox.Show("Данные для Combinational успешно загружены.");
 



                int[] inputs = comb.GetInputs();
                TextBox[] inputTextBoxes = new TextBox[] { Input1, Input2, Input3, Input4, Input5, Input6, Input7, Input8 };

                for (int i = 0; i < inputs.Length; i++)
                {
                    inputTextBoxes[i].Text = inputs[i].ToString();
                }



            }
            catch
            {
                MessageBox.Show("Ошибка");
            }


        }






        private void SaveMemory(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = "memory_save.bin";
                memory.SaveToBinary(fileName);
                MessageBox.Show($"Элемент триггера сохранен в файл: {fileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void LoadMemory(object sender, RoutedEventArgs e)
        {
            string fileName = "memory_save.bin";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл сохранения не найден.");
                return;
            }

            try
            {
                memory.LoadFromBinary(filePath);

                MessageBox.Show("Данные для Memory успешно загружены.");

               
                int inputs = memory.GetTInput();
                int state = memory.GetState();

             
                string inputsString = $"Результат: Вход: {inputs}, Состояние: {state}";

              TriggerOutput.Text = inputsString;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }







        private void SaveRegister(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = "register_save.bin";
                register.SaveToBinary(fileName);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }



        private void LoadRegister(object sender, RoutedEventArgs e)
        {

            string fileName = "register_save.bin";
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл сохранения не найден.");
                return;
            }

            try
            {

                register.LoadFromBinary(filePath);

                MessageBox.Show("Данные для Register успешно загружены.");





                int[][] parsedInputs = register.GetInputs();


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






    }
}

