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
       //     _trains = trains;
        }
 
        public List<TRAIN> Destination { get; private set; } = new List<TRAIN>();
        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string destination = DestinationTextBox.Text;
 
        
            if (string.IsNullOrWhiteSpace(destination))
            {
                MessageBox.Show("Введите пункт назначения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

       
            Destination = FindByDestination(destination);

            if (Destination.Count == 0)
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
 