using lab6;
using System;
using System.Windows;

namespace lab6
{
    public partial class MainWindow : Window
    {
        private StudentGroup group = new StudentGroup();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            DateTime dateOfBirth = DateOfBirthPicker.SelectedDate ?? DateTime.Now;

            group.AddStudent(new Student(firstName, lastName, dateOfBirth));
            UpdateResult();
        }

        private void RemoveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = SearchTextBox.Text;
            Student student = group.FindStudentByLastName(lastName);
            if (student != null)
            {
                group.RemoveStudent(student);
            }
            UpdateResult();
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            group.SortByLastName();
            UpdateResult();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = SearchTextBox.Text;
            Student student = group.FindStudentByLastName(lastName);
            if (student != null)
            {
                ResultTextBlock.Text = $"Найден студент: {student}";
            }
            else
            {
                ResultTextBlock.Text = "Студент не найден.";
            }
        }

        private void UpdateResult()
        {
            ResultTextBlock.Text = group.ToString();
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

namespace lab6
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