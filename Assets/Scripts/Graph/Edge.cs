using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Graph
{
    public class Edge<N> where N : Node
    {
        public int Id { get; set; }
        public N SourceNode { get; set; }
        public N TargetNode { get; set; }


        public Edge()
        {

        }

        public Edge(int id, N source, N target)
        {
            Id = id;
            SourceNode = source;
            TargetNode = target;
        }
    }
}
