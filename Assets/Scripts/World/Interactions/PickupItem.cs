using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableBehaviour))]
public class PickupItem : MonoBehaviour
{
    [HideInInspector] public EntityID entityID;

    private InteractableBehaviour m_Interactable;
    private EventChannel<InventoryItemEvtArgs> m_AddItemEventChannel;

    private void Awake()
    {
        entityID = GetComponent<EntityID>();
        m_Interactable = GetComponent<InteractableBehaviour>();
        m_Interactable.OnInteraction += Pickup;
        EventSystem.GetInst().GetGlobal().GetEventChannel(new ID("add_item_to_inventory"), out m_AddItemEventChannel);
    }

    private void Pickup(ID interacterID)
    {
        m_Interactable.CompleteInteraction(interacterID);
        m_AddItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = interacterID, m_ItemID = entityID.GetID() });
        Destroy(gameObject);
    }
}
