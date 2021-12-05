using System;
using System.Collections.Generic;
using UnityEngine;

// Contains most of the useful game + world data, all the data its separated into what is called a "component"
[Serializable]
public class ComponentRegistry : Singleton<ComponentRegistry>
{
    private Dictionary<Type, object> m_Components = new Dictionary<Type, object>();
    private Dictionary<Type, object> m_SingletonComponents = new Dictionary<Type, object>();

    [SerializeField] private ComponentsContainer<EntityTypeComponent> m_EntityTypeComponent = new ComponentsContainer<EntityTypeComponent>();

    [SerializeField] private ComponentsContainer<CharacterComponent> m_CharacterComponents = new ComponentsContainer<CharacterComponent>();
    [SerializeField] private ComponentsContainer<MoodComponent> m_MoodComponents = new ComponentsContainer<MoodComponent>();
    [SerializeField] private ComponentsContainer<InventoryComponent> m_InventoryComponent = new ComponentsContainer<InventoryComponent>();

    [SerializeField] private ComponentsContainer<BreadBakerComponent> m_BreadBakerComponent = new ComponentsContainer<BreadBakerComponent>();

    [SerializeField] private ComponentsContainer<ItemTypeComponent> m_ItemTypeComponent = new ComponentsContainer<ItemTypeComponent>();
    [SerializeField] private ComponentsContainer<ItemWorldComponent> m_ItemWorldComponent = new ComponentsContainer<ItemWorldComponent>();

    [SerializeField] private ComponentsContainer<BuildingZoneComponent> m_BuildingZoneComponent = new ComponentsContainer<BuildingZoneComponent>();


    [SerializeField] private BakeryComponent m_BakersComponent = new BakeryComponent();
    [SerializeField] private DayNightCycleComponent m_DayNightCycleComponent = new DayNightCycleComponent();

    public void Initialize()
    {
        s_Instance = this;
        m_Components.Add(typeof(EntityTypeComponent), m_EntityTypeComponent);

        m_Components.Add(typeof(CharacterComponent), m_CharacterComponents);
        m_Components.Add(typeof(MoodComponent), m_MoodComponents);
        m_Components.Add(typeof(InventoryComponent), m_InventoryComponent);

        m_Components.Add(typeof(BreadBakerComponent), m_BreadBakerComponent);

        m_Components.Add(typeof(ItemTypeComponent), m_ItemTypeComponent);
        m_Components.Add(typeof(ItemWorldComponent), m_ItemWorldComponent);

        m_Components.Add(typeof(BuildingZoneComponent), m_BuildingZoneComponent);

        m_SingletonComponents.Add(typeof(BakeryComponent), m_BakersComponent);
        m_SingletonComponents.Add(typeof(DayNightCycleComponent), m_DayNightCycleComponent);
    }

    public ComponentsContainer<T> GetComponentsContainer<T>() where T : class
    {
        return m_Components[typeof(T)] as ComponentsContainer<T>;
    }

    public T GetSingletonComponent<T>() where T : class
    {
        return m_SingletonComponents[typeof(T)] as T;
    }

    public T GetComponentFromEntity<T>(ID entityID) where T : class
    {
        return (m_Components[typeof(T)] as ComponentsContainer<T>)[entityID];
    }

    public void GetComponentsFromEntity(Type[] componentTypes, out object[] components)
    {
        components = new object[componentTypes.Length];
        for (int i = 0; i < components.Length; i++)
        {
            components[i] = Convert.ChangeType(m_Components[componentTypes[i]], componentTypes[i]);
        }
    }
}
