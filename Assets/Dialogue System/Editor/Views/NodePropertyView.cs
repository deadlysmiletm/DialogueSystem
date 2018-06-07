﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NodePropertyView : ViewBase
{
    //Variables Public

    //Variables Protected
    private BaseNode _currentNode;
    private Object myNode;
    private GUIStyle myStyleSmall;
    private GUIStyle myStyleBig;
    private BaseNode[] allNodes;
    private List<string> allParameters;
    private List<string> typeParam;
    private enum MyParams
    {
        Bool,
        Int,
        Float,
        String
    }

    //Constructor
    public NodePropertyView() : base("Property View")
    {
        myStyleSmall = new GUIStyle {
            fontSize = 10,
            fontStyle = FontStyle.Italic,
            alignment = TextAnchor.MiddleLeft
        };

        myStyleBig = new GUIStyle {
            fontSize = 15,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
        };
        allParameters = new List<string>();
        typeParam = new List<string>();

        typeParam.Add("Int");
        typeParam.Add("Float");
        typeParam.Add("String");
        typeParam.Add("bool");
    }

    //Métodos principales
    public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, precentageRect, e, currentGraph);
        if (currentGraph!= null)
        {
            if (currentGraph.selectedNode)
            {
                viewTitle = currentGraph.selectedNode.nodeName;
            }
        }
        

        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

        GUILayout.BeginArea(viewRect);

        Handles.color = Color.red;
        GUILayout.Label(" You can edit the parameter of the Dialogue System", myStyleSmall);

        //EditorGUILayout.LabelField("Current Node", myStyleBig);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        /*if (GUILayout.Button("Add parameter"))
        {
            allParameters.Add("");
        }
        if (allParameters.Count > 0) {
            if (GUILayout.Button("Remove parameter"))
                allParameters.RemoveAt(allParameters.Count - 1);
                //Repaint();
        }*/

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("My paramters", myStyleSmall);

        EditorGUILayout.BeginVertical();
        /* if (allParameters != null) {
             for (int i = 0; i < allParameters.Count; i++) {
                 EditorGUILayout.BeginHorizontal();
                 //MyParams = 
                 allParameters[i] = EditorGUILayout.TextField(allParameters[i]);
                 EditorGUILayout.EndHorizontal();
             }
         }*/

        if (currentGraph!=null && currentGraph.selectedNode!= null)
        {
            if (currentGraph.selectedNode.nodeType == NodeType.Answer)
            {
                currentGraph.selectedNode.nodeName = EditorGUILayout.TextField("Node Name", currentGraph.selectedNode.nodeName);
                var myAnser = (AnswerNode)currentGraph.selectedNode;
                myAnser.answer = EditorGUILayout.TextField("Answer", myAnser.answer);
            }
            if (currentGraph.selectedNode.nodeType == NodeType.Condicional)
            {
                currentGraph.selectedNode.nodeName = EditorGUILayout.TextField("Node Name", currentGraph.selectedNode.nodeName);
                //var myCond = (ConditionNode)currentGraph.selectedNode;

            }
            if (currentGraph.selectedNode.nodeType == NodeType.Delay)
            {
                currentGraph.selectedNode.nodeName = EditorGUILayout.TextField("Node Name", currentGraph.selectedNode.nodeName);
                var myDel = (DelayNode)currentGraph.selectedNode;
                myDel.delay = EditorGUILayout.FloatField("Delay", myDel.delay);
            }
            if (currentGraph.selectedNode.nodeType == NodeType.Dialogue)
            {
                currentGraph.selectedNode.nodeName = EditorGUILayout.TextField("Node Name", currentGraph.selectedNode.nodeName);
                var myDiag = (DialogueNode)currentGraph.selectedNode;
                myDiag.dialogue =EditorGUILayout.TextField("Dialogue",myDiag.dialogue);
            }
            if (currentGraph.selectedNode.nodeType == NodeType.Question)
            {
                currentGraph.selectedNode.nodeName = EditorGUILayout.TextField("Node Name", currentGraph.selectedNode.nodeName);
                //var myQue = (QuestionNode)currentGraph.selectedNode;
                //myQue.Dialogues = EditorGUILayout.TextField("Question", myQue.Dialogues);
            }

        }

        EditorGUILayout.EndVertical();

        GUILayout.EndArea();

        
    }

    //Métodos secundarios
    


}
