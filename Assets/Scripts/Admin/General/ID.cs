using System;
using UnityEngine;

[Serializable]
public struct ID
{
    public string NameID => m_NameID;

    [SerializeField] private string m_NameID;
    [SerializeField, HideInInspector] private int m_ID;
    [SerializeField, HideInInspector] private bool m_Initialized;

    public ID(string name)
    {
        m_ID = name.GetHashCode();

        m_NameID = name;
        m_Initialized = true;
    }

    public override int GetHashCode()
    {
        return m_ID;
    }

    public bool IsInitialized() => m_Initialized;

    public static bool operator ==(ID a, ID b)
    {
        if (a.m_ID == b.m_ID)
            return true;
        else
            return false;
    }

    public static bool operator !=(ID a, ID b)
    {
        if (a.m_ID != b.m_ID)
            return true;
        else
            return false;
    }

    public override bool Equals(object obj)
    {
        ID otherID = (ID)obj;
        if (m_ID == otherID.m_ID)
            return true;
        else
            return false;
    }
}
