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
        private int time = 0;
        private const int NIL = -1;
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
                Console.WriteLine("Just inserted station [ {0} ] into the graph", name);
            }
            else
            {
                Console.WriteLine("Station {0} already exists\n", name);
            }
        }

        public void InsertLink(T from, T to, string color)
        {
            int fromPos, toPos;

            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {
                if (Vertecies[fromPos].FindEdge(to, color) == -1)
                {
                    Vertecies[fromPos].Edges.Add(new Edge<T>(Vertecies.ElementAt(toPos), color));
                    Vertecies[toPos].Edges.Add(new Edge<T>(Vertecies.ElementAt(fromPos), color));
                }
                else
                {
                    Console.WriteLine("The link with color {0} from {1} to {2} already exists", color, from, to);
                }
            }
        }

        public void RemoveLink(T from, T to, string color)
        {
            int fromPos, toPos, edgePos;

            //The vertecies exists
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {

                //There is an edge
                if ((edgePos = Vertecies[fromPos].FindEdge(to, color)) > -1)
                {
                    //Remove the link from the FROM station
                    Vertecies[fromPos].Edges.RemoveAt(edgePos);
                    //Remove the link from the TO station
                    Vertecies[toPos].Edges.RemoveAt(edgePos);
                    Console.WriteLine("\nJust Deleted the link between {0} and {1}", Vertecies[fromPos].Name, Vertecies[toPos].Name);
                }
                else
                {
                    Console.WriteLine("\nThe link between {0} and {1} doesn't exist", Vertecies[fromPos].Name, Vertecies[toPos].Name);
                }
            }
            else
            {
                Console.WriteLine("\nOne of the station inputed doesn't exists");
            }
        }

        // Breadth-First Search
        // Performs a breadth-first search (with re-start)
        // Time Complexity: O(max(n,m))

        public void BreadthFirstSearch()
        {
            int i;
            for (i = 0; i < Vertecies.Count; i++)
            {
                Vertecies[i].Visited = false;              // Set all vertices as unvisited
            }
            for (i = 0; i < Vertecies.Count; i++)
                if (!Vertecies[i].Visited)                  // (Re)start with vertex i
                {
                    BreadthFirstSearch(Vertecies[i]);
                    Console.WriteLine();
                }
        }

        private void BreadthFirstSearch(Vertex<T> v)
        {
            int j;
            Vertex<T> w;
            Queue<Vertex<T>> Q = new Queue<Vertex<T>>();

            v.Visited = true;        // Mark vertex as visited when placed in the queue
            Q.Enqueue(v);

            while (Q.Count != 0)
            {
                v = Q.Dequeue();     // Output vertex when removed from the queue
                Console.WriteLine(v.Name);

                for (j = 0; j < v.Edges.Count; j++)    // Enqueue unvisited adjacent vertices
                {
                    w = v.Edges[j].AdjStation;
                    if (!w.Visited)
                    {
                        w.Visited = true;          // Mark vertex as visited
                        Q.Enqueue(w);
                    }
                }
            }
        }
        #region Articulation points
        private void APUtil(int u, bool[] visited, int[] disc, int[] low, int[] parent, bool[] ap)
        {
            // Count of children in DFS Tree 
            int children = 0;

            // Mark the current node as visited 
            visited[u] = true;

            // Initialize discovery time and low value 
            disc[u] = low[u] = ++time;

            // Go through all vertices aadjacent to this 
            for (int i = 0; i <= Vertecies.Count; i++)
            {
                int v = u + 1;  // v is current adjacent of u 
                if (v < Vertecies.Count)
                {
                    // If v is not visited yet, then make it a child of u 
                    // in DFS tree and recur for it 
                    if (!visited[v])
                    {
                        children++;
                        parent[v] = u;
                        APUtil(v, visited, disc, low, parent, ap);

                        // Check if the subtree rooted with v has a connection to 
                        // one of the ancestors of u 
                        low[u] = Math.Min(low[u], low[v]);

                        // u is an articulation point in following cases 

                        // (1) u is root of DFS tree and has two or more chilren. 
                        if (parent[u] == NIL && children > 1)
                            ap[u] = true;

                        // (2) If u is not root and low value of one of its child 
                        // is more than discovery value of u. 
                        if (parent[u] != NIL && low[v] >= disc[u])
                            ap[u] = true;
                    }
                }
                // Update low value of u for parent function calls. 
                else if (v != parent[u])
                    low[u] = Math.Min(low[u], disc[v - 1]);
            }
        }

        // The function to do DFS traversal. It uses recursive function APUtil() 
        public void AP()
        {
            int size = Vertecies.Count;
            // Mark all the vertices as not visited 
            bool[] visited = new bool[Vertecies.Count];
            //Discovery time
            int[] disc = new int[size];
            //When it was 
            int[] low = new int[size];
            //Parents
            int[] parent = new int[size];
            //ArticulationPoints
            bool[] ap = new bool[size]; // To store articulation points 

            // Initialize parent and visited, and ap(articulation point) 
            // arrays 
            for (int i = 0; i < size; i++)
            {
                parent[i] = NIL;
                visited[i] = false;
                ap[i] = false;
            }

            // Call the recursive helper function to find articulation 
            // points in DFS tree rooted with vertex 'i' 
            for (int i = 0; i < size; i++)
                if (visited[i] == false)
                {
                    APUtil(i, visited, disc, low, parent, ap);
                }

            // Now ap[] contains articulation points, print them 
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                if (ap[i] == true)
                { 
                    Console.WriteLine(Vertecies[i].Name + " Is an articulation Point");
                }
            }
            Console.WriteLine();
        }
        #endregion
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
                if (Vertecies[i].HasEdges())
                {
                    for (int j = 0; j < Vertecies[i].Edges.Count; j++)
                    {
                        Console.WriteLine("From:{0} --> TO: {1}-->Color {2}", Vertecies[i].Name, Vertecies[i].Edges[j].AdjStation.Name, Vertecies[i].Edges[j].Colour);
                    }
                }
                else
                {
                    Console.WriteLine("Station {0} doesn't have any edges", Vertecies[i].Name);
                }
            }
        }

        #endregion
    }
}
