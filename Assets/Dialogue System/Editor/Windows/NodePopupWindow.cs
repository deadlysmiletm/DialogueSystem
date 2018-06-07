using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NodePopupWindow : EditorWindow {

    //Variables
    static NodePopupWindow currentPopup;
    string graphName = "Enter a name...";

    //Métodos principales
    public static void InitNodePopup()
    {
        currentPopup = (NodePopupWindow)EditorWindow.GetWindow<NodePopupWindow>();
        currentPopup.titleContent = new GUIContent("Graph Popup");
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);

        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("Create a new Graph:", EditorStyles.boldLabel);
        graphName = EditorGUILayout.TextField("Enter name: ", graphName);

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Create Graph", GUILayout.Height(40)))
        {
            if(!string.IsNullOrEmpty(graphName) && graphName != "Enter a name...")
            {
                NodeUtilities.CreateNewGraph(graphName);
                currentPopup.Close();
            }
            else
            {
                EditorUtility.DisplayDialog("Graph Message", "Por favor, ingrese un nombre válido.", "OK");
            }
        }
        if(GUILayout.Button("Cancel", GUILayout.Height(40)))
        {
            currentPopup.Close();
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(20);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
    }

}
