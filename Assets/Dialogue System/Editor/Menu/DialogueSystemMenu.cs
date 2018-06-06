using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class DialogueSystemMenu {

	[MenuItem("Dialogue System/FlowChart/Launch Graph")]
    public static void InitNodeEditor()
    {
        NodeGraphWindow.InitEditorWindow();
    }
}
