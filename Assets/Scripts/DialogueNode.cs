using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueNode : BaseNode
{
    public List<string> Dialogues = new List<string>();
    public int current;
    public KeyCode myKey;
    

    public override void IsActive()
    {
        string currentText = Dialogues[current];

        if (Input.GetKeyDown(myKey))
            if (current < Dialogues.Count)
                current++;
            else
            {
                CurrentNode.actualNode = myNext[0];
            }
    }

    public override void DrawNode(int id)
    {

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(nodeName, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();


        ChangeColor();
    }

    //Dibujo el nodo de dialogo.
    public DialogueNode(string name) : base(name)
    {

    }

    public override void ChangeColor()
    {
        GUI.backgroundColor = Color.gray;
    }

    public override void Padding()
    {

        if (panning)
        {
            GUI.DragWindow();

            if (OverNode)
                return;
        }
    }

}
