using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(InteractionsBehaviour))]
[RequireComponent(typeof(CharacterEntity))]
public class CharacterActionsController : MonoBehaviour
{
    private NavMeshAgent _navmeshAgent;
    private InteractionsBehaviour _interactionsBehaviour;
    private CharacterEntity _entity;

    private float m_BaseMovementSpeed = 10.0f;


    // TODO: Populate with more events
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

    }
    public void MoveTo(Vector3 pos, float speedPercentage)
    {
        _navmeshAgent.SetDestination(pos);
        _navmeshAgent.speed = m_BaseMovementSpeed * speedPercentage;
    }
    public void TryInteractWith(ID interactableID) { }
    public void LookAt(Vector3 dir)
    {
        gameObject.transform.rotation *= Quaternion.FromToRotation(gameObject.transform.forward, dir);
    }
    public void GetAngry() { }
    public void GetHappy() { }
    public void Jump() { }


    public void InteractWithClosestObjectAt(Vector3 pos) { }
    public void InteractWithClosest()
    {
        _interactionsBehaviour.Interact();
    }
}
