using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BaseNode : ScriptableObject
{
    public Rect myRect;
    public List<BaseNode> myNext;
    public string nodeName;
    public float duration;
    public bool panning;

    public bool isSelected = false;
    public NodeGraph parentGraph;
    public NodeType nodeType;
    public NodeInput input;
    public NodeOutput output;

    protected GUISkin nodeSkin;

    public class NodeInput
    {
        public bool isOccupied = false;
        public BaseNode inputNode;
    }

    public class NodeOutput
    {
        public bool isOccupied = false;
        public BaseNode outputNode;
    }

    public BaseNode()
    {
        input = new NodeInput();
        output = new NodeOutput();
    }

    public virtual void InitNode()
    {

    }

    public virtual void UpdateNode(Event e, Rect viewRect)
    {
        ProcessEvent(e, viewRect);
    }


#if UNITY_EDITOR
    public virtual void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        ProcessEvent(e, viewRect);
        if(!isSelected)
        {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("DefaultNode"));
        }
        else
        {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("NodeSelected"));
        }

        EditorUtility.SetDirty(this);

        if (GUI.Button(new Rect(myRect.x - 24f, myRect.y + (myRect.height * 0.5f) - 12f, 24f, 24f), "", viewSkin.GetStyle("NodeInput")))
        {
            if(parentGraph != null)
            {
                input.inputNode = parentGraph.connectionNode;
                input.isOccupied = input.inputNode != null ? true : false;

                parentGraph.wantsConnection = false;
                parentGraph.connectionNode = null;
            }
        }

        if (GUI.Button(new Rect(myRect.x + myRect.width, myRect.y + (myRect.height * 0.5f) - 12f, 24f, 24f), " ", viewSkin.GetStyle("NodeOutput")))
        {
            if (parentGraph != null)
            {
                parentGraph.wantsConnection = true;
                parentGraph.connectionNode = this;
            }
        }

        DrawInputLines();

    }
#endif

    void ProcessEvent(Event e, Rect viewRect)
    {
        if (isSelected)
        {
            if (e.type == EventType.MouseDrag)
            {
                if (viewRect.Contains(e.mousePosition))
                {
                    myRect.x += e.delta.x;
                    myRect.y += e.delta.y;
                }
            }
        }
    }

    void DrawInputLines()
    {
        if (input.isOccupied && input.inputNode != null)
        {
            DrawLine(input, 1f);
        }
        else
        {
            input.isOccupied = false;
        }
    }

    void DrawLine(NodeInput input, float inputID)
    {
        Handles.BeginGUI();
        Handles.color = Color.white;
        Handles.DrawLine(new Vector3(input.inputNode.myRect.x + input.inputNode.myRect.width + 24f, input.inputNode.myRect.y + (input.inputNode.myRect.height * 0.5f), 0f), new Vector3(myRect.x - 24f, (myRect.y + (myRect.height * 0.5f) * inputID), 0f));

        Handles.EndGUI();
    }

    public virtual void IsActive() { }

    public virtual void ChangeColor() { }

    public virtual void Padding() { }

}