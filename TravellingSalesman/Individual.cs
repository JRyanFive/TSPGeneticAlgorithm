using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman
{
    public class Individual
    {
        public Location[] Locations { get; set; }
        public double TotalDistance { get; set; }

        public Individual(Location[] locations)
        {         
            Locations = locations;
            TotalDistance = GetTotalDistance();
        }

        public double GetTotalDistance()
        {
            double result = 0;
            int countLoc = Locations.Length - 1;
            for (int i = 0; i < countLoc; i++)
            {
                var actual = Locations[i];
                var next = Locations[i + 1];

                var distance = actual.GetDistance(next);
                result += distance;
            }

            result += Locations[Locations.Length - 1].GetDistance(Locations[0]);
            return result;
        }
    }
}
