using System;
namespace SubwayMap
{
    class Edge<T>
    {
        public Vertex<T> AdjStation { get; set; }
        public ConsoleColor Colour { get; set; }

        public Edge(Vertex<T> AdjStation, ConsoleColor Colour)
        {
            this.AdjStation = AdjStation;
            this.Colour = Colour;
        }
    }
}
