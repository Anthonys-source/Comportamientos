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
    private Vector3 counterPosition;
    private Vector3 homePosition;
    private DayNightCycleComponent dayCicle;
    private Singleton_Bakery_Queue bakeryQueue;

    private void Awake()
    {
        m_Actions = GetComponent<CharacterActions>();
        m_Blackboard = GetComponent<CharacterBlackboard>();
        m_Waypoints = GetComponent<CharacterWaypoints>();
    }

    private void Start()
    {
        dayCicle = ComponentRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>();
        bakeryPosition = m_Waypoints.GetWaypointPosition(new ID("bakery"));
        counterPosition = m_Waypoints.GetWaypointPosition(new ID("counter"));
        homePosition = m_Waypoints.GetWaypointPosition(new ID("home"));
        bakeryQueue = Singleton_Bakery_Queue.Instance;

        // Init State Machine
        CreateStateMachine();
    }

    private void Update()
    {
        m_SM.Update();
    }

    private void CreateStateMachine()
    {
        m_SM = new StateMachineEngine(false);

        State home = m_SM.CreateEntryState("atHome", () => Debug.Log("Sleeping at home..."));
        State houseToBakery = m_SM.CreateState("moving to bakery", MoveToBakery);
        State bakeryToHome = m_SM.CreateState("moving to home", MoveToHome);
        State bakery = m_SM.CreateState("atBakery", () => Debug.Log("Arrived at bakery!"));
        State inQueue = m_SM.CreateState("in queue", EnterQueue);
        State moveToCounter = m_SM.CreateState("moving to counter", MoveToCounter);
        State atCounter = m_SM.CreateState("at counter", () => Debug.Log("Waiting at the counter..."));

        Perception itsBakeryHour = m_SM.CreatePerception<ValuePerception>(CheckTimeToBakery);

        Perception canQueue = m_SM.CreatePerception<ValuePerception>(() => bakeryQueue.EnterQueue(gameObject));
        Perception notQueue = m_SM.CreatePerception<ValuePerception>(() => !bakeryQueue.EnterQueue(gameObject));

        //Vector3 dest = m_Waypoints.GetWaypointPosition(new ID("bakeryTest"));
        //Perception arriveAtBakery = m_SM.CreatePerception<ValuePerception>( () => (dest.x) == transform.position.x && dest.z == transform.position.z);
        Perception arriveAtBakery = m_SM.CreatePerception<ValuePerception>(CheckArrivedAtBakery);
        Perception arriveAtCounter = m_SM.CreatePerception<ValuePerception>(CheckArrivedAtCounter);
        Perception arriveAtHome = m_SM.CreatePerception<ValuePerception>(CheckArrivedAtHome);

        m_SM.CreateTransition("move to bakery", home, itsBakeryHour, houseToBakery);
        m_SM.CreateTransition("arrive bakery", houseToBakery, arriveAtBakery, bakery);
        //m_SM.CreateTransition("move to counter", bakery, notQueue, moveToCounter);
        m_SM.CreateTransition("move to queue", bakery, canQueue, inQueue);
        m_SM.CreateTransition("move home", bakery, notQueue, bakeryToHome);
        m_SM.CreateTransition("arrive home", bakeryToHome, arriveAtHome, home);
    }

    private bool CheckTimeToBakery()
    {
        int hour = dayCicle.m_Hour;
        return hour >= 9 && hour <= 15;
    }

    private bool CheckArrivedAtBakery()
    {
        return transform.position.x == bakeryPosition.x && transform.position.z == bakeryPosition.z;
    }

    private bool CheckArrivedAtHome()
    {
        return transform.position.x == homePosition.x && transform.position.z == homePosition.z;
    }

    private bool CheckArrivedAtCounter()
    {
        return transform.position.x == counterPosition.x && transform.position.z == counterPosition.z;
    }

    private void MoveToBakery()
    {
        m_Actions.MoveTo(bakeryPosition);
    }

    private void MoveToCounter()
    {
        m_Actions.MoveTo(counterPosition);
    }

    private void MoveToHome()
    {
        m_Actions.MoveTo(homePosition);
    }

    private void EnterQueue()
    {
        Debug.Log("Waiting in queue...");
        //bakeryQueue.EnterQueue(gameObject);
    }
}
