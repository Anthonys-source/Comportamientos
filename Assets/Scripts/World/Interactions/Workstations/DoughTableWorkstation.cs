using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughTableWorkstation : MonoBehaviour
{
    [SerializeField] private float m_InteractionDuration = 1.5f;
    [SerializeField] private WorkstationProgressMeter m_Meter;

    [HideInInspector] public EntityID entityID;

    private InteractableBehaviour m_Interactable;
    private EventChannel<InventoryItemEvtArgs> m_AddItemEventChannel;
    private EventChannel<InventoryItemEvtArgs> m_RemoveItemEventChannel;


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
        InventoryComponent inv = ComponentRegistry.GetInst().GetComponentFromEntity<InventoryComponent>(interacterID);
        if (inv.HasItem(ItemID.FLOUR, out _) && inv.HasItem(ItemID.WATER, out _) && inv.HasItem(ItemID.YEAST, out _))
        {
            m_RemoveItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = interacterID, m_ItemID = ItemID.FLOUR });
            m_RemoveItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = interacterID, m_ItemID = ItemID.WATER });
            m_RemoveItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = interacterID, m_ItemID = ItemID.YEAST });
            StopAllCoroutines();
            StartCoroutine(WorkRoutine(interacterID));
        }
        else
        {
            m_Interactable.CompleteInteraction(interacterID);
        }
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

        m_AddItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = interacterID, m_ItemID = ItemID.BREAD_DOUGH });
        m_Interactable.CompleteInteraction(interacterID);
        m_Meter.HideMeter();
    }
}
