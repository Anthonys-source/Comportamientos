using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EntityID))]
public class CharacterEntity : MonoBehaviour
{
    private EntityID m_EntityID;

    [Header("Component Data")]
    public CharacterComponent m_CharComponent;
    public MoodComponent m_MoodComponent;
    public InventoryComponent m_InventoryComponent;


    private void Awake()
    {
        m_EntityID = GetComponent<EntityID>();
        ComponentRegistry registry = ComponentRegistry.GetInst();
        m_CharComponent = registry.GetComponentFromEntity<CharacterComponent>(m_EntityID.GetInstID());
        m_MoodComponent = registry.GetComponentFromEntity<MoodComponent>(m_EntityID.GetInstID());
        m_InventoryComponent = registry.GetComponentFromEntity<InventoryComponent>(m_EntityID.GetInstID());

        m_CharComponent.m_WalkingSpeed = 10;
    }

    private void Update()
    {
        m_CharComponent.m_Position = transform.position;
    }
}
