using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DataStructure.Graph;

[CustomEditor(typeof(Graph<Node>))]
public class GraphEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Custom editor GUI elements for your graph
        EditorGUILayout.LabelField("Graph Settings", EditorStyles.boldLabel);
        // Add buttons, sliders, etc. for configuring the graph

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Graph Visualization", EditorStyles.boldLabel);
        // Add options to visualize the graph

        if (GUILayout.Button("Visualize Graph"))
        {
            // Logic to visualize the graph in the Scene view or a custom editor window
        }
    }

    private void OnSceneGUI()
    {
        // Custom drawing logic for visualizing the graph in the Scene view
    }
}
