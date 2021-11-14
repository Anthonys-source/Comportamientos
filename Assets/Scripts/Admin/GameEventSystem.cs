using System;

[Serializable]
public class GameEventSystem : Singleton<GameEventSystem>
{
    private EventSys m_GlobalEventSystem;


    public void Initialize()
    {
        s_Instance = this;
        m_GlobalEventSystem = new EventSys();
    }

    public EventSys GetGlobalEventSystem()
    {
        return m_GlobalEventSystem;
    }
}
