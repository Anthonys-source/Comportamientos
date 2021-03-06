using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericStateMachine;


public class CharacterFSM : MonoBehaviour
{
    private CharacterActions m_Actions;
    [SerializeField] private StateMachine m_StateMachine;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActions>();

        m_StateMachine = new StateMachine();
        // Init State Machine
    }

    private void Start()
    {
        // Init State Machine / Behaviour Tree

        // Examples
        // Get Current Day Hour
        // ComponentsRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>().m_Day;

        // Move Character
        // _actions.MoveTo(targetPos, 1);
        EventSystem.GetInst().GetGlobal().AddEventChannel<int>(EventID.BAKER_GOES_TO_SLEEP);
        EventSystem.GetInst().GetGlobal().GetEventChannel(EventID.BAKER_GOES_TO_SLEEP, out EventChannel<int> evt);

        // Event On Character Moved
        // _actions.OnArriveAt += Function;
    }
}
