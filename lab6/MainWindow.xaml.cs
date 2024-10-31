 
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

        private void DatePicker_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
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

            DateTime dateOfBirth = DateOfBirthPicker.SelectedDate.Value;
            group.AddStudent(new Student(firstName, lastName, dateOfBirth));
            UpdateResult();
        }

        private void AddStudentsButton_Click(object sender, RoutedEventArgs e)
        {
            var newStudents = new[]
            {
                new Student("Alice", "Smith", new DateTime(2000, 1, 15)),
                new Student("Bob", "Johnson", new DateTime(2001, 2, 20)),
                new Student("Charlie", "Williams", new DateTime(2002, 3, 25)),
                new Student("Diana", "Jones", new DateTime(2003, 4, 30)),
            new Student("Alex", "Bern", new DateTime(2002, 3, 25)),
                new Student("Lia", "Charlz", new DateTime(2003, 4, 30))
            };

            group.AddStudents(newStudents);
            UpdateResult();
        }

        private void RemoveStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = SearchTextBoxRemove.Text;
            var studentsToRemove = group.FindStudentsByLastNameFull(lastName);

            if (studentsToRemove.Length > 0)
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
            Student[] foundStudents;

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

            if (foundStudents.Length > 0)
            {
                string str = "";
                foreach (var student in foundStudents)
                {
                    str += $"{student.FirstName} {student.LastName} (Дата рождения: {student.DateOfBirth.ToShortDateString()})\n";
                }
                ResultTextBlock.Text = "Найденные студенты:\n" + str;
            }
            else
            {
                ResultTextBlock.Text = "Студенты не найдены.";
            }
        }
        void Compare_Click(object sender, RoutedEventArgs e)
        {
            Student student1 = new Student("Иван", "Иванов", new DateTime(2000, 1, 1));
            Student student2 = new Student("Петр", "Иванов", new DateTime(1999, 5, 5));
            MessageBox.Show($"Студенты:\n1. {student1.FirstName} {student1.LastName},  {student2.FirstName} {student2.LastName}");
            if (student1 == student2)
            {
                MessageBox.Show($"Студенты {student1.LastName} и {student2.LastName}  равны по фамилии.");
            }
            else
            {
                MessageBox.Show($"Студенты {student1.LastName} и {student2.LastName} не равны по фамилии.");
            }

            if (student1 > student2)
            {
                MessageBox.Show($"Студент {student1.LastName}  старше студента  {student2.LastName} ");
            }
            else
            {
                MessageBox.Show($"Студент {student1.LastName} младше студента  {student2.LastName}.");
            }
        
           

        }

        void Indecsator_Click(object sender, RoutedEventArgs e)
        {

            
   
            for (int i = 0; i < group.Count; i++)
            {
                Student student = group[i];
           
                MessageBox.Show(student.FirstName);
            }


        }

        private void UpdateResult()
        {
            ResultTextBlockAll.Text = "Студенты: \n" + group.ToString();
        }
    }
}
 