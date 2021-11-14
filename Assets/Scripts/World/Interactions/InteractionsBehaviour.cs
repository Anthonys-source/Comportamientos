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