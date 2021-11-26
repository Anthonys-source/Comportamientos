public abstract class BaseSystem
{
    public abstract void Initialize(ComponentsRegistry c, GameEventSystem e);
    public abstract void Update(float timeStep);
}