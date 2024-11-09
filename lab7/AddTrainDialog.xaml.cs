using System;
using System.Windows;
using System.Windows.Controls;

namespace lab7
{
    public partial class AddTrainDialog : Window
    {
        public string Destination { get; private set; }
        public string TrainNumber { get; private set; }
        public DateTime DepartureTime { get; private set; }

        public AddTrainDialog()
        {
            InitializeComponent();
        }





        private void DatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            DateTime newDate;
            DatePicker datePickerObj = sender as DatePicker;
            if (!DateTime.TryParse(e.Text, out newDate))
            {
                MessageBox.Show("Пожалуйста, введите корректный формат даты (dd.mm.yy)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(DestinationTextBox.Text) ||
                string.IsNullOrWhiteSpace(TrainNumberTextBox.Text) ||
                !DepartureTimePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            Destination = DestinationTextBox.Text;
            TrainNumber = TrainNumberTextBox.Text;
            DepartureTime = DepartureTimePicker.SelectedDate.Value;

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
