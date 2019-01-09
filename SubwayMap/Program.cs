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

            SubwayMap<char> subway = new SubwayMap<char>();
            ShowMenu();
            int lineNumber;
            do
            {
                 lineNumber = Convert.ToInt32(Console.ReadLine());
                Answer(lineNumber, subway);
            } while (lineNumber != 6);

            subway.PrintGraph();

            Console.ReadKey();

        }

        public static void ShowMenu()
        {
            Console.WriteLine("Enter the line number\n\n");
            Console.WriteLine("1) Insert Station");
            Console.WriteLine("2) Remove Station");
            Console.WriteLine("3) Insert Link");
            Console.WriteLine("4) Remove Link");
            Console.WriteLine("5) Exit");
        }

        public static void Answer(int lineNumber, SubwayMap<char> subway)
        {

            switch (lineNumber)
            {
                case 1:
                    Console.Write("Enter a station name: ");
                    char input = Convert.ToChar(Console.ReadLine());
                    subway.InsertStation(input);
                    break;
                case 2:
                    break;
                case 3:
                    char from, to;
                    string color;
                    Console.Write("Enter from: ");
                    from = Convert.ToChar(Console.ReadLine());
                    Console.Write("\nEnter to: ");
                    to = Convert.ToChar(Console.ReadLine());
                    Console.Write("\nEnter the link color: ");
                    color = Console.ReadLine();
                    subway.InsertLink(from, to, color);
                    break;
                case 4:
                    break;
                case 5:
                    Console.Clear();
                    ShowMenu();
                    break;
                case 6:
                    lineNumber = 6;
                    break;
                case 7:
                    subway.PrintGraph();
                    break;
                default:
                    Console.WriteLine("Please enter a correct line number");
                    break;
            }
        }
    }
}
