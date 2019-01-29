/*======================================================================================================================
|   A representation of a subway map using the Grahp algorithms
|   For this project i have used the adjacency list graph
|
|   Name:           Menu --> Class
|
|   Written by:     Vildan Hakanaj - January 2019
|
|   Written for:    COIS 3020 (Prof. Brian Patrick) Assignment #1 Trent University Winter 2019.
|
|   Purpose:        The menu of the system
|
|   Usage:          Used in the main program
|
|
======================================================================================================================*/
using System;
using System.Collections.Generic;

namespace SubwayMap
{
    class Menu : IMenu
    {
        #region Variable Declaration
        //The name of the option selected
        private string selection;
        //The index of the option
        private int index = 0;
        //The subway map
        private SubwayMap<char> map;
        //THe list of the options
        private List<string> menuItemsss = new List<string>(){
                /*[1]*/"Add Station",
                /*[2]*/"Add Link",
                /*[3]*/"Remove Link",
                /*[4]*/"Find the articulation points",
                /*[5]*/"Find shortest path between two stations",
                /*[6]*/"Print the Graph",
                /*[7]*/"Test1",
                /*[8]*/"Test2",
                /*[9]*/"Test3",
                /*[10]*/"Clear Graph",
                /*[11]*/"Exit",
            };
        #endregion

        public Menu(SubwayMap<char> map)
        {
            this.map = map;
        }
        
        #region MainMenuMethods
        /// <summary>
        /// ShowMenu
        /// 
        /// SHows the menu option to the console
        /// </summary>
        public void ShowMenu()
        {
            while (true)
            {
                selection = SelectMenu();
                if (selection.Length != 0)
                {
                    Console.CursorVisible = true;
                    ExecuteSelection(selection, map);
                }

            }
        }

        /// <summary>
        /// DeteckKey
        /// 
        /// Detects what key the user has pressed
        /// </summary>
        /// <returns></returns>
        public bool DetectKey()
        {
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == menuItemsss.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index == 0)
                {
                    index = menuItemsss.Count - 1;
                }
                else
                {
                    index--;
                }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ExecuteSelection
        /// 
        /// Executes the needed methods for the option
        /// 
        /// </summary>
        /// <param name="selection">The option name</param>
        /// <param name="map">The subway map</param>
        public void ExecuteSelection(string selection, SubwayMap<char> map)
        {
            switch (selection)
            {
                case "Add Station":
                    AddStation();
                    Helper.Wait();
                    break;
                case "Add Link":
                    AddLink();
                    Helper.Wait();
                    break;
                case "Remove Link":
                    RemoveLink();
                    Helper.Wait();
                    break;
                case "Find shortest path between two stations":
                    ShortestPath();
                    Helper.Wait();
                    break;
                case "Print the Graph":
                    Console.Clear();
                    map.PrintGraph();
                    Helper.Wait();
                    break;
                case "Find the articulation points":
                    map.CriticalPoints();
                    Helper.Wait();
                    break;
                case "Test1":
                    Test1(map);
                    Helper.Wait();
                    break;
                case "Test2":
                    Test2(map);
                    Helper.Wait();
                    break;
                case "Test3":
                    Test3(map);
                    Helper.Wait();
                    break;
                case "Clear Graph":
                    this.map = new SubwayMap<char>();
                    Console.WriteLine("Graph is cleared");
                    Helper.Wait();
                    break;
                case "Exit":
                    Environment.Exit(1);
                    break;
                default:
                    Environment.Exit(1);
                    break;
            }
        }
        public string SelectMenu()
        {
            Console.CursorVisible = false;
            for (int i = 0; i < menuItemsss.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(menuItemsss[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(menuItemsss[i]);
                }
            }

            if (DetectKey())
            {
                return menuItemsss[index];
            }

            Console.Clear();
            return "";
        }
        #endregion

        #region Options
        /// <summary>
        /// AddLink
        /// 
        /// Runs the method to add linksl
        /// </summary>
        public void AddLink()


        {
            char from = ' ', to = ' ';
            ConsoleColor linkColor = ConsoleColor.White;
            bool error = false;
            do
            {
                if (error)
                {
                    error = false;
                    Helper.MessageDisplay("Invalid input!", ConsoleColor.Red);
                    Helper.Wait();
                }
                //Get the first station
                Console.Write("\nEnter a starting station: ");
                if (!char.TryParse(Console.ReadLine(), out from))
                {
                    error = true;
                }

                //Get the second station
                Console.Write("\nEnd a end station to : ");
                if (!char.TryParse(Console.ReadLine(), out to))
                {
                    error = true;
                }

                //Get the color of the link
                Console.Write("\nEnter a color for the line: ");
                if (!Enum.TryParse(Console.ReadLine(), true, out linkColor))
                {
                    error = true;
                }

            } while (error == true);

            //Insert the link
            map.InsertLink(from, to, linkColor);
        }

        /// <summary>
        /// RemoveLink
        /// 
        /// Runs the method for removing links
        /// </summary>
        public void RemoveLink()
        {
            bool error = true;

            //Get the first station
            Console.Write("First Station: ");
            if (char.TryParse(Console.ReadLine(), out char fromStation))
            {
                //Get the second station
                Console.Write("Enter the second station: ");
                if (char.TryParse(Console.ReadLine(), out char toStation))
                {
                    //Get the color of the link
                    Console.Write("Enter the color of the link: ");
                    if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor linkColor))
                    {
                        error = false;
                        map.RemoveLink(fromStation, toStation, linkColor);
                    }
                }
            }

