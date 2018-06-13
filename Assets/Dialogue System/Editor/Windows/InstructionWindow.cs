using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InstructionWindow : EditorWindow
{
    static InstructionWindow instruction;
    private Vector2 pos;

    private GUIStyle _title;
    private GUIStyle _subtitle;
    private GUIStyle _points;

    public float _width = 580;
    public float _height = 700;

    public static void InitInstructionWindow()
    {
        instruction = (InstructionWindow)EditorWindow.GetWindow<InstructionWindow>();
        instruction.titleContent = new GUIContent("Instructiones");

        instruction._title = new GUIStyle();
        instruction._title.alignment = TextAnchor.MiddleCenter;
        instruction._title.fontSize = 16;
        instruction._title.fontStyle = FontStyle.Bold;

        instruction._subtitle = new GUIStyle();
        instruction._subtitle.fontSize = 14;
        instruction._subtitle.fontStyle = FontStyle.Bold;

        instruction._points = new GUIStyle();
        instruction._points.fontStyle = FontStyle.Bold;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        pos = EditorGUILayout.BeginScrollView(pos, GUILayout.Width(_width));

        maxSize = new Vector2(_width, _height);
        minSize = maxSize;

        GUILayout.Space(20);

        GUI.DrawTexture(GUILayoutUtility.GetRect(150, 150), (Texture)Resources.Load("Textures/Editor/logo"), ScaleMode.ScaleToFit);

        GUILayout.Space(20);

        EditorGUILayout.LabelField("Instrucciones de uso", _title);
        EditorGUI.DrawRect(GUILayoutUtility.GetRect(100, 2), Color.black);

        GUILayout.Space(20);

        EditorGUILayout.LabelField("¿Qué es «Dialogue System»?", _subtitle);
        GUILayout.Space(5);
        GUILayout.Label("«Dialogue System» es una herramienta para Unity con la cual se pueden crear sistemas \n de diálogos de forma visual con o sin bifurcaciones.");

        GUILayout.Space(20);

        EditorGUILayout.LabelField("¿Cómo crear el grapho?", _subtitle);
        GUILayout.Space(5);
        GUILayout.Label("En la barra de herramientas de Unity podrás ver agregada la opción «Dialogue System». \n La opción para abrir la ventana del grapho sería: \n «Dialogue System → FlowChart → Launch Graph».");

        GUILayout.Space(20);

        EditorGUILayout.LabelField("¿Cómo funciona la ventana del grapho?", _subtitle);
        GUILayout.Space(5);
        GUILayout.Label("Al abrirla por primera vez encontraremos tres apartados bien diferenciados:", GUILayout.ExpandWidth(false));
        EditorGUILayout.LabelField("  Toolbar", _points);
        GUILayout.Label("     La Toolbar se encuentra en la parte superior de la ventana y cuenta con todas las \n opciones para manipular los elementos en la vista del grapho. Si no se encuentra ningún \n grapho cargado, lo sabremos porque nos aparecerá las opciones de «Create Graph» y \n «Load Graph», con las cuales podremos crear un grapho nuevo o cargar uno que ya \n hallamos realizado. Una vez tenemos un grapho cargado; se añadirán varias opciones \n como la de «Add Node» que permite crear nodos, «Unload Node» para cerrar el grapho \n que tenemos sin cerrar la ventana, y si seleccionamos un nodo se añadirá la opción \n «Edit Node» en la cual podremos borrar el nodo o desconectar sus enlaces.");
        GUILayout.Space(5);
        EditorGUILayout.LabelField("  Graph View", _points);
        GUILayout.Label("     El Graph View es la ventana que cuenta con el dibujo de una grilla. Cuando no haya \n un grapho cargado no podremos hacer mucho, lo sabremos porque nos indicará un texto \n debajo de la Toolbar «no Graph» (que luego será remplazado por le nombre del grapho). \n Si precionamos clic derecho sobre el Graph View podremos acceder a un submenú que \n nos otorga las mismas opciones que la Toolbar. Además, una vez tengamos un grapho \n cargado, podremos panear en la ventana manteniendo precionado el botón de la rueda \n del ratón. Para unír los nodos sólo debemos hacer clic izquierdo en el rectángulo derecho \n (input) del nodo y luego hacer clic sobre el rectángulo izquierdo (output) de otro \n nodo, no hay que mantener presionado. Si se selecciona un nodo, éste se puede arrastrar \n manteniendo clic izquierdo sobre el mismo. Una vez creemos un grapho podremos ver \n que viene con dos nodos por defecto, los cuales son el «Start Node» y «End Node».");
        GUILayout.Space(5);
        EditorGUILayout.LabelField("  Property View", _points);
        GUILayout.Label("     La Property View se encuentra en la parte derecha de la ventana, al lado del \n Graph View. En esta vista, al tener un nodo seleccionado, encontraremos todos los \n parámetros de dicho nodo. Aquí podremos editarlos para ajustarlos a nuestras \n necesidades. Con las flechas direccionales izquierda y derecha podremos modificar \n el tamaño que abarca la vista en la ventana.");

        GUILayout.Space(20);

        EditorGUILayout.LabelField("¿Cuáles son los tipos de Nodos?", _subtitle);
        GUILayout.Space(5);
        GUILayout.Label("  Start Node", _points);
        GUILayout.Label("     Nodo inicial. Se pueden setear valores que serán predefinidos para los demás nodos, \n principalmente para el «Dialogue Node». Es obligatorio que todas las enlaces conectados \n desde este nodo terminen en el «End Node». Sólo puede tener un único output y no tiene \n input.");
        GUILayout.Space(5);
        GUILayout.Label("  End Node", _points);
        GUILayout.Label("     Nodo final. No necesita setear ningún valor para su funcionamiento. No tiene output, \n pero puede tener tantos input como desee.");
        GUILayout.Space(5);
        GUILayout.Label("  Dialogue Node", _points);
        GUILayout.Label("     Nodo de diálogo. En este nodo se debe colocar el diálogo que queremos que aparezca. \n Por defecto toma los valores dados al Start Node, pero podemos otorgarle órdenes propias \n al nodo si así lo queremos. Sólo puede tener un único output y pero los input que quiere.");
        GUILayout.Space(5);
        GUILayout.Label("  Delay Node", _points);
        GUILayout.Label("     Nodo de demora. Cuando pasa a este nodo espera el tiempo que le determines \n antes de pasar a su siguiente. Sólo puede tener un output y cuantos input quiera.");
        GUILayout.Space(5);
        GUILayout.Label("  Question Node", _points);
        GUILayout.Label("     Nodo de pregunta. No se le necesita asignar ningún valor mediante la Property View. \n Sin embargo, este nodo sólo puede conectarse con nodos del tipo Answerd Node. \n Puede contectar cuantos input y output quiera.");
        GUILayout.Space(5);
        GUILayout.Label("  Answerd Node", _points);
        GUILayout.Label("     Nodo de respuesta. En este nodo se asigna las coordenadas del botón en el canvas y el texto que debe indicar el botón. Sólo puede conectar su input a un Question Node. Sólo puede conectar un output y cuantos input quiera.");

        GUILayout.Space(30);

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
