using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class NodeUtilities {

	public static void CreateNewGraph(string graphName)
    {
        NodeGraph currentGraph = (NodeGraph)ScriptableObject.CreateInstance<NodeGraph>();
        if(currentGraph != null)
        {
            currentGraph.graphName = graphName;
            currentGraph.InitGraph();

            AssetDatabase.CreateAsset(currentGraph, "Assets/Dialogue System/Database/" + graphName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            NodeGraphWindow currentWindow = (NodeGraphWindow)EditorWindow.GetWindow<NodeGraphWindow>();

            if(currentWindow != null)
            {
                currentWindow.currentGraph = currentGraph;
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Graph Message", "No se pudo crear el Grapho", "OK");
        }
    }

    public static void LoadGraph()
    {
        NodeGraph currentGraph = null;
        string graphPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/Dialogue System/Database/", "");
        
        int appPathLen = Application.dataPath.Length;
        string finalPath = graphPath.Substring(appPathLen - 6);

        currentGraph = (NodeGraph)AssetDatabase.LoadAssetAtPath(finalPath, typeof(NodeGraph));

        if(currentGraph != null)
        {
            NodeGraphWindow currentWindow = (NodeGraphWindow)EditorWindow.GetWindow<NodeGraphWindow>();

            if (currentWindow != null)
            {
                currentWindow.currentGraph = currentGraph;
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Graph Message", "No se pudo cargar el Grapho seleccionado", "OK");
        }
    }

    public static void UnloadGraph()
    {
        NodeGraphWindow currentWindow = (NodeGraphWindow)EditorWindow.GetWindow<NodeGraphWindow>();

        if (currentWindow != null)
        {
            currentWindow.currentGraph = null;
        }
    }

    public static void CreateNode(NodeGraph currentGraph, NodeType nodeType, Vector2 mousePos)
    {
        if(currentGraph != null)
        {
            BaseNode currentNode = null;
            switch (nodeType)
            {
                case NodeType.Dialogue:
                    currentNode = (DialogueNode)ScriptableObject.CreateInstance<DialogueNode>();
                    currentNode.nodeName = "Dialogue Node";
                    break;
                case NodeType.Condicional:
                    currentNode = (ConditionNode)ScriptableObject.CreateInstance<ConditionNode>();
                    currentNode.nodeName = "Conditional Node";
                    break;
                case NodeType.Question:
                    currentNode = (QuestionNode)ScriptableObject.CreateInstance<QuestionNode>();
                    currentNode.nodeName = "Question Node";
                    break;
                case NodeType.Answer:
                    currentNode = (AnswerNode)ScriptableObject.CreateInstance<AnswerNode>();
                    currentNode.nodeName = "Answer Node";
                    break;
                default:
                    break;
            }
            if(currentNode != null)
            {
                currentNode.name = currentNode.nodeName;
                currentNode.InitNode();
                currentNode.myRect.x = mousePos.x;
                currentNode.myRect.y = mousePos.y;
                currentNode.parentGraph = currentGraph;
                currentGraph.nodes.Add(currentNode);

                AssetDatabase.AddObjectToAsset(currentNode, currentGraph);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }

    public static void DeleteNode(NodeGraph graph, int nodeID)
    {
        if(graph != null)
        {
            if(graph.nodes.Count >= nodeID)
            {
                BaseNode deleteNode = graph.nodes[nodeID];
                if(deleteNode != null)
                {
                    graph.nodes.RemoveAt(nodeID);
                    GameObject.DestroyImmediate(deleteNode, true);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    public static void DisconnectInput(NodeGraph graph, int nodeID)
    {
        if (graph.nodes[nodeID] != null)
        {
            graph.nodes[nodeID].input.inputNode = null;
            graph.nodes[nodeID].input.isOccupied = false;
        }
    }

    public static void DisconnectOutput(NodeGraph graph, int nodeID)
    {
        if(graph.nodes[nodeID] != null)
        {
            graph.nodes[nodeID].output.outputNode = null;
            graph.nodes[nodeID].output.isOccupied = false;
        }

    }

    public static void DrawGrid(Rect viewRect, float gridSpacing, float gridOpacity, Color gridColor, NodeGraph currentGraph)
    {
        int widthDivs = Mathf.CeilToInt(viewRect.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(viewRect.height / gridSpacing);

        Vector2 offset = Vector2.zero;

        if(currentGraph != null)
        {
            offset = currentGraph.offset;
        }

        Handles.BeginGUI();

        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
        
        for (int x = 0; x < widthDivs; x++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * x, 0, 0), new Vector3(gridSpacing * x, viewRect.height, 0));
        }

        for (int y = 0; y < heightDivs; y++)
        {
            Handles.DrawLine(new Vector3(0, gridSpacing * y, 0), new Vector3(viewRect.width, gridSpacing * y, 0));
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }


}
