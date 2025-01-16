using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class GraduateStudent : Student
    {

        public string UniversityName { get; set; }
        public string Degree { get; set; }

        public GraduateStudent(string firstName, string lastName, DateTime dateOfBirth, string universityName, string degree)
            : base(firstName, lastName, dateOfBirth)
        {
            UniversityName = universityName;
            Degree = degree;
        }

        public GraduateStudent()
        {
            UniversityName = "";
            Degree = "";
        }



        public override string ToString()
        {
            return $"{FirstName} {LastName}, Дата рождения: {DateOfBirth.ToShortDateString()}, Университет: {UniversityName}, Степень: {Degree}";
        }



    }
}
