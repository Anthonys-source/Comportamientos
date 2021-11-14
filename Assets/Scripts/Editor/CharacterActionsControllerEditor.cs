using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterActionsController))]
public class CharacterActionsControllerEditor : Editor
{
    private CharacterActionsController t;

    private void Awake()
    {
        t = target as CharacterActionsController;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Interact with closest"))
        {
            t.InteractWithClosest();
        }
    }
}
