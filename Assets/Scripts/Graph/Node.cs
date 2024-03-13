using System.Collections.Generic;
using System.Linq;
using System;
namespace DataStructure.Graph
{
    //The node class where all the information of the node is declared
    [Serializable]
    public class Node
    {
        public int Id { get; set; }
        public int Depth { get; set; }

        public Node()
        {

        }

        public Node(int depth)
        {
            Depth = depth;
        }
    }

}