using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataStructure.Graph
{
    public class Graph<N> where N : Node
    {
        //node id, connected nodes
        //public Dictionary<int, HashSet<N>> AdjacencyList = new();

        public Dictionary<int, Edge<N>> AdjacencyList = new();

        //list of all nodes
        public Dictionary<int,N> NodeDictionary = new();

        int NodeIdCounter = 0;
        int EdgeIdCounter = 0;

        public int NodeCount = 0;
        public int EdgeCount = 0;
        public int MaxDepth;
        public Graph()
        {
        }

        #region Adding into graph
        /// <summary>
        /// Adds the node into a node list and starts an entry in the adjacency list.
        /// </summary>
        /// <param name="node">Node to be added</param>
        public void AddNode(N node)
        {
            // this method adds the node to the node list, adjacency list and edge list;
            node.Id = NodeIdCounter;
            NodeDictionary[node.Id] = node;
            NodeIdCounter++;
            NodeCount = NodeDictionary.Count;
            //AdjacencyList[node.Id] = new HashSet<N>();

            if (node.Depth > MaxDepth)
                MaxDepth = node.Depth;
        }

        /// <summary>
        /// Adds a new edge into the list
        /// </summary>
        /// <param name="edge">Edge to add</param>
        public void AddEdge(Edge<N> edge)
        {
            //handle errors
            if (edge.SourceNode == null || edge.TargetNode == null)
            {
                Debug.LogWarning("Graph : Source or target node is null.");
                return;
            }

            if (AdjacencyList.TryGetValue(edge.Id,out Edge<N> e))
            {
                Debug.LogWarning("Graph : Edge already exists");
                return;
            }

            AdjacencyList[edge.Id] = edge; //add to the list
            EdgeIdCounter++; //increment edge count
            EdgeCount = AdjacencyList.Count;
        }
        #endregion

        /// <summary>
        /// Remove every instance of this node in the graph
        /// </summary>
        /// <param name="node">Node to be removed</param>
        public void RemoveNode(N node)
        {
            if(node == null)
            {
                Debug.LogWarning("Graph : Node is null");
                return;
            }

            if (!NodeDictionary.TryGetValue(node.Id, out N removedNode))
            {
                // Node not found in the node dictionary
                Debug.LogWarning("Node not found in the graph.");
                return;
            }

            List<int> listOfAffectedEdges = AdjacencyList
                                            .Where(item => item.Value.SourceNode == node || item.Value.TargetNode == node)
                                            .Select(item => item.Key)
                                            .ToList();

            //removing instances of the removed node from the edge list
            foreach (var item in listOfAffectedEdges)
            {
                AdjacencyList.Remove(item);
            }

            //removing the node from the adjacency list
            NodeDictionary.Remove(node.Id);
        }

        #region Getting from graph
        /// <summary>
        /// Get the node from the dictionary via the ID
        /// </summary>
        /// <param name="id">ID to be queried</param>
        /// <returns>Requested node</returns>
        public N GetNode(int id)
        {
            if (NodeDictionary.TryGetValue(id, out N node))
                return node;

            return null;
        }

        /// <summary>
        /// Get all the nodes connect to the query node
        /// </summary>
        /// <param name="id">Query node id</param>
        /// <returns>Connected nodes</returns>
        public HashSet<N> GetConnected(int id)
        {
            HashSet<N> connected = AdjacencyList
                                   .Where(item => item.Value.SourceNode == GetNode(id))
                                   .Select(item => item.Value.TargetNode)
                                   .ToHashSet();

            if (connected.Count == 0)
                return connected;

            //returns null if there isnt an entry
            Debug.LogWarning("Graph : No entry found!");
            return null;
        }

        /// <summary>
        /// Get all the nodes connect to the query node
        /// </summary>
        /// <param name="id">Query node</param>
        /// <returns>Connected nodes</returns>
        public HashSet<N> GetConnected(N node)
        {
            HashSet<N> connected = AdjacencyList
                                   .Where(item => item.Value.SourceNode == node)
                                   .Select(item => item.Value.TargetNode)
                                   .ToHashSet();

            if (connected.Count == 0)
                return connected;

            //returns null if there isnt an entry
            Debug.LogWarning("Graph : No entry found!");
            return null;
        }

        /// <summary>
        /// Returns the nodes in the next depth that are connected to the node being passed in.
        /// </summary> 
        /// <param name="id">Node to be queried</param>
        /// <returns>Set of connected nodes in next depth</returns>
        public HashSet<N> GetConnectedNextDepth(int id)
        {
            HashSet<N> connected = AdjacencyList
                                   .Where(item => item.Value.SourceNode.Depth == GetNode(id).Depth + 1 && item.Value.SourceNode == GetNode(id))
                                   .Select(item => item.Value.TargetNode)
                                   .ToHashSet();

            if (connected.Count == 0)
                return connected;

            //returns null if there isnt an entry
            Debug.LogWarning("Graph : No entry found!");
            return null;
        }

        /// <summary>
        /// Returns all the nodes in the same depth.
        /// </summary>
        /// <param name="depth">Depth to be queried</param>
        /// <returns>Nodes in queried depth.</returns>
        public Dictionary<int,N> GetNodesInDepth(int depth)
        {
            Dictionary<int,N> nodes = NodeDictionary.Where(item => item.Value.Depth == depth).ToDictionary(item => item.Key, item => item.Value);
            return nodes;
        }

        public List<N> GetNodesInDepthList(int depth)
        {
            List<N> nodes = NodeDictionary
                            .Where(item => item.Value.Depth == depth)
                            .Select(item=> item.Value)
                            .ToList();
            return nodes;
        }
        #endregion

        public bool IsConnectedGraph()
        {
            HashSet<N> visited = new();

            List<N> nodeFirstDepth = GetNodesInDepthList(0);
            Search(nodeFirstDepth.First(), visited);

            return visited.Count == NodeDictionary.Count;
        }

        public void Search(N node, HashSet<N> visited)
        {
            if(node == null)
            {
                Debug.LogWarning("Graph : Query node is null!");
                return;
            }

            // Null check for visited set
            if (visited == null)
            {
                // Handle null visited set, throw an exception or create a new HashSet
                Debug.LogWarning("Graph : Visited set is null. Creating a new HashSet.");
                visited = new HashSet<N>();
            }

            if (!visited.Contains(node))
            {
                visited.Add(node);

                foreach (var item in GetConnected(node))
                {
                    Search(item, visited);
                }
            }
            
        }
    }

}

