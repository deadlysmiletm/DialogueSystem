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
}
