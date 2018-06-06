using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DelayNode : BaseNode {

    public float delay;
    private float _delay;

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Delay;
        myRect = new Rect(10f, 10f, 110f, 55f);
        _delay = delay;
    }

    protected override void NodeStyle(GUISkin viewSkin)
    {
        if (!isSelected)
        {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("Question"));
        }
        else
        {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("QuestionSelected"));
        }
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
        if(_delay > 0)
        {
            _delay -= Time.deltaTime;
        }
        else
        {
            _delay = delay;
            CurrentNode.actualNode = output.outputNode;
        }
    }
}
