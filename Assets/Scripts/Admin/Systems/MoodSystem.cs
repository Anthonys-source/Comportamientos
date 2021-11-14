using UnityEngine;

public class MoodSystem
{
    private ComponentsContainer<MoodComponent> m_MoodComponents;

    public void Initialize(ComponentsContainer<MoodComponent> moodComponents)
    {
        m_MoodComponents = moodComponents;
    }


    public void AddMood(ID entityID, int value)
    {
#if UNITY_EDITOR
        if (value < 0.0f)
            Debug.LogWarning("Adding a negative mood value");
#endif

        var c = m_MoodComponents.GetFromEntity(entityID);
        c.m_MoodValue += value;
        c.m_MoodValue = Mathf.Clamp(c.m_MoodValue, -100.0f, 100.0f);
    }

    public void SubtractMood(ID entityID, int value)
    {
#if UNITY_EDITOR
        if (value < 0.0f)
            Debug.LogWarning("Subtracting a negative mood value");
#endif

        var c = m_MoodComponents.GetFromEntity(entityID);
        c.m_MoodValue -= value;
        c.m_MoodValue = Mathf.Clamp(c.m_MoodValue, -100.0f, 100.0f);
    }
}
