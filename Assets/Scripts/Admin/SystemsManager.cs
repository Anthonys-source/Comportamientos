using System;

[Serializable]
public class SystemsManager
{
    private InventorySystem m_InventorySystem = new InventorySystem();
    private DayNightCycleSystem m_DayNightCycleSystem = new DayNightCycleSystem();

    public void Initialize(ComponentRegistry c, EventSystem e)
    {
        m_InventorySystem.Initialize(c, e);
        m_DayNightCycleSystem.Initialize(c, e);
    }

    public void Update(float timeStep)
    {
        m_DayNightCycleSystem.Update(timeStep);
    }
}
