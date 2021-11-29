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

    private Queue<CharacterAction> m_Actions = new Queue<CharacterAction>();
    private CharacterAction m_CurrentAction;


    private void Awake()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _interactionsBehaviour = GetComponent<InteractionsBehaviour>();
        _entity = GetComponent<CharacterEntity>();
    }

    private void Update()
    {
        // Very Unoptimized
        if (m_Actions.Count > 0 && m_CurrentAction == null)
        {
            m_CurrentAction = m_Actions.Peek();
            m_CurrentAction.OnFinishEvent += RemoveCurrentAction;
            m_CurrentAction.Start();
        }
        if (m_CurrentAction != null)
            m_CurrentAction.Update(Time.deltaTime);
    }

    private void RemoveCurrentAction()
    {
        if (m_CurrentAction != null)
            m_CurrentAction.OnFinishEvent -= RemoveCurrentAction;
        m_Actions.Dequeue();
        m_CurrentAction = null;
    }

    public void MoveTo(Vector3 pos, float speedPercentage)
    {
        var action = new MoveAction(); // Big GC
        action.Initialize(_entity, _navmeshAgent, pos, speedPercentage);
        m_Actions.Enqueue(action);
    }

    public void TryInteractWith(ID interactableID)
    {
        var i = _interactionsBehaviour.GetInteractablesInRange();
        for (int j = 0; j < i.Count; j++)
            if (i[j].EntityID == interactableID)
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
        m_Actions.Enqueue(action);
    }
}