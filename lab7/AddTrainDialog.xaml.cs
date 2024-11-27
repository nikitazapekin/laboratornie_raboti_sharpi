using System;
using System.Windows;
using System.Windows.Controls;

namespace lab7
{
    public partial class AddTrainDialog : Window
    {
        public string Destination { get;  set; }
        public string TrainNumber { get;   set; }
        public DateTime DepartureTime { get;  set; }

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
            try
            {
                 
                if (string.IsNullOrWhiteSpace(DestinationTextBox.Text) ||
                    string.IsNullOrWhiteSpace(TrainNumberTextBox.Text) ||
                    !DepartureDatePicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

          
                if (!int.TryParse(TrainNumberTextBox.Text.Trim(), out _))
                {
                    MessageBox.Show("Ошибка: номер поезда должен быть целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                if (!int.TryParse(HourComboBox.SelectedItem?.ToString(), out int hours) ||
                    !int.TryParse(MinuteComboBox.SelectedItem?.ToString(), out int minutes) ||
                    !int.TryParse(SecondComboBox.SelectedItem?.ToString(), out int seconds) ||
                    hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || seconds < 0 || seconds > 59)
                {
                    MessageBox.Show("Пожалуйста, выберите корректное время.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                Destination = DestinationTextBox.Text;
                TrainNumber = TrainNumberTextBox.Text;

                DateTime selectedDate = DepartureDatePicker.SelectedDate.Value;
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
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
