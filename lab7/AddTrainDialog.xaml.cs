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
            for (int i = 0; i < 24; i++)
            {
                HourComboBox.Items.Add(i.ToString("D2"));
            }

         
            for (int i = 0; i < 60; i++)
            {
                MinuteComboBox.Items.Add(i.ToString("D2"));
                SecondComboBox.Items.Add(i.ToString("D2"));
            }

          
            HourComboBox.SelectedIndex = 0;
            MinuteComboBox.SelectedIndex = 0;
            SecondComboBox.SelectedIndex = 0;
/*
 */


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

        /*
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
        */


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DestinationTextBox.Text) ||
                string.IsNullOrWhiteSpace(TrainNumberTextBox.Text) ||
                !DepartureDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            Destination = DestinationTextBox.Text;
            TrainNumber = TrainNumberTextBox.Text;

            DateTime selectedDate = DepartureDatePicker.SelectedDate.Value;
            int hours = int.Parse(HourComboBox.SelectedItem.ToString());
            int minutes = int.Parse(MinuteComboBox.SelectedItem.ToString());
            int seconds = int.Parse(SecondComboBox.SelectedItem.ToString());
            DepartureTime = new DateTime(
                selectedDate.Year,
                selectedDate.Month,
                selectedDate.Day,
                hours,
                minutes,
                seconds
            );

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
