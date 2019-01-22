﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SubwayMap
{
    internal class SubwayMap<T> : ISubwayMap<T>
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
                //Add the station
                Vertecies.Add(new Vertex<T>(name));

                //Print a message letting the user know it was successfull
                MessageDisplay("Just inserted station [ " + name + " ] into the graph\n", ConsoleColor.Green);
            }
            else
            {
                //Print out the error
                MessageDisplay("Station " + name + " already exists\n", ConsoleColor.Red);
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
        public void InsertLink(T from, T to, string color)
        {
            //For the positions
            int fromPos, toPos;

            //Check if the stations dont exits
            if ((fromPos = FindVertex(from)) > -1 && (Vertecies[fromPos].FindEdge(to, color)) > -1)
            {
                //Get the index of the to vertex
                toPos = FindVertex(to);
                //insert the link both ways since its a undirected graph
                Vertecies[fromPos].Edges.Add(new Edge<T>(Vertecies.ElementAt(toPos), color));
                Vertecies[toPos].Edges.Add(new Edge<T>(Vertecies.ElementAt(fromPos), color));
            }
            else
            {

                MessageDisplay("The link with color " + color + " from " + from + " to " + to + " already exists\n", ConsoleColor.Red);
                MessageDisplay(" or one of the station is non existen", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Remove Link
        /// 
        /// Will remove a link between 
        /// two stations if it only exits 
        /// 
        /// </summary>
        /// <param name="from">From station name</param>
        /// <param name="to">To station name</param>
        /// <param name="color">The line color</param>
        public void RemoveLink(T from, T to, string color)
        {

            int fromPos, toPos, edgePos;
            Vertex<T> FromVertex, ToVertex;

            //Find if the vertecies exists
            if ((fromPos = FindVertex(from)) > -1 && (toPos = FindVertex(to)) > -1)
            {
                //Get the vertecies for readability
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
                    MessageDisplay("\nJust Deleted the link between " + FromVertex.Name + " and " + ToVertex.Name + "\n", ConsoleColor.Green);
                }
                else
                {
                    //Display message
                    MessageDisplay("\nThe link between " + Vertecies[fromPos].Name + " and " + Vertecies[toPos].Name + " with color " + color + " doesn't exist\n", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue!");
                    Console.ReadKey();
                }
            }
            else
            {
                //Display message
                MessageDisplay("\nOne of the station inputed doesn't exists\n", ConsoleColor.Red);

                Console.WriteLine("Press any key to continue!");
                Console.ReadKey();
            }
        }
        #endregion

        #region ShortestPath
        ///::::TDODO::::
        ///[ ] Keep track of the parent color
        ///[ ] 
        /// 
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
        public void ShortestPath(T from, T to)
        {
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            int fromPos, toPos;
            bool found = false;

            for (int i = 0; i < Vertecies.Count; i++)
            {
                Vertecies[i].Visited = false;
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
                while (queue.Count > 0 || !found)
                {
                    //Get the next vertex
                    CurrentVertex = queue.Dequeue();

                    //check if we have found the vertex
                    if (CurrentVertex.Equals(EndVertex))
                    {

                        Console.WriteLine("We found the end vertex at layer {0}\n", CurrentVertex.Layer);

                        //Print the path
                        PrintSPT(CurrentVertex);

                        //Make sure we flag as found so the loop stops
                        found = true;
                    }

                    //Add other vertecies only if we haven't found what we are looking for
                    if (!found)
                    {

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
                    }
                }
            }
            else
            {
                MessageDisplay("One of the stations doesn't exists\n", ConsoleColor.Red);
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
                //Get the adjacent vertex of the current vertex
                Vertex<T> AdjVertex = CurrentVertex.Edges[i].AdjStation;  

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

                    // (2) If Current vertex is not root and low link value of one of its child 
                    // is more than discovery value of Current vertex.  
                    if (CurrentVertex.Parent != null && AdjVertex.LowLink >= CurrentVertex.Discovered)
                    {
                        ArticulationPoints.Add(CurrentVertex);
                    }
                }
                else if (AdjVertex != CurrentVertex.Parent)
                {
                    // Update low value of CurrentVertex for parent function calls. 
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

        #region Print Methods

        /// <summary>
        /// Print Graph 
        /// 
        /// This method will simply
        /// print the vertecies with its 
        /// edges and connections
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
                        Console.WriteLine("[ {0} ] --> [ {1} ] ==> {2}", Vertecies[i].Name, Vertecies[i].Edges[j].AdjStation.Name, Vertecies[i].Edges[j].Colour);
                    }
                }
                else
                {
                    MessageDisplay("Station " + Vertecies[i].Name + " doesn't have any edges", ConsoleColor.Yellow);
                }
            }
        }

        /// <summary>
        /// Message Display
        /// 
        /// Will display the message
        /// given with the color
        /// 
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="color">The color we want the message to be</param>
        private void MessageDisplay(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }


        /// <summary>
        /// Is going to be used to print the vertex parents 
        /// </summary>
        /// <param name="station">The end station</param>

        private void PrintSPT(Vertex<T> station)
        {
            //Create a list for the to be stored
            List<T> names = new List<T>();
            //Call the recursive method passing the station name
            PrintSPT(station, names);
            //Reverse the order of the stations
            names.Reverse();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The shortest path to station [ {0} ] from station [ {1} ] is: \n", names[0], names[names.Count - 1]);
            Console.ResetColor();

            for (int i = 0; i < names.Count; i++)
            {
                Console.Write("[{0}] ===> ", names.ElementAt(i));
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Using recursion to backtrack the 
        /// path from the end point to the 
        /// start point
        /// </summary>
        /// <param name="station"></param>
        /// <param name="names"></param>
        private void PrintSPT(Vertex<T> station, List<T> names)
        {
            names.Add(station.Name);
            if (station.Parent != null)
            {
                PrintSPT(station.Parent, names);
            }
        }
        #endregion
        #endregion
    }
}
