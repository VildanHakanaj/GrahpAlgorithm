using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayMap
{
    class Edge<T>
    {
        public T StationName { get; set; }
        public string Colour { get; set; }

        public Edge(T StationName, string Colour)
        {
            this.StationName = StationName;
            this.Colour = Colour;
        }
    }
}
