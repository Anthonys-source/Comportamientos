using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClientBakerDialogueInteracter : MonoBehaviour
{
    public bool IsCustomerWaiting => m_ClientID.IsInitialized();

    [SerializeField] private float m_InteractionDuration = 1.5f;
    [SerializeField] private WorkstationProgressMeter m_Meter;

    [HideInInspector] public EntityID entityID;

    private InteractableBehaviour m_Interactable;
    private EventChannel<InventoryItemEvtArgs> m_AddItemEventChannel;
    private EventChannel<InventoryItemEvtArgs> m_RemoveItemEventChannel;

    private ID m_BakerID;
    private ID m_ClientID;

    private void Awake()
    {
        entityID = GetComponent<EntityID>();
        m_Interactable = GetComponent<InteractableBehaviour>();
        m_Interactable.OnInteraction += TryToInteract;

        EventSystem.GetInst().GetGlobal().GetEventChannel(EventID.ADD_ITEM_TO_INVENTORY, out m_AddItemEventChannel);
        EventSystem.GetInst().GetGlobal().GetEventChannel(EventID.REMOVE_ITEM_FROM_INVENTORY, out m_RemoveItemEventChannel);
    }

    private void TryToInteract(ID interacterID)
    {
        EntityTypeComponent ent = ComponentRegistry.GetInst().GetComponentFromEntity<EntityTypeComponent>(interacterID);

        if (!m_BakerID.IsInitialized() && ent.m_TypeID == EntityType.BAKER)
        {
            m_BakerID = ent.m_ID;
            StopAllCoroutines(); // This is horrible but it works
            StartCoroutine(DialogateRoutine());
        }

        if (!m_ClientID.IsInitialized() && ent.m_TypeID == EntityType.CLIENT)
        {
            m_ClientID = ent.m_ID;
            StopAllCoroutines();
            StartCoroutine(DialogateRoutine());
        }
    }

    private IEnumerator DialogateRoutine()
    {
        if (m_InteractionDuration > 0.0f)
        {
            float t = 0;
            m_Meter.ShowMeter();
            m_Meter.UpdateMeter(0);
            while (t < m_InteractionDuration)
            {
                yield return new WaitForSeconds(0.05f);
                if (m_BakerID.IsInitialized() && m_ClientID.IsInitialized())
                {
                    m_Meter.UpdateMeter(t / m_InteractionDuration);
                    t += 0.05f;
                }
            }
        }

        m_RemoveItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = m_BakerID, m_ItemID = ItemID.BREAD });
        m_AddItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = m_ClientID, m_ItemID = ItemID.BREAD });

        // Uninitialize IDs
        m_ClientID = new ID();
        m_BakerID = new ID();

        m_Interactable.CompleteInteraction(m_BakerID); // ID is useless
        m_Meter.HideMeter();
    }
}
