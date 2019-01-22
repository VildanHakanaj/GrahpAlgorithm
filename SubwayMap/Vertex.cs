﻿using System.Collections.Generic;

namespace SubwayMap
{
    class Vertex<T>
    {
        #region Data Fields

        //The name of the vertex
        public T Name { get; set; }

        //IsVisited Flag
        public bool Visited { get; set; }

        //Time when it was discovered
        public int Discovered { get; set; }

        //The lowest back edge 
        public int LowLink { get; set; }

        //The parent where it came from
        public Vertex<T> Parent { get; set; }

        //List of edges connected to this vertex
        public List<Edge<T>> Edges { get; set; }
        public int Layer { get; set; }
        #endregion

        //Constructor
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
                if (edge.Colour.Equals(Colour) && (edge.AdjStation.Name.Equals(StationName)))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
