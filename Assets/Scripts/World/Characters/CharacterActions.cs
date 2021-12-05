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
    private EntityID _entityID;
    private CharacterEntity _entity;
    private InteractionsBehaviour _interactionsBehaviour;
    private NavMeshAgent _navmeshAgent;

    [SerializeField] private ActionsScheduler m_ActionsScheduler = new ActionsScheduler();


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


    public MoveAction MoveTo(Vector3 pos, float speedPercentage)
    {
        var action = new MoveAction(); // Big GC
        action.Initialize(_entity, _navmeshAgent, pos, speedPercentage);
        m_ActionsScheduler.AddAction(action);
        return action;
    }

    public MoveAction MoveTo(Vector3 pos)
    {
        return MoveTo(pos, 1.0f);
    }

    public TryInteractAction TryInteractWith(ID interactableID)
    {
        var action = new TryInteractAction();
        action.Initialize(_interactionsBehaviour, interactableID);
        m_ActionsScheduler.AddAction(action);
        return action;
    }

    public TryInteractAction TryInteractWith2(ID interactableID)
    {
        var action = new TryInteractAction();
        action.Initialize(_interactionsBehaviour, interactableID);
        m_ActionsScheduler.SetAction(action);
        return action;
    }

    public void LookAt(Vector3 dir)
    {
        gameObject.transform.rotation *= Quaternion.FromToRotation(gameObject.transform.forward, dir);
    }

    public void GetAngry()
    {
        var comp = ComponentRegistry.GetInst().GetComponentFromEntity<MoodComponent>(_entityID.GetTypeID()); // Should Cache Component
        comp.m_MoodValue -= 10;
    }

    public void GetHappy()
    {
        var comp = ComponentRegistry.GetInst().GetComponentFromEntity<MoodComponent>(_entityID.GetTypeID()); // Should Cache Component
        comp.m_MoodValue += 10;
    }

    public void Jump() { }

    public void InteractWithClosest()
    {
        var action = new InteractWithRandomAction();
        action.Initialize(_interactionsBehaviour);
        m_ActionsScheduler.AddAction(action);
    }
}


[System.Serializable]
public class ActionsScheduler
{
    private Queue<CharacterAction> m_Actions = new Queue<CharacterAction>();
    private CharacterAction m_CurrentAction;
    public bool m_InProgress = false;


    private void RemoveCurrentAction()
    {
        if (m_CurrentAction != null)
        {
            m_CurrentAction.OnCompletedEvent -= RemoveCurrentAction;
            m_CurrentAction.OnFailedEvent -= RemoveCurrentAction;
        }
        m_Actions.Dequeue();
        m_CurrentAction = null;
        m_InProgress = false;
    }

    public void Update()
    {
        // Very Unoptimized
        if (m_Actions.Count > 0 && m_CurrentAction == null)
        {
            m_InProgress = true;
            m_CurrentAction = m_Actions.Peek();
            m_CurrentAction.OnCompletedEvent += RemoveCurrentAction;
            m_CurrentAction.OnFailedEvent += RemoveCurrentAction;
            m_CurrentAction.Start();
        }
        if (m_CurrentAction != null)
            m_CurrentAction.Update(Time.deltaTime);
    }

    public void AddAction(CharacterAction action)
    {
        m_Actions.Enqueue(action);
    }

    public void SetAction(CharacterAction action)
    {
        CancelAllActions();
        m_Actions.Enqueue(action);
    }

    public void CancelCurrentAction()
    {
        if (m_CurrentAction != null)
            m_CurrentAction.Cancel();
    }

    public void CancelAllActions()
    {
        CancelCurrentAction();
        m_Actions.Clear();
    }

    // TODO: Override all actions methods
}