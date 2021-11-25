using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericStateMachine
{
    [Serializable]
    public class StateMachine
    {
        [SerializeField] private State m_CurrentState;
        private Dictionary<ID, State> m_StatesCache;


        public void Initialize(ID startingStateID)
        {
            m_CurrentState = m_StatesCache[startingStateID];

            foreach (State state in m_StatesCache.Values)
                state.Initialize(this);
        }

        public void TransitionTo(ID newStateID)
        {
            m_CurrentState.OnStateExit();
            m_StatesCache.TryGetValue(newStateID, out State newState);

#if UNITY_EDITOR
            if (newState == null) Debug.LogError($"Couldn't find state to transition to with given ID [{newStateID.NameID}]");
#endif

            m_CurrentState.OnStateEnter();
        }

        public void AddState(ID stateID, State state)
        {
            m_StatesCache.Add(stateID, state);
        }
    }
}