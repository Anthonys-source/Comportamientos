using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericStateMachine;

public class CharacterFSM : MonoBehaviour
{
    private CharacterActionsController m_Actions;
    [SerializeField] private StateMachine m_StateMachine;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActionsController>();
        m_StateMachine = new StateMachine();
    }

    private void Start()
    {
        // Init State Machine / Behaviour Tree

        // Examples

        // Get Current Day Hour
        // ComponentsRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>().m_Day;

        // Move Character
        // _actions.MoveTo(targetPos, 1);
        GameEventSystem.GetInst().GetGlobalEventSystem().AddEventChannel<int>(EventIDs.baker_goes_to_sleep);
        GameEventSystem.GetInst().GetGlobalEventSystem().GetEventChannel(EventIDs.baker_goes_to_sleep, out EventChannel<int> evt);

        // Event On Character Moved
        // _actions.OnArriveAt += Function;
    }
}
