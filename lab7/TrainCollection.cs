 
 
using lab7;
using System;
using System.Collections.Generic;
 
public class TrainCollection
{
    private List<TRAIN> trains = new List<TRAIN>();
 
    public void Add(TRAIN train)
    {
        trains.Add(train);
    }

    public void Remove(TRAIN train)
    {
        trains.Remove(train);
    }

    public List<TRAIN> GetAllTrains()
    {
        return trains;
    }
    public void SortByDepartureTime()
    {
        trains.Sort((train1, train2) => train1.DepartureTime.CompareTo(train2.DepartureTime));

    }
    public void SortByTrainNumber()
    {
        trains.Sort((train1, train2) => train1.TrainNumber.CompareTo(train2.TrainNumber));
         
    }
    public void SortByDestination()
    {
        trains.Sort((train1, train2) => string.Compare(train1.Destination, train2.Destination, StringComparison.OrdinalIgnoreCase));
    }
    public void RemoveSelectedTrains(List<TRAIN> selectedTrains)
    {
        foreach (var train in selectedTrains)
        {
            trains.Remove(train);
        }
    }
    public List<TRAIN> FindByTrainNumber(int trainNumber)
    {
        List<TRAIN> matchingTrains = new List<TRAIN>();

        foreach (TRAIN train in trains)
        {
            if (train.TrainNumber == trainNumber)
            {
                matchingTrains.Add(train);
            }
        }

        return matchingTrains;
    }

    public List<TRAIN> FindByDestination(string destination)
    {
        List<TRAIN> matchingTrains = new List<TRAIN>();
        foreach (TRAIN train in trains)
        {
            if (string.Equals(train.Destination, destination, StringComparison.OrdinalIgnoreCase))
            {
                matchingTrains.Add(train);
            }
        }
        return matchingTrains;
    }
   public List<TRAIN> FindByDepartureDate(DateTime date)
    {
        List<TRAIN> matchingTrains = new List<TRAIN>();
        foreach (TRAIN train in trains)
        {
            if (train.DepartureTime.Date == date.Date)
            {
                matchingTrains.Add(train);
            }
        }
        return matchingTrains;
    }


    public List<TRAIN> FindByDepartureTimeRange(DateTime startDateTime, DateTime endDateTime)
    {
        List<TRAIN> matchingTrains = new List<TRAIN>();
        foreach (TRAIN train in trains)
        {
            if (train.DepartureTime >= startDateTime && train.DepartureTime <= endDateTime)
            {
                matchingTrains.Add(train);
            }
        }
        return matchingTrains;
    }


}


/*
using lab7;
using System;
using System.Collections.Generic;
using System.Linq;

public class TrainCollection
{
   private List<TRAIN> trains = new List<TRAIN>();

   public void Add(TRAIN train)
   {
       trains.Add(train);
   }

   public void Remove(TRAIN train)
   {
       trains.Remove(train);
   }

   public List<TRAIN> GetAllTrains()
   {
       return trains;
   }

   public void SortByDepartureTime()
   {
       trains = trains.OrderBy(train => train.DepartureTime).ToList();
   }

   public void SortByTrainNumber()
   {
       trains = trains.OrderBy(train => train.TrainNumber).ToList();
   }

   public void SortByDestination()
   {
       trains = trains.OrderBy(train => train.Destination, StringComparer.OrdinalIgnoreCase).ToList();
   }

   public void RemoveSelectedTrains(List<TRAIN> selectedTrains)
   {
       trains = trains.Except(selectedTrains).ToList();
   }

   public List<TRAIN> FindByTrainNumber(int trainNumber)
   {
       return trains.Where(train => train.TrainNumber == trainNumber).ToList();
   }

   public List<TRAIN> FindByDestination(string destination)
   {
       return trains.Where(train => string.Equals(train.Destination, destination, StringComparison.OrdinalIgnoreCase)).ToList();
   }

   public List<TRAIN> FindByDepartureDate(DateTime date)
   {
       return trains.Where(train => train.DepartureTime.Date == date.Date).ToList();
   }
}
*/