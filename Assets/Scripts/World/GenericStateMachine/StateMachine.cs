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
        }

        public void TransitionTo(ID newStateID)
        {
            m_CurrentState.OnStateExit();
            m_StatesCache.TryGetValue(newStateID, out State newState);

#if UNITY_EDITOR
            if (newState == null) Debug.LogError("Couldn't find ");
#endif

            m_CurrentState.OnStateEnter();
        }

        public void AddState(ID stateID, State state)
        {
            m_StatesCache.Add(stateID, state);
        }
    }


    [Serializable]
    public class State
    {
        [SerializeField] private List<Action> m_ActionsList = new List<Action>();
        [SerializeField] private List<Transition> m_TransitionsList = new List<Transition>();


        public void Initialize()
        {
            for (int i = 0; i < m_ActionsList.Count; i++)
                m_ActionsList[i].Initialize();

            for (int i = 0; i < m_TransitionsList.Count; i++)
                m_TransitionsList[i].Initialize();
        }

        public void OnStateEnter()
        {
            for (int i = 0; i < m_ActionsList.Count; i++)
                m_ActionsList[i].OnStateEnter();

            for (int i = 0; i < m_TransitionsList.Count; i++)
                m_TransitionsList[i].OnStateEnter();
        }

        public void OnStateUpdate()
        {
            for (int i = 0; i < m_ActionsList.Count; i++)
                m_ActionsList[i].OnStateUpdate();

            for (int i = 0; i < m_TransitionsList.Count; i++)
                m_TransitionsList[i].OnStateUpdate();
        }

        public void OnStateFixedUpdate()
        {
            for (int i = 0; i < m_ActionsList.Count; i++)
                m_ActionsList[i].OnStateFixedUpdate();

            for (int i = 0; i < m_TransitionsList.Count; i++)
                m_TransitionsList[i].OnStateFixedUpdate();
        }

        public void OnStateExit()
        {
            for (int i = 0; i < m_ActionsList.Count; i++)
                m_ActionsList[i].OnStateExit();

            for (int i = 0; i < m_TransitionsList.Count; i++)
                m_TransitionsList[i].OnStateExit();
        }
    }


    [Serializable]
    public abstract class Action
    {
        public void Initialize()
        {

        }

        public void OnStateEnter()
        {
        }

        public void OnStateExit()
        {
        }

        public void OnStateFixedUpdate()
        {
        }

        public void OnStateUpdate()
        {
        }
    }


    [Serializable]
    public class Transition
    {
        public void Initialize()
        {

        }

        public void OnStateEnter()
        {
        }

        public void OnStateExit()
        {
        }

        public void OnStateFixedUpdate()
        {
        }

        public void OnStateUpdate()
        {
        }
    }


    [Serializable]
    public abstract class Condition
    {
        public void Initialize()
        {

        }

        public void OnStateEnter()
        {
        }

        public void OnStateExit()
        {
        }

        public void OnStateFixedUpdate()
        {
        }

        public void OnStateUpdate()
        {
        }
    }

    public interface StateComponent
    {
        public void OnStateEnter();
        public void OnStateUpdate();
        public void OnStateFixedUpdate();
        public void OnStateExit();
    }
}