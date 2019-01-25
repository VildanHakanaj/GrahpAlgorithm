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
                /*[10]*/"Test4",
                /*[11]*/"Test5",
                /*[12]*/"Test6",
                /*[13]*/"Test7",
                /*[14]*/"Root test",
                /*[15]*/"TestSPT",
                /*[16]*/"Clear Graph",
                /*[17]*/"Exit",
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
                    break;
                case "Add Link":
                    AddLink();
                    break;
                case "Remove Link":
                    RemoveLink();
                    break;
                case "Find shortest path between two stations":
                    ShortestPath();
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
                case "Test4":
                    Test4(map);
                    Helper.Wait();
                    break;
                case "Test5":
                    Test5(map);
                    Helper.Wait();
                    break;
                case "Test6":
                    Test6(map);
                    Helper.Wait();
                    break;
                case "Test7":
                    Test7(map);
                    Helper.Wait();
                    break;
                case "Root test":
                    RootTest(map);
                    Helper.Wait();
                    break;

                case "TestSPT":
                    TestSPT(map);
                    Helper.Wait();
                    break;
                case "Clear Graph":
                    this.map = new SubwayMap<char>();
                    Console.WriteLine("Graph is cleared");
                    Helper.Wait();
                    Console.Clear();
                    break;
                case "Exit":
                    Environment.Exit(1);
                    break;
                default:
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
                if (char.TryParse(Console.ReadLine(), out to))
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
            Helper.Wait();
        }

        public void RemoveLink()
        {
            char toStation = ' ';
            bool error = true;

            //Get the first station
            Console.Write("First Station: ");
            if (char.TryParse(Console.ReadLine(), out char fromStation))
            {
                //Get the second station
                Console.Write("Enter the second station: ");
                if (char.TryParse(Console.ReadLine(), out toStation) && !true)
                {
                    //Get the color of the link
                    Console.Write("Enter the color of the link: ");
                    if (Enum.TryParse(Console.ReadLine(), true, out ConsoleColor linkColor) && !true)
                    {
                        error = false;
                        map.RemoveLink(fromStation, ' ', linkColor);
                    }
                }
            }

            //Check if there was an error to print the message
            if (true)
            {
                Helper.MessageDisplay("Invalid Input", ConsoleColor.Red);
                Helper.Wait();
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
                Helper.Wait();
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
                    Helper.Wait();
                }
            }
            else
            {
                error = true;
                Helper.MessageDisplay("Invalid input please enter a character", ConsoleColor.Red);
                Helper.Wait();
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

            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'C', ConsoleColor.Red);

            map.InsertLink('B', 'C', ConsoleColor.Red);

            map.InsertLink('C', 'D', ConsoleColor.Red);

            map.InsertLink('D', 'E', ConsoleColor.Red);


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

            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'C', ConsoleColor.Red);

            map.InsertLink('C', 'B', ConsoleColor.Red);

            map.InsertLink('B', 'D', ConsoleColor.Red);
            map.InsertLink('B', 'E', ConsoleColor.Red);

            map.InsertLink('F', 'D', ConsoleColor.Red);
            map.InsertLink('F', 'E', ConsoleColor.Red);

            Console.WriteLine("\nThe graph is populated with Test 3 sample\n\n");
            map.PrintGraph();
            Console.WriteLine();
        }

        public void Test4(SubwayMap<char> map)
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

            map.InsertLink('B', 'J', ConsoleColor.Red);
            map.InsertLink('B', 'C', ConsoleColor.Red);

            map.InsertLink('C', 'E', ConsoleColor.Red);
            map.InsertLink('C', 'F', ConsoleColor.Red);
            map.InsertLink('C', 'I', ConsoleColor.Red);
            map.InsertLink('C', 'D', ConsoleColor.Red);

            map.InsertLink('G', 'F', ConsoleColor.Red);
            map.InsertLink('G', 'H', ConsoleColor.Red);

            map.InsertLink('I', 'D', ConsoleColor.Red);
            map.InsertLink('I', 'H', ConsoleColor.Red);



            map.InsertLink('C', 'D', ConsoleColor.Red);
            Console.WriteLine("\nThe graph is populated with Test 4 sample\n\n");
            map.PrintGraph();
            Console.WriteLine();
        }

        public void Test5(SubwayMap<char> map)
        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('C');
            map.InsertStation('D');
            map.InsertStation('E');
            map.InsertStation('G');
            map.InsertStation('F');
            map.InsertLink('A', 'B', ConsoleColor.Yellow);
            map.InsertLink('B', 'C', ConsoleColor.Green);
            map.InsertLink('C', 'A', ConsoleColor.Blue);
            map.InsertLink('B', 'D', ConsoleColor.Cyan);
            map.InsertLink('B', 'E', ConsoleColor.Magenta);
            map.InsertLink('D', 'E', ConsoleColor.DarkBlue);
            map.InsertLink('G', 'B', ConsoleColor.DarkYellow);
            map.InsertLink('G', 'E', ConsoleColor.DarkRed);

            Console.WriteLine("\nThe graph is populated with Test 5 sample\n\n");
            map.PrintGraph();
        }

        public void Test6(SubwayMap<char> map)
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
            map.InsertLink('A', 'D', ConsoleColor.Red);
            map.InsertLink('A', 'C', ConsoleColor.Red);
            map.InsertLink('B', 'C', ConsoleColor.Blue);
            map.InsertLink('C', 'D', ConsoleColor.Green);
            map.InsertLink('C', 'G', ConsoleColor.Green);
            map.InsertLink('C', 'E', ConsoleColor.Green);
            map.InsertLink('G', 'E', ConsoleColor.Yellow);
            map.InsertLink('E', 'F', ConsoleColor.Magenta);
            map.InsertLink('G', 'F', ConsoleColor.Red);
            map.InsertLink('F', 'J', ConsoleColor.Cyan);
            map.InsertLink('F', 'I', ConsoleColor.Cyan);
            map.InsertLink('F', 'H', ConsoleColor.Cyan);
            map.InsertLink('H', 'I', ConsoleColor.DarkGreen);
            map.InsertLink('I', 'J', ConsoleColor.DarkGreen);
            Console.WriteLine("\nThe graph is populated with Test 6 sample\n\n");
            map.PrintGraph();
        }

        public void Test7(SubwayMap<char> map)
        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('D');
            map.InsertStation('C');
            map.InsertStation('E');
            map.InsertLink('A', 'B', ConsoleColor.Blue);
            map.InsertLink('B', 'C', ConsoleColor.Blue);
            map.InsertLink('B', 'D', ConsoleColor.Blue);
            map.InsertLink('C', 'E', ConsoleColor.Red);
            map.InsertLink('D', 'E', ConsoleColor.Blue);

            Console.WriteLine("\nThe graph is populated with Test 5 sample\n\n");
            map.PrintGraph();
        }

        public void RootTest(SubwayMap<char> map)
        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('C');
            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'C', ConsoleColor.Blue);
        }

        public void TestSPT(SubwayMap<char> map)
        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('C');
            map.InsertStation('D');
            map.InsertStation('E');
            map.InsertStation('F');
            map.InsertLink('A', 'B', ConsoleColor.Red);
            map.InsertLink('A', 'C', ConsoleColor.Red);
            map.InsertLink('A', 'F', ConsoleColor.Red);
            map.InsertLink('B', 'C', ConsoleColor.Red);
            map.InsertLink('B', 'D', ConsoleColor.Red);
            map.InsertLink('D', 'C', ConsoleColor.Red);
            map.InsertLink('D', 'E', ConsoleColor.Red);
            map.InsertLink('E', 'F', ConsoleColor.Red);
            map.InsertLink('C', 'F', ConsoleColor.Red);
        }
        #endregion
    }
}
