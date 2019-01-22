using System;
using System.Collections.Generic;

namespace SubwayMap
{
    class Menu
    {
        #region Variable Declaration
        private string selection;
        private int index = 0;
        private SubwayMap<char> map;
        private List<string> menuItemsss = new List<string>(){
                /*[1]*/"Add Station",
                /*[2]*/"Add Link",
                /*[3]*/"Remove Station",
                /*[4]*/"Remove Link",
                /*[6]*/"Find the articulation points",
                /*[5]*/"Find shortest path between two stations",
                /*[5]*/"Print the Graph",
                /*[7]*/"Test1",
                /*[8]*/"Test2",
                /*[9]*/"Test3",
                /*[9]*/"Test4",
                /*[9]*/"Test5",
                /*[10]*/"Test6",
                /*[10]*/"Test7",
                /*[10]*/"TestSPT",
                /*[11]*/"Clear Graph",
                /*[12]*/"Exit",
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
        private bool DetectKey()
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
        private void ExecuteSelection(string selection, SubwayMap<char> map)
        {
            switch (selection)
            {
                case "Add Station":
                    char station;
                    Console.Write("Enter a name for the station ==> ");
                    if (char.TryParse(Console.ReadLine(), out station))
                    {
                        Console.Clear();
                        map.InsertStation(station);
                        Wait();
                    }
                    else
                    {
                        ChangeColor(ConsoleColor.Red);
                        Console.WriteLine("\nInvalid input! Needs to be a character\n");
                        Console.ResetColor();
                    }
                    break;

                case "Add Link":
                    char from = ' ', to = ' ';
                    string color = " ";
                    bool error = false;
                    do
                    {
                        if (error)
                        {
                            error = false;
                            ChangeColor(ConsoleColor.Red);
                            Console.WriteLine("Please make sure to enter a character for from and to and a string for color");
                            Wait();
                        }

                        Console.Write("\nEnter a starting station: ");
                        if (char.TryParse(Console.ReadLine(), out from))
                        {
                            Console.Write("\nEnd a end station to : ");
                            if (char.TryParse(Console.ReadLine(), out to))
                            {
                                Console.Write("\nEnter a color for the line: ");
                                color = Console.ReadLine();
                            }
                            else
                            {
                                error = true;
                            }
                        }
                        else
                        {
                            error = true;
                        }
                    } while (error == true);


                    map.InsertLink(from, to, color);
                    Wait();
                    break;
                case "Remove Link":
                    Console.Write("Enter the starting point of the link: ");
                    char fromStation = ' ';
                    char toStation = ' ';
                    error = false;
                    if (char.TryParse(Console.ReadLine(), out fromStation))
                    {
                        Console.Write("Enter the to station: ");

                        if (char.TryParse(Console.ReadLine(), out toStation))
                        {
                            Console.Write("Enter the color of the link: ");
                            color = Console.ReadLine();
                            map.RemoveLink(fromStation, toStation, color);
                        }
                        else
                        {
                            error = true;
                            ChangeColor(ConsoleColor.Red);
                            Console.WriteLine("Invalid input please enter a character");
                        }
                    }
                    else
                    {
                        ChangeColor(ConsoleColor.Red);
                        error = true;
                        Console.WriteLine("Invalid input please enter a character");
                    }
                    break;
                case "Find shortest path between two stations":
                    fromStation = ' ';
                    toStation = ' ';
                    error = false;

                    Console.Write("Enter the the starting station: ");
                    if (char.TryParse(Console.ReadLine(), out fromStation))
                    {
                        Console.Write("Enter the endstation: ");

                        if (char.TryParse(Console.ReadLine(), out toStation))
                        {
                            map.ShortestPath(fromStation, toStation);
                        }
                        else
                        {
                            error = true;
                            ChangeColor(ConsoleColor.Red);
                            Console.WriteLine("Invalid input please enter a character");
                        }
                    }
                    else
                    {
                        ChangeColor(ConsoleColor.Red);
                        error = true;
                        Console.WriteLine("Invalid input please enter a character");
                    }
                    break;
                case "Print the Graph":
                    Console.Clear();
                    map.PrintGraph();
                    Wait();
                    break;
                case "Find the articulation points":
                    map.CriticalPoints();
                    Wait();
                    break;
                case "Test1":
                    Test1(map);
                    Wait();
                    break;
                case "Test2":
                    Test2(map);
                    Wait();
                    break;
                case "Test3":
                    Test3(map);
                    Wait();
                    break;
                case "Test4":
                    Test4(map);
                    Wait();
                    break;
                case "Test5":
                    Test5(map);
                    Wait();
                    break;
                case "Test6":
                    Test6(map);
                    Wait();
                    break;
                case "Test7":
                    Test7(map);
                    Wait();
                    break;
                case "TestSPT":
                    TestSPT(map);
                    Wait();
                    break;
                case "Clear Graph":
                    this.map = new SubwayMap<char>();
                    Console.WriteLine("Graph is cleared");
                    Wait();
                    Console.Clear();
                    break;
                case "Exit":
                    Environment.Exit(1);
                    break;
                default:
                    break;
            }
        }
        private string SelectMenu()
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

        #region HelperMethods

        private void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        private void Wait()
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();
            Console.Clear();
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

            map.InsertLink('A', 'B', "red");
            map.InsertLink('A', 'C', "red");

            map.InsertLink('B', 'C', "red");

            map.InsertLink('C', 'D', "red");

            map.InsertLink('D', 'E', "red");


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

            map.InsertLink('A', 'B', "red");

            map.InsertLink('B', 'C', "red");

            map.InsertLink('C', 'D', "red");

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

            map.InsertLink('A', 'B', "red");
            map.InsertLink('A', 'C', "red");

            map.InsertLink('C', 'B', "white");

            map.InsertLink('B', 'D', "white");
            map.InsertLink('B', 'E', "white");

            map.InsertLink('F', 'D', "white");
            map.InsertLink('F', 'E', "white");

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

            map.InsertLink('A', 'B', "red");

            map.InsertLink('B', 'J', "red");
            map.InsertLink('B', 'C', "red");

            map.InsertLink('C', 'E', "red");
            map.InsertLink('C', 'F', "red");
            map.InsertLink('C', 'I', "red");
            map.InsertLink('C', 'D', "red");

            map.InsertLink('G', 'F', "red");
            map.InsertLink('G', 'H', "red");

            map.InsertLink('I', 'D', "red");
            map.InsertLink('I', 'H', "red");



            map.InsertLink('C', 'D', "red");
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
            map.InsertLink('A', 'B', "red");
            map.InsertLink('B', 'C', "red");
            map.InsertLink('C', 'A', "red");
            map.InsertLink('B', 'D', "red");
            map.InsertLink('B', 'E', "red");
            map.InsertLink('D', 'E', "red");
            map.InsertLink('G', 'B', "red");
            map.InsertLink('G', 'E', "red");

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
            map.InsertLink('A', 'B', "red");
            map.InsertLink('A', 'D', "red");
            map.InsertLink('A', 'C', "red");
            map.InsertLink('B', 'C', "red");
            map.InsertLink('C', 'D', "red");
            map.InsertLink('C', 'G', "red");
            map.InsertLink('C', 'E', "red");
            map.InsertLink('G', 'E', "red");
            map.InsertLink('E', 'F', "red");
            map.InsertLink('G', 'F', "red");
            map.InsertLink('F', 'J', "red");
            map.InsertLink('F', 'I', "red");
            map.InsertLink('F', 'H', "red");
            map.InsertLink('H', 'I', "red");
            map.InsertLink('I', 'J', "red");
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
            map.InsertLink('A', 'B', "red");
            map.InsertLink('B', 'C', "red");
            map.InsertLink('B', 'D', "red");
            map.InsertLink('C', 'E', "red");
            map.InsertLink('D', 'E', "red");

            Console.WriteLine("\nThe graph is populated with Test 5 sample\n\n");
            map.PrintGraph();
        }

        public void TestSPT(SubwayMap<char> map)
        {
            map.InsertStation('A');
            map.InsertStation('B');
            map.InsertStation('C');
            map.InsertStation('D');
            map.InsertStation('E');
            map.InsertStation('F');
            map.InsertLink('A', 'B', "red");
            map.InsertLink('A', 'C', "red");
            map.InsertLink('A', 'F', "red");
            map.InsertLink('B', 'C', "red");
            map.InsertLink('B', 'D', "red");
            map.InsertLink('D', 'C', "red");
            map.InsertLink('D', 'E', "red");
            map.InsertLink('E', 'F', "red");
            map.InsertLink('C', 'F', "red");
        }
        #endregion
    }
}
