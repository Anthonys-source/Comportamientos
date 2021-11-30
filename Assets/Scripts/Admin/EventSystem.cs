using System;

[Serializable]
public class EventSystem : Singleton<EventSystem>
{
    private EventSys m_GlobalEventSystem;


    public void Initialize()
    {
        s_Instance = this;
        m_GlobalEventSystem = new EventSys();
    }

    public EventSys GetGlobal()
    {
        return m_GlobalEventSystem;
    }
}
