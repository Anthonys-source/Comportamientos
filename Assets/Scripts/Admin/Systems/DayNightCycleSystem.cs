using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayNightCycleSystem : MonoBehaviour
{
    [SerializeField] private Singleton_DayNightCycleComponent _dayNightCycleComponent = new Singleton_DayNightCycleComponent();


    private void Update()
    {
        var c = _dayNightCycleComponent;
        c.m_Second += Time.deltaTime;
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
