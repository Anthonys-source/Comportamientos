using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakerBehaviour : MonoBehaviour
{
    private CharacterActions m_Actions;
    private CharacterBlackboard m_Blackboard;
    private CharacterWaypoints m_Waypoints;

    private StateMachineEngine m_SM;
    private BehaviourTreeEngine m_BakeryBT;

    private ReturnValues m_BreadSold = ReturnValues.Running;
    private ReturnValues m_GetFlourState = ReturnValues.Running;
    private ReturnValues m_GetYeastState = ReturnValues.Running;
    private ReturnValues m_GetWaterState = ReturnValues.Running;
    private ReturnValues m_BakeBreadDoughState = ReturnValues.Running;
    private ReturnValues m_MakeBreadDoughState = ReturnValues.Running;
    private ReturnValues m_ArrivedAtBakery = ReturnValues.Running;

    private ComponentRegistry reg;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActions>();
        m_Blackboard = GetComponent<CharacterBlackboard>();
        m_Waypoints = GetComponent<CharacterWaypoints>();

        reg = ComponentRegistry.GetInst();
        // Init State Machine
    }

    private void Start()
    {
        m_BakeryBT = new BehaviourTreeEngine(false);
        var root = m_BakeryBT.CreateSelectorNode("root");
        m_BakeryBT.CreateLoopNode("b", root);
        var goToBakery = m_BakeryBT.CreateLeafNode("go to bakery", GoToBakery, ArrivedAtBakery);

        var sellToClient = m_BakeryBT.CreateSelectorNode("sell to client");
        var sellBread = m_BakeryBT.CreateLeafNode("sell bread", SellBread, BreadSold);

        var makeBread = m_BakeryBT.CreateSelectorNode("make bread");
        var getFlour = m_BakeryBT.CreateLeafNode("get flour", GetFlour, FlourAdquired);
        var getYeast = m_BakeryBT.CreateLeafNode("get yeast", GetYeast, YeastAdquired);
        var getWater = m_BakeryBT.CreateLeafNode("get water", GetWater, WaterAdquired);
        var bakeBreadDough = m_BakeryBT.CreateLeafNode("bake bread dough", BakeBreadDough, BreadDoughBaked);
        var makeBreadDough = m_BakeryBT.CreateLeafNode("make bread dough", MakeBreadDough, BreadDoughMade);


        root.AddChild(goToBakery);
        root.AddChild(sellToClient);
        root.AddChild(makeBread);

        sellToClient.AddChild(sellBread);

        makeBread.AddChild(bakeBreadDough);
        makeBread.AddChild(getFlour);
        makeBread.AddChild(getYeast);
        makeBread.AddChild(getWater);
        makeBread.AddChild(makeBreadDough);

        m_BakeryBT.SetRootNode(root);



        //m_SM = new StateMachineEngine(false);

        //State house = m_SM.CreateEntryState("atHouse", () => Debug.Log("Idle"));
        //State bakery = m_SM.CreateSubStateMachine("atBakery", m_BakeryBT);
        //var itsBakeryHour = m_SM.CreatePerception<ValuePerception>(() => { return ComponentRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>().m_Day >= 2; });
        //var itsHomeHour = m_SM.CreatePerception<ValuePerception>(() => { return ComponentRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>().m_Day < 2; });
        //m_SM.CreateTransition("house to bakery", house, itsBakeryHour, bakery);
        //m_SM.CreateTransition("bakery to house", bakery, itsHomeHour, house);
    }

    private void Update()
    {
        //m_SM.Update();
        m_BakeryBT.Update();
    }

    private void GetFlour()
    {
        m_GetFlourState = ReturnValues.Running;

        var a = reg.GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (a.HasItem(ItemID.FLOUR, out _) || m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.FLOUR) == null)
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

        var a = reg.GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (a.HasItem(ItemID.WATER, out _) || m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.WATER) == null)
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

        var a = reg.GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (a.HasItem(ItemID.YEAST, out _) || m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.YEAST) == null)
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

        var a = reg.GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (!a.HasItem(ItemID.BREAD_DOUGH, out _))
        {
            m_BakeBreadDoughState = ReturnValues.Failed;
        }
        else
        {
            m_Actions.MoveTo(m_Blackboard.m_WorkstationsInVision.Find((i) => i.m_ID == WorkstationID.BREAD_OVEN).m_Pos, 1.0f);
            var h = m_Actions.TryInteractWith(WorkstationID.BREAD_OVEN);
            h.OnCompletedEvent += () => { m_BakeBreadDoughState = ReturnValues.Succeed; m_Actions.GetHappy(); };
            h.OnFailedEvent += () => m_BakeBreadDoughState = ReturnValues.Failed;
        }
    }

    private void MakeBreadDough()
    {
        m_MakeBreadDoughState = ReturnValues.Running;

        var a = reg.GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
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

    private void SellBread()
    {
        m_BreadSold = ReturnValues.Running;

        var customerWaiting = reg.GetSingletonComponent<BakeryComponent>().m_IsCustomerWatingForBread;
        var inv = reg.GetComponentFromEntity<InventoryComponent>(GetComponent<EntityID>().GetInstID());
        if (!customerWaiting || !inv.HasItem(ItemID.BREAD, out _))
        {
            m_BreadSold = ReturnValues.Failed;
        }
        else
        {
            m_Actions.MoveTo(m_Blackboard.m_WorkstationsInVision.Find((i) => i.m_ID == WorkstationID.DIALOGUE_STARTER).m_Pos, 1.0f);
            var h = m_Actions.TryInteractWith(WorkstationID.DIALOGUE_STARTER);
            h.OnCompletedEvent += () => { m_BreadSold = ReturnValues.Succeed; m_Actions.GetHappy(); };
            h.OnFailedEvent += () => m_BreadSold = ReturnValues.Failed;
        }
    }

    private ReturnValues BreadSold() => m_BreadSold;
    private ReturnValues FlourAdquired() => m_GetFlourState;
    private ReturnValues WaterAdquired() => m_GetWaterState;
    private ReturnValues YeastAdquired() => m_GetYeastState;
    private ReturnValues BreadDoughBaked() => m_BakeBreadDoughState;
    private ReturnValues BreadDoughMade() => m_MakeBreadDoughState;
    private ReturnValues ArrivedAtBakery() => m_ArrivedAtBakery;
}
