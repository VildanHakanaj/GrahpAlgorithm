/*======================================================================================================================
|   A representation of a subway map using the Grahp algorithms
|   For this project i have used the adjacency list graph
|
|   Name:           ISubwayMap --> Interface
|
|   Written by:     Vildan Hakanaj - January 2019
|
|   Written for:    COIS 3020 (Prof. Brian Patrick) Assignment #1 Trent University Winter 2019.
|
|   Purpose:        To define what a subwayMap is
|
|   Usage:          Implemented by the SubayMap
|
|
======================================================================================================================*/

using System;
namespace SubwayMap
{
    interface ISubwayMap<T>
    {
        void InsertStation(T name);
        void InsertLink(T from, T to, ConsoleColor color);
        void RemoveLink(T from, T to, ConsoleColor color);
    }
}
