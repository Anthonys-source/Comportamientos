public class TryInteractAction : CharacterAction
{
    private InteractionsBehaviour _interactionsBehaviour;
    private ID _interactableID;

    private InteractableBehaviour _interactable;


    public void Initialize(InteractionsBehaviour interactionsBehaviour, ID interactableID)
    {
        _interactionsBehaviour = interactionsBehaviour;
        _interactableID = interactableID;
    }

    protected override void OnCalceled()
    {
    }

    protected override void OnStart()
    {
        bool foundInteractable = false;

        var i = _interactionsBehaviour.GetInteractablesInRange();
        for (int j = 0; j < i.Count; j++)
            if (i[j].m_EntityID.GetTypeID() == _interactableID)
            {
                _interactable = i[j];
                _interactable.OnCompleted += TryInteractAction_OnCompleted;
                _interactionsBehaviour.TryInteractWith(_interactable);
                foundInteractable = true;
                break;
            }

        if (!foundInteractable)
        {
            Fail();
        }
    }

    private void TryInteractAction_OnCompleted(ID obj)
    {
        _interactable.OnCompleted -= TryInteractAction_OnCompleted;
        Finish();
    }

    protected override void OnUpdate(float deltaTime)
    {
    }
}