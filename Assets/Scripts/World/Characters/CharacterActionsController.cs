using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(InteractionsBehaviour))]
[RequireComponent(typeof(CharacterEntity))]
[RequireComponent(typeof(EntityID))]
public class CharacterActionsController : MonoBehaviour
{
    private EntityID _entityID;
    private CharacterEntity _entity;
    private InteractionsBehaviour _interactionsBehaviour;
    private NavMeshAgent _navmeshAgent;


    public event Action<Vector3, float> OnMoveToStarted;
    public event Action<Vector3, float> OnMoveToChanged;
    public event Action<Vector3, float> OnMoveToArrived;

    public event Action<InteractableBehaviour> OnInteractionStarted;
    public event Action<InteractableBehaviour> OnInteractionInterrupted;
    public event Action<InteractableBehaviour> OnInteractionFinished;


    private void Awake()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _interactionsBehaviour = GetComponent<InteractionsBehaviour>();
        _entity = GetComponent<CharacterEntity>();
    }

    public void TryPickupItem(ID itemID)
    {
        TryInteractWith(itemID);
    }

    public void MoveTo(Vector3 pos, float speedPercentage)
    {
        OnMoveToStarted.Invoke(pos, speedPercentage);
        _navmeshAgent.SetDestination(pos);
        _navmeshAgent.speed = _entity.m_CharComponent.m_WalkingSpeed * speedPercentage;
    }

    public void TryInteractWith(ID interactableID)
    {
        var i = _interactionsBehaviour.GetInteractablesInRange();
        for (int j = 0; j < i.Count; j++)
            if (i[j].EntityID == interactableID)
            {
                OnInteractionStarted.Invoke(i[j]);
                _interactionsBehaviour.TryInteractWith(i[j]);
                OnInteractionFinished.Invoke(i[j]);
            }
    }

    public void LookAt(Vector3 dir)
    {
        gameObject.transform.rotation *= Quaternion.FromToRotation(gameObject.transform.forward, dir);
    }

    public void GetAngry()
    {
        var comp = ComponentsRegistry.GetInst().GetComponentFromEntity<MoodComponent>(_entityID.GetID()); // Should Cache Component
        comp.m_MoodValue -= 10;
    }

    public void GetHappy()
    {
        var comp = ComponentsRegistry.GetInst().GetComponentFromEntity<MoodComponent>(_entityID.GetID()); // Should Cache Component
        comp.m_MoodValue += 10;
    }

    public void Jump() { }


    public void InteractWithClosestObjectAt(Vector3 pos) { }
    public void InteractWithClosest()
    {
        _interactionsBehaviour.Interact();
    }
}
