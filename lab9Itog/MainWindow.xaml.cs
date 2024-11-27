using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace lab9Itog
{
    public partial class MainWindow : Window
    {
        private Element currentElement;
        private Combinational combinationalElement;
        private Memory memoryElement;
        private Register registerElement;

        public MainWindow()
        {
            InitializeComponent();
            InitializeElements();
        }

        private void InitializeElements()
        {
            combinationalElement = new Combinational(5);
            memoryElement = new Memory();
            UpdateTriggersInfo();
            registerElement = new Register(8);
        }

        private void ElementSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selected = ((System.Windows.Controls.ComboBoxItem)ElementSelector.SelectedItem).Content.ToString();
            switch (selected)
            {
                case "Комбинированный элемент MOD2":
                    currentElement = combinationalElement;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    ShiftPanelSettings.Visibility = Visibility.Collapsed;
               
                    break;
                case "D-Trigger":
                    currentElement = memoryElement;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    ShiftPanelSettings.Visibility = Visibility.Collapsed;
                    
                    break;
                case "Register":
                    currentElement = registerElement;
                    ShiftPanel.Visibility = Visibility.Visible;
                    ShiftPanelSettings.Visibility = Visibility.Visible;
                 
                    break;
            }
            UpdateTriggersInfo();
       
        }
 

        private void SetInputsButton_Click(object sender, RoutedEventArgs e)
        {
            var inputs = InputValues.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (currentElement == null)
            {
                MessageBox.Show("Выберите элемент");
                return;
            }

            if (inputs.Length == 0 && currentElement is not Register)
            {
                MessageBox.Show("Введите данные.");
                return;
            }

            if (currentElement is Register register)
            {
                try
                {
                    int[][] parsedInputs;

               
                    if (inputs.Length == 0)
                    {
                        parsedInputs = new int[8][];
                        parsedInputs[0] = new int[] { 1, 1 };
                        parsedInputs[1] = new int[] { 1, 1 };
                        parsedInputs[2] = new int[] { 1, 0 };
                        parsedInputs[3] = new int[] { 1, 1 };
                        parsedInputs[4] = new int[] { 0, 0 };
                        parsedInputs[5] = new int[] { 0, 1 };
                        parsedInputs[6] = new int[] { 1, 0 };
                        parsedInputs[7] = new int[] { 1, 1 };
                    }
                    else
                    {
                     
                        parsedInputs = new int[inputs.Length][];
                        for (int i = 0; i < inputs.Length; i++)
                        {
                            var values = inputs[i].Split(',');
                            if (values.Length != 2 || !int.TryParse(values[0], out var first) || !int.TryParse(values[1], out var second))
                            {
                                MessageBox.Show("Каждый элемент для Register должен быть парой чисел, разделенных запятой.");
                                return;
                            }

                            parsedInputs[i] = new int[] { first, second };
                        }
                    }

                    register.SetInputs(parsedInputs);
                    MessageBox.Show("Входные значения успешно установлены для Register.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при установке входных значений: {ex.Message}");
                }
            }
            else if (currentElement is Combinational combinational)
            {
                if (inputs.Length != 5)
                {
                    MessageBox.Show("Для Combinational должно быть введено ровно 5 элементов.");
                    return;
                }

                int[] parsedInputs = new int[inputs.Length];

                for (int i = 0; i < inputs.Length; i++)
                {
                    if (!int.TryParse(inputs[i], out parsedInputs[i]))
                    {
                        MessageBox.Show("Все значения должны быть числами.");
                        return;
                    }
                }

                combinational.SetInputs(parsedInputs);
            }
            else if (currentElement is Memory memory)
            {
                if (inputs.Length != 2)
                {
                    MessageBox.Show("Для Memory должно быть введено ровно 2 элемента.");
                    return;
                }

                int[] parsedInputs = new int[inputs.Length];

                for (int i = 0; i < inputs.Length; i++)
                {
                    if (!int.TryParse(inputs[i], out parsedInputs[i]))
                    {
                        MessageBox.Show("Все значения должны быть числами.");
                        return;
                    }
                }

                memory.SetInputs(parsedInputs);
            }

            UpdateTriggersInfo();
        }


        private void ComputeButton_Click(object sender, RoutedEventArgs e)
        {
            OutputResult.Text = $"Output: {currentElement.ComputeOutput()}";
        }

        private void InvertButton_Click(object sender, RoutedEventArgs e)
        {
            currentElement.Invert();
            OutputResult.Text = $"Output (Inverted): {currentElement.ComputeOutput()}";
            UpdateTriggersInfo();
        }

   

        private void ShiftButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentElement is Register register)
            {
                
                if (int.TryParse(ShiftValue.Text, out int shiftBits))
                {
                    try
                    {
                  
                        register.Shift(shiftBits);

                  
                        UpdateTriggersInfo();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сдвиге: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное число для сдвига.");
                }
            }
            else
            {
                MessageBox.Show("Сдвиг доступен только для Register.");
            }
        }
 
        private void UpdateTriggersInfo()
        {
            try
            {
                if (currentElement is Register register)
                {
                    var states = register.GetInputs();

                    string formattedInputs = string.Join(" ", states.Select(pair => $"[{pair[0]}, {pair[1]}]"));
                    setInputsValues.Text = "Inputs: " + formattedInputs;
                 

                }
                else if (currentElement is Combinational combinational)
                {
                    string inputsString = string.Join(", ", combinational.GetInputs());
                    setInputsValues.Text = "Inputs: " + inputsString;
                }
                else
                {
                    setInputsValues.Text = "Inputs: " + string.Join(", ", memoryElement.GetAllInputs());
                }
            }
            catch (NullReferenceException)
            {
             
            }
        }

       
        private void SaveButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentElement == null)
            {
                MessageBox.Show("Нет выбранного элемента для сохранения.");
                return;
            }

            try
            {
                string fileName = $"{currentElement.GetType().Name}_save";

                if (currentElement is Combinational combinational)
                {
                  

                    using (var writer = new StreamWriter(fileName))
                    {
                   
                       writer.WriteLine(combinationalElement.ToBinaryString());
                    }
                    MessageBox.Show($"Комбинированный элемент сохранен в файл: {fileName}");
                }
                else if (currentElement is Register register)
                {

                    using (var writer = new StreamWriter(fileName))
                    {

                        writer.WriteLine(memoryElement.ToBinaryString());
                    }
                    MessageBox.Show($"Регистр сохранен в файл: {fileName}");
                }
                else if (currentElement is Memory memory)
                {

                    using (var writer = new StreamWriter(fileName))
                    {

                        writer.WriteLine(registerElement.ToBinaryString());
                    }

                    MessageBox.Show($"Память сохранена в файл: {fileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }





        private void SetTriggerButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentElement is Register register)
            {
             
                if (!int.TryParse(TriggerIndex.Text, out int index) || index < 0 || index >= register.GetInputs().Length)
                {
                    MessageBox.Show("Введите корректный индекс.");
                    return;
                }

 
                var values = TriggerValues.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length != 2 ||
                    !int.TryParse(values[0], out int value1) ||
                    !int.TryParse(values[1], out int value2) ||
                    (value1 != 0 && value1 != 1) ||
                    (value2 != 0 && value2 != 1))
                {
                    MessageBox.Show("Введите два значения (0 или 1) через пробел.");
                    return;
                }

              
                try
                {
                    var inputs = register.GetInputs();
                    inputs[index] = new[] { value1, value2 };
                    register.SetInputs(inputs);
                    MessageBox.Show($"Триггер на индексе {index} обновлен: [{value1}, {value2}]");

                  
                    UpdateTriggersInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении триггера: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Триггеры можно обновить только для Register.");
            }
        }

    }
}
 