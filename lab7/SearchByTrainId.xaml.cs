

using System;
using System.Collections.Generic;
using System.Windows;

namespace lab7
{
    public partial class SearchByTrainId : Window
    {
        /*  private List<TRAIN> _trains;

          public SearchByTrainId(List<TRAIN> trains)
          {
              InitializeComponent();
              _trains = trains;
          }
        */
        private TrainCollection _trainCollection;

        public SearchByTrainId(TrainCollection trainCollection)
        {
            InitializeComponent();
            _trainCollection = trainCollection;
        }

        public List<TRAIN> MatchingTrains { get; private set; } = new List<TRAIN>();

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TrainNumberTextBox.Text.Trim(), out int trainNumber))
            {
                MessageBox.Show("Введите корректный номер поезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //      MatchingTrains = FindByTrainNumber(trainNumber);
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

       /* private List<TRAIN> FindByTrainNumber(int trainNumber)
        {
            List<TRAIN> matchingTrains = new List<TRAIN>();

            foreach (TRAIN train in _trains)
            {
                if (train.TrainNumber == trainNumber)
                {
                    matchingTrains.Add(train);
                }
            }
            return matchingTrains;
        }
       */
    }
}
/*
using System;
using System.Collections.Generic;
using System.Windows;

namespace lab7
{
    public partial class SearchByTrainId : Window
    {
        private List<TRAIN> _trains;

        public SearchByTrainId(List<TRAIN> trains)
        {
            InitializeComponent();
            _trains = trains;
        }

        public List<TRAIN> MatchingTrains { get; private set; } = new List<TRAIN>();

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TrainNumberTextBox.Text.Trim(), out int trainNumber))
            {
                MessageBox.Show("Введите корректный номер поезда.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MatchingTrains = FindByTrainNumber(trainNumber);

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

       private List<TRAIN> FindByTrainNumber(int trainNumber)
        {
            List<TRAIN> matchingTrains = new List<TRAIN>();

          foreach (TRAIN train in _trains)
            {
                if (train.TrainNumber == trainNumber)
                {
                    matchingTrains.Add(train);
                }
            } 
            return matchingTrains;
        }
    
    }
}
*/