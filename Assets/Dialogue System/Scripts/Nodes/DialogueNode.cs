using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[System.Serializable]
public class DialogueNode : BaseNode
{
    //Variables heredables del Start
    public bool delayMod, keyMod;
    public KeyCode key;
    public float delay;
    public Text textContainer; //No modificable en Property.

    //Variables propias
    public string dialogue;
    public bool modifVar = false;

    private bool _complete, _delayComplete;
    private float _originalDelay;


    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Dialogue;
        myRect = new Rect(10f, 10f, 110f, 55f);

    }

    public override void UpdateNode(Event e, Rect viewRect)
    {
        base.UpdateNode(e, viewRect);


        if(!Application.isPlaying)
            _originalDelay = delay;
    }

    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        base.UpdateNodeGUI(e, viewRect, viewSkin);
    }

    
    public override void IsActive()
    {
        if (!_started)
        {
            if (!modifVar)
            {
                var start = (StartNode)parentGraph.nodes[0];

                key = start.key;
                delay = start.delay;
                delayMod = start.delayMod;
                keyMod = start.keyMod;
            }

            _originalDelay = delay;

            _started = true;
        }

        WriteText();

        if (_delayComplete || Condition())
        {
            _delayComplete = false;
            delay = _originalDelay;
            _started = false;
            DialogueDatabase.activeDialogue.ChangeNode(output.outputNode);
        }

        if (delayMod)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else
            {
                _delayComplete = true;
            }
        }
    }

    void WriteText()
    {
        DialogueDatabase.activeDialogue.GetComponentInChildren<Text>().text = dialogue;
    }

    bool Condition()
    {
        if (keyMod)
        {
            if (Input.GetKeyDown(key))
                return true;
        }
        return false;
    }
}
