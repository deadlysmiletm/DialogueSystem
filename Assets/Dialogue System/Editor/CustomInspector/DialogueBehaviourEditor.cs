using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(DialogueBehaviour))]
public class DialogueBehaviourEditor : Editor {

    private DialogueBehaviour _behaviour;
    private GUIStyle _title;

    private void OnEnable()
    {
        _behaviour = (DialogueBehaviour)target;

        _title = new GUIStyle();
        _title.alignment = TextAnchor.MiddleCenter;
        _title.fontSize = 14;
        _title.fontStyle = FontStyle.Bold;
    }

    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
            EditorGUILayout.HelpBox("No puedes editar en PlayMode.", MessageType.Warning);
        else
            Inspector();
    }

    private void Inspector()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Dialogue Behaviour", _title);

        EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.black);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();

        string graphPath = "";
        string buttonPath = "";

        GUILayout.BeginHorizontal("BOX");
        if (GUILayout.Button("Load Graph"))
        {
            graphPath = EditorUtility.OpenFilePanel("Load Graph", Application.dataPath + "/Dialogue System/Resources/Database/", "");
        }

        if (GUILayout.Button("Change Button Prefab"))
        {
            buttonPath = EditorUtility.OpenFilePanel("Load Button", Application.dataPath, "");
        }
        GUILayout.EndHorizontal();

        if (graphPath != "")
        {
            int appPathLen = Application.dataPath.Length;
            string finalPath = graphPath.Substring(appPathLen - 6);

            _behaviour.graph = (NodeGraph)AssetDatabase.LoadAssetAtPath(finalPath, typeof(NodeGraph));
        }

        if (buttonPath != "")
        {
            int appPathLen = Application.dataPath.Length;
            string finalPath = buttonPath.Substring(appPathLen - 6);

            _behaviour.buttonPrefab = (UnityEngine.UI.Button)AssetDatabase.LoadAssetAtPath(finalPath, typeof(UnityEngine.UI.Button));
        }

        EditorGUILayout.Space();

        EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.black);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Node Graph: " + _behaviour.graph.name);

        EditorGUILayout.Space();

        _behaviour.buttonPrefab = (UnityEngine.UI.Button)EditorGUILayout.ObjectField("Button Prefab: ", _behaviour.buttonPrefab, typeof(UnityEngine.UI.Button), true);

        EditorGUILayout.Space();

        if (EditorGUI.EndChangeCheck())
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        Repaint();
    }

}
