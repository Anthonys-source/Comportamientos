using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterBlackboard : MonoBehaviour
{
    public event Action OnInitialized;

    public List<ItemBlackboard> m_ItemsInVision = new List<ItemBlackboard>();
    public List<ID> m_ItemsInRange = new List<ID>();

    public List<ItemBlackboard> m_WorkstationsInVision = new List<ItemBlackboard>();
    public List<ID> m_WorkstationsInRange = new List<ID>();

    public List<ID> m_CharactersInRange = new List<ID>();

    public ID m_CurrentZoneID;

    private InteractionsBehaviour _interactions;
    RaycastHit[] hits = new RaycastHit[10];
    private float updateTime = 0.0f;


    private void Awake()
    {
        _interactions = GetComponent<InteractionsBehaviour>();
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        UpdateItemsInInteractionRange();
        UpdateItemsInVisionRange();

        OnInitialized?.Invoke();
    }

    public void Update()
    {
        // Optimize
        updateTime += Time.deltaTime;
        if (updateTime > 0.8f)
        {
            UpdateItemsInInteractionRange();
            UpdateItemsInVisionRange();
            UpdateCurrentZone();
            updateTime = 0.0f;
        }
    }

    private void UpdateItemsInVisionRange()
    {
        m_ItemsInVision.Clear();
        m_WorkstationsInVision.Clear();
        hits = Physics.SphereCastAll(transform.position, 20.0f, new Vector3(0.1f, 0.1f, 0.1f));
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.TryGetComponent(out PickupItem item))
                m_ItemsInVision.Add(new ItemBlackboard(item.entityID.GetTypeID(), item.transform.position));
            else if (hits[i].collider.TryGetComponent(out WorkstationTag workstation))
                m_WorkstationsInVision.Add(new ItemBlackboard(workstation.GetComponent<EntityID>().GetTypeID(), workstation.transform.position)); // Janky
        }
    }

    private void UpdateItemsInInteractionRange()
    {
        var itm = _interactions.GetInteractablesInRange();
        m_ItemsInRange.Clear();
        for (int i = 0; i < itm.Count; i++)
            m_ItemsInRange.Add(itm[i].m_EntityID.GetTypeID());
    }

    private void UpdateCurrentZone()
    {
        RaycastHit[] hits = new RaycastHit[25];
        hits = Physics.SphereCastAll(transform.position, 10.0f, new Vector3(0.1f, 0.1f, 0.1f));
        for (int i = 0; i < hits.Length; i++)
            if (hits[i].collider.TryGetComponent(out BuildingZone zone))
                m_CurrentZoneID = zone.GetZoneID();
    }
}

public class ItemBlackboard
{
    public ID m_ID;
    public Vector3 m_Pos;

    public ItemBlackboard(ID iD, Vector3 pos)
    {
        m_ID = iD;
        m_Pos = pos;
    }
}