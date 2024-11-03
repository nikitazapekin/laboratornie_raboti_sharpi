using lab7;
using System.Collections.Generic;
using System.Linq;
//namespace lab7
//{

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
        return trains.Where(t => t.Destination.Equals(destination, System.StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public void SortByDepartureTime()
    {
        trains.Sort((t1, t2) => t1.DepartureTime.CompareTo(t2.DepartureTime));
    }

    // Здесь можно добавить методы для чтения/записи из/в файл

}

/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class TrainCollection
    {
    }
}
*/