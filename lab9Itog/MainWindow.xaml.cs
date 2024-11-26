using lab9Itog.Classes;
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

namespace lab9Itog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Combinational combinationalElement;
        private Memory memoryElement;
        private Register registerElement;
        private Element currentElement;
        private void InitializeClasses()
        {
            combinationalElement = new Combinational(5);

           // memoryElement = new Memory();
         //   registerElement = new Register(10);
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeClasses();
        }



        private void ElementSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selected = ((System.Windows.Controls.ComboBoxItem)ElementSelector.SelectedItem).Content.ToString();
            switch (selected)
            {
                case "MOD2 (Combinational)":
                    currentElement = combinationalElement;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    RegisterState.Visibility = Visibility.Collapsed;
                    break;
          /*      case "D-Trigger (Memory)":
                    currentElement = memoryElement;
                    ShiftPanel.Visibility = Visibility.Collapsed;
                    RegisterState.Visibility = Visibility.Collapsed;
                    break;
                case "Register":
                    currentElement = registerElement;
                    ShiftPanel.Visibility = Visibility.Visible;
                    RegisterState.Visibility = Visibility.Visible;
                    break;
          */
            }
         //   UpdateTriggersInfo();
            DisplayElementProperties();
        }



        private void DisplayElementProperties()
        {
         //   ElementName.Text = $"Name: {currentElement.Name}";
        //    InputCount.Text = $"Input Count: {currentElement.InputCount}";
          //  OutputCount.Text = $"Output Count: {currentElement.OutputCount}";
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
                else
                {
                    OutputAllTriggersInRegister.Text = "Inputs: " + currentElement.ToString();
                }
            }
            catch (NullReferenceException)
            {

            }

        }

    }
}
