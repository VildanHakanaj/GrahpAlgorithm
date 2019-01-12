namespace SubwayMap
{
    class Edge<T>
    {
        public Vertex<T> AdjStation { get; set; }
        public string Colour { get; set; }

        public Edge(Vertex<T> AdjStation, string Colour)
        {
            this.AdjStation = AdjStation;
            this.Colour = Colour;
        }
    }
}
