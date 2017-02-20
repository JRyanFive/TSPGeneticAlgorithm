using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 90;
            Location[] locs = new Location[size];


            Random r = new Random();

            for (int i = 0; i < size; i++)
            {
                locs[i] = new Location((i + 1).ToString(), r.Next(0, 500), r.Next(3, 500));
            }

            var e = new Engine(locs);
            double fitness = 0;          
            int j = 0;
            Individual ind = null;
            while (j < 300)
            {
                ind = e.GetBestIndividual(100);
                if (fitness == 0 || ind.TotalDistance < fitness)
                {
                    Console.WriteLine(j + " Fitness : " + 1000 / ind.TotalDistance + " Total Distance " + ind.TotalDistance);
                    fitness = ind.TotalDistance;
                }
                j++;
            }

            Console.WriteLine("Best Route:");
            foreach (var item in ind.Locations)
            {
                Console.WriteLine("({1},{2})", item.Name, item.X, item.Y);
            }
            Console.WriteLine("END");
            Console.ReadKey();
        }
    }
}
