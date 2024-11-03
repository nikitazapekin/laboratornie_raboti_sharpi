using System;

namespace lab7
{
    public struct TRAIN
    {
        public string Destination { get; set; }
        public string TrainNumber { get; set; }
        public DateTime DepartureTime { get; set; }

        public TRAIN(string destination, string trainNumber, DateTime departureTime)
        {
            Destination = destination;
            TrainNumber = trainNumber;
            DepartureTime = departureTime;
        }

        public override string ToString()
        {
            return $"Поезд №{TrainNumber}, Направление: {Destination}, Время отправления: {DepartureTime}";
        }
    }
}

/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class Class1
    {
    }
}

*/