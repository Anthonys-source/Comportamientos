public class InteractWithRandomAction : CharacterAction
{
    private InteractionsBehaviour _interactionsBehaviour;

    private InteractableBehaviour _interactable;


    public void Initialize(InteractionsBehaviour interactionsBehaviour)
    {
        _interactionsBehaviour = interactionsBehaviour;
    }

    protected override void OnCalceled()
    {
    }

    protected override void OnStart()
    {
        bool foundInteractable = false;

        var i = _interactionsBehaviour.GetInteractablesInRange();
        if (i.Count > 0)
        {
            _interactable = i[0];
            _interactable.OnCompleted += TryInteractAction_OnCompleted;
            _interactionsBehaviour.TryInteractWith(_interactable);
            foundInteractable = true;
        }

        if (!foundInteractable)
            Finish();
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
