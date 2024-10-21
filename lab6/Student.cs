
using System;
using System.Collections.Generic;
using System.Linq;

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
        return students.FirstOrDefault(s => s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
    }

    public void SortByLastName()
    {
        students.Sort();
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
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    internal class Student
    {
    }
}
*/