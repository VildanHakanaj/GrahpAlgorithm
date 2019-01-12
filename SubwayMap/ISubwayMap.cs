namespace SubwayMap
{
    interface ISubwayMap<T>
    {
        void InsertStation(T name);
        void InsertLink(T from, T to, string color);
        void RemoveLink(T from, T to, string color);
    }
}
