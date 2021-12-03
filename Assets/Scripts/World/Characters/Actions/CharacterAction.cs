using System;

public abstract class CharacterAction
{
    public event Action OnStartEvent;
    public event Action OnCanceledEvent;
    public event Action OnCompletedEvent;
    public event Action OnFailedEvent;

    public ActionState State;


    public void Start()
    {
        OnStart();
        OnStartEvent?.Invoke();
        State = ActionState.Running;
    }

    public void Update(float deltaTime)
    {
        OnUpdate(deltaTime);
    }

    public void Cancel()
    {
        OnCalceled();
        OnCanceledEvent?.Invoke();
        State = ActionState.Canceled;
    }

    protected void Finish()
    {
        OnCompletedEvent?.Invoke();
        State = ActionState.Completed;
    }

    protected void Fail()
    {
        OnFailedEvent?.Invoke();
        State = ActionState.Failed;
    }

    protected abstract void OnStart();

    protected abstract void OnUpdate(float deltaTime);

    protected abstract void OnCalceled();
}

public enum ActionState
{
    Running,
    Completed,
    Canceled,
    Failed
}
