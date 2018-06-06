using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StartNode : BaseNode {

    public StartNode()
    {
        output = new NodeOutput();
    }

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Start;
        myRect = new Rect(10f, 10f, 110f, 55f);
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

    protected override void InputDefinition(GUISkin viewSkin) {}    

    protected override void DrawLine(NodeInput input, float inputID) {}

    public override void IsActive()
    {
        CurrentNode.actualNode = output.outputNode;
    }
}
