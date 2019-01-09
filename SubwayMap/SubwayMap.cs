using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayMap
{
    class SubwayMap<T>
    {
        private List<Vertex<T>> Vertecies;

        public SubwayMap()
        {
            Vertecies = new List<Vertex<T>>();
        }

        /// <summary>
        /// This will insert the station{vertex}
        /// into the vertex list but 
        /// first makes sure that the 
        /// vertex doesn't exists in
        /// the list.
        /// </summary>
        /// <param name="name">The name of the station</param>
        public void InsertStation(T name)
        {
            if (FindVertex(name) == -1)
            {
                Vertecies.Add(new Vertex<T>(name));
            }
            else
            {
                Console.WriteLine("Station already exists\n");
            }
        }

        public void InsertLink(T from, T to, string color)
        {
            int fromPos, toPos;
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {
                if (Vertecies[fromPos].FindEdge(to, color) == -1)
                {
                    Vertecies[fromPos].Edges.Add(new Edge<T>(to, color));
                    Vertecies[toPos].Edges.Add(new Edge<T>(from, color));
                }
                else
                {
                    Console.WriteLine("The link with color {0} from {1} to {2} already exists", color, from, to);
                }
            }
        }

        #region HelperMethods

        /// <summary>
        /// Loops through the list of vertex
        /// and will check if the vertext (name) 
        /// already exists in there
        /// 
        /// </summary>
        /// <param name="name">Name of the vertex</param>
        /// <returns>{bool} true</returns> if the vertex exists
        /// <returns>{bool} false</returns> if the vertex doesn't exists
        private int FindVertex(T name)
        {
            for (int i = 0; i < Vertecies.Count; i++)
            {
                if (Vertecies[i].Name.Equals(name))
                {
                    return i;
                }
            }
            return -1;
        }

        public void PrintGraph()
        {
            for (int i = 0; i < Vertecies.Count; i++)
            {
                for (int j = 0; j < Vertecies[i].Edges.Count; j++)
                {
                    Console.WriteLine("From:{0} --> TO: {1}-->Color {2}", Vertecies[i].Name, Vertecies[i].Edges[j].StationName, Vertecies[i].Edges[j].Colour);
                }
            }
        }

        #endregion
    }
}
