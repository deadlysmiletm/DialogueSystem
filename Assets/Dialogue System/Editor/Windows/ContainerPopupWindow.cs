using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ContainerPopupWindow : EditorWindow {

    //Variables
    static ContainerPopupWindow containerPopup;
    public string containerName = "Enter a name...";
    private GameObject _prefab;

    //Métodos principales
    public static void InitContainerPopup()
    {
        containerPopup = (ContainerPopupWindow)EditorWindow.GetWindow<ContainerPopupWindow>();
        containerPopup.titleContent = new GUIContent("Conteiner Popup");
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);

        GUILayout.BeginVertical();

        EditorGUILayout.LabelField("Create a new Dialogue Container:", EditorStyles.boldLabel);
        containerName = EditorGUILayout.TextField("Enter name: ", containerName);

        GUILayout.Space(5);

        EditorGUI.BeginChangeCheck();
        _prefab = (GameObject)EditorGUILayout.ObjectField("Container Prefab", _prefab, typeof(GameObject), true);
        if (EditorGUI.EndChangeCheck())
            Repaint();

        GUILayout.Space(10);

        if (_prefab != null)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Dialogue Container", GUILayout.Height(40)))
            {
                if (!string.IsNullOrEmpty(containerName) && containerName != "Enter a name...")
                {
                    NodeUtilities.CreateContainer(_prefab);
                    containerPopup.Close();
                }
                else
                {
                    EditorUtility.DisplayDialog("Container Message", "Por favor, ingrese un nombre válido.", "OK");
                }
            }
            if (GUILayout.Button("Cancel", GUILayout.Height(40)))
            {
                containerPopup.Close();
            }
            GUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.HelpBox("The Container prefab can't be null.", MessageType.Error);
        }

        GUILayout.EndVertical();

        GUILayout.Space(20);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
    }
}
