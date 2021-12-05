using UnityEngine;

public class BakeryController : MonoBehaviour
{
    [SerializeField] private ClientBakerDialogueInteracter _dialogue;
    [SerializeField] private BakeryComponent _bakeryComponent;

    private void Awake()
    {
        _bakeryComponent = ComponentRegistry.GetInst().GetSingletonComponent<BakeryComponent>();
    }

    private void Update()
    {
        _bakeryComponent.m_IsCustomerWatingForBread = _dialogue.IsCustomerWaiting;
    }
}