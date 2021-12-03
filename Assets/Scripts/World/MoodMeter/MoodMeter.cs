using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodMeter : MonoBehaviour
{
    [SerializeField] private EntityID m_EntityID;
    [SerializeField] private Transform m_MeterTransform;
    [SerializeField] private MeshRenderer m_MeterRenderer;
    private MaterialPropertyBlock m_PropBlock;
    private MoodComponent m_MoodComp;
    private int m_PreviousMood = 0;


    private void Awake()
    {
        m_MoodComp = ComponentRegistry.GetInst().GetComponentFromEntity<MoodComponent>(m_EntityID.GetInstID());
        m_PropBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        UpdateMeter(m_MoodComp.m_MoodValue);
    }

    private void Update()
    {
        if ((int)m_MoodComp.m_MoodValue != m_PreviousMood)
        {
            UpdateMeter(m_MoodComp.m_MoodValue / 100.0f);
            m_PreviousMood = (int)m_MoodComp.m_MoodValue;
        }
    }

    public void UpdateMeter(float percentage)
    {
        Vector3 scale = m_MeterTransform.localScale;
        scale.y = percentage;
        m_MeterTransform.localScale = scale;

        // Change Color
        m_MeterRenderer.GetPropertyBlock(m_PropBlock);
        Color currCol = Color.Lerp(Color.green, Color.red, percentage);
        m_PropBlock.SetColor("_BaseColor", currCol);
        m_MeterRenderer.SetPropertyBlock(m_PropBlock);
    }
}
