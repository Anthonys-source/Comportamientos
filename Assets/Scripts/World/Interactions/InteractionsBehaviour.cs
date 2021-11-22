using System.Collections.Generic;
using UnityEngine;

public class InteractionsBehaviour : MonoBehaviour
{
    [SerializeField] private TriggerCallbacksRef _triggerCallbacksRef;

    private List<InteractableBehaviour> m_InteractablesInRange = new List<InteractableBehaviour>();


    public void Interact()
    {
        if (m_InteractablesInRange.Count > 0)
            m_InteractablesInRange[0].Interact();
    }

    public List<InteractableBehaviour> GetInteractablesInRange()
    {
        return m_InteractablesInRange;
    }

    public bool TryInteractWith(InteractableBehaviour interactable)
    {
        if (m_InteractablesInRange.Contains(interactable))
        {
            interactable.Interact();
            return true;
        }
        return false;
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