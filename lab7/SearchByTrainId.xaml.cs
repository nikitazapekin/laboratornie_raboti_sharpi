

using System;
using System.Collections.Generic;
using System.Windows;

namespace lab7
{
    public partial class SearchByTrainId : Window
    {
     
       
        private TrainCollection _trainCollection;

        public SearchByTrainId(TrainCollection trainCollection)
        {
            InitializeComponent();
            _trainCollection = trainCollection;
        }

        public List<TRAIN> MatchingTrains { get;  set; } = new List<TRAIN>();

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TrainNumberTextBox.Text.Trim(), out int trainNumber))
            {
                MessageBox.Show("Введите корректный номер поезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

          
            MatchingTrains = _trainCollection.FindByTrainNumber(trainNumber);
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
 