using System;
using ConsoleApp8.Utilities;
using ConsoleApp8.Models;

namespace ConsoleApp8 // Пространство имен должно совпадать
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта из пространства имен Models
            Person person = new Person { Name = "Alice", Age = 30 };

            // Вызов метода из пространства имен Utilities
            Console.WriteLine("Person Info:");
            Helper.PrintPersonInfo(person);
        }
    }
}
