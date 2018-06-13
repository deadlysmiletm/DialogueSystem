using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class NodeGraphWindow : EditorWindow {

    //Variables
    public static NodeGraphWindow currentWindow;

    public NodePropertyView propertyView;
    public GraphView graphView;
    public ToolbarView toolView;

    public NodeGraph currentGraph = null;

    public float viewPrecentage = 0.75f;

    //Metodos Principales
    public static void InitEditorWindow()
    {
        currentWindow = EditorWindow.GetWindow<NodeGraphWindow>();
        currentWindow.titleContent = new GUIContent("Flow Chart");

        CreateViews();
    }

    private void OnGUI()
    {
        if(propertyView == null || graphView == null || toolView == null)
        {
            CreateViews();
            return;
        }

        //EditorGUILayout.LabelField("Editor perro...");

        Event e = Event.current;
        ProcessEvents(e);

        graphView.UpdateView(new Rect(new Vector2(0, 150f), position.size), new Rect(0f, 0.188f, viewPrecentage, 1f), e, currentGraph);
        propertyView.UpdateView(new Rect(position.width, 150f, position.width, position.height), new Rect(viewPrecentage, 0.188f, 1f - viewPrecentage, 1f), e, currentGraph);
        toolView.UpdateView(new Rect(Vector2.zero,position.size), new Rect(0f, 0f, viewPrecentage, 1f), e, currentGraph);
        Repaint();
    }

    //Metodos extra
    static void CreateViews()
    {
        if (currentWindow != null)
        {
            currentWindow.propertyView = new NodePropertyView();
            currentWindow.toolView = new ToolbarView();
            currentWindow.graphView = new GraphView();
        }
        else
            currentWindow = EditorWindow.GetWindow<NodeGraphWindow>();
    }

    void ProcessEvents(Event e)
    {
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow)
            viewPrecentage -= 0.01f;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow)
            viewPrecentage += 0.01f;
    }
}
