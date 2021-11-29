using UnityEngine;
using UnityEngine.AI;

public class MoveAction : CharacterAction
{
    private CharacterEntity _entity;
    private NavMeshAgent _navmeshAgent;

    private Vector3 pos;
    private float speedPercentage;


    public void Initialize(CharacterEntity entity, NavMeshAgent navmeshAgent, Vector3 pos, float speedPercentage)
    {
        _entity = entity;
        _navmeshAgent = navmeshAgent;
        this.pos = pos;
        this.speedPercentage = speedPercentage;
    }

    protected override void OnCalceled()
    {
        Debug.Log("Move Action Canceled");
    }

    protected override void OnStart()
    {
        Debug.Log("Move Action Started");
        if (_navmeshAgent.SetDestination(pos))
            _navmeshAgent.speed = _entity.m_CharComponent.m_WalkingSpeed * speedPercentage;
        else
            Finish();
    }

    protected override void OnUpdate(float deltaTime)
    {
        Vector3 planarVec = (_navmeshAgent.transform.position - _navmeshAgent.destination);
        planarVec.y = 0;
        if (planarVec.magnitude < 0.05f)
            Finish();
    }
}
