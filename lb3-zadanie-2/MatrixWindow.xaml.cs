﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace lb3_zadanie_2
{
    public partial class MatrixInputWindow : Window
    {
        public List<List<TextBox>> InputFields { get; private set; }
        public int Rows { get; }
        public int Columns { get; }

        public MatrixInputWindow(int rows, int columns)
        {
            InitializeComponent();
            Rows = rows;
            Columns = columns;
            InputFields = new List<List<TextBox>>();
            GenerateInputFields();
        }

        private void GenerateInputFields()
        {
            for (int i = 0; i < Rows; i++)
            {
                var rowPanel = new StackPanel { Orientation = Orientation.Horizontal };
                var rowList = new List<TextBox>();
                for (int j = 0; j < Columns; j++)
                {
                    TextBox inputBox = new TextBox { Width = 50, Margin = new Thickness(5) };
                    rowPanel.Children.Add(inputBox);
                    rowList.Add(inputBox);
                }
                InputFieldsPanel.Children.Add(rowPanel);
                InputFields.Add(rowList);
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
 