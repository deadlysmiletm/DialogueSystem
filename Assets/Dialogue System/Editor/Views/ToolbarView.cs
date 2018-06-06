using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolbarView : ViewBase
{
    int addNode;
    int editNode;
    

    public ToolbarView() : base("Toolbar") {}

    public override void UpdateView(Rect editorRect, Rect precentageRect, Event e, NodeGraph currentGraph)
    {
        base.UpdateView(editorRect, precentageRect, e, currentGraph);

        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Create Graph"))
            NodePopupWindow.InitNodePopup();

        if (GUILayout.Button("Load Graph"))
            NodeUtilities.LoadGraph();


        if (currentGraph != null)
        {

            addNode = EditorGUILayout.Popup(addNode, new string[] { "Add Node", "Add Dialogue", "Add Question", "Add Condition", "Add Answer", "Add Delay" }, "Dropdown");

            switch (addNode)
            {
                case 1:
                    NodeUtilities.CreateNode(currentGraph, NodeType.Dialogue, new Vector2(50, 50));
                    addNode = 0;
                    break;
                case 2:
                    NodeUtilities.CreateNode(currentGraph, NodeType.Question, new Vector2(50, 50));
                    addNode = 0;
                    break;
                case 3:
                    NodeUtilities.CreateNode(currentGraph, NodeType.Condicional, new Vector2(50, 50));
                    addNode = 0;
                    break;
                case 4:
                    NodeUtilities.CreateNode(currentGraph, NodeType.Answer, new Vector2(50, 50));
                    addNode = 0;
                    break;
                case 5:
                    NodeUtilities.CreateNode(currentGraph, NodeType.Delay, new Vector2(50, 50));
                    addNode = 0;
                    break;
            }

            if (currentGraph.selectedNode != null)
            {
                if(currentGraph.selectedNode.nodeType != NodeType.Start && currentGraph.selectedNode.nodeType != NodeType.End)
                    editNode = EditorGUILayout.Popup(editNode, new string[] { "Edit Node", "Disconect input", "Disconect output", "Delete node" }, "Dropdown");
                if (currentGraph.selectedNode.nodeType == NodeType.End)
                    editNode = EditorGUILayout.Popup(editNode, new string[] { "Edit Node", "Disconect input"}, "Dropdown");
                if (currentGraph.selectedNode.nodeType == NodeType.Start)
                    editNode = EditorGUILayout.Popup(editNode, new string[] { "Edit Node", "Disconect output"}, "Dropdown");


                switch (editNode)
                {
                    case 1:
                        if (currentGraph.selectedNode.nodeType != NodeType.Start)
                            NodeUtilities.DisconnectInput(currentGraph, currentGraph.nodes.IndexOf(currentGraph.selectedNode));
                        if (currentGraph.selectedNode.nodeType == NodeType.Start)
                            NodeUtilities.DisconnectOutput(currentGraph, currentGraph.nodes.IndexOf(currentGraph.selectedNode));
                        editNode = 0;
                        break;
                    case 2:
                        NodeUtilities.DisconnectOutput(currentGraph, currentGraph.nodes.IndexOf(currentGraph.selectedNode));
                        editNode = 0;
                        break;
                    case 3:
                        NodeUtilities.DeleteNode(currentGraph, currentGraph.nodes.IndexOf(currentGraph.selectedNode));
                        editNode = 0;
                        break;
                }
                
            }

            if (GUILayout.Button("Unload Graph"))
                NodeUtilities.UnloadGraph();
        }
        GUILayout.EndHorizontal();
    }
}
