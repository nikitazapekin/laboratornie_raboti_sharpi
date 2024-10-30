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
               
                string text = File.ReadAllText(openFileDialog.FileName);

              
                string quotesUsingString = ExtractQuotesUsingString(text);
                string quotesUsingStringBuilder = ExtractQuotesUsingStringBuilder(text);

               
                txtQuotes.Text = "Цитаты (используя методы String):\n" + quotesUsingString + "\n\n" +
                                 "Цитаты (используя StringBuilder):\n" + quotesUsingStringBuilder;
            } else
            {
                MessageBox.Show("Ошибка чтения файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ExtractQuotesUsingString(string text)
        {
            string result = ""; 

            for (int i = 0; i < text.Length; i++)
            {
              
                if (text[i] == '"')
                {
              
                    int end = text.IndexOf('"', i + 1);

               
                    if (end == -1)
                    {
                        break;
                    }

            
                    result += text.Substring(i + 1, end - i - 1) + Environment.NewLine; 
 
                    i = end;
                }
            }

            return result;  
        }

        private string ExtractQuotesUsingStringBuilder(string text)
        {
          
            StringBuilder result = new StringBuilder();
            int start = 0;

            while ((start = text.IndexOf('"', start)) != -1)
            {
                int end = text.IndexOf('"', start + 1);
                if (end == -1) break;  

                result.AppendLine(text.Substring(start + 1, end - start - 1));
                start = end + 1;
            }

            return result.ToString();
        }
    }
}
