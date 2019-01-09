using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayMap
{
    class Menu
    {
        List<string> menuItemsss = new List<string>(){
                /*[1]*/"Add Station",
                /*[2]*/"Add Link",
                /*[3]*/"Remove Station",
                /*[4]*/"Remove Link",
                /*[5]*/"Print the Graph",
                /*[6]*/"Clear Menu",
                /*[7]*/"Exit",
            };
        string selection;
        int index = 0;

        public void ShowMenu(SubwayMap<char> map)
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
                        wait();
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
                            Console.WriteLine("Please make sure to ente a character for from and to and a string for color");
                            wait();
                        }

                        Console.Write("\nEnter a starting station: ");
                        if (char.TryParse(Console.ReadLine(), out from))
                        {
                            Console.Write("\nEnd a end station: to :");
                            if (char.TryParse(Console.ReadLine(), out to))
                            {
                                Console.Write("\nEnter a color for the line");
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
                    break;
                case "Remove Station":
                    Console.WriteLine("Selected the third option\n");
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
                case "Print the Graph":
                    Console.Clear();
                    map.PrintGraph();
                    wait();
                    break;
                case "Clear Menu":
                    Console.WriteLine("Selected the sixth option");
                    break;
                case "Exit":
                    Environment.Exit(1);
                    break;
                default:
                    break;
            }
        }

        private void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        private void wait()
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();
            Console.Clear();
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
    }
}
