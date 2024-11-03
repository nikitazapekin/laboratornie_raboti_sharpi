 
  using System;
 

using System.Linq; 
using System.Windows; // For WPF functionalities
using System.Windows.Controls; // For WPF controls

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

            ResultListFindBox.Items.Clear();
            if (foundStudents.Length > 0)
            {
                foreach (var student in foundStudents)
                {
                    ResultListFindBox.Items.Add($"{student.FirstName} {student.LastName} (Дата рождения: {student.DateOfBirth.ToShortDateString()})");
                }
            }
            else
            {
                ResultListFindBox.Items.Add("Студенты не найдены.");
            }
        }

        void Compare_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudents = ResultListBox.SelectedItems.OfType<string>().ToList();

            if (selectedStudents.Count > 0)
            {
              
                if (selectedStudents.Count >= 2)
                {
                    // Создаем сообщения о выбранных студентах
                    string message = string.Join(", ", selectedStudents);
                    MessageBox.Show("Выбранные студенты: " + message, "Выбранные студенты", MessageBoxButton.OK, MessageBoxImage.Information);
 
                }
                else
                {
                    MessageBox.Show("Выберите как минимум двух студентов для сравнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Не выбрано ни одного студента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            ResultListBox.Items.Clear();
         
          for (int i=0; i<group.Count; i++)
            {
              
                ResultListBox.Items.Add($"{group[i].FirstName} {group[i].LastName} (Дата рождения: {group[i].DateOfBirth.ToShortDateString()})");

            }
        }

    }
}
 