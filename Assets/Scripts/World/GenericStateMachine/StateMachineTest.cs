using System.Collections;
using UnityEngine;
using GenericStateMachine;


public class StateMachineTest : MonoBehaviour
{
    private StateMachine m_Sm = new StateMachine();

    private void Awake()
    {
        var s = new GState();
        s.AddAction(new LogAction { m_LoggerName = "Logger A" });

        var t = new GTransition();
        t.AddCondition(new TimerCondition());
        t.m_TargetStateID = new ID("state_b");
        s.AddTransition(t);

        m_Sm.AddState(new ID("state_a"), s);


        s = new GState();
        s.AddAction(new LogAction { m_LoggerName = "Logger B" });

        t = new GTransition();
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
