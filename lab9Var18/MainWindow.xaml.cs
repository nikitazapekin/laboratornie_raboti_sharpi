﻿using System;
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

namespace lab9Var18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Combinational comb;
       
        public MainWindow()
        {
            InitializeComponent();
           comb  = new Combinational(8);
        }

        private void ComputeComb_Click(object sender, RoutedEventArgs e)
        {


            try
            {

                int[] inputs = new int[8];
                inputs[0] = ParseInput(Input1.Text, "Вход 1");
                inputs[1] = ParseInput(Input2.Text, "Вход 2");
                inputs[2] = ParseInput(Input3.Text, "Вход 3");
                inputs[3] = ParseInput(Input4.Text, "Вход 4");
                inputs[4] = ParseInput(Input1.Text, "Вход 5");
                inputs[5] = ParseInput(Input2.Text, "Вход 6");
                inputs[6] = ParseInput(Input3.Text, "Вход 7");
                inputs[7] = ParseInput(Input4.Text, "Вход 8");

                comb.SetInputs(inputs);
                comb.SetInputs(inputs);
                int result = comb.ComputeOutput();
                ResultComb.Text = $"Выход: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void InvertComb_Click(object sender, RoutedEventArgs e)
        {
            comb.Invert();

            int[] inputs = new int[8];
            inputs = comb.GetInputs();
            Input1.Text = inputs[0].ToString();
            Input2.Text = inputs[1].ToString();
            Input3.Text = inputs[2].ToString();
            Input4.Text = inputs[3].ToString();
            Input5.Text = inputs[4].ToString();
            Input6.Text = inputs[5].ToString();
            Input7.Text = inputs[6].ToString();
            Input8.Text = inputs[7].ToString();
        }





        private int ParseInput(string input, string fieldName)
        {
            if (!int.TryParse(input, out int value) || (value != 0 && value != 1))
            {
                throw new ArgumentException($"Поле \"{fieldName}\" должно содержать только 0 или 1.");
            }
            return value;
        }



    }
}
