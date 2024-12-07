using System.Windows;
using System.Windows.Media;

namespace lab10Variant8
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Заполнение значений для выбора шрифта
            FontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies;
            FontWeightComboBox.ItemsSource = new[] { FontWeights.Normal, FontWeights.Bold, FontWeights.Bold };
        }

        private void EditGraphMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Visible;
            TextEditor.Visibility = Visibility.Collapsed;
        }

        private void EditTextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditPanel.Visibility = Visibility.Visible;
            GraphEditor.Visibility = Visibility.Collapsed;
            TextEditor.Visibility = Visibility.Visible;
        }
    }
}
