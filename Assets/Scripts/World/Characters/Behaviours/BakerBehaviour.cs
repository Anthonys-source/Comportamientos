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

    private ReturnValues m_GetIngredientsState = ReturnValues.Running;
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
        var getIngredients = m_BT.CreateLeafNode("get ingredients", GetIngredients, IngredientsAdquired);
        var bakeBread = m_BT.CreateLeafNode("bake bread", BakeBread, BreadBaked);

        hourSelector.AddChild(goToBakery);
        hourSelector.AddChild(makeBread);

        makeBread.AddChild(getIngredients);
        makeBread.AddChild(bakeBread);

        m_BT.SetRootNode(hourSelector);
    }

    private void Update()
    {
        m_BT.Update();
    }

    private void GetIngredients()
    {
        m_GetIngredientsState = ReturnValues.Running;

        m_Actions.MoveTo(m_Blackboard.m_ItemsInVision.Find((i) => i.m_ID == ItemID.FLOUR).m_Pos, 1.0f);
        var h = m_Actions.TryInteractWith(ItemID.FLOUR);

        h.OnFailedEvent += () => m_GetIngredientsState = ReturnValues.Failed;
        h.OnCompletedEvent += () => m_GetIngredientsState = ReturnValues.Succeed;
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

        var h = m_Actions.MoveTo(m_Waypoints.GetWaypointPosition(WaypointID.BAKERY), 1.0f);

        // Check if already at bakery
        var ve = (transform.position - m_Waypoints.GetWaypointPosition(WaypointID.BAKERY));
        ve.y = 0.0f;
        if (ve.magnitude < 0.5f)
            m_ArrivedAtBakery = ReturnValues.Failed;
        else
            h.OnCompletedEvent += () => m_ArrivedAtBakery = ReturnValues.Succeed;
    }

    private ReturnValues IngredientsAdquired()
    {
        return m_GetIngredientsState;
    }

    private ReturnValues BreadBaked()
    {
        return m_MakeBreadState;
    }

    private ReturnValues ArrivedAtBakery()
    {
        return m_ArrivedAtBakery;
    }
}
