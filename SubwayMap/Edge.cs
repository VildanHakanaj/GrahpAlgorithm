/*======================================================================================================================
|   A representation of a subway map using the Grahp algorithms
|
|   Name:           Edge --> Class
|
|   Written by:     Vildan Hakanaj - January 2019
|
|   Written for:    COIS 3020 (Prof. Brian Patrick) Assignment #1 Trent University Winter 2019.
|
|   Purpose:        Represents the edge between two stations
|
|   Usage:          Used in the subway class
|
|
======================================================================================================================*/
using System;
namespace SubwayMap
{
    class Edge<T>
    {
        public Vertex<T> AdjStation { get; set; }
        public ConsoleColor Colour { get; set; }

        public Edge(Vertex<T> AdjStation, ConsoleColor Colour)
        {
            this.AdjStation = AdjStation;
            this.Colour = Colour;
        }
    }
}
