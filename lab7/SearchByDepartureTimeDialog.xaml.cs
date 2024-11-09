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
using System.Windows.Shapes;

namespace lab7
{
    /// <summary>
    /// Interaction logic for SearchByDepartureTimeDialog.xaml
    /// </summary>
    public partial class SearchByDepartureTimeDialog : Window
    {
        public SearchByDepartureTimeDialog()
        {
            InitializeComponent();
        }



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
