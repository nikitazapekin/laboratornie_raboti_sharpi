﻿using System;
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
                    RegisterStateFirst.Visibility = Visibility.Collapsed;
                    RegisterStateSecond.Visibility = Visibility.Collapsed;
                    break;
                case "D-Trigger":
                    currentElement = memoryElement;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    RegisterStateFirst.Visibility = Visibility.Collapsed;
                    RegisterStateSecond.Visibility = Visibility.Collapsed;
                    break;
                case "Register":
                    currentElement = registerElement;
                    ShiftPanel.Visibility = Visibility.Visible;
                    RegisterStateFirst.Visibility = Visibility.Visible;
                    RegisterStateSecond.Visibility = Visibility.Visible;
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

         
            if (inputs.Length == 0)
            {
                MessageBox.Show("Введите хотя бы один элемент.");
                return;
            }

         
            if (currentElement is Register register)
            {

                if (inputs.Length != 8)
                {
                    MessageBox.Show("Для Register должно быть введено ровно 8 элементов.");
                    return;
                }





              
                 try
                  {


                      int[][] parsedInputs = new int[8][];
                      parsedInputs[0] = new int[] { 1, 1 };
                      parsedInputs[1] = new int[] { 1, 0 };
                      parsedInputs[2] = new int[] { 0, 1 };
                      parsedInputs[3] = new int[] { 0, 0 };
                    parsedInputs[4] = new int[] { 1, 1 };
                    parsedInputs[5] = new int[] { 1, 0 };
                    parsedInputs[6] = new int[] { 0, 1 };
                    parsedInputs[7] = new int[] { 0, 0 };

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

        /*  private void ShiftButton_Click(object sender, RoutedEventArgs e)
          {

              UpdateTriggersInfo();
          }

          */



        private void ShiftButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentElement is Register register)
            {
                // Проверка значения, введенного пользователем
                if (int.TryParse(ShiftValue.Text, out int shiftBits))
                {
                    try
                    {
                        // Вызов метода Shift
                        register.Shift(shiftBits);

                        // Обновление состояния после сдвига
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

        /* private void ShiftButton_Click(object sender, RoutedEventArgs e)
         {
             if (currentElement is Register register)
             {
                 try
                 {
                     // Извлечение первого элемента из входных данных
                     var states = register.GetInputs();
                     if (states.Length == 0)
                     {
                         MessageBox.Show("Нет данных для сдвига.");
                         return;
                     }

                     var firstElement = states[0]; // Первый элемент, например [1, 0]

                     // Получение значения сдвига из TextBox
                     if (!int.TryParse(ShiftValue.Text, out int shiftBits) || shiftBits < 0)
                     {
                         MessageBox.Show("Введите корректное неотрицательное значение для сдвига.");
                         return;
                     }

                     // Выполнение сдвига
                     register.Shift(shiftBits);

                     // Обновление интерфейса
                     UpdateTriggersInfo();
                     MessageBox.Show($"Первый элемент ({firstElement[0]}, {firstElement[1]}) был сдвинут на {shiftBits} бит.");
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show($"Ошибка при выполнении сдвига: {ex.Message}");
                 }
             }
             else
             {
                 MessageBox.Show("Сдвиг доступен только для элемента Register.");
             }
         }
         */


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
                string fileName = $"{currentElement.GetType().Name}_save.bin";

                if (currentElement is Combinational combinational)
                {
                    combinational.SaveToFile(fileName);
                    MessageBox.Show($"Комбинированный элемент сохранен в файл: {fileName}");
                }
                else if (currentElement is Register register)
                {
                  
                    MessageBox.Show($"Регистр сохранен в файл: {fileName}");
                }
                else if (currentElement is Memory memory)
                {
                    memory.SaveToFile(fileName);
                    MessageBox.Show($"Память сохранена в файл: {fileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
    }
}
 