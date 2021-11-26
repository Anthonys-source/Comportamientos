using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityID : MonoBehaviour
{
    [SerializeField] private string m_IDName;
    private ID m_ID;

    public ID GetID()
    {
        if (!m_ID.IsInitialized())
            m_ID = new ID(m_IDName);
        return m_ID;
    }
}
