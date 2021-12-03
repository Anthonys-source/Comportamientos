using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakerBehaviour : MonoBehaviour
{
    private CharacterActions m_Actions;
    private BehaviourTreeEngine m_BT;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActions>();

        // Init State Machine
    }

    private void Start()
    {
        // Init State Machine / Behaviour Tree
        var evtSys = EventSystem.GetInst().GetGlobal();

        m_BT = new BehaviourTreeEngine(false);
        var root = m_BT.CreateSelectorNode("selector day hour");
    }
}
