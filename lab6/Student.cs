 
 using System;
using System.Collections.Generic;

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

        public int CompareTo(Student other)
        {
            return CompareStrings(LastName, other.LastName);
        }

        public string ToString()
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
    }

    public class StudentGroup
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public List<Student> FindStudentsByLastName(string lastName)
        {
            List<Student> result = new List<Student>();
            foreach (Student student in students)
            {
                if (StartsWithIgnoreCase(student.LastName, lastName))
                {
                    result.Add(student);
                }
            }
            return result;
        }

        public List<Student> FindStudentsByLastNameFull(string lastName)
        {
            List<Student> result = new List<Student>();
            foreach (Student student in students)
            {
                if (EqualsIgnoreCase(student.LastName, lastName))
                {
                    result.Add(student);
                }
            }
            return result;
        }

        public void RemoveStudent(Student student)
        {
            int index = -1;
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i] == student)
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0)
            {
                for (int i = index; i < students.Count - 1; i++)
                {
                    students[i] = students[i + 1];
                }
                students.RemoveAt(students.Count - 1);
            }
        }

        public List<Student> FindStudentsByFirstName(string firstNamePart)
        {
            List<Student> result = new List<Student>();
            foreach (Student student in students)
            {
                if (StartsWithIgnoreCase(student.FirstName, firstNamePart))
                {
                    result.Add(student);
                }
            }
            return result;
        }

        public void SortByFirstName()
        {
            BubbleSort((s1, s2) => Student.CompareStrings(s1.FirstName, s2.FirstName));
        }

        public void SortByLastName()
        {
            BubbleSort((s1, s2) => Student.CompareStrings(s1.LastName, s2.LastName));
        }

        public void SortByDateOfBirth()
        {
            BubbleSort((s1, s2) => s1.DateOfBirth < s2.DateOfBirth ? -1 : (s1.DateOfBirth > s2.DateOfBirth ? 1 : 0));
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

        public Student this[int index]
        {
            get { return students[index]; }
        }

        public int Count => students.Count;
 
        private void BubbleSort(Func<Student, Student, int> compare)
        {
            for (int i = 0; i < students.Count - 1; i++)
            {
                for (int j = 0; j < students.Count - i - 1; j++)
                {
                    if (compare(students[j], students[j + 1]) > 0)
                    {
                      
                        var temp = students[j];
                        students[j] = students[j + 1];
                        students[j + 1] = temp;
                    }
                }
            }
        }

        private bool StartsWithIgnoreCase(string str, string prefix)
        {
            if (str.Length < prefix.Length) return false;
            for (int i = 0; i < prefix.Length; i++)
            {
                if (char.ToLower(str[i]) != char.ToLower(prefix[i]))
                {
                    return false;
                }
            }
            return true;
        }
 
        private bool EqualsIgnoreCase(string str1, string str2)
        {
            if (str1.Length != str2.Length) return false;
            for (int i = 0; i < str1.Length; i++)
            {
                if (char.ToLower(str1[i]) != char.ToLower(str2[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
 