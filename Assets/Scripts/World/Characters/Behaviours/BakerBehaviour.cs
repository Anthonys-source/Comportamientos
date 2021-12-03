using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakerBehaviour : MonoBehaviour
{
    private CharacterActions m_Actions;
    private CharacterBlackboard m_Blackboard;
    private CharacterWaypoints m_Waypoints;

    private BehaviourTreeEngine m_BT;

    private ReturnValues m_GetFlourState = ReturnValues.Running;
    private ReturnValues m_GetYeastState = ReturnValues.Running;
    private ReturnValues m_GetWaterState = ReturnValues.Running;
    private ReturnValues m_BakeBreadDoughState = ReturnValues.Running;
    private ReturnValues m_MakeBreadDoughState = ReturnValues.Running;
    private ReturnValues m_ArrivedAtBakery = ReturnValues.Running;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActions>();
        m_Blackboard = GetComponent<CharacterBlackboard>();
        m_Waypoints = GetComponent<CharacterWaypoints>();

        // Init State Machine
    }

    private void Start()
    {
        // Init State Machine / Behaviour Tree
        var evtSys = EventSystem.GetInst().GetGlobal();

        m_BT = new BehaviourTreeEngine(false);

        var hourSelector = m_BT.CreateSelectorNode("hour selector");
        m_BT.CreateLoopNode("b", hourSelector);

        var goToBakery = m_BT.CreateLeafNode("go to bakery", GoToBakery, ArrivedAtBakery);

        var makeBread = m_BT.CreateSelectorNode("make bread");
        var getFlour = m_BT.CreateLeafNode("get flour", GetFlour, FlourAdquired);
        var getYeast = m_BT.CreateLeafNode("get yeast", GetYeast, YeastAdquired);
        var getWater = m_BT.CreateLeafNode("get water", GetWater, WaterAdquired);
        var bakeBreadDough = m_BT.CreateLeafNode("bake bread dough", BakeBreadDough, BreadDoughBaked);
        var makeBreadDough = m_BT.CreateLeafNode("make bread dough", MakeBreadDough, BreadDoughMade);

        hourSelector.AddChild(goToBakery);
        hourSelector.AddChild(makeBread);

        makeBread.AddChild(bakeBreadDough);
        makeBread.AddChild(getFlour);
        makeBread.AddChild(getYeast);
        makeBread.AddChild(getWater);
        makeBread.AddChild(makeBreadDough);

        m_BT.SetRootNode(hourSelector);
    }

    private void Update()
    {
        m_BT.Update();
    }

    private void GetFlour()
    {
        m_GetFlourState = ReturnValues.Running;

        var a = ComponentRegistry.GetInst().GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (a.HasItem(ItemID.FLOUR, out _))
        {
            m_GetFlourState = ReturnValues.Failed;
        }
        else
        {
            m_Actions.MoveTo(m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.FLOUR).m_Pos, 1.0f);
            var h = m_Actions.TryInteractWith(ItemID.FLOUR);

            h.OnFailedEvent += () => m_GetFlourState = ReturnValues.Failed;
            h.OnCompletedEvent += () => m_GetFlourState = ReturnValues.Succeed;
        }
    }

    private void GetWater()
    {
        m_GetWaterState = ReturnValues.Running;

        var a = ComponentRegistry.GetInst().GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (a.HasItem(ItemID.WATER, out _))
        {
            m_GetWaterState = ReturnValues.Failed;
        }
        else
        {
            m_Actions.MoveTo(m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.WATER).m_Pos, 1.0f);
            var h = m_Actions.TryInteractWith(ItemID.WATER);

            h.OnFailedEvent += () => m_GetWaterState = ReturnValues.Failed;
            h.OnCompletedEvent += () => m_GetWaterState = ReturnValues.Succeed;
        }
    }

    private void GetYeast()
    {
        m_GetYeastState = ReturnValues.Running;

        var a = ComponentRegistry.GetInst().GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (a.HasItem(ItemID.YEAST, out _))
        {
            m_GetYeastState = ReturnValues.Failed;
        }
        else
        {
            m_Actions.MoveTo(m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.YEAST).m_Pos, 1.0f);
            var h = m_Actions.TryInteractWith(ItemID.YEAST);

            h.OnFailedEvent += () => m_GetYeastState = ReturnValues.Failed;
            h.OnCompletedEvent += () => m_GetYeastState = ReturnValues.Succeed;
        }
    }

    private void BakeBreadDough()
    {
        m_BakeBreadDoughState = ReturnValues.Running;

        var a = ComponentRegistry.GetInst().GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (!a.HasItem(ItemID.BREAD_DOUGH, out _))
        {
            m_BakeBreadDoughState = ReturnValues.Failed;
        }
        else
        {
            m_Actions.MoveTo(m_Blackboard.m_WorkstationsInVision.Find((i) => i.m_ID == WorkstationID.BREAD_OVEN).m_Pos, 1.0f);
            var h = m_Actions.TryInteractWith(WorkstationID.BREAD_OVEN);
            h.OnCompletedEvent += () => m_BakeBreadDoughState = ReturnValues.Succeed;
            h.OnFailedEvent += () => m_BakeBreadDoughState = ReturnValues.Failed;
        }
    }

    private void MakeBreadDough()
    {
        m_MakeBreadDoughState = ReturnValues.Running;

        var a = ComponentRegistry.GetInst().GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (!a.HasItem(ItemID.YEAST, out _) || !a.HasItem(ItemID.WATER, out _) || !a.HasItem(ItemID.FLOUR, out _))
        {
            m_MakeBreadDoughState = ReturnValues.Failed;
        }
        else
        {
            m_Actions.MoveTo(m_Blackboard.m_WorkstationsInVision.Find((i) => i.m_ID == WorkstationID.DOUGH_TABLE).m_Pos, 1.0f);
            var h = m_Actions.TryInteractWith(WorkstationID.DOUGH_TABLE);
            h.OnCompletedEvent += () => m_MakeBreadDoughState = ReturnValues.Succeed;
            h.OnFailedEvent += () => m_MakeBreadDoughState = ReturnValues.Failed;
        }
    }

    private void GoToBakery()
    {
        m_ArrivedAtBakery = ReturnValues.Running;

        if (m_Blackboard.m_CurrentZoneID == ZoneID.BAKERY)
            m_ArrivedAtBakery = ReturnValues.Failed;
        else
        {
            var h = m_Actions.MoveTo(m_Waypoints.GetWaypointPosition(WaypointID.BAKERY), 1.0f);
            h.OnCompletedEvent += () => m_ArrivedAtBakery = ReturnValues.Succeed;
        }
    }


    private ReturnValues FlourAdquired() => m_GetFlourState;
    private ReturnValues WaterAdquired() => m_GetWaterState;
    private ReturnValues YeastAdquired() => m_GetYeastState;
    private ReturnValues BreadDoughBaked() => m_BakeBreadDoughState;
    private ReturnValues BreadDoughMade() => m_MakeBreadDoughState;
    private ReturnValues ArrivedAtBakery() => m_ArrivedAtBakery;
}
