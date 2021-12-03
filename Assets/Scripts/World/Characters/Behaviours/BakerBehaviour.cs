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
    private ReturnValues m_MakeBreadState = ReturnValues.Running;
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

        var makeBread = m_BT.CreateSequenceNode("make bread", false);
        var getFlour = m_BT.CreateLeafNode("get flour", GetFlour, FlourAdquired);
        var getYeast = m_BT.CreateLeafNode("get yeast", GetYeast, YeastAdquired);
        var getWater = m_BT.CreateLeafNode("get water", GetWater, WaterAdquired);
        var bakeBread = m_BT.CreateLeafNode("bake bread", BakeBread, BreadBaked);

        hourSelector.AddChild(goToBakery);
        hourSelector.AddChild(makeBread);

        makeBread.AddChild(getFlour);
        makeBread.AddChild(getYeast);
        makeBread.AddChild(getWater);
        makeBread.AddChild(bakeBread);

        m_BT.SetRootNode(hourSelector);
    }

    private void Update()
    {
        m_BT.Update();
    }

    private void GetFlour()
    {
        m_GetFlourState = ReturnValues.Running;

        m_Actions.MoveTo(m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.FLOUR).m_Pos, 1.0f);
        var h = m_Actions.TryInteractWith(ItemID.FLOUR);

        h.OnFailedEvent += () => m_GetFlourState = ReturnValues.Failed;
        h.OnCompletedEvent += () => m_GetFlourState = ReturnValues.Succeed;
    }

    private void GetWater()
    {
        m_GetWaterState = ReturnValues.Running;

        m_Actions.MoveTo(m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.WATER).m_Pos, 1.0f);
        var h = m_Actions.TryInteractWith(ItemID.WATER);

        h.OnFailedEvent += () => m_GetWaterState = ReturnValues.Failed;
        h.OnCompletedEvent += () => m_GetWaterState = ReturnValues.Succeed;
    }

    private void GetYeast()
    {
        m_GetYeastState = ReturnValues.Running;

        m_Actions.MoveTo(m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.YEAST).m_Pos, 1.0f);
        var h = m_Actions.TryInteractWith(ItemID.YEAST);

        h.OnFailedEvent += () => m_GetYeastState = ReturnValues.Failed;
        h.OnCompletedEvent += () => m_GetYeastState = ReturnValues.Succeed;
    }

    private void BakeBread()
    {
        m_MakeBreadState = ReturnValues.Running;

        m_Actions.MoveTo(m_Blackboard.m_WorkstationsInVision.Find((i) => i.m_ID == WorkstationID.BREAD_OVEN).m_Pos, 1.0f);
        var h = m_Actions.TryInteractWith(WorkstationID.BREAD_OVEN);
        h.OnCompletedEvent += () => m_MakeBreadState = ReturnValues.Succeed;
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
    private ReturnValues BreadBaked() => m_MakeBreadState;
    private ReturnValues ArrivedAtBakery() => m_ArrivedAtBakery;
}
