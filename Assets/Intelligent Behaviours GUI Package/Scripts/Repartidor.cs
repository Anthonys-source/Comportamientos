using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Repartidor : MonoBehaviour {

    #region variables

    private StateMachineEngine Repartidor_FSM;
    

    private PushPerception PushPerception;
    private MoviendoPerception MoviendoPerception;
    private AndPerception AlafabricaPerception;
    private EstaEnFabricaPerception EstaEnFabricaPerception;
    private EstaRepartiendoPerception EstaRepartiendoPerception;
    private AndPerception AtrabajarPerception;
    private PushPerception PushPerception1;
    private ComiendoYBebiendoPerception ComiendoYBebiendoPerception;
    private EstaEnBarPerception EstaEnBarPerception;
    private AndPerception AndPerception;
    private AndPerception AcomerPerception;
    private PushPerception PushPerception2;
    private EnRedesSocialesPerception EnRedesSocialesPerception;
    private EstaEnCasaPerception EstaEnCasaPerception;
    private AndPerception AndPerception1;
    private AndPerception AcasaPerception;
    private PushPerception PushPerception3;
    private DurmiendoPerception DurmiendoPerception;
    private AndPerception AdormirPerception;
    private State Durmiendo;
    private State Caminoalafabrica;
    private State Repartiendo;
    private State Comiendoybebiendo;
    private State RedesSociales;
    
    //Place your variables here
    [SerializeField] private bool isMoving;
    private bool isSleeping;
    [SerializeField] private bool inFactory;
    [SerializeField] private bool inBar;
    [SerializeField] private bool inHouse;
    [SerializeField] private bool inSM;
    [SerializeField] private bool isEating;
    [SerializeField] private bool isDelivering;

    private CharacterActions m_Actions;
    private CharacterBlackboard m_Blackboard;
    private CharacterWaypoints m_Waypoints;
    #endregion variables

    // Start is called before the first frame update
    private void Start()
    {
        Repartidor_FSM = new StateMachineEngine(false);
        isMoving = false;
        isSleeping = false;
        inHouse = false;
        inFactory = false;
        inSM = false;
        isEating = false;
        isDelivering = false;
        inBar = false;

        m_Actions = GetComponent<CharacterActions>();
        m_Blackboard = GetComponent<CharacterBlackboard>();
        m_Waypoints = GetComponent<CharacterWaypoints>();

        CreateStateMachine();
    }
    
    
    private void CreateStateMachine()
    {
        // Perceptions
        // Modify or add new Perceptions, see the guide for more
        PushPerception = Repartidor_FSM.CreatePerception<PushPerception>();
        MoviendoPerception = Repartidor_FSM.CreatePerception<MoviendoPerception>(new MoviendoPerception());
        AlafabricaPerception = Repartidor_FSM.CreateAndPerception<AndPerception>(PushPerception, MoviendoPerception);
        EstaEnFabricaPerception = Repartidor_FSM.CreatePerception<EstaEnFabricaPerception>(new EstaEnFabricaPerception());
        EstaRepartiendoPerception = Repartidor_FSM.CreatePerception<EstaRepartiendoPerception>(new EstaRepartiendoPerception());
        AtrabajarPerception = Repartidor_FSM.CreateAndPerception<AndPerception>(EstaEnFabricaPerception, EstaRepartiendoPerception);
        PushPerception1 = Repartidor_FSM.CreatePerception<PushPerception>();
        ComiendoYBebiendoPerception = Repartidor_FSM.CreatePerception<ComiendoYBebiendoPerception>(new ComiendoYBebiendoPerception());
        EstaEnBarPerception = Repartidor_FSM.CreatePerception<EstaEnBarPerception>(new EstaEnBarPerception());
        AndPerception = Repartidor_FSM.CreateAndPerception<AndPerception>(ComiendoYBebiendoPerception, EstaEnBarPerception);
        AcomerPerception = Repartidor_FSM.CreateAndPerception<AndPerception>(PushPerception1, AndPerception);
        PushPerception2 = Repartidor_FSM.CreatePerception<PushPerception>();
        EnRedesSocialesPerception = Repartidor_FSM.CreatePerception<EnRedesSocialesPerception>(new EnRedesSocialesPerception());
        EstaEnCasaPerception = Repartidor_FSM.CreatePerception<EstaEnCasaPerception>(new EstaEnCasaPerception());
        AndPerception1 = Repartidor_FSM.CreateAndPerception<AndPerception>(EnRedesSocialesPerception, EstaEnCasaPerception);
        AcasaPerception = Repartidor_FSM.CreateAndPerception<AndPerception>(PushPerception2, AndPerception1);
        PushPerception3 = Repartidor_FSM.CreatePerception<PushPerception>();
        DurmiendoPerception = Repartidor_FSM.CreatePerception<DurmiendoPerception>(new DurmiendoPerception());
        AdormirPerception = Repartidor_FSM.CreateAndPerception<AndPerception>(PushPerception3, DurmiendoPerception);
        
        // States
        Durmiendo = Repartidor_FSM.CreateEntryState("Durmiendo", DurmiendoAction);
        Caminoalafabrica = Repartidor_FSM.CreateState("Camino a la fabrica", CaminoalafabricaAction);
        Repartiendo = Repartidor_FSM.CreateState("Repartiendo", RepartiendoAction);
        Comiendoybebiendo = Repartidor_FSM.CreateState("Comiendo y bebiendo", ComiendoybebiendoAction);
        RedesSociales = Repartidor_FSM.CreateState("Redes Sociales", RedesSocialesAction);
        
        // Transitions
        Repartidor_FSM.CreateTransition("A la fabrica", Durmiendo, AlafabricaPerception, Caminoalafabrica);
        Repartidor_FSM.CreateTransition("A trabajar", Caminoalafabrica, AtrabajarPerception, Repartiendo);
        Repartidor_FSM.CreateTransition("A comer", Repartiendo, AcomerPerception, Comiendoybebiendo);
        Repartidor_FSM.CreateTransition("A casa", Comiendoybebiendo, AcasaPerception, RedesSociales);
        Repartidor_FSM.CreateTransition("A dormir", RedesSociales, AdormirPerception, Durmiendo);
        
        // ExitPerceptions
        
        // ExitTransitions
        
    }

    // Update is called once per frame
    private void Update()
    {
        Repartidor_FSM.Update();

        /*if (Input.GetMouseButtonDown(0))
        {
            Repartidor_FSM.Fire("A la fabrica");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Repartidor_FSM.Fire("A trabajar");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Repartidor_FSM.Fire("A comer");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Repartidor_FSM.Fire("A casa");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Repartidor_FSM.Fire("A dormir");
        }*/
        if (!MoviendoPerception.Check(this.gameObject) && ComponentRegistry.GetInst().GetSingletonComponent<DayNightCycleComponent>().m_Day)
        {
            Repartidor_FSM.Fire("A la fabrica");
        }else if(!EstaRepartiendoPerception.Check(this.gameObject) && EstaEnFabricaPerception.Check(this.gameObject))
        {
            Repartidor_FSM.Fire("A trabajar");
        }else if(!ComiendoYBebiendoPerception.Check(this.gameObject) && !EstaEnBarPerception.Check(this.gameObject))
        {
            Repartidor_FSM.Fire("A comer");
        }else if(!EnRedesSocialesPerception.Check(this.gameObject) && !EstaEnCasaPerception.Check(this.gameObject))
        {
            Repartidor_FSM.Fire("A casa");
        }else if (!DurmiendoPerception.Check(this.gameObject))
        {
            Repartidor_FSM.Fire("A dormir");
        }
    }

    // Create your desired actions
    
    private void DurmiendoAction()
    {
        Debug.Log("Durmiendo");
        Dormir();
    }
    
    private void CaminoalafabricaAction()
    {
        //m_ArrivedAtBakery = ReturnValues.Running;
        Debug.Log("De camino al trabajo");
        isMoving = true;
        Moverse(ZoneID.FACTORY, WaypointID.FACTORY, 1);
    }
    
    private void RepartiendoAction()
    {
        Debug.Log("A repartir");
        isDelivering = true;
        transform.GetChild(2).gameObject.SetActive(true);
        Repartir();
    }
    
    private void ComiendoybebiendoAction()
    {
        Debug.Log("Rica comida");
        transform.GetChild(2).gameObject.SetActive(false);
        Moverse(ZoneID.BAR, WaypointID.BAR, 2);
        ComerYBeber();
    }
    
    private void RedesSocialesAction()
    {
        Debug.Log("Veamos Twitter");
        Moverse(ZoneID.HOUSE, WaypointID.HOUSE, 3);
        ConsultarSM();
    }

    public void Repartir()
    {
        isDelivering = true;
        inFactory = false;
        var h = m_Actions.MoveTo(m_Waypoints.GetWaypointPosition(WaypointID.BAKERY1), 1.0f);
        h.OnCompletedEvent += () => m_Actions.MoveTo(m_Waypoints.GetWaypointPosition(WaypointID.BAKERY2), 1.0f)
        .OnCompletedEvent += () => m_Actions.MoveTo(m_Waypoints.GetWaypointPosition(WaypointID.BAKERY3), 1.0f)
        .OnCompletedEvent += () => transform.GetChild(2).gameObject.SetActive(false);
        /*isDelivering = false;*/
    }

    public void ComerYBeber()
    {
        Debug.Log("Rico filete");
        //this.gameObject.SetActive(false);
        isDelivering = false;
        isEating = false;
        inBar = true;
    }

    public void ConsultarSM()
    {
        //this.gameObject.SetActive(false);
    }

    public void Dormir()
    {
        
    }

    public void Moverse(ID zone, ID point, int idPoint)
    {
        if (m_Blackboard.m_CurrentZoneID == zone)
            switch (idPoint)
            {
                case 1:
                    inFactory = false;
                    break;
                case 2:
                    inBar = false;
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
                    h.OnCompletedEvent += () => inFactory = true;
                                                //isMoving = false;
                    break;
                case 2:
                    h.OnCompletedEvent += () => inBar = true;
                    break;
                case 3:
                    h.OnCompletedEvent += () => inHouse = true;
                    break;
                default:
                    break;
            }
        }
    }
    
    public bool getInFactory()
    {
        return inFactory;
    }
    public bool getInHouse()
    {
        return inHouse;
    }
    public bool getSleeping()
    {
        return isSleeping;
    }
    public bool getMoving()
    {
        return isMoving;
    }
    public bool getInBar()
    {
        return inBar;
    }
    public bool getSM()
    {
        return inSM;
    }
    public bool getEating()
    {
        return isEating;
    }
    public bool getDelivering()
    {
        return isDelivering;
    }

}