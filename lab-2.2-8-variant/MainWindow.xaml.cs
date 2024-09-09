using System;
using System.Windows;
using System.Windows.Controls;

namespace lab_2._2_8_variant
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

        int k;   
        double x, y, r;


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {



            if (string.IsNullOrWhiteSpace(X.Text) || string.IsNullOrWhiteSpace(Y.Text) || string.IsNullOrWhiteSpace(R.Text))
            {
                MessageBox.Show("Для выстрела нужно заполнить все поля");
                return;
            }

            try
            {


                double r = Convert.ToDouble(R.Text);
                double x = Convert.ToDouble(X.Text);
                double y = Convert.ToDouble(Y.Text);


                //   bool inSemiCircle = (x * x + y * y <= r * r) && y >= 0;   
                //  bool inLeftRectangle = (x >= -r && x <= 0) && (y >= 0 && y <= r);   
                //  bool inRightRectangle = (x >= 0 && x <= r) && (y <= 0 && y >= -r);   
                bool isRightTopCircle = (x > 0 && y > 0 && (x * x + y * y < r * r));
                bool isRightBottomCircle = (x > 0 && y < 0 && (x * x + y * y < r * r));
                bool isLeftTopRect = (x >= (-1) * r && y>=(-1)*x);
                bool isLeftBottomRect = (x >= (-1) * r && y <= x);
                //   bool isTopLeftReactangle = (x < 0 && y > 0 );
             //   Console.WriteLine(isLeftBottomRect, isLeftTopRect, isRightBottomCircle, isRightTopCircle);
                if (isRightTopCircle || isRightBottomCircle || isLeftTopRect || isLeftBottomRect)
                {
              
                        MessageBox.Show("Попал");
                        k++;
                    
                }
                    else
                    {
                        MessageBox.Show("Не попал");
                    }
                /*      if (inSemiCircle || inLeftRectangle || inRightRectangle)
                      {
                          MessageBox.Show("Попал");
                          k++;  
                      }
                      else
                      {
                          MessageBox.Show("Не попал");
                      }
                */
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