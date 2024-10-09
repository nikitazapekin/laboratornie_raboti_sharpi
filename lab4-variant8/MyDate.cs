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
                    throw new ArgumentException("Year must be greater than 0.");
                year = value;
            }
        }

        public int Month
        {
            get => month;
            set
            {
                if (value < 1 || value > 12)
                    throw new ArgumentException("Month must be between 1 and 12.");
                month = value;
            }
        }

        public int Day
        {
            get => day;
            set
            {
                if (value < 1 || value > DaysInMonth(month, year))
                    throw new ArgumentException($"Day must be between 1 and {DaysInMonth(month, year)} for month {month}.");
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
            Year += years; // year is already validated in Year property
        }

        public override string ToString()
        {
            return $"{day:D2}.{month:D2}.{year}";
        }
    }
}


/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_variant8
{
    internal class MyDate
    {
    }
}
*/