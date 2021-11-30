using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterActions))]
public class CharacterActionsControllerEditor : Editor
{
    private CharacterActions t;
    Vector2 m_LookDir;

    private void Awake()
    {
        t = target as CharacterActions;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Interact with closest"))
            t.InteractWithClosest();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Get Happy"))
            t.GetHappy();

        if (GUILayout.Button("Get Angry"))
            t.GetAngry();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Look At Direction"))
            t.LookAt(new Vector3(m_LookDir.x, 0.0f, m_LookDir.y));
        m_LookDir = EditorGUILayout.Vector2Field("", m_LookDir);
        GUILayout.EndHorizontal();
    }
}
