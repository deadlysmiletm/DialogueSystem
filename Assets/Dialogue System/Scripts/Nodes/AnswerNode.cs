using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class AnswerNode : BaseNode {

    public Button answerButton;
    public string answer;
    public Vector2 buttonPosition;

    public KeyCode myKey;

    //Uso exclusivo por parte del QuestionNode
    public int index;

    public override void InitNode() {
        base.InitNode();
        nodeType = NodeType.Answer;
        myRect = new Rect(10f, 10f, 110f, 55f);
    }

    public override void UpdateNode(Event e, Rect viewRect) {
        base.UpdateNode(e, viewRect);
    }

    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        base.UpdateNodeGUI(e, viewRect, viewSkin);
    }

    protected override void InputDefinition(GUISkin viewSkin)
    {
        if (GUI.Button(new Rect(myRect.x - 24f, myRect.y + (myRect.height * 0.5f) - 12f, 24f, 24f), "", viewSkin.GetStyle("NodeInput")))
        {
            if (parentGraph != null)
            {
                if (ApproveConnection())
                {
                    QuestionNode connectionNode = (QuestionNode)parentGraph.connectionNode;

                    input.inputNode.Add(connectionNode);
                    input.hasSomething = input.inputNode != null ? true : false;

                    if (!connectionNode.multiOutput.outputNode.Contains(this))
                    {
                        connectionNode.multiOutput.outputNode.Add(this);

                        connectionNode.multiOutput.hasSomething = connectionNode.multiOutput.outputNode.Count > 0 ? true : false;
                    }

                    parentGraph.wantsConnection = false;
                    parentGraph.connectionNode = null;
                }
                else
                {
                    input.hasSomething = input.inputNode != null ? true : false;

                    parentGraph.wantsConnection = false;
                    parentGraph.connectionNode = null;
                }

            }
        }
    }

    protected override bool ApproveConnection()
    {
        if (parentGraph.connectionNode != null)
        {
            if (parentGraph.connectionNode.nodeType == NodeType.Question)
                return true;
        }

        return false;
    }


    public override void IsActive()
    {
        DialogueDatabase.activeDialogue.ChangeNode(output.outputNode);
    }

    protected override void NodeStyle(GUISkin viewSkin) {
        if (!isSelected) {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("Answer"));
        }
        else {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("AnswerSelected"));
        }
    }

    protected override void DrawLine(NodeInput input, float inputID)
    {
        Handles.BeginGUI();
        Handles.color = Color.white;


        for (int i = 0; i < input.inputNode.Count; i++)
        {
            QuestionNode output = (QuestionNode)input.inputNode[i];
            int outputID = output.multiOutput.outputNode.IndexOf(this);

            Handles.DrawLine(new Vector3(input.inputNode[i].myRect.x + input.inputNode[i].myRect.width + 24f, input.inputNode[i].myRect.y + (input.inputNode[i].myRect.height * 0.5f), 0f), new Vector3(myRect.x - 24f, (output.multiOutput.outputNode[outputID].myRect.y + (output.multiOutput.outputNode[outputID].myRect.height * 0.5f) * inputID), 0f));
        }

        Handles.EndGUI();
    }
}
