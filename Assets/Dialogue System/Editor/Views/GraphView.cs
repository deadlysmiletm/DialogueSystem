using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphView : ViewBase
{
    private int index = 0;

    //Variables Public

    //Variables Protected
    protected Vector2 mousePos;

    //Constructor
    public GraphView() : base("Graph View") {}

    //Métodos principales
    public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, precentageRect, e, currentGraph);

        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

        GUILayout.BeginArea(viewRect);
        Toolbar();
        if (currentGraph != null)
        {
            currentGraph.UpdateGraphGUI(e, viewRect, viewSkin);
        }
        GUILayout.EndArea();

        ProcessEvents(e);
    }

    void Toolbar()
    {
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Create Graph"))
            NodePopupWindow.InitNodePopup();

        if (GUILayout.Button("Load Graph"))
            NodeUtilities.LoadGraph();

        if (currentGraph != null)
        {           
            index = EditorGUILayout.Popup(index, new string[] {"Add Dialogue", "Add Question", "Add Condition"},"Dropdown");

            if (GUILayout.Button("Add Node"))
            {
                switch (index)
                {
                    case 0:
                        NodeUtilities.CreateNode(currentGraph, NodeType.Dialogue, new Vector2(50, 50));
                        break;
                    case 1:
                        NodeUtilities.CreateNode(currentGraph, NodeType.Question, new Vector2(50, 50));
                        break;
                    case 2:
                        NodeUtilities.CreateNode(currentGraph, NodeType.Condicional, new Vector2(50, 50));
                        break;
                }
            }
        }
        GUILayout.EndHorizontal();
    }

    public override void ProcessEvents(Event e)
    {
        base.ProcessEvents(e);

        if (viewRect.Contains(e.mousePosition))
        {
            if(e.button == 1)
            {
                if(e.type == EventType.MouseDown)
                {
                }

                if(e.type == EventType.MouseDrag)
                {

                }

                if(e.type == EventType.MouseUp)
                {
                    ProcessContextMenu(e);
                    mousePos = e.mousePosition;
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
    void ProcessContextMenu(Event e)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Create Graph"), false, ContextCallBack, "0");
        menu.AddItem(new GUIContent("Load Graph"), false, ContextCallBack, "1");

        if(currentGraph != null)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Unload Graph"), false, ContextCallBack, "2");

            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Add Dialogue Node"), false, ContextCallBack, "3");
            menu.AddItem(new GUIContent("Add Question Node"), false, ContextCallBack, "4");
            menu.AddItem(new GUIContent("Add Conditional Node"), false, ContextCallBack, "5");


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
            default:
                break;
        }
    }
}
