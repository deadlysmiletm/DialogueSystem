using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParameterWindows : EditorWindow
{
    private int myNumberOfDialogues;
    private int numberOfSentences;

    [MenuItem("Dialogue System/Pramaters Editor")]
    static void CreateWindow()
    {
        ((ParameterWindows)GetWindow(typeof(ParameterWindows))).Show();
    }

    private void OnGUI()
    {
        minSize = new Vector2(350,300);

        GUILayout.Label("You can edit the parameter of the Dialogue System", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        myNumberOfDialogues = EditorGUILayout.IntField("Number of dialogues",myNumberOfDialogues);
        GUILayout.Button("Create");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        numberOfSentences = EditorGUILayout.IntField("Number of sentences", numberOfSentences);
        GUILayout.Button("Create");
        EditorGUILayout.EndHorizontal();


    }
}
