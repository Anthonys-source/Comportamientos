using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterBlackboard : MonoBehaviour
{
    public List<ItemBlackboard> m_ItemsInVisionRange = new List<ItemBlackboard>();
    public List<ID> m_ItemsInInteractionRange = new List<ID>();

    public List<ID> m_CharactersInRange = new List<ID>();

    private InteractionsBehaviour _interactions;
    RaycastHit[] hits = new RaycastHit[10];
    private float updateTime = 0.0f;

    private void Awake()
    {
        _interactions = GetComponent<InteractionsBehaviour>();
    }

    public void Update()
    {
        // Optimize
        updateTime += Time.deltaTime;
        if (updateTime > 0.8f)
        {
            UpdateItemsInInteractionRange();
            UpdateItemsInVisionRange();
            updateTime = 0.0f;
        }
    }

    private void UpdateItemsInVisionRange()
    {
        m_ItemsInVisionRange.Clear();
        hits = Physics.SphereCastAll(transform.position, 10.0f, new Vector3(0.1f, 0.1f, 0.1f));
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.TryGetComponent(out PickupItem item))
                m_ItemsInVisionRange.Add(new ItemBlackboard(item.entityID.GetID(), item.transform.position));
        }
    }

    private void UpdateItemsInInteractionRange()
    {
        var itm = _interactions.GetInteractablesInRange();
        m_ItemsInInteractionRange.Clear();
        for (int i = 0; i < itm.Count; i++)
            m_ItemsInInteractionRange.Add(itm[i].m_EntityID.GetID());
    }
}

public struct ItemBlackboard
{
    public ID m_ID;
    public Vector3 m_Pos;

    public ItemBlackboard(ID iD, Vector3 pos)
    {
        m_ID = iD;
        m_Pos = pos;
    }
}