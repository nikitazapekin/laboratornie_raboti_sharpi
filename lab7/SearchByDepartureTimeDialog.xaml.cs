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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

      
    }
}
 