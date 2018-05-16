using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConditionNode : BaseNode
{

    public override void IsActive()
    {
        base.IsActive();
    }

    public void DrawNode()
    {
        GUI.backgroundColor = Color.blue;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(nodeName, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();
    }

    public ConditionNode(string name) : base(name)
    {

    }

}
