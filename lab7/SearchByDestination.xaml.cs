using System;
using System.Collections.Generic;
using System.Windows;

namespace lab7
{
    public partial class SearchByDestination : Window
    {

        private TrainCollection _trainCollection;

        public SearchByDestination(TrainCollection trainCollection)
        {
            InitializeComponent();
            _trainCollection = trainCollection;
        }

        public List<TRAIN> MatchingTrains { get;  set; } = new List<TRAIN>();
 
        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string destination = DestinationTextBox.Text;
 
        
            if (string.IsNullOrWhiteSpace(destination))
            {
                MessageBox.Show("Введите пункт назначения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MatchingTrains = _trainCollection.FindByDestination(destination);
          

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
 