using System;

namespace GenericStateMachine
{
    [Serializable]
    public abstract class Action
    {
        public abstract void Initialize(StateMachine sm);

        public abstract void OnStateEnter();

        public abstract void OnStateExit();

        public abstract void OnStateFixedUpdate();

        public abstract void OnStateUpdate();
    }
}