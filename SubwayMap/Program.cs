/*======================================================================================================================
|   SubwayMap
|
|   Name:           Main Class
|
|   Written by:     Vildan Hakanaj - January 2019
|
|   Written for:    COIS 3320 (Prof. Brian Patrick)Assignment 1 - Trent University winter 2019.
|
|   Purpose:        Using graphs algorithms to recreate a subwaymap and to be able to insert sations links and utilizing
|                   BDF and DFS to search the graph and find shortest path and articulation points.
|
|
|   usage:          Used to create a subway map model used by the menu 
|
|   Subroutines/libraries required:
|                                  System;
|                                  System Collection Generic 
|
======================================================================================================================*/
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
