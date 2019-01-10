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
        /// <summary>
        /// APUtil
        /// Will go and run DFS on the graph and find
        /// all the articulation points
        /// </summary>
        /// <param name="CurrentVertex">The vertex we are currently on</param>
        /// <param name="ArticulationPoints">The list of articulation points</param>
        private void APUtil(Vertex<T> CurrentVertex, List<Vertex<T>> ArticulationPoints)
        {
            // Count of children in DFS Tree 
            int children = 0;

            // Mark the current node as visited 
            CurrentVertex.Visited = true;

            //Timestamp when it was discovered 
            CurrentVertex.Discovered = CurrentVertex.LowLink = ++time;

            // Go through all vertices adjacent to this 
            for (int i = 0; i < CurrentVertex.Edges.Count; i++)
            {
                Vertex<T> AdjVertex = CurrentVertex.Edges[i].AdjStation;  // v is next adjacent of CurrentVertex 
                                                                          // If AdjVertex is not visited yet, then make it a child of current vertex in DFS tree and recur for it 
                if (!AdjVertex.Visited)
                {
                    children++;
                    
                    //Set the parent of the adjecent vertex
                    AdjVertex.Parent = CurrentVertex; 

                    //Run the Explore the adj vertex
                    APUtil(AdjVertex, ArticulationPoints);

                    // Check if the subtree rooted with v has a connection to 
                    CurrentVertex.Discovered = Math.Min(CurrentVertex.LowLink, AdjVertex.LowLink);

                    // u is an articulation point in following cases 

                    // (1) u is root of DFS tree and has two or more chilren. 
                    if (CurrentVertex.Parent == null && children > 1)
                    {
                        ArticulationPoints.Add(CurrentVertex);
                    }

                    // (2) If u is not root and low value of one of its child 
                    // is more than discovery value of u. 
                    if (CurrentVertex.Parent != null && AdjVertex.LowLink > CurrentVertex.Discovered)
                    {
                        ArticulationPoints.Add(CurrentVertex);
                    }
                }
                else if (AdjVertex != CurrentVertex.Parent)
                {
                    // Update low value of u for parent function calls. 
                    CurrentVertex.LowLink = Math.Min(CurrentVertex.LowLink, AdjVertex.Discovered);
                }
            }
        }

        // The function to do DFS traversal. It uses recursive function APUtil() 
        public void AP()
        {
            int size = Vertecies.Count;
            List<Vertex<T>> ArticulationPoints = new List<Vertex<T>>(); // To store articulation points 

            // Initialize parent and visited, and ap(articulation point) 
            // arrays 
            for (int i = 0; i < size; i++)
            {
                Vertecies[i].Parent = null;
                Vertecies[i].Visited = false;
            }

            //Call the recursive helper function to find articulation
            // points in DFS tree rooted with vertex 'i'
            for (int i = 0; i < size; i++)
                if (Vertecies[i].Visited == false)
                {
                    APUtil(Vertecies[i], ArticulationPoints);
                }

            // Now ap[] contains articulation points, print them 
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                if (ArticulationPoints.Contains(Vertecies[i]))
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
