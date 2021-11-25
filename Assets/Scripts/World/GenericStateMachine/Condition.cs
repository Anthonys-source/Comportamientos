using System;

namespace GenericStateMachine
{
    [Serializable]
    public abstract class Condition
    {
        public bool m_ExpectedResult = false;

        public abstract void Initialize(StateMachine sm);

        public abstract void OnStateEnter();

        public abstract void OnStateExit();

        public abstract bool CheckCondition();
    }
}