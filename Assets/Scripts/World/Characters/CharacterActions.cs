using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(InteractionsBehaviour))]
[RequireComponent(typeof(CharacterEntity))]
[RequireComponent(typeof(EntityID))]
public class CharacterActions : MonoBehaviour
{
    public bool InProgress => m_ActionsScheduler.m_InProgress;

    private EntityID _entityID;
    private CharacterEntity _entity;
    private InteractionsBehaviour _interactionsBehaviour;
    private NavMeshAgent _navmeshAgent;

    private ActionsScheduler m_ActionsScheduler = new ActionsScheduler();


    private void Awake()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _interactionsBehaviour = GetComponent<InteractionsBehaviour>();
        _entity = GetComponent<CharacterEntity>();
    }

    public void Update()
    {
        m_ActionsScheduler.Update();
    }


    public void MoveTo(Vector3 pos, float speedPercentage)
    {
        var action = new MoveAction(); // Big GC
        action.Initialize(_entity, _navmeshAgent, pos, speedPercentage);
        m_ActionsScheduler.AddAction(action);
    }

    public void TryInteractWith(ID interactableID)
    {
        var i = _interactionsBehaviour.GetInteractablesInRange();
        for (int j = 0; j < i.Count; j++)
            if (i[j].m_EntityID.GetID() == interactableID)
                _interactionsBehaviour.TryInteractWith(i[j]);
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

    public void InteractWithClosest()
    {
        var action = new InteractAction();
        action.Initialize(_interactionsBehaviour);
        m_ActionsScheduler.AddAction(action);
    }
}


public class ActionsScheduler
{
    private Queue<CharacterAction> m_Actions = new Queue<CharacterAction>();
    private CharacterAction m_CurrentAction;
    public bool m_InProgress = false;


    private void RemoveCurrentAction()
    {
        if (m_CurrentAction != null)
            m_CurrentAction.OnFinishEvent -= RemoveCurrentAction;
        m_Actions.Dequeue();
        m_CurrentAction = null;
        m_InProgress = false;
    }

    public void Update()
    {
        // Very Unoptimized
        if (m_Actions.Count > 0 && m_CurrentAction == null)
        {
            m_CurrentAction = m_Actions.Peek();
            m_CurrentAction.OnFinishEvent += RemoveCurrentAction;
            m_CurrentAction.Start();
            m_InProgress = true;
        }
        if (m_CurrentAction != null)
            m_CurrentAction.Update(Time.deltaTime);
    }

    public void AddAction(CharacterAction action)
    {
        m_Actions.Enqueue(action);
    }

    // TODO: Override all actions methods
}