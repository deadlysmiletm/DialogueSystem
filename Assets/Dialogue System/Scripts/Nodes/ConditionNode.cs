﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ConditionNode : BaseNode
{

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Condicional;
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

    protected override void NodeStyle(GUISkin viewSkin) {
        if (!isSelected) {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("Condition"));
        }
        else {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("ConditionSelected"));
        }
    }
}
