using System;
using System.Collections.Generic;
using System.Windows;

namespace lab7
{
    public partial class SearchByDepartureTimeDialog : Window
    {
        private List<TRAIN> _trains;

        public SearchByDepartureTimeDialog(List<TRAIN> trains)
        {
            InitializeComponent();
         //   _trains = trains;
        }

        public List<TRAIN> MatchingTrains { get; private set; } = new List<TRAIN>();

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartureDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату отправления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime selectedDate = DepartureDatePicker.SelectedDate.Value;
            MatchingTrains = FindByDepartureDate(selectedDate);

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

        private List<TRAIN> FindByDepartureDate(DateTime date)
        {
            List<TRAIN> matchingTrains = new List<TRAIN>();
            foreach (TRAIN train in _trains)
            {
                if (train.DepartureTime.Date == date.Date)
                {
                    matchingTrains.Add(train);
                }
            }
            return matchingTrains;
        }
    }
}


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
 
    public partial class SearchByDepartureTimeDialog : Window
    {

        private List<TRAIN> _trains;
        public SearchByDepartureTimeDialog(List<TRAIN> trains)
        {
            InitializeComponent();
            _trains = trains;
        }
        public List<TRAIN> DepartureTime { get; private set; } = new List<TRAIN>();


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            DateTime? departureDate = DepartureDatePicker.SelectedDate;

            if (!departureDate.HasValue)
            {
                MessageBox.Show("Выберите время отправления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

        
            MessageBox.Show($"Поиск по времени отправления {departureDate.Value}...", "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
         Close();  
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
         Close();  
        }
    }
}
*/