using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSys
{
    private Dictionary<ID, object> m_Events = new Dictionary<ID, object>();


    public EventChannel<T> AddEventChannel<T>(ID evtId)
    {
#if UNITY_EDITOR
        if (m_Events.ContainsKey(evtId))
        {
            Debug.LogError("Collision between event id's");
        }
#endif

        EventChannel<T> evt = new EventChannel<T>();
        m_Events.Add(evtId, evt);
        return evt;
    }

    public EventChannelVoid AddEventChannel(ID evtId)
    {
#if UNITY_EDITOR
        if (m_Events.ContainsKey(evtId))
        {
            Debug.LogError("Collision between event id's");
        }
#endif

        EventChannelVoid evt = new EventChannelVoid();
        m_Events.Add(evtId, evt);
        return evt;
    }

    public bool GetEventChannel<T>(ID evtId, out T evt) where T : class
    {
        if (m_Events.TryGetValue(evtId, out object evtObj))
        {
            evt = evtObj as T;
            return true;
        }
        else
        {
            evt = null;
            Debug.LogError("Event not found");
            return false;
        }
    }

    public bool GetEventChannel(ID evtId, out EventChannelVoid evt)
    {
        if (m_Events.TryGetValue(evtId, out object evtObj))
        {
            evt = evtObj as EventChannelVoid;
            return true;
        }
        else
        {
            evt = null;
            Debug.LogError("Event not found");
            return false;
        }
    }
}


public class EventChannel<T>
{
    public event Action<T> OnInvoked;

    public void Invoke(T arg)
    {
        OnInvoked?.Invoke(arg);
    }

    public void LogListeners()
    {
        var delegates = OnInvoked.GetInvocationList();
        foreach (var del in delegates)
        {
            Debug.Log(del.Target.ToString());
        }
    }
}


public class EventChannelVoid
{
    public event Action OnInvoked;

    public void Invoke()
    {
        OnInvoked?.Invoke();
    }
}
