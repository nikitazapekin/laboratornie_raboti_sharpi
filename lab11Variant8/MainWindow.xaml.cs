using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace lab11Variant8
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private string quotesUsingString;
        private string quotesUsingStringBuilder;
        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

       
                StartBackgroundTask();

                string text = await File.ReadAllTextAsync(filePath);

           

                await Task.Run(() =>
                {
                    quotesUsingString = ExtractQuotesUsingString(text);
                    quotesUsingStringBuilder = ExtractQuotesUsingStringBuilder(text);
                });



                txtQuotes.Text = "Цитаты (используя методы String):\n" + quotesUsingString +
                                 "\nЦитаты (используя StringBuilder):\n" + quotesUsingStringBuilder;

            }
            else
            {
             _cancellationTokenSource.Cancel(); 
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

                    result += text.Substring(i + 1, end - i - 1)+" "; 
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
                if (end == -1) {
                    break;
                        };

                result.AppendLine(text.Substring(start + 1, end - start - 1));
                start = end + 1;
            }

            return result.ToString();
        }

        private async void StartBackgroundTask()
        {
            await Task.Run(() => GeneratePrimeNumbers(_cancellationTokenSource.Token));
        }

        private void GeneratePrimeNumbers(CancellationToken token)
        {
            for (int i = 2; i <= 5000; i++)
            {
                if (token.IsCancellationRequested)
                    break;

                if (IsPrime(i))
                {
                    Dispatcher.Invoke(() =>
                   {
                        task.Text += $"Простое число: {i}\n";
                    });
                    Thread.Sleep(400);  
                }
            }
        }

        private bool IsPrime(int number)
        {
            if (number < 2) {
                return false;
                    }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {

                    return false;
                }
            }

            return true;
        }

  
        private async void cancel_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource.Cancel();
        }


    }
}
