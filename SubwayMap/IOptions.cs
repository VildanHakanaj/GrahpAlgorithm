using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayMap
{
    interface IOptions
    {
        void AddLink();
        void RemoveLink();
        void AddStation();
        void ShortestPath();
    }
}
