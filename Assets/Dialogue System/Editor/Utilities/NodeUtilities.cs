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

            AssetDatabase.CreateAsset(currentGraph, "Assets/Dialogue System/Resources/Database/" + graphName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            NodeGraphWindow currentWindow = (NodeGraphWindow)EditorWindow.GetWindow<NodeGraphWindow>();

            if(currentWindow != null)
            {
                currentWindow.currentGraph = currentGraph;
            }

            CreateNode(currentWindow.currentGraph, NodeType.Start, new Vector2(104, 136));
            CreateNode(currentWindow.currentGraph, NodeType.End, new Vector2(307, 136));
        }
        else
        {
            EditorUtility.DisplayDialog("Graph Message", "No se pudo crear el Grapho", "OK");
        }
    }

    public static void LoadGraph()
    {
        NodeGraph currentGraph = null;
        string graphPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/Dialogue System/Resources/Database/", "");

        if (graphPath != "")
        {
            int appPathLen = Application.dataPath.Length;
            string finalPath = graphPath.Substring(appPathLen - 6);

            currentGraph = (NodeGraph)AssetDatabase.LoadAssetAtPath(finalPath, typeof(NodeGraph));

            if (currentGraph != null)
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
                case NodeType.Delay:
                    currentNode = (DelayNode)ScriptableObject.CreateInstance<DelayNode>();
                    currentNode.nodeName = "Delay Node";
                    break;
                case NodeType.Start:
                    currentNode = (StartNode)ScriptableObject.CreateInstance<StartNode>();
                    currentNode.nodeName = "Start";
                    break;
                case NodeType.End:
                    currentNode = (EndNode)ScriptableObject.CreateInstance<EndNode>();
                    currentNode.nodeName = "End";
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
                    if (deleteNode.nodeType == NodeType.Answer)
                    {
                        for (int i = 0; i < deleteNode.input.inputNode.Count; i++)
                        {
                            QuestionNode questionNode = (QuestionNode)deleteNode.input.inputNode[i];

                            questionNode.output.outputNode.Remove(graph.nodes[nodeID]);
                            questionNode.output.hasSomething = questionNode.output.outputNode.Count == 0 ? false : true;
                        }
                    }
                    else
                    {
                        Debug.Log(deleteNode.nodeName + " " + deleteNode.input.inputNode.Count);
                        for (int i = 0; i < deleteNode.input.inputNode.Count; i++)
                        {
                            deleteNode.input.inputNode[i].output.outputNode = null;
                            deleteNode.input.inputNode[i].output.isOccupied = false;
                        }
                    }

                    if (deleteNode.nodeType == NodeType.Question)
                    {
                        QuestionNode questionNode = (QuestionNode)deleteNode;

                        for (int i = 0; i < questionNode.output.outputNode.Count; i++)
                        {
                            questionNode.output.outputNode[i].input.inputNode.Remove(questionNode);
                            questionNode.output.outputNode[i].input.hasSomething = questionNode.output.outputNode[i].input.inputNode.Count > 0 ? true : false;
                        }
                    }
                    else
                    {
                        if (deleteNode.output.outputNode != null)
                        {
                            deleteNode.output.outputNode.input.inputNode.Remove(deleteNode);
                            deleteNode.output.outputNode.input.hasSomething = deleteNode.output.outputNode.input.inputNode.Count > 0 ? true : false;
                        }
                    }

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
            Debug.Log(graph.nodes[nodeID].nodeType.ToString());
            if (graph.nodes[nodeID].nodeType == NodeType.Answer)
            {
                for (int i = 0; i < graph.nodes[nodeID].input.inputNode.Count; i++)
                {
                    QuestionNode questionNode = (QuestionNode)graph.nodes[nodeID].input.inputNode[i];

                    questionNode.output.outputNode.RemoveAt(questionNode.output.outputNode.IndexOf(graph.nodes[nodeID]));
                    questionNode.output.hasSomething = questionNode.output.outputNode.Count == 0 ? false : true;
                }
            }
            else
            {
                for (int i = 0; i < graph.nodes[nodeID].input.inputNode.Count; i++)
                {
                    graph.nodes[nodeID].input.inputNode[i].output.outputNode = null;
                    graph.nodes[nodeID].input.inputNode[i].output.isOccupied = false;
                }
            }

            graph.nodes[nodeID].input.inputNode = new List<BaseNode>();
            graph.nodes[nodeID].input.hasSomething = false;
        }
    }

    public static void DisconnectOutput(NodeGraph graph, int nodeID)
    {
        if(graph.nodes[nodeID] != null)
        {
            if (graph.nodes[nodeID].nodeType == NodeType.Question)
            {
                QuestionNode multiNode = (QuestionNode)graph.nodes[nodeID];

                if (multiNode.output.hasSomething)
                {
                    for (int i = 0; i < multiNode.output.outputNode.Count; i++)
                    {
                        multiNode.output.outputNode[i].input.inputNode.Remove(multiNode);

                        multiNode.output.outputNode[i].input.hasSomething = multiNode.output.outputNode[i].input.inputNode.Count > 0 ? true : false;
                    }

                    multiNode.output.outputNode = new List<BaseNode>();
                    multiNode.output.hasSomething = false;
                }
            }
            else
            {
                if (graph.nodes[nodeID].output.outputNode != null)
                {
                    graph.nodes[nodeID].output.outputNode.input.inputNode.Remove(graph.nodes[nodeID]);
                    graph.nodes[nodeID].output.outputNode.input.hasSomething = graph.nodes[nodeID].output.outputNode.input.inputNode.Count > 0 ? true : false;

                    graph.nodes[nodeID].output.outputNode = null;
                    graph.nodes[nodeID].output.isOccupied = false;
                }
            }


        }

    }

    public static void DrawGrid(Rect viewRect, float gridSpacing, float gridOpacity, Color gridColor, NodeGraph currentGraph)
    {
        int widthDivs = Mathf.CeilToInt(10000 / gridSpacing);
        int heightDivs = Mathf.CeilToInt(10000 / gridSpacing);

        Vector2 offset = Vector2.zero;

        if(currentGraph != null)
        {
            offset = currentGraph.offset;
        }

        Handles.BeginGUI();

        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);
        
        for (int x = -widthDivs; x < widthDivs; x++)
        {
            Handles.DrawLine(new Vector3((gridSpacing * x) + offset.x, 0, 0), new Vector3((gridSpacing * x) + offset.x, viewRect.height, 0));
        }

        for (int y = -heightDivs; y < heightDivs; y++)
        {
            Handles.DrawLine(new Vector3(0, (gridSpacing * y) + offset.y, 0), new Vector3(viewRect.width, (gridSpacing * y) + offset.y, 0));
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    public static void CreateContainer(GameObject prefab, GameObject canvas, string name)
    {
        var temp = GameObject.Instantiate(prefab);
        temp.transform.SetParent(canvas.transform, false);
        temp.name = name;
    }
}
