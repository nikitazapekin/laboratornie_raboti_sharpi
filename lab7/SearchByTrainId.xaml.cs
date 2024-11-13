/*
 * using System;
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
using System.Windows.Shapes;

namespace lab7
{
 
    public partial class SearchByTrainId : Window
    {
        public SearchByTrainId()
        {
            InitializeComponent();
        }
    }
}
*/



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
