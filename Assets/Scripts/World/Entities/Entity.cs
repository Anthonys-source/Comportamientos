using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityID))]
public class Entity : MonoBehaviour
{
    private ID m_CharacterID;
    private Dictionary<Type, object> m_Components = new Dictionary<Type, object>();

    public T GetEntityComponent<T>() where T : class
    {
        return (m_Components[typeof(T)] as T);
    }
}