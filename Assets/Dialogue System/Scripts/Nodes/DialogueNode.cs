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
        var start = (StartNode)parentGraph.nodes[0];

        textContainer = start.container.GetComponentInChildren<Text>();

        if (!modifVar)
        {
            key = start.key;
            delay = start.delay;
            delayMod = start.delayMod;
            keyMod = start.keyMod;
        }

        if(!Application.isPlaying)
            _originalDelay = delay;

        Debug.Log(textContainer.gameObject.name);
    }

    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        base.UpdateNodeGUI(e, viewRect, viewSkin);
    }

    
    public override void IsActive()
    {
        WriteText();

        if (_delayComplete || Condition())
        {
            _delayComplete = false;
            delay = _originalDelay;
            behaviour.ChangeNode(output.outputNode);
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
        textContainer.text = dialogue;
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
