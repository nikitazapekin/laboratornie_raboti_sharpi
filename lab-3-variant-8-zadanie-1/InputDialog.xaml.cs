using System;
using System.Globalization;
using System.Windows;

namespace lab_3_variant_8_zadanie_1
{
    public partial class InputDialog : Window
    {
        public double[] Numbers { get; private set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Numbers = Array.ConvertAll(NumbersInput.Text.Split(','), s => double.Parse(s.Trim(), CultureInfo.InvariantCulture));
              
                this.DialogResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Пожалуйста, введите корректные числа через запятую.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
 