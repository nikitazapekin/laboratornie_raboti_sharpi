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

    public void SortByDepartureTime()
    {
        /*  for (int i = 0; i < trains.Count - 1; i++)
            {
                for (int j = 0; j < trains.Count - i - 1; j++)
                {
                    if (trains[j].DepartureTime > trains[j + 1].DepartureTime)
                    {

                        TRAIN temp = trains[j];
                        trains[j] = trains[j + 1];
                        trains[j + 1] = temp;
                    }
                }
            }

            */


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
}
 