using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ViewBase {

    //Variables Public
    public string viewTitle;
    public Rect viewRect;


    //Variables Protected
    protected GUISkin viewSkin;
    protected NodeGraph currentGraph;

    //Constructor
    public ViewBase(string title)
    {
        viewTitle = title;
        GetEditorSkin();
    }

    //Métodos principales
    public virtual void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
    {
        if(viewSkin == null)
        {
            GetEditorSkin();
            return;
        }

        this.currentGraph = currentGraph;

        if (currentGraph != null)
        {
            viewTitle = currentGraph.graphName;
        }
        else
        {
            viewTitle = "No Graph";
        }

        viewRect = new Rect(editorRect.x * precentageRect.x, editorRect.y * precentageRect.y, editorRect.width * precentageRect.width, editorRect.height * precentageRect.height);
    }

    public virtual void ProcessEvents(Event e) { }

    //Métodos complementarios
    protected void GetEditorSkin()
    {
        viewSkin = (GUISkin)Resources.Load("GUISkins/Editor/GraphEditorSkin");
    }
}
