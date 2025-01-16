


  using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace lab9Itog
{
    public partial class MainWindow : Window
    {
        private Element currentElement;
        private Combinational combinationalElement;
        private Memory memoryElement;
        private Register registerElement;
        private MemoryChild memoryChild;
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
            memoryChild = new MemoryChild();
        }

        private void ElementSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selected = ((System.Windows.Controls.ComboBoxItem)ElementSelector.SelectedItem).Content.ToString();
            switch (selected)
            {
                case "Комбинированный элемент MOD2":
                    currentElement = combinationalElement;
                    InputValues.Text = "";
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    InputValues.Visibility = Visibility.Visible;
                    ShiftPanelSettings.Visibility = Visibility.Collapsed;
                    break;
                case "Триггер":
                    currentElement = memoryElement;
                    InputValues.Text = "";
                    InputValues.Visibility = Visibility.Visible;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    ShiftPanelSettings.Visibility = Visibility.Collapsed;
                    break;
                case "Регистр":
                    currentElement = registerElement;
                    InputValues.Text = "";
                    InputValues.Visibility = Visibility.Collapsed;
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


            foreach (var input in inputs)
            {
                if (input != "1" && input != "0")
                {
                    MessageBox.Show("Все введенные значения должны быть либо 1, либо 0.");
                    return;
                }
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
                if (inputs.Length != memory.InputCount)
                {
                    MessageBox.Show("Для Memory должно быть введено ровно 2 элемента.");
                    MessageBox.Show("Typed" + inputs.Length + "Requered" + memory.InputCount);
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
            if (currentElement == null)
            {
                MessageBox.Show("Выберите элемент");
                return;
            }
            OutputResult.Text = $"Выход: {currentElement.ComputeOutput()}";
        }

        private void InvertButton_Click(object sender, RoutedEventArgs e)
        {

            if (currentElement == null)
            {
                MessageBox.Show("Выберите элемент");
                return;
            }
            currentElement.Invert();
            OutputResult.Text = $"Выход (Инверсия): {currentElement.ComputeOutput()}";
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
                    string formattedInputs = "";
                    for (int i = 0; i < states.Length; i++)
                    {
                        formattedInputs += $"[{states[i][0]}, {states[i][1]}] ";
                    }
                    setInputsValues.Text = "Входы: " + formattedInputs;
                }
                else if (currentElement is Combinational combinational)
                {
                    string inputsString = "";
                    for (int i = 0; i < combinational.GetInputs().Length; i++)
                    {
                        inputsString += combinational.GetInputs()[i] + ", ";
                    }
                    setInputsValues.Text = "Входы: " + inputsString.TrimEnd(' ', ',');
                }


                else
                {
                    setInputsValues.Text = "Входы: " + string.Join(", ", memoryElement.GetAllInputs());
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
                    register.SaveToBinary(fileName);
                    MessageBox.Show($"Регистр сохранен в файл: {fileName}");
                }
                else if (currentElement is Memory memory)
                {
                    try
                    {
                        memory.SaveToBinary(fileName);
                        MessageBox.Show("Данные успешно сохранены в бинарный файл.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Только Memory поддерживает бинарное сохранение.");
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




        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

            if (currentElement == null)
            {
                MessageBox.Show("Выберите элемент");
                return;
            }


            string fileName = $"{currentElement.GetType().Name}_save";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл сохранения не найден.");
                return;
            }

            try
            {
                string binaryData = File.ReadAllText(filePath);

                if (currentElement is Combinational combinational)
                {
                    combinational.FromBinaryString(binaryData);
                    UpdateTriggersInfo();
                    MessageBox.Show("Данные для Combinational успешно загружены.");
                }
                else if (currentElement is Memory memory)
                {

                    try
                    {

                        memory.LoadFromBinary(fileName);
                        UpdateTriggersInfo();
                        MessageBox.Show("Данные успешно загружены из бинарного файла.");
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("Файл не найден. Сначала сохраните данные.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке: {ex.Message}");
                    }




                }
                else if (currentElement is Register register)
                {
                    register.LoadFromBinary(filePath);
                    UpdateTriggersInfo();
                    MessageBox.Show("Регистр успешно загружен из бинарного файла.");
                }
                else
                {
                    MessageBox.Show("Неизвестный тип элемента.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }




        private void RandomInputsClick(object sender, RoutedEventArgs e)
        {

            combinationalElement.RandomSetInputs();
            UpdateTriggersInfo();
        }

        private void HandleResize(object sender, RoutedEventArgs e)
        {
            memoryChild.ResizeInputs(4);

            UpdateTriggersInfo();
        }



        private void handleSort(object sender, RoutedEventArgs e)
        {
         
            Combinational[] combinationalArray = new Combinational[]
            {
        new Combinational(5) { Inputs = new int[] { 1, 0, 1, 1, 1 } },
        new Combinational(5) { Inputs = new int[] { 0, 1, 1, 0, 1 } },
        new Combinational(5) { Inputs = new int[] { 0, 0, 0, 0, 0 } }
            };
 
            Array.Sort(combinationalArray);

          
            string result = "[";
            foreach (var item in combinationalArray)
            {
                result += "[" + string.Join(", ", item.Inputs) + "], ";
            }
            result = result.TrimEnd(',', ' ') + "]";

          
            outputTextBlock.Text = result;
            handleDisplayMessage();
        }


        private void handleDisplayMessage()
        {
            MessageBox.Show("Сортировка прошла успешно");
        }

        private void handleCompare(object sender, RoutedEventArgs e)
        {
            Combinational[] combinationalArray = new Combinational[]
         {
        new Combinational(5) { Inputs = new int[] { 1, 0, 1, 1, 0 } },
     
        new Combinational(5) { Inputs = new int[] { 0, 0, 0, 0, 0 } }
         };
            if (combinationalArray[0] == combinationalArray[1]) 
            {
                MessageBox.Show("У обоих элементов 1 или 0  на выходе");
            } 
            else if (combinationalArray[0] > combinationalArray[1])
            {
                MessageBox.Show("У первого элемента  1 на выходе, у второго 0");
            } else if (combinationalArray[0] < combinationalArray[1])
            {
                MessageBox.Show("У второго элемента  1 на выходе, у первого 0");
            } else
            {
                MessageBox.Show("Error");
            }
        }






        private void handleReadXML(object sender, RoutedEventArgs e)
        {
          

            try
            {
                combinationalElement.ReadXmlData("combinational.xml");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении: {ex.Message}");

            }


            UpdateTriggersInfo();
        }

        private void handleSave(object sender, RoutedEventArgs e)
        {
            try
            {
                combinationalElement.SaveToXml("combinational.xml");
                MessageBox.Show("Сохранено в xml");
                
            }
            catch (Exception ex)
            {
              
            }

        }





        private void handleReadXMLRegister(object sender, RoutedEventArgs e)
        {


            try
            {
              registerElement.ReadXmlData("register.xml");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении: {ex.Message}");

            }


            UpdateTriggersInfo();
        }

        private void handleSaveRegister(object sender, RoutedEventArgs e)
        {
            try
            {
            registerElement.SaveToXml("register.xml");
                MessageBox.Show("Сохранено в xml");

            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }

        }





    }

}
 


