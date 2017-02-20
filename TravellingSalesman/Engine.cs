using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman
{
    public class Engine
    {
        private readonly Random _random = new Random();
        private readonly List<Individual> _populations;

        public Engine(Location[] locations, int populationCount = 100)
        {
            var startLocation = locations[0];
            _populations = new List<Individual>();

            _populations.Add(new Individual(locations));

            Location[] locsClone = locations;
            for (int i = 1; i < populationCount; i++)
            {
                locsClone = (Location[])locsClone.Clone();
                var randomLocs = GetRandomLocations(locsClone);
                _populations.Add(new Individual(randomLocs));
            }
        }

        public Location[] GetBestLocations()
        {
            var a = GetBestIndividual();
            return a.Locations;
        }

        public Individual GetBestIndividual(int reproduceCount = 10)
        {
            Reproduce();

            _populations.Sort(delegate (Individual x, Individual y)
            {
                return x.TotalDistance.CompareTo(y.TotalDistance);
            });

            for (int i = 0; i < 15; i++)
            {
                _populations.Remove(_populations.Last());
            }

            if (reproduceCount == 0)
            {
                return _populations[0];
            }

            return GetBestIndividual(reproduceCount - 1);
        }

        private void Reproduce()
        {
            for (int i = 0; i < 20; i++)
            {
                var children = CrossOver(_populations[i], _populations[i + 1]);
                foreach (var child in children)
                {
                    _populations.Add(child);
                }
                i++;
            }
        }

        private Individual[] CrossOver(Individual parent1, Individual parent2)
        {
            int locsLen = parent1.Locations.Length;
            var crossIndex = locsLen / 2;

            var locsChild1 = (Location[])parent1.Locations.Clone();
            var locsChild2 = (Location[])parent2.Locations.Clone();
            for (int i = 0; i < crossIndex; i++)
            {
                var locSwap = locsChild2[i];
                locsChild2[i] = locsChild1[i];
                locsChild1[i] = locSwap;
            }

            locsChild1 = CheckDuplicateLoc(parent2.Locations, locsChild1);
            locsChild2 = CheckDuplicateLoc(parent1.Locations, locsChild2);
            return new Individual[] {
                new Individual(Mutate(locsChild1)),
                new Individual(Mutate(locsChild2)),
            };
        }

        private Location[] CheckDuplicateLoc(Location[] parent, Location[] child)
        {
            var availableCheck = new HashSet<Location>(parent);

            List<int> duplicateIndex = new List<int>();

            for (int i = 0; i < child.Length; i++)
            {
                if (!availableCheck.Remove(child[i]))
                {
                    duplicateIndex.Add(i);
                }
            }

            foreach (var index in duplicateIndex)
            {
                child[index] = availableCheck.First();
                availableCheck.Remove(child[index]);
            }

            return child;
        }

        private Location[] Mutate(Location[] chromosome)
        {
            Random r = new Random();
            int i = r.Next(0, 90);//hardcode
            int j = r.Next(0, 90);
            Location swap = chromosome[i];
            chromosome[i] = chromosome[j];
            chromosome[j] = swap;
            return chromosome;
        }

        private int GetRandomValue(int limit)
        {
            return _random.Next(1, limit);
        }

        private Location[] GetRandomLocations(Location[] locations)
        {
            int count = locations.Length;
            for (int i = count - 1; i > 0; i--)
            {
                int value = GetRandomValue(i + 1);
                if (value != i)
                {
                    var firstLoc = locations[i];
                    var secondLoc = locations[value];
                    locations[i] = secondLoc;
                    locations[value] = firstLoc;
                }
            }

            return locations;
        }
    }
}
