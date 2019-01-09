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
                        map.InsertStation(station);
                        Console.Clear();
                        Console.WriteLine("Just inserted station [ {0} ] into the graph", station);
                        wait();
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input! Needs to be a character\n");
                    }
                    break;

                case "Add Link":
                    Console.WriteLine("Selected the second option\n");
                    break;
                case "Remove Station":
                    Console.WriteLine("Selected the third option\n");
                    break;
                case "Remove Link":
                    Console.WriteLine("Selected the fourth option\n");
                    break;
                case "Print the Graph":
                    Console.Clear();
                    map.PrintGraph();
                    Console.WriteLine("s", 3);
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
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
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
