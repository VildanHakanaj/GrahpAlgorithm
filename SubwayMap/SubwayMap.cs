/*======================================================================================================================
|   A representation of a subway map using the Grahp algorithms
|   For this project i have used the adjacency list graph
|
|   Name:           Subwaymap --> Class
|
|   Written by:     Vildan Hakanaj - January 2019
|
|   Written for:    COIS 3020 (Prof. Brian Patrick) Assignment #1 Trent University Winter 2019.
|
|   Purpose:        To Represent the Subway map and the links between stations
|
|   Usage:          Used in the main program
|
|
======================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SubwayMap
{
    class SubwayMap<T> : ISubwayMap<T>
    {
        #region Variables
        private List<Vertex<T>> Vertecies;

        //Used to track the time of dicovery for the critical points
        private int time = 0;
        #endregion

        public SubwayMap()
        {
            Vertecies = new List<Vertex<T>>();
        }

        #region Main Methods for subway

        /// <summary>
        /// InsertStation
        /// 
        /// Will insert a new vertex with 
        /// the given name only if it doesn't 
        /// exists already
        /// 
        /// </summary>
        /// <param name="name">The name of the station</param>
        public void InsertStation(T name)
        {
            //Check if the vertex doesnt already exists
            if (FindVertex(name) == -1)
            {
                Vertecies.Add(new Vertex<T>(name));
                //Print message
                Helper.MessageDisplay("Just inserted station [ " + name + " ] into the graph\n", ConsoleColor.Green);
            }
            else
            {
                //Print out the error 
                Helper.MessageDisplay("Station " + name + " already exists\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// InsertLink
        /// 
        /// Will insert a link between
        /// two existing vertecies only if 
        /// it doesn't exists already
        /// 
        /// </summary>
        /// <param name="from">Start station</param>
        /// <param name="to">End station</param>
        /// <param name="color">The line color</param>
        public void InsertLink(T from, T to, ConsoleColor color)
        {
            //For the positions of the vertecies
            int fromPos, toPos;

            //Check if the stations dont exits
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {
                //Check if the edge doesn't already exist
                if (Vertecies[fromPos].FindEdge(to, color) == -1)
                {
                    //insert the link both ways since its a undirected graph
                    Vertecies[fromPos].Edges.Add(new Edge<T>(Vertecies.ElementAt(toPos), color));
                    Vertecies[toPos].Edges.Add(new Edge<T>(Vertecies.ElementAt(fromPos), color));
                }
                else
                {
                    //Print message
                    Helper.MessageDisplay("The link with color " + color + " from " + from + " to " + to + " already exists\n", ConsoleColor.Red);
                }
            }
            else
            {
                Helper.MessageDisplay("One of the station doesn't exits", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Remove Link
        /// 
        /// Removes the link between 
        /// two stations if both stations exits 
        /// and there is a link to remove.
        ///  
        /// </summary>
        /// <param name="from">From station name</param>
        /// <param name="to">To station name</param>
        /// <param name="color">The line color</param>
        public void RemoveLink(T from, T to, ConsoleColor color)
        {

            int fromPos, toPos, edgePos;
            Vertex<T> FromVertex, ToVertex;

            //Find if the vertecies exists
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {
                //Get the vertecies
                FromVertex = Vertecies[fromPos];
                ToVertex = Vertecies[toPos];

                //I asume that the edge goes bothway so i wont need to check the other way around 
                //Check if the edge exists 
                if ((edgePos = Vertecies[fromPos].FindEdge(to, color)) > -1)
                {
                    //Remove the link from the FROM station
                    Vertecies[fromPos].Edges.RemoveAt(edgePos);

                    //Get the line positon for the adjacent vertex
                    edgePos = Vertecies[toPos].FindEdge(from, color);

                    //Remove the line from the adj vertex
                    Vertecies[toPos].Edges.RemoveAt(edgePos);

                    //Display message
                    Helper.MessageDisplay("\nJust Deleted the link between " + FromVertex.ToString() + " and " + ToVertex.ToString() + "\n", ConsoleColor.Green);
                }
                else
                {
                    //Display message
                    Helper.MessageDisplay("\nThe link between " + Vertecies[fromPos].ToString() + " and " + Vertecies[toPos].ToString() + " with color " + color + " doesn't exist\n", ConsoleColor.Red);
                }
            }
            else
            {
                //Display message
                Helper.MessageDisplay("\nOne of the station inputed doesn't exists\n", ConsoleColor.Red);
            }
        }
        #endregion

        #region ShortestPath

        /// <summary>
        /// ShortestPath
        /// 
        /// Will use the breadth-first method 
        /// to search and find the shortest path 
        /// between the two given vertecies 
        /// 
        /// </summary>
        /// <param name="from">The starting vertex</param>
        /// <param name="to">The end vertex</param>
        public Vertex<T> ShortestPath(T from, T to)
        {
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            int fromPos, toPos;
            bool found = false;

            for (int i = 0; i < Vertecies.Count; i++)
            {
                Vertecies[i].Visited = false;
                Vertecies[i].Parent = null;
            }

            //Check if the both of the stations exists
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {
                //Visit the starting point
                Vertex<T>
                    StartVertex,
                    EndVertex,
                    CurrentVertex = null;


                //Get the starting and ending vertex
                StartVertex = Vertecies[fromPos];
                EndVertex = Vertecies[toPos];

                //Start from layer one
                StartVertex.Layer = 0;
                //Visit the start vertex
                StartVertex.Visited = true;
                //Place it in the queue
                queue.Enqueue(StartVertex);
                //loop through the queue as long as there is items in 
                //the queue or we have found the 
                while (queue.Count > 0 && !found)
                {
                    //Get the next vertex
                    CurrentVertex = queue.Dequeue();

                    //check if we have found the vertex
                    if (CurrentVertex.Equals(EndVertex))
                    {
                        found = true;
                        return CurrentVertex;
                        //Make sure we flag it found so the loop stops
                    }

                    //Add other vertecies only if we haven't found what we are looking for
                    //if (!found)
                    //{

                    //Add all the adjacent vertecies
                    for (int i = 0; i < CurrentVertex.Edges.Count; i++)
                    {

                        //Check if the vertex hasnt been visited before
                        if (!CurrentVertex.Edges[i].AdjStation.Visited)
                        {

                            //Set the parent
                            CurrentVertex.GetAdjacentVertex(i).Parent = CurrentVertex;

                            //Change the layer
                            CurrentVertex.Edges[i].AdjStation.Layer = CurrentVertex.Layer++;

                            //Set the status to visited 
                            CurrentVertex.Edges[i].AdjStation.Visited = true;

                            //Enqueue the vertex
                            queue.Enqueue(CurrentVertex.Edges[i].AdjStation);
                        }
                    }
                    //}

                }
            }
            else
            {
                Helper.MessageDisplay("One of the stations doesn't exists\n", ConsoleColor.Red);
                return null;
            }

            return null;
        }
        #endregion

        #region Articulation points
        /// <summary>
        /// 
        /// Critical point will start the articulation point
        /// algorithm.
        /// 
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
            {
                if (Vertecies[i].Visited == false)
                {
                    CriticalPoints(Vertecies[i], ArticulationPoints);
                }
            }

            // Now articulationpoints list contains all articulation points, print them 
            Console.WriteLine();
            if (ArticulationPoints.Count == 0)
            {
                //There is no articulation points
                Helper.MessageDisplay("There is not articulation point in this graph", ConsoleColor.Red);
            }
            else
            {
                //Print all the articulation points
                for (int i = 0; i < size; i++)
                {
                    if (ArticulationPoints.Contains(Vertecies[i]))
                    {
                        Helper.MessageDisplay(Vertecies[i].ToString() + " Is an articulation point", ConsoleColor.Yellow);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// CriticalPoints
        /// 
        /// Runs DFS search on the graph and determines which of the points is a Articulatio point
        /// 
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

                    //Run and Explore the adj vertex
                    CriticalPoints(AdjVertex, ArticulationPoints);

                    // Check if there is a backedge from the adjacent vertex to something before 
                    CurrentVertex.LowLink = Math.Min(CurrentVertex.LowLink, AdjVertex.LowLink);

                    /*
                            ===To be an articulation point there are two cases.===
                            1. The vertex is the root
                                [ ] The vertex has no parent 
                                [ ] has more than 1 child
                            2. The vertex is not the root 
                                [ ] The vertex has a parent
                                [ ] The low link of the child is more than the discovery of itself
                     */

                    //The vertex is root 
                    if (CurrentVertex.Parent == null && children > 1)
                    {
                        ArticulationPoints.Add(CurrentVertex);
                    }

                    //The vertex is not root
                    if (CurrentVertex.Parent != null && AdjVertex.LowLink >= CurrentVertex.Discovered)
                    {
                        ArticulationPoints.Add(CurrentVertex);
                    }
                }
                else if (AdjVertex != CurrentVertex.Parent)
                {
                    // Update low value of current vertex for parent function calls. 
                    CurrentVertex.LowLink = Math.Min(CurrentVertex.LowLink, AdjVertex.Discovered);
                }
            }
        }

        #endregion

        #region HelperMethods

        /// <summary>
        /// Find Vertex
        /// 
        /// Will find if a vertex with the given name 
        /// exists in the vertecies list
        /// 
        /// </summary>
        /// <param name="name">Name of the vertex</param>
        /// <returns> { true }  </returns> if the vertex exists
        /// <returns> { false } </returns> if the vertex doesn't exists
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

        #region Printing Methods
        /// <summary>
        /// Print Graph 
        /// 
        /// This method will simply
        /// print the vertecies with its 
        /// edges and color
        /// From vertex to vertex and the color of the link
        /// 
        /// E.x
        /// [ A ] ==> [ B ] ==> Red 
        /// </summary>
        public void PrintGraph()
        {
            for (int i = 0; i < Vertecies.Count; i++)
            {
                if (Vertecies[i].HasEdges())
                {
                    for (int j = 0; j < Vertecies[i].Edges.Count; j++)
                    {
                        PrintLink(Vertecies[i], j);
                    }
                }
                else
                {
                    Helper.MessageDisplay("Station " + Vertecies[i].ToString() + " doesn't have any edges", ConsoleColor.Yellow);
                }
            }
        }


        /// <summary>
        /// PrintSPT
        /// 
        /// Prints the shortes path out
        /// 
        /// </summary>
        /// <param name="station">The end station</param>

        public void PrintSPT(T from, T to)
        {
            Vertex<T> station = ShortestPath(from, to);
            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            //Check if there was any paths
            if (station != null)
            {
                Helper.MessageDisplay("\nThe shortest path from station [ " + from + " ] to station [ " + to + " ] is: \n", ConsoleColor.Blue);
                //Print the path
                while (station.Parent != null)
                {
                    stack.Push(station);
                    station = station.Parent;
                }

                stack.Push(station);

                while (stack.Count > 0)
                {
                    Console.Write(stack.Pop().ToString() + "==>");
                }
                
                Console.WriteLine();
            }
            else
            {
                //Error message display
                Helper.MessageDisplay("No path was found", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// 
        /// Helper method to print out the link between two stations
        /// 
        /// </summary>
        /// <param name="From">The station to start from</param>
        /// <param name="toPos">the position of the edge in the edges list</param>
        private void PrintLink(Vertex<T> From, int toPos)
        {
            Console.Write("{0}", From.ToString());
            Console.ForegroundColor = From.Edges[toPos].Colour;
            Console.Write("---->");
            Console.ResetColor();
            Console.WriteLine("{0}", From.Edges[toPos].AdjStation.ToString());
        }
        #endregion

        #endregion
    }
}
