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

            menu.Test6(map);
            map.ShortestPath('A', 'E');
            Console.ReadKey();  
            //menu.ShowMenu();
        }
    }
}
