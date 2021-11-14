using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ComponentsContainer<T>
{
    private Dictionary<ID, T> m_ComponentsHashMap = new Dictionary<ID, T>();
    [SerializeField] private List<T> m_ComponentsList = new List<T>();

    public T this[ID id]
    {
        get => m_ComponentsHashMap[id];
        set => m_ComponentsHashMap[id] = value;
    }

    public void Add(ID id, T component)
    {
        m_ComponentsHashMap.Add(id, component);

        m_ComponentsList.Add(component);
    }

    public List<T> GetList()
    {
        return m_ComponentsList;
    }

    public T GetFromEntity(ID entityID)
    {
        return m_ComponentsHashMap[entityID];
    }
}
