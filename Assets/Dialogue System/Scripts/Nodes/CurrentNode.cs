using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentNode : MonoBehaviour
{
    public static BaseNode actualNode;
    public static bool endBehaviour = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!endBehaviour)
            actualNode.IsActive();
	}

    public static void EndBehaviour()
    {
        endBehaviour = true;
    }

    public static void StartBehaviour()
    {
        endBehaviour = false;
    }
}
