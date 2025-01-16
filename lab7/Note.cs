using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    public struct NOTE
    {
   
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public NOTE(string username, string title, DateTime date)
        {
         Author = username;

            Title = title;
            Date = date;
        }

        public override string ToString()
        {
            return $" ";
        }
    }
}

 