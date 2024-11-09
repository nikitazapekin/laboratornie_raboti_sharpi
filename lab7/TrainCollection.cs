using lab7;
using System;
using System.Collections.Generic;

public class TrainCollection
{
    private List<TRAIN> trains = new List<TRAIN>();
    private List<TRAIN> trainsCopy = new List<TRAIN>();
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
        trains.Sort((train1, train2) => string.Compare(train1.TrainNumber, train2.TrainNumber, StringComparison.Ordinal));
    }

    public void SortByDestination()
    {
        trains.Sort((train1, train2) => string.Compare(train1.Destination, train2.Destination, StringComparison.OrdinalIgnoreCase));
    }

    /*
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
    */

    public List<TRAIN> FindByDestination(string destination)
    {
        List<TRAIN> matchingTrains = new List<TRAIN>();
        string trimmedDestination = destination?.Trim().ToLower(); 
        foreach (TRAIN train in trains)
        {
            string trainDestination = train.Destination?.Trim().ToLower(); 
            if (string.Equals(trainDestination, trimmedDestination, StringComparison.OrdinalIgnoreCase))
            {
                matchingTrains.Add(train);
            }
        }
        return matchingTrains;
    }


    public List<TRAIN> FindByTrainNumber(string trainNumner)
    {
        List<TRAIN> matchingTrains = new List<TRAIN>();
        foreach (TRAIN train in trains)
        {
            if (string.Equals(train.TrainNumber,trainNumner, StringComparison.Ordinal))
            {
                matchingTrains.Add(train);
            }
        }
        return matchingTrains;
    }
}
 