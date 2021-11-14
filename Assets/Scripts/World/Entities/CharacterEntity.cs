using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterEntity : MonoBehaviour
{
    [Header("Entity Identifier")]
    public string m_CharacterNameID;
    private ID m_CharacterID;

    [Header("Runtime Component Data")]
    [SerializeField] private CharacterComponent m_CharComponent;
    [SerializeField] private MoodComponent m_MoodComponent;
    [SerializeField] private InventoryComponent m_InventoryComponent;


    private void Awake()
    {
        m_CharacterID = new ID(m_CharacterNameID);
        ComponentsRegistry registry = ComponentsRegistry.GetInst();
        m_CharComponent = registry.GetComponentFromEntity<CharacterComponent>(m_CharacterID);
        m_MoodComponent = registry.GetComponentFromEntity<MoodComponent>(m_CharacterID);
        m_InventoryComponent = registry.GetComponentFromEntity<InventoryComponent>(m_CharacterID);
    }

    private void Update()
    {
        m_CharComponent.m_Position = transform.position;
    }
}
