using lab6;
using System;
using System.Windows;
using System.Windows.Controls;

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
            if(firstName.Length>0 && lastName.Length>0)
            {

            DateTime dateOfBirth = DateOfBirthPicker.SelectedDate ?? DateTime.Now;

            group.AddStudent(new Student(firstName, lastName, dateOfBirth));
            UpdateResult();
            } else
            {
                MessageBox.Show("Пожалуйста, введите имя и фамилию студента!");

            }
        }

        private void RemoveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = SearchTextBoxRemove.Text;
            Student student = group.FindStudentByLastName(lastName);
            if (student != null)
            {
                group.RemoveStudent(student);
            } else
            {
                MessageBox.Show("Студент для удаления не найден!");
            }
            UpdateResult();
        }
        /*
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            //  if(group.Lenb)
            if (group.Count > 0)
            {

            group.SortByLastName();
            UpdateResult();
            } else
            {
                MessageBox.Show("Недостаточно студентов для сортировки по фамилии!");
            }
        }
        */


        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (group.Count > 0)
            {
                string selectedCriteria = (SortCriteriaComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                switch (selectedCriteria)
                {
                    case "Сортировать по имени":
                        group.SortByFirstName();
                        break;
                    case "Сортировать по фамилии":
                        group.SortByLastName();
                        break;
                    case "Сортировать по дате рождения":
                        group.SortByDateOfBirth();
                        break;
                    default:
                        MessageBox.Show("Пожалуйста, выберите критерий сортировки.");
                        return;
                }

                UpdateResult();
            }
            else
            {
                MessageBox.Show("Недостаточно студентов для сортировки!");
            }
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
            ResultTextBlockAll.Text = "Студенты: \n" + group.ToString();
        }
    }
}
 