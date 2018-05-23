using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphView : ViewBase
{
    protected Vector2 mousePos;
    int deleteNodeID = 0;

    public GraphView() : base("Graph View") {}

    public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, precentageRect, e, currentGraph);

        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

        NodeUtilities.DrawGrid(viewRect, 80f, 0.15f, Color.white);
        NodeUtilities.DrawGrid(viewRect, 40f, 0.10f, Color.white);
        NodeUtilities.DrawGrid(viewRect, 20f, 0.05f, Color.white);


        GUILayout.BeginArea(viewRect);
        if(currentGraph != null)
        {
            currentGraph.UpdateGraphGUI(e, viewRect, viewSkin);
        }
        GUILayout.EndArea();

        ProcessEvents(e);
    }

    public override void ProcessEvents(Event e)
    {
        base.ProcessEvents(e);
        Debug.Log(e.button);
        if (viewRect.Contains(e.mousePosition))
        {
            if(e.button == 1)
            {
                if(e.type == EventType.MouseDrag)
                {

                }

                if(e.type == EventType.MouseUp)
                {
                    mousePos = e.mousePosition;

                    bool overNode = false;
                    deleteNodeID = 0;
                    if(currentGraph != null)
                    {
                        if(currentGraph.nodes.Count > 0)
                        {
                            for (int i = 0; i < currentGraph.nodes.Count; i++)
                            {
                                if (currentGraph.nodes[i].myRect.Contains(mousePos))
                                {
                                    overNode = true;
                                    deleteNodeID = i;
                                }
                            }
                        }
                    }

                    if (!overNode)
                        ProcessContextMenu(e, 0);
                    else
                        ProcessContextMenu(e, 1);
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
            }
        }

        if(contextID == 1)
        {
            if (currentGraph != null)
            {
                menu.AddItem(new GUIContent("Delete Node"), false, ContextCallBack, "6");
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
                NodeUtilities.DeleteNode(currentGraph, deleteNodeID);
                break;
            default:
                break;
        }
    }
}
