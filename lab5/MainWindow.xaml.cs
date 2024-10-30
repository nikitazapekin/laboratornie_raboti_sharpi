using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace lab5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        { 
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Считываем текст из выбранного файла
                string text = File.ReadAllText(openFileDialog.FileName);

                // Извлекаем цитаты
                string quotesUsingString = ExtractQuotesUsingString(text);
                string quotesUsingStringBuilder = ExtractQuotesUsingStringBuilder(text);

                // Выводим цитаты
                txtQuotes.Text = "Цитаты (используя методы String):\n" + quotesUsingString + "\n\n" +
                                 "Цитаты (используя StringBuilder):\n" + quotesUsingStringBuilder;
            }
        }

        private string ExtractQuotesUsingString(string text)
        {
            // Используем методы класса String для извлечения цитат
            StringBuilder result = new StringBuilder();
            int start = 0;

            while ((start = text.IndexOf('"', start)) != -1)
            {
                int end = text.IndexOf('"', start + 1);
                if (end == -1) break; // Если нет закрывающей кавычки

                result.AppendLine(text.Substring(start + 1, end - start - 1));
                start = end + 1;
            }

            return result.ToString();
        }

        private string ExtractQuotesUsingStringBuilder(string text)
        {
            // Используем StringBuilder для извлечения цитат
            StringBuilder result = new StringBuilder();
            int start = 0;

            while ((start = text.IndexOf('"', start)) != -1)
            {
                int end = text.IndexOf('"', start + 1);
                if (end == -1) break; // Если нет закрывающей кавычки

                result.AppendLine(text.Substring(start + 1, end - start - 1));
                start = end + 1;
            }

            return result.ToString();
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab5
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
    }
}
*/