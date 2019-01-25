using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayMap
{
    class Helper
    {

        /// <summary>
        /// Helper function to make the user wait and press any key to continue
        /// </summary>
        public static void Wait()
        {
            Console.WriteLine("Press Any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Message Display
        /// 
        /// Will display the message
        /// given with the color
        /// 
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="color">The color we want the message to be</param>
        public static void MessageDisplay(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
