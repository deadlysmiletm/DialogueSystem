﻿using System.Collections;
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
        actualNode.IsActive();
	}
}
