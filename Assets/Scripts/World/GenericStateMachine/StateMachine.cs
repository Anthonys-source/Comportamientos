using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericStateMachine
{
    [Serializable]
    public class StateMachine
    {
        [SerializeField] private GState m_CurrentState;
        private Dictionary<ID, GState> m_StatesCache = new Dictionary<ID, GState>();


        public void Initialize(ID startingStateID)
        {
            m_CurrentState = m_StatesCache[startingStateID];

            foreach (GState state in m_StatesCache.Values)
                state.Initialize(this);
        }

        public void TransitionTo(ID newStateID)
        {
            m_CurrentState.OnStateExit();
            m_StatesCache.TryGetValue(newStateID, out GState newState);
            m_CurrentState = newState;
#if UNITY_EDITOR
            if (newState == null) Debug.LogError($"Couldn't find state to transition to with given ID [{newStateID.NameID}]");
#endif

            m_CurrentState.OnStateEnter();
        }

        public void AddState(ID stateID, GState state)
        {
            m_StatesCache.Add(stateID, state);
        }

        public void Update()
        {
            m_CurrentState.OnStateUpdate();
        }

        public void FixedUpdate()
        {
            m_CurrentState.OnStateFixedUpdate();
        }
    }
}