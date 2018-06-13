using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum OperationType
{
    Equal,
    Greater,
    Less,
}

[System.Serializable]
public class ConditionNode : BaseNode
{
    //Esta representa el caso de no cumplirse la condición.
    //Por default cancela el grapho, esta opción habilita el cargar otro grapho,
    public bool changeGraph = false;
    public NodeGraph newGraph;

    //Dictionarios para las variables creadas en el Property view
    public Dictionary<string, int> integrerConditions = new Dictionary<string, int>();
    public Dictionary<string, float> floatingConditions = new Dictionary<string, float>();
    public Dictionary<string, bool> booleanConditions = new Dictionary<string, bool>();

    private Dictionary<string, object> variables = new Dictionary<string, object>();
    private Dictionary<string, OperationType> operationTypes;

    public void ChangeIntegrerValue(string varName, int value)
    {
        if(value.GetType() == typeof(int))
            variables[varName] = (int)value;
    }

    public void ChangeFloatingValue(string varName, float value)
    {
        if(value.GetType() == typeof(float))
            variables[varName] = (float)value;
    }

    public void ChangeBooleanValue(string varName, bool value)
    {
        if(value.GetType() == typeof(bool))
            variables[varName] = (bool)value;
    }

    public object GetValue(string varName)
    {
        return variables[varName];
    }

    public void UpdateVariables()
    {
        foreach (var item in integrerConditions)
        {
            if (!variables.ContainsKey(item.Key))
                variables.Add(item.Key, item.Value);
        }

        foreach (var item in floatingConditions)
        {
            if (!variables.ContainsKey(item.Key))
                variables.Add(item.Key, item.Value);
        }

        foreach (var item in booleanConditions)
        {
            if (!variables.ContainsKey(item.Key))
                variables.Add(item.Key, item.Value);
        }
    }

    public void ClearVariables()
    {
        variables = new Dictionary<string, object>();
    }

    public override void InitNode()
    {
        base.InitNode();
        nodeType = NodeType.Condicional;
        myRect = new Rect(10f, 10f, 110f, 55f);
    }

    public override void UpdateNode(Event e, Rect viewRect)
    {
        base.UpdateNode(e, viewRect);
    }

    public override void UpdateNodeGUI(Event e, Rect viewRect, GUISkin viewSkin)
    {
        base.UpdateNodeGUI(e, viewRect, viewSkin);
    }

    public override void IsActive()
    {/*
        if (ConditionCertification())
            DialogueDatabase.activeDialogue.ChangeNode(output.outputNode);
        else
        {
            if(changeGraph)
            {
                if (newGraph != null)
                    DialogueDatabase.activeDialogue.AssignBehaviour(newGraph);
            }
            else
                DialogueDatabase.activeDialogue.Stop();
        }*/
    }

    bool ConditionCertification()
    {
        foreach (KeyValuePair<string, object> var in variables)
        {
            if (integrerConditions.ContainsKey(var.Key))
            {
                if (operationTypes[var.Key] == OperationType.Equal)
                    if (integrerConditions[var.Key] != (int)var.Value)
                        return false;

                if (operationTypes[var.Key] == OperationType.Greater)
                    if (integrerConditions[var.Key] >= (int)var.Value)
                        return false;

                if (operationTypes[var.Key] == OperationType.Less)
                    if (integrerConditions[var.Key] <= (int)var.Value)
                        return false;
            }

            if (floatingConditions.ContainsKey(var.Key))
            {
                if (operationTypes[var.Key] == OperationType.Equal)
                    if (floatingConditions[var.Key] != (float)var.Value)
                        return false;

                if (operationTypes[var.Key] == OperationType.Greater)
                    if (floatingConditions[var.Key] >= (float)var.Value)
                        return false;

                if (operationTypes[var.Key] == OperationType.Less)
                    if (floatingConditions[var.Key] <= (float)var.Value)
                        return false;
            }

            if (booleanConditions.ContainsKey(var.Key))
            {
                if (operationTypes[var.Key] == OperationType.Equal)
                    if (booleanConditions[var.Key] != (bool)var.Value)
                        return false;
            }
        }

        return true;
    }

    protected override void NodeStyle(GUISkin viewSkin) {
        if (!isSelected) {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("Condition"));
        }
        else {
            GUI.Box(myRect, nodeName, viewSkin.GetStyle("ConditionSelected"));
        }
    }
}
