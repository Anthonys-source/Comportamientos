using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(InventorySystemDebugger))]
public class InventorySystemDebuggerEditor : Editor
{
    private InventorySystemDebugger t;

    private void Awake()
    {
        t = target as InventorySystemDebugger;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ItemIDName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_InventoryIDName"));
        if (GUILayout.Button("Add Item to Inventory"))
            t.AddItem();
        serializedObject.ApplyModifiedProperties();
    }
}
