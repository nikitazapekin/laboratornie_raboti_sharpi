
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace lab_2._2_8_variant
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int countOfStrikes;
        int countOfAttempts;
        double x, y, r;

        private void DisplayOfResults()
        {
            MessageBox.Show($"Количество попыток: {countOfAttempts}\nКоличество попаданий: {countOfStrikes}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(X.Text) || string.IsNullOrWhiteSpace(Y.Text) || string.IsNullOrWhiteSpace(R.Text))
            {
                MessageBox.Show("Для выстрела нужно заполнить все поля");
                return;
            }

            try
            {
                if (countOfAttempts < 10)
                {
                    countOfAttempts++;

                
                    if (!double.TryParse(R.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out r) ||
                        !double.TryParse(X.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out x) ||
                        !double.TryParse(Y.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out y))
                    {
                        MessageBox.Show("Пожалуйста, введите численные значения");
                        return;
                    }

                    bool isRightTopCircle = (x > 0 && y > 0 && (x * x + y * y < r * r));
                    bool isRightBottomCircle = (x > 0 && y < 0 && (x * x + y * y < r * r));
                    bool isLeftTopRect = (x <= 0 && y <= r && x >= (-1) * r && y >= (-1) * x);
                    bool isLeftBottomRect = (x <= 0 && y >= (-1) * r && x >= (-1) * r && y <= x);

                    if (isRightTopCircle || isRightBottomCircle || isLeftTopRect || isLeftBottomRect)
                    {
                        MessageBox.Show("Попал");
                        countOfStrikes++;
                    }
                    else
                    {
                        MessageBox.Show("Не попал");
                    }

                    if (countOfAttempts == 10)
                    {
                        DisplayOfResults();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Попробуйте снова.");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}

/*
 * using System;
using System.Windows;
using System.Windows.Controls;

namespace lab_2._2_8_variant
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int countOfStrikes;
        int countOfAttempts;
        double x, y, r;

        private void DisplayOfResults()
        {
            MessageBox.Show($"Количество попыток: {countOfAttempts}\nКоличество попаданий: {countOfStrikes}");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(X.Text) || string.IsNullOrWhiteSpace(Y.Text) || string.IsNullOrWhiteSpace(R.Text))
            {
                MessageBox.Show("Для выстрела нужно заполнить все поля");
                return;
            }

            try
            {
                if (countOfAttempts < 10)
                {
                    countOfAttempts++;

                    r = Convert.ToDouble(R.Text);
                    x = Convert.ToDouble(X.Text);
                    y = Convert.ToDouble(Y.Text);

                    bool isRightTopCircle = (x > 0 && y > 0 && (x * x + y * y < r * r));
                    bool isRightBottomCircle = (x > 0 && y < 0 && (x * x + y * y < r * r));
                    bool isLeftTopRect = (x <= 0 && y <= r && x >= (-1) * r && y >= (-1) * x);
                    bool isLeftBottomRect = (x <= 0 && y >= (-1) * r && x >= (-1) * r && y <= x);

                    if (isRightTopCircle || isRightBottomCircle || isLeftTopRect || isLeftBottomRect)
                    {
                        MessageBox.Show("Попал");
                        countOfStrikes++;
                    }
                    else
                    {
                        MessageBox.Show("Не попал");
                    }

                    if (countOfAttempts == 10)
                    {
                        DisplayOfResults();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, введите численные значения");
            }
        }

    

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
*/
