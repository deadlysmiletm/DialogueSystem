using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class BaseNode : ScriptableObject {

    public Rect myRect;
    public string nodeName;

    public bool _overNode;
    public bool Overnode { get { return _overNode; } }

    public List<BaseNode> inputNode;
    public List<BaseNode> outputNode;

    public BaseNode(float x, float y, float width, float height, string name)
    {
        myRect = new Rect(x, y, width, height);
        inputNode = new List<BaseNode>();
        nodeName = name;
    }

    public virtual void DrawNode() { }

    public virtual void CheckInputAndOutput()
    {
        if(inputNode.Count > 0)
        {
            for (int i = 0; i < inputNode.Count; i++)
            {
                if (inputNode[i] == null)
                    inputNode.RemoveAt(i);
            }
        }
        
        if(outputNode.Count > 0)
        {
            for (int i = 0; i < outputNode.Count; i++)
            {
                if (outputNode[i] == null)
                    outputNode.RemoveAt(i);
            }
        }
    }

    public virtual void CheckMouse(Event e, Vector2 pan)
    {
        if (myRect.Contains(e.mousePosition - pan))
            _overNode = true;
        else
            _overNode = false;
    }

    public virtual void DeleteNode(BaseNode node)
    {
        Destroy(node);
    }
}
