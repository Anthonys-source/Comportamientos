using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSimplePickupBehaviour : MonoBehaviour
{
    private CharacterBlackboard _blackboard;
    private CharacterActions _actions;


    private void Awake()
    {
        _blackboard = GetComponent<CharacterBlackboard>();
        _actions = GetComponent<CharacterActions>();
    }

    private void Update()
    {
        var items = _blackboard.m_ItemsInVisionRange;
        if (items.Count > 0)
        {
            if (!_actions.InProgress)
            {
                _actions.MoveTo(items[0].m_Pos, 1.0f);
                _actions.TryInteractWith(items[0].m_ID);
            }
        }
    }
}
