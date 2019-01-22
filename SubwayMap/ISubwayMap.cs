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
