using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NodeGraph : ScriptableObject {

    //Variables Public
    public string graphName;
    public List<BaseNode> nodes;
    public BaseNode selectedNode;

    public bool wantsConnection = false;
    public BaseNode connectionNode;

    //Métodos principales
    private void OnEnable()
    {
        if(nodes == null)
        {
            nodes = new List<BaseNode>();
        }
    }


    public void InitGraph()
    {
        if(nodes.Count > 0)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].InitNode();
            }
        }
    }

    public void UpdateGraph()
    {
        if(nodes.Count > 0)
        {

        }
    }

#if UNITY_EDITOR
    public void UpdateGraphGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        if(nodes.Count > 0)
        {
            ProcessEvents(e, viewRect);
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].UpdateNodeGUI(e, viewRect, viewSkin);
            }
        }

        if (wantsConnection)
        {
            if(connectionNode  != null)
            {
                DrawConnectionToMouse(e.mousePosition);
            }
        }

        EditorUtility.SetDirty(this);
    }

#endif

    //Métodos secundarios
    void ProcessEvents(Event e, Rect viewRect)
    {
        if (viewRect.Contains(e.mousePosition))
        {
            if(e.button == 0)
            {
                if(e.type == EventType.MouseDown)
                {
                    DeselectAllNodes();
                    bool setNode = false;
                    selectedNode = null;
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        if (nodes[i].myRect.Contains(e.mousePosition))
                        {
                            nodes[i].isSelected = true;
                            selectedNode = nodes[i];
                            setNode = true;
                        }
                    } 
                    if (!setNode)
                    {
                        DeselectAllNodes();
                        //wantsConnection = false;
                        //connectionNode = null;
                    }

                    if (wantsConnection)
                    {
                        wantsConnection = false;
                    }
                }
            }
            else
            {
                if(e.type == EventType.MouseDown)
                {
                    DeselectAllNodes();
                }
            }
        }
    }

    void DeselectAllNodes()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].isSelected = false;
        }
    }

    void DrawConnectionToMouse(Vector2 mousePosition)
    {
        Handles.BeginGUI();
        Handles.color = Color.white;
        Handles.DrawLine(new Vector3(connectionNode.myRect.x + connectionNode.myRect.width + 24f, connectionNode.myRect.y + (connectionNode.myRect.height * 0.5f), 0f), new Vector3(mousePosition.x, mousePosition.y, 0f));
        Handles.EndGUI();
    }

}
