using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterEntity : MonoBehaviour
{
    public string CharacterNameID => m_CharacterNameID;
    public ID CharacterID => m_CharacterID;

    [Header("Entity Identifier")]
    [SerializeField] private string m_CharacterNameID;
    private ID m_CharacterID;

    [Header("Runtime Component Data")]
    [SerializeField] private CharacterComponent m_CharComponent;
    [SerializeField] private MoodComponent m_MoodComponent;
    [SerializeField] private InventoryComponent m_InventoryComponent;


    private void Awake()
    {
        m_CharacterID = new ID(CharacterNameID);
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

public class Entity : MonoBehaviour
{
    private ID m_CharacterID;
    private Dictionary<Type, object> m_Components = new Dictionary<Type, object>();

    public T GetEntityComponent<T>() where T : class
    {
        return (m_Components[typeof(T)] as T);
    }
}