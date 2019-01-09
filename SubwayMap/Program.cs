using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
