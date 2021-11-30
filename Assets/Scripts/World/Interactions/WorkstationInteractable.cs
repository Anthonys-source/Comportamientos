using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableBehaviour))]
public class WorkstationInteractable : MonoBehaviour
{
    [SerializeField] private float m_InteractionDuration = 1.5f;
    [SerializeField] private MeshRenderer m_Renderer;
    [SerializeField] private WorkstationProgressMeter m_Meter;

    [HideInInspector] public EntityID entityID;

    private InteractableBehaviour m_Interactable;
    private EventChannel<InventoryItemEvtArgs> m_AddItemEventChannel;


    private void Awake()
    {
        entityID = GetComponent<EntityID>();
        m_Interactable = GetComponent<InteractableBehaviour>();
        m_Interactable.OnInteraction += Work;
    }

    private void Work(ID interacterID)
    {
        StopAllCoroutines();
        StartCoroutine(WorkRoutine(interacterID));
    }

    private IEnumerator WorkRoutine(ID interacterID)
    {
        if (m_InteractionDuration > 0.0f)
        {
            float t = 0;
            m_Meter.ShowMeter();
            while (t < m_InteractionDuration)
            {
                m_Meter.UpdateMeter(t / m_InteractionDuration);
                yield return new WaitForSeconds(0.05f);
                t += 0.05f;
            }
        }
        m_Interactable.CompleteInteraction(interacterID);
        m_Meter.HideMeter();
        StartCoroutine(ColorFlashAnimation());
    }

    public IEnumerator ColorFlashAnimation()
    {
        var propBlock = new MaterialPropertyBlock();
        var wait = new WaitForSeconds(0.01f);
        float t = 0;

        m_Renderer.GetPropertyBlock(propBlock);

        Color baseCol = m_Renderer.material.color;
        Color targetCol = Color.white;

        while (t <= 1.0f)
        {
            Color currCol = Color.Lerp(baseCol, targetCol, t);
            propBlock.SetColor("_BaseColor", currCol);
            m_Renderer.SetPropertyBlock(propBlock);

            t += 0.1f; ;
            yield return wait;
        }

        while (t > 0.0f)
        {
            Color currCol = Color.Lerp(baseCol, targetCol, t);
            propBlock.SetColor("_BaseColor", currCol);
            m_Renderer.SetPropertyBlock(propBlock);

            t -= 0.1f;
            yield return wait;
        }
    }
}
