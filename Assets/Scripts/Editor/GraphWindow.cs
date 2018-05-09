using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphWindow : EditorWindow {

    [MenuItem("Dialogue System/Window")]
    public static void OpenWindow()
    {
        GraphWindow grapho = (GraphWindow)GetWindow(typeof(GraphWindow));
        grapho.wantsMouseMove = true;
        grapho.Show();
    }

    private void OnGUI()
    {
        
    }

    public static void DrawNodeCurve(Rect start, Rect end)
    {
        
    }
}
