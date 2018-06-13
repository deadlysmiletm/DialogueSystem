using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InstructionsMenu {

    [MenuItem("Dialogue System/Help")]
	public static void InitInstructions()
    {
        InstructionWindow.InitInstructionWindow();
    }
}
