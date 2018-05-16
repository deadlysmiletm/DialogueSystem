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

    public NodeGraph currentGraph = null;

    public float viewPrecentage = 0.75f;

    //Metodos Principales
    public static void InitEditorWindow()
    {
        currentWindow = EditorWindow.GetWindow<NodeGraphWindow>();
        currentWindow.titleContent = new GUIContent("Flow Chart");

        CreateViews();
    }

    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        if(propertyView == null || graphView == null)
        {
            CreateViews();
            return;
        }

        EditorGUILayout.LabelField("Editor perro...");

        Event e = Event.current;
        ProcessEvents(e);

        graphView.UpdateView(position, new Rect(0f, 0f, viewPrecentage, 1f), e, currentGraph);
        propertyView.UpdateView(new Rect(position.width, position.y, position.width, position.height), new Rect(viewPrecentage, 0f, 1f - viewPrecentage, 1f), e, currentGraph);
        Repaint();
    }

    //Metodos extra
    static void CreateViews()
    {
        if (currentWindow != null)
        {
            currentWindow.propertyView = new NodePropertyView();
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
