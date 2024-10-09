﻿using System;
using System.Windows;




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
using System.Windows.Navigation;
using System.Windows.Shapes;



using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab4_variant8
{
    public partial class MainWindow : Window
    {
        private MyDate myDate;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateDateDisplay()
        {
            CurrentDateTextBlock.Text = myDate.ToString();
        }

        private void SetDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dateParts = DateInputTextBox.Text.Split('.');
                if (dateParts.Length != 3)
                {
                    throw new ArgumentException("Invalid date format. Use DD.MM.YYYY.");
                }

                int day = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int year = int.Parse(dateParts[2]);

                myDate = new MyDate(year, month, day);
                UpdateDateDisplay();
                MessageBox.Show("Date set successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void AddDaysButton_Click(object sender, RoutedEventArgs e)
        {
            if (myDate == null)
            {
                MessageBox.Show("Please set the date first.");
                return;
            }

            myDate.AddDays(1); // добавляем 1 день
            UpdateDateDisplay();
        }

        private void AddMonthsButton_Click(object sender, RoutedEventArgs e)
        {
            if (myDate == null)
            {
                MessageBox.Show("Please set the date first.");
                return;
            }

            myDate.AddMonths(1); // добавляем 1 месяц
            UpdateDateDisplay();
        }

        private void AddYearsButton_Click(object sender, RoutedEventArgs e)
        {
            if (myDate == null)
            {
                MessageBox.Show("Please set the date first.");
                return;
            }

            myDate.AddYears(1); // добавляем 1 год
            UpdateDateDisplay();
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

namespace lab4_variant8
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