using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightCycleSystem : BaseSystem
{
    [SerializeField] private DayNightCycleComponent _dayNightCycleComponent;

    public override void Initialize(ComponentRegistry c, EventSystem e)
    {
        _dayNightCycleComponent = c.GetSingletonComponent<DayNightCycleComponent>();
    }

    public override void Update(float timeStep)
    {
        var c = _dayNightCycleComponent;
        c.m_Second += timeStep;
        if (c.m_Second >= 60.0f)
        {
            c.m_Minute += 1;
            if (c.m_Minute >= 60)
            {
                c.m_Hour += 1;
                if (c.m_Hour >= 24)
                {
                    c.m_Day += 1;
                }
            }
        }
    }
}
