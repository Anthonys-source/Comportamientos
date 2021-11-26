using System.Collections.Generic;
using UnityEngine;

public class InteractionsBehaviour : MonoBehaviour
{
    [SerializeField] private TriggerCallbacksRef _triggerCallbacksRef;
    private EntityID m_EntityID;

    private List<InteractableBehaviour> m_InteractablesInRange = new List<InteractableBehaviour>();


    public void Interact()
    {
        if (m_InteractablesInRange.Count > 0)
            m_InteractablesInRange[0].Interact(m_EntityID.GetID());
    }

    public List<InteractableBehaviour> GetInteractablesInRange()
    {
        return m_InteractablesInRange;
    }

    public bool TryInteractWith(InteractableBehaviour interactable)
    {
        if (m_InteractablesInRange.Contains(interactable))
        {
            interactable.Interact(m_EntityID.GetID());
            return true;
        }
        return false;
    }

    private void Awake()
    {
        m_EntityID = GetComponent<EntityID>();
    }

    private void OnEnable()
    {
        _triggerCallbacksRef.OnTriggerEnterCallback += OnTriggerEnterHandle;
        _triggerCallbacksRef.OnTriggerExitCallback += OnTriggerExitHandle;
    }

    private void OnDisable()
    {
        _triggerCallbacksRef.OnTriggerEnterCallback -= OnTriggerEnterHandle;
        _triggerCallbacksRef.OnTriggerExitCallback -= OnTriggerExitHandle;
    }

    private void OnTriggerEnterHandle(Collider other)
    {
        if (other.TryGetComponent(out InteractableBehaviour interactable))
        {
            m_InteractablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExitHandle(Collider other)
    {
        if (other.TryGetComponent(out InteractableBehaviour interactable))
        {
            m_InteractablesInRange.Remove(interactable);
        }
    }
}