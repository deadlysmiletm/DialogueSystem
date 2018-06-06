using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[System.Serializable]
public class QuestionNode : BaseNode
{
    public MultiNodeOutput output;
    private bool _initialized = false;

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
        if (!_initialized)
            SearchButtons();
    }

    void SearchButtons()
    {
        foreach (var node in output.outputNode)
        {
            var temp = (AnswerNode)node;

            temp.answer = behaviour.TakePool();

            temp.answer.GetComponent<RectTransform>().position = temp.buttonPosition;
            temp.answer.GetComponentInChildren<Text>().text = nodeName;
            temp.answer.onClick.AddListener(delegate { SelectAnswer(output.outputNode.IndexOf(node)); });
        }

        _initialized = true;
    }

    void SelectAnswer(int id)
    {
        _initialized = false;
        var nodeSeleceted = output.outputNode[id];

        foreach (var answers in output.outputNode)
        {
            var temp = (AnswerNode)answers;

            behaviour.ReturnPool(temp.answer);
            temp.answer = null;
        }

        behaviour.ChangeNode(nodeSeleceted);
    }

}
