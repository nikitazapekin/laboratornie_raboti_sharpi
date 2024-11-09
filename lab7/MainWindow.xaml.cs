using System;
using System.Collections.Generic;

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
 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private TrainCollection trainCollection = new TrainCollection();


      

        private void DatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            DateTime newDate;
            DatePicker datePickerObj = sender as DatePicker;
            if (!DateTime.TryParse(e.Text, out newDate))
            {
                MessageBox.Show("Пожалуйста, введите корректный формат даты (dd.mm.yy)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

 
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trains.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var item in outputListBox.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
                MessageBox.Show("Файл успешно сохранён по пути: " + filePath, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ReadFromFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "trains.txt");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл trains.txt не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                outputListBox.Items.Clear(); 

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        outputListBox.Items.Add(line); 
                    }
                }

                MessageBox.Show("Данные успешно загружены из файла.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при чтении файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void AddTrain_Click(object sender, RoutedEventArgs e)
        {
            var addTrainDialog = new AddTrainDialog();
         
            bool result = addTrainDialog.ShowDialog() == true;
            if (result == true)
            {
             
                string destination = addTrainDialog.Destination;
                string trainNumber = addTrainDialog.TrainNumber;
                DateTime departureTime = addTrainDialog.DepartureTime;

              
                TRAIN newTrain = new TRAIN(destination, trainNumber, departureTime);
                trainCollection.Add(newTrain);

                MessageBox.Show("Поезд добавлен.");
            }
        }
      
        private void DisplayAllTrains_Click(object sender, RoutedEventArgs e)
        {
            outputListBox.Items.Clear();  
            var allTrains = trainCollection.GetAllTrains();

            if (allTrains.Count == 0)
            {
                MessageBox.Show("Нет поездов для отображения.");
            }

            foreach (var train in allTrains)
            {
                outputListBox.Items.Add(train.ToString());  
            }
        }
 

        private void WriteToFile_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void SortByDepartureTime_Click(object sender, RoutedEventArgs e)
        {
            trainCollection.SortByDepartureTime();
            MessageBox.Show("Поезда отсортированы по времени отправления.");
            UpdateListBox();
        }
        private void SortByTrainNumber_Click(object sender, RoutedEventArgs e)
        {
            trainCollection.SortByTrainNumber();
            MessageBox.Show("Поезда отсортированы по номеру поезда.");
            UpdateListBox();
        }
        private void SortByDestination_Click(object sender, RoutedEventArgs e)
        {
            trainCollection.SortByDestination();
            MessageBox.Show("Поезда отсортированы пунктам назначения.");
            UpdateListBox();
        }
        private void SearchByTrainNumber_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SearchByDepartureTime_Click(object sender, RoutedEventArgs e)
        {

        }


        private void SearchByDestination_Click(object sender, RoutedEventArgs e)
        {
          /*  string destination = "Ваше направление";
            var results = trainCollection.FindByDestination(destination);
            outputTextBox.Clear();
            foreach (var train in results)
            {
                outputTextBox.AppendText(train.ToString() + Environment.NewLine);
            }
            if (results.Count == 0)
            {
                MessageBox.Show("Поезда не найдены.");
            } */
        }

        private void UpdateListBox()
        {
            outputListBoxActions.Items.Clear();

            foreach (TRAIN train in trainCollection.GetAllTrains())
            {
                outputListBoxActions.Items.Add($"Поезд №{train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
      
       
    }
}
