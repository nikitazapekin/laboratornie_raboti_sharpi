using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
namespace lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private TrainCollection trainCollection = new TrainCollection();



        private void AddTrain_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            string destination = destinationTextBox.Text;
            string trainNumber = trainNumberTextBox.Text;
            DateTime departureTime;

            // Проверка корректности ввода времени
            if (string.IsNullOrWhiteSpace(destination) || string.IsNullOrWhiteSpace(trainNumber) ||
                !DateTime.TryParse(departureTimeTextBox.Text, out departureTime))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные для всех полей.");
                return;
            }

            // Создаем новый поезд и добавляем его в коллекцию
            TRAIN newTrain = new TRAIN(destination, trainNumber, departureTime);
            trainCollection.Add(newTrain);

            // Очищаем текстовые поля
            destinationTextBox.Clear();
            trainNumberTextBox.Clear();
            departureTimeTextBox.Clear();

            MessageBox.Show("Поезд добавлен.");
        }

        private void DisplayAllTrains_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Clear();
            var allTrains = trainCollection.GetAllTrains();
            foreach (var train in allTrains)
            {
                outputTextBox.AppendText(train.ToString() + Environment.NewLine);
            }
        }
        private void ReadFromFile_Click(object sender, RoutedEventArgs e)
        {
            // Логика чтения данных из файла
            // Например, использовать StreamReader для чтения и добавления в коллекцию
        }

        private void WriteToFile_Click(object sender, RoutedEventArgs e)
        {
            // Логика записи данных в файл
            // Например, использовать StreamWriter для записи из коллекции
        }

        private void SortByDepartureTime_Click(object sender, RoutedEventArgs e)
        {
            trainCollection.SortByDepartureTime();
            MessageBox.Show("Поезда отсортированы по времени отправления.");
        }

        private void SearchByDestination_Click(object sender, RoutedEventArgs e)
        {
            string destination = "Ваше направление"; // Здесь можно получить значение из TextBox
            var results = trainCollection.FindByDestination(destination);
            outputTextBox.Clear();
            foreach (var train in results)
            {
                outputTextBox.AppendText(train.ToString() + Environment.NewLine);
            }
            if (results.Count == 0)
            {
                MessageBox.Show("Поезда не найдены.");
            }
        }

      /*  private void DisplayAllTrains_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Clear();
            var allTrains = trainCollection.GetAllTrains();
            foreach (var train in allTrains)
            {
                outputTextBox.AppendText(train.ToString() + Environment.NewLine);
            }
        }
      */

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



    }
}
