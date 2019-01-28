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
            map.InsertStation('K');

            map.InsertLink('A', 'B', ConsoleColor.Yellow);
            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'C', ConsoleColor.Yellow);
            map.InsertLink('B', 'C', ConsoleColor.Red);
            map.InsertLink('B', 'D', ConsoleColor.Red);
            map.InsertLink('C', 'D', ConsoleColor.Green);
            map.InsertLink('C', 'D', ConsoleColor.Yellow);
            map.InsertLink('D', 'E', ConsoleColor.Blue);
            map.InsertLink('D', 'F', ConsoleColor.Blue);
            map.InsertLink('D', 'G', ConsoleColor.Blue);
            map.InsertLink('E', 'F', ConsoleColor.Cyan);
            map.InsertLink('E', 'G', ConsoleColor.Cyan);
            map.InsertLink('E', 'H', ConsoleColor.Cyan);
            map.InsertLink('E', 'H', ConsoleColor.Green);
            map.InsertLink('F', 'G', ConsoleColor.Magenta);
            map.InsertLink('F', 'H', ConsoleColor.Magenta);
            map.InsertLink('G', 'H', ConsoleColor.DarkCyan);
            map.InsertLink('H', 'I', ConsoleColor.White);
            map.InsertLink('H', 'I', ConsoleColor.DarkGray);
            map.InsertLink('H', 'J', ConsoleColor.White);
            map.InsertLink('H', 'K', ConsoleColor.White);

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

            map.InsertLink('A', 'B', ConsoleColor.Red);

            map.InsertLink('B', 'C', ConsoleColor.Red);

            map.InsertLink('C', 'D', ConsoleColor.Red);

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
            map.InsertStation('F');
            map.InsertStation('G');
            map.InsertStation('H');
            map.InsertStation('I');

            map.InsertLink('A', 'B', ConsoleColor.Yellow);
            map.InsertLink('A', 'F', ConsoleColor.Yellow);
            map.InsertLink('B', 'C', ConsoleColor.Blue);
            map.InsertLink('B', 'D', ConsoleColor.Blue);
            map.InsertLink('C', 'D', ConsoleColor.Blue);
            map.InsertLink('C', 'E', ConsoleColor.Green);
            map.InsertLink('D', 'E', ConsoleColor.Green);
            map.InsertLink('F', 'G', ConsoleColor.Green);
            map.InsertLink('F', 'H', ConsoleColor.Magenta);
            map.InsertLink('G', 'H', ConsoleColor.Magenta);
            map.InsertLink('G', 'I', ConsoleColor.Magenta);
            map.InsertLink('H', 'I', ConsoleColor.Cyan);

            Console.WriteLine("\nThe graph is populated with Test 3 sample\n\n");
            map.PrintGraph();
            Console.WriteLine();
        }
        #endregion
    }
}
