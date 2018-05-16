using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QuestionNode : BaseNode
{

    public override void IsActive()
    {
        base.IsActive();
    }

    public void DrawNode()
    {
        GUI.backgroundColor = Color.red;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(nodeName, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();
    }

    public QuestionNode(string name) : base(name)
    {

    }

}
