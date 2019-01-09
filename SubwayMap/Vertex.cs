using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayMap
{
    class Vertex<T>
    {
        /*
         * Data fields
         * [ ] bool value to check if the station has been visited
         * [ ] A list of edges that this vertex is connected to
         * [ ] T name to name the vertex
         * Methods
         * [ ] Vertex() -> constructor
         * [ ] FindEdge(string name)-> Find if this vertex has any edges
         */
         //The name of the vertex
        public T Name { get; set; }
        //IsVisited Flag
        public bool Visited { get; set; }
        //List of edges connected to this vertex
        public List<Edge<T>> Edges { get; set; }

        public Vertex(T Name)
        {
            this.Name = Name;
            Edges = new List<Edge<T>>();
        }

        /// <summary>
        /// Loop through the edges list
        /// and check if there is an edge
        /// between this.vertex and the passed vertex
        /// </summary>
        /// <param name="StationName">Vertex to be checked against</param>
        /// <returns>{int} i --> if the position of the edge exists</returns>
        /// <returns>{int} -1 --> if the position of the edge doens't exists</returns>
        public int FindEdge(T StationName, string Colour)
        {
            for (int i = 0; i < Edges.Count; i++)
            {
                Edge<T> edge = Edges[i];
                if (edge.Colour.Equals(Colour) && edge.StationName.Equals(StationName))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool HasEdges() => Edges.Count > 0;

    }
}
