using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityID : MonoBehaviour
{
    [SerializeField] private string InstanceID;
    [SerializeField] private string TypeID;
    private ID m_TypeID;
    private ID m_InstanceID;

    public ID GetTypeID()
    {
        if (!m_TypeID.IsInitialized())
            m_TypeID = new ID(TypeID);
        return m_TypeID;
    }

    public ID GetInstID()
    {
        if (!m_InstanceID.IsInitialized())
            m_InstanceID = new ID(InstanceID);
        return m_InstanceID;
    }
}
