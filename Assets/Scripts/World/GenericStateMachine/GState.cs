using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenericStateMachine
{
    [Serializable]
    public class GState
    {
        [SerializeField] private List<Action> m_ActionsList = new List<Action>();
        [SerializeField] private List<GTransition> m_TransitionsList = new List<GTransition>();


        public void AddAction(Action action) => m_ActionsList.Add(action);
        public void AddTransition(GTransition transition) => m_TransitionsList.Add(transition);

        public void Initialize(StateMachine sm)
        {
            for (int i = 0; i < m_ActionsList.Count; i++)
                m_ActionsList[i].Initialize(sm);

            for (int i = 0; i < m_TransitionsList.Count; i++)
                m_TransitionsList[i].Initialize(sm);
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
        }

        public void OnStateExit()
        {
            for (int i = 0; i < m_ActionsList.Count; i++)
                m_ActionsList[i].OnStateExit();

            for (int i = 0; i < m_TransitionsList.Count; i++)
                m_TransitionsList[i].OnStateExit();
        }
    }
}