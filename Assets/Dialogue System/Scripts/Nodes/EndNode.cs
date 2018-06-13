using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndNode : BaseNode {

    public EndNode()
    {
        input = new NodeInput();

        input.inputNode = new List<BaseNode>();
    }

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.End;
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

    protected override void OutputDefinition(GUISkin viewSkin) {}

    public override void IsActive()
    {
        DialogueDatabase.activeDialogue.Stop();
    }
}