            //Check if there was an error to print the message
            if (error)
            {
                Helper.MessageDisplay("Invalid Input", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// AddStation
        /// 
        /// Run the methods to add stations
        /// 
        /// </summary>
        public void AddStation()
        {
            Console.Write("Enter a name for the station ==> ");
            if (char.TryParse(Console.ReadLine(), out char station))
            {
                Console.Clear();
                map.InsertStation(station);
            }
            else
            {
                Helper.MessageDisplay("\nInvalid input! Needs to be a character\n", ConsoleColor.Red);
            }
        }

        /// <summary>
        /// ShortestPath
        /// 
        /// Runs the shortest path algorithm 
        /// </summary>
        public void ShortestPath()
        {
            Console.Write("Enter the the starting station: ");
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            bool error = false;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            if (char.TryParse(Console.ReadLine(), out char fromStation))
            {
                Console.Write("Enter the endstation: ");

                if (char.TryParse(Console.ReadLine(), out char toStation))
                {
                    map.PrintSPT(fromStation, toStation);
                }
                else
                {
                    error = true;
                    Helper.MessageDisplay("Invalid input please enter a character\n", ConsoleColor.Red);
                }
            }
            else
            {
                error = true;
                Helper.MessageDisplay("Invalid input please enter a character", ConsoleColor.Red);
            }
        }
        #endregion

        #region Testing Cases

        /// <summary>
        /// Test 1
        /// 
        /// In this test you would expect the following.
        /// Articulation Point [ D, G ]
        /// Shortest Path A to J
        /// [ A, B, D, E, G, J ]
        /// 
        /// </summary>
        /// <param name="map"></param>
        public void Test1(SubwayMap<char> map)
        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('C');
            map.InsertStation('D');
            map.InsertStation('E');
            map.InsertStation('F');
            map.InsertStation('G');
            map.InsertStation('H');
            map.InsertStation('I');
            map.InsertStation('J');

            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'B', ConsoleColor.Yellow);

            map.InsertLink('A', 'C', ConsoleColor.Yellow);

            map.InsertLink('B', 'D', ConsoleColor.Green);

            map.InsertLink('C', 'D', ConsoleColor.Blue);

            map.InsertLink('D', 'E', ConsoleColor.Cyan);
            map.InsertLink('D', 'E', ConsoleColor.Green);

            map.InsertLink('D', 'F', ConsoleColor.Gray);

            map.InsertLink('E', 'G', ConsoleColor.Magenta);

            map.InsertLink('F', 'G', ConsoleColor.Magenta);
            map.InsertLink('F', 'G', ConsoleColor.Blue);

            map.InsertLink('G', 'H', ConsoleColor.Yellow);
            map.InsertLink('G', 'H', ConsoleColor.Cyan);

            map.InsertLink('G', 'I', ConsoleColor.Red);

            map.InsertLink('G', 'J', ConsoleColor.Red);


            Console.WriteLine("\nThe graph is populated with Test 1 sample\n\n");
            map.PrintGraph();
            Console.WriteLine("\n");
        }

        public void Test2(SubwayMap<char> map)

        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('C');
            map.InsertStation('D');
            map.InsertStation('E');
            map.InsertStation('F');
            map.InsertStation('G');
            map.InsertStation('H');

            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'G', ConsoleColor.Yellow);
            map.InsertLink('A', 'H', ConsoleColor.Blue);

            map.InsertLink('B', 'C', ConsoleColor.Red);

            map.InsertLink('B', 'D', ConsoleColor.Yellow);
            map.InsertLink('B', 'D', ConsoleColor.Green);

            map.InsertLink('C', 'F', ConsoleColor.Red);

            map.InsertLink('F', 'E', ConsoleColor.Red);
            map.InsertLink('F', 'E', ConsoleColor.Yellow);

            map.InsertLink('E', 'D', ConsoleColor.Yellow);
            map.InsertLink('E', 'D', ConsoleColor.Green);


            Console.WriteLine("\nThe graph is populated with Test 2 sample\n\n");
            map.PrintGraph();
        }
        public void Test3(SubwayMap<char> map)
        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('C');
            map.InsertStation('D');
            map.InsertStation('E');

            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'C', ConsoleColor.Yellow);

            map.InsertLink('B', 'C', ConsoleColor.Red);
            map.InsertLink('B', 'C', ConsoleColor.Blue);
            map.InsertLink('B', 'D', ConsoleColor.Yellow);

            map.InsertLink('D', 'C', ConsoleColor.Green);



            Console.WriteLine("\nThe graph is populated with Test 3 sample\n\n");
            map.PrintGraph();
            Console.WriteLine();
        }
        #endregion
    }
}
