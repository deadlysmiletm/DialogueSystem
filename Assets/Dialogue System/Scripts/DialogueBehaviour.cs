using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehaviour : MonoBehaviour {

    public NodeGraph graph;
    public UnityEngine.UI.Button buttonPrefab;
    private List<GameObject> _poolButtons = new List<GameObject>();

    private NodeGraph _currentGraph;
    private BaseNode _actualNode;
    private bool _isPlaying;

    // Use this for initialization
    void Start()
    {
        if (graph != null)
        {
            AssignBehaviour(graph);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_isPlaying && _currentGraph != null)
            Playing();
	}

    public void Play()
    {
        _isPlaying = true;
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

        foreach (var nodes in grapho.nodes)
        {
            nodes.behaviour = this;
        }

        _actualNode = grapho.nodes[0];

        var start = (StartNode)_actualNode;

        start.container = this.gameObject;
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
}
