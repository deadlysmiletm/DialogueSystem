using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueBehaviour : MonoBehaviour {

    public NodeGraph graph;
    public UnityEngine.UI.Button buttonPrefab;
    private List<GameObject> _poolButtons = new List<GameObject>();

    private NodeGraph _currentGraph;
    private BaseNode _actualNode;
    private bool _isPlaying;
    private GameObject _canvas;

    public void SetCanvas(GameObject canvas)
    {
        _canvas = canvas;
    }

    void Start()
    {
        if (graph != null)
        {
            AssignBehaviour(graph);
            this.gameObject.SetActive(false);
            _canvas = transform.parent.gameObject;
        }
	}
	
	void Update ()
    {
        if (_isPlaying && _currentGraph != null)
            Playing();
	}

    public void Play()
    {
        this.gameObject.SetActive(true);
        _isPlaying = true;
        DialogueDatabase.activeDialogue = this;
        AssignBehaviour(graph);
    }

    void Playing()
    {
        _actualNode.IsActive();
    }

    public void Stop()
    {
        _isPlaying = false;
        _actualNode = _currentGraph.nodes[0];
    }

    public void AssignBehaviour(NodeGraph grapho)
    {
        _currentGraph = grapho;

        _actualNode = grapho.nodes[0];
    }

    public void ChangeNode(BaseNode node)
    {
        _actualNode = node;
    }

    public UnityEngine.UI.Button TakePool()
    {
        if (_poolButtons.Count == 0)
            ButtonFactory();

        var temp = _poolButtons[0];

        temp.SetActive(true);
        _poolButtons.RemoveAt(0);

        return temp.GetComponent<UnityEngine.UI.Button>();
    }

    public void ReturnPool(UnityEngine.UI.Button button)
    {
        _poolButtons.Add(button.gameObject);
        button.gameObject.SetActive(false);
    }

    public void ButtonFactory()
    {
        var temp = GameObject.Instantiate(buttonPrefab);
        temp.gameObject.SetActive(false);

        _poolButtons.Add(temp.gameObject);
    }

    public void ChangeText(string text)
    {
        GetComponentInChildren<UnityEngine.UI.Text>().text = text;
    }
}
