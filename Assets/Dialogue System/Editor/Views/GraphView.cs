using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class GraphView : ViewBase
{
    protected Vector2 mousePos;
    int overNodeID = 0;
    int index;

    public GraphView() : base("Graph View") {}

    public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, precentageRect, e, currentGraph);

        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

        NodeUtilities.DrawGrid(viewRect, 80f, 0.15f, Color.white, currentGraph);
        NodeUtilities.DrawGrid(viewRect, 40f, 0.10f, Color.white, currentGraph);
        NodeUtilities.DrawGrid(viewRect, 20f, 0.05f, Color.white, currentGraph);


        GUILayout.BeginArea(viewRect);
        if (currentGraph != null)
        {
            currentGraph.UpdateGraphGUI(e, viewRect, viewSkin);

            if (!EndConnectedWithStart(currentGraph.nodes[0]))
            {
                GUILayout.BeginArea(new Rect(0, viewRect.size.y - 100, viewRect.size.x, 40));
                EditorGUILayout.HelpBox("El nodo inicial debe conectar con el nodo final en todos los caminos.", MessageType.Error);
                GUILayout.EndArea();
            }
        }
        GUILayout.EndArea();

        ProcessEvents(e);
    }

    public bool EndConnectedWithStart(BaseNode node)
    {
        if (node.GetType() == typeof(EndNode))
            return true;

        if(node.GetType() == typeof(QuestionNode))
        {
            var temp = (QuestionNode)node;
            foreach (var item in temp.multiOutput.outputNode)
            {
                if (item == temp.multiOutput.outputNode.Last())
                    return (EndConnectedWithStart(item));

                if (!EndConnectedWithStart(item))
                    return false;
            }
        }

        if (!node.output.isOccupied)
            return false;

        return EndConnectedWithStart(node.output.outputNode);
    }

    public override void ProcessEvents(Event e)
    {
        base.ProcessEvents(e);
        if (viewRect.Contains(e.mousePosition))
        {
            if(e.button == 1)
            {
                if(e.type == EventType.MouseUp)
                {
                    mousePos = e.mousePosition;

                    bool overNode = false;
                    overNodeID = 0;
                    if(currentGraph != null)
                    {
                        if(currentGraph.nodes.Count > 0)
                        {
                            for (int i = 0; i < currentGraph.nodes.Count; i++)
                            {
                                if (currentGraph.nodes[i].myRect.Contains(mousePos))
                                {
                                    overNode = true;
                                    overNodeID = i;
                                }
                            }
                        }
                    }

                    if (!overNode)
                        ProcessContextMenu(e, 0);
                    else
                    {
                        if (currentGraph.nodes[overNodeID].nodeType == NodeType.Start)
                            ProcessContextMenu(e, 2);
                        else if (currentGraph.nodes[overNodeID].nodeType == NodeType.End)
                            ProcessContextMenu(e, 3);
                        else
                            ProcessContextMenu(e, 1);
                    }
                }
            }

            if(e.button == 0)
            {
                if (e.type == EventType.MouseDown)
                {
                }
            }
        }
    }

    //Métodos secundarios
    void ProcessContextMenu(Event e, int contextID)
    {
        GenericMenu menu = new GenericMenu();

        if (contextID == 0)
        {
            menu.AddItem(new GUIContent("Create Graph"), false, ContextCallBack, "0");
            menu.AddItem(new GUIContent("Load Graph"), false, ContextCallBack, "1");

            if (currentGraph != null)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Unload Graph"), false, ContextCallBack, "2");

                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Add Dialogue Node"), false, ContextCallBack, "3");
                menu.AddItem(new GUIContent("Add Question Node"), false, ContextCallBack, "4");
                menu.AddItem(new GUIContent("Add Conditional Node"), false, ContextCallBack, "5");
                menu.AddItem(new GUIContent("Add Answerd Node"), false, ContextCallBack, "7");
                menu.AddItem(new GUIContent("Add Delay Node"), false, ContextCallBack, "10");
            }
        }

        if(contextID == 1)
        {
            if (currentGraph != null)
            {
                menu.AddItem(new GUIContent("Delete Node"), false, ContextCallBack, "6");
                menu.AddItem(new GUIContent("Disconnect Input"), false, ContextCallBack, "8");
                menu.AddItem(new GUIContent("Disconnect Output"), false, ContextCallBack, "9");
            }
        }
        if(contextID == 2)
        {
            if(currentGraph != null)
            {
                menu.AddItem(new GUIContent("Disconnect Output"), false, ContextCallBack, "9");
            }
        }
        if(contextID == 3)
        {
            if(currentGraph != null)
            {
                menu.AddItem(new GUIContent("Disconnect Input"), false, ContextCallBack, "8");
            }
        }

        menu.ShowAsContext();
        e.Use();
    }

    void ContextCallBack(object obj)
    {
        switch (obj.ToString())
        {
            case "0":
                NodePopupWindow.InitNodePopup();
                break;
            case "1":
                NodeUtilities.LoadGraph();
                break;
            case "2":
                NodeUtilities.UnloadGraph();
                break;
            case "3":
                NodeUtilities.CreateNode(currentGraph, NodeType.Dialogue, mousePos);
                break;
            case "4":
                NodeUtilities.CreateNode(currentGraph, NodeType.Question, mousePos);
                break;
            case "5":
                NodeUtilities.CreateNode(currentGraph, NodeType.Condicional, mousePos);
                break;
            case "6":
                NodeUtilities.DeleteNode(currentGraph, overNodeID);
                break;
            case "7":
                NodeUtilities.CreateNode(currentGraph, NodeType.Answer, mousePos);
                break;
            case "8":
                NodeUtilities.DisconnectInput(currentGraph, overNodeID);
                break;
            case "9":
                NodeUtilities.DisconnectOutput(currentGraph, overNodeID);
                break;
            case "10":
                NodeUtilities.CreateNode(currentGraph, NodeType.Delay, mousePos);
                break;
            default:
                break;
        }
    }
}
