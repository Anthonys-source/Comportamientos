public abstract class BaseSystem
{
    public abstract void Initialize(ComponentsRegistry c, EventSystem e);
    public abstract void Update(float timeStep);
}