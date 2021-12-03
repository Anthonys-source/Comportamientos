public abstract class BaseSystem
{
    public abstract void Initialize(ComponentRegistry c, EventSystem e);
    public abstract void Update(float timeStep);
}