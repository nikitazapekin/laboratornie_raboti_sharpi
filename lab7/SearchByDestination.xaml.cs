using System;
using System.Collections.Generic;
using System.Windows;

namespace lab7
{
    public partial class SearchByDestination : Window
    {
        private List<TRAIN> _trains;

 
        public SearchByDestination(List<TRAIN> trains)
        {
            InitializeComponent();
            _trains = trains;
        }
 
        public List<TRAIN> Destination { get; private set; } = new List<TRAIN>();
        /*
               private void SearchButton_Click(object sender, RoutedEventArgs e)
               {
                   string destination = DestinationTextBox.Text;


                   MessageBox.Show($" поездa:  ");
                   foreach (var train in _trains)
                   {

                       MessageBox.Show($"  поездa: {train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
                   }



                   if (string.IsNullOrWhiteSpace(destination))
                   {
                       MessageBox.Show("Введите пункт назначения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                       return;
                   }


                   Destination = FindByDestination(destination);


                   if(Destination.Count== 0)
                   {
                       MessageBox.Show("Ничего не найдено!");
                 foreach (var train in Destination)
                   {

                       MessageBox.Show($"Найден поезд: {train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
                   } 
                   }
                   Close();
               }

          */
        /*

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string destination = DestinationTextBox.Text;

            // Выводим все поезда для проверки
            MessageBox.Show("Поезда:");
            foreach (var train in _trains)
            {
                MessageBox.Show($"Поезд: {train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                MessageBox.Show("Введите пункт назначения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Получаем поезда по указанному пункту назначения
            Destination = FindByDestination(destination);

            if (Destination.Count == 0)
            {
                MessageBox.Show("Ничего не найдено!");
            }
            else
            {
                // Выводим найденные поезда
                foreach (var train in Destination)
                {
                    MessageBox.Show($"Найден поезд: {train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
                }
            }

            Close(); // Закрытие окна после выполнения
        }
        */


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string destination = DestinationTextBox.Text;

            // Выводим все поезда для проверки
            MessageBox.Show("Поезда:");
            foreach (var train in _trains)
            {
                MessageBox.Show($"Поезд: {train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                MessageBox.Show("Введите пункт назначения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Получаем поезда по указанному пункту назначения
            Destination = FindByDestination(destination);

            if (Destination.Count == 0)
            {
                MessageBox.Show("Ничего не найдено!");
            }
            else
            {
                // Выводим найденные поезда
                foreach (var train in Destination)
                {
                    MessageBox.Show($"Найден поезд: {train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
                }
            }

            // Устанавливаем результат поиска как успешный
            this.DialogResult = true; // Устанавливаем возвращаемое значение как true
            Close(); // Закрытие окна после выполнения
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    
        private List<TRAIN> FindByDestination(string destination)
        {
            List<TRAIN> matchingTrains = new List<TRAIN>();
            foreach (TRAIN train in _trains)
            {
                if (string.Equals(train.Destination, destination, StringComparison.OrdinalIgnoreCase))
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
    /// <summary>
    /// Interaction logic for SearchByDestination.xaml
    /// </summary>
    public partial class SearchByDestination : Window
    {
        public SearchByDestination()
        {
            InitializeComponent();
        }
        public string Destination { get; private set; }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string destination = DestinationTextBox.Text;

            if (string.IsNullOrWhiteSpace(destination))
            {
                MessageBox.Show("Введите пункт назначения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Destination = DestinationTextBox.Text;




 



                // MessageBox.Show($"Поиск по пункту назначения {destination}...", "Результаты поиска", MessageBoxButton.OK, MessageBoxImage.Information);
                Close(); 
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
          Close(); 
        }


    }
}
*/