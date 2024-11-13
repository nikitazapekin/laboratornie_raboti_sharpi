﻿using System;
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
                    foreach (var item in trainCollection.GetAllTrains())
                    {
                     
                        string line = $"{item.TrainNumber}|{item.Destination}|{item.DepartureTime:dd.MM.yyyy HH:mm}";
                        writer.WriteLine(line);
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
                trainCollection = new TrainCollection();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        int trainNumber;
                        if (parts.Length == 3)
                        {

                           int.TryParse(parts[0].Trim(), out trainNumber);
                  
                            string destination = parts[1].Trim();
                            DateTime departureTime;

                            if (DateTime.TryParseExact(parts[2].Trim(), "dd.MM.yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out departureTime))
                            {
                                TRAIN train = new TRAIN(destination, trainNumber, departureTime);
                                trainCollection.Add(train);
                                outputListBox.Items.Add(train.ToString());
                            }
                            else
                            {
                                MessageBox.Show($"Ошибка в формате времени для поезда {trainNumber}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Неверный формат данных в строке: {line}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
 


                if (int.TryParse(addTrainDialog.TrainNumber.Trim(), out int trainNumber))
                {
                    DateTime departureTime = addTrainDialog.DepartureTime;

                    TRAIN newTrain = new TRAIN(destination, trainNumber, departureTime);
                    trainCollection.Add(newTrain);

                    MessageBox.Show("Поезд добавлен.");
                }
                else
                {
                   
                    MessageBox.Show("Ошибка: номер поезда должен быть целым числом.");
                }


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
        private void SearchByDepartureTime_Click(object sender, RoutedEventArgs e)
        {
           
            var searchDialog = new SearchByDepartureTimeDialog(trainCollection);

            bool result = searchDialog.ShowDialog() == true;


            if (result)
            {
                List<TRAIN> foundTrains = searchDialog.MatchingTrains;

                if (foundTrains.Count > 0)
                {
                    outputListBoxActions.Items.Clear();
                    foreach (var train in foundTrains)
                    {
                        outputListBoxActions.Items.Add($"Поезд №{train.TrainNumber} - {train.Destination} - {train.DepartureTime}");

                    }
                }
                else
                {
                    MessageBox.Show("Поезда не найдены.");
                }


            }
        }

      
        private void SearchByTrainNumber_Click(object sender, RoutedEventArgs e)
        {
          
            var searchDialog = new SearchByTrainId(trainCollection);

            bool result = searchDialog.ShowDialog() == true;


            if (result)
            {

                List<TRAIN> foundTrains = searchDialog.MatchingTrains;

                if (foundTrains.Count > 0)
                {
                    outputListBoxActions.Items.Clear();
                    foreach (var train in foundTrains)
                    {
                        outputListBoxActions.Items.Add($"Поезд №{train.TrainNumber} - {train.Destination} - {train.DepartureTime}");

                    }
                }
                else
                {
                    MessageBox.Show("Поезда не найдены.");
                }



            }
        }

        private void SearchByDestination_Click(object sender, RoutedEventArgs e)
        {
          
            var searchDialog = new SearchByDestination(trainCollection);

            bool result = searchDialog.ShowDialog() == true;

            if (result)
            {
             
                List<TRAIN> foundTrains = searchDialog.MatchingTrains;

                if (foundTrains.Count > 0)
                {
                    outputListBoxActions.Items.Clear();
                    foreach (var train in foundTrains)
                    {
                        outputListBoxActions.Items.Add($"Поезд №{train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
                  
                    }
                }
                else
                {
                    MessageBox.Show("Поезда не найдены.");
                }
            }
        }



        private void UpdateListBox()
        {
            outputListBoxActions.Items.Clear();

            foreach (TRAIN train in trainCollection.GetAllTrains())
            {
                outputListBoxActions.Items.Add($"Поезд №{train.TrainNumber} - {train.Destination} - {train.DepartureTime}");
            }
        }

 


        private void HandleDelete_Click(object sender, RoutedEventArgs e)
        { 
            var selectedTrains = outputListBox.SelectedItems.OfType<string>().ToList();

            foreach (var selectedTrain in selectedTrains)
            {
             
                var match = System.Text.RegularExpressions.Regex.Match(selectedTrain, @"\d+");

                if (match.Success)
                {
                  
                    int trainNumber = int.Parse(match.Value);
                    var trainToRemove = trainCollection.GetAllTrains().FirstOrDefault(train => train.TrainNumber == trainNumber);  
                    trainCollection.Remove(trainToRemove);
                    outputListBox.Items.Remove(selectedTrain);
 
                    outputListBox.Items.Refresh();  

                    MessageBox.Show($"Поезд с номером {trainNumber} удален.");
                }
                else
                {
                    MessageBox.Show("Не удалось извлечь номер поезда.");
                }
            }
        }





        private void AddSixTrains_Click(object sender, RoutedEventArgs e)
        {
          
            var trainsToAdd = new List<TRAIN>
    {
        new TRAIN("Москва", 101, new DateTime(2024, 11, 13, 15, 30, 0)),
        new TRAIN("Санкт-Петербург", 102, new DateTime(2024, 11, 13, 16, 00, 0)),
        new TRAIN("Владивосток", 103, new DateTime(2024, 11, 13, 17, 30, 0)),
        new TRAIN("Калуга", 104, new DateTime(2024, 11, 13, 18, 00, 0)),
        new TRAIN("Нижний Новгород", 105, new DateTime(2024, 11, 13, 19, 15, 0)),
        new TRAIN("Ярославль", 106, new DateTime(2024, 11, 13, 20, 45, 0))
    };

          
            foreach (var train in trainsToAdd)
            {
                trainCollection.Add(train);
            }

            MessageBox.Show("Шесть поездов успешно добавлены в коллекцию.");
        }





    }
}
