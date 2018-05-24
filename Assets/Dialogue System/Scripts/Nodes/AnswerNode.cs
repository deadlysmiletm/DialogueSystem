using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerNode : BaseNode {

    public string answer;

    public int current;
    public KeyCode myKey;
    public NodeOutput output;

    public AnswerNode()
    {
        output = new NodeOutput();
    }

    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Dialogue;
        myRect = new Rect(10f, 10f, 110f, 55f);
    }

    public override void UpdateNode(Event e, Rect viewRect) {
        base.UpdateNode(e, viewRect);
    }

    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin) {
        base.UpdateNodeGUI(e, viewRect, viewSkin);
    }


    public override void IsActive()
    {

    }

    protected override void NodeStyle(GUISkin viewSkin) {
        if (!isSelected) {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("Answer"));
        }
        else {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("AnswerSelected"));
        }
    }
}
