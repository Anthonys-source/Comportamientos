using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOverlay : MonoBehaviour
{
    private ComponentsContainer<CharacterComponent> _characterComponents;

    private void Awake()
    {
        _characterComponents = ComponentsRegistry.GetInst().GetComponentsContainer<CharacterComponent>();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 220, 80));
        GUILayout.BeginVertical();
        GUILayout.Box($"Active Characters: " + _characterComponents.GetList().Count);
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
