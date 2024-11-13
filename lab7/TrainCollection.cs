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
        return trains.Where(train => train.TrainNumber == trainNumber).ToList();
    }



}
