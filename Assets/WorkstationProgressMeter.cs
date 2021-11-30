using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationProgressMeter : MonoBehaviour
{
    [SerializeField] private Transform m_MeterContainer;
    [SerializeField] private Transform m_MeterTransform;
    [SerializeField] private MeshRenderer m_MeterRenderer;
    private MaterialPropertyBlock m_PropBlock;


    private void Awake()
    {
        m_PropBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        HideMeter();
        UpdateMeter(0);
    }

    public void ShowMeter()
    {
        m_MeterContainer.gameObject.SetActive(true);
    }

    public void HideMeter()
    {
        m_MeterContainer.gameObject.SetActive(false);
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
