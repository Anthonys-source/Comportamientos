using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFSM : MonoBehaviour
{
    private CharacterActionsController _actions;


    private void Awake()
    {
        _actions = GetComponent<CharacterActionsController>();
    }

    private void Start()
    {
        // Init State Machine / Behaviour Tree

        // Examples

        // Get Current Day Hour
        // ComponentsRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>().m_Day;

        // Move Character
        // _actions.MoveTo(targetPos, 1);
        GameEventSystem.GetInst().GetGlobalEventSystem().AddEventChannel<int>(new ID("baker_goes_to_sleep"));
        GameEventSystem.GetInst().GetGlobalEventSystem().GetEventChannel(new ID("baker_goes_to_sleep"), out EventChannel<int> evt);

        // Event On Character Moved
        // _actions.OnArriveAt += Function;
    }
}
