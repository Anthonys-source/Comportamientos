using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : MonoBehaviour
{
    public event Action<ID> OnInteraction;
    public event Action<InteractableBehaviour> OnDestroyed;

    [HideInInspector] public EntityID m_EntityID;
    [SerializeField] private MeshRenderer m_Renderer;

    private void Awake()
    {
        m_EntityID = GetComponent<EntityID>();
    }

    public void Interact(ID InteracterID)
    {
        Debug.Log($"Interacted with [{gameObject.name}]");
        OnInteraction?.Invoke(InteracterID);

        // Interaction Animation
        StopAllCoroutines();
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

    public void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
