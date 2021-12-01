using UnityEngine;
using GenericStateMachine;

public class TimerCondition : Condition
{
    private float m_TimeElapsed = 0.0f;

    public override void Initialize(StateMachine sm)
    {
    }

    public override void OnStateEnter()
    {
        m_TimeElapsed = 0.0f;
    }

    public override void OnStateExit()
    {
    }

    public override bool CheckCondition()
    {
        m_TimeElapsed += Time.deltaTime;

        if (m_TimeElapsed > 1.0f)
            return true;
        else
            return false;
    }
}