public class EntityTypeComponent
{
    public ID m_ID;
    public ID m_TypeID;
}

public static class EntityType
{
    public static readonly ID BAKER = new ID("baker");
    public static readonly ID CLIENT = new ID("client");
}