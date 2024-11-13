using System;

namespace lab7
{
    public struct TRAIN
    {
        public string Destination { get; set; }
        public int TrainNumber { get; set; }
       // public string TrainNumber { get; set; }
        public DateTime DepartureTime { get; set; }

        public TRAIN(string destination, int trainNumber, DateTime departureTime)
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
 