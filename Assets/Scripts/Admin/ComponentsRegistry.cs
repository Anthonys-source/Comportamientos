using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ComponentsRegistry : Singleton<ComponentsRegistry>
{
    private Dictionary<Type, object> m_Containers = new Dictionary<Type, object>();
    private Dictionary<Type, object> m_SingletonComponents = new Dictionary<Type, object>();

    [SerializeField] private ComponentsContainer<CharacterComponent> m_CharacterComponents = new ComponentsContainer<CharacterComponent>();
    [SerializeField] private ComponentsContainer<MoodComponent> m_MoodComponents = new ComponentsContainer<MoodComponent>();
    [SerializeField] private ComponentsContainer<InventoryComponent> m_InventoryComponent = new ComponentsContainer<InventoryComponent>();

    [SerializeField] private ComponentsContainer<BreadBakerComponent> m_BreadBakerComponent = new ComponentsContainer<BreadBakerComponent>();

    [SerializeField] private ComponentsContainer<ItemComponent> m_ItemComponent = new ComponentsContainer<ItemComponent>();
    [SerializeField] private ComponentsContainer<ItemWorldComponent> m_ItemWorldComponent = new ComponentsContainer<ItemWorldComponent>();

    [SerializeField] private ComponentsContainer<BuildingZoneComponent> m_BuildingZoneComponent = new ComponentsContainer<BuildingZoneComponent>();


    public void Initialize()
    {
        s_Instance = this;
        m_Containers.Add(typeof(CharacterComponent), m_CharacterComponents);
        m_Containers.Add(typeof(MoodComponent), m_MoodComponents);
        m_Containers.Add(typeof(InventoryComponent), m_InventoryComponent);

        m_Containers.Add(typeof(BreadBakerComponent), m_BreadBakerComponent);

        m_Containers.Add(typeof(ItemComponent), m_ItemComponent);
        m_Containers.Add(typeof(ItemWorldComponent), m_ItemWorldComponent);

        m_Containers.Add(typeof(BuildingZoneComponent), m_BuildingZoneComponent);
    }

    public ComponentsContainer<T> GetComponentsContainer<T>() where T : class
    {
        return m_Containers[typeof(T)] as ComponentsContainer<T>;
    }

    public T GetSingletonComponent<T>() where T : class
    {
        return m_SingletonComponents[typeof(T)] as T;
    }

    public T GetComponentFromEntity<T>(ID entityID) where T : class
    {
        return (m_Containers[typeof(T)] as ComponentsContainer<T>)[entityID];
    }

    public void GetComponentsFromEntity(Type[] componentTypes, out object[] components)
    {
        components = new object[componentTypes.Length];
        for (int i = 0; i < components.Length; i++)
        {
            components[i] = Convert.ChangeType(m_Containers[componentTypes[i]], componentTypes[i]);
        }
    }
}
