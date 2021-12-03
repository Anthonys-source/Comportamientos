using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakerBehaviour : MonoBehaviour
{
    private CharacterActions m_Actions;
    private CharacterBlackboard m_Blackboard;

    private BehaviourTreeEngine m_BT;

    private ReturnValues m_GetIngredientsState = ReturnValues.Running;
    private ReturnValues m_MakeBreadState = ReturnValues.Running;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActions>();
        m_Blackboard = GetComponent<CharacterBlackboard>();

        // Init State Machine
    }

    private void Start()
    {
        // Init State Machine / Behaviour Tree
        var evtSys = EventSystem.GetInst().GetGlobal();

        m_BT = new BehaviourTreeEngine(false);

        var hourSelector = m_BT.CreateSelectorNode("hour selector");

        var makeBread = m_BT.CreateSequenceNode("make bread", false);
        m_BT.CreateLoopNode("b", makeBread);
        var timePerception = m_BT.CreatePerception<ValuePerception>(() => ComponentRegistry.GetInst().GetSingletonComponent<Singleton_DayNightCycleComponent>().m_Hour > 2.0f);
        var getIngredients = m_BT.CreateLeafNode("get ingredients", GetIngredients, IngredientsAdquired);
        var bakeBread = m_BT.CreateLeafNode("bake bread", BakeBread, BreadBaked);


        makeBread.AddChild(getIngredients);
        makeBread.AddChild(bakeBread);

        m_BT.SetRootNode(makeBread);
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
        h.OnCompletedEvent += () => m_GetIngredientsState = ReturnValues.Succeed;
    }

    private void BakeBread()
    {
        m_MakeBreadState = ReturnValues.Running;
        m_Actions.MoveTo(m_Blackboard.m_WorkstationsInVision.Find((i) => i.m_ID == WorkstationID.BREAD_OVEN).m_Pos, 1.0f);
        var h = m_Actions.TryInteractWith(WorkstationID.BREAD_OVEN);
        h.OnCompletedEvent += () => m_MakeBreadState = ReturnValues.Succeed;
    }

    private ReturnValues IngredientsAdquired()
    {
        return m_GetIngredientsState;
    }

    private ReturnValues BreadBaked()
    {
        return m_MakeBreadState;
    }
}
