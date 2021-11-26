using System;

[Serializable]
public class SystemsManager
{
    private InventorySystem m_InventorySystem = new InventorySystem();

    public void Initialize(ComponentsRegistry c, GameEventSystem e)
    {
        m_InventorySystem.Initialize(c, e);
    }

    public void Update(float timeStep)
    {
    }
}
