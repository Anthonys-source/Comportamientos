using UnityEngine;
using GenericStateMachine;

public class LogAction : Action
{
    public string m_LoggerName;

    public override void Initialize(StateMachine sm)
    {
        Debug.Log($"Initialize Log [{m_LoggerName}]");
    }

    public override void OnStateEnter()
    {
        Debug.Log($"State Enter Log [{m_LoggerName}]");
    }

    public override void OnStateExit()
    {
        Debug.Log($"State Exit Log [{m_LoggerName}]");
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        Debug.Log($"Update Log [{m_LoggerName}]");
    }
}
