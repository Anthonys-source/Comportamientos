public class InteractAction : CharacterAction
{
    private InteractionsBehaviour _interactionsBehaviour;


    public void Initialize(InteractionsBehaviour interactionsBehaviour)
    {
        _interactionsBehaviour = interactionsBehaviour;
    }

    protected override void OnCalceled()
    {
    }

    protected override void OnStart()
    {
        _interactionsBehaviour.Interact();
        Finish();
    }

    protected override void OnUpdate(float deltaTime)
    {
    }
}