using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class QuestionNode : BaseNode
{

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Question;
        myRect = new Rect(10f, 10f, 110f, 55f);
    }

    public override void UpdateNode(Event e, Rect viewRect)
    {
        base.UpdateNode(e, viewRect);
    }

    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        base.UpdateNodeGUI(e, viewRect, viewSkin);
    }

    public override void IsActive()
    {
        base.IsActive();
    }
    /*
    public void DrawNode()
    {
        GUI.backgroundColor = Color.red;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(nodeName, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();
    }

    public QuestionNode(string name) : base(name)
    {

    }*/

}
