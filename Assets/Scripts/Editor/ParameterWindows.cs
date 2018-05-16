using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParameterWindows : EditorWindow
{
    private BaseNode _currentNode;
    private Object myNode;
    private int _minNodes;
    private int _maxConnections;
    

    [MenuItem("Dialogue System/Pramaters Editor")]
    static void CreateWindow()
    {
        ((ParameterWindows)GetWindow(typeof(ParameterWindows))).Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350,300);

        GUILayout.Label("You can edit the parameter of the Dialogue System", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        myNode = EditorGUILayout.ObjectField("My current node is :", myNode, typeof(MonoScript),true);
        //EditorGUILayout.SelectableLabel("my");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        _minNodes = EditorGUILayout.IntField("Min Nodes",_minNodes);
        GUILayout.Button("Create");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        _maxConnections = EditorGUILayout.IntField("Max connection per Nodes", _maxConnections);
        GUILayout.Button("Create");
        EditorGUILayout.EndHorizontal();


        if (_minNodes<=2)
        {
            _minNodes = 2;
        }
        if (_maxConnections<=1)
        {
            _maxConnections = 1;
        }



    }
}
