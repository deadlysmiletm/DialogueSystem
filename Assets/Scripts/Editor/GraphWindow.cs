using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphWindow : EditorWindow {

    private List<BaseNode> _allNodes;
    private float _toolbarHeight = 50;
    private GUIStyle _title;
    private BaseNode _selectedNode;

    private bool _panning;
    private Vector2 graphPan;
    private Vector2 _originalMousePosition;
    private Vector2 _prevPan;
    private Rect graphRect;

    public GUIStyle wrapTextFieldStyle;

    [MenuItem("Dialogue System/Flowchart")]
    public static void OpenWindow()
    {
        GraphWindow grapho = (GraphWindow)GetWindow(typeof(GraphWindow));
        grapho.wantsMouseMove = true;
        grapho.graphPan = new Vector2(0, grapho._toolbarHeight);
        grapho.graphRect = new Rect(0, grapho._toolbarHeight, Mathf.Infinity, Mathf.Infinity);

        grapho._title = new GUIStyle
        {
            fontSize = 18,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        grapho.wrapTextFieldStyle = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true
        };

        grapho._allNodes = new List<BaseNode>();

        grapho.Show();
    }

    private void OnGUI()
    {
        CheckMouseInput(Event.current);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("FlowChart", _title, GUILayout.Height(25));
        EditorGUILayout.Space();

        graphRect.x = graphPan.x;
        graphRect.y = graphRect.y;
        EditorGUI.DrawRect(new Rect(0, _toolbarHeight, position.width, position.height - _toolbarHeight), Color.gray);

        GUI.BeginGroup(graphRect);

        BeginWindows();

        for (int i = 0; i < _allNodes.Count; i++)
        {
            foreach (var n in _allNodes[i].myNext)
            {
                Handles.DrawLine(new Vector2(_allNodes[i].myRect.position.x + _allNodes[i].myRect.width / 2, _allNodes[i].myRect.position.y + _allNodes[i].myRect.height / 2), new Vector2(n.myRect.position.x + n.myRect.width/2, n.myRect.position.y + n.myRect.height/2));
            }

            _allNodes[i].panning = _panning;
            _allNodes[i].ChangeColor();
            _allNodes[i].Padding();
        }

        for (int i = 0; i < _allNodes.Count; i++)
        {
            _allNodes[i].myRect = GUI.Window(i, _allNodes[i].myRect, _allNodes[i].DrawNode, _allNodes[i].nodeName);
        }
        EndWindows();
        GUI.EndGroup();

    }

    private void CheckMouseInput(Event mouse)
    {
        if (!graphRect.Contains(mouse.mousePosition) || !(focusedWindow == this || mouseOverWindow == this))
            return;

        if (mouse.button == 2 && mouse.type == EventType.MouseDown)
        {
            _panning = true;
            _prevPan = new Vector2(graphPan.x, graphPan.y);
            _originalMousePosition = mouse.mousePosition;
        }
        else if (mouse.button == 2 && mouse.type == EventType.MouseUp)
            _panning = false;

        if (_panning)
        {
            var padX = _prevPan.x + mouse.mousePosition.x - _originalMousePosition.x;
            graphPan.x = padX > 0 ? 0 : padX;

            var padY = _prevPan.y + mouse.mousePosition.y - _originalMousePosition.y;
            graphPan.y = padY > _toolbarHeight ? _toolbarHeight : padY;

            Repaint();
        }

        if (mouse.button == 1 && mouse.type == EventType.MouseDown)
            ContextMenuOpen();
    }

    private void ContextMenuOpen()
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Add Dialogue Node"), false, NewDialogueNode);
        menu.ShowAsContext();
    }

    private void NewDialogueNode()
    {
        _allNodes.Add(new DialogueNode("Dialogue Node"));
        Repaint();
    }
}
