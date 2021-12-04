using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BuildingZone : MonoBehaviour
{
    [SerializeField] private string m_IDName;
    private BoxCollider _bc;

    private void Awake()
    {
        _bc = GetComponent<BoxCollider>();
    }

    public ID GetZoneID()
    {
        return new ID(m_IDName);
    }
}
