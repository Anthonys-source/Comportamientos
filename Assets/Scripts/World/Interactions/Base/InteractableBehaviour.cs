using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : MonoBehaviour
{
    public event Action<ID> OnInteraction;
    public event Action<ID> OnCompleted;

    public event Action<InteractableBehaviour> OnDestroyed;

    [HideInInspector] public EntityID m_EntityID;


    private void Awake()
    {
        m_EntityID = GetComponent<EntityID>();
    }

    public void Interact(ID InteracterID)
    {
        OnInteraction?.Invoke(InteracterID);
    }

    public void CompleteInteraction(ID interacterID)
    {
        OnCompleted?.Invoke(interacterID);
    }


    public void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
