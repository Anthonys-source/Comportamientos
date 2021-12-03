using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemEntity : MonoBehaviour
{
    private EntityID _entityID;

    [Header("Runtime Component Data")]
    [SerializeField] private ItemTypeComponent m_ItemTypeComponent;


    private void Awake()
    {
        _entityID = GetComponent<EntityID>();
        ComponentRegistry registry = ComponentRegistry.GetInst();
        m_ItemTypeComponent = registry.GetComponentFromEntity<ItemTypeComponent>(_entityID.GetTypeID());
    }
}