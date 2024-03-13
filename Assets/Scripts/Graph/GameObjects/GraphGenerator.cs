using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStructure.Graph;

public class GraphGenerator : MonoBehaviour
{
    Graph<Node> graph;
    Dictionary<int, Node> nodeDict;
    Dictionary<int, Edge<Node>> edgeDict;

    public GameObject nodePrefab, edgePrefab;
    // Start is called before the first frame update
    void Start()
    {
        graph = new();
        nodeDict = graph.NodeDictionary;
        edgeDict = graph.AdjacencyList;
    }

    void GenerateNode()
    {
        int numStart = Random.Range(0, 5);
        for (int i = 0; i < numStart; i++)
        {
            Node node = new();
            node.Depth = 0;
            graph.AddNode(node);
        }
    }

    void DisplayGraph()
    {
        foreach (var item in nodeDict)
        {
            
        }
    }
}
