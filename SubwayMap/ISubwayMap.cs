/*======================================================================================================================
|   SubwayMap
|
|   Name:           ISubwayMap Interface
|
|   Written by:     Vildan Hakanaj - January 2019
|
|   Written for:    COIS 3320 (Prof. Brian Patrick)Assignment 1 - Trent University winter 2019.
|
|   Purpose:        It is implemented by the SubwayMap Class to have the methods of this interface
|                   
|
|
|   usage:          Used by the SubwayMap 
|
|   Subroutines/libraries required:
======================================================================================================================*/
namespace SubwayMap
{
    interface ISubwayMap<T>
    {
        void InsertStation(T name);
        void InsertLink(T from, T to, string color);
        void RemoveLink(T from, T to, string color);
    }
}
