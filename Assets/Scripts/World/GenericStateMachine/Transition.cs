using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenericStateMachine
{
    [Serializable]
    public class Transition
    {
        public ID m_TargetStateID;
        [SerializeField] private List<Condition> m_ConditionsList = new List<Condition>();
        [NonSerialized] private StateMachine m_Sm;

        public void AddCondition(Condition condition) => m_ConditionsList.Add(condition);

        public void Initialize(StateMachine sm)
        {
            m_Sm = sm;
        }

        public void OnStateEnter()
        {
            for (int i = 0; i < m_ConditionsList.Count; i++)
                m_ConditionsList[i].OnStateEnter();
        }

        public void OnStateExit()
        {
            for (int i = 0; i < m_ConditionsList.Count; i++)
                m_ConditionsList[i].OnStateExit();
        }

        public void OnStateUpdate()
        {
            bool shouldTransition = true;
            for (int i = 0; i < m_ConditionsList.Count; i++)
            {
                if (!m_ConditionsList[i].CheckCondition())
                    shouldTransition = false;
            }
            if (shouldTransition)
                m_Sm.TransitionTo(m_TargetStateID);
        }
    }
}