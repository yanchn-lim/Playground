using System.Collections.Generic;
using System.Linq;

namespace DataStructure.Graph
{
    //The node class where all the information of the node is declared
    public abstract class Node
    {
        public int Id { get; set; }
        public int Depth { get; set; }

        public Node()
        {

        }

        public Node(int id, int depth)
        {
            Id = id;
            Depth = depth;
        }
    }

}