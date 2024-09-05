using System;
using ConsoleApp8.Models;
namespace ConsoleApp8.Utilities
{
    public static class Helper
    {
        public static void PrintPersonInfo(Person person)
        {
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
        }
    }
}
