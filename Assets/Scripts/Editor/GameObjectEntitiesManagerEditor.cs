using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameObjectEntitiesManager))]
public class GameObjectEntitiesManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedProperty charEntities = serializedObject.FindProperty("m_CharacterEntities");
        EditorGUILayout.PropertyField(charEntities);
        if (GUILayout.Button("Find Characters In Scene"))
        {
            serializedObject.Update();
            CharacterEntity[] behavioursInScene = FindObjectsOfType<CharacterEntity>(true);
            charEntities.ClearArray();
            for (int i = 0; i < behavioursInScene.Length; i++)
            {
                charEntities.InsertArrayElementAtIndex(0);
                charEntities.GetArrayElementAtIndex(0).objectReferenceValue = behavioursInScene[i];
            }
            serializedObject.ApplyModifiedProperties();
        }

        SerializedProperty itemEntities = serializedObject.FindProperty("m_ItemEntities");
        EditorGUILayout.PropertyField(itemEntities);
        if (GUILayout.Button("Find Items In Scene"))
        {
            serializedObject.Update();
            ItemEntity[] behavioursInScene = FindObjectsOfType<ItemEntity>(true);
            itemEntities.ClearArray();
            for (int i = 0; i < behavioursInScene.Length; i++)
            {
                itemEntities.InsertArrayElementAtIndex(0);
                itemEntities.GetArrayElementAtIndex(0).objectReferenceValue = behavioursInScene[i];
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
