/*======================================================================================================================
|   A representation of a subway map using the Grahp algorithms
|
|   Name:           Helper --> Class
|
|   Written by:     Vildan Hakanaj - January 2019
|
|   Written for:    COIS 3020 (Prof. Brian Patrick) Assignment #1 Trent University Winter 2019.
|
|   Purpose:        This class holds methods that i use accross the project. 
|                   
|       
======================================================================================================================*/

using System;

namespace SubwayMap
{
    class Helper
    {

        /// <summary>
        /// Wait
        /// 
        /// Helper function to make the user wait and press any key to continue
        /// 
        /// </summary>
        public static void Wait()
        {
            Console.WriteLine("Press Any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Message Display
        /// Helper Function to display the message with the color
        /// 
        /// </summary>
        /// <param name="message">The message to be displayed</param>
        /// <param name="color">The color the message is going be</param>
        public static void MessageDisplay(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
