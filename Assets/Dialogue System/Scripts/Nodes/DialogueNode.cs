﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class DialogueNode : BaseNode
{
    public string Dialogues;
    public int current;
    public KeyCode myKey;

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Dialogue;
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
        /*string currentText = Dialogues[current];

        if (Input.GetKeyDown(myKey))
            if (current < Dialogues.Count)
                current++;
            else
            {
                CurrentNode.actualNode = output.outputNode;
            }*/
    }

    public override void ChangeColor()
    {
        GUI.backgroundColor = Color.gray;
    }
}
