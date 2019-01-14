using System;
using System.Collections.Generic;
using System.Linq;

namespace SubwayMap
{
    class SubwayMap<T> : ISubwayMap<T>
    {
        #region Variables
        private List<Vertex<T>> Vertecies;
        private int time = 0;
        #endregion

        public SubwayMap()
        {
            Vertecies = new List<Vertex<T>>();
        }

        #region Main Methods for subway

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
                MessageDisplay("Just inserted station [ " + name + " ] into the graph\n", ConsoleColor.Green);
            }
            else
            {
                MessageDisplay("Station " + name + " already exists\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        ///InsertLink
        ///Will insert a link if it doesn't exist
        ///if it does then it will not accept the link
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="color"></param>
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
                    MessageDisplay("The link with color " + color + " from " + from + " to " + to + " already exists\n", ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Remove the link between two vertecies
        /// if the vertecies dont exists it will throw an error.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="color"></param>
        public void RemoveLink(T from, T to, string color)
        {
            int fromPos, toPos, edgePos;
            Vertex<T> FromVertex, ToVertex;
            //Find if the vertecies exists
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {

                FromVertex = Vertecies[fromPos];
                ToVertex = Vertecies[toPos];

                //I asume that the edge is viceversa so i wont need to check the other way around 
                //There is an edge
                if ((edgePos = Vertecies[fromPos].FindEdge(to, color)) > -1)
                {
                    //Remove the link from the FROM station
                    Vertecies[fromPos].Edges.RemoveAt(edgePos);
                    edgePos = Vertecies[toPos].FindEdge(from, color);
                    Vertecies[toPos].Edges.RemoveAt(edgePos);
                    MessageDisplay("\nJust Deleted the link between " + FromVertex.Name + " and " + ToVertex.Name + "\n", ConsoleColor.Green);
                }
                else
                {
                    MessageDisplay("\nThe link between " + Vertecies[fromPos].Name + " and " + Vertecies[toPos].Name + " with color " + color + " doesn't exist\n", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue!");
                    Console.ReadKey();
                }
            }
            else
            {
                MessageDisplay("\nOne of the station inputed doesn't exists\n", ConsoleColor.Red);
                Console.WriteLine("Press any key to continue!");
                Console.ReadKey();
            }
        }
        #endregion

        #region ShortestPath
        public void ShortestPath(T from, T to)
        {
            //Store the position of the vertecies from and to
            int fromPos, toPos;
            //Check if the vertexes exists
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {
                Console.WriteLine("The two vertecies exist");
                ShortestPath(Vertecies[fromPos], Vertecies[toPos]);
            }
            else
            {
                Console.WriteLine("Error one of the stations doesn't exists");
            }

        }

        private void ShortestPath(Vertex<T> CurrentStation, Vertex<T> ToStation)
        {
            //Queue to store the vertex i find
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            //To keep track of the previous vertex
            Vertex<T> prev;
            //Set the status as visited for that vertex 
            CurrentStation.Visited = true;
            //Store the first vertex
            queue.Enqueue(CurrentStation);
            //Do until the queue is empty
            while (queue.Count > 0)
            {
                //Keep the parent
                prev = CurrentStation;

                //[ ] Add the current vertex as the parent of the Adjvertex 

                //Get the next child
                CurrentStation = queue.Dequeue();

                //Check if we have reached the destination
                if (!CurrentStation.Visited && CurrentStation.Equals(ToStation))
                {
                    Console.WriteLine("We have a match for sation {0}", ToStation.Name);
                }
                else
                {
                    //Set the vertex where it came from
                    CurrentStation.Parent = prev;
                    for (int i = 0; i < CurrentStation.Edges.Count; i++)
                    {
                        if (!CurrentStation.Edges[i].AdjStation.Visited)
                        {
                            //Set the status as visited 
                            CurrentStation.Edges[i].AdjStation.Visited = true;
                            //Add that vertex into the queue
                            queue.Enqueue(CurrentStation.Edges[i].AdjStation);
                        }
                    }
                }
            }
        }
        #endregion

        #region Articulation points
        /// <summary>
        /// CriticalPoints 
        /// Sets all the vertecies like unvisited 
        /// Creates the articulation poin list
        /// </summary>
        public void CriticalPoints()
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
                    CriticalPoints(Vertecies[i], ArticulationPoints);
                }

            // Now ap[] contains articulation points, print them 
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                if (ArticulationPoints.Contains(Vertecies[i]))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Vertecies[i].Name + " Is an articulation Point");
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// CriticalPoints
        /// Will go and run DFS on the graph and find
        /// all the articulation points
        /// 
        /// In this method i have utilized the articulation point algorithm 
        /// which will find the critical points of this graphs
        /// </summary>
        /// <param name="CurrentVertex">The vertex we are currently on</param>
        /// <param name="ArticulationPoints">The list of articulation points</param>
        private void CriticalPoints(Vertex<T> CurrentVertex, List<Vertex<T>> ArticulationPoints)
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
                    CriticalPoints(AdjVertex, ArticulationPoints);

                    // Check if the subtree rooted with AdjVertex has a connection to 
                    CurrentVertex.LowLink = Math.Min(CurrentVertex.LowLink, AdjVertex.LowLink);

                    // u is an articulation point in following cases 

                    // (1) u is root of DFS tree and has two or more chilren. 
                    if (CurrentVertex.Parent == null && children > 1)
                    {
                        ArticulationPoints.Add(CurrentVertex);
                    }

                    // (2) If u is not root and low value of one of its child 
                    // is more than discovery value of u.  
                    if (CurrentVertex.Parent != null && AdjVertex.LowLink >= CurrentVertex.Discovered)
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
                        Console.WriteLine("[ {0} ] --> [ {1} ] ==> {2}", Vertecies[i].Name, Vertecies[i].Edges[j].AdjStation.Name, Vertecies[i].Edges[j].Colour);
                    }
                }
                else
                {
                    MessageDisplay("Station " + Vertecies[i].Name + " doesn't have any edges", ConsoleColor.Yellow);
                }
            }
            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

        }

        private void MessageDisplay(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();

        }

        #endregion
    }
}
