using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class NodeInput
{
    public bool hasSomething = false;
    public List<BaseNode> inputNode;
}

[System.Serializable]
public class NodeOutput
{
    public bool isOccupied = false;
    public BaseNode outputNode;
}

public class BaseNode : ScriptableObject
{
    public Rect myRect;
    public List<BaseNode> myNext;
    public string nodeName;
    public float duration;
    public bool isPanning;

    public bool isSelected = false;
    public NodeGraph parentGraph;
    public NodeType nodeType;
    public NodeInput input;
    public NodeOutput output;

    protected GUISkin nodeSkin;
    protected bool _started = false;

    public BaseNode()
    {
        input = new NodeInput();
        output = new NodeOutput();

        input.inputNode = new List<BaseNode>();
    }

    public virtual void InitNode() {}

    public virtual void UpdateNode(Event e, Rect viewRect)
    {
        ProcessEvent(e, viewRect);
    }


#if UNITY_EDITOR
    public virtual void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        ProcessEvent(e, viewRect);

        NodeStyle(viewSkin);

        EditorUtility.SetDirty(this);

        InputDefinition(viewSkin);

        OutputDefinition(viewSkin);

        DrawInputLines();
    }
#endif

    protected virtual void InputDefinition(GUISkin viewSkin)
    {
        if (GUI.Button(new Rect(myRect.x - 24f, myRect.y + (myRect.height * 0.5f) - 12f, 24f, 24f), "", viewSkin.GetStyle("NodeInput")))
        {
            if (parentGraph != null)
            {
                if (ApproveConnection())
                {
                    if (parentGraph.connectionNode.output.isOccupied)
                    {
                        parentGraph.connectionNode.output.outputNode.input.inputNode.Remove(parentGraph.connectionNode);
                    }

                    Debug.Log(parentGraph.connectionNode.nodeName);

                    input.inputNode.Add(parentGraph.connectionNode);
                    input.hasSomething = input.inputNode.Count > 0 ? true : false;

                    if (parentGraph.connectionNode.nodeType == NodeType.Question)
                    {
                        QuestionNode connectionNode = (QuestionNode)parentGraph.connectionNode;

                        connectionNode.multiOutput.outputNode.Add(this);
                        connectionNode.multiOutput.hasSomething = true;
                    }
                    else
                    {
                        parentGraph.connectionNode.output.outputNode = this;
                        parentGraph.connectionNode.output.isOccupied = true;
                    }

                    parentGraph.wantsConnection = false;
                    parentGraph.connectionNode = null;
                }
                else
                {
                    input.hasSomething = input.inputNode != null ? true : false;

                    parentGraph.wantsConnection = false;
                    parentGraph.connectionNode = null;
                }

            }
        }
    }

    protected virtual void OutputDefinition(GUISkin viewSkin)
    {
        if (GUI.Button(new Rect(myRect.x + myRect.width, myRect.y + (myRect.height * 0.5f) - 12f, 24f, 24f), " ", viewSkin.GetStyle("NodeOutput")))
        {
            if (parentGraph != null)
            {
                parentGraph.wantsConnection = true;
                parentGraph.connectionNode = this;
            }
        }
    }

    protected virtual bool ApproveConnection()
    {
        if (parentGraph.connectionNode != null)
        {
            if (parentGraph.connectionNode.nodeType == NodeType.Question)
                return false;
            else
                return true;
        }
        else
            return false;
    }

    protected virtual void NodeStyle(GUISkin viewSkin)
    {
        if (!isSelected) {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("DefaultNode"));
        }
        else {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("NodeSelected"));
        }
    }

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

        if (isPanning)
        {
            myRect.x += e.delta.x;
            myRect.y += e.delta.y;
        }
    }

    void DrawInputLines()
    {
        if (input.hasSomething && input.inputNode != null)
        {
            DrawLine(input, 1f);
        }
        else
        {
            input.hasSomething = false;
        }
    }

    protected virtual void DrawLine(NodeInput input, float inputID)
    {
        Handles.BeginGUI();
        Handles.color = Color.white;

        for (int i = 0; i < input.inputNode.Count; i++)
        {
            Handles.DrawLine(new Vector3(input.inputNode[i].myRect.x + input.inputNode[i].myRect.width + 24f, input.inputNode[i].myRect.y + (input.inputNode[i].myRect.height * 0.5f), 0f), new Vector3(myRect.x - 24f, (input.inputNode[i].output.outputNode.myRect.y + (input.inputNode[i].output.outputNode.myRect.height * 0.5f) * inputID), 0f));
        }

        Handles.EndGUI();
    }

    public virtual void IsActive() { }

    public virtual void ChangeColor() { }

}