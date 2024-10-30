using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            return LastName.CompareTo(other.LastName);
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Дата рождения: {DateOfBirth.ToShortDateString()}";
        }
    }

    public class StudentGroup
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            students.Remove(student);
        }

        public List<Student> FindStudentsByLastName(string lastNamePart)
        {
            return students
                .Where(s => s.LastName.StartsWith(lastNamePart, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void SortByFirstName()
        {
            students = students.OrderBy(s => s.FirstName).ToList();
        }

        public void SortByLastName()
        {
            students = students.OrderBy(s => s.LastName).ToList();
        }

        public void SortByDateOfBirth()
        {
            students = students.OrderBy(s => s.DateOfBirth).ToList();
        }

        public override string ToString()
        {
            return string.Join("\n", students.Select(s => s.ToString()));
        }

        public Student this[int index]
        {
            get { return students[index]; }
        }

        public int Count => students.Count;
    }
}


/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
        return LastName.CompareTo(other.LastName);  
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, Дата рождения: {DateOfBirth.ToShortDateString()}";
    }
}

public class StudentGroup
{
    private List<Student> students = new List<Student>();

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
    }

    public Student FindStudentByLastName(string lastName)
    {

         //   MessageBox.Show($"Search {lastName} ");
            return students.FirstOrDefault(s => s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
    }


        public void SortByFirstName()
        {
            students = students.OrderBy(s => s.FirstName).ToList();
        }

        public void SortByLastName()
        {
            students = students.OrderBy(s => s.LastName).ToList();
        }

        public void SortByDateOfBirth()
        {
            students = students.OrderBy(s => s.DateOfBirth).ToList();
        }

    
    public  string ToString()
    {
        return string.Join("\n", students.Select(s => s.ToString()));
    }

    public Student this[int index]
    {
        get { return students[index]; }
    }

    public int Count => students.Count;
}
}

 */