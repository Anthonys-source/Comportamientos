using System.Collections;
using UnityEngine;
using GenericStateMachine;


public class StateMachineTest : MonoBehaviour
{
    private StateMachine m_Sm = new StateMachine();

    private void Awake()
    {
        var s = new State();
        s.AddAction(new LogAction { m_LoggerName = "Logger A" });

        var t = new Transition();
        t.AddCondition(new TimerCondition());
        t.m_TargetStateID = new ID("state_b");
        s.AddTransition(t);

        m_Sm.AddState(new ID("state_a"), s);


        s = new State();
        s.AddAction(new LogAction { m_LoggerName = "Logger B" });

        t = new Transition();
        t.AddCondition(new TimerCondition());
        t.m_TargetStateID = new ID("state_a");
        s.AddTransition(t);

        m_Sm.AddState(new ID("state_b"), s);

        m_Sm.Initialize(new ID("state_a"));
    }

    private void Update()
    {
        m_Sm.Update();
    }
}

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