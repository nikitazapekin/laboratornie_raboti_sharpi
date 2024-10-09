using System;

namespace lab4_variant8
{
    public class MyDate
    {
        private int year;
        private int month;
        private int day;

        public int Year
        {
            get => year;
            set
            {
                if (value < 1)
                {

                    throw new ArgumentException("Введите год больше 0.");
                } 
                year = value;
            }
        }
        public int Month
        {
            get => month;
            set
            {
                if (value < 1 || value > 12)
                {

                    throw new ArgumentException("Месяц должен быть числом от 1 до 12.");
                }
                month = value;
            }
        }

        public int Day
        {
            get => day;
            set
            {
                if (value < 1 || value > DaysInMonth(month, year))
                {

                    throw new ArgumentException($"Дней должно быть от 1 до {DaysInMonth(month, year)} для месяца {month}.");
                }
                day = value;
            }
        }

        public MyDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        private int DaysInMonth(int month, int year)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public void AddDays(int days)
        {
            DateTime date = new DateTime(year, month, day);
            date = date.AddDays(days);
            year = date.Year;
            month = date.Month;
            day = date.Day;
        }

        public void AddMonths(int months)
        {
            DateTime date = new DateTime(year, month, day);
            date = date.AddMonths(months);
            year = date.Year;
            month = date.Month;
            day = date.Day;
        }

        public void AddYears(int years)
        {
            Year += years; 
        }

        public override string ToString()
        {
            return $"{day:D2}.{month:D2}.{year}";
        }
    }
}
 