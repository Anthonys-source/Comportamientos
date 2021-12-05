using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kid : MonoBehaviour {

    #region variables

    private StateMachineEngine Kid_FSM;
    

    private PushPerception PushPerception;
    private ValuePerception SeMueve;
    private ValuePerception EnClase;
    private AndPerception AndPerception;
    private AndPerception IraclasePerception;
    private PushPerception PushPerception1;
    private ValuePerception EstaComiendo;
    private ValuePerception EnComedor;
    private AndPerception AndPerception1;
    private AndPerception AcomerPerception;
    private ValuePerception EstaHaciendoDeberes;
    private ValuePerception EstaDurmiendo;
    private AndPerception AdormirPerception;
    private PushPerception PushPerception2;
    private ValuePerception EnCasa;
    private AndPerception AndPerception2;
    private AndPerception AndPerception3;
    private AndPerception AcasaPerception;
    private State Durmiendo;
    private State Estudiando;
    private State Comiendo;
    private State Haciendodeberes;

    //Place your variables here
    private bool isMoving;
    private bool inClass;
    private bool isEating;
    private bool inCafeteria;
    private bool inHouse;
    private bool isDoingHomework;
    private bool isSleeping;

    private CharacterActions m_Actions;
    private CharacterBlackboard m_Blackboard;
    private CharacterWaypoints m_Waypoints;
    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        Kid_FSM = new StateMachineEngine(false);
        m_Actions = GetComponent<CharacterActions>();
        m_Blackboard = GetComponent<CharacterBlackboard>();
        m_Waypoints = GetComponent<CharacterWaypoints>();

        CreateStateMachine();
    }
    
    
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        PushPerception = Kid_FSM.CreatePerception<PushPerception>();
        SeMueve = Kid_FSM.CreatePerception<ValuePerception>(() => isMoving = false /*Replace this with a boolean function*/);
        EnClase = Kid_FSM.CreatePerception<ValuePerception>(() => inClass = false /*Replace this with a boolean function*/);
        AndPerception = Kid_FSM.CreateAndPerception<AndPerception>(SeMueve, EnClase);
        IraclasePerception = Kid_FSM.CreateAndPerception<AndPerception>(PushPerception, AndPerception);
        
        PushPerception1 = Kid_FSM.CreatePerception<PushPerception>();
        EstaComiendo = Kid_FSM.CreatePerception<ValuePerception>(() => isEating = false /*Replace this with a boolean function*/);
        EnComedor = Kid_FSM.CreatePerception<ValuePerception>(() => inCafeteria = false /*Replace this with a boolean function*/);
        AndPerception1 = Kid_FSM.CreateAndPerception<AndPerception>(EstaComiendo, EnComedor);
        AcomerPerception = Kid_FSM.CreateAndPerception<AndPerception>(PushPerception1, AndPerception1);
        
        EstaHaciendoDeberes = Kid_FSM.CreatePerception<ValuePerception>(() => isDoingHomework = false /*Replace this with a boolean function*/);
        EstaDurmiendo = Kid_FSM.CreatePerception<ValuePerception>(() => isSleeping = false /*Replace this with a boolean function*/);
        AdormirPerception = Kid_FSM.CreateAndPerception<AndPerception>(EstaHaciendoDeberes, EstaDurmiendo);

        PushPerception2 = Kid_FSM.CreatePerception<PushPerception>();
        EnCasa = Kid_FSM.CreatePerception<ValuePerception>(() => inHouse = false /*Replace this with a boolean function*/);
        AndPerception2 = Kid_FSM.CreateAndPerception<AndPerception>(SeMueve, EnCasa);
        AndPerception3 = Kid_FSM.CreateAndPerception<AndPerception>(EstaHaciendoDeberes, AndPerception2);
        AcasaPerception = Kid_FSM.CreateAndPerception<AndPerception>(PushPerception2, AndPerception3);
        
        // States
        Durmiendo = Kid_FSM.CreateEntryState("Durmiendo", DurmiendoAction);
        Estudiando = Kid_FSM.CreateState("Estudiando", EstudiandoAction);
        Comiendo = Kid_FSM.CreateState("Comiendo", ComiendoAction);
        Haciendodeberes = Kid_FSM.CreateState("Haciendo deberes", HaciendodeberesAction);
        
        // Transitions
        Kid_FSM.CreateTransition("Ir a clase", Durmiendo, IraclasePerception, Estudiando);
        Kid_FSM.CreateTransition("A comer", Estudiando, AcomerPerception, Comiendo);
        Kid_FSM.CreateTransition("A dormir", Haciendodeberes, AdormirPerception, Durmiendo);
        Kid_FSM.CreateTransition("A casa", Comiendo, AcasaPerception, Haciendodeberes);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        Kid_FSM.Update();
    }

    // Create your desired actions
    
    private void DurmiendoAction()
    {
        Debug.Log("A dormir");
        Dormir();
    }
    
    private void EstudiandoAction()
    {
        Debug.Log("A clase");
        Moverse(ZoneID.SCHOOL, WaypointID.SCHOOL, 1);
        Estudiar();
    }
    
    private void ComiendoAction()
    {
        Debug.Log("Al comedor");
        Moverse(ZoneID.CAFE, WaypointID.CAFE, 2);
    }
    
    private void HaciendodeberesAction()
    {
        Debug.Log("A casa");
        Moverse(ZoneID.HOUSE, WaypointID.HOUSE, 3);
    }

    public void Moverse(ID zone, ID point, int idPoint)
    {
        if (m_Blackboard.m_CurrentZoneID == zone)
            switch (idPoint)
            {
                case 1:
                    inClass = false;
                    break;
                case 2:
                    inCafeteria = false;
                    break;
                case 3:
                    inHouse = false;
                    break;
                default:
                    break;
            }
        else
        {
            var h = m_Actions.MoveTo(m_Waypoints.GetWaypointPosition(point), 1.0f);
            switch (idPoint)
            {
                case 1:
                    h.OnCompletedEvent += () => inClass = true;
                    break;
                case 2:
                    h.OnCompletedEvent += () => Comer();
                    break;
                case 3:
                    h.OnCompletedEvent += () => HacerDeberes();
                    break;
                default:
                    break;
            }
        }
    }

    public void Estudiar()
    {
        Debug.Log("En clase");
    }

    public void Comer()
    {
        Debug.Log("Comiendo");
    }

    public void HacerDeberes()
    {
        Debug.Log("Haciendo deberes");
    }

    public void Dormir()
    {
        Debug.Log("Durmiendo");
    }
}