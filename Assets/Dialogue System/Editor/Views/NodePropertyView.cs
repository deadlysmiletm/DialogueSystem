using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NodePropertyView : ViewBase
{
    //Variables Public

    //Variables Protected


    //Constructor
    public NodePropertyView() : base("Property View") { }

    //Métodos principales
    public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, precentageRect, e, currentGraph);

        GUI.Box(viewRect, viewTitle, viewSkin.GetStyle("ViewBG"));

        GUILayout.BeginArea(viewRect);

        GUILayout.EndArea();
    }

    //Métodos secundarios

}
