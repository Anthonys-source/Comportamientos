using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientNormalBehaviour : MonoBehaviour
{
    private CharacterActions m_Actions;
    private CharacterBlackboard m_Blackboard;
    private CharacterWaypoints m_Waypoints;

    private StateMachineEngine m_SM;

    private Vector3 bakeryPosition;
    private DayNightCycleComponent dayCicle;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActions>();
        m_Blackboard = GetComponent<CharacterBlackboard>();
        m_Waypoints = GetComponent<CharacterWaypoints>();

        
    }

    private void Start()
    {
        // Init State Machine
        CreateStateMachine();

        bakeryPosition = m_Waypoints.GetWaypointPosition(new ID("bakery"));

        dayCicle = ComponentRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>();
    }

    private void Update()
    {
        m_SM.Update();
    }

    private void CreateStateMachine()
    {
        m_SM = new StateMachineEngine(false);

        State house = m_SM.CreateEntryState("atHouse", () => Debug.Log("Sleeping at home..."));
        State houseToBakery = m_SM.CreateState("moving to bakery", MoveToBakery);
        State bakery = m_SM.CreateState("atBakery", () => Debug.Log("Arrived at bakery!"));

        Perception itsBakeryHour = m_SM.CreatePerception<ValuePerception>(CheckTimeToBakery);

        //Vector3 dest = m_Waypoints.GetWaypointPosition(new ID("bakeryTest"));
        //Perception arriveAtBakery = m_SM.CreatePerception<ValuePerception>( () => (dest.x) == transform.position.x && dest.z == transform.position.z);
        Perception arriveAtBakery = m_SM.CreatePerception<ValuePerception>(CheckArrivedAtBakery);

        m_SM.CreateTransition("house to bakery", house, itsBakeryHour, houseToBakery);
        m_SM.CreateTransition("bakery", houseToBakery, arriveAtBakery, bakery);
    }

    private bool CheckTimeToBakery()
    {
        //int hour = dayCicle.m_Hour;
        int hour = ComponentRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>().m_Hour;
        return hour >= 9 && hour <= 15;
    }

    private bool CheckArrivedAtBakery()
    {
        return transform.position.x == bakeryPosition.x && transform.position.z == bakeryPosition.z;
    }

    private void MoveToBakery()
    {
        m_Actions.MoveTo(m_Waypoints.GetWaypointPosition(new ID("bakery")));
    }
}
