using System;

namespace SubwayMap
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            SubwayMap<char> map = new SubwayMap<char>();
            Menu menu = new Menu(map);
            menu.ShowMenu();
        }
    }
}
