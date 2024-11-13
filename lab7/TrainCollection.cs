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

}
