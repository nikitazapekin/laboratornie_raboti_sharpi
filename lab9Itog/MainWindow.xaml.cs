using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
 
 
 
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace lab9Itog
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            //    registerElement = new Register(10);
        }

        private void ElementSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selected = ((System.Windows.Controls.ComboBoxItem)ElementSelector.SelectedItem).Content.ToString();
            switch (selected)
            {
                case "Комбинированный элемент MOD2":
                    currentElement = combinationalElement;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    RegisterState.Visibility = Visibility.Collapsed;
                    break;
                case "D-Trigger":
                    currentElement = memoryElement;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    RegisterState.Visibility = Visibility.Collapsed;
                    break;
          /*      case "Register":
                    currentElement = registerElement;
                    ShiftPanel.Visibility = Visibility.Visible;
                    RegisterState.Visibility = Visibility.Visible;
                    break;
          */

            }
            UpdateTriggersInfo();
         DisplayElementProperties();
        }

        private void DisplayElementProperties()
        {
           
        }
      
        private void SetInputsButton_Click(object sender, RoutedEventArgs e)
        {
            var inputs = InputValues.Text.Split(',')
                .Select(int.Parse)
                .ToArray();

            if (currentElement == null)
            {
                MessageBox.Show($"Выберите элемент");
                return;
            }

            if (currentElement is Combinational combinational)
            {
                combinational.SetInputs(inputs);
                UpdateTriggersInfo(); 
            }
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
        

            UpdateTriggersInfo();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var writer = new StreamWriter("save.txt"))
            {
              
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
          
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //   registerElement.SetSetState(1);
            UpdateTriggersInfo();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            //  registerElement.SetSetState(0);
            UpdateTriggersInfo();
        }


 


        private void UpdateTriggersInfo()
        {
            try
            {
                if (currentElement is Register)
                {
                    OutputAllTriggersInRegister.Text = "Triggers: " + currentElement.ToString();
                    RegisterState.IsChecked = registerElement.getCurrentState() == 0 ? false : true;
                }
                else if (currentElement is Combinational combinational)
                {
                    // Update inputs string only if inputs are valid
                    string inputsString = string.Join(", ", combinational.GetInputs());
                    setInputsValues.Text = "Inputs: " + inputsString;
                }
                else
                {
                    setInputsValues.Text = "Inputs: " + currentElement.ToString();
                }
            }
            catch (NullReferenceException)
            {
                // Handle the error if currentElement is null
            }
        }
    }
}
 