using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueNode : BaseNode
{

    public DialogueNode(float x, float y, float width, float height, string name) : base(x, y, width, height, name)
    {
        myRect = new Rect(x, y, width, height);
        inputNode = new List<BaseNode>();
        nodeName = name;
    }

    public override void DrawNode()
    {
        nodeName = EditorGUILayout.TextField("Name = ", nodeName);
        name = nodeName;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Inputs " + inputNode.Count);
        GUILayout.FlexibleSpace();
        if(GUILayout.Button("Link Output"))
        {

        }
        EditorGUILayout.EndHorizontal();
    }
}
