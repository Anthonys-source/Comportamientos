using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemEntity : MonoBehaviour
{
    private EntityID _entityID;

    [Header("Runtime Component Data")]
    [SerializeField] private ItemComponent m_ItemComponent;


    private void Awake()
    {
        _entityID = GetComponent<EntityID>();
        ComponentsRegistry registry = ComponentsRegistry.GetInst();
        m_ItemComponent = registry.GetComponentFromEntity<ItemComponent>(_entityID.GetID());
    }
}