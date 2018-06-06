using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class QuestionNode : BaseNode
{

    public MultiNodeOutput output;

    [System.Serializable]
    public class MultiNodeOutput
    {
        public bool hasSomething;
        public List<BaseNode> outputNode;
    }

    public QuestionNode()
    {
        input = new NodeInput();
        output = new MultiNodeOutput();

        output.outputNode = new List<BaseNode>();
        input.inputNode = new List<BaseNode>();
    }

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Question;
        myRect = new Rect(10f, 10f, 110f, 55f);
    }

    protected override void NodeStyle(GUISkin viewSkin)
    {
        if (!isSelected) {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("Question"));
        }
        else {
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
        base.IsActive();
    }

}
