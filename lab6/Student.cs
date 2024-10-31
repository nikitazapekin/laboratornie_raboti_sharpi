using System;

namespace lab6
{
    public class Student : IComparable<Student>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
        public Student()
        {
            FirstName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
        }

        public int CompareTo(Student other)
        {
            return CompareStrings(LastName, other.LastName);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Дата рождения: {DateOfBirth.ToShortDateString()}";
        }

        public static int CompareStrings(string str1, string str2)
        {
            int length = Math.Min(str1.Length, str2.Length);
            for (int i = 0; i < length; i++)
            {
                if (str1[i] < str2[i]) return -1;
                if (str1[i] > str2[i]) return 1;
            }
            if (str1.Length < str2.Length) return -1;
            if (str1.Length > str2.Length) return 1;
            return 0;
        }


        public bool Equals(Student other)
        {
            if (other is null) return false;
            return string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Student);
        }

      

        public static bool operator ==(Student left, Student right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Student left, Student right)
        {
            return !(left == right);
        }

        public static bool operator >(Student left, Student right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(Student left, Student right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >=(Student left, Student right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <=(Student left, Student right)
        {
            return left.CompareTo(right) <= 0;
        }
    }

    public class StudentGroup
    {
        private Student[] students = new Student[0];

        public void AddStudent(Student student)
        {
        
            Student[] newStudents = new Student[students.Length + 1];

            
            for (int i = 0; i < students.Length; i++)
            {
                newStudents[i] = students[i];
            }

         
            newStudents[newStudents.Length - 1] = student;

          
            students = newStudents;
        }

        public void AddStudents(Student[] newStudents)
        {
          
            Student[] combinedStudents = new Student[students.Length + newStudents.Length];
 
            for (int i = 0; i < students.Length; i++)
            {
                combinedStudents[i] = students[i];
            }

          
            for (int i = 0; i < newStudents.Length; i++)
            {
                combinedStudents[students.Length + i] = newStudents[i];
            }

     
            students = combinedStudents;
        }

        public Student[] FindStudentsByLastName(string lastName)
        {
       
            int count = 0;
            foreach (var student in students)
            {
                if (student.LastName.StartsWith(lastName, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
             
            Student[] results = new Student[count];
            int index = 0;

           
            foreach (var student in students)
            {
                if (student.LastName.StartsWith(lastName, StringComparison.OrdinalIgnoreCase))
                {
                    results[index++] = student;
                }
            }

            return results;
        }

        public Student[] FindStudentsByLastNameFull(string lastName)
        { 
            int count = 0;
            foreach (var student in students)
            {
                if (string.Equals(student.LastName, lastName, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }

           
            Student[] results = new Student[count];
            int index = 0;
 
            foreach (var student in students)
            {
                if (string.Equals(student.LastName, lastName, StringComparison.OrdinalIgnoreCase))
                {
                    results[index++] = student;
                }
            }

            return results;
        }

        public void RemoveStudent(Student student)
        {
           
            int count = 0;
            foreach (var s in students)
            {
                if (s != student)
                {
                    count++;
                }
            }

          
            Student[] newStudents = new Student[count];
            int index = 0;

           
            foreach (var s in students)
            {
                if (s != student)
                {
                    newStudents[index++] = s;
                }
            }

          
            students = newStudents;
        }

        public Student[] FindStudentsByFirstName(string firstNamePart)
        {
           
            int count = 0;
            foreach (var student in students)
            {
                if (student.FirstName.StartsWith(firstNamePart, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }

          
            Student[] results = new Student[count];
            int index = 0;
 
            foreach (var student in students)
            {
                if (student.FirstName.StartsWith(firstNamePart, StringComparison.OrdinalIgnoreCase))
                {
                    results[index++] = student;
                }
            }

            return results;
        }


        /*
           public void SortByFirstName()
        {
            Array.Sort(students, (s1, s2) => string.Compare(s1.FirstName, s2.FirstName, StringComparison.OrdinalIgnoreCase));
        }

        public void SortByLastName()
        {
            Array.Sort(students, (s1, s2) => string.Compare(s1.LastName, s2.LastName, StringComparison.OrdinalIgnoreCase));
        }

        public void SortByDateOfBirth()
        {
            Array.Sort(students, (s1, s2) => s1.DateOfBirth.CompareTo(s2.DateOfBirth));
        }



        */
        public void SortByFirstName()
        {
            
            for (int i = 0; i < students.Length - 1; i++)
            {
                for (int j = i + 1; j < students.Length; j++)
                {
                    if (string.Compare(students[i].FirstName, students[j].FirstName, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                    
                        Student temp = students[i];
                        students[i] = students[j];
                        students[j] = temp;
                    }
                }
            }
        }

        public void SortByLastName()
        {
           
            for (int i = 0; i < students.Length - 1; i++)
            {
                for (int j = i + 1; j < students.Length; j++)
                {
                    if (string.Compare(students[i].LastName, students[j].LastName, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                     
                        Student temp = students[i];
                        students[i] = students[j];
                        students[j] = temp;
                    }
                }
            }
        }

        public void SortByDateOfBirth()
        {
           
            for (int i = 0; i < students.Length - 1; i++)
            {
                for (int j = i + 1; j < students.Length; j++)
                {
                    if (students[i].DateOfBirth > students[j].DateOfBirth)
                    {
                        
                        Student temp = students[i];
                        students[i] = students[j];
                        students[j] = temp;
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (Student student in students)
            {
                result += student.ToString() + "\n";
            }
            return result.TrimEnd();
        }

        public int Count => students.Length;


        public Student this[int index]
        {
            get
            {
                if (index < 0 || index >= students.Length)
                {
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
                }
                return students[index];
            }
            set
            {
                if (index < 0 || index >= students.Length)
                {
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
                }
                students[index] = value;
            }
        }
  


}
}
