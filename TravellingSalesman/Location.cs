using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman
{
    public sealed class Location
    {
        public string Name { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Location(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public double GetDistance(Location other)
        {
            int diffX = Math.Abs(X - other.X);
            int diffY = Math.Abs(Y - other.Y);
            return Math.Sqrt(diffX * diffX + diffY * diffY);
        }
    }
}
