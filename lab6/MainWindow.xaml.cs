
using lab6;
using System;
using System.Collections.Generic;
using System.Linq;
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


        private void DatePicker_DateValidationError(object sender,
                DatePickerDateValidationErrorEventArgs e)
        {
            DateTime newDate;
            DatePicker datePickerObj = sender as DatePicker;
            if (!DateTime.TryParse(e.Text, out newDate))
            {
                MessageBox.Show("Пожалуйста, введите корректный формат даты (dd.mm.yy)!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        
        }
        
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string dateFormat = "dd.MM.yy";

            if (string.IsNullOrWhiteSpace(firstName))
            {
                MessageBox.Show("Пожалуйста, введите имя студента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Пожалуйста, введите фамилию студента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!DateOfBirthPicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите дату рождения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime dateOfBirth;
            if (!DateTime.TryParseExact(DateOfBirthPicker.SelectedDate.Value.ToString(dateFormat), dateFormat, null, System.Globalization.DateTimeStyles.None, out dateOfBirth))
            {
                MessageBox.Show($"Некорректный формат даты. Ожидаемый формат: {dateFormat}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            group.AddStudent(new Student(firstName, lastName, dateOfBirth));
            UpdateResult();
        }
    



        private void RemoveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = SearchTextBoxRemove.Text;
            var studentsToRemove = group.FindStudentsByLastNameFull(lastName);

            if (studentsToRemove.Count > 0)
            {
                foreach (var student in studentsToRemove)
                {
                    group.RemoveStudent(student);
                }
            }
            else
            {
                MessageBox.Show("Студент для удаления не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UpdateResult();
        }

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
                        MessageBox.Show("Пожалуйста, выберите критерий сортировки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }

                UpdateResult();
            }
            else
            {
                MessageBox.Show("Недостаточно студентов для сортировки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            string selectedCriteria = (SortCriteriaComboBox_Copy.SelectedItem as ComboBoxItem)?.Content.ToString();
            List<Student> foundStudents;

            if (selectedCriteria == "Поиск по имени")
            {
                foundStudents = group.FindStudentsByFirstName(searchText);
            }
            else if (selectedCriteria == "Поиск по фамилии")
            {
                foundStudents = group.FindStudentsByLastName(searchText);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите критерий поиска.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (foundStudents.Count > 0)
            {
                ResultTextBlock.Text = "Найденные студенты:\n" + string.Join("\n", foundStudents.Select(s => s.ToString()));
            }
            else
            {
                ResultTextBlock.Text = "Студенты не найдены.";
            }
        }

        private void UpdateResult()
        {
            ResultTextBlockAll.Text = "Студенты: \n" + group.ToString();
        }
    }
}


 