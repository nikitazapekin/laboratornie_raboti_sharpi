using System;
using System.Windows;

namespace lab_3_variant_8_zadanie_1
{
    public partial class InputDialog : Window
    {
        public int[] Numbers { get; private set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Numbers = Array.ConvertAll(NumbersInput.Text.Split(','), int.Parse);
                this.DialogResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Пожалуйста, введите корректные числа через запятую.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
 