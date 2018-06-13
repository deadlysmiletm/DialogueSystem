using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;

[System.Serializable]
public class MultiNodeOutput
{
    public bool hasSomething;
    public List<BaseNode> outputNode;
}

[System.Serializable]
public class QuestionNode : BaseNode
{
    public MultiNodeOutput multiOutput;
    private bool _initialized = false;

    public QuestionNode()
    {
        input = new NodeInput();
        multiOutput = new MultiNodeOutput();

        multiOutput.outputNode = new List<BaseNode>();
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
        if (!_initialized)
            SearchButtons();
    }

    void SearchButtons()
    {
        _initialized = true;
        DialogueDatabase.buttonsActive = new List<Button>();

        foreach (var node in multiOutput.outputNode)
        {
            var temp = (AnswerNode)node;
            DialogueDatabase.buttonsActive.Add(DialogueDatabase.activeDialogue.TakePool());

            DialogueDatabase.buttonsActive.Last().GetComponent<RectTransform>().anchoredPosition = temp.buttonPosition;
            DialogueDatabase.buttonsActive.Last().GetComponentInChildren<Text>().text = temp.answer;
            DialogueDatabase.buttonsActive.Last().onClick.AddListener(delegate { SelectAnswer(multiOutput.outputNode.IndexOf(node)); });
        }
    }

    void SelectAnswer(int id)
    {
        _initialized = false;
        var nodeSeleceted = multiOutput.outputNode[id];

        foreach (var item in DialogueDatabase.buttonsActive)
        {
            DialogueDatabase.activeDialogue.ReturnPool(item);
        }

        DialogueDatabase.buttonsActive = new List<Button>();

        DialogueDatabase.activeDialogue.ChangeNode(nodeSeleceted);
    }

}
