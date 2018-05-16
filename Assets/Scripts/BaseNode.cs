using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode
{
    public Rect myRect;
    public List<BaseNode> myNext;
    public string nodeName;
    public float duration;
    private bool _overNode;
    public bool panning;

    public BaseNode(string name)
    {
        myRect = new Rect(0, 0, 200, 150);
        nodeName = name;
        myNext = new List<BaseNode>();
    }

    public virtual void IsActive() { }

    public virtual void DrawNode(int id) { }

    public void CheckMouse(Event cE, Vector2 pan)
    {
        if (myRect.Contains(cE.mousePosition - pan))
            _overNode = true;
        else
            _overNode = false;
    }

    public virtual void ChangeColor() { }

    public virtual void Padding() { }

    public bool OverNode
    { get { return _overNode; } }
}