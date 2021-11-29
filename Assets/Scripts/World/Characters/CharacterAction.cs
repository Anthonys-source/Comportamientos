using System;

public abstract class CharacterAction
{
    public event Action OnStartEvent;
    public event Action OnCanceledEvent;
    public event Action OnFinishEvent;


    public void Start()
    {
        OnStart();
        OnStartEvent?.Invoke();
    }

    public void Update(float deltaTime)
    {
        OnUpdate(deltaTime);
    }

    public void Cancel()
    {
        OnCalceled();
        OnCanceledEvent?.Invoke();
    }

    protected void Finish()
    {
        OnFinishEvent?.Invoke();
    }

    protected abstract void OnStart();

    protected abstract void OnUpdate(float deltaTime);

    protected abstract void OnCalceled();
}
