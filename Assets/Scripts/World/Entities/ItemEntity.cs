using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemEntity : MonoBehaviour
{
    [Header("Entity Identifier")]
    public string m_ItemNameID;
    private ID m_ID;

    [Header("Runtime Component Data")]
    [SerializeField] private ItemComponent m_ItemComponent;


    private void Awake()
    {
        m_ID = new ID(m_ItemNameID);
        ComponentsRegistry registry = ComponentsRegistry.GetInst();
        m_ItemComponent = registry.GetComponentFromEntity<ItemComponent>(m_ID);
    }
}