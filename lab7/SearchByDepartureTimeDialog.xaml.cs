using System;
using System.Collections.Generic;
using System.Windows;

namespace lab7
{
    public partial class SearchByDepartureTimeDialog : Window
    {
  
        private TrainCollection _trainCollection;

        public SearchByDepartureTimeDialog(TrainCollection trainCollection)
        {
            InitializeComponent();
            _trainCollection = trainCollection;
        }

        public List<TRAIN> MatchingTrains { get;  set; } = new List<TRAIN>();

        /*  private void SearchButton_Click(object sender, RoutedEventArgs e)
          {
              if (DepartureDatePicker.SelectedDate == null)
              {
                  MessageBox.Show("Выберите дату отправления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                  return;
              }

              DateTime selectedDate = DepartureDatePicker.SelectedDate.Value;

              MatchingTrains = _trainCollection.FindByDepartureDate(selectedDate);
              if (MatchingTrains.Count == 0)
              {
                  MessageBox.Show("Ничего не найдено!");
              }

              this.DialogResult = true;
              Close();
          }

          */

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartureDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату отправления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(StartTimeTextBox.Text) || string.IsNullOrWhiteSpace(EndTimeTextBox.Text))
            {
                MessageBox.Show("Введите диапазон времени.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(StartTimeTextBox.Text, out TimeSpan startTime) ||
                !TimeSpan.TryParse(EndTimeTextBox.Text, out TimeSpan endTime))
            {
                MessageBox.Show("Некорректный формат времени. Используйте формат ЧЧ:ММ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (startTime > endTime)
            {
                MessageBox.Show("Время начала должно быть меньше времени конца.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime selectedDate = DepartureDatePicker.SelectedDate.Value;
            DateTime startDateTime = selectedDate.Date + startTime;
            DateTime endDateTime = selectedDate.Date + endTime;

            MatchingTrains = _trainCollection.FindByDepartureTimeRange(startDateTime, endDateTime);

            if (MatchingTrains.Count == 0)
            {
                MessageBox.Show("Ничего не найдено!");
            }
            else
            {
                this.DialogResult = true;
            }

            Close();
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

      
    }
}
 