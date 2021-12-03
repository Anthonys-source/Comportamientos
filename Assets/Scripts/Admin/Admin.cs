using System.Collections;
using UnityEngine;


public class Admin : MonoBehaviour
{
    private static Admin s_Admin;

    [SerializeField] private GameObjectToEntitiesConversionManager _gameObjectEntities;
    [SerializeField] private ComponentRegistry m_ComponentsRegistry = new ComponentRegistry();
    [SerializeField] private GameDataLoader m_GameDataLoader = new GameDataLoader();
    [SerializeField] private EventSystem m_GameEventSystem = new EventSystem();
    private SystemsManager m_SystemsManager = new SystemsManager();


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void BootLoader()
    {
        s_Admin = FindObjectOfType<Admin>();
        s_Admin.Initialize();
    }

    public void Initialize()
    {
        m_GameEventSystem.Initialize();
        m_ComponentsRegistry.Initialize();
        m_GameDataLoader.LoadData();
        _gameObjectEntities.Initialize(m_ComponentsRegistry);
        m_SystemsManager.Initialize(m_ComponentsRegistry, m_GameEventSystem);
    }

    public void Update()
    {
        m_SystemsManager.Update(Time.deltaTime);
    }
}
